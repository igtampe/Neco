namespace Igtampe.Neco.V2N.Forms.Details {
    public partial class DetailsForm : Form {

        public DetailsForm(string Title, string Description, Dictionary<string,string> Details, Dictionary<string,object> DataSources) {
            InitializeComponent();

            Text = Title;
            MainLabel.Text = Title;
            DetailsLabel.Text = Description;

            //Convert the details dictionary to the details pane
            DetailsTableLayoutPanel.RowCount = Details.Count+1;
            DetailsTableLayoutPanel.RowStyles.Clear();

            int CurPos=0;

            foreach (var item in Details) {

                Label ItemNameLabel = new() {
                    Text = item.Key,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleRight,
                    Margin = new(5)
                };
                
                
                Label ItemValueLabel = new() {
                    Text = item.Value,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Margin = new(5)
                };

                DetailsTableLayoutPanel.Controls.Add(ItemNameLabel, 0, CurPos);
                DetailsTableLayoutPanel.Controls.Add(ItemValueLabel, 1, CurPos);
                DetailsTableLayoutPanel.RowStyles.Add(new());
                CurPos++;

            }

            DetailsTableLayoutPanel.RowStyles.Add(new(SizeType.Percent, 100F));

            if (DataSources.Count == 0) {

                //MOVE THE OK BUTTON
                MainTableLayoutPanel.Controls.Add(OKBTN, 0, 4);
                MainTableLayoutPanel.ColumnCount = 1;
                return;
            }

            //Else we gotta map out some de-esta cosas:
            DetailsTableLayoutPanel.RowCount = DataSources.Count;
            DetailsTableLayoutPanel.RowStyles.Clear();

            //Determine the percentage of everything
            float PerDatagrid = 100F / DataSources.Count;
            CurPos = 0;

            //now do it
            foreach (var Source in DataSources) {
                DetailGridViewsTableLayoutPanel.RowStyles.Add(new(SizeType.Percent, PerDatagrid));
                DetailsGridView D = new(Source.Key, Source.Value) { 
                    Dock = DockStyle.Fill,
                };
                DetailGridViewsTableLayoutPanel.Controls.Add(D, 0, CurPos);
                CurPos++;
            }

            //this.MainTableLayoutPanel.SetColumnSpan(this.MainLabel, 2); //this may not be necessary but it also might so imma leave this here

        }

        private void OKBTN_Click(object sender, EventArgs e) => Close();
    }
}
