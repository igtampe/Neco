using Igtampe.Neco.Common.Banking;
using Igtampe.Neco.Common.Income;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Igtampe.Neco.Common.Taxes {

    /// <summary>A report of taxes to be paid based on income earned during the current month</summary>
    public class TaxReport : AutomaticallyGeneratableIdentifiable {

        private const string SectionLine = "================================================================================\n";
        private const string ItemLine = "-------------------------------------------------------------------------------\n";

        /// <summary>Account this Tax Report belongs to</summary>
        [JsonIgnore]
        public Account? Account { get; set; }

        /// <summary>Amount of static income (income from <see cref="IncomeItem"/>s) in the report.</summary>
        public long StaticIncome { get; set; }

        /// <summary>Amount of Extra Income (income from <see cref="Transaction"/>s) in the report.</summary>
        public long ExtraIncome { get; set; }

        /// <summary>Portion of <see cref="ExtraIncome"/> that's Taxable</summary>
        public long ExtraIncomeTaxable { get; set; }

        /// <summary>Grand total of Income during this period</summary>
        public long GrandTotalIncome => ExtraIncome + StaticIncome;

        /// <summary>Grand total of tax during this period</summary>
        public long GrandTotalTax { get; set; }

        /// <summary>Report as a text document</summary>
        public string TextReport { get; set; } = "";

        /// <summary>Report as a CSV</summary>
        public string CSVReport { get; set; } = "";

        /// <summary>Date this report was generated</summary>
        public DateTime DateGenerated { get; set; } = DateTime.Now;

        /// <summary>Indicates whetehr or not this tax report is empty</summary>
        [JsonIgnore]
        [NotMapped]
        public bool IsEmpty { get; private set; } = false;

        /// <summary>Payment breakdown to pay taxes</summary>
        [NotMapped]
        [JsonIgnore]
        public Dictionary<Jurisdiction, long> TaxPaymentDictionary { get; private set; } = new();

        /// <summary>An Empty Tax Report</summary>
        public readonly static TaxReport Empty = new() {
            Account = null,
            CSVReport = "",
            DateGenerated = DateTime.Now,
            ExtraIncome = 0,
            ExtraIncomeTaxable = 0,
            StaticIncome = 0,
            TextReport = "There was no income during this period",
            GrandTotalTax = 0,
            IsEmpty = true
        };

        /// <summary>Generates a Tax Report</summary>
        /// <param name="Account">Account with all Income Items included, and Country and Jurisdiction</param>
        /// <param name="Transactions">Transactions for the tax period to calculate which should include Accounts and their owners</param>
        /// <returns></returns>
        public static TaxReport Create(Account Account, List<Transaction> Transactions) {

            List<IncomeItem> IncomeItems = Account.IncomeItems;

            if (Account.Jurisdiction == null) { throw new ArgumentException("Account district was not included"); }
            if (Transactions.Count == 0 && IncomeItems.Count == 0) { return Empty; }

            TaxReport TR = new() {
                Account= Account,  DateGenerated = DateTime.Now,
                StaticIncome = 0, ExtraIncome = 0, ExtraIncomeTaxable = 0, GrandTotalTax = 0,
                TextReport = "", CSVReport = ""
            };

            TR.TextReport = $"EZTax Tax Report\nPrepared on {DateTime.Now} for {Account.Name}\n\n";
            TR.CSVReport = $"EZTax Tax Report\nPrepared on {DateTime.Now} for {Account.Name}\n\n";

            TR.TextReport += $"{SectionLine}Transactions this month\n{SectionLine}";
            TR.CSVReport += $"ID,Name,Amount,FromBank,ToBank,Type\n";

            foreach (Transaction T in Transactions) {

                //Make sure we don't count transfers between a user's own bank accounts
                if (T.Origin is null || T.Destination is null) { throw new ArgumentException($"Origin or destination for transaction {T.ID} were not included"); }

                //If the owners for both accounts are the same, assume it's the 
                if (!T.Origin.Owners.Except(T.Destination.Owners).Any() &&T.Origin.IncomeType==IncomeType.PERSONAL && T.Destination.IncomeType==IncomeType.PERSONAL){
                    //This was a Personal transaction between two bank accounts owned by the same person or persons and is therefore ignored by the tax report generator
                    continue;
                }

                //If User received this money
                if (T.Destination.Equals(Account)) {

                    //Add it to net extra income
                    TR.ExtraIncome += T.Amount;

                    //If the origin isn't a government incometype account
                    if (T.Origin.IncomeType!=IncomeType.GOVERNMENT) {
                        TR.ExtraIncomeTaxable += T.Amount; //add the transaction's amount to the taxable extra income.

                        TR.TextReport += $"{T.Name}: {T.Amount:n0} from {T.Origin.ID} to {T.Destination.ID} (Taxable)\n{ItemLine}";
                        TR.CSVReport += string.Join(',', T.ID, T.Name, T.Amount, T.Origin.ID, T.Destination.ID, "Taxable", "\n");
                    } else {
                        TR.TextReport += $"{T.ID}: {T.Name}, {T.Amount:n0} from {T.Origin.ID} to {T.Destination.ID} (Non-Taxable)\n{ItemLine}";
                        TR.CSVReport += string.Join(',', T.ID, T.Name, T.Amount, T.Origin.ID, T.Destination.ID, "Non-Taxable", "\n");
                    }
                } else {
                    //User sent this money out

                    //If this transaction is to a nontaxable destination
                    if (T.Destination.IncomeType == IncomeType.CHARITY) {
                        //Consider this a tax deduction
                        TR.ExtraIncomeTaxable -= T.Amount;
                        TR.TextReport += $"{T.ID}: {T.Name}, {T.Amount:n0} from {T.Origin.ID} to {T.Destination.ID} (Tax-Deductible)\n{ItemLine}";
                        TR.CSVReport += string.Join(',', T.ID, T.Name, T.Amount, T.Origin.ID, T.Destination.ID, "Tax-Deductible", "\n");
                    }
                }
            }

            Dictionary<Jurisdiction,long> IncomeBreakdownDictionary = new();
            AddIncomeToBreakdownDictionary(ref IncomeBreakdownDictionary, Account.Jurisdiction, TR.ExtraIncomeTaxable);

            //Now then
            //Finish off the reports
            TR.TextReport += $"{ItemLine}TOTAL NET INCOME     : {TR.ExtraIncome:n0}\n" +
                                   $"TOTAL TAXABLE INCOME : {TR.ExtraIncomeTaxable:n0}\n{SectionLine}";
            TR.CSVReport += "\n\n";

            //Headers for the static income items
            TR.TextReport += $"\n{SectionLine}Static Income Items\n{SectionLine}";
            TR.CSVReport += "ID,Name,Federal Jurisdiction,Local Jurisdiction,Income\n";

            //Add incomes from every 
            foreach (IncomeItem I in IncomeItems) {

                if (I is Corporation C && !C.Approved) { continue; } //Check for unapproved corporations

                TR.StaticIncome += I.Income();
                if (I.Jurisdiction is null) { throw new ArgumentException($"Income Item {I.ID} did not include District"); }

                Jurisdiction Parent = AddIncomeToBreakdownDictionary(ref IncomeBreakdownDictionary, I.Jurisdiction, I.Income());

                TR.CSVReport += string.Join(',', I.ID, I.Name, Parent.Name, I.Jurisdiction.Name, I.Income(), "\n");
                TR.TextReport += $"{I.Name}:{I.Income()}\nlocated in {I.Address}, {I.Jurisdiction.Name}, {Parent.Name}\n{ItemLine}";

            }

            //Finish off the two reports static income section
            TR.TextReport += $"{ItemLine}TOTAL STATIC INCOME  : {TR.StaticIncome:n0}\n{SectionLine}";
            TR.CSVReport += "\n\n";

            TR.TextReport += $"\n{SectionLine}Breakdown:\n{SectionLine}";

            //Breakdown Section
            foreach (Jurisdiction J in IncomeBreakdownDictionary.Keys) { TR.TextReport += $"{J.Name} INCOME : {IncomeBreakdownDictionary[J]:n0}\n"; }

            TR.TextReport += $"GRAND TOTAL INCOME : {TR.GrandTotalIncome:n0}\n"; 

            TR.TextReport += $"\n{SectionLine}Taxes:\n{SectionLine}";
            TR.CSVReport += "Jurisdiction,Income,Bracket,Percent,Tax\n";

            //Lastly, let's calcualte tax
            foreach (Jurisdiction J in IncomeBreakdownDictionary.Keys) {
                (long, Bracket?) Result = J.CalculateTax(IncomeBreakdownDictionary[J], Account.IncomeType);

                TR.TaxPaymentDictionary.Add(J, Result.Item1);
                TR.CSVReport += string.Join(',', J.Name, IncomeBreakdownDictionary[J], Result.Item2 != null ? Result.Item2.Name : "NO TAX",
                                                 Result.Item2 != null ? Result.Item2.Rate : 0.0, Result.Item1, "\n");
                
                TR.TextReport += $"{J.Name} TAX : {Result.Item1:n0} ({Result.Item2?.Name} ({Result.Item2?.Rate}))\n";

                TR.GrandTotalTax += Result.Item1;
            }

            //Finalize the report
            TR.TextReport += $"GRAND TOTAL TAX : {TR.GrandTotalTax:n0}\n";
            TR.CSVReport += "\n\n";

            return TR;

        }

        /// <summary>Adds given income to given jurisdiction and ALL parent jurisdictions</summary>
        /// <param name="IBD"></param>
        /// <param name="Jurisdiction"></param>
        /// <param name="Income"></param>
        /// <returns>Parent jurisdiction</returns>
        private static Jurisdiction AddIncomeToBreakdownDictionary(ref Dictionary<Jurisdiction, long> IBD, Jurisdiction Jurisdiction, long Income) {

            Jurisdiction? J = Jurisdiction;
            if (J is null) { throw new ArgumentException("Given jurisdiction is null"); }

            do {

                if (IBD.ContainsKey(J)) { IBD[J] += Income; } 
                else { IBD.Add(J, Income); }
                if (J.ParentJurisdiction is null) { return J; }
                J = J.ParentJurisdiction;
            
            } while(J is not null);

            throw new InvalidOperationException("Something happened if we reached this part of the code. Please, send help");
        }
    }
}
