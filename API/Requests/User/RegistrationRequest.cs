using Igtampe.Neco.Common;

namespace Igtampe.Neco.API.Requests {

    /// <summary>Request to register to Neco</summary>
    public class RegistrationRequest : Nameable {

        /// <summary>Name of the user registering</summary>
        public string Name { get; set; } = "";

        /// <summary>Password of the user registering</summary>
        public string Password { get; set; } = "";
    }
}
