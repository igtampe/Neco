
namespace Igtampe.LandViewPlotter {
    partial class PreviewForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
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
            this.MainPanel = new System.Windows.Forms.Panel();
            this.ImageBox = new System.Windows.Forms.PictureBox();
            this.PictureMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImageBox)).BeginInit();
            this.PictureMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.AutoScroll = true;
            this.MainPanel.Controls.Add(this.ImageBox);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(932, 690);
            this.MainPanel.TabIndex = 1;
            // 
            // ImageBox
            // 
            this.ImageBox.ContextMenuStrip = this.PictureMenuStrip;
            this.ImageBox.Location = new System.Drawing.Point(0, 0);
            this.ImageBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ImageBox.Name = "ImageBox";
            this.ImageBox.Size = new System.Drawing.Size(932, 690);
            this.ImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.ImageBox.TabIndex = 0;
            this.ImageBox.TabStop = false;
            this.ImageBox.Click += new System.EventHandler(this.ImageBox_Click);
            // 
            // PictureMenuStrip
            // 
            this.PictureMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.PictureMenuStrip.Name = "PictureMenuStrip";
            this.PictureMenuStrip.Size = new System.Drawing.Size(170, 48);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.copyToolStripMenuItem.Text = "Copy to clipboard";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.saveToolStripMenuItem.Text = "Save to disk";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // ExportFileDialog
            // 
            this.ExportFileDialog.DefaultExt = "png";
            this.ExportFileDialog.FileName = "UserTrace.png";
            this.ExportFileDialog.Filter = "PNG Files|*.png";
            this.ExportFileDialog.Title = "Export";
            // 
            // PreviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(932, 690);
            this.Controls.Add(this.MainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "PreviewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Preview";
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImageBox)).EndInit();
            this.PictureMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox ImageBox;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.ContextMenuStrip PictureMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog ExportFileDialog;
    }
}