using System.Text.Json.Serialization;

namespace Igtampe.Neco.Common.Banking {

    /// <summary>A representation of a financial institution</summary>
    public class Bank : Identifiable<string>, Nameable, Depictable {

        /// <summary>Name of this bank</summary>
        public string Name { get; set; } = "";

        /// <summary>Image URL of the logo of this bank</summary>
        public string ImageURL { get; set; } = "";

        /// <summary>Accounts with this Bank</summary>
        [JsonIgnore]
        public List<Account> Accounts { get; set; } = new();
    }
}
