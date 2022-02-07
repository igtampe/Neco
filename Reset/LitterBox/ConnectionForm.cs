using System;
using System.ComponentModel;
using System.Windows.Forms;
using Igtampe.Neco.Data;

namespace Igtampe.LitterBox {
    public partial class ConnectionForm: Form {

        /// <summary>Mode the context will be created in</summary>
        public NecoContextMode Mode { get; private set; } = NecoContextMode.AUTOMATIC;

        /// <summary>Return context generated from this form. Null if there was no neco context created.</summary>
        public NecoContext Context { get; private set; }

        private struct NecoContextOptions { 
            public NecoContextMode Mode { get; set; }
            public string DBURL { get; set; }
        }

        public ConnectionForm() { 
            InitializeComponent();
            ConnectAndCheckBWorker.DoWork += ConnectAndCheckBWorker_DoWork;
            ConnectAndCheckBWorker.RunWorkerCompleted += ConnectAndCheckBWorker_RunWorkerCompleted;
            Icon = Properties.Resources.MainIco;
        }

        private void ProvidersComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            switch (ProvidersComboBox.SelectedIndex) {
                case 1:
                    Mode = NecoContextMode.SQL_SERVER;
                    ConnectionStringBox.Text = "Data Source=Localhost;Initial Catalog=Neco;Integrated Security=True";
                    break;
                case 2:
                    Mode = NecoContextMode.POSTGRES;
                    ConnectionStringBox.Text = Environment.GetEnvironmentVariable("DATABASE_URL");
                    break;
                default:
                    Mode = NecoContextMode.AUTOMATIC;
                    break;
            }
        }

        private void OKButton_Click(object sender, EventArgs e) {

            NecoContextOptions Options = new() {
                DBURL = ConnectionStringBox.Text,
                Mode = Mode
            };

            new BackgroundWorkerForm(ConnectAndCheckBWorker, Options).ShowDialog();

        }

        private void ConnectAndCheckBWorker_DoWork(object sender, DoWorkEventArgs e) {
            NecoContextOptions Options = (NecoContextOptions)e.Argument;

            try {

                if (string.IsNullOrWhiteSpace(Options.DBURL)) {
                    //if there was no de-esta cosa specified, just have the NecoContext do its own thing
                    Context = new(Options.Mode);
                } else {
                    Context = new(Options.Mode, Options.DBURL);
                }

                //Now check the DB
                var TestUser = Context.User.Find("57174");
                e.Result = "nothing";
            } catch (Exception E) { e.Result = E; }

        }

        private void ConnectAndCheckBWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (e.Result is Exception) {
                var E = e.Result as Exception;
                //deal with the exception
                MessageBox.Show($"There was an error setting up the Neco Context:\n\n{E.Source}:{E.Message}\n\n{E.StackTrace}","Uh oh spaghettios"
                    ,MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);

                Context.Dispose();
                Context = null;

                //Return
                return;
            }

            DialogResult = DialogResult.OK;
            Close();

        }
    }
}
