using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Igtampe.Neco.Common;
using Igtampe.Neco.Common.LandView;
using Igtampe.Neco.Common.LandView.Requests;
using Igtampe.Neco.Data;

namespace Igtampe.Neco.Backend.Controllers {

    [Route("LandView")]
    [ApiController]
    public class LandViewController: Controller {
        private readonly NecoContext NecoDB;

        public LandViewController(NecoContext context) { NecoDB = context; }

        #region Country

        // GET: Country
        [HttpGet("Country")]
        public async Task<IActionResult> CountryIndex() { return Ok(await NecoDB.Country.ToListAsync()); }

        // GET: Country/5
        [HttpGet("Country/{id}")]
        public async Task<IActionResult> CountryDetails(Guid? id) {
            if (id == null) { return NotFound(); }

            var asset = await NecoDB.Country.FirstOrDefaultAsync(m => m.ID == id);
            if (asset == null) { return NotFound(); }

            return Ok(asset);
        }

        // GET: Country/5/Districts
        [HttpGet("Country/{id}/Districts")]
        public async Task<IActionResult> CountryDistricts(Guid? id) {
            if (id == null) { return NotFound(); }

            var Districts = await NecoDB.District.Where(m => m.Country.ID == id).ToListAsync();

            return Ok(Districts);
        }

        // GET: Country/5/Roads
        [HttpGet("Country/{id}/Roads")]
        public async Task<IActionResult> CountryRoads(Guid? id) { //take me home!
            if (id == null) { return NotFound(); }

            var Roads = await NecoDB.District.Where(m => m.Country.ID == id).ToListAsync();

            return Ok(Roads);
        }

        #endregion

        #region District

        // GET: District
        [HttpGet("District")]
        public async Task<IActionResult> DistrictIndex() { return Ok(await NecoDB.District.Include(m=> m.Country).ToListAsync()); }

        // GET: District/5
        [HttpGet("District/{id}")]
        public async Task<IActionResult> DistrictDetails(Guid? id) {
            if (id == null) { return NotFound(); }

            var asset = await NecoDB.District
                .Include(m => m.Country)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (asset == null) { return NotFound(); }

            return Ok(asset);
        }

        // GET: District/5/Plots
        [HttpGet("District/{id}/Plots")]
        public async Task<IActionResult> DistrictPlots(Guid? id) { 
            if (id == null) { return NotFound(); }

            var Plots = await NecoDB.Plot.Where(m => m.District.ID == id).ToListAsync();

            return Ok(Plots);
        }

        #endregion

        #region Plot

        // GET: District
        [HttpGet("Plot")]
        public async Task<IActionResult> PlotIndex() { return Ok(await NecoDB.Plot
            .Include(m => m.District)
            .Include(m => m.District).ThenInclude(m => m.Country)
            .Include(m=>m.Owner).ThenInclude(m=>m.Type)
            .ToListAsync()); }

        // GET: District/5
        [HttpGet("Plot/{id}")]
        public async Task<IActionResult> PlotDetails(Guid? id) {
            if (id == null) { return NotFound(); }

            var asset = await NecoDB.Plot
                .Include(m => m.District).ThenInclude(m => m.Country)
                .Include(m => m.Owner).ThenInclude(m => m.Type)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (asset == null) { return NotFound(); }

            return Ok(asset);
        }

        public async Task<IActionResult> CreatePlotReal(CreatePlotRequest Request, bool CheckOnly) {
            Session S = SessionManager.Manager.FindSession(Request.SessionID);
            if (S == null) { return Unauthorized("Invalid session"); }

            //We *really* need to figure out how the heck to figure out if two polygons intersect
            //If we can't do this, the entire thing is screwed TODO
            throw new NotImplementedException();
        }

        // POST: Plot/Create
        [HttpPost("Plot/Create")]
        public async Task<IActionResult> PlotCreate(CreatePlotRequest Request) { return await CreatePlotReal(Request,false); }

        // POST: Plot/Check
        [HttpPost("Plot/Check")]
        public async Task<IActionResult> PlotCheck(CreatePlotRequest Request) { return await CreatePlotReal(Request, true); }


        // POST: Plot/Transfer
        [HttpPost("Plot/Transfer")]
        public async Task<IActionResult> PlotTransfer(TransferPlotOwnershipRequest Request) {
            Session S = SessionManager.Manager.FindSession(Request.SessionID);
            if (S == null) { return Unauthorized("Invalid session"); }

            //Retrieve Plot
            Plot P = await NecoDB.Plot
                .Include(m => m.District)
                .Include(m => m.District).ThenInclude(m => m.Country)
                .Include(m => m.Owner).ThenInclude(m => m.Type)
                .FirstOrDefaultAsync(p => p.ID == Request.PlotID);
            if (P == null) { return NotFound("Could not find plot"); }
            if (P.Owner.ID != S.UserID) { return Unauthorized("Session owner doesn't own this plot"); }

            //Retrieve new owner
            User U = await NecoDB.User
                .Include(U => U.Type)
                .FirstOrDefaultAsync(U => U.ID == Request.NewOwnerID);
            if (U == null) { return NotFound("Could not find new owner"); }

            //Update the owner
            P.Owner = U;

            //Update the Database
            NecoDB.Update(P);
            await NecoDB.SaveChangesAsync();
            
            //Return the plot
            return Ok(P);
        }

        //POST: Plot/Status
        [HttpPost("Plot/Status")]
        public async Task<IActionResult> PlotChangeStatus(ChangePlotStatusRequest Request) {
            Session S = SessionManager.Manager.FindSession(Request.SessionID);
            if (S == null) { return Unauthorized("Invalid session"); }

            //Retrieve Plot
            Plot P = await NecoDB.Plot
                .Include(m => m.District)
                .Include(m => m.District).ThenInclude(m => m.Country)
                .Include(m => m.Owner).ThenInclude(m => m.Type)
                .FirstOrDefaultAsync(p => p.ID == Request.PlotID);
            if (P == null) { return NotFound("Could not find plot"); }
            if (P.Owner.ID != S.UserID) { return Unauthorized("Session owner doesn't own this plot"); }

            //Update the owner
            P.Status = Request.NewStatus;

            //Update the Database
            NecoDB.Update(P);
            await NecoDB.SaveChangesAsync();

            //Return the plot
            return Ok(P);
        }

        #endregion

    }
}
