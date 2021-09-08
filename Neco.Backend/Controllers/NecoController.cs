using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Igtampe.Neco.Common;
using Igtampe.Neco.Data;

namespace Igtampe.Neco.Backend.Controllers {
    [Route("")]
    [ApiController]
    public class NecoController: Controller {
        private readonly NecoContext _context;

        private readonly Random UserIDRandomizer = new();

        public NecoController(NecoContext context) { _context = context; }

        #region Bank
        // GET: UMSAT
        [HttpGet("Bank")]
        public async Task<IActionResult> BankIndex() { return Ok(await _context.Bank.ToListAsync()); }

        // GET: Banks/UMSNB
        [HttpGet("Bank/{id}")]
        public async Task<IActionResult> BankDetails(string id) {
            if (id == null) { return NotFound(); }

            var asset = await _context.Bank.FirstOrDefaultAsync(m => m.Id == id);
            if (asset == null) { return NotFound(); }

            return Ok(asset);
        }

        // GET: Bank/UMSNB/Accs
        [HttpGet("Bank/{id}/Accs")]
        public async Task<IActionResult> BankBankAccs(string id) {
            if (id == null) { return NotFound(); }

            var Types = await _context.BankAccount
                //.Include(m=>m.Type)
                //.Where(m => m.Bank.Id == id)
                .ToListAsync();

            return Ok(Types);
        }

        // GET: Bank/UMSNB/Types
        [HttpGet("Bank/{id}/Types")]
        public async Task<IActionResult> BankBankAccountTypes(string id) {
            if (id == null) { return NotFound(); }

            var Types = await _context.BankAccountType.Where(m => m.Bank.Id == id).ToListAsync();

            return Ok(Types);
        }

        #endregion

        #region BankAccountType
        // GET: UMSAT
        [HttpGet("BankAccountType")]
        public async Task<IActionResult> BankAccountTypeIndex() { return Ok(await _context.BankAccountType.Include(M => M.Bank).ToListAsync()); }

        // GET: UMSAT/5
        [HttpGet("BankAccountType/{id}")]
        public async Task<IActionResult> BankAccountTypeDetails(Guid? id) {
            if (id == null) { return NotFound(); }

            var asset = await _context.BankAccountType.Include(M => M.Bank).FirstOrDefaultAsync(m => m.Id == id);
            if (asset == null) { return NotFound(); }

            return Ok(asset);
        }

        #endregion

        #region BankAccount
        // GET: UMSAT
        [HttpGet("BankAccount")] //TODO: Remove before release
        public async Task<IActionResult> Index() {
            return Ok(await _context.BankAccount
                .Include(m => m.Owner).ThenInclude(m => m.Type)
                //.Include(m => m.Bank)
                //.Include(m => m.Type)
                .ToListAsync());
        }

        // GET: UMSAT/5
        [HttpGet("BankAccount/{id}")]
        public async Task<IActionResult> Details(Guid? id) {
            if (id == null) { return NotFound(); }

            var asset = await _context.BankAccount
                .Include(m => m.Owner).ThenInclude(m => m.Type)
                //.Include(m => m.Bank)
                //.Include(m => m.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asset == null) { return NotFound(); }

            return Ok(asset);
        }

        // POST: UMSAT
        [HttpPost("BankAccount")]
        public async Task<IActionResult> CreateBankAccount(BankAccountDetail asset) {
            if (asset.Id != Guid.Empty) { BadRequest("Asset has an ID. Did you mean to edit it?"); }
            asset.Id = Guid.NewGuid();
            _context.Add(asset);
            await _context.SaveChangesAsync();
            return Created($"BankAccount/{asset.Id}", asset);
        }

        // PUT: UMSAT/5
        [HttpPut("BankAccount/{id}")]
        public async Task<IActionResult> Edit(Guid id, BankAccountDetail asset) {
            if (id != asset.Id) { return NotFound(); }

            try {
                _context.Update(asset);
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!BankAccountExists(asset.Id)) { return NotFound(); } else { throw; }
            }

            return Ok(asset);
        }

        //DELETE: UMSAT/5
        [HttpDelete("BankAccount/{id}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id) {
            var asset = await _context.BankAccount.FindAsync(id);
            if (asset.Balance > 0) { return BadRequest("Bank account is not empty. Empty it out before trying to remove it"); }
            _context.BankAccount.Remove(asset);
            await _context.SaveChangesAsync();
            return Ok(asset);
        }

        private bool BankAccountExists(Guid id) { return _context.BankAccount.Any(e => e.Id == id); }
        #endregion

        #region CertifiedItem
        // GET: UMSAT
        [HttpGet("CertifiedItem")]
        public async Task<IActionResult> CertifiedItemIndex() {
            return Ok(await _context.CertifiedItem
                .Include(m => m.CertifiedBy).ThenInclude(m => m.Type)
                .ToListAsync());
        }

        // GET: UMSAT/5
        [HttpGet("CertifiedItem/{id}")]
        public async Task<IActionResult> CertifiedItemDetails(Guid? id) {
            if (id == null) { return NotFound(); }

            var asset = await _context.CertifiedItem.Include(m => m.CertifiedBy).ThenInclude(m => m.Type).FirstOrDefaultAsync(m => m.Id == id);
            if (asset == null) { return NotFound(); }

            return Ok(asset);
        }

        // POST: UMSAT
        [HttpPost("CertifiedItem")]
        public async Task<IActionResult> CreateNotification(CertifiedItem asset) {
            if (asset.Id != Guid.Empty) { BadRequest("Asset has an ID. Did you mean to edit it?"); }
            asset.Id = Guid.NewGuid();
            _context.Add(asset);
            await _context.SaveChangesAsync();
            return Created($"CertifiedItem/{asset.Id}", asset);
        }

        #endregion

        #region Notification
        // GET: UMSAT
        [HttpGet("Notification")] //TODO: Remove later
        public async Task<IActionResult> NotificationIndex() {
            return Ok(await _context.Notification
                .Include(m => m.User).ThenInclude(m => m.Type)
                .ToListAsync());
        }

        // GET: UMSAT/5
        [HttpGet("Notification/{id}")]
        public async Task<IActionResult> NotificationDetails(Guid? id) {
            if (id == null) { return NotFound(); }

            var asset = await _context.Notification.Include(m => m.User).ThenInclude(m => m.Type)
            .FirstOrDefaultAsync(m => m.Id == id);
            if (asset == null) { return NotFound(); }

            return Ok(asset);
        }

        // POST: UMSAT
        [HttpPost("Notification")]
        public async Task<IActionResult> NotificationCreate(Notification asset) {
            if (asset.Id != Guid.Empty) { BadRequest("Asset has an ID. Did you mean to edit it?"); }
            asset.Id = Guid.NewGuid();
            _context.Add(asset);
            await _context.SaveChangesAsync();
            return Created($"Notification/{asset.Id}", asset);
        }

        //DELETE: UMSAT/5
        [HttpDelete("Notification/{id}")]
        public async Task<IActionResult> NotificationDelete(Guid id) {
            var asset = await _context.Notification.FindAsync(id);
            _context.Notification.Remove(asset);
            await _context.SaveChangesAsync();
            return Ok(asset);
        }

        #endregion

        #region Transaction
        // GET: UMSAT
        [HttpGet("Transaction")] //TODO: Also remove
        public async Task<IActionResult> TransactionIndex() {
            return Ok(await _context.Transaction
                //.Include(m => m.FromAccount.Id)
                .Include(m => m.FromUser).ThenInclude(m => m.Type)
                //.Include(m => m.ToBankAccount.Id)
                .Include(m => m.ToUser).ThenInclude(m => m.Type)
                .ToListAsync());
        }

        // GET: UMSAT/5
        [HttpGet("Transaction/{id}")]
        public async Task<IActionResult> TransactionDetails(Guid? id) {
            if (id == null) { return NotFound(); }

            var asset = await _context.Transaction
                    //.Include(m => m.FromAccount.Id)
                    .Include(m => m.FromUser).ThenInclude(m => m.Type)
                    .Include(m => m.ToBankAccount)
                    .Include(m => m.ToUser).ThenInclude(m => m.Type)
                    .FirstOrDefaultAsync(m => m.Id == id);
            if (asset == null) { return NotFound(); }

            return Ok(asset);
        }

        // POST: UMSAT
        [HttpPost("Transaction")]
        public async Task<IActionResult> TransactionCreate(Transaction asset) {
            if (asset.Id != Guid.Empty) { BadRequest("Asset has an ID. Did you mean to edit it?"); }
            asset.Id = Guid.NewGuid();
            _context.Add(asset);
            await _context.SaveChangesAsync();
            return Created($"Transaction/{asset.Id}", asset);
        }

        // PUT: UMSAT/5
        [HttpPost("Transaction/Execute/{id}")]
        public async Task<IActionResult> ExecuteTransaction(Guid id) {

//            var transaction = await _context.Transaction.FirstOrDefaultAsync(m => m.Id == id);
//            if (transaction == null) { return NotFound(); }
//
//            //Now then:
//            if (transaction.FromAccount.Balance < transaction.Amount) {
//
//                transaction.Failed = true;
//                _context.Update(transaction);
//                await _context.SaveChangesAsync();
//
//                return BadRequest("Origin of this transaction does not have enough funds");
//            }
//
//            if (transaction.FromUser.Equals(transaction.ToUser)) { transaction.Taxable = false; }
//            //Ensure transfers are non-taxed when users are transfering money between their accounts
//
//            //Actually do the transaction
//            transaction.FromAccount.Balance -= transaction.Amount;
//            transaction.ToBankAccount.Balance += transaction.Amount;
//
//            //Add a notification to the to user if it's not the same
//            if (!transaction.FromUser.Equals(transaction.ToUser)) {
//                if (transaction.ToUser.Notifications == null) { transaction.ToUser.Notifications = new List<Notification>(); }
//                transaction.ToUser.Notifications.Add(new Notification() {
//                    Id = Guid.NewGuid(),
//                    Read = false,
//                    Time = DateTime.Now,
//                    User = transaction.ToUser,
//                    Text = $"{transaction.FromUser} sent you {transaction.Amount:N0}p to your {transaction.ToBankAccount.Type.Name} account"
//                });
//
//            }
//
//            transaction.Executed = true;
//
//            _context.Update(transaction);
//            await _context.SaveChangesAsync();
//
            return Ok();
        }

        #endregion

        #region User
        // GET: USER
        [HttpGet("User")]
        public async Task<IActionResult> UserIndex() { return Ok(await _context.User.Include(m=> m.Type).ToListAsync()); }

        // GET: USER/5
        [HttpGet("User/{id}")]
        public async Task<IActionResult> UserDetails(string id) {
            if (id == null) { return NotFound(); }

            var asset = await _context.User.Include(m => m.Type).FirstOrDefaultAsync(m => m.Id == id);
            if (asset == null) { return NotFound(); }

            return Ok(asset);
        }

        // GET: USER/57174/NOTIFS
        [HttpGet("User/{id}/Notifs")]
        public async Task<IActionResult> UserNotifications(string id) {
            if (id == null) { return NotFound(); }

            var Types = await _context.Notification.Where(m => m.User.Id == id).ToListAsync();

            return Ok(Types);
        }

        // GET: USER/57174/Accs
        [HttpGet("User/{id}/Accs")]
        public async Task<IActionResult> UserAccounts(string id) {
            if (id == null) { return NotFound(); }

            var Types = await _context.BankAccount.Where(m => m.Owner.Id == id).ToListAsync();

            return Ok(Types);
        }

        // POST: UMSAT
        [HttpPost("User")]
        public async Task<IActionResult> UserCreate(User asset) {
            if (string.IsNullOrEmpty(asset.Id)) { BadRequest("User has an ID. Did you mean to edit it?"); }
            string ID;
            do {
                ID = "";
                while (ID.Length < 5) { ID += UserIDRandomizer.Next(10); }
            } while (UserExists(ID));

            asset.Id = ID;
            _context.Add(asset);
            await _context.SaveChangesAsync();
            return Created($"User/{asset.Id}", asset);
        }

        private bool UserExists(string id) { return _context.User.Any(e => e.Id == id); }
        #endregion

        #region UserType       
        // GET: UMSAT
        [HttpGet("UserType")]
        public async Task<IActionResult> UserTypeIndex() { return Ok(await _context.UserType.ToListAsync()); }

        // GET: UMSAT/5
        [HttpGet("UserType/{id}")]
        public async Task<IActionResult> UserTypeDetails(Guid? id) {
            if (id == null) { return NotFound(); }

            var asset = await _context.UserType.FirstOrDefaultAsync(m => m.Id == id);
            if (asset == null) { return NotFound(); }

            return Ok(asset);
        }
        #endregion

    }
}
