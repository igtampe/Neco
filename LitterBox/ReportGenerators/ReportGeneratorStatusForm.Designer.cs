namespace Igtampe.LitterBox.ReportGenerators {
    partial class ReportGeneratorStatusForm {
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.LoadingPictureBox = new System.Windows.Forms.PictureBox();
            this.MainLabel = new System.Windows.Forms.Label();
            this.Submainlabel = new System.Windows.Forms.Label();
            this.MainProgressBar = new System.Windows.Forms.ProgressBar();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.CancelBTN = new System.Windows.Forms.Button();
            this.DetailsGroupBox = new System.Windows.Forms.GroupBox();
            this.Logbox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LoadingPictureBox)).BeginInit();
            this.DetailsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.LoadingPictureBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.MainLabel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.Submainlabel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.MainProgressBar, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.StatusLabel, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.CancelBTN, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.DetailsGroupBox, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(547, 479);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // LoadingPictureBox
            // 
            this.LoadingPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LoadingPictureBox.Image = global::Igtampe.LitterBox.Properties.Resources.Load;
            this.LoadingPictureBox.Location = new System.Drawing.Point(20, 20);
            this.LoadingPictureBox.Margin = new System.Windows.Forms.Padding(20);
            this.LoadingPictureBox.Name = "LoadingPictureBox";
            this.tableLayoutPanel1.SetRowSpan(this.LoadingPictureBox, 4);
            this.LoadingPictureBox.Size = new System.Drawing.Size(160, 115);
            this.LoadingPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.LoadingPictureBox.TabIndex = 0;
            this.LoadingPictureBox.TabStop = false;
            // 
            // MainLabel
            // 
            this.MainLabel.AutoSize = true;
            this.MainLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainLabel.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.MainLabel.Location = new System.Drawing.Point(203, 0);
            this.MainLabel.Name = "MainLabel";
            this.MainLabel.Size = new System.Drawing.Size(341, 75);
            this.MainLabel.TabIndex = 1;
            this.MainLabel.Text = "Please Wait!";
            this.MainLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // Submainlabel
            // 
            this.Submainlabel.AutoSize = true;
            this.Submainlabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Submainlabel.Location = new System.Drawing.Point(203, 75);
            this.Submainlabel.Name = "Submainlabel";
            this.Submainlabel.Size = new System.Drawing.Size(341, 20);
            this.Submainlabel.TabIndex = 2;
            this.Submainlabel.Text = "Several Necos are working tirelessly to bring you that report\r\n";
            // 
            // MainProgressBar
            // 
            this.MainProgressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainProgressBar.Location = new System.Drawing.Point(203, 105);
            this.MainProgressBar.Margin = new System.Windows.Forms.Padding(3, 10, 20, 10);
            this.MainProgressBar.Name = "MainProgressBar";
            this.MainProgressBar.Size = new System.Drawing.Size(324, 20);
            this.MainProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.MainProgressBar.TabIndex = 3;
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Location = new System.Drawing.Point(203, 135);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(79, 15);
            this.StatusLabel.TabIndex = 4;
            this.StatusLabel.Text = "Please Wait....";
            this.StatusLabel.TextChanged += new System.EventHandler(this.StatusLabel_TextChanged);
            // 
            // CancelBTN
            // 
            this.CancelBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelBTN.Location = new System.Drawing.Point(452, 446);
            this.CancelBTN.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.CancelBTN.Name = "CancelBTN";
            this.CancelBTN.Size = new System.Drawing.Size(75, 23);
            this.CancelBTN.TabIndex = 5;
            this.CancelBTN.Text = "Cancel";
            this.CancelBTN.UseVisualStyleBackColor = true;
            this.CancelBTN.Click += new System.EventHandler(this.CancelBTN_Click);
            // 
            // DetailsGroupBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.DetailsGroupBox, 2);
            this.DetailsGroupBox.Controls.Add(this.Logbox);
            this.DetailsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DetailsGroupBox.Location = new System.Drawing.Point(20, 158);
            this.DetailsGroupBox.Margin = new System.Windows.Forms.Padding(20, 3, 20, 3);
            this.DetailsGroupBox.Name = "DetailsGroupBox";
            this.DetailsGroupBox.Size = new System.Drawing.Size(507, 275);
            this.DetailsGroupBox.TabIndex = 6;
            this.DetailsGroupBox.TabStop = false;
            this.DetailsGroupBox.Text = "Details";
            // 
            // Logbox
            // 
            this.Logbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Logbox.Location = new System.Drawing.Point(3, 19);
            this.Logbox.Multiline = true;
            this.Logbox.Name = "Logbox";
            this.Logbox.ReadOnly = true;
            this.Logbox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Logbox.Size = new System.Drawing.Size(501, 253);
            this.Logbox.TabIndex = 0;
            // 
            // ReportGeneratorStatusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 479);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ReportGeneratorStatusForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "A Report is on the way!";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LoadingPictureBox)).EndInit();
            this.DetailsGroupBox.ResumeLayout(false);
            this.DetailsGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox LoadingPictureBox;
        private System.Windows.Forms.Label MainLabel;
        private System.Windows.Forms.Label Submainlabel;
        private System.Windows.Forms.ProgressBar MainProgressBar;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.Button CancelBTN;
        private System.Windows.Forms.GroupBox DetailsGroupBox;
        private System.Windows.Forms.TextBox Logbox;
    }
}