
namespace Igtampe.LandViewPlotter {
    partial class DistrictPlotter {
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
            this.NameLabel = new System.Windows.Forms.Label();
            this.BankAccountLabel = new System.Windows.Forms.Label();
            this.NameBox = new System.Windows.Forms.TextBox();
            this.BankAccountBox = new System.Windows.Forms.TextBox();
            this.PPSMLabel = new System.Windows.Forms.Label();
            this.PPSMBox = new System.Windows.Forms.TextBox();
            this.DetailTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.PointsLabel = new System.Windows.Forms.Label();
            this.PointsBox = new System.Windows.Forms.TextBox();
            this.MainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.PreviewGroupBox = new System.Windows.Forms.GroupBox();
            this.PreviewPictureBox = new System.Windows.Forms.PictureBox();
            this.DistrictDetailsGroupbox = new System.Windows.Forms.GroupBox();
            this.CountryDetailTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.PlotGroupBox = new System.Windows.Forms.GroupBox();
            this.PlotsListView = new System.Windows.Forms.ListView();
            this.PlotNameHeader = new System.Windows.Forms.ColumnHeader();
            this.PlotContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NewPlotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditPlotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GraphicalDetailsGroupBox = new System.Windows.Forms.GroupBox();
            this.CanButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.DetailTableLayoutPanel.SuspendLayout();
            this.MainTableLayoutPanel.SuspendLayout();
            this.PreviewGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PreviewPictureBox)).BeginInit();
            this.DistrictDetailsGroupbox.SuspendLayout();
            this.CountryDetailTableLayoutPanel.SuspendLayout();
            this.PlotGroupBox.SuspendLayout();
            this.PlotContextMenu.SuspendLayout();
            this.GraphicalDetailsGroupBox.SuspendLayout();
            this.SuspendLayout();
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
            // BankAccountLabel
            // 
            this.BankAccountLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.BankAccountLabel.Location = new System.Drawing.Point(3, 29);
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
            // BankAccountBox
            // 
            this.BankAccountBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.BankAccountBox.Location = new System.Drawing.Point(109, 32);
            this.BankAccountBox.Name = "BankAccountBox";
            this.BankAccountBox.Size = new System.Drawing.Size(264, 23);
            this.BankAccountBox.TabIndex = 7;
            // 
            // PPSMLabel
            // 
            this.PPSMLabel.AutoSize = true;
            this.PPSMLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PPSMLabel.Location = new System.Drawing.Point(3, 58);
            this.PPSMLabel.Name = "PPSMLabel";
            this.PPSMLabel.Size = new System.Drawing.Size(100, 29);
            this.PPSMLabel.TabIndex = 8;
            this.PPSMLabel.Text = "Price per m²";
            this.PPSMLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PPSMBox
            // 
            this.PPSMBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PPSMBox.Location = new System.Drawing.Point(109, 61);
            this.PPSMBox.Name = "PPSMBox";
            this.PPSMBox.Size = new System.Drawing.Size(264, 23);
            this.PPSMBox.TabIndex = 9;
            // 
            // DetailTableLayoutPanel
            // 
            this.DetailTableLayoutPanel.ColumnCount = 2;
            this.DetailTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.DetailTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.DetailTableLayoutPanel.Controls.Add(this.NameLabel, 0, 0);
            this.DetailTableLayoutPanel.Controls.Add(this.BankAccountLabel, 0, 3);
            this.DetailTableLayoutPanel.Controls.Add(this.NameBox, 1, 0);
            this.DetailTableLayoutPanel.Controls.Add(this.BankAccountBox, 1, 3);
            this.DetailTableLayoutPanel.Controls.Add(this.PPSMLabel, 0, 5);
            this.DetailTableLayoutPanel.Controls.Add(this.PPSMBox, 1, 5);
            this.DetailTableLayoutPanel.Controls.Add(this.PointsLabel, 0, 6);
            this.DetailTableLayoutPanel.Controls.Add(this.PointsBox, 1, 6);
            this.DetailTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DetailTableLayoutPanel.Location = new System.Drawing.Point(3, 19);
            this.DetailTableLayoutPanel.Name = "DetailTableLayoutPanel";
            this.DetailTableLayoutPanel.RowCount = 7;
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.DetailTableLayoutPanel.Size = new System.Drawing.Size(376, 168);
            this.DetailTableLayoutPanel.TabIndex = 0;
            // 
            // PointsLabel
            // 
            this.PointsLabel.AutoSize = true;
            this.PointsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PointsLabel.Location = new System.Drawing.Point(3, 87);
            this.PointsLabel.Name = "PointsLabel";
            this.PointsLabel.Size = new System.Drawing.Size(100, 81);
            this.PointsLabel.TabIndex = 10;
            this.PointsLabel.Text = "Points";
            this.PointsLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // PointsBox
            // 
            this.PointsBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PointsBox.Location = new System.Drawing.Point(109, 90);
            this.PointsBox.Multiline = true;
            this.PointsBox.Name = "PointsBox";
            this.PointsBox.Size = new System.Drawing.Size(264, 75);
            this.PointsBox.TabIndex = 11;
            // 
            // MainTableLayoutPanel
            // 
            this.MainTableLayoutPanel.ColumnCount = 3;
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.MainTableLayoutPanel.Controls.Add(this.PreviewGroupBox, 1, 0);
            this.MainTableLayoutPanel.Controls.Add(this.DistrictDetailsGroupbox, 0, 0);
            this.MainTableLayoutPanel.Controls.Add(this.CanButton, 2, 1);
            this.MainTableLayoutPanel.Controls.Add(this.OKButton, 1, 1);
            this.MainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.MainTableLayoutPanel.Name = "MainTableLayoutPanel";
            this.MainTableLayoutPanel.RowCount = 2;
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainTableLayoutPanel.Size = new System.Drawing.Size(984, 574);
            this.MainTableLayoutPanel.TabIndex = 3;
            // 
            // PreviewGroupBox
            // 
            this.MainTableLayoutPanel.SetColumnSpan(this.PreviewGroupBox, 2);
            this.PreviewGroupBox.Controls.Add(this.PreviewPictureBox);
            this.PreviewGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PreviewGroupBox.Location = new System.Drawing.Point(403, 3);
            this.PreviewGroupBox.Name = "PreviewGroupBox";
            this.PreviewGroupBox.Size = new System.Drawing.Size(578, 539);
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
            this.PreviewPictureBox.Size = new System.Drawing.Size(572, 517);
            this.PreviewPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PreviewPictureBox.TabIndex = 0;
            this.PreviewPictureBox.TabStop = false;
            this.PreviewPictureBox.Click += new System.EventHandler(this.PreviewPictureBox_Click);
            // 
            // DistrictDetailsGroupbox
            // 
            this.DistrictDetailsGroupbox.Controls.Add(this.CountryDetailTableLayoutPanel);
            this.DistrictDetailsGroupbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DistrictDetailsGroupbox.Location = new System.Drawing.Point(3, 3);
            this.DistrictDetailsGroupbox.Name = "DistrictDetailsGroupbox";
            this.DistrictDetailsGroupbox.Size = new System.Drawing.Size(394, 539);
            this.DistrictDetailsGroupbox.TabIndex = 1;
            this.DistrictDetailsGroupbox.TabStop = false;
            this.DistrictDetailsGroupbox.Text = "District Details";
            // 
            // CountryDetailTableLayoutPanel
            // 
            this.CountryDetailTableLayoutPanel.ColumnCount = 2;
            this.CountryDetailTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.CountryDetailTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.CountryDetailTableLayoutPanel.Controls.Add(this.PlotGroupBox, 0, 0);
            this.CountryDetailTableLayoutPanel.Controls.Add(this.GraphicalDetailsGroupBox, 0, 1);
            this.CountryDetailTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CountryDetailTableLayoutPanel.Location = new System.Drawing.Point(3, 19);
            this.CountryDetailTableLayoutPanel.Name = "CountryDetailTableLayoutPanel";
            this.CountryDetailTableLayoutPanel.RowCount = 2;
            this.CountryDetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.CountryDetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 196F));
            this.CountryDetailTableLayoutPanel.Size = new System.Drawing.Size(388, 517);
            this.CountryDetailTableLayoutPanel.TabIndex = 0;
            // 
            // PlotGroupBox
            // 
            this.CountryDetailTableLayoutPanel.SetColumnSpan(this.PlotGroupBox, 2);
            this.PlotGroupBox.Controls.Add(this.PlotsListView);
            this.PlotGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PlotGroupBox.Location = new System.Drawing.Point(3, 3);
            this.PlotGroupBox.Name = "PlotGroupBox";
            this.PlotGroupBox.Size = new System.Drawing.Size(382, 315);
            this.PlotGroupBox.TabIndex = 0;
            this.PlotGroupBox.TabStop = false;
            this.PlotGroupBox.Text = "Plots";
            // 
            // PlotsListView
            // 
            this.PlotsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.PlotNameHeader});
            this.PlotsListView.ContextMenuStrip = this.PlotContextMenu;
            this.PlotsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PlotsListView.FullRowSelect = true;
            this.PlotsListView.HideSelection = false;
            this.PlotsListView.Location = new System.Drawing.Point(3, 19);
            this.PlotsListView.Name = "PlotsListView";
            this.PlotsListView.Size = new System.Drawing.Size(376, 293);
            this.PlotsListView.TabIndex = 0;
            this.PlotsListView.UseCompatibleStateImageBehavior = false;
            this.PlotsListView.View = System.Windows.Forms.View.Details;
            // 
            // PlotNameHeader
            // 
            this.PlotNameHeader.Text = "Plots";
            this.PlotNameHeader.Width = 300;
            // 
            // PlotContextMenu
            // 
            this.PlotContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewPlotToolStripMenuItem,
            this.EditPlotToolStripMenuItem});
            this.PlotContextMenu.Name = "DistrictsContextMenu";
            this.PlotContextMenu.Size = new System.Drawing.Size(123, 48);
            // 
            // NewPlotToolStripMenuItem
            // 
            this.NewPlotToolStripMenuItem.Name = "NewPlotToolStripMenuItem";
            this.NewPlotToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.NewPlotToolStripMenuItem.Text = "New Plot";
            // 
            // EditPlotToolStripMenuItem
            // 
            this.EditPlotToolStripMenuItem.Name = "EditPlotToolStripMenuItem";
            this.EditPlotToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.EditPlotToolStripMenuItem.Text = "Edit Plot";
            // 
            // GraphicalDetailsGroupBox
            // 
            this.CountryDetailTableLayoutPanel.SetColumnSpan(this.GraphicalDetailsGroupBox, 2);
            this.GraphicalDetailsGroupBox.Controls.Add(this.DetailTableLayoutPanel);
            this.GraphicalDetailsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GraphicalDetailsGroupBox.Location = new System.Drawing.Point(3, 324);
            this.GraphicalDetailsGroupBox.Name = "GraphicalDetailsGroupBox";
            this.GraphicalDetailsGroupBox.Size = new System.Drawing.Size(382, 190);
            this.GraphicalDetailsGroupBox.TabIndex = 2;
            this.GraphicalDetailsGroupBox.TabStop = false;
            this.GraphicalDetailsGroupBox.Text = "Details";
            // 
            // CancelButton
            // 
            this.CanButton.Location = new System.Drawing.Point(906, 548);
            this.CanButton.Name = "CancelButton";
            this.CanButton.Size = new System.Drawing.Size(75, 23);
            this.CanButton.TabIndex = 2;
            this.CanButton.Text = "Cancel";
            this.CanButton.UseVisualStyleBackColor = true;
            this.CanButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OKButton.Location = new System.Drawing.Point(825, 548);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 3;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // DistrictPlotter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 574);
            this.Controls.Add(this.MainTableLayoutPanel);
            this.Name = "DistrictPlotter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "District Plotter";
            this.DetailTableLayoutPanel.ResumeLayout(false);
            this.DetailTableLayoutPanel.PerformLayout();
            this.MainTableLayoutPanel.ResumeLayout(false);
            this.PreviewGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PreviewPictureBox)).EndInit();
            this.DistrictDetailsGroupbox.ResumeLayout(false);
            this.CountryDetailTableLayoutPanel.ResumeLayout(false);
            this.PlotGroupBox.ResumeLayout(false);
            this.PlotContextMenu.ResumeLayout(false);
            this.GraphicalDetailsGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Label BankAccountLabel;
        private System.Windows.Forms.TextBox NameBox;
        private System.Windows.Forms.TextBox BankAccountBox;
        private System.Windows.Forms.Label PPSMLabel;
        private System.Windows.Forms.TextBox PPSMBox;
        private System.Windows.Forms.TableLayoutPanel DetailTableLayoutPanel;
        private System.Windows.Forms.Label PointsLabel;
        private System.Windows.Forms.TextBox PointsBox;
        private System.Windows.Forms.TableLayoutPanel MainTableLayoutPanel;
        private System.Windows.Forms.GroupBox PreviewGroupBox;
        private System.Windows.Forms.PictureBox PreviewPictureBox;
        private System.Windows.Forms.GroupBox DistrictDetailsGroupbox;
        private System.Windows.Forms.TableLayoutPanel CountryDetailTableLayoutPanel;
        private System.Windows.Forms.GroupBox PlotGroupBox;
        private System.Windows.Forms.ListView PlotsListView;
        private System.Windows.Forms.ColumnHeader PlotNameHeader;
        private System.Windows.Forms.ContextMenuStrip PlotContextMenu;
        private System.Windows.Forms.ToolStripMenuItem NewPlotToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditPlotToolStripMenuItem;
        private System.Windows.Forms.GroupBox GraphicalDetailsGroupBox;
        private System.Windows.Forms.Button CanButton;
        private System.Windows.Forms.Button OKButton;
    }
}