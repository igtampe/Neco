using Microsoft.AspNetCore.Mvc;
using Igtampe.Neco.Common.Income;
using Igtampe.Neco.Data;
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
                .Select(T => new { T.Key.Year, T.Key.Month, Count = T.Count(), Sum = T.Sum(T => T.Amount) })
                .OrderByDescending(A=>A.Year).ThenByDescending(A=>A.Month).ToListAsync();
                
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
        [HttpGet("Tax")]
        public async Task<IActionResult> JurisdictionsReport() => Ok(await TaxController.TaxDay(DB));

        /// <summary>Runs a Model Tax day to see how much each jurisdiction is projected to make</summary>
        /// <returns></returns>
        //Total amount taken by tax (involves generating tax reports)
        [HttpGet("Jurisdictions")]
        public async Task<IActionResult> JurisdictionWealthReport() {

            var Report = (await DB.Account.Where(A=>A.Jurisdiction!=null).Include(A=>A.Jurisdiction).ThenInclude(A=>A!.ParentJurisdiction).ToListAsync())
                .GroupBy(A => new { A.Jurisdiction!.ID, A.Jurisdiction.Name, A.Jurisdiction.Flag })
                .Select(T => new { T.Key.ID, T.Key.Name,T.Key.Flag, Wealth = T.Sum(A => A.Balance) })
                .OrderByDescending(A=>A.Wealth).ThenBy(A=>A.Name);

            return Ok(Report.ToList());

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
        /// <returns></returns>
        [HttpGet("Income")]
        public async Task<IActionResult> Income() => await IncomeItemStatistics(DB.IncomeItem);

        private async Task<IActionResult> IncomeItemStatistics<E>(IQueryable<E> BaseSet) where E : IncomeItem {

            var Breakdown = (await BaseSet
                    .Where(T => T.Jurisdiction != null).Where(A => A.Approved)
                    .Include(A => A.Jurisdiction).ThenInclude(A => A!.ParentJurisdiction)
                    .ToListAsync())
                .GroupBy(T => new { T.Jurisdiction!.ID, T.Jurisdiction.Name, T.Jurisdiction.Flag })
                .Select(T => new { T.Key.ID, T.Key.Name, T.Key.Flag, Count = T.Count(), Income = T.Sum(T => T.Income()) })
                .OrderByDescending(A=>A.Income).ThenBy(A=>A.Name) //We'll order by income and not count
                .ToList();

            int TotalCount = Breakdown.Sum(T => T.Count);
            long TotalIncome = Breakdown.Sum(T => T.Income);

            return Ok(new { Breakdown, TotalCount, TotalIncome });
        
        }

        /// <summary>Gets statistics for all banks in the Neco system</summary>
        /// <returns></returns>
        [HttpGet("Banks")]
        public async Task<IActionResult> Banks() {

            //Get all Banks, include accounts
            var AccountSummary = await DB.Account.Where(T => T.Bank != null)
                .GroupBy(T => new { T.Bank!.ID, T.Bank.Name, T.Bank.ImageURL }) //We don't calculate market share even though with SQL we probably could (:Shrug:)
                .Select(T => new { T.Key.ID, T.Key.Name, T.Key.ImageURL, Count = T.Count(), Balance = T.Sum(T => T.Balance) })
                .OrderByDescending(T => T.Balance).ThenBy(T => T.Name)
                .ToListAsync();

            int TotalCount = AccountSummary.Sum(T => T.Count);
            long TotalMoney = AccountSummary.Sum(T => T.Balance);

            return Ok(new {AccountSummary, TotalCount, TotalMoney});

        }
    }
}
