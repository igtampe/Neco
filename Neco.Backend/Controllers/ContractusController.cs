using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Igtampe.Neco.Common.Contractus;
using Igtampe.Neco.Data;
using Igtampe.Neco.Common.Contractus.Requests;

namespace Igtampe.Neco.Backend.Controllers {

    [Route("Contractus")]
    [ApiController]
    public class ContractusController: Controller {
        private readonly NecoContext NecoDB;

        public ContractusController(NecoContext context) { NecoDB = context; }

        // GET: CONTRACTUS
        [HttpGet]
        public async Task<IActionResult> Index() { return Ok(await NecoDB.Contract
            .Include(m=>m.FromUser).ThenInclude(m=>m.Type)
            .Include(m=>m.TopBidder).ThenInclude(m => m.Type)
            .Where(m=> m.Status==ContractStatus.AUCTION)
            .ToListAsync()); }

        // POST: CONTRACTUS
        [HttpPost]
        public async Task<IActionResult> UserIndex(Guid SessionID) {
            Session S = SessionManager.Manager.FindSession(SessionID);
            if (S == null) { return Unauthorized("Invalid session"); }

            var contract = await NecoDB.Contract
                .Include(m => m.FromUser).ThenInclude(m => m.Type)
                .Include(m => m.TopBidder).ThenInclude(m => m.Type)
                .FirstOrDefaultAsync(m => m.FromUser.ID == S.UserID || (m.TopBidder.ID==S.UserID && m.Status != ContractStatus.AUCTION));
            if (contract == null) { return NotFound(); }

            return Ok(contract);
        }

        // POST: CONTRACTUS/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create(NewContractRequest Request) {
            Session S = SessionManager.Manager.FindSession(Request.SessionID);
            if (S == null) { return Unauthorized("Invalid session"); }

            Contract C = new() {
                ID = Guid.NewGuid(),
                Amount = long.MaxValue,
                Description = Request.Description,
                Name = Request.Name,
                Status = ContractStatus.AUCTION,
                TopBidder = null,
                FromUser = await NecoDB.User.FirstOrDefaultAsync(A=>A.ID == S.UserID)
            };

            NecoDB.Add(C);
            await NecoDB.SaveChangesAsync();
            return Ok(C);
        }

        // POST: CONTRACTUS/Bid
        [HttpPost("Bid")]
        public async Task<IActionResult> Bid(AddBidRequest Request) {
            Session S = SessionManager.Manager.FindSession(Request.SessionID);
            if (S == null) { return Unauthorized("Invalid session"); }

            //Load the contract
            Contract C = await NecoDB.Contract
                .Include(m => m.FromUser).ThenInclude(m => m.Type)
                .Include(m => m.TopBidder).ThenInclude(m => m.Type)
                .FirstOrDefaultAsync(C => C.ID == Request.ContractID);
            if (C == null) { return NotFound(); }

            //Contract in Auction State?
            if (C.Status != ContractStatus.AUCTION) { return BadRequest("Contract is not in Auction state"); }

            //Bid is lower?
            if (C.Amount >= Request.Bid) { return BadRequest($"Provided bid is not lower than the current bid of {C.Amount}"); }

            C.Amount = Request.Bid;
            C.TopBidder = await NecoDB.User.FirstOrDefaultAsync(U => U.ID == S.UserID);
            NecoDB.Update(C);
            await NecoDB.SaveChangesAsync();

            return Ok(C);
        }

        //POST: CONTRACTUS/Change
        [HttpPost("Change")]
        public async Task<IActionResult> ChangeStatus(ChangeContractStatusRequest Request) {
            Session S = SessionManager.Manager.FindSession(Request.SessionID);
            if (S == null) { return Unauthorized("Invalid session"); }

            //Load the contract
            Contract C = await NecoDB.Contract
                .Include(C=>C.FromUser).ThenInclude(m => m.Type)
                .Include(C=>C.TopBidder).ThenInclude(m => m.Type)
                .FirstOrDefaultAsync(C => C.ID == Request.ContractID);
            if (C == null) { return NotFound(); }

            switch (C.Status) {
                case ContractStatus.AUCTION:
                    if (S.UserID != C.FromUser.ID) { return Unauthorized("User is unauthorized to modify contract state in its current state"); }
                    if (Request.NewStatus == ContractStatus.CANCELLED) {
                        C.Status = ContractStatus.CANCELLED;
                        break;
                    } else if (Request.NewStatus == ContractStatus.AWARDED) {

                        if (C.TopBidder == null) { return BadRequest("Cannot award the contract! Nobody has bidded!"); }
                        C.Status = ContractStatus.AWARDED;
                        break;
                    } else { return BadRequest("Invalid status to change to was provided"); }
                case ContractStatus.AWARDED:
                    if (S.UserID != C.TopBidder.ID) { return Unauthorized("User is unauthorized to modify contract state in its current state"); }
                    if (Request.NewStatus == ContractStatus.CANCELLED) {
                        C.Status = ContractStatus.CANCELLED;
                        break;
                    } else if (Request.NewStatus == ContractStatus.PENDING_PAYMENT) {
                        C.Status = ContractStatus.PENDING_PAYMENT;
                        break;
                    } else { return BadRequest("Invalid status to change to was provided"); }
                case ContractStatus.PENDING_PAYMENT:
                    if (S.UserID != C.TopBidder.ID) { return Unauthorized("User is unauthorized to modify contract state in its current state"); }
                    if (Request.NewStatus == ContractStatus.COMPELTED) {
                        C.Status = ContractStatus.COMPELTED;
                        break;
                    } else { return BadRequest("Invalid status to change to was provided"); }
                case ContractStatus.COMPELTED:
                case ContractStatus.CANCELLED:
                default:
                    return BadRequest("Contract is in an Invalid or Final state. We can't change its status.");
            }

            NecoDB.Update(C);
            await NecoDB.SaveChangesAsync();

            return Ok(C);
        }
    }
}
