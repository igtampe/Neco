
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
            this.ImageBox = new System.Windows.Forms.PictureBox();
            this.BigTextLabel = new System.Windows.Forms.Label();
            this.StatusTextLabel = new System.Windows.Forms.Label();
            this.MainTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // MainTableLayoutPanel
            // 
            this.MainTableLayoutPanel.ColumnCount = 2;
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.32832F));
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.67168F));
            this.MainTableLayoutPanel.Controls.Add(this.ImageBox, 0, 0);
            this.MainTableLayoutPanel.Controls.Add(this.BigTextLabel, 1, 0);
            this.MainTableLayoutPanel.Controls.Add(this.StatusTextLabel, 1, 1);
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
            this.ImageBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImageBox.Image = global::Igtampe.LandViewPlotter.Properties.Resources.Load;
            this.ImageBox.Location = new System.Drawing.Point(20, 20);
            this.ImageBox.Margin = new System.Windows.Forms.Padding(20, 20, 5, 20);
            this.ImageBox.Name = "LoadIconPictureBox";
            this.MainTableLayoutPanel.SetRowSpan(this.ImageBox, 2);
            this.ImageBox.Size = new System.Drawing.Size(70, 69);
            this.ImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ImageBox.TabIndex = 0;
            this.ImageBox.TabStop = false;
            // 
            // PleaseWaitLabel
            // 
            this.BigTextLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BigTextLabel.AutoSize = true;
            this.BigTextLabel.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.BigTextLabel.Location = new System.Drawing.Point(98, 26);
            this.BigTextLabel.Name = "PleaseWaitLabel";
            this.BigTextLabel.Size = new System.Drawing.Size(100, 28);
            this.BigTextLabel.TabIndex = 1;
            this.BigTextLabel.Text = "Sit Tight!";
            // 
            // label1
            // 
            this.StatusTextLabel.AutoSize = true;
            this.StatusTextLabel.Location = new System.Drawing.Point(98, 54);
            this.StatusTextLabel.Name = "label1";
            this.StatusTextLabel.Size = new System.Drawing.Size(204, 30);
            this.StatusTextLabel.TabIndex = 2;
            this.StatusTextLabel.Text = "A Neco has been dispatched and will process your request";
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
            ((System.ComponentModel.ISupportInitialize)(this.ImageBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel MainTableLayoutPanel;
    }
}