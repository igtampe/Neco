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
        BackgroundWorker MyBackgroundWorker;

        public PictureBox ImageBox { get { return LoadIconPictureBox; } set { LoadIconPictureBox = value; } }
        public Label BigTextLabel { get { return PleaseWaitLabel; } set { PleaseWaitLabel = value; } }
        public Label StatusTextLabel { get { return StatusLabel; } set { StatusLabel = value; } }

        public BackgroundWorkerForm(BackgroundWorker BW) {
            InitializeComponent();
            MyBackgroundWorker = BW;
            MyBackgroundWorker.RunWorkerCompleted += MyBackgroundWorker_RunWorkerCompleted;
            Shown += BackgroundWorkerForm_Shown;
        }

        private void BackgroundWorkerForm_Shown(object sender, EventArgs e) {MyBackgroundWorker.RunWorkerAsync();}
        private void MyBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {Close();}
    }
}
