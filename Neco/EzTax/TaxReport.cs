using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Igtampe.Neco.Common.EzTax {

    /// <summary>Holds a Tax Report </summary>
    public class TaxReport {

        private const string SectionLine = "================================================================================\n";
        private const string ItemLine = "-------------------------------------------------------------------------------\n";

        /// <summary>ID of this Tax Report</summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid ID { get; set; }

        /// <summary>User this Report belongs to</summary>
        [JsonIgnore]
        public User Owner { get; set; }

        /// <summary>Date at which this tax report was prepared</summary>
        public DateTime PreparedDate { get; set; }

        /// <summary>Amount of static income (income from <see cref="IncomeItem"/>s) in the report.</summary>
        public long StaticIncome { get; set; }

        /// <summary>Amount of Extra Income (income from <see cref="Transaction"/>s) in the report.</summary>
        public long ExtraIncome { get; set; }

        /// <summary>Portion of <see cref="ExtraIncome"/> that's Taxable</summary>
        public long ExtraIncomeTaxable { get; set; }

        /// <summary>Grand total of Income during this period</summary>
        public long GrandTotalIncome { get { return ExtraIncome + StaticIncome; } }

        /// <summary>Grand total of tax during this period</summary>
        public long GrandTotalTax { get; set; }

        /// <summary>Actual Tax Report Text</summary>
        public string Report { get; set; }

        /// <summary>Tax Report text presented in CSV format for exporting</summary>
        public string CSVReport { get; set; }

        /// <summary>Dictionary of Tax Payment Breakdown</summary>
        [JsonIgnore]
        [NotMapped]
        public Dictionary<TaxJurisdiction, long> PaymentBreakdownDictionary { get; private set; }

        /// <summary>Dictionary of Income Breakdown</summary>
        [JsonIgnore]
        [NotMapped]
        public Dictionary<TaxJurisdiction, long> IncomeBreakdownDictionary { get; private set; }


        /// <summary>Generates a Tax Report object based on a User's income items and monthly transactions</summary>
        /// <param name="User"></param>
        /// <param name="IncomeItems"></param>
        /// <param name="CurrentMonthTransactions"></param>
        /// <param name="UserFederalJurisdiction"></param>
        /// <returns></returns>
        public static TaxReport GenerateTaxReport(User User, ICollection<IncomeItem> IncomeItems, ICollection<Transaction> CurrentMonthTransactions, TaxJurisdiction UserFederalJurisdiction) {

            TaxReport TR = new();

            //Initialize the vars
            TR.PreparedDate = DateTime.Now;
            TR.StaticIncome = 0;
            TR.ExtraIncome = 0;
            TR.ExtraIncomeTaxable = 0;
            TR.GrandTotalTax = 0;
            TR.Report = "";
            TR.CSVReport = "";
            TR.PaymentBreakdownDictionary = new();

            //I would use Linq here to calculate everything but we're going to need to go throug the entire thing anyway so we may as well

            //Report Headers
            TR.Report = $"EZTax Tax Report\nPrepared on {DateTime.Now} for {User.Name}\n\n";
            TR.CSVReport = $"EZTax Tax Report\nPrepared on {DateTime.Now} for {User.Name}\n\n";

            //Transaction Headers
            TR.Report += $"{SectionLine}Transactions this month\n{SectionLine}";
            TR.CSVReport += $"ID,Name,Amount,FromBank,ToBank,Type\n";

#pragma warning disable S1643 // I Really do not understand SolarLint's problem with + on a string in loops. It's probably waranted but I don't want to deal with it.

            foreach (Transaction T in CurrentMonthTransactions) {

                //Make sure we don't count transfers between a user's own bank accounts
                if (T.FromAccount.Owner.ID == T.ToAccount.Owner.ID) { continue; }

                //If User received this money
                if (T.ToAccount.Owner.ID == User.ID) {
                    
                    //Add it to net extra income
                    TR.ExtraIncome += T.Amount;

                    //If the origin isn't nontaxableOrigin
                    if (T.FromAccount.Owner.Type.Taxation != TaxationType.NontaxableOrigin) {
                        TR.ExtraIncomeTaxable += T.Amount; //add the transaction's amount to the taxable extra income.

                        TR.Report += $"{T.ID}: {T.Name}, {T.Amount:n0} from {T.FromAccount.ID} to {T.ToAccount.ID} (Taxable)\n{ItemLine}";
                        TR.CSVReport += string.Join(',', T.ID, T.Name, T.Amount, T.FromAccount.ID, T.ToAccount.ID, "Taxable", "\n");
                    } else {
                        TR.Report += $"{T.ID}: {T.Name}, {T.Amount:n0} from {T.FromAccount.ID} to {T.ToAccount.ID} (Non-Taxable)\n{ItemLine}";
                        TR.CSVReport += string.Join(',', T.ID, T.Name, T.Amount, T.FromAccount.ID, T.ToAccount.ID, "Non-Taxable", "\n");
                    }

                } else {
                    //User sent this money out

                    //If this transaction is to a nontaxable destination
                    if (T.ToAccount.Owner.Type.Taxation == TaxationType.NonTaxableDestination) {
                        //Consider this a tax deduction
                        TR.ExtraIncomeTaxable -= T.Amount;
                        TR.Report += $"{T.ID}: {T.Name}, {T.Amount:n0} from {T.FromAccount.ID} to {T.ToAccount.ID} (Tax-Deductible)\n{ItemLine}";
                        TR.CSVReport += string.Join(',', T.ID, T.Name, T.Amount, T.FromAccount.ID, T.ToAccount.ID, "Tax-Deductible", "\n");
                    }
                }
            }

            TR.IncomeBreakdownDictionary.Add(UserFederalJurisdiction, TR.ExtraIncome);

            //Finish off the reports
            TR.Report += $"{ItemLine}TOTAL NET INCOME     : {TR.ExtraIncome:n0}\n" +
                                   $"TOTAL TAXABLE INCOME : {TR.ExtraIncomeTaxable:n0}\n{SectionLine}";
            TR.CSVReport += "\n\n";


            //Headers for the static income items
            TR.Report += $"\n{SectionLine}Static Income Items\n{SectionLine}";
            TR.CSVReport += "ID,Name,Federal Jurisdiction,Local Jurisdiction,Income";

            //Add incomes from every 
            foreach (IncomeItem I in IncomeItems) {

                TR.StaticIncome += I.TotalMonthlyIncome();
                //Add to local jurisdiction
                if (!TR.IncomeBreakdownDictionary.ContainsKey(I.LocalJurisdiction)) {
                    TR.IncomeBreakdownDictionary.Add(I.LocalJurisdiction, I.TotalMonthlyIncome());
                } else {
                    TR.IncomeBreakdownDictionary[I.LocalJurisdiction] += I.TotalMonthlyIncome();
                }

                //Add to federal jurisdiction
                if (!TR.IncomeBreakdownDictionary.ContainsKey(I.FederalJurisdiction)) {
                    TR.IncomeBreakdownDictionary.Add(I.FederalJurisdiction, I.TotalMonthlyIncome());
                } else {
                    TR.IncomeBreakdownDictionary[I.FederalJurisdiction] += I.TotalMonthlyIncome();
                }

                TR.CSVReport += string.Join(',', I.ID, I.Name, I.FederalJurisdiction.Name, I.LocalJurisdiction.Name, I.TotalMonthlyIncome(),"\n");
                TR.Report += $"{I.ID}: {I.Name} located in {I.LocalJurisdiction.Name},{I.FederalJurisdiction.Name}\n";

                if (I.Subitems.Count > 0) {
                    TR.Report += "\nSubitems:\n\n";
                    foreach (var S in I.Subitems) {
                        TR.Report += $"{S.ID}: {S.Name} with income {S.Income():n0}\n";
                    }
                }

                TR.Report += $"\nMisc Income: {I.MiscIncome:n0}\n\nTOTAL INCOME: {I.TotalMonthlyIncome():n0}\n{ItemLine}";

            }

            //Finish off the two reports static income section
            TR.Report += $"{ItemLine}TOTAL STATIC INCOME  : {TR.StaticIncome:n0}\n{SectionLine}";
            TR.CSVReport += "\n\n";

            TR.Report += $"\n\nBreakdown:\n\n";

            //Breakdown Section
            foreach (TaxJurisdiction J in TR.IncomeBreakdownDictionary.Keys) {
                TR.Report += $"{J.Name} INCOME : {TR.IncomeBreakdownDictionary[J]:n0}\n";
            }

            TR.Report += $"GRAND TOTAL INCOME : {TR.GrandTotalIncome:n0}\n";


            TR.Report += $"\n\nTaxes:\n\n";
            TR.CSVReport += "Jurisdiction,Income,Bracket,Percent,Tax\n";

            //Lastly, let's calcualte tax
            foreach (TaxJurisdiction J in TR.IncomeBreakdownDictionary.Keys) {
                (long, TaxBracket) Result = J.CalculateTax(User, TR.IncomeBreakdownDictionary[J]);
                TR.PaymentBreakdownDictionary.Add(J, Result.Item1); //Split here for the report

                TR.CSVReport += string.Join(',', J.Name, TR.IncomeBreakdownDictionary[J],Result.Item2.Name,Result.Item2.Rate,Result.Item1, "\n");
                TR.Report += $"{J.Name} TAX : {Result.Item1:n0} ({Result.Item2.Name} ({Result.Item2.Rate}))\n";

                TR.GrandTotalTax += Result.Item1;
            }

            //Finalize the report
            TR.Report += $"GRAND TOTAL TAX : {TR.GrandTotalTax:n0}\n";
            TR.CSVReport += "\n\n";

#pragma warning restore S1643 // Strings should not be concatenated using '+' in a loop


            return TR;
        }


        /// <summary>Compares this Tax Report to another object</summary>
        /// <param name="obj"></param>
        /// <returns>True if and only if the object is a Tax Report and the <see cref="ID"/> matches with this one's</returns>
        public override bool Equals(object obj) {
            if (obj is TaxReport C) { return C.ID == ID; }
            return false;
        }

        /// <summary>Gets a hash code for this Tax Report. Delegates to <see cref="ID"/></summary>
        /// <returns></returns>
        public override int GetHashCode() { return ID.GetHashCode(); }

        /// <summary>Creates a string representation of this Tax Report</summary>
        /// <returns>{ID} : {PreparedDate} {GrandTotalIncome:n0} {GrandTotalTax:n0}</returns>
        public override string ToString() { return $"{ID} : {PreparedDate} {GrandTotalIncome:n0} {GrandTotalTax:n0}"; }

    }
}
