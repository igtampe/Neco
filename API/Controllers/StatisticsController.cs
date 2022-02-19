using Microsoft.AspNetCore.Mvc;
using Igtampe.Neco.Common;
using Igtampe.Neco.Common.Banking;
using Igtampe.Neco.Common.Income;
using Igtampe.Neco.Common.Taxes;
using Igtampe.Neco.Data;
using Igtampe.ChopoSessionManager;
using Microsoft.EntityFrameworkCore;

namespace Igtampe.Neco.API.Controllers {

    /// <summary>Controller that handles User operations</summary>
    [Route("API/Statistics")]
    [ApiController]
    public class StatisticsController : ControllerBase {

        private readonly NecoContext DB;

        /// <summary>Creates a User Controller</summary>
        /// <param name="Context"></param>
        public StatisticsController(NecoContext Context) => DB = Context;

        /// <summary>Gets a month by month breakdown of transaction amount and count month by month</summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        //Total transactions month by month
        //Total transaction amount month by month
        [HttpGet("Transactions/Monthly")]
        public async Task<IActionResult> TransactionsMonthly([FromQuery] DateTime? StartDate, [FromQuery] DateTime? EndDate) {

            DateTime Start = StartDate ?? DateTime.UtcNow.AddMonths(-6).AddDays(-DateTime.UtcNow.Day);
            DateTime End = EndDate ?? DateTime.UtcNow;

            //D a m n 
            var Data = await DB.Transaction.Where(T=>T.Date > Start && T.Date < End).GroupBy(T => new { T.Date.Month, T.Date.Year })
                .Select(T => new { T.Key.Year, T.Key.Month, Count = T.Count(), Sum = T.Sum(T => T.Amount) }).ToListAsync();
                
            return Ok(Data);

        }

        /// <summary>Gets global overall statistics on transactions</summary>
        /// <returns></returns>
        //Total transaction to date
        //Total transaction amount to date
        [HttpGet("Transactions")]
        public async Task<IActionResult> Transactions() {

            long Sum = await DB.Transaction.SumAsync(T => T.Amount);
            long Count = await DB.Transaction.CountAsync();
            return Ok(new { Sum, Count });
        
        }

        /// <summary>Runs a Model Tax day to see how much each jurisdiction is projected to make</summary>
        /// <returns></returns>
        //Total amount taken by tax (involves generating tax reports)
        [HttpGet("TaxDayReport")]
        public async Task<IActionResult> JurisdictionsReport() {

            //Get a list of all the accounts/
            Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}] Getting all accounts");
            List<Account> Accounts = await TaxController.GetAccountsForTaxReport(DB);

            //Get a list of all the transactions that occurred.
            Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}] Getting all transactions for this tax period");
            List<Transaction> Transactions = await TaxController.GetAccountTransactionsForTaxReport(DB);

            Dictionary<Jurisdiction, long> PaymentDictionary = new();

            Parallel.ForEach(Accounts, A => {
                Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}] Preparing to generate a tax day report for account {A.ID}. Waiting on lock to get account and transactions");

                //Get the transactions from *this* user
                List<Transaction> Ts = Transactions.Where(T => (T.Origin!.ID == A.ID || T.Destination!.ID == A.ID)).ToList();

                //Generate a tax report for this account
                TaxReport TR = TaxReport.Create(A,Ts);
                if (TR.IsEmpty) { return; }

                Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}] Report generated {A.ID}");

                //Add the Jurisdictions to the payment dictionary
                foreach (Jurisdiction J in TR.TaxPaymentDictionary.Keys) {
                    Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}] Waiting on lock of payment dictionary to add {TR.TaxPaymentDictionary[J]:n0}p to {J.Name} to main payment dictionary");
                    lock (PaymentDictionary) {
                        Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}] Adding or Updating");
                        if (PaymentDictionary.ContainsKey(J)) { PaymentDictionary[J] += TR.TaxPaymentDictionary[J]; } 
                        else { PaymentDictionary.Add(J, TR.TaxPaymentDictionary[J]); }
                        Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}] Done. Out of here.");
                    }
                }
            });

            var TaxPayments = PaymentDictionary.Select(T => new { T.Key.ID, T.Key.Name, TaxCollected = T.Value });

            return Ok(TaxPayments);

        }

        /// <summary>Runs a Model Tax day to see how much each jurisdiction is projected to make</summary>
        /// <returns></returns>
        //Total amount taken by tax (involves generating tax reports)
        [HttpGet("WealthReport")]
        public async Task<IActionResult> JurisdictionWealthReport() {

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var Report = DB.Account.Where(A=>A.Jurisdiction!=null).GroupBy(A => new { A.Jurisdiction.ID, A.Jurisdiction.Name })
                .Select(T => new { T.Key.ID, T.Key.Name, Wealth = T.Sum(A => A.Balance) });
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            return Ok(await Report.ToListAsync());

        }

        //Total amount of each type of income entities
        //Total amount of each type of static monthly income generated.

        /// <summary>Gets statistics for all airlines in the Neco system</summary>
        /// <returns></returns>
        [HttpGet("Income/Airlines")]
        public async Task<IActionResult> AirlineStatistics() => await IncomeItemStatistics(DB.Airline);
        
        /// <summary>Gets statistics for all apartments in the Neco system</summary>
        /// <returns></returns>
        [HttpGet("Income/Apartments")]
        public async Task<IActionResult> ApartmentStatistics() => await IncomeItemStatistics(DB.Apartment);
        
        /// <summary>Gets statistics for all businesses in the Neco system</summary>
        /// <returns></returns>
        [HttpGet("Income/Businesses")]
        public async Task<IActionResult> BusinessStatistics() => await IncomeItemStatistics(DB.Business);
        
        /// <summary>Gets statistics for all corporations in the Neco system</summary>
        /// <returns></returns>
        [HttpGet("Income/Corporations")]
        public async Task<IActionResult> CorporationStatistics() => await IncomeItemStatistics(DB.Corporation);
        
        /// <summary>Gets statistics for all hotels in the neco system</summary>
        /// <returns></returns>
        [HttpGet("Income/Hotel")]
        public async Task<IActionResult> HotelStatistics() => await IncomeItemStatistics(DB.Hotel);

        //Total amount of monthly income generated
        /// <summary>Gets total income statistics for all types of income items in the neco system</summary>
        /// <typeparam name="E"></typeparam>
        /// <returns></returns>
        [HttpGet("Income")]
        public async Task<IActionResult> Income<E>() => await IncomeItemStatistics(DB.IncomeItem);

        private async Task<IActionResult> IncomeItemStatistics<E>(IQueryable<E> BaseSet) where E : IncomeItem {

#pragma warning disable CS8602 // Dereference of a possibly null reference. //Wheres still don't deal with dereference
            var Breakdown = (await BaseSet
                    .Where(T => T.Jurisdiction != null).Where(A => A.Approved)
                    .Include(A => A.Jurisdiction)
                    .ToListAsync()
                    )
                .GroupBy(T => new { T.Jurisdiction.ID, T.Jurisdiction.Name })
                .Select(T => new { T.Key.ID, T.Key.Name, Count = T.Count(), Income = T.Sum(T => T.Income()) })
                .ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            int TotalCount = Breakdown.Sum(T => T.Count);
            long TotalIncome = Breakdown.Sum(T => T.Income);

            return Ok(new { Breakdown, TotalCount, TotalIncome });
        
        }

        /// <summary>Gets statistics for all banks in the Neco system</summary>
        /// <returns></returns>
        [HttpGet("Banks")]
        public async Task<IActionResult> Banks() {

            //Get all Banks, include accounts
#pragma warning disable CS8602 // Dereference of a possibly null reference. //Wheres still don't deal with dereference
            var AccountSummary = await DB.Account.Where(T => T.Bank != null)
                .GroupBy(T => new { T.Bank.ID, T.Bank.Name, T.Bank.ImageURL }) //We don't calculate market share even though with SQL we probably could (:Shrug:)
                .Select(T => new { T.Key.ID, T.Key.Name, T.Key.ImageURL, Count = T.Count(), Balance = T.Sum(T => T.Balance) }).ToListAsync();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            int TotalCount = AccountSummary.Sum(T => T.Count);
            long TotalMoney = AccountSummary.Sum(T => T.Balance);

            return Ok(new {AccountSummary, TotalCount, TotalMoney});

        }
    }
}
