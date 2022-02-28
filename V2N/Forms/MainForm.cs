using System.Data;
using Igtampe.Neco.Data;
using Igtampe.Neco.Common.Banking;
using Igtampe.Neco.Common.Taxes;
using Igtampe.Neco.V2N.Classes;
using Igtampe.Neco.V2N.Forms.Details;
using Microsoft.EntityFrameworkCore;

namespace Igtampe.Neco.V2N.Forms {
    public partial class MainForm : Form {

        NecoContext? DB;

        /// <summary>Banks in the neco system</summary>
        readonly List<Bank> NecoBanks = new();

        Bank? UMSNB;
        Bank? GBANK;
        Bank? RIVER;
            
        /// <summary>Jurisdictions in the neco system that correspond in level to those in ViBE (These being states)</summary>
        readonly List<Jurisdiction> NecoJurisdictions = new();
        readonly List<JurisdictionListItem> NecoJurisdictionsForGridview = new();

        Jurisdiction? Newpond;
        Jurisdiction? Paradisus;
        Jurisdiction? Laertes;
        Jurisdiction? Urbia;
        Jurisdiction? NOsten;
        Jurisdiction? SOsten;

        V2NConverter? Converter;

        readonly List<ConvertibleViBEUser> ViBEUsers = new();
        readonly List<Common.User> NecoUsers = new();

        struct MergeIntoListItem {
            public string Name { get; set; }
            public V2NConverter.MergeAllAccountsInto MergeInto { get; set; }
        }

        struct JurisdictionListItem {

            public string Name => Jurisdiction?.Name ?? string.Empty;

            public Jurisdiction? Jurisdiction { get; set; }
        }

        public MainForm() {
            InitializeComponent();

            //Set the step 2, 3, and 4 groupboxes as disabled
            NecoTransferOptionsGroupbox.Enabled = ViBEUsersGroupBox.Enabled = NecoUsersGroupBox.Enabled = false; //I think this works.

            //Cool now check if a DBURL.txt already exists
            if (File.Exists("DBURL.txt")) { DBURLBox.Text = File.ReadAllText("DBURL.txt"); }

            //Make the list for the datasource:
            List<MergeIntoListItem> MergeIntoChoices = new() {
                new() { Name = "Highest Balance", MergeInto = V2NConverter.MergeAllAccountsInto.HIGHEST_BALANCE },
                new() { Name = "UMSNB", MergeInto = V2NConverter.MergeAllAccountsInto.UMSNB },
                new() { Name = "GBANK", MergeInto = V2NConverter.MergeAllAccountsInto.GBANK},
                new() { Name = "RIVER", MergeInto = V2NConverter.MergeAllAccountsInto.RIVER }
            };

            //Configure the binding sources:
            ViBEUsersBindingSource.DataSource = ViBEUsers;
            NecoUsersBindingSource.DataSource = NecoUsers;

            //Configure the columns for the neco de-esta cosas
            ViBEUsersDataGridView.Columns.Clear();
            ViBEUsersDataGridView.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "ID", DataPropertyName="ID", ReadOnly = true, });
            ViBEUsersDataGridView.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Name", DataPropertyName = "Name", ReadOnly = true, });
            ViBEUsersDataGridView.Columns.Add(new DataGridViewComboBoxColumn() {
                HeaderText = "Location", DataPropertyName = "Location", ReadOnly = false, Visible = true,
                DataSource = NecoJurisdictionsForGridview, DisplayMember = "Name", ValueMember = "Jurisdiction"
            });
            ViBEUsersDataGridView.Columns.Add(new DataGridViewCheckBoxColumn() { HeaderText = "Merge All Accounts", DataPropertyName = "MergeAllAccounts", ReadOnly = false, });
            ViBEUsersDataGridView.Columns.Add(new DataGridViewComboBoxColumn() {
                HeaderText = "Merge Into", DataPropertyName = "MergeInto", ReadOnly = false, Visible = true,
                DataSource = MergeIntoChoices , DisplayMember = "Name", ValueMember = "MergeInto"
            });
            ViBEUsersDataGridView.Columns.Add(new DataGridViewCheckBoxColumn() { HeaderText = "Convert", DataPropertyName = "Convert", ReadOnly = false, });
            
            NecoUsersDataGrid.Columns.Clear();
            NecoUsersDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "ID", DataPropertyName = "ID", ReadOnly = true, });
            NecoUsersDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Name", DataPropertyName = "Name", ReadOnly = true, });

            //Now adios

        }

        private void ViBEUserFolderBrowseBTN_Click(object sender, EventArgs e) {
            if (ViBEFolderBrowser.ShowDialog() == DialogResult.OK) { ViBEUsersFolderBox.Text = ViBEFolderBrowser.SelectedPath; }
        }
        
        private void ViBEIRFsFolderBrowseBTN_Click(object sender, EventArgs e) {
            if (ViBEFolderBrowser.ShowDialog() == DialogResult.OK) { ViBEIRFsFolderBox.Text = ViBEFolderBrowser.SelectedPath; }
        }

        private async void SetDataSourcesBTN_Click(object sender, EventArgs e) {

            Enabled = false;

            ProgressForm LF = new();
            LF.Show();

            if (!Directory.Exists(ViBEUsersFolderBox.Text)) { MessageBox.Show("ViBE Users folder was not found", "Not found", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            if (!string.IsNullOrWhiteSpace(ViBEIRFsFolderBox.Text) && !Directory.Exists(ViBEIRFsFolderBox.Text)) { MessageBox.Show("ViBE IRFs folder was not found", "Not found", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            try {

                LF.UpdateStatus("Getting ViBE Users");

                //Load all the ViBE Users:
                //Get a list of all directories
                string[] Users = Directory.GetDirectories(ViBEUsersFolderBox.Text);
                ViBEUsers.Clear();
                NecoUsers.Clear();

                double PerUserIncrement = 1.0 / Users.Length;
                double Progress = 0.0;

                foreach (string UserFolder in Users) {

                    LF.SetPercentage(Progress);
                    Progress += PerUserIncrement;

                    //Ensure its actually a user folder
                    if (!ViBE.User.IsViBEUserFolder(UserFolder)) continue;

                    //Get only their ID
                    string? ID = Path.GetFileName(UserFolder);
                    if (ID is null) throw new InvalidOperationException("Something happened that was really not supposed to happen");

                    LF.UpdateStatus($"Processing User {ID}");

                    //Load the user
                    ConvertibleViBEUser U = ConvertibleViBEUser.FromViBEUser(await ViBE.User.FromFiles(ID, ViBEUsersFolderBox.Text, ViBEIRFsFolderBox.Text));
                    if (U is null) throw new InvalidOperationException("This didn't work");

                    //now then let's add it to the list
                    ViBEUsers.Add(U);

                }

                //Now configure the DatagridView
                LF.SetPercentage(-1);
                LF.UpdateStatus($"Configuring ViBE Data Grid View");
                ViBEUsersDataGridView.DataSource = ViBEUsers;

                LF.UpdateStatus($"Writing DBURL");

                //Write the DBURL
                if (File.Exists("DBURL.txt")) { File.Delete("DBURL.txt"); }
                File.WriteAllText("DBURL.txt", DBURLBox.Text);

                LF.UpdateStatus($"Initializing DB");

                if (DB is not null) { await DB.DisposeAsync(); }
                DB = await Task.Run(()=>new NecoContext());

                //The DB is now ready because we said so

                //Grab the Banks
                LF.UpdateStatus($"Getting Neco Banks...");
                NecoBanks.Clear();
                NecoBanks.AddRange(await DB.Bank.ToListAsync());
                    
                //Attempt to find the UMSNB, GBANK, and RIVER.
                LF.UpdateStatus($"Attempting to Auto Detect UMSNB");
                LF.SetPercentage(0);
                UMSNB = await DB.Bank.FindAsync("UMSNB");
                
                LF.UpdateStatus($"Attempting to Auto Detect GBANK");
                LF.SetPercentage(33); 
                GBANK = await DB.Bank.FindAsync("GBANK");
                
                LF.UpdateStatus($"Attempting to Auto Detect RIVER");
                LF.SetPercentage(66);
                RIVER = await DB.Bank.FindAsync("RIVER");

                LF.UpdateStatus($"Linking BankComboBoxes");
                
                //Link the fields
                LF.SetPercentage(0);
                LinkBankComboBox(ref UMSNBComboBox, UMSNB);
                LF.SetPercentage(33);               
                LinkBankComboBox(ref GBANKComboBox, GBANK);
                LF.SetPercentage(66);               
                LinkBankComboBox(ref RIVERComboBox, RIVER);

                //Grab all the Jurisdictions:
                LF.UpdateStatus($"Getting all STATE level jurisdictions");
                LF.SetPercentage(-1);
                NecoJurisdictions.Clear();
                NecoJurisdictions.AddRange(await DB.Jurisdiction.Where(A => A.Type == JurisdictionType.STATE).ToListAsync());
                NecoJurisdictionsForGridview.Clear();
                NecoJurisdictionsForGridview.AddRange(NecoJurisdictions.ConvertAll(A => new JurisdictionListItem() { Jurisdiction=A})); //God bless linq

                //Attempt to find the UMS Jurisdictions
                LF.UpdateStatus($"Attempting to Auto Detect Newpond");
                LF.SetPercentage(0);
                Newpond = await GetJurisdictionFromDB("Newpond");
                
                LF.UpdateStatus($"Attempting to Auto Detect Paradisus");
                LF.SetPercentage(16);
                Paradisus = await GetJurisdictionFromDB("Paradisus");

                LF.UpdateStatus($"Attempting to Auto Detect Laertes");
                LF.SetPercentage(33);
                Laertes = await GetJurisdictionFromDB("Laertes");

                LF.UpdateStatus($"Attempting to Auto Detect Urbia");
                LF.SetPercentage(50);
                Urbia = await GetJurisdictionFromDB("Urbia");

                LF.UpdateStatus($"Attempting to Auto Detect North Osten");
                LF.SetPercentage(66);
                NOsten = await GetJurisdictionFromDB("North Osten");

                LF.UpdateStatus($"Attempting to Auto Detect South Osten");
                LF.SetPercentage(84);
                SOsten = await GetJurisdictionFromDB("South Osten");

                LF.UpdateStatus($"Linking Jurisdiction ComboBoxes");
                LF.SetPercentage(-1);

                //Link all of these things
                LF.SetPercentage(0);
                LinkJurisdictionComboBox(ref NewpondComboBox, Newpond);
                LF.SetPercentage(16);
                LinkJurisdictionComboBox(ref ParadisusComboBox, Paradisus);
                LF.SetPercentage(33);
                LinkJurisdictionComboBox(ref LaertesComboBox, Laertes);
                LF.SetPercentage(50);
                LinkJurisdictionComboBox(ref UrbiaComboBox, Urbia);
                LF.SetPercentage(66);
                LinkJurisdictionComboBox(ref NOstenComboBox, NOsten);
                LF.SetPercentage(84);
                LinkJurisdictionComboBox(ref SOstenComboBox, SOsten);
                
                LF.UpdateStatus($"Wrapping up");
                LF.SetPercentage(-1);
                //Lastly set the appropriate groupboxes to be enabled/disabled
                NecoTransferOptionsGroupbox.Enabled = true;
                ViBEUsersGroupBox.Enabled = false;
                NecoUsersGroupBox.Enabled = false;
                UpdateViBEUsersLabel();

            } catch (Exception E) { new ExceptionForm(E).ShowDialog(); }

            LF.Close();
            Focus();
            Enabled = true;

        }

        private void UpdateViBEUsersLabel() => ViBEUserDetailsLabel.Text = $"{ViBEUsers.Count} User(s) loaded.";
        
        private void UpdateNecoUsersLabel() => label1.Text = $"{NecoUsers.Count} User(s) Converted.";

        //This is possibly one of the riskiest exclamation marks I've put but I don't really care.
        private async Task<Jurisdiction?> GetJurisdictionFromDB(string Name) => await DB!.Jurisdiction.Where(A => A.Type == JurisdictionType.STATE && A.Name==Name).FirstOrDefaultAsync();

        private void LinkBankComboBox(ref ComboBox Box, Bank? Bank) {
            Box.DataSource = new List<Bank>(NecoBanks!);
            Box.DisplayMember = "ID";
            Box.ValueMember = "ID";
            if (Bank is not null) { Box.SelectedItem = Bank; }
        }
        private void LinkJurisdictionComboBox(ref ComboBox Box, Jurisdiction? Jurisdiction) {
            Box.DataSource = new List<Jurisdiction>(NecoJurisdictions!);
            Box.DisplayMember = "Name";
            Box.ValueMember = "ID";
            if (Jurisdiction is not null) { Box.SelectedItem = Jurisdiction; }
        }

        private void SetTransferOptionsBTN_Click(object sender, EventArgs e) {
            if (UMSNB is null || GBANK is null || RIVER is null ||
                Newpond is null || Paradisus is null || Laertes is null || Urbia is null || SOsten is null || NOsten is null) {
                MessageBox.Show("Please set all banks and jurisdictions","No",MessageBoxButtons.OK,MessageBoxIcon.Error); return;
            }
            Converter = new(UMSNB, GBANK, RIVER, Newpond, Paradisus, Urbia, Laertes,SOsten, NOsten);
            //Clear the list of converted users
            NecoUsers.Clear();
            //Enable the third step
            ViBEUsersGroupBox.Enabled = true;
            NecoUsersGroupBox.Enabled = false;
        }

        private void ViBEUsersConvertBTN_Click(object sender, EventArgs e) {
            if (Converter is null) { MessageBox.Show("The V2N Converter was not set up. This isn't supposed to happen. Repeat step 2.","Error",MessageBoxButtons.OK,MessageBoxIcon.Warning); return; }

            if (ViBEUsers.Any(A => A.Convert && !A.CanConvert())) { MessageBox.Show("Some of the users you want to convert cannot be converted. Ensure all of them have at least a jurisdiction set.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            List<Common.User> NewNecoUsers = new();
            
            Enabled = false;
            ProgressForm LF = new();
            LF.Show();

            try {

                List<ConvertibleViBEUser> UsersToConvert = ViBEUsers.Where(A => A.Convert).ToList();
                double PerUserIncrement = 1.0 / UsersToConvert.Count;
                double Progress = 0.0;

                foreach (ConvertibleViBEUser User in UsersToConvert) {

                    LF.UpdateStatus($"Converting user {User.ID}");
                    LF.SetPercentage(Progress);
                    Progress += PerUserIncrement;
                    NewNecoUsers.Add(Converter.ViBEUserToNecoUser(User));

                }

                //Link the datagrid and configure it
                NecoUsersBindingSource.DataSource = NewNecoUsers;
                NecoUsers.Clear();
                NecoUsers.AddRange(NewNecoUsers);

                NecoUsersDataGrid.DataSource = NecoUsersBindingSource;

                //Ensure the appropriate de-estas cosas are enabled
                NecoUsersGroupBox.Enabled = true;

            } catch (Exception E) { new ExceptionForm(E).ShowDialog(); }

            LF.Close();
            Focus();
            Enabled = true;
            UpdateNecoUsersLabel();

        }

        private async void NecoUsersSaveBTN_Click(object sender, EventArgs e) {

            Enabled = false;
            ProgressForm LF = new();
            LF.Show();

            double PerUserIncrement = 1.0 / NecoUsers.Count;
            double Progress = 0.0;

            try {

                foreach (var User in NecoUsers) {

                    LF.SetPercentage(Progress);
                    Progress += PerUserIncrement;
                    LF.UpdateStatus($"Verifying and preparing to save {User.ID}");

                    //Set proper IDs for the accounts
                    for (int i = 0; i < User.Accounts.Count; i++) {
                        while (await DB!.Account.AnyAsync(A => A.ID == User.Accounts[i].ID)) { 
                            User.Accounts[i].ID = User.Accounts[i].IDGenerator.Generate(); 
                        }
                    }

                    Common.User? Existing = await DB!.User.FirstOrDefaultAsync(A => A.ID == User.ID);

                    //Now verify if the user already exists
                    if (Existing is not null) {
                        MessageBox.Show($"There exists an account with ID {User.ID}!\n\nConverted: {User}\n{Existing}\n\nPlease remove them to proceed", "Uh...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        LF.Close();
                        Focus();
                        Enabled = true;
                        return;
                    }
                }

                int Result = 0;

                if (MessageBox.Show($"Changes are ready to proceed. Do it?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                    DB!.AddRange(NecoUsers);
                    Result = await DB!.SaveChangesAsync();
                    MessageBox.Show($"{Result} Change(s) executed. You can now close the V2N", "Cool beans", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            } catch (Exception E) { new ExceptionForm(E).ShowDialog(); }

            LF.Close();
            Focus();
            Enabled = true;

        }

        private void CommonComboBox_SelectedValueChanged(object sender, EventArgs e) {
            //I REALLY WISH I COULD USE A SWITCH STATEMENT HERE PERO SWITCH REQUIRES CONSTANT VALUES!!!
            //Why
            if (sender is not ComboBox Box        ||         Box.SelectedItem is null) return;
            else if (Box == UMSNBComboBox) { UMSNB =         Box.SelectedItem as Bank; }
            else if (Box == GBANKComboBox) { GBANK =         Box.SelectedItem as Bank; }
            else if (Box == RIVERComboBox) { RIVER =         Box.SelectedItem as Bank; }
            else if (Box == NewpondComboBox) { Newpond =     Box.SelectedItem as Jurisdiction; }
            else if (Box == ParadisusComboBox) { Paradisus = Box.SelectedItem as Jurisdiction; }
            else if (Box == LaertesComboBox) { Laertes =     Box.SelectedItem as Jurisdiction; }
            else if (Box == UrbiaComboBox) { Urbia =         Box.SelectedItem as Jurisdiction; }
            else if (Box == NOstenComboBox) { NOsten =       Box.SelectedItem as Jurisdiction; }
            else if (Box == SOstenComboBox) { SOsten =       Box.SelectedItem as Jurisdiction; }
        }

        private void ViBEUsersDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e) {
            if (e.ColumnIndex != 0) { return; } //If we're not clicking the ID, just leave
            try {
                if (sender == ViBEUsersDataGridView) { DetailsFormGenerator.GenerateDetailsForm(ViBEUsers[e.RowIndex]).ShowDialog(); } 
                else if (sender == NecoUsersDataGrid) { DetailsFormGenerator.GenerateDetailsForm(NecoUsers[e.RowIndex]).ShowDialog(); }
            } catch (Exception E) {
                new ExceptionForm(E).ShowDialog();
            }
        }
    }
}
