using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Igtampe.LandViewPlotter {
    public partial class BackgroundWorkerForm: Form {
        private BackgroundWorker MyBackgroundWorker;
        private object? Argument;

        public PictureBox ImageBox { get { return LoadIconPictureBox; } set { LoadIconPictureBox = value; } }
        public Label BigTextLabel { get { return PleaseWaitLabel; } set { PleaseWaitLabel = value; } }
        public Label StatusTextLabel { get { return StatusLabel; } set { StatusLabel = value; } }

        public BackgroundWorkerForm(BackgroundWorker BW, object? Arg = null) {
            InitializeComponent();
            MyBackgroundWorker = BW;
            Argument = Arg;
            Shown += StartBW;
            MyBackgroundWorker.RunWorkerCompleted += CloseBW;
        }

        private void StartBW(object sender, EventArgs e) {MyBackgroundWorker.RunWorkerAsync(Argument);}
        private void CloseBW(object sender, RunWorkerCompletedEventArgs e) {Close();}
    }
}
