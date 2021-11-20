using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Igtampe.Neco.Common.EzTax;
using Igtampe.Neco.Common;

namespace Igtampe.LitterBox.ItemForms {
    public partial class TaxBracketEditor : Form {

        public TaxBracket Bracket { get; set; }

        private List<UserType> Types;

        public TaxBracketEditor(TaxBracket Bracket, List<UserType> Types) {
            InitializeComponent();
            this.Bracket = Bracket;
            this.Types = Types;

            //Setup the de-esta cosas
            BracketStartUpDown.Maximum = long.MaxValue;
            BracketEndUpDown.Maximum = long.MaxValue;

            //Now lets ensure
            UpdateBracketMinMax();

            //Populate the combobox
            Types.ForEach(T => UserTypeComboBox.Items.Add(T.Name)); //Beatufiul

            //Populate the data
            NameBox.Text= Bracket.Name;
            InterestRateUpDown.Value = Convert.ToDecimal(Bracket.Rate*100);
            UserTypeComboBox.Text = Bracket.Type?.Name;
            BracketEndUpDown.Value = Bracket.End;
            BracketStartUpDown.Value = Bracket.Start;

        }

        private void OKBTN_Click(object sender, EventArgs e) {
            Bracket.Name = NameBox.Text;
            Bracket.Rate = Convert.ToDouble(InterestRateUpDown.Value) / 100.0;
            Bracket.Start = Convert.ToInt64(BracketStartUpDown.Value);
            Bracket.End = Convert.ToInt64(BracketEndUpDown.Value);
            Bracket.Type = UserTypeComboBox.SelectedIndex == -1 ? Bracket.Type : Types[UserTypeComboBox.SelectedIndex];
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelBTN_Click(object sender, EventArgs e) { Close(); }

        private void BracketStartUpDown_ValueChanged(object sender, EventArgs e) { UpdateBracketMinMax(); }
        private void BracketEndUpDown_ValueChanged(object sender, EventArgs e) { UpdateBracketMinMax(); }

        private void UpdateBracketMinMax() {
            BracketEndUpDown.Minimum = BracketStartUpDown.Value;
            BracketStartUpDown.Maximum = BracketEndUpDown.Value;
        }

    }
}
