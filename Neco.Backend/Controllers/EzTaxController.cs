using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Igtampe.Neco.Common.EzTax;
using Igtampe.Neco.Common.EzTax.Subitems;
using Igtampe.Neco.Common;
using Igtampe.Neco.Data;

namespace Igtampe.Neco.Backend.Controllers {

    [Route("EzTax")]
    [ApiController]
    public class EzTaxController: Controller {
        private readonly NecoContext NecoDB;

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

        // POST: IncomeItem
        [HttpPost("IncomeItem")]
        public async Task<IActionResult> IncomeItemCreate(IncomeItem asset) {
            if (asset.ID != Guid.Empty) { BadRequest("Asset has an ID. Did you mean to edit it?"); }
            NecoDB.Add(asset);
            await NecoDB.SaveChangesAsync();
            return Ok(asset);
        }

        // PUT: IncomeItem/5
        [HttpPut("IncomeItem/{id}")]
        public async Task<IActionResult> IncomeItemEdit(Guid id, IncomeItem asset) {
            if (id != asset.ID) { return NotFound(); }

            try {
                NecoDB.Update(asset);
                await NecoDB.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!IncomeItemExists(asset.ID)) { return NotFound(); } else { throw; }
            }

            return Ok(asset);
        }

        //DELETE: IncomeItem/5
        [HttpDelete("IncomeItem/{id}")]
        public async Task<IActionResult> IncomeItemDelete(Guid id) {
            var asset = await NecoDB.IncomeItem.FindAsync(id);
            NecoDB.IncomeItem.Remove(asset);
            await NecoDB.SaveChangesAsync();
            return Ok(asset);
        }

        /// <summary>Generates TaxReport of the given user</summary>
        /// <param name="SessionID"></param>
        /// <returns></returns>
        [HttpPost("TaxReport")]
        public async Task<IActionResult> GenerateTR(Guid SessionID) {
            Session S = SessionManager.Manager.FindSession(SessionID);
            if (S == null) { return Unauthorized("Invalid session"); }

            User U = await NecoDB.User.Include(U => U.Type).FirstOrDefaultAsync(U => U.ID == S.UserID);

            //return Ok(TaxReport.GenerateTaxReport(U);
            return NotFound();
        }

        private bool IncomeItemExists(Guid id) { return NecoDB.IncomeItem.Any(e => e.ID == id); }
        #endregion



    }
}
