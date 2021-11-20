using System;
using System.Windows.Forms;

namespace Igtampe.TinyForms {

    /// <summary>A form with a caption, and a textbox</summary>
    public partial class TextboxForm:Form {

        /// <summary>Return text for this textbox form</summary>
        public string ReturnText => TheBox.Text;

        /// <summary>Creates a mini textbox form</summary>
        /// <param name="caption"></param>
        /// <param name="title"></param>
        public TextboxForm(string caption, string title) {
            InitializeComponent();
            TheLabel.Text = caption;
            Text = title;
        }

        private void OKBtn_Click(object sender,EventArgs e) {
            DialogResult = DialogResult.OK;
            return;
        }

        private void CancelBTN_Click(object sender,EventArgs e) {
            DialogResult = DialogResult.Cancel;
            return;
        }
    }
}
