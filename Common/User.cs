using System.Text.Json.Serialization;
using Igtampe.Neco.Common.IDGenerators;
using Igtampe.Neco.Common.Banking;

namespace Igtampe.Neco.Common {

    /// <summary>A Neco User</summary>
    public class User : ManuallyGeneratableIdentifiable<string>, Nameable, Depictable {

        private readonly static IDGenerator<string> Gen = new NumericalGenerator(5);

        /// <summary>Name of this user</summary>
        public string Name { get; set; } = "";

        /// <summary>Profile picture of this user</summary>
        public string ImageURL { get; set; } = "";

        /// <summary>Password of this User</summary>
        [JsonIgnore]
        public string Password { get; set; } = "";

        /// <summary>Roles of this user</summary>
        public Roles Roles { get; set; } = new();

        //-[Banking]-

        /// <summary>List of all accounts this person holds</summary>
        [JsonIgnore]
        public List<Account> Accounts { get; set; } = new();

        /// <summary>Notifications this user has</summary>
        [JsonIgnore]
        public List<Notification> Notifications { get; set; } = new();

        //-[Overrides]-

        /// <summary>Generator for a UserID </summary>
        public override IDGenerator<string> IDGenerator => Gen;
    }
}
