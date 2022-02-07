
namespace Igtampe.LandViewPlotter {
    partial class CountryPlotter {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CountryPlotter));
            this.MainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.PreviewGroupBox = new System.Windows.Forms.GroupBox();
            this.PreviewPictureBox = new System.Windows.Forms.PictureBox();
            this.CountryDetailsGroupbox = new System.Windows.Forms.GroupBox();
            this.CountryDetailTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.DistrictsGroupBox = new System.Windows.Forms.GroupBox();
            this.DistrictsListView = new System.Windows.Forms.ListView();
            this.DistrictNameHeader = new System.Windows.Forms.ColumnHeader();
            this.DistrictsContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newDistrictToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditDistrictToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RoadsGroupBox = new System.Windows.Forms.GroupBox();
            this.RoadsListView = new System.Windows.Forms.ListView();
            this.RoadNameHeader = new System.Windows.Forms.ColumnHeader();
            this.RoadContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newRoadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditRoadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteRoadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GraphicalDetailsGroupBox = new System.Windows.Forms.GroupBox();
            this.DetailTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.NameLabel = new System.Windows.Forms.Label();
            this.WidthLabel = new System.Windows.Forms.Label();
            this.HeightLabel = new System.Windows.Forms.Label();
            this.BankAccountLabel = new System.Windows.Forms.Label();
            this.NameBox = new System.Windows.Forms.TextBox();
            this.WidthBox = new System.Windows.Forms.TextBox();
            this.HeightBox = new System.Windows.Forms.TextBox();
            this.BankAccountBox = new System.Windows.Forms.TextBox();
            this.PPSMLabel = new System.Windows.Forms.Label();
            this.PPSMBox = new System.Windows.Forms.TextBox();
            this.WindowMainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CountryPickerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.PreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadCountriesBW = new System.ComponentModel.BackgroundWorker();
            this.SaveCountryBW = new System.ComponentModel.BackgroundWorker();
            this.MainTableLayoutPanel.SuspendLayout();
            this.PreviewGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PreviewPictureBox)).BeginInit();
            this.CountryDetailsGroupbox.SuspendLayout();
            this.CountryDetailTableLayoutPanel.SuspendLayout();
            this.DistrictsGroupBox.SuspendLayout();
            this.DistrictsContextMenu.SuspendLayout();
            this.RoadsGroupBox.SuspendLayout();
            this.RoadContextMenu.SuspendLayout();
            this.GraphicalDetailsGroupBox.SuspendLayout();
            this.DetailTableLayoutPanel.SuspendLayout();
            this.WindowMainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainTableLayoutPanel
            // 
            this.MainTableLayoutPanel.ColumnCount = 2;
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTableLayoutPanel.Controls.Add(this.PreviewGroupBox, 1, 0);
            this.MainTableLayoutPanel.Controls.Add(this.CountryDetailsGroupbox, 0, 0);
            this.MainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.MainTableLayoutPanel.Name = "MainTableLayoutPanel";
            this.MainTableLayoutPanel.RowCount = 1;
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTableLayoutPanel.Size = new System.Drawing.Size(1009, 585);
            this.MainTableLayoutPanel.TabIndex = 0;
            // 
            // PreviewGroupBox
            // 
            this.PreviewGroupBox.Controls.Add(this.PreviewPictureBox);
            this.PreviewGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PreviewGroupBox.Location = new System.Drawing.Point(403, 3);
            this.PreviewGroupBox.Name = "PreviewGroupBox";
            this.PreviewGroupBox.Size = new System.Drawing.Size(603, 579);
            this.PreviewGroupBox.TabIndex = 0;
            this.PreviewGroupBox.TabStop = false;
            this.PreviewGroupBox.Text = "Preview";
            // 
            // PreviewPictureBox
            // 
            this.PreviewPictureBox.BackgroundImage = global::Igtampe.LandViewPlotter.Properties.Resources.TinyDiagBWTile;
            this.PreviewPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PreviewPictureBox.Location = new System.Drawing.Point(3, 19);
            this.PreviewPictureBox.Name = "PreviewPictureBox";
            this.PreviewPictureBox.Size = new System.Drawing.Size(597, 557);
            this.PreviewPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PreviewPictureBox.TabIndex = 0;
            this.PreviewPictureBox.TabStop = false;
            this.PreviewPictureBox.Click += new System.EventHandler(this.PreviewPictureBox_Click);
            // 
            // CountryDetailsGroupbox
            // 
            this.CountryDetailsGroupbox.Controls.Add(this.CountryDetailTableLayoutPanel);
            this.CountryDetailsGroupbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CountryDetailsGroupbox.Location = new System.Drawing.Point(3, 3);
            this.CountryDetailsGroupbox.Name = "CountryDetailsGroupbox";
            this.CountryDetailsGroupbox.Size = new System.Drawing.Size(394, 579);
            this.CountryDetailsGroupbox.TabIndex = 1;
            this.CountryDetailsGroupbox.TabStop = false;
            this.CountryDetailsGroupbox.Text = "Country Details";
            // 
            // CountryDetailTableLayoutPanel
            // 
            this.CountryDetailTableLayoutPanel.ColumnCount = 2;
            this.CountryDetailTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.CountryDetailTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.CountryDetailTableLayoutPanel.Controls.Add(this.DistrictsGroupBox, 0, 0);
            this.CountryDetailTableLayoutPanel.Controls.Add(this.RoadsGroupBox, 1, 0);
            this.CountryDetailTableLayoutPanel.Controls.Add(this.GraphicalDetailsGroupBox, 0, 1);
            this.CountryDetailTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CountryDetailTableLayoutPanel.Location = new System.Drawing.Point(3, 19);
            this.CountryDetailTableLayoutPanel.Name = "CountryDetailTableLayoutPanel";
            this.CountryDetailTableLayoutPanel.RowCount = 2;
            this.CountryDetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.CountryDetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 175F));
            this.CountryDetailTableLayoutPanel.Size = new System.Drawing.Size(388, 557);
            this.CountryDetailTableLayoutPanel.TabIndex = 0;
            // 
            // DistrictsGroupBox
            // 
            this.DistrictsGroupBox.Controls.Add(this.DistrictsListView);
            this.DistrictsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DistrictsGroupBox.Location = new System.Drawing.Point(3, 3);
            this.DistrictsGroupBox.Name = "DistrictsGroupBox";
            this.DistrictsGroupBox.Size = new System.Drawing.Size(188, 376);
            this.DistrictsGroupBox.TabIndex = 0;
            this.DistrictsGroupBox.TabStop = false;
            this.DistrictsGroupBox.Text = "Districts";
            // 
            // DistrictsListView
            // 
            this.DistrictsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.DistrictNameHeader});
            this.DistrictsListView.ContextMenuStrip = this.DistrictsContextMenu;
            this.DistrictsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DistrictsListView.FullRowSelect = true;
            this.DistrictsListView.HideSelection = false;
            this.DistrictsListView.Location = new System.Drawing.Point(3, 19);
            this.DistrictsListView.Name = "DistrictsListView";
            this.DistrictsListView.Size = new System.Drawing.Size(182, 354);
            this.DistrictsListView.TabIndex = 0;
            this.DistrictsListView.UseCompatibleStateImageBehavior = false;
            this.DistrictsListView.View = System.Windows.Forms.View.Details;
            // 
            // DistrictNameHeader
            // 
            this.DistrictNameHeader.Text = "Districts";
            this.DistrictNameHeader.Width = 120;
            // 
            // DistrictsContextMenu
            // 
            this.DistrictsContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newDistrictToolStripMenuItem,
            this.EditDistrictToolStripMenuItem});
            this.DistrictsContextMenu.Name = "DistrictsContextMenu";
            this.DistrictsContextMenu.Size = new System.Drawing.Size(139, 48);
            // 
            // newDistrictToolStripMenuItem
            // 
            this.newDistrictToolStripMenuItem.Name = "newDistrictToolStripMenuItem";
            this.newDistrictToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.newDistrictToolStripMenuItem.Text = "New District";
            this.newDistrictToolStripMenuItem.Click += new System.EventHandler(this.NewDistrictToolStripMenuItem_Click);
            // 
            // EditDistrictToolStripMenuItem
            // 
            this.EditDistrictToolStripMenuItem.Name = "EditDistrictToolStripMenuItem";
            this.EditDistrictToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.EditDistrictToolStripMenuItem.Text = "Edit District";
            this.EditDistrictToolStripMenuItem.Click += new System.EventHandler(this.EditDistrictToolStripMenuItem_Click);
            // 
            // RoadsGroupBox
            // 
            this.RoadsGroupBox.Controls.Add(this.RoadsListView);
            this.RoadsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RoadsGroupBox.Location = new System.Drawing.Point(197, 3);
            this.RoadsGroupBox.Name = "RoadsGroupBox";
            this.RoadsGroupBox.Size = new System.Drawing.Size(188, 376);
            this.RoadsGroupBox.TabIndex = 1;
            this.RoadsGroupBox.TabStop = false;
            this.RoadsGroupBox.Text = "Roads";
            // 
            // RoadsListView
            // 
            this.RoadsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.RoadNameHeader});
            this.RoadsListView.ContextMenuStrip = this.RoadContextMenu;
            this.RoadsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RoadsListView.FullRowSelect = true;
            this.RoadsListView.HideSelection = false;
            this.RoadsListView.Location = new System.Drawing.Point(3, 19);
            this.RoadsListView.Name = "RoadsListView";
            this.RoadsListView.Size = new System.Drawing.Size(182, 354);
            this.RoadsListView.TabIndex = 0;
            this.RoadsListView.UseCompatibleStateImageBehavior = false;
            this.RoadsListView.View = System.Windows.Forms.View.Details;
            // 
            // RoadNameHeader
            // 
            this.RoadNameHeader.Text = "Road";
            this.RoadNameHeader.Width = 150;
            // 
            // RoadContextMenu
            // 
            this.RoadContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newRoadToolStripMenuItem,
            this.EditRoadToolStripMenuItem,
            this.DeleteRoadToolStripMenuItem});
            this.RoadContextMenu.Name = "RoadContextMenu";
            this.RoadContextMenu.Size = new System.Drawing.Size(138, 70);
            // 
            // newRoadToolStripMenuItem
            // 
            this.newRoadToolStripMenuItem.Name = "newRoadToolStripMenuItem";
            this.newRoadToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.newRoadToolStripMenuItem.Text = "New Road";
            this.newRoadToolStripMenuItem.Click += new System.EventHandler(this.NewRoadToolStripMenuItem_Click);
            // 
            // EditRoadToolStripMenuItem
            // 
            this.EditRoadToolStripMenuItem.Name = "EditRoadToolStripMenuItem";
            this.EditRoadToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.EditRoadToolStripMenuItem.Text = "Edit Road";
            this.EditRoadToolStripMenuItem.Click += new System.EventHandler(this.EditRoadToolStripMenuItem_Click);
            // 
            // DeleteRoadToolStripMenuItem
            // 
            this.DeleteRoadToolStripMenuItem.Name = "DeleteRoadToolStripMenuItem";
            this.DeleteRoadToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.DeleteRoadToolStripMenuItem.Text = "Delete Road";
            this.DeleteRoadToolStripMenuItem.Click += new System.EventHandler(this.DeleteRoadToolStripMenuItem_Click);
            // 
            // GraphicalDetailsGroupBox
            // 
            this.CountryDetailTableLayoutPanel.SetColumnSpan(this.GraphicalDetailsGroupBox, 2);
            this.GraphicalDetailsGroupBox.Controls.Add(this.DetailTableLayoutPanel);
            this.GraphicalDetailsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GraphicalDetailsGroupBox.Location = new System.Drawing.Point(3, 385);
            this.GraphicalDetailsGroupBox.Name = "GraphicalDetailsGroupBox";
            this.GraphicalDetailsGroupBox.Size = new System.Drawing.Size(382, 169);
            this.GraphicalDetailsGroupBox.TabIndex = 2;
            this.GraphicalDetailsGroupBox.TabStop = false;
            this.GraphicalDetailsGroupBox.Text = "Details";
            // 
            // DetailTableLayoutPanel
            // 
            this.DetailTableLayoutPanel.ColumnCount = 2;
            this.DetailTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.DetailTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.DetailTableLayoutPanel.Controls.Add(this.NameLabel, 0, 0);
            this.DetailTableLayoutPanel.Controls.Add(this.WidthLabel, 0, 1);
            this.DetailTableLayoutPanel.Controls.Add(this.HeightLabel, 0, 2);
            this.DetailTableLayoutPanel.Controls.Add(this.BankAccountLabel, 0, 3);
            this.DetailTableLayoutPanel.Controls.Add(this.NameBox, 1, 0);
            this.DetailTableLayoutPanel.Controls.Add(this.WidthBox, 1, 1);
            this.DetailTableLayoutPanel.Controls.Add(this.HeightBox, 1, 2);
            this.DetailTableLayoutPanel.Controls.Add(this.BankAccountBox, 1, 3);
            this.DetailTableLayoutPanel.Controls.Add(this.PPSMLabel, 0, 5);
            this.DetailTableLayoutPanel.Controls.Add(this.PPSMBox, 1, 5);
            this.DetailTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DetailTableLayoutPanel.Location = new System.Drawing.Point(3, 19);
            this.DetailTableLayoutPanel.Name = "DetailTableLayoutPanel";
            this.DetailTableLayoutPanel.RowCount = 6;
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.DetailTableLayoutPanel.Size = new System.Drawing.Size(376, 147);
            this.DetailTableLayoutPanel.TabIndex = 0;
            // 
            // NameLabel
            // 
            this.NameLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.NameLabel.Location = new System.Drawing.Point(3, 0);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(100, 23);
            this.NameLabel.TabIndex = 0;
            this.NameLabel.Text = "Name";
            this.NameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // WidthLabel
            // 
            this.WidthLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.WidthLabel.Location = new System.Drawing.Point(3, 29);
            this.WidthLabel.Name = "WidthLabel";
            this.WidthLabel.Size = new System.Drawing.Size(100, 23);
            this.WidthLabel.TabIndex = 1;
            this.WidthLabel.Text = "Width";
            this.WidthLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // HeightLabel
            // 
            this.HeightLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.HeightLabel.Location = new System.Drawing.Point(3, 58);
            this.HeightLabel.Name = "HeightLabel";
            this.HeightLabel.Size = new System.Drawing.Size(100, 23);
            this.HeightLabel.TabIndex = 2;
            this.HeightLabel.Text = "Height";
            this.HeightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BankAccountLabel
            // 
            this.BankAccountLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.BankAccountLabel.Location = new System.Drawing.Point(3, 87);
            this.BankAccountLabel.Name = "BankAccountLabel";
            this.BankAccountLabel.Size = new System.Drawing.Size(100, 23);
            this.BankAccountLabel.TabIndex = 3;
            this.BankAccountLabel.Text = "Bank Account";
            this.BankAccountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // NameBox
            // 
            this.NameBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.NameBox.Location = new System.Drawing.Point(109, 3);
            this.NameBox.Name = "NameBox";
            this.NameBox.Size = new System.Drawing.Size(264, 23);
            this.NameBox.TabIndex = 4;
            // 
            // WidthBox
            // 
            this.WidthBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.WidthBox.Location = new System.Drawing.Point(109, 32);
            this.WidthBox.Name = "WidthBox";
            this.WidthBox.Size = new System.Drawing.Size(264, 23);
            this.WidthBox.TabIndex = 5;
            // 
            // HeightBox
            // 
            this.HeightBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.HeightBox.Location = new System.Drawing.Point(109, 61);
            this.HeightBox.Name = "HeightBox";
            this.HeightBox.Size = new System.Drawing.Size(264, 23);
            this.HeightBox.TabIndex = 6;
            // 
            // BankAccountBox
            // 
            this.BankAccountBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.BankAccountBox.Location = new System.Drawing.Point(109, 90);
            this.BankAccountBox.Name = "BankAccountBox";
            this.BankAccountBox.Size = new System.Drawing.Size(264, 23);
            this.BankAccountBox.TabIndex = 7;
            // 
            // PPSMLabel
            // 
            this.PPSMLabel.AutoSize = true;
            this.PPSMLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PPSMLabel.Location = new System.Drawing.Point(3, 116);
            this.PPSMLabel.Name = "PPSMLabel";
            this.PPSMLabel.Size = new System.Drawing.Size(100, 31);
            this.PPSMLabel.TabIndex = 8;
            this.PPSMLabel.Text = "Price per m²";
            this.PPSMLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PPSMBox
            // 
            this.PPSMBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PPSMBox.Location = new System.Drawing.Point(109, 119);
            this.PPSMBox.Name = "PPSMBox";
            this.PPSMBox.Size = new System.Drawing.Size(264, 23);
            this.PPSMBox.TabIndex = 9;
            // 
            // WindowMainMenuStrip
            // 
            this.WindowMainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.WindowMainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.WindowMainMenuStrip.Name = "WindowMainMenuStrip";
            this.WindowMainMenuStrip.Size = new System.Drawing.Size(1009, 24);
            this.WindowMainMenuStrip.TabIndex = 2;
            this.WindowMainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CountryPickerToolStripMenuItem,
            this.toolStripSeparator,
            this.saveToolStripMenuItem,
            this.toolStripSeparator1,
            this.PreviewToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // CountryPickerToolStripMenuItem
            // 
            this.CountryPickerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("CountryPickerToolStripMenuItem.Image")));
            this.CountryPickerToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CountryPickerToolStripMenuItem.Name = "CountryPickerToolStripMenuItem";
            this.CountryPickerToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.CountryPickerToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.CountryPickerToolStripMenuItem.Text = "Country &Picker";
            this.CountryPickerToolStripMenuItem.Click += new System.EventHandler(this.OpenMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(192, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(192, 6);
            // 
            // PreviewToolStripMenuItem
            // 
            this.PreviewToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("PreviewToolStripMenuItem.Image")));
            this.PreviewToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PreviewToolStripMenuItem.Name = "PreviewToolStripMenuItem";
            this.PreviewToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.PreviewToolStripMenuItem.Text = "Pre&view";
            this.PreviewToolStripMenuItem.Click += new System.EventHandler(this.PreviewPictureBox_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(192, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
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
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // CountryPlotter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1009, 585);
            this.Controls.Add(this.WindowMainMenuStrip);
            this.Controls.Add(this.MainTableLayoutPanel);
            this.Name = "CountryPlotter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CountryPlotter";
            this.MainTableLayoutPanel.ResumeLayout(false);
            this.PreviewGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PreviewPictureBox)).EndInit();
            this.CountryDetailsGroupbox.ResumeLayout(false);
            this.CountryDetailTableLayoutPanel.ResumeLayout(false);
            this.DistrictsGroupBox.ResumeLayout(false);
            this.DistrictsContextMenu.ResumeLayout(false);
            this.RoadsGroupBox.ResumeLayout(false);
            this.RoadContextMenu.ResumeLayout(false);
            this.GraphicalDetailsGroupBox.ResumeLayout(false);
            this.DetailTableLayoutPanel.ResumeLayout(false);
            this.DetailTableLayoutPanel.PerformLayout();
            this.WindowMainMenuStrip.ResumeLayout(false);
            this.WindowMainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel MainTableLayoutPanel;
        private System.Windows.Forms.GroupBox PreviewGroupBox;
        private System.Windows.Forms.PictureBox PreviewPictureBox;
        private System.Windows.Forms.GroupBox CountryDetailsGroupbox;
        private System.Windows.Forms.TableLayoutPanel CountryDetailTableLayoutPanel;
        private System.Windows.Forms.GroupBox DistrictsGroupBox;
        private System.Windows.Forms.ListView DistrictsListView;
        private System.Windows.Forms.ColumnHeader DistrictNameHeader;
        private System.Windows.Forms.GroupBox RoadsGroupBox;
        private System.Windows.Forms.ListView RoadsListView;
        private System.Windows.Forms.ColumnHeader RoadNameHeader;
        private System.Windows.Forms.GroupBox GraphicalDetailsGroupBox;
        private System.Windows.Forms.ContextMenuStrip DistrictsContextMenu;
        private System.Windows.Forms.ToolStripMenuItem newDistrictToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditDistrictToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip RoadContextMenu;
        private System.Windows.Forms.ToolStripMenuItem newRoadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditRoadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteRoadToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel DetailTableLayoutPanel;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Label WidthLabel;
        private System.Windows.Forms.Label HeightLabel;
        private System.Windows.Forms.Label BankAccountLabel;
        private System.Windows.Forms.TextBox NameBox;
        private System.Windows.Forms.TextBox WidthBox;
        private System.Windows.Forms.TextBox HeightBox;
        private System.Windows.Forms.TextBox BankAccountBox;
        private System.Windows.Forms.MenuStrip WindowMainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CountryPickerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem PreviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label PPSMLabel;
        private System.Windows.Forms.TextBox PPSMBox;
        private System.ComponentModel.BackgroundWorker LoadCountriesBW;
        private System.ComponentModel.BackgroundWorker SaveCountryBW;
    }
}