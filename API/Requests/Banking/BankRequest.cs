using Igtampe.Neco.Common;

namespace Igtampe.Neco.API.Requests {

    /// <summary>Request relating to editing or creating a Bank</summary>
    public class BankRequest : Nameable, Depictable {

        /// <summary>New name of the bank</summary>
        public string Name { get; set; } = "";

        /// <summary>New ImageURL of the bank</summary>
        public string ImageURL { get; set; } = "";

    }
}
