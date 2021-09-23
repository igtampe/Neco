using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Igtampe.Neco.Common.LandView;
using Igtampe.Neco.Data;

namespace Igtampe.LandViewPlotter {
    public partial class DistrictPlotter: Form {

        private NecoContext NecoDB;
        private District MyDistrict;
        private bool Edited;

        public DistrictPlotter(District D, NecoContext NecoDB) {
            InitializeComponent();
            MyDistrict = D;
            this.NecoDB = NecoDB;

            #region Event handlers
            NameBox.GotFocus += NameBox_GotFocus;
            NameBox.LostFocus += NameBox_LostFocus;
            PointsBox.GotFocus += PointsBox_GotFocus;
            PointsBox.LostFocus += PointsBox_LostFocus;
            PointsBox.TextChanged += PointsBox_TextChanged;
            BankAccountBox.GotFocus += BankAccountBox_GotFocus;
            BankAccountBox.LostFocus += BankAccountBox_LostFocus;
            PPSMBox.GotFocus += PPSMBox_GotFocus;
            PPSMBox.LostFocus += PPSMBox_LostFocus;
            PlotContextMenu.Opening += PlotContextMenu_Opening;
            NewPlotToolStripMenuItem.Click += NewPlotToolStripMenuItem_Click;
            EditPlotToolStripMenuItem.Click += EditPlotToolStripMenuItem_Click;
            DeletePlotToolStripMenuItem.Click += DeletePlotToolStripMenuItem_Click;
            #endregion

            PopulateData();
        }

        private void PopulateData() {
            NameBox.Text = MyDistrict.Name;
            PointsBox.Text = MyDistrict.Points;
            BankAccountBox.Text = MyDistrict.DistrictBankAccount?.ID;
            PPSMBox.Text = MyDistrict.PricePerSquareMeter+"";

            PlotsListView.Items.Clear();
            foreach (Plot P in MyDistrict.Plots) { PlotsListView.Items.Add(P.Name); }

        }

        private void PlotContextMenu_Opening(object sender, CancelEventArgs e) {
            bool EnableEditOptions = PlotsListView.SelectedIndices.Count > 0;
            EditPlotToolStripMenuItem.Enabled = EnableEditOptions;
            DeletePlotToolStripMenuItem.Enabled = EnableEditOptions;
        }

        private void NewPlotToolStripMenuItem_Click(object sender, EventArgs e) {
        }

        private void EditPlotToolStripMenuItem_Click(object sender, EventArgs e) {
        }

        private void DeletePlotToolStripMenuItem_Click(object sender, EventArgs e) {
        }

        private void PPSMBox_GotFocus(object sender, EventArgs e) { PPSMLabel.Text = "Price per m²*"; }

        private void PPSMBox_LostFocus(object sender, EventArgs e) {
            PPSMLabel.Text = "Price per m²";
            if (!int.TryParse(PPSMBox.Text, out int P)) {
                ShowCriticalMessagebox($"Could not parse '{PPSMBox.Text}' as an integer");
                PPSMBox.Text = MyDistrict.PricePerSquareMeter + "";
            } else {
                if (P == MyDistrict.PricePerSquareMeter) { return; }
                MyDistrict.PricePerSquareMeter = P;
                MarkEdited();
            }
        }

        private void BankAccountBox_GotFocus(object sender, EventArgs e) {BankAccountLabel.Text = "Bank Account*";}

        private void BankAccountBox_LostFocus(object sender, EventArgs e) {
            BankAccountLabel.Text = "Bank Account";
            if (MyDistrict.DistrictBankAccount?.ID == BankAccountBox.Text) { return; }
            if (string.IsNullOrWhiteSpace(BankAccountBox.Text)) {
                MyDistrict.DistrictBankAccount = null;
                MarkEdited();
                return;
            }

            var B = NecoDB.BankAccount.Find(BankAccountBox.Text);
            if (B == null) {
                ShowCriticalMessagebox($"Could not find bank account with ID '{BankAccountBox.Text}'");
                BankAccountBox.Text = MyDistrict.DistrictBankAccount?.ID;
                return;
            }

            if (B.Closed) {
                ShowCriticalMessagebox($"Bank Account with ID '{BankAccountBox.Text}' is closed");
                BankAccountBox.Text = MyDistrict.DistrictBankAccount?.ID;
                return;
            }

            MyDistrict.DistrictBankAccount = B;
            MarkEdited();
        }

        private void PointsBox_GotFocus(object sender, EventArgs e) {
            PointsLabel.Text = "Points*";
        }

        private void PointsBox_TextChanged(object sender, EventArgs e) {
            if (!ValidatePoints(PointsBox.Text)) { return; }
            MyDistrict.Points = PointsBox.Text;
            MarkEdited();
        }

        private void PointsBox_LostFocus(object sender, EventArgs e) {
            PointsLabel.Text = "Points";
            if (MyDistrict.Points == PointsBox.Text) { return; }
            if (!ValidatePoints(PointsBox.Text)) {
                ShowCriticalMessagebox("Could not parse points! Please fix them before continuing");
                PointsBox.Focus();
                return;
            }
            MyDistrict.Points = PointsBox.Text;
            MarkEdited();
        }

        private bool ValidatePoints(string Points) {

            //Ensure blank ones can be actually saved.
            if (Points.Length == 0) { return true; }

            //First let's split this by semicolon
            foreach (string ppair in Points.Split(";")) {
                //Split it again by colon
                string[] ppairsplit = ppair.Split(",");
                if (ppairsplit.Count() != 2) { return false; } //If we have more than three coords then no
                if (!int.TryParse(ppairsplit[0], out int _)) { return false; } //Test to ensure both can be
                if (!int.TryParse(ppairsplit[1], out int _)) { return false; } //parsed as ints
            }
            return true;
        }

        private void NameBox_GotFocus(object sender, EventArgs e) {
            NameLabel.Text = "Name*";
        }

        private void NameBox_LostFocus(object sender, EventArgs e) {
            NameLabel.Text = "Name";
            if (MyDistrict.Name == NameBox.Text) { return; }
            MyDistrict.Name = NameBox.Text;
            MarkEdited();
        }

        private void OKButton_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e) {
            Close();
        }

        private void PreviewPictureBox_Click(object sender, EventArgs e) {
            if (PreviewPictureBox.Image == null) { return; }
            new PreviewForm(PreviewPictureBox.Image).ShowDialog();
        }

        private void MarkEdited() {
            Edited = true;
            PreviewPictureBox.Image = LandViewGraphicsEngine.GenerateDistrictImage(MyDistrict);
        }

        private static void ShowCriticalMessagebox(string message) { MessageBox.Show(message, "No", MessageBoxButtons.OK, MessageBoxIcon.Error); }

    }
}
