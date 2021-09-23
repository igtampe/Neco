
namespace Igtampe.LandViewPlotter {
    partial class BackgroundWorkerForm {
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
            this.LoadIconPictureBox = new System.Windows.Forms.PictureBox();
            this.PleaseWaitLabel = new System.Windows.Forms.Label();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.MainTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LoadIconPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // MainTableLayoutPanel
            // 
            this.MainTableLayoutPanel.ColumnCount = 2;
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.32832F));
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.67168F));
            this.MainTableLayoutPanel.Controls.Add(this.LoadIconPictureBox, 0, 0);
            this.MainTableLayoutPanel.Controls.Add(this.PleaseWaitLabel, 1, 0);
            this.MainTableLayoutPanel.Controls.Add(this.StatusLabel, 1, 1);
            this.MainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.MainTableLayoutPanel.Name = "MainTableLayoutPanel";
            this.MainTableLayoutPanel.RowCount = 2;
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.MainTableLayoutPanel.Size = new System.Drawing.Size(306, 109);
            this.MainTableLayoutPanel.TabIndex = 0;
            // 
            // LoadIconPictureBox
            // 
            this.LoadIconPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LoadIconPictureBox.Image = global::Igtampe.LandViewPlotter.Properties.Resources.Load;
            this.LoadIconPictureBox.Location = new System.Drawing.Point(20, 20);
            this.LoadIconPictureBox.Margin = new System.Windows.Forms.Padding(20, 20, 5, 20);
            this.LoadIconPictureBox.Name = "LoadIconPictureBox";
            this.MainTableLayoutPanel.SetRowSpan(this.LoadIconPictureBox, 2);
            this.LoadIconPictureBox.Size = new System.Drawing.Size(70, 69);
            this.LoadIconPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.LoadIconPictureBox.TabIndex = 0;
            this.LoadIconPictureBox.TabStop = false;
            // 
            // PleaseWaitLabel
            // 
            this.PleaseWaitLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.PleaseWaitLabel.AutoSize = true;
            this.PleaseWaitLabel.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.PleaseWaitLabel.Location = new System.Drawing.Point(98, 26);
            this.PleaseWaitLabel.Name = "PleaseWaitLabel";
            this.PleaseWaitLabel.Size = new System.Drawing.Size(100, 28);
            this.PleaseWaitLabel.TabIndex = 1;
            this.PleaseWaitLabel.Text = "Sit Tight!";
            // 
            // label1
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Location = new System.Drawing.Point(98, 54);
            this.StatusLabel.Name = "label1";
            this.StatusLabel.Size = new System.Drawing.Size(204, 30);
            this.StatusLabel.TabIndex = 2;
            this.StatusLabel.Text = "A Neco has been dispatched and will process your request";
            // 
            // BackgroundWorkerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 109);
            this.Controls.Add(this.MainTableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "BackgroundWorkerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Please Wait";
            this.MainTableLayoutPanel.ResumeLayout(false);
            this.MainTableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LoadIconPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel MainTableLayoutPanel;
        private System.Windows.Forms.PictureBox LoadIconPictureBox;
        private System.Windows.Forms.Label PleaseWaitLabel;
        private System.Windows.Forms.Label StatusLabel;
    }
}