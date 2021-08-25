using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Igtampe.Neco.Common;
using Igtampe.Neco.Common.Auth;
using Igtampe.Neco.Data;

namespace Igtampe.Neco.Backend.Controllers {

    [Route("Auth")]
    [ApiController]
    public class AuthController: Controller {
        private readonly AuthContext _context;

        public AuthController(AuthContext context) { _context = context; }

        // POST: Auth
        [HttpPost]
        public async Task<IActionResult> Check(UserAuth U) {
            if (string.IsNullOrEmpty(U.Id)) { return BadRequest("Blank User Auth object"); }

            //Find a user:
            UserAuth DBU = await _context.Users.FindAsync(U.Id);
            return Ok(DBU.Equals(U));
        }

        // PUT: Auth
        [HttpPut]
        public async Task<IActionResult> Update(PasswordChangeRequest PCR) {
            if (string.IsNullOrEmpty(PCR.UserID)) { return BadRequest("Blank User Auth object"); }

            //Find a user:
            UserAuth DBU = await _context.Users.FindAsync(PCR.UserID);
            if (!DBU.CheckPin(PCR.CurrentPassword)) { return BadRequest("Password did not match"); }

            //OK go for change
            DBU.Pin = PCR.NewPassword;
            _context.Update(DBU);
            await _context.SaveChangesAsync();

            return Ok(true);
        }

    }
}