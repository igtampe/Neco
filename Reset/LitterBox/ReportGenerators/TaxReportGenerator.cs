using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Igtampe.Neco.Common.EzTax;
using Igtampe.Neco.Common;
using Igtampe.Neco.Data;
using Microsoft.EntityFrameworkCore;

namespace Igtampe.LitterBox.ReportGenerators {
    public class TaxReportGenerator {

        public static async Task<TaxReport> ReportGenerator(NecoContext NecoDB, User U) {

            ICollection<IncomeItem> Is = await NecoDB.IncomeItem
                .Include(m => m.User)
                .Include(m => m.FederalJurisdiction).ThenInclude(m => m.Account)
                .Include(m => m.FederalJurisdiction).ThenInclude(m => m.Brackets)
                .Include(m => m.LocalJurisdiction).ThenInclude(m => m.Account)
                .Include(m => m.LocalJurisdiction).ThenInclude(m => m.Brackets)
                .Include(m => m.Apartments)
                .Include(m => m.Hotels)
                .Include(m => m.Businesses)
                .Where(i => i.User.ID == U.ID)
                .ToListAsync();
            ICollection<Transaction> Ts;

            if (DateTime.Now.Day > 15) {

                //Get Transactions since this month's 15th.
                Ts = await NecoDB.Transaction
                    .Include(T => T.FromAccount).ThenInclude(A => A.Owner).ThenInclude(U => U.Type)
                    .Include(T => T.ToAccount).ThenInclude(A => A.Owner).ThenInclude(U => U.Type)
                    .Where(T => T.Time > DayOfThisMonth(15) && T.State == TransactionState.COMPLETED
                                && T.FromAccount.ID != "999999999")
                    .ToListAsync();

            } else {
                //Get Transactions from the last 15th to this 15th

                Ts = await NecoDB.Transaction
                    .Include(T => T.FromAccount).ThenInclude(A => A.Owner).ThenInclude(U => U.Type)
                    .Include(T => T.ToAccount).ThenInclude(A => A.Owner).ThenInclude(U => U.Type)
                    .Where(T => T.Time > DayOfLastMonth(15) && T.Time < DayOfThisMonth(15)
                                && T.State == TransactionState.COMPLETED && T.FromAccount.ID != "999999999")
                    .ToListAsync();

            }

            //Right now we have no real way to save a User's federal jurisdiction.
            //So we're going to assume there's only one federal jurisdiction and every income item has the same one
            //We can fix this later :shrug:
            TaxJurisdiction J = (await NecoDB.IncomeItem.Include(I => I.FederalJurisdiction).ThenInclude(J => J.Brackets).FirstAsync()).FederalJurisdiction;
            TaxReport R = await Task.Run(() => TaxReport.GenerateTaxReport(U, Is, Ts, J));

            return R;
        }

        /// <summary>Gets a DateTime representing the specified day of last month (IE if it's currently December 5th, executing
        /// this function with param day=15, it would return November 15th). Use day 31 to get the last day of the last month.</summary>
        /// <param name="Day"></param>
        /// <returns></returns>
        private static DateTime DayOfLastMonth(int Day) {

            int LastMonth = DateTime.Now.Month - 1;
            int Year = DateTime.Now.Year;
            if (LastMonth == 0) {
                Year--;
                LastMonth = 12;
            }

            //The one condition this may fail in is if we ask for the last 31st and last month didn't have a 31st.
            //In this and similar instances (if the previous month does not have this date), let's use the month's maximum length.

            return new DateTime(Year, LastMonth, Math.Min(Day, DateTime.DaysInMonth(Year, LastMonth)));
        }

        /// <summary>Gets a DateTime representing the specified day of this month (IE if it's currently December 5th, executing
        /// this function with param day=15, it would return December 15th). Use day 31 to get the last day of this month.</summary>
        /// <param name="Day"></param>
        /// <returns></returns>
        private static DateTime DayOfThisMonth(int Day) {
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, Math.Min(Day, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)));
        }

    }
}
