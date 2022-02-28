namespace Igtampe.Neco.V2N.Forms.Details {
    public partial class DetailsGridView : UserControl {
        public DetailsGridView(string Name, object DataSource) {
            InitializeComponent();
            DetailGroupbox.Text = Name;
            MainGridView.DataSource = DataSource;
        }
    }
}
