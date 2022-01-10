using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Igtampe.Neco.Common.LandView;

namespace Igtampe.LandViewPlotter {
    public partial class CountryPicker: Form {

        private readonly List<Country> Countries;
        public Country SelectedCountry { get; private set; } = null;

        public CountryPicker(List<Country> Countries) {
            InitializeComponent();
            Icon = Properties.Resources.MainIcon;

            this.Countries = Countries;

            //Prepare the combobox
            CountriesComboBox.Items.Clear();
            
            //Add a new option
            CountriesComboBox.Items.Add("(New)");

            //Add all countries
            foreach (Country C in Countries) {
                CountriesComboBox.Items.Add($"{C.Name}: {C.Districts.Count} District(s)");
            }
        }

        private void OKClick(object sender, EventArgs e) {

            //Find the combobox selected index
            int Index = CountriesComboBox.SelectedIndex-1;

            //If Index is -2, nothing is selected, show error and return
            if (Index == -2) { MessageBox.Show("Please select an option","no",MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            //If index is -1, index was 0, and we create a new country, and return
            if (Index == -1) {
                SelectedCountry = new() { Districts = new List<District>(), Height = 0, Width = 0, PricePerSquareMeter=0, Roads = new List<Road>() };
            } else { //else we selected a country so zoop
                SelectedCountry = Countries[Index];
            }

            DialogResult = DialogResult.OK;
            Close();
            
        }
    }
}
