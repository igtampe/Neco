using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Igtampe.TinyForms {

    /// <summary>A form with a caption, and a textbox</summary>
    public partial class DropdownForm<T>:Form {

        /// <summary>Return text for this Dropdown form</summary>
        public string ReturnText => TheBox.Text;

        /// <summary>Index of the selected item in the dropdown</summary>
        public int ReturnIndex => TheBox.SelectedIndex;

        
        /// <summary>Creates a Mini dropdown (AKA ComboBox) form</summary>
        /// <param name="caption">Caption of the Form</param>
        /// <param name="title">Title of the form</param>
        /// <param name="Data">Data to display of type T</param>
        /// <param name="PropName">Name of the property to extract from each item of type T in the data list (IE T.Name). Leave blank to just toString() the data</param>
        public DropdownForm(string caption, string title, List<T> Data, string PropName="") {
            InitializeComponent();
            TheLabel.Text = caption;
            Text = title;

            TheBox.Items.Clear();

            if (string.IsNullOrWhiteSpace(PropName)) {
                foreach (T D in Data) { TheBox.Items.Add(D.ToString()); }
            } else {
                foreach (T D in Data) { TheBox.Items.Add((typeof(T)).GetProperty(PropName).GetValue(D).ToString()); }
            }
            
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
