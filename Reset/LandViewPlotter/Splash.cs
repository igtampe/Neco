using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace Igtampe.LandViewPlotter {
    public partial class Splash: Form {
        public Splash() {
            InitializeComponent();
            Icon = Properties.Resources.MainIcon;
            Shown += Splash_Shown;
            WaitAndLaunchBWorker.DoWork += WaitAndLaunchBWorker_DoWork;
            WaitAndLaunchBWorker.RunWorkerCompleted += WaitAndLaunchBWorker_RunWorkerCompleted;
        }

        private void WaitAndLaunchBWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            Hide();
            new CountryPlotter().ShowDialog();
            Close();
        }

        private void WaitAndLaunchBWorker_DoWork(object sender, DoWorkEventArgs e) {Thread.Sleep(1500);}

        private void Splash_Shown(object sender, EventArgs e) {WaitAndLaunchBWorker.RunWorkerAsync();}
    }
}
