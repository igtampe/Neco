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
        private readonly EzTaxContext _context;

        public EzTaxController(EzTaxContext context) { _context = context; }

        #region TaxUserInfo

        // GET: UMSAT
        [HttpGet("TaxUserInfo")]
        public async Task<IActionResult> TaxUserInfoIndex() { return Ok(await _context.TaxUserInfos.ToListAsync()); }

        // GET: UMSAT/5
        [HttpGet("TaxUserInfo/{id}")]
        public async Task<IActionResult> TaxUserInfoDetails(Guid? id) {
            if (id == null) { return NotFound(); }

            var asset = await _context.TaxUserInfos.FirstOrDefaultAsync(m => m.Id == id);
            if (asset == null) { return NotFound(); }

            return Ok(asset);
        }

        // GET: UMSAT/5
        [HttpPost("TaxUserInfo/User")]
        public async Task<IActionResult> TaxUserInfoByUser(User U) {
            if (U == null) { return NotFound(); }

            var asset = await _context.TaxUserInfos.FirstOrDefaultAsync(m => m.User.Equals(U));
            if (asset == null) { return NotFound(); }

            return Ok(asset);
        }



        // POST: UMSAT
        [HttpPost("TaxUserInfo")]
        public async Task<IActionResult> TaxUserInfoCreate(TaxUserInfo UserInfo) {
            if (UserInfo.Id != Guid.Empty) { BadRequest("Asset has an ID. Did you mean to edit it?"); }
            UserInfo.Id = Guid.NewGuid();
            _context.Add(UserInfo);
            await _context.SaveChangesAsync();
            return Created($"EzTax/TaxUserInfo/{UserInfo.Id}", UserInfo);
        }

        // PUT: UMSAT/5
        [HttpPut("TaxUserInfo/{id}")]
        public async Task<IActionResult> TaxUserInfoEdit(Guid id, TaxUserInfo UserInfo) {
            if (id != UserInfo.Id) { return NotFound(); }

            try {
                _context.Update(UserInfo);
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!TaxUserInfoExists(UserInfo.Id)) { return NotFound(); } else { throw; }
            }

            return Ok(UserInfo);
        }

        //DELETE: UMSAT/5
        [HttpDelete("TaxUserInfo/{id}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id) {
            var asset = await _context.TaxUserInfos.FindAsync(id);
            _context.TaxUserInfos.Remove(asset);
            await _context.SaveChangesAsync();
            return Ok(asset);
        }

        private bool TaxUserInfoExists(Guid id) { return _context.TaxUserInfos.Any(e => e.Id == id); }

        #endregion

        #region TaxJurisdiction
        // GET: UMSAT
        [HttpGet("TaxJurisdiction")]
        public async Task<IActionResult> TaxJurisdictionIndex() { return Ok(await _context.TaxJurisdictions.ToListAsync()); }

        // GET: UMSAT/5
        [HttpGet("TaxJurisdiction/{id}")]
        public async Task<IActionResult> Details(Guid? id) {
            if (id == null) { return NotFound(); }

            var asset = await _context.TaxJurisdictions.FirstOrDefaultAsync(m => m.ID == id);
            if (asset == null) { return NotFound(); }

            return Ok(asset);
        }

        #endregion

        #region TaxBracket
        // GET: UMSAT
        [HttpGet("TaxBracket")]
        public async Task<IActionResult> TaxBracketIndex() { return Ok(await _context.TaxBrackets.ToListAsync()); }

        // GET: UMSAT/5
        [HttpGet("TaxBracket/{id}")]
        public async Task<IActionResult> TaxBracketDetails(Guid? id) {
            if (id == null) { return NotFound(); }

            var asset = await _context.TaxBrackets.FirstOrDefaultAsync(m => m.ID == id);
            if (asset == null) { return NotFound(); }

            return Ok(asset);
        }

        #endregion

        #region IncomeItem
        // GET: UMSAT
        [HttpGet("IncomeItem")]
        public async Task<IActionResult> IncomeItemIndex() { return Ok(await _context.IncomeItems.ToListAsync()); }

        // GET: UMSAT/5
        [HttpGet("IncomeItem/{id}")]
        public async Task<IActionResult> IncomeItemDetails(Guid? id) {
            if (id == null) { return NotFound(); }

            var asset = await _context.IncomeItems.FirstOrDefaultAsync(m => m.ID == id);
            if (asset == null) { return NotFound(); }

            return Ok(asset);
        }

        // POST: UMSAT
        [HttpPost("IncomeItem")]
        public async Task<IActionResult> IncomeItemCreate(IncomeItem asset) {
            if (asset.ID != Guid.Empty) { BadRequest("Asset has an ID. Did you mean to edit it?"); }
            asset.ID = Guid.NewGuid();
            _context.Add(asset);
            await _context.SaveChangesAsync();
            return Created($"EzTax/IncomeItem/UMSAT/{asset.ID}", asset);
        }

        // PUT: UMSAT/5
        [HttpPut("IncomeItem/{id}")]
        public async Task<IActionResult> Edit(Guid id, IncomeItem asset) {
            if (id != asset.ID) { return NotFound(); }

            try {
                _context.Update(asset);
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!IncomeItemExists(asset.ID)) { return NotFound(); } else { throw; }
            }

            return Ok(asset);
        }

        //DELETE: UMSAT/5
        [HttpDelete("IncomeItem/{id}")]
        public async Task<IActionResult> IncomeItemDelete(Guid id) {
            var asset = await _context.IncomeItems.FindAsync(id);
            _context.IncomeItems.Remove(asset);
            await _context.SaveChangesAsync();
            return Ok(asset);
        }

        private bool IncomeItemExists(Guid id) { return _context.IncomeItems.Any(e => e.ID == id); }
        #endregion

        #region Apartment
        // GET: UMSAT
        [HttpGet("Apartment")]
        public async Task<IActionResult> ApartmentIndex() { return Ok(await _context.Apartments.ToListAsync()); }

        // GET: UMSAT/5
        [HttpGet("Apartment/{id}")]
        public async Task<IActionResult> ApartmentDetails(Guid? id) {
            if (id == null) { return NotFound(); }

            var asset = await _context.Apartments.FirstOrDefaultAsync(m => m.ID == id);
            if (asset == null) { return NotFound(); }

            return Ok(asset);
        }

        // POST: UMSAT
        [HttpPost("Apartment")]
        public async Task<IActionResult> ApartmentCreate(Apartment asset) {
            if (asset.ID != Guid.Empty) { BadRequest("Asset has an ID. Did you mean to edit it?"); }
            asset.ID = Guid.NewGuid();
            _context.Add(asset);
            await _context.SaveChangesAsync();
            return Created($"EzTax/Apartment/{asset.ID}", asset);
        }

        // PUT: UMSAT/5
        [HttpPut("Apartment/{id}")]
        public async Task<IActionResult> Edit(Guid id, Apartment asset) {
            if (id != asset.ID) { return NotFound(); }

            try {
                _context.Update(asset);
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!ApartmentExists(asset.ID)) { return NotFound(); } else { throw; }
            }

            return Ok(asset);
        }

        //DELETE: UMSAT/5
        [HttpDelete("Apartment/{id}")]
        public async Task<IActionResult> ApartmentDelete(Guid id) {
            var asset = await _context.Apartments.FindAsync(id);
            _context.Apartments.Remove(asset);
            await _context.SaveChangesAsync();
            return Ok(asset);
        }

        private bool ApartmentExists(Guid id) { return _context.Apartments.Any(e => e.ID == id); }
        #endregion

        #region Business
        // GET: UMSAT
        [HttpGet("Business")]
        public async Task<IActionResult> BusinessIndex() { return Ok(await _context.Businesses.ToListAsync()); }

        // GET: UMSAT/5
        [HttpGet("Business/{id}")]
        public async Task<IActionResult> BusinessDetails(Guid? id) {
            if (id == null) { return NotFound(); }

            var asset = await _context.Businesses.FirstOrDefaultAsync(m => m.ID == id);
            if (asset == null) { return NotFound(); }

            return Ok(asset);
        }

        // POST: UMSAT
        [HttpPost("Business")]
        public async Task<IActionResult> BusinessCreate(Business asset) {
            if (asset.ID != Guid.Empty) { BadRequest("Asset has an ID. Did you mean to edit it?"); }
            asset.ID = Guid.NewGuid();
            _context.Add(asset);
            await _context.SaveChangesAsync();
            return Created($"EzTax/Business/{asset.ID}", asset);
        }

        // PUT: UMSAT/5
        [HttpPut("Business/{id}")]
        public async Task<IActionResult> BusinessEdit(Guid id, Business asset) {
            if (id != asset.ID) { return NotFound(); }

            try {
                _context.Update(asset);
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!BusinessExists(asset.ID)) { return NotFound(); } else { throw; }
            }

            return Ok(asset);
        }

        //DELETE: UMSAT/5
        [HttpDelete("Business/{id}")]
        public async Task<IActionResult> BusinessDelete(Guid id) {
            var asset = await _context.Businesses.FindAsync(id);
            _context.Businesses.Remove(asset);
            await _context.SaveChangesAsync();
            return Ok(asset);
        }

        private bool BusinessExists(Guid id) { return _context.Businesses.Any(e => e.ID == id); }
        #endregion

        #region Hotel
        // GET: UMSAT
        [HttpGet("Hotel")]
        public async Task<IActionResult> HotelIndex() { return Ok(await _context.Hotels.ToListAsync()); }

        // GET: UMSAT/5
        [HttpGet("Hotel/{id}")]
        public async Task<IActionResult> HotelDetails(Guid? id) {
            if (id == null) { return NotFound(); }

            var asset = await _context.Hotels.FirstOrDefaultAsync(m => m.ID == id);
            if (asset == null) { return NotFound(); }

            return Ok(asset);
        }

        // POST: UMSAT
        [HttpPost("Hotel")]
        public async Task<IActionResult> Create(Hotel asset) {
            if (asset.ID != Guid.Empty) { BadRequest("Asset has an ID. Did you mean to edit it?"); }
            asset.ID = Guid.NewGuid();
            _context.Add(asset);
            await _context.SaveChangesAsync();
            return Created($"EzTax/Hotel/{asset.ID}", asset);
        }

        // PUT: UMSAT/5
        [HttpPut("Hotel/{id}")]
        public async Task<IActionResult> Edit(Guid id, Hotel asset) {
            if (id != asset.ID) { return NotFound(); }

            try {
                _context.Update(asset);
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!HotelExists(asset.ID)) { return NotFound(); } else { throw; }
            }

            return Ok(asset);
        }

        //DELETE: UMSAT/5
        [HttpDelete("Hotel/{id}")]
        public async Task<IActionResult> HotelDelete(Guid id) {
            var asset = await _context.Hotels.FindAsync(id);
            _context.Hotels.Remove(asset);
            await _context.SaveChangesAsync();
            return Ok(asset);
        }

        private bool HotelExists(Guid id) { return _context.Hotels.Any(e => e.ID == id); }
        #endregion

    }
}
