using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Igtampe.Neco.Common;
using Igtampe.Neco.Common.Requests;
using Igtampe.Neco.Data;

namespace Igtampe.Neco.Backend.Controllers {

    [Route("Auth")]
    [ApiController]
    public class AuthController: Controller {
        private readonly NecoContext _context;

        public AuthController(NecoContext context) { _context = context; }

        // POST: Auth
        [HttpPost]
        public async Task<IActionResult> Check(UserAuth U) {
            if (string.IsNullOrEmpty(U.Id)) { return BadRequest("Blank User Auth object"); }

            //Find a user:
            UserAuth DBU = await _context.UserAuth.FindAsync(U.Id);
            return Ok(DBU.Equals(U));
        }

        // PUT: Auth
        [HttpPut]
        public async Task<IActionResult> Update(PasswordChangeRequest PCR) {
            if (PCR.SessionID == System.Guid.Empty) { return BadRequest("Blank Session ID"); }

            //Find Session
            Session S = SessionManager.Manager.FindSession(PCR.SessionID);
            if (S == null) { return BadRequest("Session ID not found"); }

            //Find a user:
            UserAuth DBU = await _context.UserAuth.FindAsync(S.UserID);
            if (!DBU.CheckPin(PCR.CurrentPassword)) { return BadRequest("Password did not match"); }

            //OK go for change
            DBU.Pin = PCR.NewPassword;
            _context.Update(DBU);
            await _context.SaveChangesAsync();

            return Ok(true);
        }

    }
}