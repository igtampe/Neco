namespace Igtampe.Neco.V2N.Forms {
    public partial class ProgressForm : Form {

        /// <summary>Updates the  status label and appends the current status to the detailsbox</summary>
        /// <param name="Status"></param>
        public void UpdateStatus(string Status) {
            StatusLabel.Text = Status;
            DetailsBox.AppendText($"[{DateTime.Now:HH:mm:ss:FFFF}] {Status}" + Environment.NewLine);
        }

        /// <summary>Sets the percentage of the mainprogressbar. Can handle double percentages as 0.0-1.0 and 0.0-100.0 Values under 0.0 will set the progress bar to marquee</summary>
        /// <param name="Percentage"></param>
        public void SetPercentage(double Percentage) => SetPercentage(Convert.ToInt32(Percentage > 0 && Percentage < 1 ? Percentage * 100.0 : Percentage));

        /// <summary>Sets the percentage of the mainproressbar. Value should be between 0 and 100. Values under 0 will be assumed to be to set the progress bar to marquee.</summary>
        /// <param name="Percentage"></param>
        public void SetPercentage(int Percentage) {
            MainProgressBar.Style = Percentage < 0 ? ProgressBarStyle.Marquee : ProgressBarStyle.Continuous;
            MainProgressBar.Value = Math.Max(0,Math.Min(100,Percentage));
        }

        /// <summary>Updates the loading image</summary>
        /// <param name="Image"></param>
        public void SetPicture(Image Image) => NecoPictureBox.Image = Image;

        /// <summary>Updates the text in the main title label</summary>
        /// <param name="Text"></param>
        public void SetTitleLabel(string Text) => SitTightLabel.Text = Text;

        /// <summary>Updates the text in the description label</summary>
        /// <param name="Text"></param>
        public void SetDescriptionLabel(string Text) => NecosDispatchedLabel.Text = Text;

        public ProgressForm() => InitializeComponent();

        //I love small functions. Don't you?
        private void DetailsBTN_Click(object sender, EventArgs e) => Size = Size == MinimumSize ? MaximumSize : MinimumSize;
    }
}
