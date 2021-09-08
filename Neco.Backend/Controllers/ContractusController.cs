﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Igtampe.Neco.Common.Contractus;
using Igtampe.Neco.Data;

namespace Igtampe.Neco.Backend.Controllers {

    [Route("Contractus")]
    [ApiController]
    public class ContractusController: Controller {
        private readonly NecoContext _context;

        public ContractusController(NecoContext context) { _context = context; }

        // GET: UMSAT
        [HttpGet]
        public async Task<IActionResult> Index() { return Ok(await _context.Contract
            .Include(m=>m.FromUser).ThenInclude(m=>m.Type)
            .Include(m=>m.TopBidder).ThenInclude(m => m.Type)
            .ToListAsync()); }

        // GET: UMSAT/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid? id) {
            if (id == null) { return NotFound(); }

            var contract = await _context.Contract
                .Include(m => m.FromUser).ThenInclude(m => m.Type)
                .Include(m => m.TopBidder).ThenInclude(m => m.Type)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (contract == null) { return NotFound(); }

            return Ok(contract);
        }

        // POST: UMSAT
        [HttpPost]
        public async Task<IActionResult> Create(Contract contract) {
            if (contract.ID != Guid.Empty) { BadRequest("Contract has an ID. Did you mean to edit it?"); }
            contract.ID = Guid.NewGuid();
            _context.Add(contract);
            await _context.SaveChangesAsync();
            return Created($"Contractus/{contract.ID}", contract);
        }

        // PUT: UMSAT/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, Contract contract) {
            if (id != contract.ID) { return NotFound(); }

            try {
                _context.Update(contract);
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!AssetExists(contract.ID)) { return NotFound(); } else { throw; }
            }

            return Ok(contract);
        }

        //DELETE: UMSAT/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id) {
            var asset = await _context.Contract.FindAsync(id);
            _context.Contract.Remove(asset);
            await _context.SaveChangesAsync();
            return Ok(asset);
        }

        private bool AssetExists(Guid id) { return _context.Contract.Any(e => e.ID == id); }
    }
}
