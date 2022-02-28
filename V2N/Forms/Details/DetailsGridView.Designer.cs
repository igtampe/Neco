namespace Igtampe.Neco.V2N.Forms.Details {
    partial class DetailsGridView {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.DetailGroupbox = new System.Windows.Forms.GroupBox();
            this.MainGridView = new System.Windows.Forms.DataGridView();
            this.DetailGroupbox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // DetailGroupbox
            // 
            this.DetailGroupbox.Controls.Add(this.MainGridView);
            this.DetailGroupbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DetailGroupbox.Location = new System.Drawing.Point(0, 0);
            this.DetailGroupbox.Margin = new System.Windows.Forms.Padding(10);
            this.DetailGroupbox.Name = "DetailGroupbox";
            this.DetailGroupbox.Size = new System.Drawing.Size(528, 280);
            this.DetailGroupbox.TabIndex = 0;
            this.DetailGroupbox.TabStop = false;
            this.DetailGroupbox.Text = "Grid Name";
            // 
            // MainGridView
            // 
            this.MainGridView.AllowUserToAddRows = false;
            this.MainGridView.AllowUserToDeleteRows = false;
            this.MainGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MainGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGridView.Location = new System.Drawing.Point(3, 19);
            this.MainGridView.Name = "MainGridView";
            this.MainGridView.ReadOnly = true;
            this.MainGridView.RowTemplate.Height = 25;
            this.MainGridView.Size = new System.Drawing.Size(522, 258);
            this.MainGridView.TabIndex = 0;
            // 
            // DetailsGridView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DetailGroupbox);
            this.Name = "DetailsGridView";
            this.Size = new System.Drawing.Size(528, 280);
            this.DetailGroupbox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox DetailGroupbox;
        private DataGridView MainGridView;
    }
}
