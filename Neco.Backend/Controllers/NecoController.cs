using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Igtampe.Neco.Common;
using Igtampe.Neco.Data;
using Igtampe.Neco.Common.Requests;

namespace Igtampe.Neco.Backend.Controllers {
    [Route("")]
    [ApiController]
    public class NecoController: Controller {
        private readonly NecoContext NecoDB;

        private readonly Random Randomizer = new();

        public NecoController(NecoContext context) { NecoDB = context; }

        //GET:
        [HttpGet]
        public IActionResult Ping() {return Ok("You've connected to the server! Congrats.");}

        // GET: Dir
        [HttpGet("dir")]
        public async Task<IActionResult> Directory() {
            return Ok(await NecoDB.User
                .Include(m => m.Type)
                .Include(m => m.Accounts).ThenInclude(m => m.Type)
                .ToListAsync());
        }

        // GET: INFO/5
        [HttpGet("INFO/{id}")]
        public async Task<IActionResult> UserDetails(string id) {
            if (id == null) { return NotFound(); }

            var asset = await NecoDB.User
                .Include(m => m.Type)
                .Include(m => m.Accounts).ThenInclude(m => m.Type)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (asset == null) { return NotFound(); }

            return Ok(asset);
        }

        // POST: Register
        [HttpPost("Register")]
        public async Task<IActionResult> Registration(NewUserRequest NewUser) {
            if (string.IsNullOrWhiteSpace(NewUser.Name) ||
                string.IsNullOrWhiteSpace(NewUser.Pin) ||
                NewUser.Type == null) { return BadRequest("All fields of a New User Request must be filled"); }

            string ID;
            do {
                ID = "";
                while (ID.Length < 5) { ID += Randomizer.Next(10); }
            } while (UserExists(ID));

            User U = new() {
                ID = ID,
                Name = NewUser.Name,
                Type = NewUser.Type,
            };

            UserAuth UA = new() {
                ID = ID,
                Pin = NewUser.Pin
            };

            NecoDB.Add(U);
            NecoDB.Add(UA);

            await NecoDB.SaveChangesAsync();

            return Ok(U);
        }

        // GET: Cert
        [HttpGet("Cert")]
        public async Task<IActionResult> Certifications(int? Start, int? End) {
            int realstart = Start != null ? (int)Start : 0;
            int realend = End != null ? (int)End : 20;
            return Ok(await NecoDB.CertifiedItem //Get all the certified items
                .Include(m => m.CertifiedBy).ThenInclude(m => m.Type) //include who certified it and their type jic
                .OrderByDescending(C => C.Date) //Sort in descending order by date
                .Skip(realstart).Take(realend-realstart) //take only the specified amount
                .ToListAsync()); //To list async
        }

        // GET: Cert/5
        [HttpGet("Cert/{id}")]
        public async Task<IActionResult> GetCertification(Guid? id) {
            if (id == null) { return NotFound(); }
            var asset = await NecoDB.CertifiedItem.Include(m => m.CertifiedBy).ThenInclude(m => m.Type).FirstOrDefaultAsync(m => m.ID == id);
            if (asset == null) { return NotFound(); }
            return Ok(asset);
        }

        //Get; Transaction/5
        [HttpGet("Transactions/{id}")]
        public async Task<IActionResult> GetTransaction(Guid? id) {
            if (id == null) { return NotFound(); }
            var asset = await NecoDB.Transaction
                .Include(T=> T.FromAccount).Include(T => T.ToAccount)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (asset == null) { return NotFound(); }
            return Ok(asset);
        }

        // GET: Banks
        [HttpGet("Banks")]
        public async Task<IActionResult> Banks() {
            return Ok(await NecoDB.Bank
                .Include(b => b.AccountTypes)
                .ToListAsync());
        }

        // GET: UMSAT
        [HttpGet("UserType")]
        public async Task<IActionResult> UserTypeIndex() { return Ok(await NecoDB.UserType.ToListAsync()); }


        //Session Required Actions

        //POST: Transaction
        [HttpPost("SM")]
        public async Task<IActionResult> ExecuteTransaction(TransactionRequest TransactRequest) {
            Session S = SessionManager.Manager.FindSession(TransactRequest.SessionID);
            if (S == null) { return Unauthorized("Invalid session"); }

            BankAccount FromBank = await NecoDB.BankAccount.Include(b => b.Details).Include(d => d.Owner).FirstOrDefaultAsync(B => B.ID == TransactRequest.FromBankID);
            BankAccount ToBank   = await NecoDB.BankAccount.Include(b => b.Details).Include(d => d.Owner).FirstOrDefaultAsync(B => B.ID == TransactRequest.ToBankID);

            if (FromBank.Owner.ID != S.UserID) { return Unauthorized("From bank account does not belong to the user in this session"); }
            if (FromBank.Details.Balance < TransactRequest.Amount) { return BadRequest("Insufficient Funds"); }
            if (FromBank.Closed || ToBank.Closed) { return BadRequest("One or more of the bank acounts in this transaction are closed."); }

            Notification N = null;

            if (FromBank.Owner.ID.Equals(ToBank.Owner.ID)) {
                N = new() {
                    Read = false,
                    Text = $"{FromBank.Owner.Name} sent a Neco with {TransactRequest.Amount : N0}p to Bank {ToBank.ID}",
                    Time = DateTime.Now,
                    User = ToBank.Owner
                };
            }


            //Create a transaction to update
            Transaction T = new() {
                Amount = TransactRequest.Amount,
                State = TransactionState.COMPLETED,
                FromAccount = FromBank,
                ToAccount = ToBank,
                Time = DateTime.Now,
                Name = string.IsNullOrWhiteSpace(TransactRequest.Name) 
                ? $"TRANSFER FROM {FromBank.ID} TO {ToBank.ID}"
                : TransactRequest.Name 
            };

            //Execute the transaction
            FromBank.Details.Balance -= TransactRequest.Amount;
            ToBank.Details.Balance += TransactRequest.Amount;

            //Add/update entities
            NecoDB.Add(T);
            if (N != null) { NecoDB.Add(N); }
            NecoDB.Update(FromBank);
            NecoDB.Update(ToBank);

            //Save context
            await NecoDB.SaveChangesAsync();

            return Ok(T.ID);
        }

        // POST: INFO
        [HttpPost("INFO")]
        public async Task<IActionResult> UserDetailsWithSessionID(Guid SessionID) {
            Session S = SessionManager.Manager.FindSession(SessionID);
            if (S == null) { return BadRequest("Session Invalid"); }

            var User = await NecoDB.User
                .Include(u => u.Type)
                .Include(u => u.Accounts).ThenInclude(b => b.Type)
                .Include(u => u.Accounts).ThenInclude(b=> b.Details)
                .Include(u=> u.Notifications)
                .FirstOrDefaultAsync(m => m.ID == S.UserID);
            if (User == null) { return NotFound(); }

            //Sort the lists
            User.Accounts = User.Accounts.OrderBy(B => B.ID).ToList();
            User.Notifications = User.Notifications.OrderByDescending(N => N.Time).ToList();
            
            //Return it 
            return Ok(User);
        }

        //POST:CERT
        [HttpPost("Cert")]
        public async Task<IActionResult> Certify(CertificationRequest CertRequest) {
            Session S = SessionManager.Manager.FindSession(CertRequest.SessionID);
            if (S == null) { return Unauthorized("Invalid session"); }

            CertifiedItem C = new() {
                CertifiedBy = await NecoDB.User.FirstOrDefaultAsync(u => u.ID == S.UserID),
                Date = DateTime.Now,
                Text = CertRequest.Text
            };

            NecoDB.Add(C);
            await NecoDB.SaveChangesAsync();
            return Ok(C);
        }

        //POST ReadNotif/5
        [HttpPost("ReadNotif/{NotifID}")]
        public async Task<IActionResult> ReadNotif(Guid NotifID, [FromBody]Guid SessionID) {
            Session S = SessionManager.Manager.FindSession(SessionID);
            if (S == null) { return Unauthorized("Invalid session"); }

            //Load Notif
            Notification N = await NecoDB.Notification
                .Include(N=>N.User)
                .FirstOrDefaultAsync(N=>N.ID==NotifID);
            if (N == null) { return NotFound(); }

            if (N.User.ID != S.UserID) { return Unauthorized("Notification does not belong to this user"); }
            N.Read = true;

            NecoDB.Update(N);
            await NecoDB.SaveChangesAsync();
            return Ok(N);
        }

        //POST ReadNotif/All
        [HttpPost("ReadNotif/all")]
        public async Task<IActionResult> ReadNotifAll(Guid SessionID) {
            Session S = SessionManager.Manager.FindSession(SessionID);
            if (S == null) { return Unauthorized("Invalid session"); }

            //Load Notif
            HashSet<Notification> Ns = NecoDB.Notification
                .Include(N => N.User)
                .Where(N => N.User.ID == S.UserID && !N.Read).ToHashSet();
            if (Ns == null) { return NotFound(); }

            int UpdatedNotifs=0;

            foreach (var N in Ns) {
                N.Read = true;
                NecoDB.Update(N);
                UpdatedNotifs++;
            }

            await NecoDB.SaveChangesAsync();
            return Ok(UpdatedNotifs);
        }

        //POST ReadNotif/Del
        [HttpPost("ReadNotif/Del")]
        public async Task<IActionResult> ReadNotifDel(Guid SessionID) {
            Session S = SessionManager.Manager.FindSession(SessionID);
            if (S == null) { return Unauthorized("Invalid session"); }

            //Load Notif
            HashSet<Notification> Ns = NecoDB.Notification
                .Include(N => N.User)
                .Where(N => N.User.ID == S.UserID && N.Read).ToHashSet();
            if (Ns == null) { return NotFound(); }

            NecoDB.RemoveRange(Ns);

            await NecoDB.SaveChangesAsync();
            return Ok(Ns.Count);
        }

        //POST: BNKO
        [HttpPost("BNKO")]
        public async Task<IActionResult> BNKOpen(BankAccountOpenRequest BNKORequest) {
            Session S = SessionManager.Manager.FindSession(BNKORequest.SessionID);
            if (S == null) { return Unauthorized("Invalid session"); }

            BankAccountType Type = await NecoDB.BankAccountType
                .Include(T => T.Bank)
                .FirstOrDefaultAsync(T => T.ID == BNKORequest.BankAccountTypeID);
            if (Type == null) { return NotFound("Requested Bank account type not found"); }

            string ID;

            do {
                ID = "";
                while (ID.Length < 9) { ID += Randomizer.Next(10); }
            } while (BankAccountExists(ID));

            BankAccount A = new() {
                Bank = Type.Bank,
                Closed = false,
                Details = new() { Balance = 0 },
                ID = ID,
                Owner = await NecoDB.User.FirstOrDefaultAsync(U=>U.ID==S.UserID),
                Type = Type
            };

            NecoDB.Add(A);
            await NecoDB.SaveChangesAsync();
            return Ok(A);
        }

        //POST: BNKC
        [HttpPost("BNKC")]
        public async Task<IActionResult> BNKClose(BankAccountActionRequest BNKCRequest) {
            Session S = SessionManager.Manager.FindSession(BNKCRequest.SessionID);
            if (S == null) { return Unauthorized("Invalid session"); }

            BankAccount Account = await NecoDB.BankAccount
                .Include(A=> A.Owner)
                .Include(A=>A.Details)
                .FirstOrDefaultAsync(T => T.ID == BNKCRequest.BankAccountID);
            if (Account == null) { return NotFound("Requested Bank account not found"); }
            if (Account.Owner.ID != S.UserID) { return Unauthorized("Bank account does not belong to the session holder"); }
            if (Account.Details.Balance > 0) { return BadRequest("Bank account is holding funds. These must be moved before closing the account"); }
            if (Account.Details.Balance < 0) { return BadRequest("Bank account is overdrafted. Debts must be paid before closing the account"); }

            Account.Closed = true;
            NecoDB.Update(Account);
            await NecoDB.SaveChangesAsync();

            return Ok(Account);
        }

        //POST: BNKL
        [HttpPost("BNKL")]
        public async Task<IActionResult> BNKLog(BankAccountActionRequest BNKLogRequest) {
            Session S = SessionManager.Manager.FindSession(BNKLogRequest.SessionID);
            if (S == null) { return Unauthorized("Invalid session"); }

            BankAccount Account = await NecoDB.BankAccount
                .Include(A => A.Owner)
                .FirstOrDefaultAsync(T => T.ID == BNKLogRequest.BankAccountID);
            if (Account == null) { return NotFound("Requested Bank account not found"); }
            if (Account.Owner.ID != S.UserID) { return Unauthorized("Bank account does not belong to the session holder"); }

            //Now that we have the bank accoutn and have verified everything, let's get the transactions
            List<Transaction> Transacts = await NecoDB.Transaction
                                          .Where(T => T.FromAccount.ID == Account.ID || T.ToAccount.ID == Account.ID)
                                          .OrderByDescending(T => T.Time).ToListAsync();
            return Ok(Transacts);
        }

        private bool BankAccountExists(string id) { return NecoDB.BankAccount.Any(e => e.ID == id); }

        private bool UserExists(string id) { return NecoDB.User.Any(e => e.ID == id); }
    }
}
