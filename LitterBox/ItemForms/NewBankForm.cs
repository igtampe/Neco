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
    public partial class NewBankForm : Form {

        public Bank ReturnBank { get; set; }

        public NewBankForm() { InitializeComponent(); }

        private void CancelBTN_Click(object sender, EventArgs e) { Close(); }

        private void OKBTN_Click(object sender, EventArgs e) {
            if (IDBox.Text.Length != 5) {
                MessageBox.Show("ID of Bank must be 5 characters","No",MessageBoxButtons.OK,MessageBoxIcon.Hand); //When did MessageboxIcon Hand get added?
                return;
            }

            if (string.IsNullOrWhiteSpace(IDBox.Text) || string.IsNullOrWhiteSpace(NameBox.Text)) {
                MessageBox.Show("Make sure to fill in all details pls", "No", MessageBoxButtons.OK, MessageBoxIcon.Hand); //When did MessageboxIcon Hand get added?
                return;
            }

            ReturnBank = new() {
                ID = IDBox.Text,
                Name = NameBox.Text,
                Accounts = new(),
                AccountTypes = new()
            };

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
