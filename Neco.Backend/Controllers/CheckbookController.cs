using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Igtampe.Neco.Common;
using Igtampe.Neco.Data;

namespace Igtampe.Neco.Backend.Controllers {

    [Route("Checkbook")]
    [ApiController]
    public class CheckbookController: Controller {
        private readonly NecoContext _context;

        public CheckbookController(NecoContext context) { _context = context; }

        // GET: Checkbook
        [HttpGet]
        public async Task<IActionResult> Index() { return Ok(await _context.CheckbookItem
            //.Include(m=>m.AttachedTransaciton).ThenInclude(m=>m.FromUser).ThenInclude(m=>m.Type)
            //.Include(m => m.AttachedTransaciton).ThenInclude(m => m.ToUser).ThenInclude(m => m.Type)
            .ToListAsync()); }

        // GET: Checkbook/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid? id) {
            if (id == null) { return NotFound(); }
            var asset = await _context.CheckbookItem
                //.Include(m => m.AttachedTransaciton).ThenInclude(m => m.FromUser).ThenInclude(m => m.Type)
                //.Include(m => m.AttachedTransaciton).ThenInclude(m => m.ToUser).ThenInclude(m => m.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asset == null) { return NotFound(); }
            return Ok(asset);
        }

        // GET: Checkbook/User
        [HttpPost("User")]
        public async Task<IActionResult> IndexFromUser(User U) {
            if (string.IsNullOrEmpty(U?.Id)) { return NotFound(); }

            var asset = await _context.CheckbookItem
                //.Include(m => m.AttachedTransaciton).ThenInclude(m => m.FromUser).ThenInclude(m => m.Type)
                //.Include(m => m.AttachedTransaciton).ThenInclude(m => m.ToUser).ThenInclude(m => m.Type)
                //.Where(m => (m.AttachedTransaciton.FromUser.Id == U.Id && m.Type == CheckbookItem.ItemType.BILL)
                //        || (m.AttachedTransaciton.ToUser.Id == U.Id && m.Type == CheckbookItem.ItemType.CHECK))
            .ToListAsync();
            if (asset == null) { return NotFound(); }
            return Ok(asset);
        }


        // Checkbook: UMSAT
        [HttpPost]
        public async Task<IActionResult> Create(CheckbookItem asset) {
            if (asset.Id != Guid.Empty) { BadRequest("Asset has an ID. Did you mean to edit it?"); }
            asset.Id = Guid.NewGuid();
            _context.Add(asset);
            await _context.SaveChangesAsync();
            return Created($"Checkbook/{asset.Id}", asset);
        }

        // PUT: UMSAT/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, CheckbookItem asset) {
            if (id != asset.Id) { return NotFound(); }

            try {
                _context.Update(asset);
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!CheckbookItemExists(asset.Id)) { return NotFound(); } else { throw; }
            }

            return Ok(asset);
        }

        //DELETE: UMSAT/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id) {
            var asset = await _context.CheckbookItem.FindAsync(id);
            _context.CheckbookItem.Remove(asset);
            await _context.SaveChangesAsync();
            return Ok(asset);
        }

        private bool CheckbookItemExists(Guid id) { return _context.CheckbookItem.Any(e => e.Id == id); }
    }
}
