using Microsoft.AspNetCore.Mvc;
using Igtampe.Neco.Data;
using Igtampe.ChopoSessionManager;
using Microsoft.EntityFrameworkCore;
using Igtampe.Neco.Common.Assets;
using Igtampe.Neco.API.Requests;
using Igtampe.Neco.Common.Banking;
using Igtampe.Neco.Common.Taxes;

namespace Igtampe.Neco.API.Controllers {

    /// <summary>Controller that handles User operations</summary>
    [Route("API/Assets")]
    [ApiController]
    public class AssetController : ControllerBase {

        private readonly NecoContext DB;

        /// <summary>Creates a User Controller</summary>
        /// <param name="Context"></param>
        public AssetController(NecoContext Context) => DB = Context;

        /// <summary>Get a list of all assets in the Neco DB</summary>
        /// <param name="Type">Type (Building = 0, 1 = Unit)</param>
        /// <param name="Sort">Sort order of the returned list</param>
        /// <param name="Status">Status of the assets</param>
        /// <param name="Query">Search query for the name or description</param>
        /// <param name="Skip">Amount to skip</param>
        /// <param name="Take">Amount to take</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index(int? Type, IncomeItemSortType? Sort, AssetStatus? Status, string? Query, int? Skip, int? Take) => Type switch {
            0 => Ok(await GetAssets(DB.Building, Sort, Status, Query, Skip, Take)),
            1 => Ok(await GetAssets(DB.Unit, Sort, Status, Query, Skip, Take)),
            _ => Ok(await GetAssets(DB.Asset, Sort, Status, Query, Skip, Take)),
        };

        /// <summary>Get a list of all assets an account owns</summary>
        /// <param name="Account">Account to search for</param>
        /// <param name="Type">Type (Building = 0, 1 = Unit)</param>
        /// <param name="Sort">Sort order of the returned list</param>
        /// <param name="Status">Status of the assets</param>
        /// <param name="Query">Search query for the name or description</param>
        /// <param name="Skip">Amount to skip</param>
        /// <param name="Take">Amount to take</param>
        /// <returns></returns>
        [HttpGet("Account/{Account}")]
        public async Task<IActionResult> IndexByAccount(string Account, int? Type,
            IncomeItemSortType? Sort, AssetStatus? Status, string? Query, int? Skip, int? Take) => Type switch {
                0 => Ok(await GetAssets(DB.Building.Where(A => A.Owner != null && A.Owner.ID == Account), Sort, Status, Query, Skip, Take)),
                1 => Ok(await GetAssets(DB.Unit.Where(A => A.Owner != null && A.Owner.ID == Account), Sort, Status, Query, Skip, Take)),
                _ => Ok(await GetAssets(DB.Asset.Where(A => A.Owner != null && A.Owner.ID == Account), Sort, Status, Query, Skip, Take)),
            };

        /// <summary>Get a list of all assets an account owns</summary>
        /// <param name="IncomeItem"></param>
        /// <returns></returns>
        [HttpGet("IncomeItem/{IncomeItem}")]
        public async Task<IActionResult> IndexByIncomeItem(Guid IncomeItem)
            => Ok(await DB.IncomeItem.Where(A => A.ID == IncomeItem).Select(A => A.RelatedAssets).FirstOrDefaultAsync()); //90% sure this may not work. If it doesn't we can change it.

        /// <summary>Gets a list of buildings that are for sale, or that have units for sale</summary>
        /// <returns></returns>
        [HttpGet("ForSale")]
        public async Task<IActionResult> BuildingsWithUnitsForSale(IncomeItemSortType? Sort) {

            //this is a MASSIVE MEGA QUERY which maybe should be trimmed. If this doesn't work out, we can kneecap it at just selecting the key.

            //Get a list of units which are for sale and group them by building:
            IQueryable<Building> Collection = DB.Unit //Get units that are for sale
                .Where(A => A.Status == AssetStatus.FOR_SALE)
                .GroupBy(A => A.Building) //Group by building
                .Where(A => A.Key != null) //Where we have a key
                .Include(A => A.Key!.Units //include all of those buildings units
                        .Where(A => A.Status == AssetStatus.FOR_SALE)) //only where they are for sale
                .Select(A => A.Key!) //Select just the key (the buildings)
                .Union(DB.Building //then unite this by the buildings that are just for sale themselves
                    .Where(A => A.Status == AssetStatus.FOR_SALE)
                    .Include(A => A.Units //Include their units
                        .Where(A => A.Status == AssetStatus.FOR_SALE))) //of course only those that are for sale
                .Distinct(); //Make sure the buildings are distinct

            Collection = Sort switch {
                IncomeItemSortType.NAME_DESC => Collection.OrderByDescending(A => A.Name),
                IncomeItemSortType.DATE_CREATED => Collection.OrderByDescending(A => A.DateCreated),
                IncomeItemSortType.DATE_CREATED_ASC => Collection.OrderBy(A => A.DateCreated),
                IncomeItemSortType.DATE_UPDATED => Collection.OrderByDescending(A => A.DateUpdated),
                IncomeItemSortType.DATE_UPDATED_ASC => Collection.OrderBy(A => A.DateUpdated),
                _ => Collection.OrderBy(A => A.Name),
            };

            //I think that's actually it. Hopefully this doesn't explode
            return Ok(await Collection.ToListAsync());

        }

        /// <summary>Gets a specific Asset</summary>
        /// <param name="AssetID">ID of the asset to retrieve</param>
        /// <returns></returns>
        [HttpGet("{AssetID}")]
        public async Task<IActionResult> GetAsset(Guid AssetID) {
            var A = await GetAsset(DB.Asset, AssetID);
            return A is Unit
                ? Ok(A as Unit) //we need to convert to subtypes so the JSON serializer will actually *do it* 
                : A is Building
                    ? Ok(A as Building)
                    : Ok(A);
        }

        /// <summary>Gets a building's list of units</summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet("{BuildingID}/Dir")]
        public async Task<IActionResult> GetBuildingDirectory(Guid ID) => Ok(await DB.Unit.Where(A => A.Building != null && A.Building.ID == ID).ToListAsync());

        /// <summary>Creates a building under given accountID</summary>
        /// <param name="SessionID"></param>
        /// <param name="Request"></param>
        /// <param name="AccountID"></param>
        /// <returns></returns>
        [HttpPost("Account/{AccountID}")]
        public async Task<IActionResult> CreateBuilding([FromHeader] Guid? SessionID, BuildingRequest Request, string AccountID) {
            Session? S = await Task.Run(() => SessionManager.Manager.FindSession(SessionID ?? Guid.Empty));
            if (S is null) { return Unauthorized(ErrorResult.Reusable.InvalidSession); }

            //Get the account and include the owners
            Account? N = await DB.Account.Include(A => A.Owners).FirstOrDefaultAsync(A => A.ID == AccountID);
            if (N is null) { return NotFound(ErrorResult.NotFound("Account was not found", "AccountID")); }
            if (!N.Owners.Any(A => A.ID == S.UserID)) { return Unauthorized(ErrorResult.Forbidden("User does not own account")); }

            Jurisdiction? J = await DB.Jurisdiction.FirstOrDefaultAsync(A => A.ID == Request.JurisdictionID);
            if (J is null) { return NotFound(ErrorResult.NotFound("Jurisdiction was not found", "Request.jurisdictionID")); }

            //Set everything
            Building B = new() {
                Address = Request.Address,
                Beds = Request.Beds,
                BuildingType = Request.BuildingType,
                Coordinates = Request.Coordinates,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                Description = Request.Description,
                Jurisdiction = J,
                Name = Request.Name,
                Owner = N,
                RelatedIncomeItems = new(),
                Status = Request.Status,
            };

            //Save it
            DB.Add(B);
            await DB.SaveChangesAsync();

            //Get the heck out
            return Ok(B);
        }

        /// <summary>Creates a unit in a building</summary>
        /// <param name="SessionID"></param>
        /// <param name="Request"></param>
        /// <param name="AccountID"></param>
        /// <param name="BuildingID"></param>
        /// <returns></returns>
        [HttpPost("Account/{AccountID}/{BuildingID}")]
        public async Task<IActionResult> CreateUnit([FromHeader] Guid? SessionID, UnitRequest Request, string AccountID, Guid BuildingID) {
            Session? S = await Task.Run(() => SessionManager.Manager.FindSession(SessionID ?? Guid.Empty));
            if (S is null) { return Unauthorized(ErrorResult.Reusable.InvalidSession); }

            //Get the account and include the owners
            Account? N = await DB.Account.Include(A => A.Owners).FirstOrDefaultAsync(A => A.ID == AccountID);
            if (N is null) { return NotFound(ErrorResult.NotFound("Account was not found", "AccountID")); }
            if (!N.Owners.Any(A => A.ID == S.UserID)) { return Unauthorized(ErrorResult.Forbidden("User does not own account")); }

            //Now get a building
            Building? B = await DB.Building.Include(A => A.Owner).FirstOrDefaultAsync(A => A.ID == BuildingID);
            if (B is null) { return NotFound(ErrorResult.NotFound("Building was not found", "BuildingID")); }
            if (N.ID != B.AccountID) { return Unauthorized(ErrorResult.Forbidden("Cannot create building unit with different owner than building owner. First create the unit, then transfer it")); }

            Unit U = new() {
                Building = B,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                Description = Request.Description,
                Name = Request.Name,
                Status = Request.Status,
                Owner= N
            };

            DB.Add(U);
            await DB.SaveChangesAsync();

            return Ok(U);
        }

        /// <summary></summary>
        /// <param name="SessionID"></param>
        /// <param name="Request"></param>
        /// <param name="AccountID"></param>
        /// <param name="BuildingID"></param>
        /// <returns></returns>
        //Update an asset's details
        [HttpPut("Account/{AccountID}/{BuildingID}")]
        public async Task<IActionResult> UpdateBuilding([FromHeader] Guid? SessionID, BuildingRequest Request, string AccountID, Guid BuildingID) {
            Session? S = await Task.Run(() => SessionManager.Manager.FindSession(SessionID ?? Guid.Empty));
            if (S is null) { return Unauthorized(ErrorResult.Reusable.InvalidSession); }

            Jurisdiction? J = await DB.Jurisdiction.FirstOrDefaultAsync(A => A.ID == Request.JurisdictionID);
            if (J is null) { return NotFound(ErrorResult.NotFound("Jurisdiction was not found", "Request.jurisdictionID")); }

            //Now get a building
            Building? B = await DB.Building.Include(A => A.Owner).ThenInclude(A=>A!.Owners).FirstOrDefaultAsync(A => A.ID == BuildingID);
            if (B is null) { return NotFound(ErrorResult.NotFound("Building was not found", "BuildingID")); }
            if (!B.Owner!.Owners.Any(A => A.ID == S.UserID)) { return Unauthorized(ErrorResult.Forbidden("User does not own account")); }

            //Set everything
            B.Address = Request.Address;
            B.Beds = Request.Beds;
            B.BuildingType = Request.BuildingType;
            B.Coordinates = Request.Coordinates;
            B.DateUpdated = DateTime.UtcNow;
            B.Description = Request.Description;
            B.Jurisdiction = J;
            B.Name = Request.Name;
            B.Status = Request.Status;
            

            //Save it
            DB.Update(B);
            await DB.SaveChangesAsync();

            //Get the heck out
            return Ok(B);
        }

        /// <summary>Updates a Unit's details</summary>
        /// <param name="SessionID"></param>
        /// <param name="Request"></param>
        /// <param name="AccountID"></param>
        /// <param name="BuildingID"></param>
        /// <param name="UnitID"></param>
        /// <returns></returns>
        [HttpPut("Account/{AccountID}/{BuildingID}/{UnitID}")] //Technically speaking, there's no need for building ID. It's just here for uniformity.
        public async Task<IActionResult> UpdateUnit([FromHeader] Guid? SessionID, BuildingRequest Request, string AccountID, Guid BuildingID, Guid UnitID) {
            Session? S = await Task.Run(() => SessionManager.Manager.FindSession(SessionID ?? Guid.Empty));
            if (S is null) { return Unauthorized(ErrorResult.Reusable.InvalidSession); }

            //Get the account and include the owners
            Account? N = await DB.Account.Include(A => A.Owners).FirstOrDefaultAsync(A => A.ID == AccountID);
            if (N is null) { return NotFound(ErrorResult.NotFound("Account was not found", "AccountID")); }
            if (!N.Owners.Any(A => A.ID == S.UserID)) { return Unauthorized(ErrorResult.Forbidden("User does not own account")); }

            //Now get a building
            Unit? U = await DB.Unit.Include(A => A.Owner).ThenInclude(A=>A!.Owners).FirstOrDefaultAsync(A => A.ID == UnitID);
            if (U is null) { return NotFound(ErrorResult.NotFound("Unit was not found", "UnitID")); }
            if (!U.Owner!.Owners.Any(A => A.ID == S.UserID)) { return Unauthorized(ErrorResult.Forbidden("User does not own account")); }

            U.DateUpdated = DateTime.UtcNow;
            U.Description = Request.Description;
            U.Name = Request.Name;
            U.Status = Request.Status;
            
            DB.Add(U);
            await DB.SaveChangesAsync();

            return Ok(U);

        }

        /// <summary>Gets incomeitems related to this asset</summary>
        /// <param name="AssetID"></param>
        /// <returns></returns>
        [HttpGet("{AssetID}/IncomeItems")]
        public async Task<IActionResult> GetIncomeItems(Guid AssetID) {
            Asset? B = await DB.Asset.Include(A => A.RelatedIncomeItems).FirstOrDefaultAsync(A=>A.ID==AssetID);
            return B is null 
                ? NotFound(ErrorResult.NotFound("Account was not found")) 
                : Ok(B.RelatedIncomeItems);
        }

        /// <summary>Adds a given incomeitem to the list of related income items of this asset</summary>
        /// <param name="SessionID"></param>
        /// <param name="AssetID"></param>
        /// <param name="IncomeItemID"></param>
        /// <returns></returns>
        [HttpPost("{AssetID}/IncomeItems/{IncomeItemID}")]
        public async Task<IActionResult> AddIncomeItem([FromHeader]Guid? SessionID, Guid AssetID, Guid IncomeItemID) {
            Session? S = await Task.Run(() => SessionManager.Manager.FindSession(SessionID ?? Guid.Empty));
            if (S is null) { return Unauthorized(ErrorResult.Reusable.InvalidSession); }

            Asset? B = await DB.Asset.Include(A => A.RelatedIncomeItems).Include(A=>A.Owner).ThenInclude(A=>A!.Owners).FirstOrDefaultAsync(A => A.ID == AssetID);
            if (B is null) { return NotFound(ErrorResult.NotFound("Account was not found")); }

            //Ensure the asset is owned by this session holder
            if (!B.Owner!.Owners.Any(A => A.ID == S.UserID)) { return Unauthorized(ErrorResult.Forbidden("Session does not own given asset")); }

            //Get the income item and include the account
            var I = await DB.IncomeItem.Include(A=>A.Account).FirstOrDefaultAsync(A=>A.ID==IncomeItemID);
            if(I is null) { return NotFound(ErrorResult.NotFound("Income Item was not found")); }

            if (I.Account != B.Owner) { return BadRequest(ErrorResult.BadRequest("IncomeItem account and Asset account mismatch")); }
            
            B.RelatedIncomeItems.Add(I);
            DB.Update(B);
            await DB.SaveChangesAsync();
            return Ok(B);
            
        }

        /// <summary>Deletes an income item from the list of related income items of this asset</summary>
        /// <param name="SessionID"></param>
        /// <param name="AssetID"></param>
        /// <param name="IncomeItemID"></param>
        /// <returns></returns>
        [HttpDelete("{AssetID}/Incomeitems/{IncomeItemID}")]
        public async Task<IActionResult> DelIncomeItem([FromHeader] Guid? SessionID, Guid AssetID, Guid IncomeItemID) {
            Session? S = await Task.Run(() => SessionManager.Manager.FindSession(SessionID ?? Guid.Empty));
            if (S is null) { return Unauthorized(ErrorResult.Reusable.InvalidSession); }

            Asset? B = await DB.Asset.Include(A => A.RelatedIncomeItems).Include(A => A.Owner).ThenInclude(A => A!.Owners).FirstOrDefaultAsync(A => A.ID == AssetID);
            if (B is null) { return NotFound(ErrorResult.NotFound("Account was not found")); }

            //Ensure the asset is owned by this session holder
            if (!B.Owner!.Owners.Any(A => A.ID == S.UserID)) { return Unauthorized(ErrorResult.Forbidden("Session does not own given asset")); }

            //Get the income item and include the account

            B.RelatedIncomeItems.RemoveAll(A => A.ID == IncomeItemID);
            DB.Update(B);
            await DB.SaveChangesAsync();
            return Ok(B);

        }

        /// <summary>Transfer an asset from its current owner to a new owner</summary>
        /// <param name="SessionID"></param>
        /// <param name="AssetID"></param>
        /// <param name="NewAccountID"></param>
        /// <returns></returns>
        [HttpPut("Transfer/{AssetID}")]
        public async Task<IActionResult> TransferAssetOwnership([FromHeader] Guid? SessionID, [FromRoute] Guid AssetID, [FromBody] string NewAccountID) {
            Session? S = await Task.Run(() => SessionManager.Manager.FindSession(SessionID ?? Guid.Empty));
            if (S is null) { return Unauthorized(ErrorResult.Reusable.InvalidSession); }

            //Get the asset
            Asset? B = await DB.Asset.Include(A => A.Owner).ThenInclude(A => A!.Owners).FirstOrDefaultAsync(A => A.ID == AssetID);
            if (B is null) { return NotFound(ErrorResult.NotFound("Asset was not found", "AssetID")); }

            //Check ownership
            if (!B.Owner!.Owners.Any(A => A.ID == S.UserID)) { return Unauthorized(ErrorResult.Forbidden("Account this asset belongs to is not owned by session holder")); }

            //Get new account
            Account? N = await DB.Account.FirstOrDefaultAsync(A => A.ID == NewAccountID);
            if (N is null) { return NotFound(ErrorResult.NotFound("New account was not found")); }

            B.Owner = N;
            DB.Update(B);
            await DB.SaveChangesAsync();

            return Ok(B);
        }

        /// <summary>Deletes an asset from the Neco DB</summary>
        /// <param name="SessionID"></param>
        /// <param name="AssetID"></param>
        /// <returns></returns>
        [HttpDelete("{AssetID}")]
        public async Task<IActionResult> DeleteAsset([FromHeader] Guid? SessionID, [FromRoute] Guid AssetID) {
            Session? S = await Task.Run(() => SessionManager.Manager.FindSession(SessionID ?? Guid.Empty));
            if (S is null) { return Unauthorized(ErrorResult.Reusable.InvalidSession); }

            //Get the asset
            Asset? B = await DB.Asset.Include(A => A.Owner).ThenInclude(A => A!.Owners).FirstOrDefaultAsync(A => A.ID == AssetID);
            if (B is null) { return NotFound(ErrorResult.NotFound("Asset was not found", "AssetID")); }

            //Check ownership
            if (!B.Owner!.Owners.Any(A => A.ID == S.UserID)) { return Unauthorized(ErrorResult.Forbidden("Account this asset belongs to is not owned by session holder")); }

            //Cascade should kick into effect. If not then we have to worry about eso later.

            DB.Remove(B);
            await DB.SaveChangesAsync();

            return Ok(B);

        }

        #region Helpers
        private static async Task<E?> GetAsset<E>(IQueryable<E> Collection, Guid ID) where E : Asset 
            => await Collection.Include(A=>A.Owner).FirstOrDefaultAsync(A => A.ID== ID);

        private static async Task<List<E>> GetAssets<E>(IQueryable<E> Collection, //Let's re-ue IncomeItemSortType because yes
            IncomeItemSortType? Sort, AssetStatus? Status, string? Query, int? Skip, int? Take) where E : Asset {

            if (Query is not null) {
                Query = Query.ToLower();
                Collection = Collection.Where(A => 
                    A.Name.ToLower().Contains(Query) || 
                    A.Description.ToLower().Contains(Query));
            }

            if (Status is not null) { Collection = Collection.Where(A => A.Status == Status); }

            Collection = Sort switch {
                IncomeItemSortType.NAME_DESC => Collection.OrderByDescending(A=>A.Name),
                IncomeItemSortType.DATE_CREATED => Collection.OrderByDescending(A=>A.DateCreated),
                IncomeItemSortType.DATE_CREATED_ASC => Collection.OrderBy(A=>A.DateCreated),
                IncomeItemSortType.DATE_UPDATED => Collection.OrderByDescending(A => A.DateUpdated),
                IncomeItemSortType.DATE_UPDATED_ASC => Collection.OrderBy(A => A.DateUpdated),
                _ =>Collection.OrderBy(A=>A.Name),
            };

            Collection = Collection.Skip(Skip ?? 0).Take(Take ?? 20);
            return await Collection.ToListAsync();

        }
        
        #endregion

    }
}
