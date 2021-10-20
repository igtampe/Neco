using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Igtampe.Neco.Data;
using Igtampe.Neco.Common;
using Igtampe.Neco.Common.EzTax;

namespace Igtampe.LitterBox {
    public partial class LaunchForm: Form {

        private NecoContext NecoDB;

        public LaunchForm() {
            InitializeComponent();
            FormClosing += LaunchForm_FormClosing;
            MainPannel.Enabled = false;
            Icon = Properties.Resources.MainIco;
            Shown += LaunchForm_Shown;
        }

        private void LaunchForm_Shown(object sender, EventArgs e) {connectDisconnectToolStripMenuItem_Click(sender, e); }

        private void connectDisconnectToolStripMenuItem_Click(object sender, EventArgs e) {
            if (NecoDB != null) {
                NecoDB.Dispose();
                connectDisconnectToolStripMenuItem.Text = "&Connect";
                NecoDB = null;
                MainPannel.Enabled = false;
                return;
            }

            ConnectionForm F = new ConnectionForm();
            F.ShowDialog();
            if (F.Context != null) {

                //OK we have a de-esta cosa
                NecoDB = F.Context;
                connectDisconnectToolStripMenuItem.Text = "&Disconnect";
                MainPannel.Enabled = true;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Close();
        }

        private void LaunchForm_FormClosing(object sender, FormClosingEventArgs e) {
            NecoDB?.Dispose(); //Just in case
        }

    }
}
