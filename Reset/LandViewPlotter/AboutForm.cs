using System;
using System.Windows.Forms;

namespace Igtampe.LandViewPlotter {
    public partial class AboutForm: Form {
        public AboutForm() { InitializeComponent(); Icon = Properties.Resources.MainIcon; }        
        private void OkButton_Click(object sender, EventArgs e) {Close();}
    }
}
