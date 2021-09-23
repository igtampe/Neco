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
using Microsoft.EntityFrameworkCore;

namespace Igtampe.LandViewPlotter {
    public partial class CountryPlotter: Form {

        private NecoContext NecoDB;
        private Country MyCountry;
        private bool Edited;

        public CountryPlotter() {
            InitializeComponent();

            //Mira if the designer won't respect me then:
            #region Event handlers
            Shown += OpenMenuItem_Click;
            FormClosing += CountryPlotter_FormClosing;
            NameBox.GotFocus += NameBox_GotFocus;
            NameBox.LostFocus += NameBox_LostFocus;
            WidthBox.GotFocus += WidthBox_GotFocus;
            WidthBox.LostFocus += WidthBox_LostFocus;
            HeightBox.GotFocus += HeightBox_GotFocus;
            HeightBox.LostFocus += HeightBox_LostFocus;
            BankAccountBox.GotFocus += BankAccountBox_GotFocus;
            BankAccountBox.LostFocus += BankAccountBox_LostFocus;
            PPSMBox.GotFocus += PPSMBox_GotFocus;
            PPSMBox.LostFocus += PPSMBox_LostFocus;
            DistrictsContextMenu.Opening += DistrictsContextMenu_Opening;
            RoadContextMenu.Opening += RoadContextMenu_Opening;
            //newDistrictToolStripMenuItem.Click += NewDistrictToolStripMenuItem_Click; //so is this one
            //EditDistrictToolStripMenuItem.Click += EditDistrictToolStripMenuItem_Click; //This one is actually respected.
            newRoadToolStripMenuItem.Click += NewRoadToolStripMenuItem_Click;
            EditRoadToolStripMenuItem.Click += EditRoadToolStripMenuItem_Click;
            DeleteRoadToolStripMenuItem.Click += DeleteRoadToolStripMenuItem_Click;
            CountryPickerToolStripMenuItem.Click += OpenMenuItem_Click;
            saveToolStripMenuItem.Click += SaveToolStripMenuItem_Click;
            exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
            #endregion

        }

        /// <summary>Populates the form with information</summary>
        private void PopulateData() {
            NameBox.Text = MyCountry.Name;
            WidthBox.Text = MyCountry.Width+"";
            HeightBox.Text = MyCountry.Height+"";
            BankAccountBox.Text = MyCountry.FederalBankAccount?.ID;
            PPSMBox.Text = MyCountry.PricePerSquareMeter + "";
            PreviewPictureBox.Image = LandViewGraphicsEngine.GenerateCountryImage(MyCountry);

            DistrictsListView.Items.Clear();
            foreach (District D in MyCountry.Districts) {DistrictsListView.Items.Add(D.Name);}

            RoadsListView.Items.Clear();
            foreach (Road R in MyCountry.Roads) { RoadsListView.Items.Add(R.Name); }
        }

        private void CountryPlotter_FormClosing(object sender, FormClosingEventArgs e) {
            if (Edited) {
                switch (MessageBox.Show("There are unsaved changes! Would you like to save?", "Are You Sure?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)) {
                    case DialogResult.Yes:
                        SaveToolStripMenuItem_Click(sender, null);
                        break;
                    case DialogResult.No:
                        return;
                    case DialogResult.None:
                    case DialogResult.Cancel:
                    case DialogResult.OK:
                    case DialogResult.Abort:
                    case DialogResult.Retry:
                    case DialogResult.Ignore:
                        e.Cancel = true;
                        break;
                }
            }
        }


        #region MainContextMenu

        private void PreviewPictureBox_Click(object sender, EventArgs e) {
            if (PreviewPictureBox.Image == null) { return; }
            new PreviewForm(PreviewPictureBox.Image).ShowDialog();
        }

        private void OpenMenuItem_Click(object sender, EventArgs e) {
            //Load all countries
            if (NecoDB != null) {

                FormClosingEventArgs E = new(CloseReason.None,false);
                CountryPlotter_FormClosing(sender, E);
                if (E.Cancel) { return; }
                
                NecoDB.Dispose(); 
            
            }
            NecoDB = new();

            List<Country> Countries = NecoDB.Country
                    .Include(C => C.Districts).ThenInclude(D => D.Plots).ThenInclude(P => P.Owner).ThenInclude(U => U.Type)
                    .Include(C => C.Districts).ThenInclude(D => D.DistrictBankAccount)
                    .Include(C => C.FederalBankAccount)
                    .ToList();

            CountryPicker CP = new(Countries);
            if (CP.ShowDialog() != DialogResult.OK && MyCountry == null) { Close(); return; }

            MyCountry = CP.SelectedCountry;
            if (MyCountry.Districts == null) { MyCountry.Districts = new List<District>(); }
            if (MyCountry.Roads == null) { MyCountry.Roads = new List<Road>(); }
            PopulateData();
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e) {
            if (MyCountry.ID == Guid.Empty) { NecoDB.Add(MyCountry); } else { NecoDB.Update(MyCountry); }
            NecoDB.SaveChanges();
            Edited = false;
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e) { Close(); }

        #endregion

        #region DistrictsContextMenu

        private void DistrictsContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e) {
            EditDistrictToolStripMenuItem.Enabled = DistrictsListView.SelectedIndices.Count > 0;
        }

        private void NewDistrictToolStripMenuItem_Click(object sender, EventArgs e) {
            District D = new() {
                Country = MyCountry,
                Plots =  new List<Plot>()
            };

            DistrictPlotter DPlotter = new(D, NecoDB);
            if (DPlotter.ShowDialog() == DialogResult.OK) {
                //Add the plot
                MyCountry.Districts.Add(DPlotter.MyDistrict);
                MarkEdited();
                PopulateData();
            }
        }

        private void EditDistrictToolStripMenuItem_Click(object sender, EventArgs e) {
            int Index = GetSelectedDistrictIndex();
            if (Index == -1) { return; }

            District D = MyCountry.Districts[Index];
            DistrictPlotter DPlotter = new(D, NecoDB);
            if (DPlotter.ShowDialog() == DialogResult.OK) {
                if (!DPlotter.Edited) { return; }
                MyCountry.Districts[Index] = DPlotter.MyDistrict;
                MarkEdited();
                PopulateData();
            }
        }

        private int GetSelectedDistrictIndex() {
            if (DistrictsListView.SelectedIndices.Count == 0) { return -1; } else { return DistrictsListView.SelectedIndices[0]; }
        }

        #endregion

        #region RoadContextMenu

        private void RoadContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e) {
            bool EnableEditOptions = RoadsListView.SelectedIndices.Count > 0;
            EditRoadToolStripMenuItem.Enabled = EnableEditOptions;
            DeleteRoadToolStripMenuItem.Enabled = EnableEditOptions;
        }

        private void NewRoadToolStripMenuItem_Click(object sender, EventArgs e) {
            Road R = new() { Country = MyCountry, };

            RoadPlotter RPlotter = new(R);
            if (RPlotter.ShowDialog() == DialogResult.OK) {
                //Add the plot
                MyCountry.Roads.Add(RPlotter.MyRoad);
                MarkEdited();
                PopulateData();
            }

        }

        private void EditRoadToolStripMenuItem_Click(object sender, EventArgs e) {
            int Index = GetSelectedRoadIndex();
            if (Index == -1) { return; }

            Road R = MyCountry.Roads[Index];
            RoadPlotter RPlotter = new(R);
            if (RPlotter.ShowDialog() == DialogResult.OK) {
                if (!RPlotter.Edited) { return; }
                MyCountry.Roads[Index] = RPlotter.MyRoad;
                MarkEdited();
                PopulateData();
            }
        }

        private void DeleteRoadToolStripMenuItem_Click(object sender, EventArgs e) {
            int Index = GetSelectedRoadIndex();
            if (Index == -1) { return; }

            Road R = MyCountry.Roads[Index];
            if (MessageBox.Show($"Are you sure you want to delete Road {R.Name}?\n" +
                $"\n" +
                $"This is IRREVIRSIBLE and will take effect as soon as you hit YES (NOT WHEN YOU HIT SAVE)!\n","Are you sure?",
                MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation)==DialogResult.Yes) {
                NecoDB.Remove(R);
                NecoDB.SaveChanges();
                MyCountry.Roads.Remove(R);
                PopulateData();
            }

        }

        private int GetSelectedRoadIndex() {
            if (RoadsListView.SelectedIndices.Count == 0) { return -1; } else { return RoadsListView.SelectedIndices[0]; }
        }


        #endregion

        #region TextBox Handlers

        private void BankAccountBox_GotFocus(object sender, System.EventArgs e) {BankAccountLabel.Text = "Bank Account*";}

        private void BankAccountBox_LostFocus(object sender, System.EventArgs e) {
            BankAccountLabel.Text = "Bank Account";
            if (MyCountry.FederalBankAccount?.ID == BankAccountBox.Text) { return; }
            if (string.IsNullOrWhiteSpace(BankAccountBox.Text)) {
                MyCountry.FederalBankAccount = null;
                MarkEdited();
                return;
            }

            var B = NecoDB.BankAccount.Find(BankAccountBox.Text);
            if (B==null) {
                ShowCriticalMessagebox($"Could not find bank account with ID '{BankAccountBox.Text}'");
                BankAccountBox.Text = MyCountry.FederalBankAccount?.ID;
                return; 
            }
            
            if (B.Closed) {
                ShowCriticalMessagebox($"Bank Account with ID '{BankAccountBox.Text}' is closed");
                BankAccountBox.Text = MyCountry.FederalBankAccount?.ID;
                return;
            }

            MyCountry.FederalBankAccount = B;
            MarkEdited();

        }

        private void HeightBox_GotFocus(object sender, System.EventArgs e) {HeightLabel.Text = "Height*";}

        private void HeightBox_LostFocus(object sender, System.EventArgs e) {
            HeightLabel.Text = "Height";
            if (!int.TryParse(HeightBox.Text, out int H)) {
                ShowCriticalMessagebox($"Could not parse '{HeightBox.Text}' as an integer");
                HeightBox.Text = MyCountry.Width + "";
            } else {
                if (H == MyCountry.Height) { return; }
                MyCountry.Height = H;
                MarkEdited();
            }
        }

        private void WidthBox_GotFocus(object sender, System.EventArgs e) {WidthLabel.Text = "Width*";}

        private void WidthBox_LostFocus(object sender, System.EventArgs e) {
            WidthLabel.Text = "Width";
            if (!int.TryParse(WidthBox.Text, out int W)) {
                ShowCriticalMessagebox($"Could not parse '{WidthBox.Text}' as an integer");
                WidthBox.Text = MyCountry.Width + "";
            } else {
                if (W == MyCountry.Width) { return; }
                MyCountry.Width = W;
                MarkEdited();
            }
        }

        private void NameBox_GotFocus(object sender, System.EventArgs e) {NameLabel.Text = "Name*";}

        private void NameBox_LostFocus(object sender, System.EventArgs e) {
            NameLabel.Text = "Name";
            if (MyCountry.Name == NameBox.Text) { return; }
            MyCountry.Name = NameBox.Text;
            MarkEdited();
        }

        private void PPSMBox_GotFocus(object sender, EventArgs e) {
            PPSMLabel.Text = "Price per m²*";
        }

        private void PPSMBox_LostFocus(object sender, EventArgs e) {
            PPSMLabel.Text = "Price per m²";
            if (!int.TryParse(PPSMBox.Text, out int P)) {
                ShowCriticalMessagebox($"Could not parse '{PPSMBox.Text}' as an integer");
                PPSMBox.Text = MyCountry.PricePerSquareMeter + "";
            } else {
                if (P == MyCountry.PricePerSquareMeter) { return; }
                MyCountry.PricePerSquareMeter = P;
                MarkEdited();
            }
        }

        #endregion

        private void MarkEdited() {
            Edited = true;
            PreviewPictureBox.Image = LandViewGraphicsEngine.GenerateCountryImage(MyCountry);
        }

        private static void ShowCriticalMessagebox(string message) {MessageBox.Show(message, "No", MessageBoxButtons.OK, MessageBoxIcon.Error);}

    }
}
