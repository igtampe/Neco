using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Igtampe.Neco.Data;
using Igtampe.Neco.Common;
using Igtampe.Neco.Common.EzTax;
using Microsoft.EntityFrameworkCore;

namespace Igtampe.LitterBox {
    public partial class LaunchForm: Form {

        private NecoContext NecoDB;

        public LaunchForm() {
            InitializeComponent();
            FormClosing += LaunchForm_FormClosing;
            MainPannel.Enabled = false;
            Icon = Properties.Resources.MainIco;
            Shown += LaunchForm_Shown;
        }

        private void LaunchForm_Shown(object sender, EventArgs e) {ConnectDisconnectToolStripMenuItem_Click(sender, e); }

        private void ConnectDisconnectToolStripMenuItem_Click(object sender, EventArgs e) {
            if (NecoDB != null) {
                NecoDB.Dispose();
                connectDisconnectToolStripMenuItem.Text = "&Connect";
                NecoDB = null;
                MainPannel.Enabled = false;
                StatusLabel.Text = "Disconnected";
                return;
            }

            ConnectionForm F = new ConnectionForm();
            F.ShowDialog();
            if (F.Context != null) {

                //OK we have a de-esta cosa
                NecoDB = F.Context;
                connectDisconnectToolStripMenuItem.Text = "&Disconnect";
                MainPannel.Enabled = true;
                StatusLabel.Text = $"Connected to Neco Database";
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e) { Close(); }
        private void LaunchForm_FormClosing(object sender, FormClosingEventArgs e) { NecoDB?.Dispose(); } //Just in case

        #region User

        private void UsersListView_SelectedIndexChanged(object sender, EventArgs e) {

        }

        private void UserChangePinButton_Click(object sender, EventArgs e) {

        }

        private void GenerateIncomeReportButton_Click(object sender, EventArgs e) {

        }

        private void UserDetailsUserTypeComboBox_SelectedIndexChanged(object sender, EventArgs e) {

        }

        #endregion

        #region Cleanup

        private async void CleanupTransactionsButton_Click(object sender, EventArgs e) {

            MainPannel.Enabled = false;

            StatusLabel.Text = "Deleting old transactions";
            StatusProgressBar.Style = ProgressBarStyle.Marquee;

            //Get the transactions and checks
            List<Transaction> Trans = await NecoDB.Transaction.Where((T) => T.Time < DateTime.Now.AddMonths(-3)).ToListAsync();

            int count = 0;

            StatusLabel.Text = $"Deleting {Trans.Count} Transactions";
            StatusProgressBar.Style = ProgressBarStyle.Continuous;

            foreach (var T in Trans) {
                //Try to find a check with a matching transaction
                var Check = NecoDB.CheckbookItem.FirstOrDefaultAsync((C)=>C.AttachedTransaciton.ID==T.ID);
                if (Check != null) { NecoDB.Remove(Check); } //Remove the check

                //now remove the transaction
                NecoDB.Remove(T);

                StatusProgressBar.Value = (count * 100) / Trans.Count;
            }

            StatusLabel.Text = "Saving...";
            StatusProgressBar.Value = 0;
            StatusProgressBar.Style = ProgressBarStyle.Marquee;

            try {
                int Rows = await NecoDB.SaveChangesAsync();
                MessageBox.Show($"Cleanup complete! Deleted {Rows} row(s)","OK!",MessageBoxButtons.OK,MessageBoxIcon.Information);
            } catch (Exception E) {
                ShowExceptionDialogue(E, "There was an error saving the information to the database", "You may want to disconnect and reconnect to the database");
            }

            MainPannel.Enabled = true;
            StatusLabel.Text = "Connected to the Neco Database";
            StatusProgressBar.Style = ProgressBarStyle.Continuous;

        }

        private async void CleanupNotificationsButton_Click(object sender, EventArgs e) {

            MainPannel.Enabled = false;

            StatusLabel.Text = "Deleting old Notifications";
            StatusProgressBar.Style = ProgressBarStyle.Marquee;

            List<Notification> N = await NecoDB.Notification.Where((N) => N.Time < DateTime.Now.AddMonths(-1)).ToListAsync();
            
            StatusLabel.Text = $"Deleting {N.Count} Notification(s)";
            NecoDB.Notification.RemoveRange(N);

            StatusLabel.Text = "Saving...";
            try {
                int Rows = await NecoDB.SaveChangesAsync();
                MessageBox.Show($"Cleanup complete! Deleted {Rows} row(s)", "OK!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch (Exception E) {
                ShowExceptionDialogue(E, "There was an error saving the information to the database", "You may want to disconnect and reconnect to the database");
            }

            MainPannel.Enabled = true;
            StatusLabel.Text = "Connected to the Neco Database";
            StatusProgressBar.Style = ProgressBarStyle.Continuous;

        }

        private async void CleanupTaxReportsButton_Click(object sender, EventArgs e) {

            MainPannel.Enabled = false;

            StatusLabel.Text = "Deleting old Tax Reports";
            StatusProgressBar.Style = ProgressBarStyle.Marquee;

            List<TaxReport> N = await NecoDB.TaxReport.Where((N) => N.PreparedDate < DateTime.Now.AddMonths(-6)).ToListAsync();

            StatusLabel.Text = $"Deleting {N.Count} Tax Report(s)";
            NecoDB.TaxReport.RemoveRange(N);

            StatusLabel.Text = "Saving...";
            try {
                int Rows = await NecoDB.SaveChangesAsync();
                MessageBox.Show($"Cleanup complete! Deleted {Rows} row(s)", "OK!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch (Exception E) {
                ShowExceptionDialogue(E, "There was an error saving the information to the database", "You may want to disconnect and reconnect to the database");
            }

            MainPannel.Enabled = true;
            StatusLabel.Text = "Connected to the Neco Database";
            StatusProgressBar.Style = ProgressBarStyle.Continuous;

        }

        private async void CleanupCertifiedItemsButton_Click(object sender, EventArgs e) {

            MainPannel.Enabled = false;
             
            StatusLabel.Text = "Deleting old Certified Items";
            StatusProgressBar.Style = ProgressBarStyle.Marquee;

            List<CertifiedItem> N = await NecoDB.CertifiedItem.Where((N) => N.Date < DateTime.Now.AddMonths(-6)).ToListAsync();

            StatusLabel.Text = $"Deleting {N.Count} Certified Item(s)";
            NecoDB.CertifiedItem.RemoveRange(N);

            StatusLabel.Text = "Saving...";
            try {
                int Rows = await NecoDB.SaveChangesAsync();
                MessageBox.Show($"Cleanup complete! Deleted {Rows} row(s)", "OK!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch (Exception E) {
                ShowExceptionDialogue(E, "There was an error saving the information to the database", "You may want to disconnect and reconnect to the database");
            }

            MainPannel.Enabled = true;
            StatusLabel.Text = "Connected to the Neco Database";
            StatusProgressBar.Style = ProgressBarStyle.Continuous;

        }
        #endregion


        public static void ShowExceptionDialogue(Exception E, string Header = "", string Footer = "") {
            MessageBox.Show($"{Header}\n\n{E.Source}:{E.Message}\n\n{E.StackTrace}\n\n{Footer}", "Uh oh spaghettios"
            , MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        }

    }
}
