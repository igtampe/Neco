using Igtampe.Neco.Common;

namespace Igtampe.Neco.API.Requests {

    /// <summary>Request relating to editing or creating a bank account</summary>
    public class AccountRequest : Nameable {

        /// <summary>Name of this Account</summary>
        public string Name { get; set; } = "";

        /// <summary>Whether or not this account is publicly listed</summary>
        public bool PubliclyListed { get; set; } = false;

        /// <summary>Address of this Account</summary>
        public string Address { get; set; } = "";

        /// <summary>ID of the Bank this account belongs to</summary>
        public Guid? BankID { get; set; } = Guid.Empty;

        /// <summary>ID of the district this account is located in</summary>
        public Guid DistrictID { get; set; } = Guid.Empty;
    }
}
