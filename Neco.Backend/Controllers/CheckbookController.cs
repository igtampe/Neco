using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Igtampe.Neco.Common;
using Igtampe.Neco.Data;
using Igtampe.Neco.Common.Requests;

namespace Igtampe.Neco.Backend.Controllers {

    [Route("Checkbook")]
    [ApiController]
    public class CheckbookController: Controller {
        private readonly NecoContext NecoDB;

        public CheckbookController(NecoContext context) { NecoDB = context; }

        // POST: Checkbook
        [HttpPost]
        public async Task<IActionResult> IndexFromUser(Guid SessionID) {
            Session S = SessionManager.Manager.FindSession(SessionID);
            if (S == null) { return Unauthorized("Invalid session"); }

            var asset = await NecoDB.CheckbookItem
                .Include(C => C.AttachedTransaciton).ThenInclude(AT => AT.FromAccount)
                .Include(C => C.AttachedTransaciton).ThenInclude(AT => AT.ToAccount)
                .Where(C=> (C.Type==CheckbookItem.ItemType.BILL && C.AttachedTransaciton.FromAccount.Owner.ID==S.UserID) || 
                      (C.Type==CheckbookItem.ItemType.CHECK && C.AttachedTransaciton.ToAccount.Owner.ID==S.UserID))
            .ToListAsync();
            if (asset == null) { return NotFound(); }
            return Ok(asset);
        }


        // POST: Checkbook/Send
        [HttpPost("Send")]
        public async Task<IActionResult> Create(CheckbookSendItemRequest CheckbookRequest ) {
            Session S = SessionManager.Manager.FindSession(CheckbookRequest.SessionID);
            if (S == null) { return Unauthorized("Invalid session"); }

            //Validate the internal transaction
            if (CheckbookRequest.TransactRequest.SessionID != CheckbookRequest.SessionID) { return BadRequest("Transaction Request Sesion ID and Checkbook Request Session ID do not match"); }

            //Get the bank Accounts
            BankAccount FromBank = await NecoDB.BankAccount
                .Include(BA=> BA.Owner)
                .FirstOrDefaultAsync(BA => BA.ID == CheckbookRequest.TransactRequest.FromBankID);
            if (FromBank == null) { return NotFound("From Bank was not found"); }

            BankAccount ToBank = await NecoDB.BankAccount
                .Include(BA => BA.Owner)
                .FirstOrDefaultAsync(BA => BA.ID == CheckbookRequest.TransactRequest.ToBankID);
            if (ToBank == null) { return NotFound("From Bank was not found"); }

            if (CheckbookRequest.ItemType == CheckbookItem.ItemType.BILL) {
                if (ToBank.Owner.ID == S.UserID) { return BadRequest("Bill was not addressed to account Session Owner owns"); }
            } else if (CheckbookRequest.ItemType == CheckbookItem.ItemType.CHECK) {
                if (FromBank.Owner.ID == S.UserID) { return BadRequest("Check was not from account Session Owner owns"); }
            } else { return BadRequest("Checkbook item type was not specified, or was unable to be found"); }

            Transaction T = new() {
                Amount = CheckbookRequest.TransactRequest.Amount,
                FromAccount = FromBank,
                ToAccount = ToBank,
                Name = string.IsNullOrWhiteSpace(CheckbookRequest.TransactRequest.Name) ? $"{CheckbookRequest.ItemType} FROM {FromBank.Owner.Name} to {ToBank.Owner.Name}" : CheckbookRequest.TransactRequest.Name,
                State = TransactionState.PENDING,
                Time = DateTime.Now
            };

            CheckbookItem C = new() {
                AttachedTransaciton = T,
                Comment = CheckbookRequest.Comment,
                Type = CheckbookRequest.ItemType,
                Variant = CheckbookRequest.Variant
            };

            NecoDB.Add(C);
            await NecoDB.SaveChangesAsync();
            
            return Ok(C);
        }

        // POST: Checkbook/Execute/{ID}
        [HttpPut("Execute/{ID}")]
        public async Task<IActionResult> Execute([FromBody]Guid SessionID, [FromQuery] Guid ID) {
            Session S = SessionManager.Manager.FindSession(SessionID);
            if (S == null) { return Unauthorized("Invalid session"); }

            CheckbookItem C = await NecoDB.CheckbookItem
                .Include(C => C.AttachedTransaciton).ThenInclude(AT => AT.FromAccount).ThenInclude(A=> A.Owner)
                .Include(C => C.AttachedTransaciton).ThenInclude(AT => AT.ToAccount).ThenInclude(A => A.Owner)
                .Include(C => C.AttachedTransaciton).ThenInclude(AT => AT.FromAccount).ThenInclude(A=>A.Details)
                .Include(C => C.AttachedTransaciton).ThenInclude(AT => AT.ToAccount)  .ThenInclude(A=>A.Details)
                .FirstOrDefaultAsync(C => C.ID == ID);

            if (C == null) { return NotFound("Requested checkbook item was not found"); }
            if (C.AttachedTransaciton.State == TransactionState.COMPLETED) { return BadRequest("Transaction already executed"); }

            if (C.Type == CheckbookItem.ItemType.BILL) {
                if (C.AttachedTransaciton.FromAccount.Owner.ID == S.UserID) { return Unauthorized("Session owner is unauthorized to execute this item"); }
            } else if (C.Type == CheckbookItem.ItemType.CHECK) {
                if (C.AttachedTransaciton.ToAccount.Owner.ID == S.UserID) { return Unauthorized("Session owner is unauthorized to execute this item"); }
            } else { throw new InvalidOperationException("For some reason, a check in the database has no type. Time to Panic"); }

            //More Validation
            if (C.AttachedTransaciton.FromAccount.Details.Balance < C.AttachedTransaciton.Amount) { return BadRequest("Insufficient Funds"); }
            if (C.AttachedTransaciton.FromAccount.Closed || C.AttachedTransaciton.ToAccount.Closed) { return BadRequest("One or more of the bank acounts in this transaction are closed."); }

            //Execute Transaction
            C.AttachedTransaciton.FromAccount.Details.Balance -= C.AttachedTransaciton.Amount;
            C.AttachedTransaciton.ToAccount.Details.Balance += C.AttachedTransaciton.Amount;
            C.AttachedTransaciton.State = TransactionState.COMPLETED;

            //Build base Notification
            Notification N = new() {
                Read = false,
                Time = DateTime.Now,
            };

            //Determine who's going to get the notif and what the text will be
            if (C.Type == CheckbookItem.ItemType.BILL) {
                N.User = C.AttachedTransaciton.ToAccount.Owner;
                N.Text = $"Bill you sent to {C.AttachedTransaciton.FromAccount.Owner.Name} for {C.AttachedTransaciton.Amount:N0}p has been paid!";
            } else if (C.Type == CheckbookItem.ItemType.CHECK) {
                N.User = C.AttachedTransaciton.FromAccount.Owner;
                N.Text = $"Check you made for {C.AttachedTransaciton.ToAccount.Owner.Name} for {C.AttachedTransaciton.Amount:N0}p has been cashed!";
            } else { throw new InvalidOperationException("For some reason, a check in the database has no type. Time to Panic"); }

            NecoDB.Update(C);
            NecoDB.Add(N);
            await NecoDB.SaveChangesAsync();

            return Ok(C);
        }
    }
}
