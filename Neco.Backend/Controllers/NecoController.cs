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

        private Random UserIDRandomizer = new();

        public NecoController(NecoContext context) { _context = context; }

        #region Bank
        // GET: UMSAT
        [HttpGet("Bank")]
        public async Task<IActionResult> BankIndex() { return Ok(await _context.Banks.ToListAsync()); }

        // GET: UMSAT/5
        [HttpGet("Bank/{id}")]
        public async Task<IActionResult> BankDetails(string? id) {
            if (id == null) { return NotFound(); }

            var asset = await _context.Banks.FirstOrDefaultAsync(m => m.Id == id);
            if (asset == null) { return NotFound(); }

            return Ok(asset);
        }

        #endregion

        #region BankAccountType
        // GET: UMSAT
        [HttpGet("BankAccountType")]
        public async Task<IActionResult> BankAccountTypeIndex() { return Ok(await _context.BankAccountTypes.ToListAsync()); }

        // GET: UMSAT/5
        [HttpGet("BankAccountType/{id}")]
        public async Task<IActionResult> BankAccountTypeDetails(Guid? id) {
            if (id == null) { return NotFound(); }

            var asset = await _context.BankAccountTypes.FirstOrDefaultAsync(m => m.Id == id);
            if (asset == null) { return NotFound(); }

            return Ok(asset);
        }

        #endregion

        #region BankAccount
        // GET: UMSAT
        [HttpGet("BankAccount")] //TODO: Remove before release
        public async Task<IActionResult> Index() { return Ok(await _context.BankAccounts.ToListAsync()); }

        // GET: UMSAT/5
        [HttpGet("BankAccount/{id}")]
        public async Task<IActionResult> Details(Guid? id) {
            if (id == null) { return NotFound(); }

            var asset = await _context.BankAccounts.FirstOrDefaultAsync(m => m.Id == id);
            if (asset == null) { return NotFound(); }

            return Ok(asset);
        }

        // POST: UMSAT
        [HttpPost("BankAccount")]
        public async Task<IActionResult> Create(BankAccount asset) {
            if (asset.Id != Guid.Empty) { BadRequest("Asset has an ID. Did you mean to edit it?"); }
            asset.Id = Guid.NewGuid();
            _context.Add(asset);
            await _context.SaveChangesAsync();
            return Created($"BankAccount/{asset.Id}", asset);
        }

        // PUT: UMSAT/5
        [HttpPut("BankAccount/{id}")]
        public async Task<IActionResult> Edit(Guid id, BankAccount asset) {
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
            var asset = await _context.BankAccounts.FindAsync(id);
            if (asset.Balance > 0) { return BadRequest("Bank account is not empty. Empty it out before trying to remove it"); }
            _context.BankAccounts.Remove(asset);
            await _context.SaveChangesAsync();
            return Ok(asset);
        }

        private bool BankAccountExists(Guid id) { return _context.BankAccounts.Any(e => e.Id == id); }
        #endregion

        #region CertifiedItem
        // GET: UMSAT
        [HttpGet("CertifiedItem")]
        public async Task<IActionResult> CertifiedItemIndex() { return Ok(await _context.CertifiedItems.ToListAsync()); }

        // GET: UMSAT/5
        [HttpGet("CertifiedItem/{id}")]
        public async Task<IActionResult> CertifiedItemDetails(Guid? id) {
            if (id == null) { return NotFound(); }

            var asset = await _context.CertifiedItems.FirstOrDefaultAsync(m => m.Id == id);
            if (asset == null) { return NotFound(); }

            return Ok(asset);
        }

        // POST: UMSAT
        [HttpPost("CertifiedItem")]
        public async Task<IActionResult> Create(CertifiedItem asset) {
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
        public async Task<IActionResult> NotificationIndex() { return Ok(await _context.Notifications.ToListAsync()); }

        // GET: UMSAT/5
        [HttpGet("Notification/{id}")]
        public async Task<IActionResult> NotificationDetails(Guid? id) {
            if (id == null) { return NotFound(); }

            var asset = await _context.Notifications.FirstOrDefaultAsync(m => m.Id == id);
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
            var asset = await _context.Notifications.FindAsync(id);
            _context.Notifications.Remove(asset);
            await _context.SaveChangesAsync();
            return Ok(asset);
        }

        #endregion

        #region Transaction
        // GET: UMSAT
        [HttpGet("Transaction")] //TODO: Also remove
        public async Task<IActionResult> TransactionIndex() { return Ok(await _context.Transactions.ToListAsync()); }

        // GET: UMSAT/5
        [HttpGet("Transaction/{id}")]
        public async Task<IActionResult> TransactionDetails(Guid? id) {
            if (id == null) { return NotFound(); }

            var asset = await _context.Transactions.FirstOrDefaultAsync(m => m.Id == id);
            if (asset == null) { return NotFound(); }

            return Ok(asset);
        }

        // POST: UMSAT
        [HttpPost("Transaction")]
        public async Task<IActionResult> Create(Transaction asset) {
            if (asset.Id != Guid.Empty) { BadRequest("Asset has an ID. Did you mean to edit it?"); }
            asset.Id = Guid.NewGuid();
            _context.Add(asset);
            await _context.SaveChangesAsync();
            return Created($"Transaction/{asset.Id}", asset);
        }

        // PUT: UMSAT/5
        [HttpPost("Transaction/Execute/{id}")]
        public async Task<IActionResult> ExecuteTransaction(Guid id) {

            var transaction = await _context.Transactions.FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null) { return NotFound(); }

            //Now then:
            if (transaction.FromAccount.Balance < transaction.Amount) {

                transaction.Failed = true;
                _context.Update(transaction);
                await _context.SaveChangesAsync();

                return BadRequest("Origin of this transaction does not have enough funds"); 
            }

            if (transaction.FromUser.Equals(transaction.ToUser)) { transaction.Taxable = false; }  
            //Ensure transfers are non-taxed when users are transfering money between their accounts

            //Actually do the transaction
            transaction.FromAccount.Balance -= transaction.Amount;
            transaction.ToBankAccount.Balance += transaction.Amount;

            //Add a notification to the to user if it's not the same
            if (!transaction.FromUser.Equals(transaction.ToUser)) {
                if (transaction.ToUser.Notifications == null) { transaction.ToUser.Notifications = new List<Notification>(); }
                transaction.ToUser.Notifications.Add(new Notification() {
                    Id = new Guid(),
                    Read = false,
                    Time = DateTime.Now,
                    User = transaction.ToUser,
                    Text = $"{transaction.FromUser} sent you {transaction.Amount.ToString("N0")}p to your {transaction.ToBankAccount.Type.Name} account"
                }) ;
            
            }

            transaction.Executed = true;

            _context.Update(transaction);
            await _context.SaveChangesAsync();

            return Ok(transaction);
        }

        #endregion

        #region User
        // GET: UMSAT
        [HttpGet("User")]
        public async Task<IActionResult> UserIndex() { return Ok(await _context.Users.ToListAsync()); }

        // GET: UMSAT/5
        [HttpGet("User/{id}")]
        public async Task<IActionResult> UserDetails(string id) {
            if (id == null) { return NotFound(); }

            var asset = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (asset == null) { return NotFound(); }

            return Ok(asset);
        }

        // POST: UMSAT
        [HttpPost("User")]
        public async Task<IActionResult> Create(User asset) {
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

        private bool UserExists(string id) { return _context.Users.Any(e => e.Id == id); }
        #endregion

        #region UserType       
        // GET: UMSAT
        [HttpGet("UserType")]
        public async Task<IActionResult> UserTypeIndex() { return Ok(await _context.UserTypes.ToListAsync()); }

        // GET: UMSAT/5
        [HttpGet("UserType/{id}")]
        public async Task<IActionResult> UserTypeDetails(Guid? id) {
            if (id == null) { return NotFound(); }

            var asset = await _context.UserTypes.FirstOrDefaultAsync(m => m.Id == id);
            if (asset == null) { return NotFound(); }

            return Ok(asset);
        }
        #endregion

    }
}
