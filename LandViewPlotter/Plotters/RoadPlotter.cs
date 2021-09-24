﻿using System;
using System.Drawing;
using System.Windows.Forms;
using Igtampe.Neco.Common.LandView;

namespace Igtampe.LandViewPlotter {
    public partial class RoadPlotter: Form {

        public Road MyRoad { get; private set; }
        public bool Edited { get; private set; }


        public RoadPlotter(Road R) {
            InitializeComponent();
            Icon = Properties.Resources.MainIcon;
            MyRoad = R;

            #region Event handlers
            NameBox.GotFocus += NameBox_GotFocus;
            NameBox.LostFocus += NameBox_LostFocus;
            PointsBox.GotFocus += PointsBox_GotFocus;
            PointsBox.LostFocus += PointsBox_LostFocus;
            PointsBox.TextChanged += PointsBox_TextChanged;
            WidthBox.GotFocus += WidthBox_GotFocus;
            WidthBox.LostFocus += WidthBox_LostFocus;
            #endregion

            if (R.ID != Guid.Empty) {
                CanButton.Visible = false;
                DetailTableLayoutPanel.SetColumn(OKButton, 1);
                DialogResult = DialogResult.OK;
            }

            PopulateData();
        }

        private void PopulateData() {
            NameBox.Text = MyRoad.Name;
            WidthBox.Text = MyRoad.Width + "";
            PointsBox.Text = MyRoad.Points;
        }


        private void NameBox_GotFocus(object sender, EventArgs e) {
            NameLabel.Text = "Name*";
        }

        private void NameBox_LostFocus(object sender, EventArgs e) {
            NameLabel.Text = "Name";
            if (MyRoad.Name == NameBox.Text) { return; }
            MyRoad.Name = NameBox.Text;
            MarkEdited();
        }

        private void PointsBox_GotFocus(object sender, EventArgs e) {
            PointsLabel.Text = "Points*";
        }

        private void PointsBox_LostFocus(object sender, EventArgs e) {
            PointsLabel.Text = "Points";
            PointsLabel.ForeColor = Color.Black;
            if (MyRoad.Points == PointsBox.Text) { return; }
            if (!LandViewUtils.ValidatePoints(PointsBox.Text, 2)) {
                PointsLabel.Text = "Points*";
                PointsLabel.ForeColor = Color.Red;
                return;
            }
            MyRoad.Points = PointsBox.Text;
            MarkEdited();
        }

        private void PointsBox_TextChanged(object sender, EventArgs e) {
            if (!LandViewUtils.ValidatePoints(PointsBox.Text, 2)) { return; }
            MyRoad.Points = PointsBox.Text;
            MarkEdited();
        }

        private void WidthBox_GotFocus(object sender, EventArgs e) {
            WidthLabel.Text = "Width*";
        }

        private void WidthBox_LostFocus(object sender, EventArgs e) {
            WidthLabel.Text = "Width";
            if (!int.TryParse(WidthBox.Text, out int W)) {
                ShowCriticalMessagebox($"Could not parse '{WidthBox.Text}' as an integer");
                WidthBox.Text = MyRoad.Width + "";
            } else {
                if (W == MyRoad.Width) { return; }
                MyRoad.Width = W;
                MarkEdited();
            }
        }

        private void OKButton_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e) {
            Close();
        }

        private void MarkEdited() { Edited = true; }

        private static void ShowCriticalMessagebox(string message) { MessageBox.Show(message, "No", MessageBoxButtons.OK, MessageBoxIcon.Error); }


    }
}
