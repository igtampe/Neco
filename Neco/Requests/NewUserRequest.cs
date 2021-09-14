using System.ComponentModel.DataAnnotations;

namespace Igtampe.Neco.Common.Requests {

    /// <summary>Request to the Neco server to create a new user</summary>
    public class NewUserRequest {

        /// <summary>Name of this new user</summary>
        public string Name { get; set; }
        
        /// <summary>Pin for this new user</summary>
        [MaxLength(4)]
        [MinLength(4)]
        public string Pin { get; set; }

        /// <summary>Type of user to create</summary>
        public UserType Type { get; set; }

    }
}
