﻿using System;
using System.Windows.Forms;

namespace Igtampe.TinyForms {

    /// <summary>Displays a large body of text</summary>
    public partial class BigTextForm : Form {

        /// <summary>Text to Display</summary>
        public string DisplayText { get { return DisplayBox.Text;  } set { DisplayBox.Text = value; } }

        public BigTextForm() {InitializeComponent();}

        private void button1_Click(object sender, EventArgs e) { Close(); }
    }
}
