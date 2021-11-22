using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Igtampe.Neco.Common;
using Igtampe.TinyForms;

namespace Igtampe.LitterBox.ItemForms
{
    public partial class UserTypesForm : Form {

        public List<UserType> UserTypes { get; private set; }

        public UserTypesForm(ref List<UserType> UTs) {
            InitializeComponent();

            UserTypeNameBox.LostFocus += EditLostFocus;

            UserTypes = UTs;
            UserTypeDetailsGroupBox.Enabled = false;
            PopulateListView();
        }

        private void EditLostFocus(object sender, EventArgs e) {
            if (UserTypesListview.SelectedIndices.Count < 1) { return; }
            int index = UserTypesListview.SelectedIndices[0];
            UserTypeDetailsGroupBox.Enabled = false;
            PopulateListView();
            UserTypesListview.SelectedIndices.Add(index);
        }

        public void PopulateListView() { 
        
            UserTypesListview.Items.Clear();
            foreach (UserType t in UserTypes) {
                ListViewItem LV = new(t.Name);
                LV.SubItems.Add(Enum.GetName(t.Taxation));
                LV.SubItems.Add(t.UserOpenable ? "Yes" : "No");
                UserTypesListview.Items.Add(LV);
            }

        }

        private void UserTypesListview_SelectedIndexChanged(object sender, EventArgs e) {
            if (UserTypesListview.SelectedIndices.Count < 1) { return; }
            UserTypeDetailsGroupBox.Enabled = true;
            int index = UserTypesListview.SelectedIndices[0];
            UserTypeNameBox.Text = UserTypes[index].Name;
            UserTypeTaxationTypeComboBox.SelectedIndex = (int)UserTypes[index].Taxation;
            UserTypeUserOpenableCheckbox.Checked = UserTypes[index].UserOpenable;
        }

        private void UserTypeNameBox_TextChanged(object sender, EventArgs e) {
            if (UserTypesListview.SelectedIndices.Count < 1) { return; }
            int index = UserTypesListview.SelectedIndices[0];
            UserTypes[index].Name = UserTypeNameBox.Text;
        }

        private void UserTypeTaxationTypeComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (UserTypesListview.SelectedIndices.Count < 1) { return; }
            int index = UserTypesListview.SelectedIndices[0];
            UserTypes[index].Taxation = (TaxationType)UserTypeTaxationTypeComboBox.SelectedIndex;

        }

        private void UserTypeUserOpenableCheckbox_CheckedChanged(object sender, EventArgs e) {
            if (UserTypesListview.SelectedIndices.Count < 1) { return; }
            int index = UserTypesListview.SelectedIndices[0];
            UserTypes[index].UserOpenable = UserTypeUserOpenableCheckbox.Checked;
        }

        private void OKButton_Click(object sender, EventArgs e) { Close(); }

        private void newToolStripMenuItem_Click(object sender, EventArgs e) {
            AreYouSure.Ask("create a new user type (This is permanent)");
            UserTypes.Add(new() { Name = "New", Taxation = TaxationType.Taxable, UserOpenable = true });
            PopulateListView();
        }
    }
}
