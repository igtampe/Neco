
namespace Igtampe.LitterBox {
    partial class ConnectionForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionForm));
            this.MainTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.LogoPictureBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DBProviderGroupbox = new System.Windows.Forms.GroupBox();
            this.ProvidersComboBox = new System.Windows.Forms.ComboBox();
            this.ConnectionComboBox = new System.Windows.Forms.GroupBox();
            this.ConnectionStringBox = new System.Windows.Forms.TextBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.ConnectAndCheckBWorker = new System.ComponentModel.BackgroundWorker();
            this.MainTableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).BeginInit();
            this.DBProviderGroupbox.SuspendLayout();
            this.ConnectionComboBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainTableLayout
            // 
            this.MainTableLayout.ColumnCount = 1;
            this.MainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTableLayout.Controls.Add(this.LogoPictureBox, 0, 0);
            this.MainTableLayout.Controls.Add(this.label1, 0, 1);
            this.MainTableLayout.Controls.Add(this.DBProviderGroupbox, 0, 2);
            this.MainTableLayout.Controls.Add(this.ConnectionComboBox, 0, 3);
            this.MainTableLayout.Controls.Add(this.OKButton, 0, 4);
            this.MainTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTableLayout.Location = new System.Drawing.Point(0, 0);
            this.MainTableLayout.Name = "MainTableLayout";
            this.MainTableLayout.Padding = new System.Windows.Forms.Padding(10, 10, 10, 3);
            this.MainTableLayout.RowCount = 5;
            this.MainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.MainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 61F));
            this.MainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainTableLayout.Size = new System.Drawing.Size(368, 424);
            this.MainTableLayout.TabIndex = 0;
            // 
            // LogoPictureBox
            // 
            this.LogoPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogoPictureBox.Image = global::Igtampe.LitterBox.Properties.Resources.Logo;
            this.LogoPictureBox.Location = new System.Drawing.Point(13, 13);
            this.LogoPictureBox.Name = "LogoPictureBox";
            this.LogoPictureBox.Size = new System.Drawing.Size(342, 94);
            this.LogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.LogoPictureBox.TabIndex = 0;
            this.LogoPictureBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 125);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 15, 3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(340, 60);
            this.label1.TabIndex = 1;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // DBProviderGroupbox
            // 
            this.DBProviderGroupbox.Controls.Add(this.ProvidersComboBox);
            this.DBProviderGroupbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DBProviderGroupbox.Location = new System.Drawing.Point(13, 193);
            this.DBProviderGroupbox.Name = "DBProviderGroupbox";
            this.DBProviderGroupbox.Size = new System.Drawing.Size(342, 55);
            this.DBProviderGroupbox.TabIndex = 2;
            this.DBProviderGroupbox.TabStop = false;
            this.DBProviderGroupbox.Text = "DB Provider";
            // 
            // ProvidersComboBox
            // 
            this.ProvidersComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProvidersComboBox.FormattingEnabled = true;
            this.ProvidersComboBox.Items.AddRange(new object[] {
            "(Automatic)",
            "SQL Server",
            "Postgresql"});
            this.ProvidersComboBox.Location = new System.Drawing.Point(3, 19);
            this.ProvidersComboBox.Name = "ProvidersComboBox";
            this.ProvidersComboBox.Size = new System.Drawing.Size(336, 23);
            this.ProvidersComboBox.TabIndex = 0;
            this.ProvidersComboBox.SelectedIndexChanged += new System.EventHandler(this.ProvidersComboBox_SelectedIndexChanged);
            // 
            // ConnectionComboBox
            // 
            this.ConnectionComboBox.Controls.Add(this.ConnectionStringBox);
            this.ConnectionComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConnectionComboBox.Location = new System.Drawing.Point(13, 254);
            this.ConnectionComboBox.Name = "ConnectionComboBox";
            this.ConnectionComboBox.Size = new System.Drawing.Size(342, 135);
            this.ConnectionComboBox.TabIndex = 3;
            this.ConnectionComboBox.TabStop = false;
            this.ConnectionComboBox.Text = "Connection String";
            // 
            // ConnectionStringBox
            // 
            this.ConnectionStringBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConnectionStringBox.Location = new System.Drawing.Point(3, 19);
            this.ConnectionStringBox.Multiline = true;
            this.ConnectionStringBox.Name = "ConnectionStringBox";
            this.ConnectionStringBox.Size = new System.Drawing.Size(336, 113);
            this.ConnectionStringBox.TabIndex = 0;
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OKButton.Location = new System.Drawing.Point(280, 395);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 4;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // ConnectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 424);
            this.Controls.Add(this.MainTableLayout);
            this.Name = "ConnectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connection";
            this.MainTableLayout.ResumeLayout(false);
            this.MainTableLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).EndInit();
            this.DBProviderGroupbox.ResumeLayout(false);
            this.ConnectionComboBox.ResumeLayout(false);
            this.ConnectionComboBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel MainTableLayout;
        private System.Windows.Forms.PictureBox LogoPictureBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox DBProviderGroupbox;
        private System.Windows.Forms.ComboBox ProvidersComboBox;
        private System.Windows.Forms.GroupBox ConnectionComboBox;
        private System.Windows.Forms.TextBox ConnectionStringBox;
        private System.Windows.Forms.Button OKButton;
        private System.ComponentModel.BackgroundWorker ConnectAndCheckBWorker;
    }
}