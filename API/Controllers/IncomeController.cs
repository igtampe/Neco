﻿using Microsoft.AspNetCore.Mvc;
using Igtampe.Neco.Common;
using Igtampe.Neco.Common.Income;
using Igtampe.Neco.Common.Banking;
using Igtampe.Neco.Data;
using Igtampe.Neco.API.Requests;
using Igtampe.ChopoSessionManager;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Igtampe.Neco.Common.Taxes;

namespace Igtampe.Neco.API.Controllers {

    /// <summary>Sort by income</summary>
    public enum IncomeItemSortType { 
     
        /// <summary>Sort by name ascending</summary>
        NAME = 0,

        /// <summary>Sort by name descending</summary>
        NAME_DESC = 1,

        /// <summary>Sort by date created descending</summary>
        DATE_CREATED = 2,

        /// <summary>Sort by date created ascending</summary>
        DATE_CREATED_ASC = 3,

        /// <summary>Sort by date updated descending</summary>
        DATE_UPDATED = 4,

        /// <summary>Sort by date updated ascending</summary>
        DATE_UPDATED_ASC = 5,

    }

    /// <summary>Item for feed unioning of all incomeitems</summary>
    public class FeedItem : IncomeItem {

        /// <summary>Calculated income saved before this item was converted to a FeedItem</summary>
        public new long CalculatedIncome { get; set; } = 0;

        /// <summary>Returns the calculated income for anything after FeedItem that may need it</summary>
        /// <returns></returns>
        public override long Income() => CalculatedIncome;

        /// <summary>Creates a FeedItem</summary>
        /// <param name="E">Item to copy and convert to a FeedItem</param>
        /// <param name="Income">Income calculated beforehand to preserve the calculation</param>
        public FeedItem(IncomeItem E, long Income) {

            Address = E.Address;
            DateCreated = E.DateCreated;
            DateUpdated = E.DateUpdated;
            Description = E.Description;
            CalculatedIncome = Income;
            ID = E.ID;
            MiscIncome = E.MiscIncome;
            Name = E.Name;

        }
    }

    /// <summary>Controller that handles User operations</summary>
    [Route("API/Income")]
    [ApiController]
    public class IncomeController : ControllerBase {

        private readonly NecoContext DB;

        /// <summary>Creates a User Controller</summary>
        /// <param name="Context"></param>
        public IncomeController(NecoContext Context) => DB = Context;

        #region Summary

        private struct IncomeItemSummary {

            public long TotalIncome { get; set; } = 0;
            public int Count { get; set; } = 0;

            public IncomeItemSummary() { }

            public static async Task<IncomeItemSummary> CreateSummary<E>(IQueryable<E> Set, string Account) where E : IncomeItem{
                //what a disaster
                List<E> TheList = await Set.Include(A => A.Jurisdiction).ThenInclude(A => A!.ParentJurisdiction).ThenInclude(A => A!.ParentJurisdiction).ThenInclude(A => A!.ParentJurisdiction)
                    .Where(A=>A.Account != null && A.Account.ID==Account && A.Approved).ToListAsync();
                return new() {
                    TotalIncome = TheList.Sum(A => A.CalculatedIncome),
                    Count = TheList.Count,
                };
            }

            public static IncomeItemSummary CreateTotalSummary(params IncomeItemSummary[] Summaries) {
                return new() {
                    TotalIncome = Summaries.Sum(A => A.TotalIncome),
                    Count = Summaries.Sum(A => A.Count),
                };
            }
        }

        /// <summary>Gets a summary</summary>
        /// <param name="SessionID"></param>
        /// <param name="AccountID"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Summary([FromHeader] Guid SessionID, [FromQuery] string AccountID) {

            //Check SessionID
            Session? S = await GetSession(SessionID);
            if (S is null) { return Unauthorized(ErrorResult.Reusable.InvalidSession); }

            //Manuel is smiling with a big smile of I TOLD U SO :)
            //Look I know this could be a single SQL Query if I just had access to that table pero pues oops at least most times I have to do this we need the data anyway pero damnit.
            bool isAccountOwner = (await DB.Account.Include(A=>A.Owners).FirstOrDefaultAsync(A => A.ID == AccountID))?.Owners.Any(U=>U.ID==S.UserID) ?? false;
            if (!isAccountOwner && !await IsAdminOrSDC(S.UserID)) { return NotFound(ErrorResult.NotFound("Account was not found or is not owned by session owner", "AccountID")); }

            //Total income from all item subtypes individual and grand total

            //I really REALLY hope this maps, but I have a sneaking suspicion it won't
            //var Airline     = await IncomeItemSummary.CreateSummary(DB.Airline, AccountID);
            var Apartment   = await IncomeItemSummary.CreateSummary(DB.Apartment, AccountID);
            var Business    = await IncomeItemSummary.CreateSummary(DB.Business, AccountID);
            var Corporation = await IncomeItemSummary.CreateSummary(DB.Corporation, AccountID);
            var Hotel       = await IncomeItemSummary.CreateSummary(DB.Hotel, AccountID);
            var Total = IncomeItemSummary.CreateTotalSummary(Apartment, Business, Corporation, Hotel);
            

            return Ok(new { /*Airline,*/ Apartment, Business, Corporation, Hotel, Total });
        }

        #endregion

        #region IncomeDay
        
        /// <summary>Executes an Income Day event</summary>
        /// <param name="SessionID"></param>
        /// <param name="Force"></param>
        /// <returns></returns> 
        [HttpPost("IncomeDay")] //There's no real reason to make this a post, but I just want this to not be accessible via a standard web-browser
        public async Task<IActionResult> ExecuteIncomeDay([FromHeader] Guid? SessionID, [FromQuery] bool? Force) {
            Session? S = await Task.Run(() => SessionManager.Manager.FindSession(SessionID ?? Guid.Empty));
            return S is null
                ? Unauthorized("Invalid Session")
                : !await IsAdmin(S.UserID)
                    ? Unauthorized(ErrorResult.ForbiddenRoles("Admin"))
                    : DateTime.UtcNow.Day != 1 && Force != true
                        ? BadRequest("It is not currently income day! If you wish to run income anyways, add Force=true")
                        : Ok(await IncomeDay(DB, false));
        }
        internal struct IncomeReportItem {
            public string ID { get; set; }
            public string Name { get; set; }
            public long Income { get; set; }
        }

        internal struct IncomeReport {
            public List<IncomeReportItem> Breakdown { get; set; } = new();
            public long TotalIncome => Breakdown.Sum(A => A.Income);

            public IncomeReport() { }
        }

        [NonAction]
        internal static async Task<IncomeReport> IncomeDay(NecoContext DB, bool Simulate = true) {

            Console.WriteLine($"[Running {(Simulate ? "Model " : "")} Income Day]----------------------------------------------------------------------------------------------");
            Console.WriteLine($"[{DateTime.UtcNow:HH:mm:ss:FFFFFFF}] Getting all accounts");

            //Get *all* the accounts along with their income items
            List<Account> Accounts = await DB.Account
                .Where(A=>!A.Closed)
                .Include(A => A.IncomeItems.Where(A => A.Approved)).ThenInclude(I => I.Jurisdiction).ThenInclude(J => J!.ParentJurisdiction)
                .Include(A => A.Owners).ToListAsync();

            IncomeReport Report = new();

            foreach (Account A in Accounts) {

                Console.WriteLine($"[{DateTime.UtcNow:HH:mm:ss:FFFFFFF}] Processing account {A.Name} ({A.ID})");

                //Get the total income
                long TotalIncome = A.IncomeItems.Sum(I => I.CalculatedIncome);

                Console.WriteLine($"[{DateTime.UtcNow:HH:mm:ss:FFFFFFF}] TotalIncome is {TotalIncome:n0}p");
                Report.Breakdown.Add(new() {ID=A.ID!, Name=A.Name, Income=TotalIncome });

                if (Simulate) { continue; }

                //Deposit it
                A.Balance += TotalIncome; //Don't do a transaction. It'll be taxed twice.

                Console.WriteLine($"[{DateTime.UtcNow:HH:mm:ss:FFFFFFF}] New Balance {A.Balance}");

                //Send a notification
                foreach (User Owner in A.Owners) {

                    //Add the notification
                    //Create and add a notif
                    Notification N = new() {
                        Date = DateTime.UtcNow, User = Owner,
                        Text = $"Neco has deposited {A.Name} ({A.ID})'s monthly declared income of {TotalIncome:n0}p",
                    };

                    DB.Add(N);
                }

                DB.Update(A);
            }

            if (!Simulate) { await DB.SaveChangesAsync(); }
            Report.Breakdown = Report.Breakdown.OrderByDescending(A => A.Income).ToList();
            return Report;
        }

        #endregion

        #region SDC

        /// <summary>Gets a list of all unapproved corporations in the Neco system</summary>
        /// <param name="SessionID"></param>
        /// <returns></returns>
        [HttpGet("SDC/Unapproved")]
        public async Task<IActionResult> GetUnapprovedCorps([FromHeader] Guid SessionID) {

            //Get the session
            Session? S = await GetSession(SessionID);
            if (S is null) { return Unauthorized(ErrorResult.Reusable.InvalidSession); }

            //Ensure the Session is either Admin or SDC:
            if (! await IsAdminOrSDC(S.UserID)) { return Unauthorized(ErrorResult.ForbiddenRoles("Admin or SDC")); }

            //Just get all of it. Please have the SDC not leave more than a ton of corporations
            List<IncomeItem> Corps = await DB.IncomeItem
                .Include(C=>C.Jurisdiction).ThenInclude(A => A!.ParentJurisdiction).ThenInclude(A => A!.ParentJurisdiction).ThenInclude(A => A!.ParentJurisdiction)
                .Include(C=>C.Account)
                .Where(C => !C.Approved)
                .OrderByDescending(C => C.DateUpdated).ToListAsync();

            //Now all we need to do is return it
            return Ok(Corps);

        }

        /// <summary>Get a feed of the 20 most recently approved non-corporation income items of any type</summary>
        /// <param name="SessionID"></param>
        /// <returns></returns>
        [HttpGet("SDC/Approved")]
        public async Task<IActionResult> GetFeed([FromHeader] Guid SessionID) {

            //Get the session
            Session? S = await GetSession(SessionID);
            if (S is null) { return Unauthorized(ErrorResult.Reusable.InvalidSession); }

            //Ensure the Session is either Admin or SDC:
            if (!await IsAdminOrSDC(S.UserID)) { return Unauthorized(ErrorResult.ForbiddenRoles("Admin or SDC")); }

            //We need to make a massive union of a couple of lists.
            IQueryable<IncomeItem> TheBigSet = DB.IncomeItem 
                .Include(A => A.Account)
                .Include(A => A.Jurisdiction).ThenInclude(A => A!.ParentJurisdiction).ThenInclude(A => A!.ParentJurisdiction).ThenInclude(A => A!.ParentJurisdiction)
                .Where(A=>A.Approved);
                
            TheBigSet = TheBigSet.OrderByDescending(C => C.DateUpdated).Take(20);

            return Ok(await TheBigSet.ToListAsync());
                
        }

        /// <summary>Approves a corporation</summary>
        /// <param name="SessionID"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPut("SDC/{ID}")]
        public async Task<IActionResult> Approve([FromHeader] Guid SessionID, [FromRoute] Guid ID) {

            //Get the session
            Session? S = await GetSession(SessionID);
            if (S is null) { return Unauthorized(ErrorResult.Reusable.InvalidSession); }

            //Ensure the Session is either Admin or SDC:
            if (!await IsAdminOrSDC(S.UserID)) { return Unauthorized(ErrorResult.ForbiddenRoles("Admin or SDC")); }

            //Get the corporation
            IncomeItem? C = await DB.IncomeItem.FindAsync(ID);
            if (C is null) { return NotFound(ErrorResult.NotFound("Item was not found", "ID")); }

            C.Approved = !C.Approved;
            C.DateUpdated = DateTime.UtcNow;
            
            DB.Update(C);
            await DB.SaveChangesAsync();

            return Ok(C);

        }

        #endregion

        #region Get Collections

        /// <summary>Gets an account's Airlines</summary>
        /// <param name="SessionID"></param>
        /// <param name="AccountID"></param>
        /// <param name="Sort"></param>
        /// <param name="Query"></param>
        /// <param name="Skip"></param>
        /// <param name="Take"></param>
        /// <returns></returns>
        [HttpGet("Airlines")]
        public async Task<IActionResult> GetAirlines([FromHeader] Guid SessionID, [FromQuery] string AccountID, [FromQuery] IncomeItemSortType? Sort,
            [FromQuery] string? Query, [FromQuery] int? Skip, [FromQuery] int? Take) => await GetIncomeItems(DB.Airline, SessionID, AccountID, Sort, Query, Skip, Take);

        /// <summary>Gets an account's apartments</summary>
        /// <param name="SessionID"></param>
        /// <param name="AccountID"></param>
        /// <param name="Sort"></param>
        /// <param name="Query"></param>
        /// <param name="Skip"></param>
        /// <param name="Take"></param>
        /// <returns></returns>
        [HttpGet("Apartments")]
        public async Task<IActionResult> GetApartments([FromHeader] Guid SessionID, [FromQuery] string AccountID, [FromQuery] IncomeItemSortType? Sort,
            [FromQuery] string? Query, [FromQuery] int? Skip, [FromQuery] int? Take) => await GetIncomeItems(DB.Apartment, SessionID, AccountID, Sort, Query, Skip, Take);

        /// <summary>Gets an account's Businesses</summary>
        /// <param name="SessionID"></param>
        /// <param name="AccountID"></param>
        /// <param name="Sort"></param>
        /// <param name="Query"></param>
        /// <param name="Skip"></param>
        /// <param name="Take"></param>
        /// <returns></returns>
        [HttpGet("Businesses")]
        public async Task<IActionResult> GetBusinesses([FromHeader] Guid SessionID, [FromQuery] string AccountID, [FromQuery] IncomeItemSortType? Sort,
            [FromQuery] string? Query, [FromQuery] int? Skip, [FromQuery] int? Take) => await GetIncomeItems(DB.Business, SessionID, AccountID, Sort, Query, Skip, Take);

        /// <summary>Gets an account's corporations</summary>
        /// <param name="SessionID"></param>
        /// <param name="AccountID"></param>
        /// <param name="Sort"></param>
        /// <param name="Query"></param>
        /// <param name="Skip"></param>
        /// <param name="Take"></param>
        /// <returns></returns>
        [HttpGet("Corporations")]
        public async Task<IActionResult> GetCorporations([FromHeader] Guid SessionID, [FromQuery] string AccountID, [FromQuery] IncomeItemSortType? Sort,
            [FromQuery] string? Query, [FromQuery] int? Skip, [FromQuery] int? Take) => await GetIncomeItems(DB.Corporation, SessionID, AccountID, Sort, Query, Skip, Take);

        /// <summary>Gets an account's Hotels</summary>
        /// <param name="SessionID"></param>
        /// <param name="AccountID"></param>
        /// <param name="Sort"></param>
        /// <param name="Query"></param>
        /// <param name="Skip"></param>
        /// <param name="Take"></param>
        /// <returns></returns>
        [HttpGet("Hotels")]
        public async Task<IActionResult> GetHotels([FromHeader] Guid SessionID, [FromQuery] string AccountID, [FromQuery] IncomeItemSortType? Sort,
            [FromQuery] string? Query, [FromQuery] int? Skip, [FromQuery] int? Take) => await GetIncomeItems(DB.Hotel, SessionID, AccountID, Sort, Query, Skip, Take);

        #endregion

        #region Get Individual

        /// <summary>Gets specific airline</summary>
        /// <param name="SessionID"></param>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        [HttpGet("Airlines/{ItemID}")]
        public async Task<IActionResult> GetAirline([FromHeader] Guid SessionID, [FromRoute] Guid ItemID) 
            => await GetIncomeItem(DB.Airline, SessionID, ItemID);

        /// <summary>Gets specific Apartment</summary>
        /// <param name="SessionID"></param>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        [HttpGet("Apartments/{ItemID}")]
        public async Task<IActionResult> GetApartment([FromHeader] Guid SessionID, [FromRoute] Guid ItemID)
            => await GetIncomeItem(DB.Apartment, SessionID, ItemID);

        /// <summary>Gets a specific Business</summary>
        /// <param name="SessionID"></param>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        [HttpGet("Businesses/{ItemID}")]
        public async Task<IActionResult> GetBusiness([FromHeader] Guid SessionID, [FromRoute] Guid ItemID)
            => await GetIncomeItem(DB.Business, SessionID, ItemID);

        /// <summary>Gets specific Corporation</summary>
        /// <param name="SessionID"></param>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        [HttpGet("Corporations/{ItemID}")]
        public async Task<IActionResult> GetCorporation([FromHeader] Guid SessionID, [FromRoute] Guid ItemID)
            => await GetIncomeItem(DB.Corporation, SessionID, ItemID);

        /// <summary>Gets a specific Hotel</summary>
        /// <param name="SessionID"></param>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        [HttpGet("Hotels/{ItemID}")]
        public async Task<IActionResult> GetHotel([FromHeader] Guid SessionID, [FromRoute] Guid ItemID)
            => await GetIncomeItem(DB.Hotel, SessionID, ItemID);

        #endregion

        #region Create Individual

        /// <summary>Creates an Airline</summary>
        /// <param name="SessionID"></param>
        /// <param name="Request"></param>
        /// <returns></returns>
        [HttpPost("Airlines")]
        public async Task<IActionResult> CreateAirline([FromHeader] Guid SessionID, [FromBody] AirlineRequest Request)
            => await CreateIncomeItem<Airline,AirlineRequest>(SessionID, Request);

        /// <summary>Creates an apartment</summary>
        /// <param name="SessionID"></param>
        /// <param name="Request"></param>
        /// <returns></returns>
        [HttpPost("Apartments")]
        public async Task<IActionResult> CreateApartment([FromHeader] Guid SessionID, [FromBody] ApartmentRequest Request)
            => await CreateIncomeItem<Apartment,ApartmentRequest>(SessionID, Request);

        /// <summary>Creates a Business</summary>
        /// <param name="SessionID"></param>
        /// <param name="Request"></param>
        /// <returns></returns>
        [HttpPost("Businesses")]
        public async Task<IActionResult> CreateBusiness([FromHeader] Guid SessionID, [FromBody] BusinessRequest Request)
            => await CreateIncomeItem<Business,BusinessRequest>(SessionID, Request);

        /// <summary>Creates a corporation</summary>
        /// <param name="SessionID"></param>
        /// <param name="Request"></param>
        /// <returns></returns>
        [HttpPost("Corporations")]
        public async Task<IActionResult> CreateCorporation([FromHeader] Guid SessionID, [FromBody] CorporationRequest<Corporation> Request)
            => await CreateIncomeItem<Corporation,CorporationRequest<Corporation>>(SessionID, Request);

        /// <summary>Creates a hotel</summary>
        /// <param name="SessionID"></param>
        /// <param name="Request"></param>
        /// <returns></returns>
        [HttpPost("Hotels")]
        public async Task<IActionResult> CreateHotel([FromHeader] Guid SessionID, [FromBody] HotelRequest Request)
            => await CreateIncomeItem<Hotel,HotelRequest>(SessionID, Request);

        #endregion

        #region Update Individual

        /// <summary>Updates an Airline</summary>
        /// <param name="SessionID"></param>
        /// <param name="ItemID"></param>
        /// <param name="Request"></param>
        /// <returns></returns>
        [HttpPut("Airlines/{ItemID}")]
        public async Task<IActionResult> UpdateAirline([FromHeader] Guid SessionID, [FromRoute] Guid ItemID, [FromBody] AirlineRequest Request)
            => await UpdateIncomeItem(DB.Airline, SessionID, ItemID, Request);

        /// <summary>Updates an apartment</summary>
        /// <param name="SessionID"></param>
        /// <param name="ItemID"></param>
        /// <param name="Request"></param>
        /// <returns></returns>
        [HttpPut("Apartments/{ItemID}")]
        public async Task<IActionResult> UpdateApartment([FromHeader] Guid SessionID, [FromRoute] Guid ItemID, [FromBody] ApartmentRequest Request)
            => await UpdateIncomeItem(DB.Apartment, SessionID, ItemID, Request);

        /// <summary>Updates a Business</summary>
        /// <param name="SessionID"></param>
        /// <param name="ItemID"></param>
        /// <param name="Request"></param>
        /// <returns></returns>
        [HttpPut("Businesses/{ItemID}")]
        public async Task<IActionResult> UpdateBusiness([FromHeader] Guid SessionID, [FromRoute] Guid ItemID, [FromBody] BusinessRequest Request)
            => await UpdateIncomeItem(DB.Business, SessionID, ItemID, Request);

        /// <summary>Updates a corporation</summary>
        /// <param name="SessionID"></param>
        /// <param name="ItemID"></param>
        /// <param name="Request"></param>
        /// <returns></returns>
        [HttpPut("Corporations/{ItemID}")]
        public async Task<IActionResult> UpdateCorporation([FromHeader] Guid SessionID, [FromRoute] Guid ItemID, [FromBody] CorporationRequest<Corporation> Request)
            => await UpdateIncomeItem(DB.Corporation, SessionID, ItemID, Request);

        /// <summary>Updates a hotel</summary>
        /// <param name="SessionID"></param>
        /// <param name="ItemID"></param>
        /// <param name="Request"></param>
        /// <returns></returns>
        [HttpPut("Hotels/{ItemID}")]
        public async Task<IActionResult> UpdateHotel([FromHeader] Guid SessionID, [FromRoute] Guid ItemID, [FromBody] HotelRequest Request)
            => await UpdateIncomeItem(DB.Hotel, SessionID, ItemID, Request);

        #endregion

        #region Delete Individual

        /// <summary>Deletes specific airline</summary>
        /// <param name="SessionID"></param>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        [HttpDelete("Airlines/{ItemID}")]
        public async Task<IActionResult> DeleteAirline([FromHeader] Guid SessionID, [FromRoute] Guid ItemID)
            => await DeleteIncomeItem(DB.Airline, SessionID, ItemID);

        /// <summary>Deletes specific Apartment</summary>
        /// <param name="SessionID"></param>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        [HttpDelete("Apartments/{ItemID}")]
        public async Task<IActionResult> DeleteApartment([FromHeader] Guid SessionID, [FromRoute] Guid ItemID)
            => await DeleteIncomeItem(DB.Apartment, SessionID, ItemID);

        /// <summary>Deletes a specific Business</summary>
        /// <param name="SessionID"></param>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        [HttpDelete("Businesses/{ItemID}")]
        public async Task<IActionResult> DeleteBusiness([FromHeader] Guid SessionID, [FromRoute] Guid ItemID)
            => await DeleteIncomeItem(DB.Business, SessionID, ItemID);

        /// <summary>Deletes specific Corporation</summary>
        /// <param name="SessionID"></param>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        [HttpDelete("Corporations/{ItemID}")]
        public async Task<IActionResult> DeleteCorporation([FromHeader] Guid SessionID, [FromRoute] Guid ItemID)
            => await DeleteIncomeItem(DB.Corporation, SessionID, ItemID);

        /// <summary>Deletes a specific Hotel</summary>
        /// <param name="SessionID"></param>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        [HttpDelete("Hotels/{ItemID}")]
        public async Task<IActionResult> DeleteHotel([FromHeader] Guid SessionID, [FromRoute] Guid ItemID)
            => await DeleteIncomeItem(DB.Hotel, SessionID, ItemID);

        #endregion

        #region Generic Functions

        /// <summary>Generic function to get any type of income item list</summary>
        /// <typeparam name="E">Type of income item list to retrieve</typeparam>
        /// <param name="BaseSet">Set to take IncomeItems from</param>
        /// <param name="SessionID">ID of the session executing this request</param>
        /// <param name="Sort">Sort order the item list</param>
        /// <param name="Query">Search query to search the name or description of item type</param>
        /// <param name="Skip">How many items to skip</param>
        /// <param name="Take">How many items to take</param>
        /// <param name="AccountID">Account ID to get </param>
        /// <returns></returns>
        private async Task<IActionResult> GetIncomeItems<E>(IQueryable<E> BaseSet, Guid? SessionID, string? AccountID,
            IncomeItemSortType? Sort, string? Query, int? Skip, int? Take) where E : IncomeItem {

            Session? S = await GetSession(SessionID);
            if (S is null) { return Unauthorized(ErrorResult.Reusable.InvalidSession); }

            bool isAccountOwner = (await DB.Account.Include(A => A.Owners).FirstOrDefaultAsync(A => A.ID == AccountID))?.Owners.Any(U => U.ID == S.UserID) ?? false;
            if (!isAccountOwner && ! await IsAdminOrSDC(S.UserID)) { return NotFound(ErrorResult.NotFound("Account was not found or is not owned by session owner", "AccountID")); }

            BaseSet = BaseSet.Include(A=>A.Jurisdiction).ThenInclude(A=>A!.ParentJurisdiction).ThenInclude(A => A!.ParentJurisdiction).ThenInclude(A => A!.ParentJurisdiction)
                .Where(I => I.Account != null && I.Account.ID == AccountID);
            if (!string.IsNullOrWhiteSpace(Query)) { BaseSet = BaseSet.Where(I => I.Name.ToLower().Contains(Query.ToLower()) || I.Description.ToLower().Contains(Query.ToLower())); }

            BaseSet = Sort switch {
                IncomeItemSortType.NAME_DESC => BaseSet.OrderByDescending(I => I.Name),
                IncomeItemSortType.DATE_CREATED => BaseSet.OrderByDescending(I => I.DateCreated),
                IncomeItemSortType.DATE_CREATED_ASC => BaseSet.OrderBy(I => I.DateCreated),
                IncomeItemSortType.DATE_UPDATED => BaseSet.OrderByDescending(I => I.DateUpdated),
                IncomeItemSortType.DATE_UPDATED_ASC => BaseSet.OrderBy(I => I.DateUpdated),
                _ => BaseSet.OrderBy(I => I.Name),
            };

            BaseSet.Skip(Skip ?? 0).Take(Take ?? 20);

            return Ok(await BaseSet.ToListAsync());
        }

        /// <summary>Generic function to get any type of income item</summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="BaseSet"></param>
        /// <param name="SessionID"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        private async Task<IActionResult> GetIncomeItem<E>(IQueryable<E> BaseSet, Guid? SessionID, Guid? ID) where E : IncomeItem {

            Session? S = await GetSession(SessionID);
            if (S is null) { return Unauthorized(ErrorResult.Reusable.InvalidSession); }

            //Ok this should be simple
            E? Item = await GetItem(BaseSet,S,ID);

            return Item is null
                ? NotFound(ErrorResult.NotFound("Item was not found or is not owned by account owned by session owner", "ID"))
                : Ok(Item);
        }

        /// <summary>Generic function to create any type of income item</summary>
        /// <typeparam name="E"></typeparam>
        /// <typeparam name="F"></typeparam>
        /// <param name="SessionID"></param>
        /// <param name="ItemRequest"></param>
        /// <returns></returns>
        private async Task<IActionResult> CreateIncomeItem<E, F>(Guid? SessionID, F ItemRequest) where E : IncomeItem, new() where F : IncomeItemRequest<E> {

            Session? S = await GetSession(SessionID);
            if (S is null) { return Unauthorized(ErrorResult.Reusable.InvalidSession); }

            E Item = new();

            foreach (PropertyInfo Prop in typeof(F).GetProperties()) {

                //Check for the properties we need to skip
                switch (Prop.Name.ToUpper()) {
                    case "ID":
                    case "DATECREATED":
                    case "DATEUPDATED":
                        //Ignore ID and Dates
                        continue;
                    case "JURISDICTIONID":
                        //Look for the District ID then keep going

                        Jurisdiction? J = await DB.Jurisdiction.FindAsync(ItemRequest.JurisdictionID);
                        if (J is null) { return NotFound(ErrorResult.NotFound("Jurisdiction was not found","Jurisdiction")); }
                        Item.Jurisdiction = J;

                        continue;

                    case "ACCOUNTID":
                        //Look for the account ID then keep going

                        Account? A = await DB.Account.Include(A=>A.Owners).FirstOrDefaultAsync(A => A.ID == ItemRequest.AccountID);
                        if (A is null) { return NotFound(ErrorResult.NotFound("Account was not found", "Account")); }
                        if (!A.Owners.Any(u => u.ID == S.UserID)) { return Unauthorized(ErrorResult.Unauthorized("User does not own account")); }

                        if (A.IncomeType != IncomeType.CORPORATE && Item is Corporation) {
                            return BadRequest(ErrorResult.BadRequest("Only corporate accounts can hold corporate income items", "Account"));
                        }

                        Item.Account = A;

                        continue;
                    default:
                        break;
                }

                //Get the updated value
                object? O = Prop.GetValue(ItemRequest);

                //Update the value as long as its not null
                if (O is not null) {
                    PropertyInfo? DestProp = typeof(E).GetProperty(Prop.Name);
                    if (DestProp is null) { throw new InvalidOperationException("Property in this request did not appear on the destination object"); }
                    DestProp.SetValue(Item, O);
                }
            }

            //Update the dates
            Item.DateCreated= DateTime.UtcNow;
            Item.DateUpdated = DateTime.UtcNow;

            //If we're dealing with a corporation, do not approve. Else approve.
            Item.Approved = Item is not Corporation && Item.Income() < 500000   ;

            DB.Add(Item);
            await DB.SaveChangesAsync();

            return Ok(Item);

        }

        /// <summary>Generic function to update any type of income item</summary>
        /// <typeparam name="E"></typeparam>
        /// <typeparam name="F"></typeparam>
        /// <param name="BaseSet"></param>
        /// <param name="SessionID"></param>
        /// <param name="ID"></param>
        /// <param name="ItemRequest"></param>
        /// <returns></returns>
        private async Task<IActionResult> UpdateIncomeItem<E, F>(IQueryable<E> BaseSet, Guid? SessionID, Guid? ID, F ItemRequest) where E : IncomeItem where F : IncomeItemRequest<E> {

            Session? S = await GetSession(SessionID);
            if (S is null) { return Unauthorized(ErrorResult.Reusable.InvalidSession); }

            E? Item = await GetItem(BaseSet, S, ID);
            if (Item is null) { return NotFound(ErrorResult.NotFound("Item was not found or is not owned by account owned by session owner","ID")); }

            foreach (PropertyInfo Prop in typeof(F).GetProperties()) {

                //Check for the properties we need to skip
                switch (Prop.Name.ToUpper()) {
                    case "ID":
                    case "DATECREATED":
                    case "DATEUPDATED":
                        //Ignore ID and dates
                        continue;
                    case "JURISDICTIONID":
                        //Look for the District ID then keep going

                        Jurisdiction? J = await DB.Jurisdiction.FindAsync(ItemRequest.JurisdictionID);
                        if (J is null) { return NotFound(ErrorResult.NotFound("Jurisdiction was not found", "Jurisdiction")); }
                        Item.Jurisdiction = J;

                        continue;

                    case "ACCOUNTID":
                        //Look for the account ID then keep going

                        Account? A = await DB.Account.Include(A => A.Owners).FirstOrDefaultAsync(A => A.ID == ItemRequest.AccountID);
                        if (A is null) { return NotFound(ErrorResult.NotFound("Account was not found", "Account")); }
                        if (!A.Owners.Any(u => u.ID == S.UserID)) { return Unauthorized(ErrorResult.Unauthorized("User does not own account")); }
                        if (A.IncomeType != IncomeType.CORPORATE && Item is Corporation) {
                            return BadRequest(ErrorResult.BadRequest("Only corporate accounts can hold corporate income items","Account"));
                        }

                        continue;
                    default:
                        break;
                }

                //Get the updated value
                object? O = Prop.GetValue(ItemRequest);

                //Update the value as long as its not null
                if (O is not null) {
                    PropertyInfo? DestProp = typeof(E).GetProperty(Prop.Name);
                    if (DestProp is null) { throw new InvalidOperationException("Property in this request did not appear on the destination object"); }
                    DestProp.SetValue(Item, O);
                }
            }

            //Update the date updated
            Item.DateUpdated = DateTime.UtcNow;

            //If we're dealing with a corporation, do not approve. Else approve.
            Item.Approved = Item is not Corporation && Item.Income() < 500000; ;

            DB.Update(Item);
            await DB.SaveChangesAsync();

            return Ok(Item);

        }

        /// <summary>Generic function to delete any type of income item</summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="BaseSet"></param>
        /// <param name="SessionID"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        private async Task<IActionResult> DeleteIncomeItem<E>(IQueryable<E> BaseSet, Guid? SessionID, Guid? ID) where E : IncomeItem {

            Session? S = await GetSession(SessionID);
            if (S is null) { return Unauthorized(ErrorResult.Reusable.InvalidSession); }

            E? Item = await GetItem(BaseSet, S, ID);
            if (Item is null) { return NotFound(ErrorResult.NotFound("Item was not found or is not owned by account owned by session owner", "ID")); }

            DB.Remove(Item);
            await DB.SaveChangesAsync();

            return Ok(Item);

        }

        #endregion

        /// <summary>Actually gets an item (verifying if the user has access)</summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="BaseSet"></param>
        /// <param name="S"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        private async Task<E?> GetItem<E>(IQueryable<E> BaseSet, Session S, Guid? ID) where E : IncomeItem{

            //Goddamnit this is probably the worst one
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            BaseSet = BaseSet.Include(I => I.Jurisdiction).ThenInclude(A => A.ParentJurisdiction).ThenInclude(A => A.ParentJurisdiction).ThenInclude(A => A.ParentJurisdiction)
                .Where(A => A.Account != null).Include(A => A.Account).ThenInclude(A => A.Owners);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            E? Item = await BaseSet.FirstOrDefaultAsync(I => I.ID == ID);
            return Item is null || await IsAdminOrSDC(S.UserID)
                ? Item
                : Item.Account != null && Item.Account.Owners != null && !Item.Account.Owners.Any(U => U.ID == S.UserID) 
                    ? null 
                    : Item; //have whatever deals with this deal with this

        }

        /// <summary>Checks if given user is either an administrator, or a member of the Salary Determination Committee</summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        private async Task<bool> IsAdminOrSDC(string UserID) => await DB.User.AnyAsync(U => U.ID == UserID && (U.IsAdmin || U.IsSDC));

        /// <summary>Checks if given user is an administrator</summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        private async Task<bool> IsAdmin(string UserID) => await DB.User.AnyAsync(U => U.ID == UserID && (U.IsAdmin));

        /// <summary>Gets a session asynchronously</summary> //(this should maybe just be a function within the sesion manager)
        /// <param name="SessionID"></param>
        /// <returns></returns>
        private static async Task<Session?> GetSession(Guid? SessionID) => await Task.Run(() => SessionManager.Manager.FindSession(SessionID ?? Guid.Empty));

    }
}
