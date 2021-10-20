
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
            this.MainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectDisconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainPannel = new System.Windows.Forms.Panel();
            this.MainTabController = new System.Windows.Forms.TabControl();
            this.UserManagementPage = new System.Windows.Forms.TabPage();
            this.BankManagementPage = new System.Windows.Forms.TabPage();
            this.TaxJurisdictionManagementPage = new System.Windows.Forms.TabPage();
            this.CleanupTabPage = new System.Windows.Forms.TabPage();
            this.UsersTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.BankTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.JurisdictionTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.CleanupTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.MainMenuStrip.SuspendLayout();
            this.MainPannel.SuspendLayout();
            this.MainTabController.SuspendLayout();
            this.UserManagementPage.SuspendLayout();
            this.BankManagementPage.SuspendLayout();
            this.TaxJurisdictionManagementPage.SuspendLayout();
            this.CleanupTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenuStrip
            // 
            this.MainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
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
            this.connectDisconnectToolStripMenuItem.Click += new System.EventHandler(this.connectDisconnectToolStripMenuItem_Click);
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
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
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
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
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
            // UsersTableLayoutPanel
            // 
            this.UsersTableLayoutPanel.ColumnCount = 2;
            this.UsersTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.UsersTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.UsersTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UsersTableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.UsersTableLayoutPanel.Name = "UsersTableLayoutPanel";
            this.UsersTableLayoutPanel.RowCount = 2;
            this.UsersTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.UsersTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.UsersTableLayoutPanel.Size = new System.Drawing.Size(1183, 427);
            this.UsersTableLayoutPanel.TabIndex = 0;
            // 
            // BankTableLayoutPanel
            // 
            this.BankTableLayoutPanel.ColumnCount = 2;
            this.BankTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.BankTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.BankTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BankTableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.BankTableLayoutPanel.Name = "BankTableLayoutPanel";
            this.BankTableLayoutPanel.RowCount = 2;
            this.BankTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.BankTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.BankTableLayoutPanel.Size = new System.Drawing.Size(1183, 427);
            this.BankTableLayoutPanel.TabIndex = 0;
            // 
            // JurisdictionTableLayoutPanel
            // 
            this.JurisdictionTableLayoutPanel.ColumnCount = 2;
            this.JurisdictionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.JurisdictionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.JurisdictionTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.JurisdictionTableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.JurisdictionTableLayoutPanel.Name = "JurisdictionTableLayoutPanel";
            this.JurisdictionTableLayoutPanel.RowCount = 2;
            this.JurisdictionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.JurisdictionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.JurisdictionTableLayoutPanel.Size = new System.Drawing.Size(1183, 427);
            this.JurisdictionTableLayoutPanel.TabIndex = 0;
            // 
            // CleanupTableLayoutPanel
            // 
            this.CleanupTableLayoutPanel.ColumnCount = 2;
            this.CleanupTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.CleanupTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.CleanupTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CleanupTableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.CleanupTableLayoutPanel.Name = "CleanupTableLayoutPanel";
            this.CleanupTableLayoutPanel.RowCount = 2;
            this.CleanupTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.CleanupTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.CleanupTableLayoutPanel.Size = new System.Drawing.Size(1183, 427);
            this.CleanupTableLayoutPanel.TabIndex = 0;
            // 
            // LaunchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1266, 505);
            this.Controls.Add(this.MainPannel);
            this.Controls.Add(this.MainMenuStrip);
            this.Name = "LaunchForm";
            this.Text = "LitterBox";
            this.MainMenuStrip.ResumeLayout(false);
            this.MainMenuStrip.PerformLayout();
            this.MainPannel.ResumeLayout(false);
            this.MainTabController.ResumeLayout(false);
            this.UserManagementPage.ResumeLayout(false);
            this.BankManagementPage.ResumeLayout(false);
            this.TaxJurisdictionManagementPage.ResumeLayout(false);
            this.CleanupTabPage.ResumeLayout(false);
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
    }
}