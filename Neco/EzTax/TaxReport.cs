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
            TR.PaymentBreakdownDictionary = new();

            //I would use Linq here to calculate everything but we're going to need to go throug the entire thing anyway so we may as well

            //TODO: Prepare both reports at the same time

            foreach (Transaction T in CurrentMonthTransactions) {
                //If User received this money
                if (T.ToAccount.Owner.ID == User.ID) {
                    
                    //Add it to net extra income
                    TR.ExtraIncome += T.Amount;

                    //If the origin isn't nontaxableOrigin
                    if (T.FromAccount.Owner.Type.Taxation != TaxationType.NontaxableOrigin) {
                        TR.ExtraIncomeTaxable += T.Amount; //add the transaction's amount to the taxable extra income.
                    }
                } else {
                    //User sent this money out

                    //If this transaction is to a nontaxable destination
                    if (T.ToAccount.Owner.Type.Taxation == TaxationType.NonTaxableDestination) {
                        //Consider this a tax deduction
                        TR.ExtraIncomeTaxable -= T.Amount;
                    }
                }
            }

            TR.IncomeBreakdownDictionary.Add(UserFederalJurisdiction, TR.ExtraIncome);

            //Now then,
            
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

            }

            //Lastly, let's calcualte tax
            foreach (TaxJurisdiction J in TR.IncomeBreakdownDictionary.Keys) {
                (long, TaxBracket) Result = J.CalculateTax(User, TR.IncomeBreakdownDictionary[J]);
                TR.PaymentBreakdownDictionary.Add(J, Result.Item1); //Split here for the report TODO
                TR.GrandTotalTax += Result.Item1;
            }

            //Finalize the report
            

            return TR;
        }

    }
}
