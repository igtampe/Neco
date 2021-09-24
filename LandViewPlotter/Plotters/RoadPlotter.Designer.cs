
namespace Igtampe.LandViewPlotter {
    partial class RoadPlotter {
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
            this.DetailTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.RoadDetailsGroupBox = new System.Windows.Forms.GroupBox();
            this.MainTableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.GraphicalDetailsGroupBox = new System.Windows.Forms.GroupBox();
            this.GraphicalDetailsTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.NameLabel = new System.Windows.Forms.Label();
            this.WidthLabel = new System.Windows.Forms.Label();
            this.NameBox = new System.Windows.Forms.TextBox();
            this.WidthBox = new System.Windows.Forms.TextBox();
            this.PointsLabel = new System.Windows.Forms.Label();
            this.PointsBox = new System.Windows.Forms.TextBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.CanButton = new System.Windows.Forms.Button();
            this.PreviewPictureBox = new System.Windows.Forms.PictureBox();
            this.RoadDetailsGroupBox.SuspendLayout();
            this.MainTableLayoutPanel1.SuspendLayout();
            this.GraphicalDetailsGroupBox.SuspendLayout();
            this.GraphicalDetailsTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PreviewPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // DetailTableLayoutPanel
            // 
            this.DetailTableLayoutPanel.ColumnCount = 2;
            this.DetailTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.DetailTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
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
            this.DetailTableLayoutPanel.Size = new System.Drawing.Size(188, 240);
            this.DetailTableLayoutPanel.TabIndex = 0;
            // 
            // RoadDetailsGroupBox
            // 
            this.RoadDetailsGroupBox.Controls.Add(this.MainTableLayoutPanel1);
            this.RoadDetailsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RoadDetailsGroupBox.Location = new System.Drawing.Point(0, 0);
            this.RoadDetailsGroupBox.Name = "RoadDetailsGroupBox";
            this.RoadDetailsGroupBox.Size = new System.Drawing.Size(902, 329);
            this.RoadDetailsGroupBox.TabIndex = 2;
            this.RoadDetailsGroupBox.TabStop = false;
            this.RoadDetailsGroupBox.Text = "Road Details";
            // 
            // MainTableLayoutPanel1
            // 
            this.MainTableLayoutPanel1.ColumnCount = 3;
            this.MainTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 450F));
            this.MainTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.MainTableLayoutPanel1.Controls.Add(this.GraphicalDetailsGroupBox, 0, 0);
            this.MainTableLayoutPanel1.Controls.Add(this.CanButton, 2, 2);
            this.MainTableLayoutPanel1.Controls.Add(this.OKButton, 1, 2);
            this.MainTableLayoutPanel1.Controls.Add(this.PreviewPictureBox, 1, 0);
            this.MainTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTableLayoutPanel1.Location = new System.Drawing.Point(3, 19);
            this.MainTableLayoutPanel1.Name = "MainTableLayoutPanel1";
            this.MainTableLayoutPanel1.RowCount = 3;
            this.MainTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 196F));
            this.MainTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainTableLayoutPanel1.Size = new System.Drawing.Size(896, 307);
            this.MainTableLayoutPanel1.TabIndex = 0;
            // 
            // GraphicalDetailsGroupBox
            // 
            this.GraphicalDetailsGroupBox.Controls.Add(this.GraphicalDetailsTableLayoutPanel);
            this.GraphicalDetailsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GraphicalDetailsGroupBox.Location = new System.Drawing.Point(3, 3);
            this.GraphicalDetailsGroupBox.Name = "GraphicalDetailsGroupBox";
            this.MainTableLayoutPanel1.SetRowSpan(this.GraphicalDetailsGroupBox, 2);
            this.GraphicalDetailsGroupBox.Size = new System.Drawing.Size(444, 272);
            this.GraphicalDetailsGroupBox.TabIndex = 2;
            this.GraphicalDetailsGroupBox.TabStop = false;
            this.GraphicalDetailsGroupBox.Text = "Details";
            // 
            // GraphicalDetailsTableLayoutPanel
            // 
            this.GraphicalDetailsTableLayoutPanel.ColumnCount = 2;
            this.GraphicalDetailsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.GraphicalDetailsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.GraphicalDetailsTableLayoutPanel.Controls.Add(this.NameLabel, 0, 0);
            this.GraphicalDetailsTableLayoutPanel.Controls.Add(this.WidthLabel, 0, 3);
            this.GraphicalDetailsTableLayoutPanel.Controls.Add(this.NameBox, 1, 0);
            this.GraphicalDetailsTableLayoutPanel.Controls.Add(this.WidthBox, 1, 3);
            this.GraphicalDetailsTableLayoutPanel.Controls.Add(this.PointsLabel, 0, 6);
            this.GraphicalDetailsTableLayoutPanel.Controls.Add(this.PointsBox, 1, 6);
            this.GraphicalDetailsTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GraphicalDetailsTableLayoutPanel.Location = new System.Drawing.Point(3, 19);
            this.GraphicalDetailsTableLayoutPanel.Name = "GraphicalDetailsTableLayoutPanel";
            this.GraphicalDetailsTableLayoutPanel.RowCount = 7;
            this.GraphicalDetailsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.GraphicalDetailsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.GraphicalDetailsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.GraphicalDetailsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.GraphicalDetailsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.GraphicalDetailsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.GraphicalDetailsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.GraphicalDetailsTableLayoutPanel.Size = new System.Drawing.Size(438, 250);
            this.GraphicalDetailsTableLayoutPanel.TabIndex = 0;
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
            this.WidthLabel.TabIndex = 3;
            this.WidthLabel.Text = "Width";
            this.WidthLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // NameBox
            // 
            this.NameBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.NameBox.Location = new System.Drawing.Point(109, 3);
            this.NameBox.Name = "NameBox";
            this.NameBox.Size = new System.Drawing.Size(326, 23);
            this.NameBox.TabIndex = 4;
            // 
            // WidthBox
            // 
            this.WidthBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.WidthBox.Location = new System.Drawing.Point(109, 32);
            this.WidthBox.Name = "WidthBox";
            this.WidthBox.Size = new System.Drawing.Size(326, 23);
            this.WidthBox.TabIndex = 7;
            // 
            // PointsLabel
            // 
            this.PointsLabel.AutoSize = true;
            this.PointsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PointsLabel.Location = new System.Drawing.Point(3, 58);
            this.PointsLabel.Name = "PointsLabel";
            this.PointsLabel.Size = new System.Drawing.Size(100, 192);
            this.PointsLabel.TabIndex = 10;
            this.PointsLabel.Text = "Points";
            this.PointsLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // PointsBox
            // 
            this.PointsBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PointsBox.Location = new System.Drawing.Point(109, 61);
            this.PointsBox.Multiline = true;
            this.PointsBox.Name = "PointsBox";
            this.PointsBox.Size = new System.Drawing.Size(326, 186);
            this.PointsBox.TabIndex = 11;
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OKButton.Location = new System.Drawing.Point(737, 281);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 3;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CanButton
            // 
            this.CanButton.Location = new System.Drawing.Point(818, 281);
            this.CanButton.Name = "CanButton";
            this.CanButton.Size = new System.Drawing.Size(75, 23);
            this.CanButton.TabIndex = 4;
            this.CanButton.Text = "Cancel";
            this.CanButton.UseVisualStyleBackColor = true;
            this.CanButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // PreviewPictureBox
            // 
            this.MainTableLayoutPanel1.SetColumnSpan(this.PreviewPictureBox, 2);
            this.PreviewPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PreviewPictureBox.Location = new System.Drawing.Point(453, 3);
            this.PreviewPictureBox.Name = "PreviewPictureBox";
            this.MainTableLayoutPanel1.SetRowSpan(this.PreviewPictureBox, 2);
            this.PreviewPictureBox.Size = new System.Drawing.Size(440, 272);
            this.PreviewPictureBox.TabIndex = 5;
            this.PreviewPictureBox.TabStop = false;
            this.PreviewPictureBox.Click += new System.EventHandler(this.PreviewPictureBox_Click);
            // 
            // RoadPlotter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 329);
            this.Controls.Add(this.RoadDetailsGroupBox);
            this.Name = "RoadPlotter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Road Plotter";
            this.RoadDetailsGroupBox.ResumeLayout(false);
            this.MainTableLayoutPanel1.ResumeLayout(false);
            this.GraphicalDetailsGroupBox.ResumeLayout(false);
            this.GraphicalDetailsTableLayoutPanel.ResumeLayout(false);
            this.GraphicalDetailsTableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PreviewPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel DetailTableLayoutPanel;
        private System.Windows.Forms.GroupBox RoadDetailsGroupBox;
        private System.Windows.Forms.TableLayoutPanel MainTableLayoutPanel1;
        private System.Windows.Forms.GroupBox GraphicalDetailsGroupBox;
        private System.Windows.Forms.TableLayoutPanel GraphicalDetailsTableLayoutPanel;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Label WidthLabel;
        private System.Windows.Forms.TextBox NameBox;
        private System.Windows.Forms.TextBox WidthBox;
        private System.Windows.Forms.Label PointsLabel;
        private System.Windows.Forms.TextBox PointsBox;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button CanButton;
        private System.Windows.Forms.PictureBox PreviewPictureBox;
    }
}