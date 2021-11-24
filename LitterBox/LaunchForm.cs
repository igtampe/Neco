using Igtampe.Neco.Common;
using Igtampe.Neco.Common.EzTax;
using Igtampe.Neco.Data;
using Igtampe.LitterBox.ItemForms;
using Igtampe.TinyForms;
using Igtampe.LitterBox.ReportGenerators;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Igtampe.LitterBox {
    public partial class LaunchForm : Form {

        private NecoContext NecoDB;
        private int CurrentTab = 0;
        private bool DirtyBit = false;
        private bool Freeze = false;

        private List<User> Users;
        private List<UserType> UserTypes;
        private List<UserAuth> UserAuths;
        private List<Bank> Banks;
        private List<Bank> BanksToAdd; //This is the only list that needs to be maintained to add because Banks have a specified (not generated) ID.
        private List<TaxJurisdiction> Jurisdictions;


        public LaunchForm() {
            InitializeComponent();
            FormClosing += LaunchForm_FormClosing;
            MainPannel.Enabled = false;
            GenerateToolStripMenuItem.Enabled = false;
            Icon = Properties.Resources.MainIco;
            Shown += LaunchForm_Shown;
            JurisdictionBankAccountBox.LostFocus += JurisdictionBankAccountBox_LostFocus;
            UserSearchBox.LostFocus += RefreshToolStripMenuItem_Click;
            ClearBanks();
            ClearUsers();
            ClearJurisdictions();
        }

        private void LaunchForm_Shown(object sender, EventArgs e) { ConnectDisconnectToolStripMenuItem_Click(sender, e); }

        private void LaunchForm_FormClosing(object sender, FormClosingEventArgs e) { NecoDB?.Dispose(); } //Just in case

        private async void MainTabController_SelectedIndexChanged(object sender, EventArgs e) {

            await SaveCurrentTab();

            DirtyBit = false;
            CurrentTab = MainTabController.SelectedIndex;

            await LoadCurrentTab();
        }


        #region Data Operations

        #region Users

        private async Task LoadUsers(string SearchQuery) {

            Busy();

            StatusLabel.Text = "Getting Users...";
            Users?.Clear();
            Users = await NecoDB.User.OrderBy(U => U.ID).Where(
                U=>U.Name.ToLower().Contains(SearchQuery.ToLower())||
                   U.ID.ToLower().Contains(SearchQuery.ToLower()) 
                ).ToListAsync();

            StatusLabel.Text = "Getting Users Auths";
            UserAuths?.Clear();
            UserAuths = await NecoDB.UserAuth.OrderBy(U => U.ID).ToListAsync();

            StatusLabel.Text = "Getting User Types";
            UserTypes?.Clear();
            UserTypes = await NecoDB.UserType.ToListAsync();

            StatusLabel.Text = "Populating...";
            PopulateUsers();
            UserDetailsGroupBox.Enabled = false;

            Unbusy();
        }

        private void ClearUsers() {

            UsersListView.Items.Clear();
            UserDetailsUserTypeComboBox.Items.Clear();
            UserDetailsGroupBox.Enabled = false;
            ClearUserDetails();
        }

        private void PopulateUsers() {

            ClearUsers();

            //Populate Users Listview
            foreach (User U in Users) {
                ListViewItem LVI = new(U.ID);
                LVI.SubItems.Add(U.Name);
                UsersListView.Items.Add(LVI);
            }

            PopulateUserTypes();

        }

        private void PopulateUserTypes() {
            //Populate User Types
            UserTypes.ForEach(UT => UserDetailsUserTypeComboBox.Items.Add(UT.Name));
        }

        private async Task LoadUserDetails(int Index) {
            Busy();

            StatusLabel.Text = $"Loading Bank {Users[Index].Name}";

            //If the two subitems are not loaded, load them!
            Users[Index] = await NecoDB.User
                .Include(u => u.Accounts).ThenInclude(B => B.Details)
                .Include(u => u.Accounts).ThenInclude(B => B.Bank)
                .Include(u => u.Type).FirstOrDefaultAsync(u => u.ID == Users[Index].ID) ?? Users[Index];

            PopulateUserDetails(Index);
            UserDetailsGroupBox.Enabled = true;

            Unbusy();
        }

        private void ClearUserDetails() {
            UserBankAccountsListView.Items.Clear();
            UserIDNameLabel.Text = "";
            UserDetailsUserTypeComboBox.Text = "";

        }

        private void PopulateUserDetails(int Index) {

            User U = Users[Index];

            ClearUserDetails();
            UserIDNameLabel.Text = $"{U.Name} ({U.ID})";
            UserDetailsUserTypeComboBox.Text = U.Type.Name;

            foreach (BankAccount A in U.Accounts) {
                ListViewItem LVI = new(A.Bank.Name);
                LVI.SubItems.Add(A.ID);
                LVI.SubItems.Add(A.Details?.Balance.ToString("n0") + "p");
                UserBankAccountsListView.Items.Add(LVI);
            }
        }

        private async Task SaveUsers() {

            Busy();

            StatusLabel.Text = "Saving Users...";
            NecoDB.User.UpdateRange(Users);
            NecoDB.UserAuth.UpdateRange(UserAuths);
            NecoDB.UserType.UpdateRange(UserTypes);
            await TrySave();

            Unbusy();

        }

        #endregion

        #region Banks

        private async Task LoadBanks() {

            Busy();

            StatusLabel.Text = "Loading Banks";
            Banks?.Clear();
            Banks = await NecoDB.Bank.ToListAsync();
            
            PopulateBanks();
            BankDetailsGroupBox.Enabled = false;

            Unbusy();
        }

        private void ClearBanks() {
            BanksListView.Items.Clear();
            BankDetailsGroupBox.Enabled = false;
            ClearBankDetails();
        }

        private void PopulateBanks() {
            ClearBanks();

            foreach (Bank B in Banks) {
                ListViewItem LVI = new(B.ID);
                LVI.SubItems.Add(B.Name);
                BanksListView.Items.Add(LVI);
            }


        }

        private async Task LoadBankDetails(int Index) {
            Busy();

            StatusLabel.Text = $"Loading Bank {Banks[Index].Name}";

            if (Banks[Index].AccountTypes == null) {
                Banks[Index] = await NecoDB.Bank
                    .Include(B => B.AccountTypes)
                    .FirstOrDefaultAsync(B => B.ID == Banks[Index].ID) ?? Banks[Index];
            }
            PopulateBankDetails(Index);
            BankDetailsGroupBox.Enabled = true;

            Unbusy();
        }

        private void ClearBankDetails() {
            BankNameLabel.Text = "";
            BankAccountTypesListView.Items.Clear();
        }

        private void PopulateBankDetails(int Index) {
            Bank B = Banks[Index];
            ClearBankDetails();
            BankNameLabel.Text = B?.Name;

            foreach (BankAccountType T in B.AccountTypes ?? new()) {
                ListViewItem LVI = new(T.Name);
                LVI.SubItems.Add(((decimal)T.InterestRate * 100.0m) + "");
                BankAccountTypesListView.Items.Add(LVI);
            }
        }

        private async Task SaveBanks() {

            Busy();

            StatusLabel.Text = "Checking if there's any banks to add";

            if (BanksToAdd?.Count > 0) {
                StatusLabel.Text = "Adding new banks";
                NecoDB.Bank.AddRange(BanksToAdd);
                await TrySave();
                BanksToAdd.Clear();
            }

            NecoDB.Bank.UpdateRange(Banks);
            await TrySave();

            StatusLabel.Text = "Saving Banks";
            NecoDB.Bank.UpdateRange(Banks);
            await TrySave();

            Unbusy();

        }

        #endregion

        #region Jurisdictions

        private async Task LoadJurisdictions() {

            Busy();

            StatusLabel.Text = "Loading Jurisdictions";
            Jurisdictions?.Clear();
            Jurisdictions = await NecoDB.TaxJurisdiction.ToListAsync();

            StatusLabel.Text = "Getting User Types";
            UserTypes?.Clear();
            UserTypes = await NecoDB.UserType.ToListAsync();

            PopulateJurisdiction();
            JurisdictionDetailsGroupBox.Enabled = false;

            Unbusy();

        }

        private void ClearJurisdictions() {
            JurisdictionsListView.Items.Clear();
            JurisdictionDetailsGroupBox.Enabled = false;
            ClearJurisdictionDetails();
        }

        private void PopulateJurisdiction() {
            ClearJurisdictions();

            Jurisdictions.ForEach(J => JurisdictionsListView.Items.Add(J.Name));
        }

        private async Task LoadJurisdictionDetails(int Index) {

            Busy();

            StatusLabel.Text = $"Getting details for Jurisdiction {Jurisdictions[Index].Name}";

            //If it's not already loaded, load it
            if (Jurisdictions[Index].Account == null || Jurisdictions[Index].Brackets == null) {
                Jurisdictions[Index] = await NecoDB.TaxJurisdiction
                    .Include(J => J.Brackets).ThenInclude(J => J.Type)
                    .Include(J => J.Account)
                    .FirstOrDefaultAsync(J => J.ID == Jurisdictions[Index].ID) ?? Jurisdictions[Index];
            }

            PopulateJurisdictionDetails(Index);
            JurisdictionDetailsGroupBox.Enabled = true;

            Unbusy();
        }


        private void ClearJurisdictionDetails() {
            JurisdictionNameLabel.Text = "";
            JurisdictionBankAccountBox.Text = "";
            JurisdictionBracketsListView.Items.Clear();
        }

        private void PopulateJurisdictionDetails(int Index) {
            ClearJurisdictionDetails();

            JurisdictionNameLabel.Text = Jurisdictions[Index].Name;
            JurisdictionBankAccountBox.Text = Jurisdictions[Index].Account?.ID;

            foreach (TaxBracket B in Jurisdictions[Index].Brackets ?? new()) {
                ListViewItem LVI = new(B.Name);
                LVI.SubItems.Add((B.Rate * 100) + "");
                LVI.SubItems.Add(B.Start.ToString("N0") + "p");
                LVI.SubItems.Add(B.End.ToString("N0") + "p");
                JurisdictionBracketsListView.Items.Add(LVI);
            }
        }


        private async Task SaveJurisdictions() {

            Busy();

            StatusLabel.Text = "Saving Jurisdictions";
            NecoDB.TaxJurisdiction.UpdateRange(Jurisdictions);

            await TrySave();

            Unbusy();
        }

        #endregion

        private void Busy() {

            MainTabController.Enabled = false;
            Cursor = Cursors.WaitCursor;
            StatusLabel.Text = "Please Wait";
            StatusProgressBar.Style = ProgressBarStyle.Marquee;

        }

        private void Unbusy() {

            MainTabController.Enabled = true;
            Cursor = Cursors.Default;
            StatusLabel.Text = "Ready";
            StatusProgressBar.Style = ProgressBarStyle.Continuous;
            StatusProgressBar.Value = 0;
        }

        #endregion

        #region User UI Interactions

        private async void UsersListView_SelectedIndexChanged(object sender, EventArgs e) {
            if (UsersListView.SelectedItems.Count < 1 ) { return; }
            int Index = UsersListView.SelectedIndices[0];
            await LoadUserDetails(Index);

        }

        private void UserChangePinButton_Click(object sender, EventArgs e) {
            if (UsersListView.SelectedItems.Count < 1) { return; }
            int Index = UsersListView.SelectedIndices[0];


            TextboxForm T = new("Specify a new Pin for this user", "New Pin");
            if (T.ShowDialog() == DialogResult.OK) {
                UserAuths[UserAuths.IndexOf(UserAuths.First(i => i.ID == Users[Index].ID))].Pin = T.ReturnText;
                DirtyBit = true;
            }
        }

        private async void GenerateIncomeReportButton_Click(object sender, EventArgs e) {
            if (UsersListView.SelectedItems.Count < 1) { return; }
            int Index = UsersListView.SelectedIndices[0];
            User U = Users[Index];

            Busy();
            StatusLabel.Text = "Generating Report...";
            TaxReport R = await TaxReportGenerator.ReportGenerator(NecoDB, U);
            Unbusy();

            BigTextForm BT = new() {
                DisplayText = R.Report,
                Text = $"Report for {R.Owner?.Name} prepared on {R.PreparedDate}"
            };
            BT.ShowDialog();

        }

        private void UserDetailsUserTypeComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (UsersListView.SelectedItems.Count < 1 || Freeze) { return; }
            int Index = UsersListView.SelectedIndices[0];

            if (UserDetailsUserTypeComboBox.SelectedIndex < 0) { return; }
            Users[Index].Type = UserTypes[UserDetailsUserTypeComboBox.SelectedIndex];
            DirtyBit = true;
        }

        private void ModifyUserTypesButton_Click(object sender, EventArgs e) {
            if (UsersListView.SelectedItems.Count < 1) { return; }
            int Index = UsersListView.SelectedIndices[0];

            new UserTypesForm(ref UserTypes).ShowDialog();
            
            //Assume a change has been done
            DirtyBit = true;

            //Freeze the thing
            Freeze = true;

            //Repopulate the combobox
            UserDetailsUserTypeComboBox.Items.Clear();
            PopulateUserTypes();

            //Set the combobox to display the usertype the current user is.
            UserDetailsUserTypeComboBox.Text = Users[Index].Type.Name;

            //Unfreeze
            Freeze = false;
        }

        private void UserDetailsContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e) {
            if (UsersListView.SelectedItems.Count < 1 || UserBankAccountsListView.SelectedItems.Count < 1) { e.Cancel = true; return; }
        }

        private void CopyUserBankAccountIDMenuItem_Click(object sender, EventArgs e) {
            if (UsersListView.SelectedItems.Count < 1 || UserBankAccountsListView.SelectedItems.Count < 1) { return; }
            int I1 = UsersListView.SelectedIndices[0];
            int I2 = UserBankAccountsListView.SelectedIndices[0];
            Clipboard.SetText(Users[I1].Accounts[I2].ID );
        }

        private void CoypUserBankBalanceMenuItem_Click(object sender, EventArgs e) {
            if (UsersListView.SelectedItems.Count < 1 || UserBankAccountsListView.SelectedItems.Count < 1) { return; }
            int I1 = UsersListView.SelectedIndices[0];
            int I2 = UserBankAccountsListView.SelectedIndices[0];
            Clipboard.SetText(Users[I1].Accounts[I2].Details.Balance+"");
        }

        #endregion

        #region Bank UI interactions

        private async void BanksListView_SelectedIndexChanged(object sender, EventArgs e) {
            if (BanksListView.SelectedItems.Count < 1) { return; }
            int Index = BanksListView.SelectedIndices[0];
            await LoadBankDetails(Index);
        }

        private void BankAccountTypesListView_DoubleClick(object sender, EventArgs e) {
            if (BanksListView.SelectedItems.Count < 1 || BankAccountTypesListView.SelectedItems.Count < 1) { return; }
            int I1 = BanksListView.SelectedIndices[0];
            int I2 = BankAccountTypesListView.SelectedIndices[0];
            AccountTypeEditor ATE = new(Banks[I1].AccountTypes[I2]);

            if (ATE.ShowDialog() == DialogResult.OK) { 
                Banks[I1].AccountTypes[I2] = ATE.Type;
                DirtyBit = true;
            }
            ATE.Dispose();

            PopulateBankDetails(I1);
        }

        private void NewBankButton_Click(object sender, EventArgs e) {

            NewBankForm NBF = new();
            if (NBF.ShowDialog() == DialogResult.OK) {
                if (Banks.Any(B => B.ID == NBF.ReturnBank.ID)) { 
                    MessageBox.Show("Bank with that ID already exists!","Sorry kiddo",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    NBF.Dispose();
                    return;
                }
                Banks.Add(NBF.ReturnBank);
                if (BanksToAdd is null) { BanksToAdd = new(); }
                BanksToAdd.Add(NBF.ReturnBank);
                DirtyBit = true;
            }

            NBF.Dispose();
            PopulateBanks();
        }

        private void NewAccountTypeButton_Click(object sender, EventArgs e) {
            if (BanksListView.SelectedItems.Count < 1) { return; }
            int I1 = BanksListView.SelectedIndices[0];
            AccountTypeEditor ATE = new(new() {
                Bank=Banks[I1],
                Name="New Account Type for " + Banks[I1].Name
            });

            if (ATE.ShowDialog() == DialogResult.OK) { 
                Banks[I1].AccountTypes.Add(ATE.Type);
                DirtyBit = true;
            }

            ATE.Dispose();

            PopulateBankDetails(I1);
        }

        private void BankAccountTypesMenuItem_Opening(object sender, System.ComponentModel.CancelEventArgs e) {
            if (BanksListView.SelectedItems.Count < 1 || BankAccountTypesListView.SelectedItems.Count < 1) { e.Cancel = true; return; }
        }

        private void CopyBankAccountTypeID_Click(object sender, EventArgs e) {
            if (BanksListView.SelectedItems.Count < 1 || BankAccountTypesListView.SelectedItems.Count < 1) { return; }
            int I1 = BanksListView.SelectedIndices[0];
            int I2 = BankAccountTypesListView.SelectedIndices[0];
            Clipboard.SetText(Banks[I1].AccountTypes[I2].ID + "");
        }

        #endregion

        #region Jurisdiction UI Interactions

        private async void JurisdictionsListView_SelectedIndexChanged(object sender, EventArgs e) {
            if (JurisdictionsListView.SelectedItems.Count < 1) { return; }
            int Index = JurisdictionsListView.SelectedIndices[0];
            await LoadJurisdictionDetails(Index);
        }

        private async void JurisdictionBankAccountBox_LostFocus(object sender, EventArgs e) {
            if (JurisdictionsListView.SelectedItems.Count < 1) { return; }
            int index = JurisdictionsListView.SelectedIndices[0];

            if (string.IsNullOrWhiteSpace(JurisdictionBankAccountBox.Text)) { return; }

            Busy();

            if (!await NecoDB.BankAccount.AnyAsync(B => B.ID == JurisdictionBankAccountBox.Text)) {
                MessageBox.Show($"Bank account {JurisdictionBankAccountBox.Text} does not exist!", "N o", MessageBoxButtons.OK, MessageBoxIcon.Error);
                JurisdictionBankAccountBox.Focus();
                Unbusy(); return;
            }

            Jurisdictions[index].Account = await NecoDB.BankAccount.FindAsync(JurisdictionBankAccountBox.Text);
            DirtyBit = true;

            Unbusy();
        }

        private void JurisdictionBracketsListView_DoubleClick(object sender, EventArgs e) {
            if (JurisdictionsListView.SelectedItems.Count < 1 || JurisdictionBracketsListView.SelectedItems.Count < 1) { return; }
            int I1 = JurisdictionsListView.SelectedIndices[0];
            int I2 = JurisdictionBracketsListView.SelectedIndices[0];
            TaxBracketEditor TBE = new(Jurisdictions[I1].Brackets[I2], UserTypes);

            if (TBE.ShowDialog() == DialogResult.OK) { 
                Jurisdictions[I1].Brackets[I2] = TBE.Bracket;
                DirtyBit = true;
            }
            TBE.Dispose();

            PopulateJurisdictionDetails(I1);
        }

        private void NewJurisdictionButton_Click(object sender, EventArgs e) {
            TextboxForm TBF = new("Name for the new Jurisdiction", "Please input a name");

            if (TBF.ShowDialog() == DialogResult.OK) {
                Jurisdictions.Add(new() {
                    Account = null, Brackets = new List<TaxBracket>(), Name = TBF.TheBox.Text
                });
                DirtyBit = true;
            }

            TBF.Dispose();
            PopulateJurisdiction();
        }

        private void NewBracketButton_Click(object sender, EventArgs e) {
            if (JurisdictionsListView.SelectedItems.Count < 1) { return; }
            int I1 = JurisdictionsListView.SelectedIndices[0];
            TaxBracketEditor TBE = new(new() {
                Jurisdiction = Jurisdictions[I1],
                Name = "New Tax Bracket for " + Jurisdictions[I1].Name
            }, UserTypes);

            if (TBE.ShowDialog() == DialogResult.OK) { 
                Jurisdictions[I1].Brackets.Add(TBE.Bracket);
                DirtyBit = true;
            }
            TBE.Dispose();

            PopulateJurisdictionDetails(I1);
        }

        #endregion

        #region Cleanup

        private async void CleanupTransactionsButton_Click(object sender, EventArgs e) {

            MainPannel.Enabled = false;

            StatusLabel.Text = "Deleting old transactions";
            StatusProgressBar.Style = ProgressBarStyle.Marquee;

            //Get the transactions and checks
            List<Transaction> Trans = await NecoDB.Transaction.Where((T) => T.Time < DateTime.Now.AddMonths(-3)).ToListAsync();

            int count = 0;

            StatusLabel.Text = $"Deleting {Trans.Count} Transactions";
            StatusProgressBar.Style = ProgressBarStyle.Continuous;

            foreach (var T in Trans) {
                //Try to find a check with a matching transaction
                var Check = NecoDB.CheckbookItem.FirstOrDefaultAsync((C) => C.AttachedTransaciton.ID == T.ID);
                if (Check != null) { NecoDB.Remove(Check); } //Remove the check

                //now remove the transaction
                NecoDB.Remove(T);

                StatusProgressBar.Value = (count * 100) / Trans.Count;
            }

            StatusLabel.Text = "Saving...";
            StatusProgressBar.Value = 0;
            StatusProgressBar.Style = ProgressBarStyle.Marquee;

            await TrySave();

            MainPannel.Enabled = true;
            StatusLabel.Text = "Connected to the Neco Database";
            StatusProgressBar.Style = ProgressBarStyle.Continuous;

        }

        private async void CleanupNotificationsButton_Click(object sender, EventArgs e) {

            MainPannel.Enabled = false;

            StatusLabel.Text = "Deleting old Notifications";
            StatusProgressBar.Style = ProgressBarStyle.Marquee;

            List<Notification> N = await NecoDB.Notification.Where((N) => N.Time < DateTime.Now.AddMonths(-1)).ToListAsync();

            StatusLabel.Text = $"Deleting {N.Count} Notification(s)";
            NecoDB.Notification.RemoveRange(N);

            StatusLabel.Text = "Saving...";
            await TrySave();

            MainPannel.Enabled = true;
            StatusLabel.Text = "Connected to the Neco Database";
            StatusProgressBar.Style = ProgressBarStyle.Continuous;

        }

        private async void CleanupTaxReportsButton_Click(object sender, EventArgs e) {

            MainPannel.Enabled = false;

            StatusLabel.Text = "Deleting old Tax Reports";
            StatusProgressBar.Style = ProgressBarStyle.Marquee;

            List<TaxReport> N = await NecoDB.TaxReport.Where((N) => N.PreparedDate < DateTime.Now.AddMonths(-6)).ToListAsync();

            StatusLabel.Text = $"Deleting {N.Count} Tax Report(s)";
            NecoDB.TaxReport.RemoveRange(N);

            StatusLabel.Text = "Saving...";
            await TrySave();

            MainPannel.Enabled = true;
            StatusLabel.Text = "Connected to the Neco Database";
            StatusProgressBar.Style = ProgressBarStyle.Continuous;

        }

        private async void CleanupCertifiedItemsButton_Click(object sender, EventArgs e) {

            MainPannel.Enabled = false;

            StatusLabel.Text = "Deleting old Certified Items";
            StatusProgressBar.Style = ProgressBarStyle.Marquee;

            List<CertifiedItem> N = await NecoDB.CertifiedItem.Where((N) => N.Date < DateTime.Now.AddMonths(-6)).ToListAsync();

            StatusLabel.Text = $"Deleting {N.Count} Certified Item(s)";
            NecoDB.CertifiedItem.RemoveRange(N);

            StatusLabel.Text = "Saving...";
            await TrySave();

            MainPannel.Enabled = true;
            StatusLabel.Text = "Connected to the Neco Database";
            StatusProgressBar.Style = ProgressBarStyle.Continuous;

        }
        #endregion

        #region MenuItems

        private async void ConnectDisconnectToolStripMenuItem_Click(object sender, EventArgs e) {
            if (NecoDB != null) {

                await SaveCurrentTab();
                DirtyBit = false;

                NecoDB.Dispose();
                connectDisconnectToolStripMenuItem.Text = "&Connect";
                NecoDB = null;
                MainPannel.Enabled = false;
                StatusLabel.Text = "Disconnected";
                GenerateToolStripMenuItem.Enabled = false;
                return;
            }

            ConnectionForm F = new();
            F.ShowDialog();
            if (F.Context != null) {

                //OK we have a de-esta cosa
                NecoDB = F.Context;
                connectDisconnectToolStripMenuItem.Text = "&Disconnect";
                MainPannel.Enabled = true;
                StatusLabel.Text = $"Connected to Neco Database";
                GenerateToolStripMenuItem.Enabled = true;
                await LoadCurrentTab();
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e) { Close(); }

        private async void SaveToolStripMenuItem_Click(object sender, EventArgs e) { await SaveCurrentTab(); }

        private async Task SaveCurrentTab() {
            if (DirtyBit && MessageBox.Show("Save changes to current tab?", "Save?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                switch (CurrentTab) {
                    case 0:
                        //UsersTab
                        await SaveUsers();
                        break;
                    case 1:
                        //Banks
                        await SaveBanks();
                        break;
                    case 2:
                        //Jurisdictions
                        await SaveJurisdictions();
                        break;
                    default:
                        break;
                }
            }
        }

        private async void RefreshToolStripMenuItem_Click(object sender, EventArgs e) {

            await SaveCurrentTab();
            await LoadCurrentTab();

        }

        private async Task LoadCurrentTab() {
            switch (CurrentTab) {
                case 0:
                    //Users Tab
                    await LoadUsers(UserSearchBox.Text);
                    break;
                case 1:
                    //Banks tab
                    await LoadBanks();
                    break;
                case 2:
                    //Jurisdiction Tab
                    await LoadJurisdictions();
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region ReportGenerators

        private async void generateIncomeReportToolStripMenuItem_Click(object sender, EventArgs e) {
            if (NecoDB == null) { return; }
            //Ensure everything is as saved as the user wants it to be
            await SaveCurrentTab();

            SaveFileDialog SFD = new() {
                Filter = "Excel File (*.xlsx)|*.xlsx|All Files (*.*)|*.*",
                DefaultExt = "xlsx",
                FileName="Report.xlsx",
                Title="Save Report to?",
                OverwritePrompt=true
            };

            if (SFD.ShowDialog() != DialogResult.OK) { return; }
            
            AsimovReportGenerator ARG = new(NecoDB);
            await ARG.GenerateReport(SFD.FileName);

            SFD.Dispose();
        }

        private async void generateBankMarketReportToolStripMenuItem_Click(object sender, EventArgs e) {
            if (NecoDB == null) { return; }
            //Ensure everything is as saved as the user wants it to be
            await SaveCurrentTab();

        }


        private async void generateBankReportToolStripMenuItem_Click(object sender, EventArgs e) {
            if (NecoDB == null) { return; }

            //Ensure everything is as saved as the user wants it to be
            await SaveCurrentTab();

            //Ensure we have banks loaded
            await LoadBanks();

            //Now we have to create a dropdown form
            DropdownForm<Bank> DForm = new("Choose a bank to generate a report for", "Choose a Bank", Banks, "Name");

            //Ensure we have the right to continue
            if (DForm.ShowDialog() != DialogResult.OK) { DForm.Dispose(); return; }

            //Now we gotta wait for the generator


        }

        private async void generateJurisdictionReportToolStripMenuItem_Click(object sender, EventArgs e) {
            if (NecoDB == null) { return; }

            //Ensure everything is as saved as the user wants it to be
            await SaveCurrentTab();

            //Ensure we have Jurisdictions loaded
            await LoadJurisdictions();

            //Now we have to create a dropdown form
            DropdownForm<TaxJurisdiction> DForm = new("Choose a Jurisdiction to generate a report for", "Choose a Jurisdiction", Jurisdictions, "Name");

            //Ensure we have the right to continue
            if (DForm.ShowDialog() != DialogResult.OK) { DForm.Dispose(); return; }

            //Now we gotta wait for the generator

        }

        #endregion


        public async Task<int> TrySave() {
            try { int Rows = await NecoDB.SaveChangesAsync(); return Rows; } catch (Exception E) { ExceptionDialogue.Show(E, "There was an error saving the information to the database", "You may want to disconnect and reconnect to the database"); return -1; }
        }
    }
}
