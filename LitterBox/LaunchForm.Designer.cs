
namespace Igtampe.LitterBox {
    partial class LaunchForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LaunchForm));
            this.MainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectDisconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RefreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainPannel = new System.Windows.Forms.Panel();
            this.MainTabController = new System.Windows.Forms.TabControl();
            this.UserManagementPage = new System.Windows.Forms.TabPage();
            this.UsersTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.UsersGroupBox = new System.Windows.Forms.GroupBox();
            this.UsersListView = new System.Windows.Forms.ListView();
            this.BankManagementPage = new System.Windows.Forms.TabPage();
            this.BankTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.BankGroupBox = new System.Windows.Forms.GroupBox();
            this.BanksListView = new System.Windows.Forms.ListView();
            this.TaxJurisdictionManagementPage = new System.Windows.Forms.TabPage();
            this.JurisdictionTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.JurisdictionsGroupBox = new System.Windows.Forms.GroupBox();
            this.JurisdictionsListView = new System.Windows.Forms.ListView();
            this.CleanupTabPage = new System.Windows.Forms.TabPage();
            this.CleanupTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.WarnIconPictureBox = new System.Windows.Forms.PictureBox();
            this.SlowDownLabel = new System.Windows.Forms.Label();
            this.CleanupExplinationLabel = new System.Windows.Forms.Label();
            this.CleanupButtonsTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.CleanupCertifiedItemsButton = new System.Windows.Forms.Button();
            this.CleanupTaxReportsButton = new System.Windows.Forms.Button();
            this.CleanupNotificationsButton = new System.Windows.Forms.Button();
            this.CleanupTransactionsButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.UserIDColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.UsernameColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.GenerateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateIncomeReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateBankReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateJurisdictionReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BankIDColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.BankNameColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.JurisdictionNameColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.MainMenuStrip.SuspendLayout();
            this.MainPannel.SuspendLayout();
            this.MainTabController.SuspendLayout();
            this.UserManagementPage.SuspendLayout();
            this.UsersTableLayoutPanel.SuspendLayout();
            this.UsersGroupBox.SuspendLayout();
            this.BankManagementPage.SuspendLayout();
            this.BankTableLayoutPanel.SuspendLayout();
            this.BankGroupBox.SuspendLayout();
            this.TaxJurisdictionManagementPage.SuspendLayout();
            this.JurisdictionTableLayoutPanel.SuspendLayout();
            this.JurisdictionsGroupBox.SuspendLayout();
            this.CleanupTabPage.SuspendLayout();
            this.CleanupTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WarnIconPictureBox)).BeginInit();
            this.CleanupButtonsTableLayout.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenuStrip
            // 
            this.MainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.GenerateToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.MainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MainMenuStrip.Name = "MainMenuStrip";
            this.MainMenuStrip.Size = new System.Drawing.Size(1266, 24);
            this.MainMenuStrip.TabIndex = 1;
            this.MainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectDisconnectToolStripMenuItem,
            this.RefreshToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // connectDisconnectToolStripMenuItem
            // 
            this.connectDisconnectToolStripMenuItem.Name = "connectDisconnectToolStripMenuItem";
            this.connectDisconnectToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.connectDisconnectToolStripMenuItem.Text = "&Connect";
            this.connectDisconnectToolStripMenuItem.Click += new System.EventHandler(this.ConnectDisconnectToolStripMenuItem_Click);
            // 
            // RefreshToolStripMenuItem
            // 
            this.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem";
            this.RefreshToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.RefreshToolStripMenuItem.Text = "&Refresh";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(116, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            // 
            // MainPannel
            // 
            this.MainPannel.Controls.Add(this.MainTabController);
            this.MainPannel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPannel.Location = new System.Drawing.Point(0, 24);
            this.MainPannel.Name = "MainPannel";
            this.MainPannel.Padding = new System.Windows.Forms.Padding(20);
            this.MainPannel.Size = new System.Drawing.Size(1266, 481);
            this.MainPannel.TabIndex = 2;
            // 
            // MainTabController
            // 
            this.MainTabController.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.MainTabController.Controls.Add(this.UserManagementPage);
            this.MainTabController.Controls.Add(this.BankManagementPage);
            this.MainTabController.Controls.Add(this.TaxJurisdictionManagementPage);
            this.MainTabController.Controls.Add(this.CleanupTabPage);
            this.MainTabController.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTabController.HotTrack = true;
            this.MainTabController.Location = new System.Drawing.Point(20, 20);
            this.MainTabController.Margin = new System.Windows.Forms.Padding(50);
            this.MainTabController.Multiline = true;
            this.MainTabController.Name = "MainTabController";
            this.MainTabController.Padding = new System.Drawing.Point(6, 6);
            this.MainTabController.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.MainTabController.SelectedIndex = 0;
            this.MainTabController.Size = new System.Drawing.Size(1226, 441);
            this.MainTabController.TabIndex = 0;
            // 
            // UserManagementPage
            // 
            this.UserManagementPage.Controls.Add(this.UsersTableLayoutPanel);
            this.UserManagementPage.Location = new System.Drawing.Point(33, 4);
            this.UserManagementPage.Name = "UserManagementPage";
            this.UserManagementPage.Padding = new System.Windows.Forms.Padding(3);
            this.UserManagementPage.Size = new System.Drawing.Size(1189, 433);
            this.UserManagementPage.TabIndex = 0;
            this.UserManagementPage.Text = "Users";
            this.UserManagementPage.UseVisualStyleBackColor = true;
            // 
            // UsersTableLayoutPanel
            // 
            this.UsersTableLayoutPanel.ColumnCount = 2;
            this.UsersTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.UsersTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.UsersTableLayoutPanel.Controls.Add(this.UsersGroupBox, 0, 0);
            this.UsersTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UsersTableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.UsersTableLayoutPanel.Name = "UsersTableLayoutPanel";
            this.UsersTableLayoutPanel.RowCount = 2;
            this.UsersTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.UsersTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.UsersTableLayoutPanel.Size = new System.Drawing.Size(1183, 427);
            this.UsersTableLayoutPanel.TabIndex = 0;
            // 
            // UsersGroupBox
            // 
            this.UsersGroupBox.Controls.Add(this.UsersListView);
            this.UsersGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UsersGroupBox.Location = new System.Drawing.Point(3, 3);
            this.UsersGroupBox.Name = "UsersGroupBox";
            this.UsersTableLayoutPanel.SetRowSpan(this.UsersGroupBox, 2);
            this.UsersGroupBox.Size = new System.Drawing.Size(394, 421);
            this.UsersGroupBox.TabIndex = 0;
            this.UsersGroupBox.TabStop = false;
            this.UsersGroupBox.Text = "Users";
            // 
            // UsersListView
            // 
            this.UsersListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.UserIDColumnHeader,
            this.UsernameColumnHeader});
            this.UsersListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UsersListView.HideSelection = false;
            this.UsersListView.Location = new System.Drawing.Point(3, 19);
            this.UsersListView.Name = "UsersListView";
            this.UsersListView.Size = new System.Drawing.Size(388, 399);
            this.UsersListView.TabIndex = 0;
            this.UsersListView.UseCompatibleStateImageBehavior = false;
            this.UsersListView.View = System.Windows.Forms.View.Details;
            // 
            // BankManagementPage
            // 
            this.BankManagementPage.Controls.Add(this.BankTableLayoutPanel);
            this.BankManagementPage.Location = new System.Drawing.Point(33, 4);
            this.BankManagementPage.Name = "BankManagementPage";
            this.BankManagementPage.Padding = new System.Windows.Forms.Padding(3);
            this.BankManagementPage.Size = new System.Drawing.Size(1189, 433);
            this.BankManagementPage.TabIndex = 1;
            this.BankManagementPage.Text = "Banks";
            this.BankManagementPage.UseVisualStyleBackColor = true;
            // 
            // BankTableLayoutPanel
            // 
            this.BankTableLayoutPanel.ColumnCount = 2;
            this.BankTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.BankTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.BankTableLayoutPanel.Controls.Add(this.BankGroupBox, 0, 0);
            this.BankTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BankTableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.BankTableLayoutPanel.Name = "BankTableLayoutPanel";
            this.BankTableLayoutPanel.RowCount = 2;
            this.BankTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.BankTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.BankTableLayoutPanel.Size = new System.Drawing.Size(1183, 427);
            this.BankTableLayoutPanel.TabIndex = 0;
            // 
            // BankGroupBox
            // 
            this.BankGroupBox.Controls.Add(this.BanksListView);
            this.BankGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BankGroupBox.Location = new System.Drawing.Point(3, 3);
            this.BankGroupBox.Name = "BankGroupBox";
            this.BankTableLayoutPanel.SetRowSpan(this.BankGroupBox, 2);
            this.BankGroupBox.Size = new System.Drawing.Size(394, 421);
            this.BankGroupBox.TabIndex = 1;
            this.BankGroupBox.TabStop = false;
            this.BankGroupBox.Text = "Banks";
            // 
            // BanksListView
            // 
            this.BanksListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.BankIDColumnHeader,
            this.BankNameColumnHeader});
            this.BanksListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BanksListView.HideSelection = false;
            this.BanksListView.Location = new System.Drawing.Point(3, 19);
            this.BanksListView.Name = "BanksListView";
            this.BanksListView.Size = new System.Drawing.Size(388, 399);
            this.BanksListView.TabIndex = 1;
            this.BanksListView.UseCompatibleStateImageBehavior = false;
            this.BanksListView.View = System.Windows.Forms.View.Details;
            // 
            // TaxJurisdictionManagementPage
            // 
            this.TaxJurisdictionManagementPage.Controls.Add(this.JurisdictionTableLayoutPanel);
            this.TaxJurisdictionManagementPage.Location = new System.Drawing.Point(33, 4);
            this.TaxJurisdictionManagementPage.Name = "TaxJurisdictionManagementPage";
            this.TaxJurisdictionManagementPage.Padding = new System.Windows.Forms.Padding(3);
            this.TaxJurisdictionManagementPage.Size = new System.Drawing.Size(1189, 433);
            this.TaxJurisdictionManagementPage.TabIndex = 2;
            this.TaxJurisdictionManagementPage.Text = "Jurisdictions";
            this.TaxJurisdictionManagementPage.UseVisualStyleBackColor = true;
            // 
            // JurisdictionTableLayoutPanel
            // 
            this.JurisdictionTableLayoutPanel.ColumnCount = 2;
            this.JurisdictionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.JurisdictionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.JurisdictionTableLayoutPanel.Controls.Add(this.JurisdictionsGroupBox, 0, 0);
            this.JurisdictionTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.JurisdictionTableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.JurisdictionTableLayoutPanel.Name = "JurisdictionTableLayoutPanel";
            this.JurisdictionTableLayoutPanel.RowCount = 2;
            this.JurisdictionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.JurisdictionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.JurisdictionTableLayoutPanel.Size = new System.Drawing.Size(1183, 427);
            this.JurisdictionTableLayoutPanel.TabIndex = 0;
            // 
            // JurisdictionsGroupBox
            // 
            this.JurisdictionsGroupBox.Controls.Add(this.JurisdictionsListView);
            this.JurisdictionsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.JurisdictionsGroupBox.Location = new System.Drawing.Point(3, 3);
            this.JurisdictionsGroupBox.Name = "JurisdictionsGroupBox";
            this.JurisdictionTableLayoutPanel.SetRowSpan(this.JurisdictionsGroupBox, 2);
            this.JurisdictionsGroupBox.Size = new System.Drawing.Size(394, 421);
            this.JurisdictionsGroupBox.TabIndex = 1;
            this.JurisdictionsGroupBox.TabStop = false;
            this.JurisdictionsGroupBox.Text = "Jurisdictions";
            // 
            // JurisdictionsListView
            // 
            this.JurisdictionsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.JurisdictionNameColumnHeader});
            this.JurisdictionsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.JurisdictionsListView.HideSelection = false;
            this.JurisdictionsListView.Location = new System.Drawing.Point(3, 19);
            this.JurisdictionsListView.Name = "JurisdictionsListView";
            this.JurisdictionsListView.Size = new System.Drawing.Size(388, 399);
            this.JurisdictionsListView.TabIndex = 1;
            this.JurisdictionsListView.UseCompatibleStateImageBehavior = false;
            this.JurisdictionsListView.View = System.Windows.Forms.View.Details;
            // 
            // CleanupTabPage
            // 
            this.CleanupTabPage.Controls.Add(this.CleanupTableLayoutPanel);
            this.CleanupTabPage.Location = new System.Drawing.Point(33, 4);
            this.CleanupTabPage.Name = "CleanupTabPage";
            this.CleanupTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.CleanupTabPage.Size = new System.Drawing.Size(1189, 433);
            this.CleanupTabPage.TabIndex = 4;
            this.CleanupTabPage.Text = "Cleanup";
            this.CleanupTabPage.UseVisualStyleBackColor = true;
            // 
            // CleanupTableLayoutPanel
            // 
            this.CleanupTableLayoutPanel.ColumnCount = 1;
            this.CleanupTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.CleanupTableLayoutPanel.Controls.Add(this.WarnIconPictureBox, 0, 0);
            this.CleanupTableLayoutPanel.Controls.Add(this.SlowDownLabel, 0, 1);
            this.CleanupTableLayoutPanel.Controls.Add(this.CleanupExplinationLabel, 0, 2);
            this.CleanupTableLayoutPanel.Controls.Add(this.CleanupButtonsTableLayout, 0, 3);
            this.CleanupTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CleanupTableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.CleanupTableLayoutPanel.Name = "CleanupTableLayoutPanel";
            this.CleanupTableLayoutPanel.RowCount = 4;
            this.CleanupTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.CleanupTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.CleanupTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.CleanupTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.CleanupTableLayoutPanel.Size = new System.Drawing.Size(1183, 427);
            this.CleanupTableLayoutPanel.TabIndex = 0;
            // 
            // WarnIconPictureBox
            // 
            this.WarnIconPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WarnIconPictureBox.Image = global::Igtampe.LitterBox.Properties.Resources.WarnIcon;
            this.WarnIconPictureBox.Location = new System.Drawing.Point(3, 3);
            this.WarnIconPictureBox.Name = "WarnIconPictureBox";
            this.WarnIconPictureBox.Size = new System.Drawing.Size(1177, 114);
            this.WarnIconPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.WarnIconPictureBox.TabIndex = 0;
            this.WarnIconPictureBox.TabStop = false;
            // 
            // SlowDownLabel
            // 
            this.SlowDownLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SlowDownLabel.AutoSize = true;
            this.SlowDownLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.SlowDownLabel.Location = new System.Drawing.Point(3, 125);
            this.SlowDownLabel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.SlowDownLabel.Name = "SlowDownLabel";
            this.SlowDownLabel.Size = new System.Drawing.Size(1177, 30);
            this.SlowDownLabel.TabIndex = 1;
            this.SlowDownLabel.Text = "Slow down there bud";
            this.SlowDownLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // CleanupExplinationLabel
            // 
            this.CleanupExplinationLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.CleanupExplinationLabel.Location = new System.Drawing.Point(363, 160);
            this.CleanupExplinationLabel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.CleanupExplinationLabel.Name = "CleanupExplinationLabel";
            this.CleanupExplinationLabel.Size = new System.Drawing.Size(456, 52);
            this.CleanupExplinationLabel.TabIndex = 2;
            this.CleanupExplinationLabel.Text = resources.GetString("CleanupExplinationLabel.Text");
            this.CleanupExplinationLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // CleanupButtonsTableLayout
            // 
            this.CleanupButtonsTableLayout.ColumnCount = 2;
            this.CleanupButtonsTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.CleanupButtonsTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.CleanupButtonsTableLayout.Controls.Add(this.CleanupCertifiedItemsButton, 1, 1);
            this.CleanupButtonsTableLayout.Controls.Add(this.CleanupTaxReportsButton, 0, 1);
            this.CleanupButtonsTableLayout.Controls.Add(this.CleanupNotificationsButton, 1, 0);
            this.CleanupButtonsTableLayout.Controls.Add(this.CleanupTransactionsButton, 0, 0);
            this.CleanupButtonsTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CleanupButtonsTableLayout.Location = new System.Drawing.Point(3, 215);
            this.CleanupButtonsTableLayout.Name = "CleanupButtonsTableLayout";
            this.CleanupButtonsTableLayout.Padding = new System.Windows.Forms.Padding(3);
            this.CleanupButtonsTableLayout.RowCount = 2;
            this.CleanupButtonsTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.CleanupButtonsTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.CleanupButtonsTableLayout.Size = new System.Drawing.Size(1177, 209);
            this.CleanupButtonsTableLayout.TabIndex = 3;
            // 
            // CleanupCertifiedItemsButton
            // 
            this.CleanupCertifiedItemsButton.Location = new System.Drawing.Point(598, 114);
            this.CleanupCertifiedItemsButton.Margin = new System.Windows.Forms.Padding(10);
            this.CleanupCertifiedItemsButton.Name = "CleanupCertifiedItemsButton";
            this.CleanupCertifiedItemsButton.Size = new System.Drawing.Size(260, 30);
            this.CleanupCertifiedItemsButton.TabIndex = 3;
            this.CleanupCertifiedItemsButton.Text = "Delete Certified Items over 6 months old";
            this.CleanupCertifiedItemsButton.UseVisualStyleBackColor = true;
            this.CleanupCertifiedItemsButton.Click += new System.EventHandler(this.CleanupCertifiedItemsButton_Click);
            // 
            // CleanupTaxReportsButton
            // 
            this.CleanupTaxReportsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CleanupTaxReportsButton.Location = new System.Drawing.Point(318, 114);
            this.CleanupTaxReportsButton.Margin = new System.Windows.Forms.Padding(10);
            this.CleanupTaxReportsButton.Name = "CleanupTaxReportsButton";
            this.CleanupTaxReportsButton.Size = new System.Drawing.Size(260, 30);
            this.CleanupTaxReportsButton.TabIndex = 2;
            this.CleanupTaxReportsButton.Text = "Delete Tax Reports over 6 months old";
            this.CleanupTaxReportsButton.UseVisualStyleBackColor = true;
            this.CleanupTaxReportsButton.Click += new System.EventHandler(this.CleanupTaxReportsButton_Click);
            // 
            // CleanupNotificationsButton
            // 
            this.CleanupNotificationsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CleanupNotificationsButton.Location = new System.Drawing.Point(598, 64);
            this.CleanupNotificationsButton.Margin = new System.Windows.Forms.Padding(10);
            this.CleanupNotificationsButton.Name = "CleanupNotificationsButton";
            this.CleanupNotificationsButton.Size = new System.Drawing.Size(260, 30);
            this.CleanupNotificationsButton.TabIndex = 1;
            this.CleanupNotificationsButton.Text = "Delete Notifications over 1 month old";
            this.CleanupNotificationsButton.UseVisualStyleBackColor = true;
            this.CleanupNotificationsButton.Click += new System.EventHandler(this.CleanupNotificationsButton_Click);
            // 
            // CleanupTransactionsButton
            // 
            this.CleanupTransactionsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CleanupTransactionsButton.Location = new System.Drawing.Point(318, 64);
            this.CleanupTransactionsButton.Margin = new System.Windows.Forms.Padding(10);
            this.CleanupTransactionsButton.Name = "CleanupTransactionsButton";
            this.CleanupTransactionsButton.Size = new System.Drawing.Size(260, 30);
            this.CleanupTransactionsButton.TabIndex = 0;
            this.CleanupTransactionsButton.Text = "Delete Transactions over 3 months old";
            this.CleanupTransactionsButton.UseVisualStyleBackColor = true;
            this.CleanupTransactionsButton.Click += new System.EventHandler(this.CleanupTransactionsButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusProgressBar,
            this.StatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 483);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1266, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusProgressBar
            // 
            this.StatusProgressBar.Name = "StatusProgressBar";
            this.StatusProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(79, 17);
            this.StatusLabel.Text = "Disconnected";
            // 
            // UserIDColumnHeader
            // 
            this.UserIDColumnHeader.Text = "ID";
            // 
            // UsernameColumnHeader
            // 
            this.UsernameColumnHeader.Text = "Name";
            this.UsernameColumnHeader.Width = 250;
            // 
            // GenerateToolStripMenuItem
            // 
            this.GenerateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateIncomeReportToolStripMenuItem,
            this.generateBankReportToolStripMenuItem,
            this.generateJurisdictionReportToolStripMenuItem});
            this.GenerateToolStripMenuItem.Name = "GenerateToolStripMenuItem";
            this.GenerateToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.GenerateToolStripMenuItem.Text = "Generate";
            // 
            // generateIncomeReportToolStripMenuItem
            // 
            this.generateIncomeReportToolStripMenuItem.Name = "generateIncomeReportToolStripMenuItem";
            this.generateIncomeReportToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.generateIncomeReportToolStripMenuItem.Text = "Generate &Income Report";
            // 
            // generateBankReportToolStripMenuItem
            // 
            this.generateBankReportToolStripMenuItem.Name = "generateBankReportToolStripMenuItem";
            this.generateBankReportToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.generateBankReportToolStripMenuItem.Text = "Generate &Bank Report";
            // 
            // generateJurisdictionReportToolStripMenuItem
            // 
            this.generateJurisdictionReportToolStripMenuItem.Name = "generateJurisdictionReportToolStripMenuItem";
            this.generateJurisdictionReportToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.generateJurisdictionReportToolStripMenuItem.Text = "Generate &Jurisdiction Report";
            // 
            // BankIDColumnHeader
            // 
            this.BankIDColumnHeader.Text = "ID";
            // 
            // BankNameColumnHeader
            // 
            this.BankNameColumnHeader.Text = "Name";
            this.BankNameColumnHeader.Width = 250;
            // 
            // JurisdictionNameColumnHeader
            // 
            this.JurisdictionNameColumnHeader.Text = "Name";
            this.JurisdictionNameColumnHeader.Width = 320;
            // 
            // LaunchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1266, 505);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.MainPannel);
            this.Controls.Add(this.MainMenuStrip);
            this.Name = "LaunchForm";
            this.Text = "LitterBox";
            this.MainMenuStrip.ResumeLayout(false);
            this.MainMenuStrip.PerformLayout();
            this.MainPannel.ResumeLayout(false);
            this.MainTabController.ResumeLayout(false);
            this.UserManagementPage.ResumeLayout(false);
            this.UsersTableLayoutPanel.ResumeLayout(false);
            this.UsersGroupBox.ResumeLayout(false);
            this.BankManagementPage.ResumeLayout(false);
            this.BankTableLayoutPanel.ResumeLayout(false);
            this.BankGroupBox.ResumeLayout(false);
            this.TaxJurisdictionManagementPage.ResumeLayout(false);
            this.JurisdictionTableLayoutPanel.ResumeLayout(false);
            this.JurisdictionsGroupBox.ResumeLayout(false);
            this.CleanupTabPage.ResumeLayout(false);
            this.CleanupTableLayoutPanel.ResumeLayout(false);
            this.CleanupTableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WarnIconPictureBox)).EndInit();
            this.CleanupButtonsTableLayout.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip MainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectDisconnectToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Panel MainPannel;
        private System.Windows.Forms.TabControl MainTabController;
        private System.Windows.Forms.TabPage UserManagementPage;
        private System.Windows.Forms.TabPage BankManagementPage;
        private System.Windows.Forms.TabPage TaxJurisdictionManagementPage;
        private System.Windows.Forms.TabPage CleanupTabPage;
        private System.Windows.Forms.TableLayoutPanel UsersTableLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel BankTableLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel JurisdictionTableLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel CleanupTableLayoutPanel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.GroupBox UsersGroupBox;
        private System.Windows.Forms.GroupBox BankGroupBox;
        private System.Windows.Forms.GroupBox JurisdictionsGroupBox;
        private System.Windows.Forms.PictureBox WarnIconPictureBox;
        private System.Windows.Forms.Label SlowDownLabel;
        private System.Windows.Forms.Label CleanupExplinationLabel;
        private System.Windows.Forms.TableLayoutPanel CleanupButtonsTableLayout;
        private System.Windows.Forms.Button CleanupCertifiedItemsButton;
        private System.Windows.Forms.Button CleanupTaxReportsButton;
        private System.Windows.Forms.Button CleanupNotificationsButton;
        private System.Windows.Forms.Button CleanupTransactionsButton;
        private System.Windows.Forms.ToolStripMenuItem RefreshToolStripMenuItem;
        private System.Windows.Forms.ListView UsersListView;
        private System.Windows.Forms.ListView BanksListView;
        private System.Windows.Forms.ListView JurisdictionsListView;
        private System.Windows.Forms.ToolStripProgressBar StatusProgressBar;
        private System.Windows.Forms.ToolStripMenuItem GenerateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateIncomeReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateBankReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateJurisdictionReportToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader UserIDColumnHeader;
        private System.Windows.Forms.ColumnHeader UsernameColumnHeader;
        private System.Windows.Forms.ColumnHeader BankIDColumnHeader;
        private System.Windows.Forms.ColumnHeader BankNameColumnHeader;
        private System.Windows.Forms.ColumnHeader JurisdictionNameColumnHeader;
    }
}