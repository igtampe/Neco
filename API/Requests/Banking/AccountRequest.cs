using Igtampe.Neco.Common;
using Igtampe.Neco.Common.Income;

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
        public string BankID { get; set; } = "";

        /// <summary>ID of the district this account is located in</summary>
        public string JurisdictionID { get; set; } = "";

        /// <summary>Type of income in this account</summary>
        public IncomeType IncomeType { get; set; } = IncomeType.PERSONAL;
    }
}
