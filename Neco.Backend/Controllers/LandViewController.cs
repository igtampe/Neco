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
using System.IO;

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

        [NonAction]
        public async Task<IActionResult> CreatePlotReal(CreatePlotRequest Request, bool CheckOnly) {
            Session S = SessionManager.Manager.FindSession(Request.SessionID);
            if (S == null) { return Unauthorized("Invalid session"); }

            //Get the owner
            User U = await NecoDB.User.FindAsync(S.UserID);
            BankAccount B = await NecoDB.BankAccount
                .Include(B => B.Owner)
                .Include(B => B.Details)
                .FirstOrDefaultAsync(B => B.ID == Request.BankAccountID);

            if (B.Owner.ID != S.UserID) { return Unauthorized("Session owner does not own given bank account"); }

            Country C = await NecoDB.Country
                .Include(C => C.Districts).ThenInclude(D => D.DistrictBankAccount).ThenInclude(B => B.Owner) //Include every district and their bank accounts
                .Include(C => C.Districts).ThenInclude(D => D.DistrictBankAccount).ThenInclude(B => B.Details) //Include details because we're going to need it for the transaction.
                .Include(C => C.Districts).ThenInclude(D => D.Plots) //Include plots and their owners
                .FirstOrDefaultAsync(D => D.ID == Request.CountryID);
            if (C == null) { return NotFound("Could not find country"); }

            Plot MyPlot = new() {
                GraphicalPoints = Request.DefiningPoints,
                Name = Request.Name,
                Owner = U,
                Status = PlotStatus.NOT_FOR_SALE
            };

            //First validate the points
            if (!LandViewUtils.ValidatePoints(MyPlot.Points, 3)) {
                return BadRequest("Points are not valid. Fix them before creating");
            }

            //Ensure this plot is in the district its supposed to be in
            MyPlot.District = LandViewUtils.CalculatePlotDistrict(C, MyPlot);

            if (MyPlot.District == null) {
                return BadRequest("We could not calculate what district this plot is contained by. This could mean that this plot is not in a single district, is in federal" +
                                  " land, or is not in the right country. Fix this before creating this plot.");
            }

            //Ensure this plot doesn't intersect any other plots
            Plot ConflictingPlot = LandViewUtils.GetIntersectingPlot(MyPlot);

            if (ConflictingPlot != null) {
                return BadRequest($"The plot you created intersects with plot {ConflictingPlot.Name}. Fix this before creating this plot.");
            }

            //OK then we're good to go.

            //Now calculate the area, and calculate the price
            long Price = Convert.ToInt64(MyPlot.Area()) * MyPlot.District.PricePerSquareMeter;


            //If this is check only, return the price
            if (CheckOnly) { return Ok(Price); }

            //Otherwise, let's commit the transaction

            if (B.Details.Balance < Price) { return BadRequest("Bank account has insufficient funds"); }

            Transaction T = new() {
                Amount = Price,
                FromAccount = B,
                ToAccount = MyPlot.District.DistrictBankAccount,
                Name = $"PURCHASE OF PLOT {MyPlot.Name}",
                State = TransactionState.COMPLETED,
                Time = DateTime.Now
            };

            T.FromAccount.Details.Balance -= T.Amount;
            T.ToAccount.Details.Balance += T.Amount;

            NecoDB.Add(T);

            //Add a notification to the owner
            Notification N = new() {
                Read = false,
                Text = $"{U.Name} has forwarded payment for the creation of plot {MyPlot.Name}",
                Time = DateTime.Now,
                User = T.ToAccount.Owner
            };

            NecoDB.Add(N);

            //Finally add MyPlot
            NecoDB.Add(MyPlot);

            //And lastly, save to the database
            await NecoDB.SaveChangesAsync();

            //Finally, return the plot
            return Ok(MyPlot);

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

        #region Images

        [HttpGet("Images/Country/{id}.png")]
        public async Task<IActionResult> GetCountryImage(Guid ID) {
            //Get the country and include ***everything***

            Country C = await NecoDB.Country
                .Include(C => C.Districts).ThenInclude(D => D.Plots)
                .Include(C => C.Roads)
                .FirstOrDefaultAsync(D => D.ID == ID);
            
            if (C == null) { return NotFound("Could not find country"); }

            var ReturnImage = await Task.Run(() => GraphicsEngine.GenerateDetailedCountryImage(C));
            return File(ImageToPngByteArray(ReturnImage), "image/png");
        }

        [HttpGet("Images/District/{id}.png")]
        public async Task<IActionResult> GetDistrictImage(Guid ID) {
            //Get the district and include ***everything***
            District D = await NecoDB.District
                .Include(D => D.Country).ThenInclude(C => C.Districts).ThenInclude(D => D.Plots)
                .Include(D => D.Country).ThenInclude(C => C.Roads)
                .Include(D => D.Plots)
                .FirstOrDefaultAsync(D => D.ID == ID);

            if (D == null) { return NotFound("Could not find District"); }

            var ReturnImage = await Task.Run(() => GraphicsEngine.GenerateDistrictImage(D));
            return File(ImageToPngByteArray(ReturnImage), "image/png");
        }

        [HttpGet("Images/Plot/{id}.png")]
        public async Task<IActionResult> GetPlotImage(Guid ID) {
            //Get the plot and include ***everything***
            Plot P = await NecoDB.Plot
                .Include(P => P.District).ThenInclude(D => D.Country).ThenInclude(C => C.Districts).ThenInclude(D=>D.Plots)
                .Include(P => P.District).ThenInclude(D => D.Country).ThenInclude(C => C.Roads)
                .Include(P => P.District).ThenInclude(D => D.Plots)
                .FirstOrDefaultAsync(P => P.ID == ID);

            if (P == null) { return NotFound("Could not find Plot"); }

            var ReturnImage = await Task.Run(() => GraphicsEngine.GeneratePlotImage(P));
            return File(ImageToPngByteArray(ReturnImage), "image/png");
        }

        [HttpPost("Images/plot/Preview.png")]
        public async Task<IActionResult> GetPlotPreviewImage(CreatePlotRequest PR) {

            //We actually don't need any information relating to session ID or anything. I'm just reusing this 
            //request object because it contains a list of points and a country ID.

            //Get the country
            Country C = await NecoDB.Country
                .Include(C => C.Districts).ThenInclude(D => D.Plots)
                .Include(C => C.Roads)
                .FirstOrDefaultAsync(C => C.ID == PR.CountryID);
            if (C == null) { return NotFound("Country could not be found"); }
            if (C.Districts.Count == 0) { return BadRequest("There are no districts in the specified country. We cannot fulfill this request"); }

            Plot P = new() {
                GraphicalPoints = PR.DefiningPoints,
                District = C.Districts.First() //Here it doesn't actually matter what district this belongs to, as long as it belongs to the country
            };

            var ReturnImage = await Task.Run(() => GraphicsEngine.GeneratePlotImage(P));
            return File(ImageToPngByteArray(ReturnImage), "image/png");
        }

        private static byte[] ImageToPngByteArray(System.Drawing.Image I) {
            using MemoryStream ms = new();
            I.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }

        #endregion

    }
}
