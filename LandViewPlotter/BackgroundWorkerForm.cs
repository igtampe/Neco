using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Igtampe.LandViewPlotter {
    public partial class BackgroundWorkerForm: Form {
        private readonly BackgroundWorker MyBackgroundWorker;
        private readonly object Argument;

        public PictureBox ImageBox { get; set; }
        public Label BigTextLabel { get; set; }
        public Label StatusTextLabel { get; set; }

        public BackgroundWorkerForm(BackgroundWorker BW, object? Arg = null) {
            InitializeComponent();
            Icon = Properties.Resources.MainIcon;
            MyBackgroundWorker = BW;
            Argument = Arg;
            Shown += StartBW;
            MyBackgroundWorker.RunWorkerCompleted += CloseBW;
        }

        private void StartBW(object sender, EventArgs e) {MyBackgroundWorker.RunWorkerAsync(Argument);}
        private void CloseBW(object sender, RunWorkerCompletedEventArgs e) {Close();}
    }
}
