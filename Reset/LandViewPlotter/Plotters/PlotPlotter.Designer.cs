
namespace Igtampe.LandViewPlotter {
    partial class PlotPlotter {
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
            this.OKButton = new System.Windows.Forms.Button();
            this.CountryDetailsGroupbox = new System.Windows.Forms.GroupBox();
            this.CountryDetailTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.GraphicalDetailsGroupBox = new System.Windows.Forms.GroupBox();
            this.DetailTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.NameLabel = new System.Windows.Forms.Label();
            this.OwnerLabel = new System.Windows.Forms.Label();
            this.NameBox = new System.Windows.Forms.TextBox();
            this.OwnerBox = new System.Windows.Forms.TextBox();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.PointsLabel = new System.Windows.Forms.Label();
            this.PointsBox = new System.Windows.Forms.TextBox();
            this.StatusComboBox = new System.Windows.Forms.ComboBox();
            this.PreviewPictureBox = new System.Windows.Forms.PictureBox();
            this.PreviewGroupBox = new System.Windows.Forms.GroupBox();
            this.MainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.CanButton = new System.Windows.Forms.Button();
            this.CountryDetailsGroupbox.SuspendLayout();
            this.CountryDetailTableLayoutPanel.SuspendLayout();
            this.GraphicalDetailsGroupBox.SuspendLayout();
            this.DetailTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PreviewPictureBox)).BeginInit();
            this.PreviewGroupBox.SuspendLayout();
            this.MainTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OKButton.Location = new System.Drawing.Point(641, 299);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 3;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CountryDetailsGroupbox
            // 
            this.CountryDetailsGroupbox.Controls.Add(this.CountryDetailTableLayoutPanel);
            this.CountryDetailsGroupbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CountryDetailsGroupbox.Location = new System.Drawing.Point(3, 3);
            this.CountryDetailsGroupbox.Name = "CountryDetailsGroupbox";
            this.CountryDetailsGroupbox.Size = new System.Drawing.Size(394, 290);
            this.CountryDetailsGroupbox.TabIndex = 1;
            this.CountryDetailsGroupbox.TabStop = false;
            this.CountryDetailsGroupbox.Text = "Plot Details";
            // 
            // CountryDetailTableLayoutPanel
            // 
            this.CountryDetailTableLayoutPanel.ColumnCount = 2;
            this.CountryDetailTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.CountryDetailTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.CountryDetailTableLayoutPanel.Controls.Add(this.GraphicalDetailsGroupBox, 0, 0);
            this.CountryDetailTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CountryDetailTableLayoutPanel.Location = new System.Drawing.Point(3, 19);
            this.CountryDetailTableLayoutPanel.Name = "CountryDetailTableLayoutPanel";
            this.CountryDetailTableLayoutPanel.RowCount = 2;
            this.CountryDetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.CountryDetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 196F));
            this.CountryDetailTableLayoutPanel.Size = new System.Drawing.Size(388, 268);
            this.CountryDetailTableLayoutPanel.TabIndex = 0;
            // 
            // GraphicalDetailsGroupBox
            // 
            this.CountryDetailTableLayoutPanel.SetColumnSpan(this.GraphicalDetailsGroupBox, 2);
            this.GraphicalDetailsGroupBox.Controls.Add(this.DetailTableLayoutPanel);
            this.GraphicalDetailsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GraphicalDetailsGroupBox.Location = new System.Drawing.Point(3, 3);
            this.GraphicalDetailsGroupBox.Name = "GraphicalDetailsGroupBox";
            this.CountryDetailTableLayoutPanel.SetRowSpan(this.GraphicalDetailsGroupBox, 2);
            this.GraphicalDetailsGroupBox.Size = new System.Drawing.Size(382, 262);
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
            this.DetailTableLayoutPanel.Controls.Add(this.OwnerLabel, 0, 3);
            this.DetailTableLayoutPanel.Controls.Add(this.NameBox, 1, 0);
            this.DetailTableLayoutPanel.Controls.Add(this.OwnerBox, 1, 3);
            this.DetailTableLayoutPanel.Controls.Add(this.StatusLabel, 0, 5);
            this.DetailTableLayoutPanel.Controls.Add(this.PointsLabel, 0, 6);
            this.DetailTableLayoutPanel.Controls.Add(this.PointsBox, 1, 6);
            this.DetailTableLayoutPanel.Controls.Add(this.StatusComboBox, 1, 5);
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
            this.DetailTableLayoutPanel.Size = new System.Drawing.Size(376, 240);
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
            // OwnerLabel
            // 
            this.OwnerLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.OwnerLabel.Location = new System.Drawing.Point(3, 29);
            this.OwnerLabel.Name = "OwnerLabel";
            this.OwnerLabel.Size = new System.Drawing.Size(100, 23);
            this.OwnerLabel.TabIndex = 3;
            this.OwnerLabel.Text = "Owner";
            this.OwnerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // NameBox
            // 
            this.NameBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.NameBox.Location = new System.Drawing.Point(109, 3);
            this.NameBox.Name = "NameBox";
            this.NameBox.Size = new System.Drawing.Size(264, 23);
            this.NameBox.TabIndex = 4;
            // 
            // OwnerBox
            // 
            this.OwnerBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.OwnerBox.Location = new System.Drawing.Point(109, 32);
            this.OwnerBox.Name = "OwnerBox";
            this.OwnerBox.Size = new System.Drawing.Size(264, 23);
            this.OwnerBox.TabIndex = 7;
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StatusLabel.Location = new System.Drawing.Point(3, 58);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(100, 29);
            this.StatusLabel.TabIndex = 8;
            this.StatusLabel.Text = "Status";
            this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PointsLabel
            // 
            this.PointsLabel.AutoSize = true;
            this.PointsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PointsLabel.Location = new System.Drawing.Point(3, 87);
            this.PointsLabel.Name = "PointsLabel";
            this.PointsLabel.Size = new System.Drawing.Size(100, 153);
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
            this.PointsBox.Size = new System.Drawing.Size(264, 147);
            this.PointsBox.TabIndex = 11;
            // 
            // StatusComboBox
            // 
            this.StatusComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StatusComboBox.FormattingEnabled = true;
            this.StatusComboBox.Items.AddRange(new object[] {
            "FOR SALE",
            "NOT FOR SALE",
            "BUILT"});
            this.StatusComboBox.Location = new System.Drawing.Point(109, 61);
            this.StatusComboBox.Name = "StatusComboBox";
            this.StatusComboBox.Size = new System.Drawing.Size(264, 23);
            this.StatusComboBox.TabIndex = 12;
            // 
            // PreviewPictureBox
            // 
            this.PreviewPictureBox.BackgroundImage = global::Igtampe.LandViewPlotter.Properties.Resources.TinyDiagBWTile;
            this.PreviewPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PreviewPictureBox.Location = new System.Drawing.Point(3, 19);
            this.PreviewPictureBox.Name = "PreviewPictureBox";
            this.PreviewPictureBox.Size = new System.Drawing.Size(388, 268);
            this.PreviewPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PreviewPictureBox.TabIndex = 0;
            this.PreviewPictureBox.TabStop = false;
            this.PreviewPictureBox.Click += new System.EventHandler(this.PreviewPictureBox_Click);
            // 
            // PreviewGroupBox
            // 
            this.MainTableLayoutPanel.SetColumnSpan(this.PreviewGroupBox, 2);
            this.PreviewGroupBox.Controls.Add(this.PreviewPictureBox);
            this.PreviewGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PreviewGroupBox.Location = new System.Drawing.Point(403, 3);
            this.PreviewGroupBox.Name = "PreviewGroupBox";
            this.PreviewGroupBox.Size = new System.Drawing.Size(394, 290);
            this.PreviewGroupBox.TabIndex = 0;
            this.PreviewGroupBox.TabStop = false;
            this.PreviewGroupBox.Text = "Preview";
            // 
            // MainTableLayoutPanel
            // 
            this.MainTableLayoutPanel.ColumnCount = 3;
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.MainTableLayoutPanel.Controls.Add(this.PreviewGroupBox, 1, 0);
            this.MainTableLayoutPanel.Controls.Add(this.CountryDetailsGroupbox, 0, 0);
            this.MainTableLayoutPanel.Controls.Add(this.CanButton, 2, 1);
            this.MainTableLayoutPanel.Controls.Add(this.OKButton, 1, 1);
            this.MainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.MainTableLayoutPanel.Name = "MainTableLayoutPanel";
            this.MainTableLayoutPanel.RowCount = 2;
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainTableLayoutPanel.Size = new System.Drawing.Size(800, 325);
            this.MainTableLayoutPanel.TabIndex = 4;
            // 
            // CanButton
            // 
            this.CanButton.Location = new System.Drawing.Point(722, 299);
            this.CanButton.Name = "CanButton";
            this.CanButton.Size = new System.Drawing.Size(75, 23);
            this.CanButton.TabIndex = 2;
            this.CanButton.Text = "Cancel";
            this.CanButton.UseVisualStyleBackColor = true;
            this.CanButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // PlotPlotter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 325);
            this.Controls.Add(this.MainTableLayoutPanel);
            this.Name = "PlotPlotter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Plot Plotter";
            this.CountryDetailsGroupbox.ResumeLayout(false);
            this.CountryDetailTableLayoutPanel.ResumeLayout(false);
            this.GraphicalDetailsGroupBox.ResumeLayout(false);
            this.DetailTableLayoutPanel.ResumeLayout(false);
            this.DetailTableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PreviewPictureBox)).EndInit();
            this.PreviewGroupBox.ResumeLayout(false);
            this.MainTableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.GroupBox CountryDetailsGroupbox;
        private System.Windows.Forms.PictureBox PreviewPictureBox;
        private System.Windows.Forms.GroupBox PreviewGroupBox;
        private System.Windows.Forms.TableLayoutPanel MainTableLayoutPanel;
        private System.Windows.Forms.Button CanButton;
        private System.Windows.Forms.TableLayoutPanel CountryDetailTableLayoutPanel;
        private System.Windows.Forms.GroupBox GraphicalDetailsGroupBox;
        private System.Windows.Forms.TableLayoutPanel DetailTableLayoutPanel;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Label OwnerLabel;
        private System.Windows.Forms.TextBox NameBox;
        private System.Windows.Forms.TextBox OwnerBox;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.Label PointsLabel;
        private System.Windows.Forms.TextBox PointsBox;
        private System.Windows.Forms.ComboBox StatusComboBox;
    }
}