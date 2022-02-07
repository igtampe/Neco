using System;
using System.Windows.Forms;
using Igtampe.Neco.Common;

namespace Igtampe.LitterBox.ItemForms {
    public partial class AccountTypeEditor : Form {

        public BankAccountType Type { get; set; }

        public AccountTypeEditor(BankAccountType Type) {
            InitializeComponent();
            this.Type = Type;
            NameBox.Text = Type.Name;
            InterestRateUpDown.Value = (decimal)Type.InterestRate * 100;
        }

        private void OKBTN_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Type.Name = NameBox.Text;
            Type.InterestRate = decimal.ToDouble(InterestRateUpDown.Value) / 100.0;
            Close();
        }

        private void CancelBTN_Click(object sender, EventArgs e) { Close(); }        
    }
}
