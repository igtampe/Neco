using Igtampe.Neco.Common;

namespace Igtampe.Neco.API.Requests {

    /// <summary>Request to log in to Neco </summary>
    public class LoginRequest : Identifiable<string> {

        /// <summary>Password of the user</summary>
        public string Password { get; set; } = "";

    }
}
