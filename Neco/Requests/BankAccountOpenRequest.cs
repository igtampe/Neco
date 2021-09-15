namespace Igtampe.Neco.Common.Requests {
    /// <summary>Holds a request to Open a Neco bank account</summary>
    public class BankAccountOpenRequest:INecoRequest {

        /// <summary>ID of the session executing this request</summary>
        public System.Guid SessionID { get; set; }

        /// <summary>ID of the bank account type to open</summary>
        public System.Guid BankAccountTypeID { get; set; }

    }
}
