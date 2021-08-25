using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Igtampe.Neco.Common.UMSAT;
using Igtampe.Neco.Data;

namespace Igtampe.Neco.Backend.Controllers {

    [Route("UMSAT")]
    [ApiController]
    public class UMSATController: Controller {

        private readonly UMSATContext _context;
        public UMSATController(UMSATContext context) {_context = context;}

        // GET: UMSAT
        [HttpGet]
        public async Task<IActionResult> Index() { return Ok(await _context.Assets.ToListAsync()); }

        // GET: UMSAT/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid? id) {
            if (id == null) { return NotFound(); }

            var asset = await _context.Assets .FirstOrDefaultAsync(m => m.ID == id);
            if (asset == null) { return NotFound(); }

            return Ok(asset);
        }

        // POST: UMSAT
        [HttpPost]
        public async Task<IActionResult> Create(Asset asset) {
            if (asset.ID != Guid.Empty) { BadRequest("Asset has an ID. Did you mean to edit it?"); }
            asset.ID = Guid.NewGuid();
            _context.Add(asset);
            await _context.SaveChangesAsync();
            return Created($"UMSAT/{asset.ID}", asset);
        }

        // PUT: UMSAT/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, Asset asset) {
            if (id != asset.ID) { return NotFound(); }

            try {
                _context.Update(asset);
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!AssetExists(asset.ID)) { return NotFound(); } else { throw; }
            }

            return Ok(asset);
        }

        //DELETE: UMSAT/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id) {
            var asset = await _context.Assets.FindAsync(id);
            _context.Assets.Remove(asset);
            await _context.SaveChangesAsync();
            return Ok(asset);
        }

        private bool AssetExists(Guid id) { return _context.Assets.Any(e => e.ID == id); }
    }
}
