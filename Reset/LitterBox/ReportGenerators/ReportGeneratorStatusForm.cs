using System;
using System.Windows.Forms;

namespace Igtampe.LitterBox.ReportGenerators {
    public partial class ReportGeneratorStatusForm : Form {

        public bool Done { get; set; } = false;
        public bool CancelPending { get; private set; } = false;

        public ReportGeneratorStatusForm() => InitializeComponent();

        public void UpdateText(string Text) => StatusLabel.Text = Text;

        public void MarkDone(bool Success, string Header, string Message) {
            Done = true;
            CancelBTN.Text = "Close";
            LoadingPictureBox.Image = Success ? Properties.Resources.CheckIcon : Properties.Resources.XIcon;
            StatusLabel.Text = "Done";
            MainLabel.Text = Header;
            Text = Header;
            Submainlabel.Text = Message;
            SetProgress(0);
        }

        public void AppendToLogbox(string Text) => Logbox.AppendText($"{Environment.NewLine}{Text}");

        public void SetProgress(int Progress) {
            if (Progress == -1) {
                MainProgressBar.Value = 0;
                MainProgressBar.Style = ProgressBarStyle.Marquee;
                return;
            }

            MainProgressBar.Style = ProgressBarStyle.Continuous;
            MainProgressBar.Value = Progress;
        }

        private void StatusLabel_TextChanged(object sender, EventArgs e) => AppendToLogbox(StatusLabel.Text);

        private void CancelBTN_Click(object sender, EventArgs e) {
            if (Done) { Close(); return; }
            CancelPending = true;
            MainLabel.Text = "Cancelling...";
            Submainlabel.Text = "Calling back the Necos...";
            StatusLabel.Text = "Preparing to cancel";

        }
    }
}
