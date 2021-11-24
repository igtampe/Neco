namespace Igtampe.TinyForms {
    partial class DropdownForm<T> {
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
            this.TheLabel = new System.Windows.Forms.Label();
            this.OKBtn = new System.Windows.Forms.Button();
            this.CancelBTN = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.TheBox = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TheLabel
            // 
            this.TheLabel.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.TheLabel, 3);
            this.TheLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TheLabel.Location = new System.Drawing.Point(20, 20);
            this.TheLabel.Margin = new System.Windows.Forms.Padding(20, 20, 20, 10);
            this.TheLabel.Name = "TheLabel";
            this.TheLabel.Size = new System.Drawing.Size(360, 22);
            this.TheLabel.TabIndex = 0;
            this.TheLabel.Text = "label1";
            // 
            // OKBtn
            // 
            this.OKBtn.Location = new System.Drawing.Point(196, 98);
            this.OKBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 20);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new System.Drawing.Size(88, 27);
            this.OKBtn.TabIndex = 2;
            this.OKBtn.Text = "OK";
            this.OKBtn.UseVisualStyleBackColor = true;
            this.OKBtn.Click += new System.EventHandler(this.OKBtn_Click);
            // 
            // CancelBTN
            // 
            this.CancelBTN.Location = new System.Drawing.Point(292, 98);
            this.CancelBTN.Margin = new System.Windows.Forms.Padding(4, 3, 20, 3);
            this.CancelBTN.Name = "CancelBTN";
            this.CancelBTN.Size = new System.Drawing.Size(88, 27);
            this.CancelBTN.TabIndex = 3;
            this.CancelBTN.Text = "Cancel";
            this.CancelBTN.UseVisualStyleBackColor = true;
            this.CancelBTN.Click += new System.EventHandler(this.CancelBTN_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.CancelBTN, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.TheLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.OKBtn, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.TheBox, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(400, 145);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // TheBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.TheBox, 3);
            this.TheBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TheBox.FormattingEnabled = true;
            this.TheBox.Location = new System.Drawing.Point(20, 62);
            this.TheBox.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.TheBox.Name = "TheBox";
            this.TheBox.Size = new System.Drawing.Size(360, 23);
            this.TheBox.TabIndex = 4;
            // 
            // DropdownForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 145);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "DropdownForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ComboBox Form";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label TheLabel;
        private System.Windows.Forms.Button OKBtn;
        private System.Windows.Forms.Button CancelBTN;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox TheBox;
    }
}