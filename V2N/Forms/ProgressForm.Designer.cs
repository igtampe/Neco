namespace Igtampe.Neco.V2N.Forms {
    partial class ProgressForm {
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
            this.MainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.NecoPictureBox = new System.Windows.Forms.PictureBox();
            this.SitTightLabel = new System.Windows.Forms.Label();
            this.NecosDispatchedLabel = new System.Windows.Forms.Label();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.MainProgressBar = new System.Windows.Forms.ProgressBar();
            this.DetailsBTN = new System.Windows.Forms.Button();
            this.DetailsGroupBox = new System.Windows.Forms.GroupBox();
            this.DetailsBox = new System.Windows.Forms.TextBox();
            this.MainTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NecoPictureBox)).BeginInit();
            this.DetailsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainTableLayoutPanel
            // 
            this.MainTableLayoutPanel.ColumnCount = 2;
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTableLayoutPanel.Controls.Add(this.NecoPictureBox, 0, 0);
            this.MainTableLayoutPanel.Controls.Add(this.SitTightLabel, 1, 0);
            this.MainTableLayoutPanel.Controls.Add(this.NecosDispatchedLabel, 1, 1);
            this.MainTableLayoutPanel.Controls.Add(this.StatusLabel, 0, 2);
            this.MainTableLayoutPanel.Controls.Add(this.MainProgressBar, 0, 3);
            this.MainTableLayoutPanel.Controls.Add(this.DetailsBTN, 1, 4);
            this.MainTableLayoutPanel.Controls.Add(this.DetailsGroupBox, 0, 5);
            this.MainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.MainTableLayoutPanel.Name = "MainTableLayoutPanel";
            this.MainTableLayoutPanel.Padding = new System.Windows.Forms.Padding(10);
            this.MainTableLayoutPanel.RowCount = 6;
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTableLayoutPanel.Size = new System.Drawing.Size(339, 166);
            this.MainTableLayoutPanel.TabIndex = 0;
            // 
            // NecoPictureBox
            // 
            this.NecoPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NecoPictureBox.Image = global::Igtampe.Neco.V2N.Properties.Resources.Load;
            this.NecoPictureBox.Location = new System.Drawing.Point(13, 13);
            this.NecoPictureBox.Name = "NecoPictureBox";
            this.MainTableLayoutPanel.SetRowSpan(this.NecoPictureBox, 2);
            this.NecoPictureBox.Size = new System.Drawing.Size(69, 54);
            this.NecoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.NecoPictureBox.TabIndex = 0;
            this.NecoPictureBox.TabStop = false;
            // 
            // SitTightLabel
            // 
            this.SitTightLabel.AutoSize = true;
            this.SitTightLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SitTightLabel.Location = new System.Drawing.Point(88, 10);
            this.SitTightLabel.Name = "SitTightLabel";
            this.SitTightLabel.Size = new System.Drawing.Size(100, 30);
            this.SitTightLabel.TabIndex = 1;
            this.SitTightLabel.Text = "Sit tight!";
            // 
            // NecosDispatchedLabel
            // 
            this.NecosDispatchedLabel.AutoSize = true;
            this.NecosDispatchedLabel.Location = new System.Drawing.Point(88, 40);
            this.NecosDispatchedLabel.Name = "NecosDispatchedLabel";
            this.NecosDispatchedLabel.Size = new System.Drawing.Size(222, 30);
            this.NecosDispatchedLabel.TabIndex = 2;
            this.NecosDispatchedLabel.Text = "The Necos are being dispatched to fulfill your request. They won\'t be long!";
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.MainTableLayoutPanel.SetColumnSpan(this.StatusLabel, 2);
            this.StatusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StatusLabel.Location = new System.Drawing.Point(13, 80);
            this.StatusLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(313, 15);
            this.StatusLabel.TabIndex = 3;
            this.StatusLabel.Text = "Meowing...";
            // 
            // MainProgressBar
            // 
            this.MainTableLayoutPanel.SetColumnSpan(this.MainProgressBar, 2);
            this.MainProgressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainProgressBar.Location = new System.Drawing.Point(13, 98);
            this.MainProgressBar.Name = "MainProgressBar";
            this.MainProgressBar.Size = new System.Drawing.Size(313, 23);
            this.MainProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.MainProgressBar.TabIndex = 4;
            // 
            // DetailsBTN
            // 
            this.DetailsBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DetailsBTN.Location = new System.Drawing.Point(251, 134);
            this.DetailsBTN.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.DetailsBTN.Name = "DetailsBTN";
            this.DetailsBTN.Size = new System.Drawing.Size(75, 23);
            this.DetailsBTN.TabIndex = 5;
            this.DetailsBTN.Text = "Details";
            this.DetailsBTN.UseVisualStyleBackColor = true;
            this.DetailsBTN.Click += new System.EventHandler(this.DetailsBTN_Click);
            // 
            // DetailsGroupBox
            // 
            this.MainTableLayoutPanel.SetColumnSpan(this.DetailsGroupBox, 2);
            this.DetailsGroupBox.Controls.Add(this.DetailsBox);
            this.DetailsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DetailsGroupBox.Location = new System.Drawing.Point(13, 163);
            this.DetailsGroupBox.Name = "DetailsGroupBox";
            this.DetailsGroupBox.Size = new System.Drawing.Size(313, 1);
            this.DetailsGroupBox.TabIndex = 6;
            this.DetailsGroupBox.TabStop = false;
            this.DetailsGroupBox.Text = "Details";
            // 
            // DetailsBox
            // 
            this.DetailsBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DetailsBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DetailsBox.Location = new System.Drawing.Point(3, 19);
            this.DetailsBox.Margin = new System.Windows.Forms.Padding(10);
            this.DetailsBox.Multiline = true;
            this.DetailsBox.Name = "DetailsBox";
            this.DetailsBox.ReadOnly = true;
            this.DetailsBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DetailsBox.Size = new System.Drawing.Size(307, 0);
            this.DetailsBox.TabIndex = 0;
            this.DetailsBox.Text = "Getting Started...";
            // 
            // ProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 166);
            this.Controls.Add(this.MainTableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximumSize = new System.Drawing.Size(750, 600);
            this.MinimumSize = new System.Drawing.Size(355, 205);
            this.Name = "ProgressForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "The Necos are Working";
            this.MainTableLayoutPanel.ResumeLayout(false);
            this.MainTableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NecoPictureBox)).EndInit();
            this.DetailsGroupBox.ResumeLayout(false);
            this.DetailsGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel MainTableLayoutPanel;
        private PictureBox NecoPictureBox;
        private Label SitTightLabel;
        private Label NecosDispatchedLabel;
        private Label StatusLabel;
        private ProgressBar MainProgressBar;
        private Button DetailsBTN;
        private GroupBox DetailsGroupBox;
        private TextBox DetailsBox;
    }
}