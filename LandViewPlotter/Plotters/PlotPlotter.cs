using System;
using System.Windows.Forms;
using Igtampe.Neco.Data;
using Igtampe.Neco.Common.LandView;
using System.Drawing;

namespace Igtampe.LandViewPlotter {
    public partial class PlotPlotter: Form {

        public Plot MyPlot { get; private set; }
        private readonly NecoContext NecoDB;
        public bool Edited { get; private set; }

        public PlotPlotter(Plot P, NecoContext NecoDB) {
            InitializeComponent();
            Icon = Properties.Resources.MainIcon;
            MyPlot = P;
            this.NecoDB = NecoDB;

            #region Event handlers
            NameBox.GotFocus += NameBox_GotFocus;
            NameBox.LostFocus += NameBox_LostFocus;
            PointsBox.GotFocus += PointsBox_GotFocus;
            PointsBox.LostFocus += PointsBox_LostFocus;
            PointsBox.TextChanged += PointsBox_TextChanged;
            OwnerBox.GotFocus += OwnerBox_GotFocus;
            OwnerBox.LostFocus += OwnerBox_LostFocus;
            StatusComboBox.SelectedIndexChanged += StatusComboBox_SelectedIndexChanged;
            #endregion

            if (P.ID != Guid.Empty) {
                CanButton.Visible = false;
                MainTableLayoutPanel.SetColumn(OKButton, 2);
                DialogResult = DialogResult.OK;
            }

            PopulateData();
        }

        private void PopulateData() {
            NameBox.Text = MyPlot.Name;
            PointsBox.Text = MyPlot.Points;
            OwnerBox.Text = MyPlot.Owner?.ID;
            StatusComboBox.SelectedIndex = (int)MyPlot.Status;
        }


        private void NameBox_GotFocus(object sender, EventArgs e) {
            NameLabel.Text = "Name*";
        }

        private void NameBox_LostFocus(object sender, EventArgs e) {
            NameLabel.Text = "Name";
            if (MyPlot.Name == NameBox.Text) { return; }
            MyPlot.Name = NameBox.Text;
            MarkEdited();
        }

        private void PointsBox_GotFocus(object sender, EventArgs e) {
            PointsLabel.Text = "Points*";
        }

        private void PointsBox_TextChanged(object sender, EventArgs e) {
            if (!LandViewUtils.ValidatePoints(PointsBox.Text, 3)) { return; }
            MyPlot.Points = PointsBox.Text;
            MarkEdited();
        }

        private void PointsBox_LostFocus(object sender, EventArgs e) {
            PointsLabel.Text = "Points";
            PointsLabel.ForeColor = Color.Black;
            if (MyPlot.Points == PointsBox.Text) { return; }
            if (!LandViewUtils.ValidatePoints(PointsBox.Text, 3)) {
                PointsLabel.Text = "Points*";
                PointsLabel.ForeColor = Color.Red;
                return;
            }
            MyPlot.Points = PointsBox.Text;
            MarkEdited();
        }

        private void OwnerBox_GotFocus(object sender, EventArgs e) { OwnerLabel.Text = "Owner*"; }

        private void OwnerBox_LostFocus(object sender, EventArgs e) {
            OwnerLabel.Text = "Owner";
            if (MyPlot.Owner?.ID == OwnerBox.Text) { return; }
            if (string.IsNullOrWhiteSpace(OwnerBox.Text)) {
                MyPlot.Owner = null;
                MarkEdited();
                return;
            }

            var U = NecoDB.User.Find(OwnerBox.Text);
            if (U == null) {
                ShowCriticalMessagebox($"Could not find bank account with ID '{OwnerBox.Text}'");
                OwnerBox.Text = MyPlot.Owner?.ID;
                return;
            }

            MyPlot.Owner = U;
            MarkEdited();
        }

        private void StatusComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            MyPlot.Status = (PlotStatus)StatusComboBox.SelectedIndex;
            MarkEdited();
        }

        private void PreviewPictureBox_Click(object sender, EventArgs e) {
            if (PreviewPictureBox.Image == null) { return; }
            new PreviewForm(PreviewPictureBox.Image).ShowDialog();
        }

        private void OKButton_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e) {
            Close();
        }

        private void MarkEdited() {
            Edited = true;
            PreviewPictureBox.Image = LandViewGraphicsEngine.GeneratePlotImage(MyPlot);
            ZoomOrCenter();
        }

        private void ZoomOrCenter() {
            if (PreviewPictureBox.Image.Width < PreviewPictureBox.Width &&
                PreviewPictureBox.Image.Height < PreviewPictureBox.Height) {
                //If the Image is not big enough to fit or to squash to fit in the picture box, center it
                PreviewPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
                return;
            }

            //Else the image is either the exact size of the window or larger in some axis and must be s q u a s h e d
            PreviewPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private static void ShowCriticalMessagebox(string message) { MessageBox.Show(message, "No", MessageBoxButtons.OK, MessageBoxIcon.Error); }

    }
}
