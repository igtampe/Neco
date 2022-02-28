namespace Igtampe.Neco.V2N.Forms.Details {
    partial class DetailsForm {
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
            this.MainLabel = new System.Windows.Forms.Label();
            this.DetailsLabel = new System.Windows.Forms.Label();
            this.DetailsGroupBox = new System.Windows.Forms.GroupBox();
            this.DetailsTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.OKBTN = new System.Windows.Forms.Button();
            this.DetailGridViewsTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.MainTableLayoutPanel.SuspendLayout();
            this.DetailsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainTableLayoutPanel
            // 
            this.MainTableLayoutPanel.ColumnCount = 2;
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTableLayoutPanel.Controls.Add(this.MainLabel, 0, 0);
            this.MainTableLayoutPanel.Controls.Add(this.DetailsLabel, 0, 1);
            this.MainTableLayoutPanel.Controls.Add(this.DetailsGroupBox, 0, 2);
            this.MainTableLayoutPanel.Controls.Add(this.OKBTN, 1, 4);
            this.MainTableLayoutPanel.Controls.Add(this.DetailGridViewsTableLayoutPanel, 1, 2);
            this.MainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.MainTableLayoutPanel.Name = "MainTableLayoutPanel";
            this.MainTableLayoutPanel.Padding = new System.Windows.Forms.Padding(25);
            this.MainTableLayoutPanel.RowCount = 3;
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainTableLayoutPanel.Size = new System.Drawing.Size(650, 568);
            this.MainTableLayoutPanel.TabIndex = 0;
            // 
            // MainLabel
            // 
            this.MainLabel.AutoSize = true;
            this.MainTableLayoutPanel.SetColumnSpan(this.MainLabel, 2);
            this.MainLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainLabel.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.MainLabel.Location = new System.Drawing.Point(28, 25);
            this.MainLabel.Name = "MainLabel";
            this.MainLabel.Size = new System.Drawing.Size(594, 29);
            this.MainLabel.TabIndex = 0;
            this.MainLabel.Text = "Igtampe (57174)";
            // 
            // DetailsLabel
            // 
            this.DetailsLabel.AutoSize = true;
            this.MainTableLayoutPanel.SetColumnSpan(this.DetailsLabel, 2);
            this.DetailsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DetailsLabel.Location = new System.Drawing.Point(28, 54);
            this.DetailsLabel.Name = "DetailsLabel";
            this.DetailsLabel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.DetailsLabel.Size = new System.Drawing.Size(594, 25);
            this.DetailsLabel.TabIndex = 1;
            this.DetailsLabel.Text = "Personal Account";
            // 
            // DetailsGroupBox
            // 
            this.DetailsGroupBox.Controls.Add(this.DetailsTableLayoutPanel);
            this.DetailsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DetailsGroupBox.Location = new System.Drawing.Point(28, 82);
            this.DetailsGroupBox.Name = "DetailsGroupBox";
            this.DetailsGroupBox.Size = new System.Drawing.Size(244, 429);
            this.DetailsGroupBox.TabIndex = 2;
            this.DetailsGroupBox.TabStop = false;
            this.DetailsGroupBox.Text = "Details";
            // 
            // DetailsTableLayoutPanel
            // 
            this.DetailsTableLayoutPanel.ColumnCount = 2;
            this.DetailsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.DetailsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.DetailsTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DetailsTableLayoutPanel.Location = new System.Drawing.Point(3, 19);
            this.DetailsTableLayoutPanel.Name = "DetailsTableLayoutPanel";
            this.DetailsTableLayoutPanel.RowCount = 1;
            this.DetailsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.DetailsTableLayoutPanel.Size = new System.Drawing.Size(238, 407);
            this.DetailsTableLayoutPanel.TabIndex = 0;
            // 
            // OKBTN
            // 
            this.OKBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OKBTN.Location = new System.Drawing.Point(547, 517);
            this.OKBTN.Name = "OKBTN";
            this.OKBTN.Size = new System.Drawing.Size(75, 23);
            this.OKBTN.TabIndex = 3;
            this.OKBTN.Text = "OK";
            this.OKBTN.UseVisualStyleBackColor = true;
            this.OKBTN.Click += new System.EventHandler(this.OKBTN_Click);
            // 
            // DetailGridViewsTableLayoutPanel
            // 
            this.DetailGridViewsTableLayoutPanel.ColumnCount = 1;
            this.DetailGridViewsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.DetailGridViewsTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DetailGridViewsTableLayoutPanel.Location = new System.Drawing.Point(278, 82);
            this.DetailGridViewsTableLayoutPanel.Name = "DetailGridViewsTableLayoutPanel";
            this.DetailGridViewsTableLayoutPanel.RowCount = 1;
            this.DetailGridViewsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.DetailGridViewsTableLayoutPanel.Size = new System.Drawing.Size(344, 429);
            this.DetailGridViewsTableLayoutPanel.TabIndex = 4;
            // 
            // DetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 568);
            this.Controls.Add(this.MainTableLayoutPanel);
            this.Name = "DetailsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Details Form";
            this.MainTableLayoutPanel.ResumeLayout(false);
            this.MainTableLayoutPanel.PerformLayout();
            this.DetailsGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel MainTableLayoutPanel;
        private Label MainLabel;
        private Label DetailsLabel;
        private GroupBox DetailsGroupBox;
        private TableLayoutPanel DetailsTableLayoutPanel;
        private Button OKBTN;
        private TableLayoutPanel DetailGridViewsTableLayoutPanel;
    }
}