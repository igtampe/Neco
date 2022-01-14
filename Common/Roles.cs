using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Igtampe.Neco.Common {
    
    /// <summary>User roles</summary>
    public class Roles : AutomaticallyGeneratableIdentifiable {

        /// <summary>Whether or not this user is a Neco Admin</summary>
        public bool Admin { get; set; } = false;

        /// <summary>Whether or not this user is a Government Official</summary>
        public bool Government { get; set; } = false;

        /// <summary>Whether or not this user is a member of the Salary Determination Committee</summary>
        public bool SDC { get; set; } = false;

        /// <summary>Whether or not this user is allowed to upload images to the server</summary>
        public bool ImageUploader { get; set; } = false;

    }
}
