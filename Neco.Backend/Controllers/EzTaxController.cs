using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Igtampe.Neco.Common.EzTax;
using Igtampe.Neco.Common.EzTax.Requests;
using Igtampe.Neco.Common.EzTax.Subitems;
using Igtampe.Neco.Common;
using Igtampe.Neco.Data;

namespace Igtampe.Neco.Backend.Controllers {

    [Route("EzTax")]
    [ApiController]
    public class EzTaxController: Controller {
        private readonly NecoContext NecoDB;

        /// <summary>IMEX's a</summary>
        public static readonly BankAccount THE_PEOPLE = new() {
            ID = "999999999",
            Bank = null,
            Closed = false,
            Details = null,
            Type = null,
            Owner = new() {
                ID="99999",
                Accounts = null,
                Type = new() {
                    Name = "Citizens",
                    Taxation = TaxationType.NontaxableOrigin
                },
                Name = "Citizens of the World"
            }
        };

        public EzTaxController(NecoContext context) { NecoDB = context; }

        #region TaxJurisdiction
        // GET: UMSAT
        [HttpGet("TaxJurisdiction")]
        public async Task<IActionResult> TaxJurisdictionIndex() { return Ok(await NecoDB.TaxJurisdiction
            .Include(J=>J.Brackets).ThenInclude(B=>B.Type)
            .Include(J => J.Account).ThenInclude(A=>A.Type)
            .ToListAsync()); 
        }
        #endregion

        #region IncomeItem
        // POST: IncomeItems
        [HttpPost("IncomeItems")]
        public async Task<IActionResult> IncomeItemsFromUser(Guid SessionID) {
            Session S = SessionManager.Manager.FindSession(SessionID);
            if (S == null) { return Unauthorized("Invalid session"); }


            return Ok(await NecoDB.IncomeItem
                            .Include(m => m.User)
                            .Include(m => m.FederalJurisdiction)
                            .Include(m => m.LocalJurisdiction)
                            .Include(m => m.Apartments)
                            .Include(m => m.Hotels)
                            .Include(m => m.Businesses)
                            .Where(i=> i.User.ID==S.UserID)
                            .ToListAsync());
        }

        // POST: IncomeItems/create
        [HttpPost("IncomeItems/Mod")]
        public async Task<IActionResult> IncomeItemMod(IncomeItemRequest Request) {
            Session S = SessionManager.Manager.FindSession(Request.SessionID);
            if (S == null) { return Unauthorized("Invalid session"); }

            if (Request.Item.ID == Guid.Empty) {

                //New Item
                NecoDB.Add(Request.Item);

            } else {

                IncomeItem I = await NecoDB.IncomeItem
                    .Include(I => I.User)
                    .Include(m => m.Apartments)
                    .Include(m => m.Hotels)
                    .Include(m => m.Businesses)
                    .FirstOrDefaultAsync(I=> I.ID == Request.Item.ID);

                if (I.User.ID != S.UserID) { return Unauthorized("Session owner does not own this income item"); }

                //We don't need to worry about Modified or Added income subitems. What we do need is to find deleted items
                IEnumerable<IncomeSubitem> DeletedSubItems = I.Subitems.Except(Request.Item.Subitems); //AMAZING THANK U LINQ
                                                                                                       //Get all the old sub items *except* those that are still in the new item

                //Now that we have a list of subitems, we still need to go through them to remove them, since they
                //are being treated as incomesubitems, not as their respective classes, and i think EF will throw a fit
                foreach (IncomeSubitem Sub in DeletedSubItems) {
                    if (Sub is Apartment SubA) { NecoDB.Remove(SubA); }
                    else if (Sub is Hotel SubH) { NecoDB.Remove(SubH); }
                    else if (Sub is Business SubB) { NecoDB.Remove(SubB); }
                    else { throw new InvalidOperationException($"Income Item {Sub} is of unknown type!"); }
                }

                I = Request.Item;

                //Old Item
                NecoDB.Update(I);

            }

            await NecoDB.SaveChangesAsync();
            return Ok(Request.Item);
        }

        //POST: IncomeItems/Delete
        [HttpPost("IncomeItems/Delete")]
        public async Task<IActionResult> IncomeItemDelete(IncomeItemRequest Request) {
            Session S = SessionManager.Manager.FindSession(Request.SessionID);
            if (S == null) { return Unauthorized("Invalid session"); }

            //Although we have a copy of the income item, let's ensure its correct by loading it again anyway.
            IncomeItem I = await NecoDB.IncomeItem
                .Include(I => I.User)
                .Include(m => m.Apartments)
                .Include(m => m.Hotels)
                .Include(m => m.Businesses)
                .FirstOrDefaultAsync(I => I.ID == Request.Item.ID);

            if (I.User.ID != S.UserID) { return Unauthorized("Session owner does not own this income item"); }

            //Now delete all subitems
            NecoDB.RemoveRange(I.Apartments);
            NecoDB.RemoveRange(I.Hotels);
            NecoDB.RemoveRange(I.Businesses);
            
            //Finally remove the Item
            NecoDB.Remove(I);

            await NecoDB.SaveChangesAsync();
            return Ok(I);
        }

        #endregion

        #region TaxReport

        //POST: TaxReport
        /// <summary>Generates TaxReport of the given user</summary>
        /// <param name="SessionID"></param>
        /// <returns></returns>
        [HttpPost("TaxReport")]
        public async Task<IActionResult> GenerateTR(Guid SessionID) {
            Session S = SessionManager.Manager.FindSession(SessionID);
            if (S == null) { return Unauthorized("Invalid session"); }

            User U = await NecoDB.User.Include(U => U.Type).FirstOrDefaultAsync(U => U.ID == S.UserID);
            return Ok(await CommonReportGenerator(U));
        }

        //POST: TaxReports
        [HttpPost("TaxReports")]
        public async Task<IActionResult> GetTR(Guid SessionID) {
            Session S = SessionManager.Manager.FindSession(SessionID);
            if (S == null) { return Unauthorized("Invalid session"); }

            return Ok(await NecoDB.TaxReport.Where(Tr => Tr.Owner.ID == S.UserID).ToListAsync());
        }

        #endregion

        #region Private Helper Functions

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

        /// <summary>Generates a Report based on a user and the context configured for this controller</summary>
        /// <param name="U"></param>
        /// <returns></returns>
        private async Task<TaxReport> CommonReportGenerator(User U) {
            ICollection<IncomeItem> Is = await NecoDB.IncomeItem
                .Include(m => m.User)
                .Include(m => m.FederalJurisdiction).ThenInclude(m=>m.Account)
                .Include(m => m.LocalJurisdiction).ThenInclude(m => m.Account)
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
                    .Where(T => T.Time > DayOfThisMonth(15) && T.State==TransactionState.COMPLETED
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

            return TaxReport.GenerateTaxReport(U, Is, Ts, J);
        }

        //OTHER FUNCTIONS THAT WILL BE USED BY EZTAX BUT NOT ACCESSIBLE ON THE WEB

        #endregion

        #region Tax and Income functions

        /// <summary></summary>
        [NonAction]
        public async Task<int> ExecuteTaxDay(bool Force) {
            if (DateTime.Now.Day != 15 && !Force) { throw new InvalidOperationException("Today is not a tax day!"); }
            List<User> Directory = NecoDB.User
                .Include(U => U.Type) //include type for taxation
                .Include(U => U.Accounts).ThenInclude(A => A.Details) //include accoutns to get taxes.
                .Where(U => U.Type.Taxation == TaxationType.Taxable).ToList();

            foreach (User U in Directory) {

                //If there is already a tax report for this month, assume this user was already taxed somehow, and skip.
                if (NecoDB.TaxReport.Any(T => T.Owner.ID == U.ID && T.PreparedDate.Month == DateTime.Now.Month)) { continue; }

                //Otherwise time to tax.

                //Generate a tax report:
                TaxReport T = await CommonReportGenerator(U);

                //Save the report beforehand
                NecoDB.Add(T);
                await NecoDB.SaveChangesAsync();

                Notification N;

                //User has no tax, and probably no income so time to skip
                if (T.GrandTotalTax == 0) {
                    N = new() {
                        Read = false,
                        Text = $"IMEX has calculated you owed no taxes this month, and as such you were not debited any. For any questions, see your latest tax report",
                        Time=DateTime.Now,
                        User=U
                    };

                    NecoDB.Add(N);
                    await NecoDB.SaveChangesAsync();

                    continue;
                }

                //Determine which account a User can use to pay the grand total tax
                BankAccount B = U.Accounts.First(B => B.Details.Balance > T.GrandTotalTax);

                //If we didn't find one find the first one that's NOT closed
                if (B == null) { B = U.Accounts.First(B=>B.Closed=false); }
                
                //If somehow, we STILL don't have a valid bank account and there is tax to be paid:
                if (B == null) {
                    N = new() {
                        Read = false,
                        Text = $"URGENT: You currently owe approximately {T.GrandTotalTax:0}p in taxes, and have no valid bank account. Please see your latest tax report for details",
                        Time = DateTime.Now,
                        User = U
                    };

                    NecoDB.Add(N);
                    await NecoDB.SaveChangesAsync();
                    continue;
                }

                //Now that we have a tax report:
                foreach (TaxJurisdiction J in T.PaymentBreakdownDictionary.Keys) {
                    //Prepare a transaction
                    Transaction Tr = new() {
                        Amount = T.PaymentBreakdownDictionary[J],
                        FromAccount = B,
                        Name=$"TAX TO {J.Name}",
                        State=TransactionState.COMPLETED,
                        Time=DateTime.Now,
                        ToAccount=J.Account
                    };

                    Tr.FromAccount.Details.Balance -= Tr.Amount;
                    Tr.ToAccount.Details.Balance += Tr.Amount;

                    NecoDB.Add(Tr);
                    await NecoDB.SaveChangesAsync();
                }

                N = new() {
                    Read = false,
                    Text = $"IMEX has automatically deducted {T.GrandTotalTax:0}p in taxes from account {B.ID}. See your latest tax report for more details",
                    Time = DateTime.Now,
                    User = U
                };

                NecoDB.Add(N);
                await NecoDB.SaveChangesAsync();
            }

            return 0;

        }

        [NonAction]
        public async Task<int> ExecuteIncomeDay(bool Force) {

            //Ensure the people is registered
            if (!NecoDB.BankAccount.Any(A => A.ID == THE_PEOPLE.ID)) {
                NecoDB.Add(THE_PEOPLE);
                await NecoDB.SaveChangesAsync();
            }

            if (DateTime.Now.Day != 1 && !Force) { throw new InvalidOperationException("Today is not a tax day!"); }
            List<User> Directory = NecoDB.User
                .Include(U => U.Accounts).ThenInclude(A => A.Details) //include accoutns to drop in income
                .ToList();

            //For each user
            foreach (User U in Directory) {
                Notification N;

                TaxUserInfo TUI = new() {
                    Items = await NecoDB.IncomeItem
                        .Include(I => I.User)
                        .Include(m => m.Apartments)
                        .Include(m => m.Hotels)
                        .Include(m => m.Businesses)
                        .Where(m => m.User.ID == U.ID).ToListAsync()
                };

                //Determine which account a User can be paid to
                BankAccount B = U.Accounts.First(B => B.Closed = false);

                //If somehow, we don't have a valid bank account 
                if (B == null) {
                    N = new() {
                        Read = false,
                        Text = $"URGENT: You currently have approximately {TUI.TotalMonthlyIncome():0}p in income, and have no valid bank account!",
                        Time = DateTime.Now,
                        User = U
                    };

                    NecoDB.Add(N);
                    await NecoDB.SaveChangesAsync();
                    continue;
                }

                ICollection<IncomeItem> Is = await NecoDB.IncomeItem
                    .Include(m => m.User)
                    .Include(m => m.Apartments)
                    .Include(m => m.Hotels)
                    .Include(m => m.Businesses)
                    .Where(i => i.User.ID == U.ID)
                    .ToListAsync();


                foreach (IncomeItem I in Is) {
                    //If a transaction for income exists this mont for this item, don't do it
                    if (NecoDB.Transaction.Any(
                        T => T.ToAccount.Owner.ID == U.ID && 
                        T.Name == $"INCOME {I.Name}" && 
                        T.Time.Month == DateTime.Now.Month)) { continue; }

                    //OK do it
                    Transaction T = new() {
                        Amount = I.TotalMonthlyIncome(),
                        FromAccount = THE_PEOPLE,
                        Name = $"INCOME {I.Name}",
                        State = TransactionState.COMPLETED,
                        Time = DateTime.Now,
                        ToAccount = B,
                    };

                    T.ToAccount.Details.Balance += T.Amount;

                    NecoDB.Update(T);
                    await NecoDB.SaveChangesAsync();

                }

                N = new() {
                    Read = false,
                    Text = $"IMEX has forwarded your income ({TUI.TotalMonthlyIncome():0}p) for the month to account {B.ID}.",
                    Time = DateTime.Now,
                    User = U
                };

                NecoDB.Add(N);
                await NecoDB.SaveChangesAsync();
            }

            return 0;

        }

        #endregion
    }
}
