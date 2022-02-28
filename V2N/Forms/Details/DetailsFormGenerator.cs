namespace Igtampe.Neco.V2N.Forms.Details {
    public static class DetailsFormGenerator {

        public static DetailsForm GenerateDetailsForm(ViBE.User VUser) {

            //Generate a Dictionary for the properties we want to expose
            Dictionary<string, string> Details = new() {
                { "PIN", $"{VUser.Pin}" },
                { "UMSNB", $"{VUser.UMSNB ?? 0  :n0}p" },
                { "GBANK", $"{VUser.GBANK ?? 0:n0}p" },
                { "RIVER", $"{VUser.RIVER ?? 0:n0}p" },
                { "TotalWealth", $"{VUser.TotalWealth:n0}p" },
                { "IncomeItem(s)", $"{VUser.IncomeItems.Count:n0}" },
            };

            //Generate a dictionary of the datasources we want
            Dictionary<string, object> DataSources = new() {
                { "Income Items", VUser.IncomeItems },
            };

            //Return the thing:
            return new($"{VUser.Name} ({VUser.ID})", $"{(VUser.IsGov ? "Government Account" : VUser.IsCorp ? "Corporate Account" : "Personal Account")}", Details, DataSources) { Icon = Properties.Resources.V2N };

        }

        public static DetailsForm GenerateDetailsForm(Common.User NUser) {
            //Generate a Dictionary for the properties we want to expose
            Dictionary<string, string> Details = new() {
                { "Password", $"{NUser.Password}" },
            };

            //Generate a dictionary of the datasources we want
            Dictionary<string, object> DataSources = new() {
                { "Accounts", NUser.Accounts },
                { "Income Items on first account", NUser.Accounts[0].IncomeItems }
            };

            //Return the thing:
            return new($"{NUser.Name} ({NUser.ID})", $"{(NUser.IsGov ? "Government Account" : "Standard Account")}", Details, DataSources) { Icon = Properties.Resources.V2N};

        }
    }
}
