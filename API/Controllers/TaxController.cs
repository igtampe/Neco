using Microsoft.AspNetCore.Mvc;
using Igtampe.Neco.Common;
using Igtampe.Neco.Common.Banking;
using Igtampe.Neco.Common.Taxes;
using Igtampe.Neco.Common.Income;
using Igtampe.Neco.Data;
using Igtampe.Neco.API.Requests;
using Igtampe.ChopoSessionManager;
using Microsoft.EntityFrameworkCore;

namespace Igtampe.Neco.API.Controllers {

    /// <summary>Controller that handles User operations</summary>
    [Route("API/Taxes")]
    [ApiController]
    public class TaxController : ControllerBase {

        private readonly NecoContext DB;

        /// <summary>Creates a User Controller</summary>
        /// <param name="Context"></param>
        public TaxController(NecoContext Context) => DB = Context;

        /// <summary>Gets Jurisdictions based on a type</summary>
        /// <param name="Type">Type of jurisdictions to return. By default: Country</param>
        /// <param name="Query"></param>
        /// <param name="Skip"></param>
        /// <param name="Take"></param>
        /// <returns></returns>
        [HttpGet("Jurisdictions")]
        public async Task<IActionResult> GetJurisdictions([FromQuery] JurisdictionType? Type, [FromQuery] string? Query, int? Skip, int? Take) {

            IQueryable<Jurisdiction> Set = DB.Jurisdiction.Include(J => J.ParentJurisdiction).Include(J=>J.TiedAccount);
            if (Type != null) { Set = Set.Where(J => J.Type == (Type ?? JurisdictionType.COUNTRY)); }

            if (!string.IsNullOrWhiteSpace(Query)) { Set = Set.Where(J => J.Name.ToLower().Contains(Query.ToLower())); }

            Set = Set.OrderBy(J => J.Type).ThenBy(J => J.Name);

            if (Skip is not null && Take is not null) { Set = Set.Skip(Skip ?? 0).Take(Take ?? 20); }

            return Ok(await Set.ToListAsync());

        }

        /// <summary>Gets a specific jurisdiction</summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet("Jurisdictions/{ID}")]
        public async Task<IActionResult> GetJurisdiction(string ID) {

            Jurisdiction? J = await DB.Jurisdiction.Include(J => J.ParentJurisdiction).FirstOrDefaultAsync(J => J.ID == ID);
            return J is null ? NotFound(ErrorResult.NotFound("Jurisdiction was not found","ID")) : Ok(J);

        }

        /// <summary>Gets a specific jurisdiction's children</summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet("Jurisdictions/{ID}/Children")]
        public async Task<IActionResult> GetJurisdictionChildren(string ID) {

            Jurisdiction? J = await DB.Jurisdiction.Include(J => J.ChildJurisdictions).Include(J=>J.ParentJurisdiction).FirstOrDefaultAsync(J => J.ID == ID);
            return J is null ? NotFound(ErrorResult.NotFound("Jurisdiction was not found", "ID")) : Ok(J.ChildJurisdictions);

        }

        /// <summary>Gets a specific Jurisdiction's brackets</summary>
        /// <returns></returns>
        [HttpGet("Jurisdiction/{ID}/Brackets")]
        public async Task<IActionResult> GetJurisdictionBrackets(string ID) {

            Jurisdiction? J = await DB.Jurisdiction.Include(J => J.Brackets).FirstOrDefaultAsync(J => J.ID == ID);

            return J is null ? NotFound(ErrorResult.NotFound("Jurisdiction was not found", "ID")) : Ok(J.Brackets.OrderBy(J=>J.IncomeType).ThenByDescending(J=>J.Start));

        }

        /// <summary>Gets a specific bracket</summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet("Brackets/{ID}")]
        public async Task<IActionResult> GetBracket(Guid ID) {
            Bracket? B = await DB.Bracket.FindAsync(ID);
            return B is null ? NotFound(ErrorResult.NotFound("Bracket was not found","ID")): Ok(B);
        }

        /// <summary>Generates a tax report for given account ID</summary>
        /// <param name="SessionID"></param>
        /// <param name="AccountID"></param>
        /// <returns></returns>
        [HttpGet("GenerateReport")]
        public async Task<IActionResult> GetTaxReport([FromHeader] Guid? SessionID, [FromQuery] string AccountID) {

            //Check the session:
            Session? S = await Task.Run(() => SessionManager.Manager.FindSession(SessionID ?? Guid.Empty));
            if (S is null) { return Unauthorized(ErrorResult.Reusable.InvalidSession); }

            //Check the account
            return !await IsOwned(S,AccountID) && ! await IsAdmin(S.UserID)
                ? NotFound(ErrorResult.NotFound("Account was not found or is not owned by the session owner","Account"))
                : Ok(await GenerateReport(DB,AccountID));
        }

        /// <summary>Gets a previously saved tax report with given ID</summary>
        /// <param name="SessionID"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet("Reports")]
        public async Task<IActionResult> GetTaxReports([FromHeader] Guid? SessionID, [FromQuery] string ID) {

            Session? S = await Task.Run(() => SessionManager.Manager.FindSession(SessionID ?? Guid.Empty));
            if (S is null) { return Unauthorized(ErrorResult.Reusable.InvalidSession); }

            //Verify the session
            if (!await IsOwned(S, ID)) { return NotFound(); }

            List<TaxReport> TRs = await DB.TaxReport.Where(A => A.Account != null && A.Account.ID==ID)
                .OrderByDescending(A => A.DateGenerated).ToListAsync(); ;

            return Ok(TRs);
        }

        /// <summary>Gets a previously saved tax report with given ID</summary>
        /// <param name="SessionID"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet("Reports/{ID}")]
        public async Task<IActionResult> GetTaxReport([FromHeader] Guid? SessionID, [FromQuery] Guid ID) {

            Session? S = await Task.Run(() => SessionManager.Manager.FindSession(SessionID ?? Guid.Empty));
            if (S is null) { return Unauthorized("Invalid session"); }

            TaxReport? TR = await DB.TaxReport.FirstOrDefaultAsync(A => A.ID == ID && A.Account != null );
            return TR is null
                ? NotFound(ErrorResult.NotFound("Tax Report was not found, or is not from an account this session owner owns","ID"))
                : Ok(TR);
        }

        /// <summary>Creates a jurisdiction</summary>
        /// <param name="SessionID"></param>
        /// <param name="Request"></param>
        /// <returns></returns>
        [HttpPost("Jurisdiction")]
        public async Task<IActionResult> CreateJurisdiction([FromHeader] Guid? SessionID, [FromBody] JurisdictionRequest Request) {
            Session? S = await Task.Run(() => SessionManager.Manager.FindSession(SessionID ?? Guid.Empty));
            if (S is null) { return Unauthorized(ErrorResult.Reusable.InvalidSession); }

            if (!await IsAdmin(S.UserID)) { return Unauthorized(ErrorResult.ForbiddenRoles("Admin")); }

            Jurisdiction? Parent = null;
            if (!string.IsNullOrWhiteSpace(Request.ParentJurisdictionID)) { 
                Parent = await DB.Jurisdiction.FirstOrDefaultAsync(J => J.ID == Request.ParentJurisdictionID);
                if (Parent is null) { return NotFound(ErrorResult.NotFound("Parent jurisdiction was not found","Head")); }
            }

            Account? A = null;

            if (!string.IsNullOrWhiteSpace(Request.TiedAccountID)) {
                A = await DB.Account.FindAsync(Request.TiedAccountID);
                if (A is null) { return NotFound(ErrorResult.NotFound("Tied account was not found", "Account")); }
            }

            if (Request.Type != JurisdictionType.COUNTRY && Parent is null) { return BadRequest(ErrorResult.BadRequest("No parent jurisdiction is set and this is not a Country","Head")); }
            if (Parent is not null && Parent.Type != Request.Type - 1) { return BadRequest(ErrorResult.BadRequest("Parent type is not one layer above this jurisdiction", "Type")); }

            Jurisdiction J = new() {
                ImageURL = Request.ImageURL, Name = Request.Name,
                Population = Request.Population, Type = Request.Type,
                ParentJurisdiction = Parent,
                TiedAccount = A
            };

            do { J.ID=J.IDGenerator.Generate(); } while (await DB.Jurisdiction.AnyAsync(j=>j.ID==J.ID));

            //OK we're ready
            DB.Add(J);
            await DB.SaveChangesAsync();
            
            return Ok(J);
        }

        /// <summary>Creates a Tax Bracket</summary>
        /// <param name="SessionID"></param>
        /// <param name="Jurisdiction"></param>
        /// <param name="Bracket"></param>
        /// <returns></returns>
        [HttpPost("Brackets")]
        public async Task<IActionResult> CreateBracket([FromHeader] Guid? SessionID, [FromQuery] string? Jurisdiction,  [FromBody] Bracket Bracket) {
            if (Bracket.End == -1) { Bracket.End = long.MaxValue; }
            if (Bracket.Start > Bracket.End) { return BadRequest("Bracket start is after bracket end"); }

            Session? S = await Task.Run(() => SessionManager.Manager.FindSession(SessionID ?? Guid.Empty));
            if (S is null) { return Unauthorized("Invalid session"); }

            if (!await IsAdmin(S.UserID)) { return Unauthorized(ErrorResult.ForbiddenRoles("Admin")); }

            //Find the jurisdiction
            Jurisdiction? J = await DB.Jurisdiction.FindAsync(Jurisdiction ?? "");
            if (J is null) { return NotFound(ErrorResult.NotFound("Jurisdiction this bracket will belong to was not found","Jurisdiction")); }

            Bracket.Jurisdiction = J;

            DB.Add(Bracket);
            await DB.SaveChangesAsync();

            return Ok(Bracket);
        }

        /// <summary>Updates a Jurisdiction</summary>
        /// <param name="SessionID"></param>
        /// <param name="ID"></param>
        /// <param name="Request"></param>
        /// <returns></returns>
        [HttpPut("Jurisdiction/{ID}")]
        public async Task<IActionResult> UpdateJurisdiction([FromHeader] Guid? SessionID, string ID, [FromBody] JurisdictionRequest Request) {
            Session? S = await Task.Run(() => SessionManager.Manager.FindSession(SessionID ?? Guid.Empty));
            if (S is null) { return Unauthorized("Invalid session"); }

            if (!await IsAdmin(S.UserID)) { return Unauthorized(ErrorResult.ForbiddenRoles("Admin")); }

            Jurisdiction? J = await DB.Jurisdiction.Include(J => J.ChildJurisdictions).FirstOrDefaultAsync(J=>J.ID==ID);
            if(J is null) { return NotFound(ErrorResult.NotFound("Jurisdiction was not found","ID")); }

            Jurisdiction? Parent = null;
            if (!string.IsNullOrWhiteSpace(Request.ParentJurisdictionID)) {
                Parent = await DB.Jurisdiction.FirstOrDefaultAsync(J => J.ID == Request.ParentJurisdictionID);
                if (Parent is null) { return NotFound(ErrorResult.NotFound("Parent jurisdiction was not found", "Head")); }
            }

            Account? A = null;

            if (!string.IsNullOrWhiteSpace(Request.TiedAccountID)) {
                A = await DB.Account.FindAsync(Request.TiedAccountID);
                if (A is null) { return NotFound(ErrorResult.NotFound("Tied account was not found", "Account")); }
            }

            //Change in type:
            //We could hypothetically update all of the children pero sabes? That's going to be far too much.
            if (J.Type != Request.Type && !J.ChildJurisdictions.Any()) { return BadRequest(ErrorResult.BadRequest("Cannot change jurisdiction type if this jurisdiction has children","Type")); }

            if (Request.Type != JurisdictionType.COUNTRY && Parent is null) { return BadRequest(ErrorResult.BadRequest("No parent jurisdiction is set and this is not a Country", "Head")); }
            if (Parent is not null && Parent.Type != Request.Type - 1) { return BadRequest(ErrorResult.BadRequest("Parent type is not one layer above this jurisdiction", "Type")); }

            J.Name = Request.Name;
            J.ParentJurisdiction = Parent;
            J.ImageURL = Request.ImageURL;
            J.Population = Request.Population;
            J.TiedAccount = A;
            J.Type = Request.Type;

            //OK we're ready
            DB.Update(J);
            await DB.SaveChangesAsync();

            return Ok(J);

        }

        /// <summary>Edits a bracket</summary>
        /// <param name="SessionID"></param>
        /// <param name="ID"></param>
        /// <param name="Bracket"></param>
        /// <returns></returns>
        [HttpPut("Brackets/{ID}")]
        public async Task<IActionResult> UpdateBracket([FromHeader] Guid? SessionID, Guid ID, Bracket Bracket) {
            if (Bracket.End == -1) { Bracket.End = long.MaxValue; }
            if (Bracket.Start > Bracket.End) { return BadRequest("Bracket start is after bracket end"); }

            Session? S = await Task.Run(() => SessionManager.Manager.FindSession(SessionID ?? Guid.Empty));
            if (S is null) { return Unauthorized("Invalid session"); }

            if (!await IsAdmin(S.UserID)) { return Unauthorized(ErrorResult.ForbiddenRoles("Admin")); }

            //Find the existing bracket
            Bracket? B = await DB.Bracket.FindAsync(ID);
            if (B is null) { return NotFound(ErrorResult.NotFound("Bracket was not found","ID")); }
            
            B.Name = Bracket.Name;
            B.Description = Bracket.Description;
            B.Rate = Bracket.Rate;
            B.Start = Bracket.Start;
            B.End = Bracket.End;
            B.IncomeType = Bracket.IncomeType;

            DB.Update(B);
            await DB.SaveChangesAsync();

            return Ok(B);

        }

        /// <summary>Deletes a Bracket</summary>
        /// <param name="SessionID"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpDelete("Brackets/{ID}")]
        public async Task<IActionResult> DeleteBracket([FromHeader] Guid? SessionID, Guid ID) {
            Session? S = await Task.Run(() => SessionManager.Manager.FindSession(SessionID ?? Guid.Empty));
            if (S is null) { return Unauthorized("Invalid session"); }

            if (!await IsAdmin(S.UserID)) { return Unauthorized(ErrorResult.ForbiddenRoles("Admin")); }

            Bracket? B = await DB.Bracket.FindAsync(ID);
            if (B is null) { return NotFound(ErrorResult.NotFound("Bracket was not found", "ID")); }

            DB.Remove(B);
            await DB.SaveChangesAsync();
            return Ok(B);

        }

        /// <summary>Executes a Tax Day event</summary>
        /// <param name="SessionID"></param>
        /// <param name="Force"></param>
        /// <returns></returns> 
        [HttpPost("TaxDay")] //There's no real reason to make this a post, but I just want this to not be accessible via a standard web-browser
        public async Task<IActionResult> ExecuteTaxDay([FromHeader] Guid? SessionID, [FromQuery] bool? Force) {
            Session? S = await Task.Run(() => SessionManager.Manager.FindSession(SessionID ?? Guid.Empty));
            return S is null
                ? Unauthorized("Invalid Session")
                : !await IsAdmin(S.UserID)
                    ? Unauthorized(ErrorResult.ForbiddenRoles("Admin"))
                    : DateTime.UtcNow.Day != 1 && Force != true
                        ? BadRequest("It is not currently tax day! If you wish to run tax anyways, add Force=true")
                        : Ok(TaxDay(DB, false));
        }

        #region Helpers

        internal struct JurisdictionTaxReportItem {
            public string ID { get; set; }
            public string Name { get; set; }
            public long TaxCollected { get; set; }
        }

        internal struct JurisdictionTaxReport {
            public List<JurisdictionTaxReportItem> Breakdown { get; set; }
            public long TotalTaxCollected => Breakdown.Sum(A => A.TaxCollected);
        }

        [NonAction]
        internal static async Task<JurisdictionTaxReport> TaxDay(NecoContext DB, bool Simulate = true) {
            //Get a list of all the accounts/
            Console.WriteLine($"[Running {(Simulate ? "Model" : "")} Tax Day]----------------------------------------------------------------------------------------------");
            Console.WriteLine($"[{DateTime.UtcNow:HH:mm:ss:FFFFFFF}] Getting all accounts");
            List<Account> Accounts = await GetAccountsForTaxReport(DB);

            //Get a list of all the transactions that occurred.
            Console.WriteLine($"[{DateTime.UtcNow:HH:mm:ss:FFFFFFF}] Getting all transactions for this tax period");
            List<Transaction> Transactions = await GetAccountTransactionsForTaxReport(DB);

            Dictionary<Jurisdiction, long> PaymentDictionary = new();

            Parallel.ForEach(Accounts, A => {
                Console.WriteLine($"[{DateTime.UtcNow:HH:mm:ss:FFFFFFF}] Preparing to generate a tax day report for account {A.ID}.");

                //Get the transactions from *this* user
                List<Transaction> Ts = Transactions.Where(T => (T.Origin!.ID == A.ID || T.Destination!.ID == A.ID)).ToList();

                //Generate a tax report for this account
                TaxReport TR = TaxReport.Create(A, Ts);
                if (TR.IsEmpty || TR.Account is null) { return; }

                Console.WriteLine($"[{DateTime.UtcNow:HH:mm:ss:FFFFFFF}] Report generated {A.ID}");

                //Add the Jurisdictions to the payment dictionary
                foreach (Jurisdiction J in TR.TaxPaymentDictionary.Keys) {
                    Console.WriteLine($"[{DateTime.UtcNow:HH:mm:ss:FFFFFFF}] Waiting on lock of payment dictionary to add {TR.TaxPaymentDictionary[J]:n0}p to {J.Name} to main payment dictionary");
                    lock (PaymentDictionary) {
                        Console.WriteLine($"[{DateTime.UtcNow:HH:mm:ss.FFFFFFF}] Adding or Updating");
                        if (PaymentDictionary.ContainsKey(J)) { PaymentDictionary[J] += TR.TaxPaymentDictionary[J]; } else { PaymentDictionary.Add(J, TR.TaxPaymentDictionary[J]); }
                        Console.WriteLine($"[{DateTime.UtcNow:HH:mm:ss:FFFFFFF}] Done. Out of here.");
                    }
                }

                //If we're simulating, we just return
                if (Simulate) { return; }

                Console.WriteLine($"[{DateTime.UtcNow:HH:mm:ss:FFFFFFF}] Taking money out of {TR.Account.ID}");

                //Remove the grand total tax from the account
                TR.Account.Balance -= TR.GrandTotalTax; //These updates ***should*** be passed along when we save the tax report

                Console.WriteLine($"[{DateTime.UtcNow:HH:mm:ss:FFFFFFF}] {TR.Account.ID}'s balance is now {TR.Account.Balance}");

                //Send notifications to all the owners of this account
                foreach (User Owner in TR.Account.Owners) {

                    //Add the notification
                    //Create and add a notif
                    Notification N = new() {
                        Date = DateTime.UtcNow, User = Owner,
                        Text = $"Neco has withdrawn {TR.Account.Name} ({TR.Account.ID})'s monthly tax of {TR.GrandTotalTax:n0}p. See your newest Tax Report for more details",
                    };

                    Console.WriteLine($"[{DateTime.UtcNow:HH:mm:ss:FFFFFFF}] Waiting on lock to add notification to {Owner.ID}");
                    lock (DB) { DB.Add(N); }
                }

                Console.WriteLine($"[{DateTime.UtcNow:HH:mm:ss:FFFFFFF}] Waiting on lock to add TaxReport for {TR.Account.ID}");
                lock (DB) { DB.Add(TR); }
            });

            if (!Simulate) {

                Console.WriteLine($"[{DateTime.UtcNow:HH:mm:ss:FFFFFFF}] Sending money to jurisdictions");

                //For each jurisdiction that needs to be paid, pay it
                foreach (Jurisdiction J in PaymentDictionary.Keys) {
                    
                    //We can skip accounts that will get nada
                    if (PaymentDictionary[J] == 0) { continue; }

                    if (J.TiedAccount == null) {
                        Console.WriteLine($"[{DateTime.UtcNow:HH:mm:ss:FFFFFFF}] Jurisdiction {J.Name} has no tied account. We will be skipping it");
                        continue; 
                    }

                    J.TiedAccount.Balance += PaymentDictionary[J];

                    //Send notifications to all the owners of this account
                    foreach (User Owner in J.TiedAccount.Owners) {

                        //Add the notification
                        //Create and add a notif
                        Notification N = new() {
                            Date = DateTime.UtcNow, User = Owner,
                            Text = $"Neco has deposited {PaymentDictionary[J]:n0}p in tax for this month to {J.TiedAccount.Name} ({J.TiedAccount.ID}).",
                        };

                        DB.Add(N);
                    }

                    DB.Update(J.TiedAccount);
                }

                //Cleanup old tax reports
                DB.RemoveRange(DB.TaxReport.Where(A => A.DateGenerated < DateTime.SpecifyKind(DateTime.Now.AddMonths(-3).AddDays(1), DateTimeKind.Utc)));

                await DB.SaveChangesAsync();
            }

            var TaxPayments = PaymentDictionary.Select(T => new JurisdictionTaxReportItem() { ID = T.Key.ID!, Name = T.Key.Name, TaxCollected = T.Value });
            return new() { Breakdown = TaxPayments.ToList() };

        }

        /// <summary>Generates a report for a user</summary>
        /// <param name="DB">Neco Context to pull all data from</param>
        /// <param name="AccountID"></param>
        /// <returns></returns>
        [NonAction]
        internal static async Task<TaxReport> GenerateReport(NecoContext DB, string AccountID) {

            //Get account with districts
            Account? A = (await GetAccountsForTaxReport(DB, AccountID)).FirstOrDefault();
            
            //Governments and charities pay no taxes
            if (A is null || A.IncomeType == IncomeType.GOVERNMENT || A.IncomeType == IncomeType.CHARITY) { return TaxReport.Empty; }
            
            //Decide the time period we'll get transactions from:
            List<Transaction> Ts = await GetAccountTransactionsForTaxReport(DB,AccountID);

            return TaxReport.Create(A, Ts);

        }

        internal static async Task<List<Account>> GetAccountsForTaxReport(NecoContext DB, string AccountID = "") {
            var Set = DB.Account //This check should cover the null dereference
                .Include(A => A.Jurisdiction).ThenInclude(A => A!.TiedAccount).ThenInclude(A => A!.Owners) //Do self joins require incldues?
                .Include(A => A.Jurisdiction).ThenInclude(D => D!.ParentJurisdiction).ThenInclude(A => A!.TiedAccount).ThenInclude(A => A!.Owners)
                .Include(A => A.Jurisdiction).ThenInclude(D => D!.ChildJurisdictions).ThenInclude(A => A!.TiedAccount).ThenInclude(A => A!.Owners)
                .Include(A => A.Jurisdiction).ThenInclude(D => D!.Brackets)
                .Include(A => A.IncomeItems).ThenInclude(A => A.Jurisdiction).ThenInclude(D => D!.ParentJurisdiction).ThenInclude(D => D!.Brackets)
                .Include(A => A.IncomeItems).ThenInclude(A => A.Jurisdiction).ThenInclude(D => D!.Brackets)
                .Where(A => A.Jurisdiction != null && A.IncomeType!=IncomeType.GOVERNMENT && A.IncomeType!=IncomeType.CHARITY);

            if (!string.IsNullOrWhiteSpace(AccountID)) { Set=Set.Where(A=>A.ID==AccountID); }
            return await Set.ToListAsync();
        }

        internal static async Task<List<Transaction>> GetAccountTransactionsForTaxReport(NecoContext DB, string AccountID="") {
            var Set = DB.Transaction
                    .Include(T => T.Origin).ThenInclude(O => O!.Owners)
                    .Include(T => T.Destination).ThenInclude(O => O!.Owners)
                    .Where(T => T.Origin != null && T.Destination != null);

            if (!string.IsNullOrWhiteSpace(AccountID)) { Set = Set.Where(T => (T.Origin!.ID == AccountID || T.Destination!.ID == AccountID)); }

            Set = DateTime.UtcNow.Day > 15
                ? Set.Where(T => T.Date > DayOfThisMonth(15))
                : Set.Where(T => T.Date > DayOfLastMonth(15) && T.Date < DayOfThisMonth(15));
            return await Set.ToListAsync();
        }

        /// <summary>Checks if a given user is an administrator or not</summary>
        /// <returns></returns>
        [NonAction]
        private async Task<bool> IsAdmin(string UserID) => await DB.User.AnyAsync(U => U.ID == UserID && U.IsAdmin);

        [NonAction]
        private async Task<bool> IsOwned(Session S, string ID) {

            Account? A = await DB.Account.Include(A => A.Owners).FirstOrDefaultAsync(A => A.ID == ID);
            return A != null && A.Owners.Any(U => U.ID == S.UserID);

        }

        /// <summary>Gets a DateTime representing the specified day of last month (IE if it's currently December 5th, executing
        /// this function with param day=15, it would return November 15th). Use day 31 to get the last day of the last month.</summary>
        /// <param name="Day"></param>
        /// <returns></returns>
        private static DateTime DayOfLastMonth(int Day) {

            int LastMonth = DateTime.UtcNow.Month - 1;
            int Year = DateTime.UtcNow.Year;
            if (LastMonth == 0) {
                Year--;
                LastMonth = 12;
            }

            //The one condition this may fail in is if we ask for the last 31st and last month didn't have a 31st.
            //In this and similar instances (if the previous month does not have this date), let's use the month's maximum length.

            return DateTime.SpecifyKind(new(Year, LastMonth, Math.Min(Day, DateTime.DaysInMonth(Year, LastMonth))),DateTimeKind.Utc);
        }

        /// <summary>Gets a DateTime representing the specified day of this month (IE if it's currently December 5th, executing
        /// this function with param day=15, it would return December 15th). Use day 31 to get the last day of this month.</summary>
        /// <param name="Day"></param>
        /// <returns></returns>
        private static DateTime DayOfThisMonth(int Day) => DateTime.SpecifyKind(new (DateTime.UtcNow.Year, DateTime.UtcNow.Month, Math.Min(Day, DateTime.DaysInMonth(DateTime.UtcNow.Year, DateTime.UtcNow.Month))), DateTimeKind.Utc);

        #endregion

    }
}
