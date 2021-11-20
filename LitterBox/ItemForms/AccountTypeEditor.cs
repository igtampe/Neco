using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Igtampe.Neco.Common;

namespace Igtampe.LitterBox.ItemForms {
    public partial class AccountTypeEditor : Form {

        public BankAccountType Type { get; set; }

        public AccountTypeEditor(BankAccountType Type) {
            InitializeComponent();
            this.Type = Type;
            NameBox.Text = Type.Name;
            InterestRateUpDown.Value = Convert.ToDecimal(Type.InterestRate * 100);
        }

        private void OKBTN_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Type.Name = NameBox.Text;
            Type.InterestRate = Convert.ToDouble(InterestRateUpDown.Value) / 100.0;
            Close();
        }

        private void CancelBTN_Click(object sender, EventArgs e) { Close(); }

        
    }
}
