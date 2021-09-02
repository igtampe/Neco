﻿using System;
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
        private readonly NecoContext _context;

        public EzTaxController(NecoContext context) { _context = context; }

        #region TaxUserInfo

        // GET: UMSAT
        [HttpGet("TaxUserInfo")]
        public async Task<IActionResult> TaxUserInfoIndex() { return Ok(await _context.TaxUserInfo.Include(m=> m.User).ToListAsync()); }

        // GET: UMSAT/5
        [HttpGet("TaxUserInfo/{id}")]
        public async Task<IActionResult> TaxUserInfoDetails(Guid? id) {
            if (id == null) { return NotFound(); }

            var asset = await _context.TaxUserInfo
                .Include(m=>m.User)
                .Include(m=>m.Items).ThenInclude(m=> m.FederalJurisdiction)
                .Include(m => m.Items).ThenInclude(m => m.LocalJurisdiction)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asset == null) { return NotFound(); }

            return Ok(asset);
        }

        // GET: UMSAT/5
        [HttpPost("TaxUserInfo/User")]
        public async Task<IActionResult> TaxUserInfoByUser(User U) {
            if (U == null) { return NotFound(); }

            var asset = await _context.TaxUserInfo.FirstOrDefaultAsync(m => m.User.Equals(U));
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
            var asset = await _context.TaxUserInfo.FindAsync(id);
            _context.TaxUserInfo.Remove(asset);
            await _context.SaveChangesAsync();
            return Ok(asset);
        }

        private bool TaxUserInfoExists(Guid id) { return _context.TaxUserInfo.Any(e => e.Id == id); }

        #endregion

        #region TaxJurisdiction
        // GET: UMSAT
        [HttpGet("TaxJurisdiction")]
        public async Task<IActionResult> TaxJurisdictionIndex() { return Ok(await _context.TaxJurisdiction.Include(m=> m.Account).ToListAsync()); }

        // GET: UMSAT/5
        [HttpGet("TaxJurisdiction/{id}")]
        public async Task<IActionResult> Details(Guid? id) {
            if (id == null) { return NotFound(); }

            var asset = await _context.TaxJurisdiction.Include(m => m.Account).Include(m => m.Brackets).FirstOrDefaultAsync(m => m.ID == id);
            if (asset == null) { return NotFound(); }

            //Manually get the brackets TODO
            asset.Brackets = await _context.TaxBracket.Where(m => m.Jurisdiction == asset).ToListAsync();


            return Ok(asset);
        }

        #endregion

        #region TaxBracket
        // GET: UMSAT
        [HttpGet("TaxBracket")]
        public async Task<IActionResult> TaxBracketIndex() { return Ok(await _context.TaxBracket.Include(m=> m.Type).Include(m=> m.Jurisdiction).ToListAsync()); }

        // GET: UMSAT/5
        [HttpGet("TaxBracket/{id}")]
        public async Task<IActionResult> TaxBracketDetails(Guid? id) {
            if (id == null) { return NotFound(); }

            var asset = await _context.TaxBracket.Include(m => m.Type).Include(m => m.Jurisdiction).FirstOrDefaultAsync(m => m.ID == id);
            if (asset == null) { return NotFound(); }

            return Ok(asset);
        }

        #endregion

        #region IncomeItem
        // GET: UMSAT
        [HttpGet("IncomeItem")]
        public async Task<IActionResult> IncomeItemIndex() { return Ok(await _context.IncomeItem
                .Include(m => m.User)
                .Include(m => m.FederalJurisdiction)
                .Include(m => m.LocalJurisdiction)
                .ToListAsync()); }

        // GET: UMSAT/5
        [HttpGet("IncomeItem/{id}")]
        public async Task<IActionResult> IncomeItemDetails(Guid? id) {
            if (id == null) { return NotFound(); }

            var asset = await _context.IncomeItem
                .Include(m => m.User)
                .Include(m => m.FederalJurisdiction)
                .Include(m => m.LocalJurisdiction)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (asset == null) { return NotFound(); }

            //Find all related subitems manually here TODO

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
        public async Task<IActionResult> IncomeItemEdit(Guid id, IncomeItem asset) {
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
            var asset = await _context.IncomeItem.FindAsync(id);
            _context.IncomeItem.Remove(asset);
            await _context.SaveChangesAsync();
            return Ok(asset);
        }

        private bool IncomeItemExists(Guid id) { return _context.IncomeItem.Any(e => e.ID == id); }
        #endregion

        #region Apartment
        // GET: UMSAT
        [HttpGet("Apartment")]
        public async Task<IActionResult> ApartmentIndex() { return Ok(await _context.Apartment.ToListAsync()); }

        // GET: UMSAT/5
        [HttpGet("Apartment/{id}")]
        public async Task<IActionResult> ApartmentDetails(Guid? id) {
            if (id == null) { return NotFound(); }

            var asset = await _context.Apartment.FirstOrDefaultAsync(m => m.ID == id);
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
        public async Task<IActionResult> ApartmentEdit(Guid id, Apartment asset) {
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
            var asset = await _context.Apartment.FindAsync(id);
            _context.Apartment.Remove(asset);
            await _context.SaveChangesAsync();
            return Ok(asset);
        }

        private bool ApartmentExists(Guid id) { return _context.Apartment.Any(e => e.ID == id); }
        #endregion

        #region Business
        // GET: UMSAT
        [HttpGet("Business")]
        public async Task<IActionResult> BusinessIndex() { return Ok(await _context.Business.ToListAsync()); }

        // GET: UMSAT/5
        [HttpGet("Business/{id}")]
        public async Task<IActionResult> BusinessDetails(Guid? id) {
            if (id == null) { return NotFound(); }

            var asset = await _context.Business.FirstOrDefaultAsync(m => m.ID == id);
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
            var asset = await _context.Business.FindAsync(id);
            _context.Business.Remove(asset);
            await _context.SaveChangesAsync();
            return Ok(asset);
        }

        private bool BusinessExists(Guid id) { return _context.Business.Any(e => e.ID == id); }
        #endregion

        #region Hotel
        // GET: UMSAT
        [HttpGet("Hotel")]
        public async Task<IActionResult> HotelIndex() { return Ok(await _context.Hotel.ToListAsync()); }

        // GET: UMSAT/5
        [HttpGet("Hotel/{id}")]
        public async Task<IActionResult> HotelDetails(Guid? id) {
            if (id == null) { return NotFound(); }

            var asset = await _context.Hotel.FirstOrDefaultAsync(m => m.ID == id);
            if (asset == null) { return NotFound(); }

            return Ok(asset);
        }

        // POST: UMSAT
        [HttpPost("Hotel")]
        public async Task<IActionResult> HotelCreate(Hotel asset) {
            if (asset.ID != Guid.Empty) { BadRequest("Asset has an ID. Did you mean to edit it?"); }
            asset.ID = Guid.NewGuid();
            _context.Add(asset);
            await _context.SaveChangesAsync();
            return Created($"EzTax/Hotel/{asset.ID}", asset);
        }

        // PUT: UMSAT/5
        [HttpPut("Hotel/{id}")]
        public async Task<IActionResult> HotelEdit(Guid id, Hotel asset) {
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
            var asset = await _context.Hotel.FindAsync(id);
            _context.Hotel.Remove(asset);
            await _context.SaveChangesAsync();
            return Ok(asset);
        }

        private bool HotelExists(Guid id) { return _context.Hotel.Any(e => e.ID == id); }
        #endregion

    }
}