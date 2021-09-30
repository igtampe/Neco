using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Igtampe.Neco.Common.UMSAT;
using Igtampe.Neco.Common.UMSAT.Requests;
using Igtampe.Neco.Common;
using Igtampe.Neco.Data;

namespace Igtampe.Neco.Backend.Controllers {

    [Route("UMSAT")]
    [ApiController]
    public class UMSATController: Controller {

        private readonly NecoContext NecoDB;
        public UMSATController(NecoContext context) {NecoDB = context;}

        // GET: UMSAT
        [HttpGet]
        public async Task<IActionResult> Index(int? start, int? end) {
            int realstart = start!=null ? (int)start : 0;
            int realend = end != null ? (int)end : 20;
            return Ok(await NecoDB.Asset
                .Include(a => a.Plot).ThenInclude(m => m.District).ThenInclude(m => m.Country)
                .Include(a => a.Owner).ThenInclude(m=>m.Type).Skip(realstart).Take(realend-realstart).ToListAsync()); 
        }

        // GET: UMSAT/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid? id) {
            if (id == null) { return NotFound(); }

            var asset = await NecoDB.Asset
                .Include(a => a.Plot).ThenInclude(m => m.District).ThenInclude(m => m.Country)
                .Include(a => a.Owner).ThenInclude(m => m.Type).FirstOrDefaultAsync(m => m.ID == id);

            if (asset == null) { return NotFound(); }

            return Ok(asset);
        }

        //POST: UMSAT
        [HttpPost]
        public async Task<IActionResult> CreateEdit(Guid SessionID) {
            Session S = SessionManager.Manager.FindSession(SessionID);
            if (S == null) { return Unauthorized("Invalid session"); }

            var asset = await NecoDB.Asset
                .Include(a => a.Plot).ThenInclude(m => m.District).ThenInclude(m => m.Country)
                .Include(a => a.Owner).ThenInclude(m => m.Type).Where(m => m.Owner.ID==S.UserID)
                .OrderByDescending(m=>m.UpdateDate).ToListAsync();
            
            return Ok(asset);
        }

        // POST: UMSAT/Mod
        [HttpPost("Mod")]
        public async Task<IActionResult> CreateEdit(AssetModRequest Request) {
            Session S = SessionManager.Manager.FindSession(Request.SessionID);
            if (S == null) { return Unauthorized("Invalid session"); }

            User U = await NecoDB.User.FirstOrDefaultAsync(U => U.ID == S.UserID);

            Asset A;

            if (Request.AssetID == Guid.Empty) {
                //This is a New Asset
                A = new() {
                    Complete = Request.Complete,
                    CreationDate = DateTime.Now,
                    Description = Request.Description,
                    Name = Request.Name,
                    Owner = U,
                    SpecificLocaiton=Request.SpecificLocation,
                    UpdateDate=DateTime.Now
                };

                NecoDB.Add(A);

            } else {

                //This is a modified asset.
                A = await NecoDB.Asset.Include(A=>A.Owner).FirstOrDefaultAsync(A => A.ID == Request.AssetID);
                if (A == null) { return NotFound("Asset was not found"); }
                if (A.Owner.ID != S.UserID) { return Unauthorized("Session owner isn't this asset's owner"); }
                
                //Update the asset details
                A.Complete = Request.Complete;
                A.Description = Request.Description;
                A.Name = Request.Name;
                A.SpecificLocaiton = Request.SpecificLocation;
                A.UpdateDate = DateTime.Now;

                NecoDB.Update(A);
            }

            //Now save the asset
            await NecoDB.SaveChangesAsync();
            return Ok(A);
        }

        // POST: UMSAT/Transfer
        [HttpPost("Transfer")]
        public async Task<IActionResult> Transfer(AssetTransferRequest Request) {
            Session S = SessionManager.Manager.FindSession(Request.SessionID);
            if (S == null) { return Unauthorized("Invalid session"); }

            Asset A = await NecoDB.Asset.Include(A => A.Owner).FirstOrDefaultAsync(A => A.ID == Request.AssetID);
            if (A == null) { return NotFound("Asset was not found"); }
            if (A.Owner.ID != S.UserID) { return Unauthorized("Session owner isn't this asset's owner"); }

            User U = await NecoDB.User.FirstOrDefaultAsync(U => U.ID == Request.NewOwnerID);
            if (U == null) { return NotFound("New owner was not found"); }

            A.Owner = U;
            NecoDB.Update(A);
            await NecoDB.SaveChangesAsync();

            return Ok(A);
        }

        //POST: UMSAT/Del
        [HttpPost("Del")]
        public async Task<IActionResult> Delete(AssetDeleteRequest Request) {
            Session S = SessionManager.Manager.FindSession(Request.SessionID);
            if (S == null) { return Unauthorized("Invalid session"); }

            Asset A = await NecoDB.Asset.Include(A => A.Owner).FirstOrDefaultAsync(A => A.ID == Request.AssetID);
            if (A == null) { return NotFound("Asset was not found"); }
            if (A.Owner.ID != S.UserID) { return Unauthorized("Session owner isn't this asset's owner"); }

            NecoDB.Remove(A);
            await NecoDB.SaveChangesAsync();
            return Ok(A);

            
        }
    }
}
