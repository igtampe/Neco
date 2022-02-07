using System.Drawing;
using System.Windows.Forms;

namespace Igtampe.LandViewPlotter {

    /// <summary>Previews an image</summary>
    public partial class PreviewForm:Form {

        /// <summary>Creates a preview form to show an image (but without an image)</summary>
        public PreviewForm() { 
            InitializeComponent();
            Icon = Properties.Resources.MainIcon;
        }

        /// <summary>Creates a preview form to show an image (with an image)</summary>
        /// <param name="I"></param>
        public PreviewForm(Image I) : this() { SetImage(I); }

        /// <summary>Sets the image in this previewform</summary>
        /// <param name="I"></param>
        public void SetImage(Image I) {
            ImageBox.Image = I;
            Size = new Size(I.Width,I.Height);
        }

        /// <summary>Copies the image to the clipboard</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyToolStripMenuItem_Click(object sender,System.EventArgs e) { Clipboard.SetImage(ImageBox.Image); }

        /// <summary>Saves Image to Disk</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveToolStripMenuItem_Click(object sender,System.EventArgs e) {
            if(ExportFileDialog.ShowDialog() == DialogResult.OK) { ImageBox.Image.Save(ExportFileDialog.FileName,System.Drawing.Imaging.ImageFormat.Png); }
        }

        /// <summary>Close the preview picturebox if the image is clicked</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageBox_Click(object sender,System.EventArgs e) { Close(); }
    }
}
