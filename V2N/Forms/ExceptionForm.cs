namespace Igtampe.Neco.V2N.Forms {

    //Maybe we should package this and make it a reusable component (?)
    
    public partial class ExceptionForm : Form {
        public ExceptionForm(Exception E) {
            InitializeComponent();
            Text= E.Message;
            ExceptionTextBox.Text = E.ToString();
        }

        private void OKBTN_Click(object sender, EventArgs e) => Close();
    }
}
