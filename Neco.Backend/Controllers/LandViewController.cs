﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Igtampe.Neco.Common.LandView;
using Igtampe.Neco.Data;

namespace Igtampe.Neco.Backend.Controllers {

    [Route("LandView")]
    [ApiController]
    public class LandViewController: Controller {
        private readonly NecoContext _context;

        public LandViewController(NecoContext context) { _context = context; }

        #region Country

        // GET: Country
        [HttpGet("Country")]
        public async Task<IActionResult> CountryIndex() { return Ok(await _context.Country.ToListAsync()); }

        // GET: Country/5
        [HttpGet("Country/{id}")]
        public async Task<IActionResult> CountryDetails(Guid? id) {
            if (id == null) { return NotFound(); }

            var asset = await _context.Country.FirstOrDefaultAsync(m => m.ID == id);
            if (asset == null) { return NotFound(); }

            return Ok(asset);
        }

        #endregion

        #region District

        // GET: District
        [HttpGet("District")]
        public async Task<IActionResult> DistrictIndex() { return Ok(await _context.District.ToListAsync()); }

        // GET: District/5
        [HttpGet("District/{id}")]
        public async Task<IActionResult> DistrictDetails(Guid? id) {
            if (id == null) { return NotFound(); }

            var asset = await _context.District.FirstOrDefaultAsync(m => m.ID == id);
            if (asset == null) { return NotFound(); }

            return Ok(asset);
        }

        #endregion

        #region Plot

        // GET: District
        [HttpGet("Plot")]
        public async Task<IActionResult> PlotIndex() { return Ok(await _context.Plot.ToListAsync()); }

        // GET: District/5
        [HttpGet("Plot/{id}")]
        public async Task<IActionResult> PlotDetails(Guid? id) {
            if (id == null) { return NotFound(); }

            var asset = await _context.Plot.FirstOrDefaultAsync(m => m.ID == id);
            if (asset == null) { return NotFound(); }

            return Ok(asset);
        }

        // POST: Plot
        [HttpPost("Plot")]
        public async Task<IActionResult> PlotCreate(Plot plot) {
            if (plot.ID != Guid.Empty) { BadRequest("Asset has an ID. Did you mean to edit it?"); }
            plot.ID = Guid.NewGuid();
            _context.Add(plot);
            await _context.SaveChangesAsync();
            return Created($"LandView/plot/{plot.ID}", plot);
        }

        // PUT: Plot/5
        [HttpPut("Plot/{id}")]
        public async Task<IActionResult> PlotEdit(Guid id, Plot plot) {
            if (id != plot.ID) { return NotFound(); }

            try {
                _context.Update(plot);
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!PlotExists(plot.ID)) { return NotFound(); } else { throw; }
            }

            return Ok(plot);
        }

        //DELETE: Plot/5
        [HttpDelete("Plot/{id}")]
        public async Task<IActionResult> PlotDelete(Guid id) {
            var asset = await _context.Plot.FindAsync(id);
            _context.Plot.Remove(asset);
            await _context.SaveChangesAsync();
            return Ok(asset);
        }

        private bool PlotExists(Guid id) { return _context.Plot.Any(e => e.ID == id); }

        #endregion

    }
}