namespace Igtampe.LitterBox.ItemForms
{
    partial class UserTypesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.UserTypeDetailsGroupBox = new System.Windows.Forms.GroupBox();
            this.UserTypeDetailsTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.UserTypeNameLabel = new System.Windows.Forms.Label();
            this.UserTypeTaxationTypeLabel = new System.Windows.Forms.Label();
            this.UserTypeUserOpenableLabel = new System.Windows.Forms.Label();
            this.UserTypeNameBox = new System.Windows.Forms.TextBox();
            this.UserTypeTaxationTypeComboBox = new System.Windows.Forms.ComboBox();
            this.UserTypeUserOpenableCheckbox = new System.Windows.Forms.CheckBox();
            this.UserTypesGroupBox = new System.Windows.Forms.GroupBox();
            this.UserTypesListview = new System.Windows.Forms.ListView();
            this.UserTypesNameColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.UserTypesTaxationTypeColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.UserTypeUserOpenableColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.MainContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OKButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.UserTypeDetailsGroupBox.SuspendLayout();
            this.UserTypeDetailsTableLayoutPanel.SuspendLayout();
            this.UserTypesGroupBox.SuspendLayout();
            this.MainContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.UserTypeDetailsGroupBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.UserTypesGroupBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.OKButton, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(432, 408);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // UserTypeDetailsGroupBox
            // 
            this.UserTypeDetailsGroupBox.AutoSize = true;
            this.UserTypeDetailsGroupBox.Controls.Add(this.UserTypeDetailsTableLayoutPanel);
            this.UserTypeDetailsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserTypeDetailsGroupBox.Location = new System.Drawing.Point(10, 245);
            this.UserTypeDetailsGroupBox.Margin = new System.Windows.Forms.Padding(10);
            this.UserTypeDetailsGroupBox.Name = "UserTypeDetailsGroupBox";
            this.UserTypeDetailsGroupBox.Size = new System.Drawing.Size(412, 110);
            this.UserTypeDetailsGroupBox.TabIndex = 3;
            this.UserTypeDetailsGroupBox.TabStop = false;
            this.UserTypeDetailsGroupBox.Text = "Details";
            // 
            // UserTypeDetailsTableLayoutPanel
            // 
            this.UserTypeDetailsTableLayoutPanel.ColumnCount = 2;
            this.UserTypeDetailsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.UserTypeDetailsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.UserTypeDetailsTableLayoutPanel.Controls.Add(this.UserTypeNameLabel, 0, 0);
            this.UserTypeDetailsTableLayoutPanel.Controls.Add(this.UserTypeTaxationTypeLabel, 0, 1);
            this.UserTypeDetailsTableLayoutPanel.Controls.Add(this.UserTypeUserOpenableLabel, 0, 2);
            this.UserTypeDetailsTableLayoutPanel.Controls.Add(this.UserTypeNameBox, 1, 0);
            this.UserTypeDetailsTableLayoutPanel.Controls.Add(this.UserTypeTaxationTypeComboBox, 1, 1);
            this.UserTypeDetailsTableLayoutPanel.Controls.Add(this.UserTypeUserOpenableCheckbox, 1, 2);
            this.UserTypeDetailsTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserTypeDetailsTableLayoutPanel.Location = new System.Drawing.Point(3, 19);
            this.UserTypeDetailsTableLayoutPanel.Name = "UserTypeDetailsTableLayoutPanel";
            this.UserTypeDetailsTableLayoutPanel.RowCount = 3;
            this.UserTypeDetailsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.UserTypeDetailsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.UserTypeDetailsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.UserTypeDetailsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.UserTypeDetailsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.UserTypeDetailsTableLayoutPanel.Size = new System.Drawing.Size(406, 88);
            this.UserTypeDetailsTableLayoutPanel.TabIndex = 1;
            // 
            // UserTypeNameLabel
            // 
            this.UserTypeNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UserTypeNameLabel.AutoSize = true;
            this.UserTypeNameLabel.Location = new System.Drawing.Point(158, 0);
            this.UserTypeNameLabel.Name = "UserTypeNameLabel";
            this.UserTypeNameLabel.Size = new System.Drawing.Size(42, 29);
            this.UserTypeNameLabel.TabIndex = 0;
            this.UserTypeNameLabel.Text = "Name:";
            this.UserTypeNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UserTypeTaxationTypeLabel
            // 
            this.UserTypeTaxationTypeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UserTypeTaxationTypeLabel.AutoSize = true;
            this.UserTypeTaxationTypeLabel.Location = new System.Drawing.Point(119, 29);
            this.UserTypeTaxationTypeLabel.Name = "UserTypeTaxationTypeLabel";
            this.UserTypeTaxationTypeLabel.Size = new System.Drawing.Size(81, 29);
            this.UserTypeTaxationTypeLabel.TabIndex = 1;
            this.UserTypeTaxationTypeLabel.Text = "Taxation Type:";
            this.UserTypeTaxationTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UserTypeUserOpenableLabel
            // 
            this.UserTypeUserOpenableLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UserTypeUserOpenableLabel.AutoSize = true;
            this.UserTypeUserOpenableLabel.Location = new System.Drawing.Point(113, 58);
            this.UserTypeUserOpenableLabel.Name = "UserTypeUserOpenableLabel";
            this.UserTypeUserOpenableLabel.Size = new System.Drawing.Size(87, 30);
            this.UserTypeUserOpenableLabel.TabIndex = 2;
            this.UserTypeUserOpenableLabel.Text = "User Openable:";
            this.UserTypeUserOpenableLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UserTypeNameBox
            // 
            this.UserTypeNameBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UserTypeNameBox.Location = new System.Drawing.Point(206, 3);
            this.UserTypeNameBox.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.UserTypeNameBox.Name = "UserTypeNameBox";
            this.UserTypeNameBox.Size = new System.Drawing.Size(190, 23);
            this.UserTypeNameBox.TabIndex = 3;
            this.UserTypeNameBox.TextChanged += new System.EventHandler(this.UserTypeNameBox_TextChanged);
            // 
            // UserTypeTaxationTypeComboBox
            // 
            this.UserTypeTaxationTypeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UserTypeTaxationTypeComboBox.FormattingEnabled = true;
            this.UserTypeTaxationTypeComboBox.Items.AddRange(new object[] {
            "Taxable (Normal)",
            "NonTaxableDestination (Charity)",
            "NonTaxableOrigin (Gov)"});
            this.UserTypeTaxationTypeComboBox.Location = new System.Drawing.Point(206, 32);
            this.UserTypeTaxationTypeComboBox.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.UserTypeTaxationTypeComboBox.Name = "UserTypeTaxationTypeComboBox";
            this.UserTypeTaxationTypeComboBox.Size = new System.Drawing.Size(190, 23);
            this.UserTypeTaxationTypeComboBox.TabIndex = 4;
            this.UserTypeTaxationTypeComboBox.Text = "NonTaxableDestination (Charity)";
            this.UserTypeTaxationTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.UserTypeTaxationTypeComboBox_SelectedIndexChanged);
            // 
            // UserTypeUserOpenableCheckbox
            // 
            this.UserTypeUserOpenableCheckbox.AutoSize = true;
            this.UserTypeUserOpenableCheckbox.Location = new System.Drawing.Point(206, 61);
            this.UserTypeUserOpenableCheckbox.Name = "UserTypeUserOpenableCheckbox";
            this.UserTypeUserOpenableCheckbox.Size = new System.Drawing.Size(15, 14);
            this.UserTypeUserOpenableCheckbox.TabIndex = 5;
            this.UserTypeUserOpenableCheckbox.UseVisualStyleBackColor = true;
            this.UserTypeUserOpenableCheckbox.CheckedChanged += new System.EventHandler(this.UserTypeUserOpenableCheckbox_CheckedChanged);
            // 
            // UserTypesGroupBox
            // 
            this.UserTypesGroupBox.Controls.Add(this.UserTypesListview);
            this.UserTypesGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserTypesGroupBox.Location = new System.Drawing.Point(10, 10);
            this.UserTypesGroupBox.Margin = new System.Windows.Forms.Padding(10);
            this.UserTypesGroupBox.Name = "UserTypesGroupBox";
            this.UserTypesGroupBox.Size = new System.Drawing.Size(412, 215);
            this.UserTypesGroupBox.TabIndex = 2;
            this.UserTypesGroupBox.TabStop = false;
            this.UserTypesGroupBox.Text = "User Types";
            // 
            // UserTypesListview
            // 
            this.UserTypesListview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.UserTypesNameColumnHeader,
            this.UserTypesTaxationTypeColumnHeader,
            this.UserTypeUserOpenableColumnHeader});
            this.UserTypesListview.ContextMenuStrip = this.MainContextMenuStrip;
            this.UserTypesListview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserTypesListview.FullRowSelect = true;
            this.UserTypesListview.Location = new System.Drawing.Point(3, 19);
            this.UserTypesListview.Name = "UserTypesListview";
            this.UserTypesListview.Size = new System.Drawing.Size(406, 193);
            this.UserTypesListview.TabIndex = 1;
            this.UserTypesListview.UseCompatibleStateImageBehavior = false;
            this.UserTypesListview.View = System.Windows.Forms.View.Details;
            this.UserTypesListview.SelectedIndexChanged += new System.EventHandler(this.UserTypesListview_SelectedIndexChanged);
            // 
            // UserTypesNameColumnHeader
            // 
            this.UserTypesNameColumnHeader.Text = "Name";
            this.UserTypesNameColumnHeader.Width = 150;
            // 
            // UserTypesTaxationTypeColumnHeader
            // 
            this.UserTypesTaxationTypeColumnHeader.Text = "Taxation Type";
            this.UserTypesTaxationTypeColumnHeader.Width = 130;
            // 
            // UserTypeUserOpenableColumnHeader
            // 
            this.UserTypeUserOpenableColumnHeader.Text = "User Openable?";
            this.UserTypeUserOpenableColumnHeader.Width = 100;
            // 
            // MainContextMenuStrip
            // 
            this.MainContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem});
            this.MainContextMenuStrip.Name = "MainContextMenuStrip";
            this.MainContextMenuStrip.Size = new System.Drawing.Size(99, 26);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OKButton.Location = new System.Drawing.Point(347, 375);
            this.OKButton.Margin = new System.Windows.Forms.Padding(10);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 0;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // UserTypesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 408);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "UserTypesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Types";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.UserTypeDetailsGroupBox.ResumeLayout(false);
            this.UserTypeDetailsTableLayoutPanel.ResumeLayout(false);
            this.UserTypeDetailsTableLayoutPanel.PerformLayout();
            this.UserTypesGroupBox.ResumeLayout(false);
            this.MainContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.GroupBox UserTypesGroupBox;
        private System.Windows.Forms.ListView UserTypesListview;
        private System.Windows.Forms.ColumnHeader UserTypesNameColumnHeader;
        private System.Windows.Forms.GroupBox UserTypeDetailsGroupBox;
        private System.Windows.Forms.TableLayoutPanel UserTypeDetailsTableLayoutPanel;
        private System.Windows.Forms.Label UserTypeNameLabel;
        private System.Windows.Forms.Label UserTypeTaxationTypeLabel;
        private System.Windows.Forms.Label UserTypeUserOpenableLabel;
        private System.Windows.Forms.TextBox UserTypeNameBox;
        private System.Windows.Forms.ComboBox UserTypeTaxationTypeComboBox;
        private System.Windows.Forms.CheckBox UserTypeUserOpenableCheckbox;
        private System.Windows.Forms.ContextMenuStrip MainContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader UserTypesTaxationTypeColumnHeader;
        private System.Windows.Forms.ColumnHeader UserTypeUserOpenableColumnHeader;
    }
}