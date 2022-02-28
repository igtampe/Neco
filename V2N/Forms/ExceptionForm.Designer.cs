namespace Igtampe.Neco.V2N.Forms {
    partial class ExceptionForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExceptionForm));
            this.MainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.ErrorPictureBox = new System.Windows.Forms.PictureBox();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.ExceptionLabel = new System.Windows.Forms.Label();
            this.ExceptionTextBox = new System.Windows.Forms.TextBox();
            this.OKBTN = new System.Windows.Forms.Button();
            this.MainTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // MainTableLayoutPanel
            // 
            this.MainTableLayoutPanel.ColumnCount = 2;
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTableLayoutPanel.Controls.Add(this.ErrorPictureBox, 0, 0);
            this.MainTableLayoutPanel.Controls.Add(this.TitleLabel, 1, 0);
            this.MainTableLayoutPanel.Controls.Add(this.ExceptionLabel, 1, 1);
            this.MainTableLayoutPanel.Controls.Add(this.ExceptionTextBox, 0, 2);
            this.MainTableLayoutPanel.Controls.Add(this.OKBTN, 1, 3);
            this.MainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.MainTableLayoutPanel.Name = "MainTableLayoutPanel";
            this.MainTableLayoutPanel.Padding = new System.Windows.Forms.Padding(20);
            this.MainTableLayoutPanel.RowCount = 4;
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainTableLayoutPanel.Size = new System.Drawing.Size(708, 397);
            this.MainTableLayoutPanel.TabIndex = 0;
            // 
            // ErrorPictureBox
            // 
            this.ErrorPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ErrorPictureBox.Image = global::Igtampe.Neco.V2N.Properties.Resources.X;
            this.ErrorPictureBox.Location = new System.Drawing.Point(23, 23);
            this.ErrorPictureBox.Name = "ErrorPictureBox";
            this.MainTableLayoutPanel.SetRowSpan(this.ErrorPictureBox, 2);
            this.ErrorPictureBox.Size = new System.Drawing.Size(64, 71);
            this.ErrorPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ErrorPictureBox.TabIndex = 0;
            this.ErrorPictureBox.TabStop = false;
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.TitleLabel.ForeColor = System.Drawing.Color.White;
            this.TitleLabel.Location = new System.Drawing.Point(93, 20);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(198, 32);
            this.TitleLabel.TabIndex = 1;
            this.TitleLabel.Text = "Crisis Averted";
            // 
            // ExceptionLabel
            // 
            this.ExceptionLabel.AutoSize = true;
            this.ExceptionLabel.ForeColor = System.Drawing.Color.White;
            this.ExceptionLabel.Location = new System.Drawing.Point(93, 52);
            this.ExceptionLabel.Name = "ExceptionLabel";
            this.ExceptionLabel.Size = new System.Drawing.Size(587, 45);
            this.ExceptionLabel.TabIndex = 2;
            this.ExceptionLabel.Text = resources.GetString("ExceptionLabel.Text");
            // 
            // ExceptionTextBox
            // 
            this.ExceptionTextBox.BackColor = System.Drawing.Color.Black;
            this.ExceptionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MainTableLayoutPanel.SetColumnSpan(this.ExceptionTextBox, 2);
            this.ExceptionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ExceptionTextBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ExceptionTextBox.ForeColor = System.Drawing.Color.White;
            this.ExceptionTextBox.Location = new System.Drawing.Point(23, 107);
            this.ExceptionTextBox.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.ExceptionTextBox.Multiline = true;
            this.ExceptionTextBox.Name = "ExceptionTextBox";
            this.ExceptionTextBox.PlaceholderText = "Hey wait a minute";
            this.ExceptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ExceptionTextBox.Size = new System.Drawing.Size(662, 231);
            this.ExceptionTextBox.TabIndex = 3;
            // 
            // OKBTN
            // 
            this.OKBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OKBTN.BackColor = System.Drawing.Color.White;
            this.OKBTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OKBTN.Location = new System.Drawing.Point(610, 351);
            this.OKBTN.Name = "OKBTN";
            this.OKBTN.Size = new System.Drawing.Size(75, 23);
            this.OKBTN.TabIndex = 4;
            this.OKBTN.Text = "OK";
            this.OKBTN.UseVisualStyleBackColor = false;
            this.OKBTN.Click += new System.EventHandler(this.OKBTN_Click);
            // 
            // ExceptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(708, 397);
            this.Controls.Add(this.MainTableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ExceptionForm";
            this.Opacity = 0.7D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "An Exception Happened, but we handled it";
            this.MainTableLayoutPanel.ResumeLayout(false);
            this.MainTableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel MainTableLayoutPanel;
        private PictureBox ErrorPictureBox;
        private Label TitleLabel;
        private Label ExceptionLabel;
        private TextBox ExceptionTextBox;
        private Button OKBTN;
    }
}