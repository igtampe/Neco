namespace Igtampe.Neco.Common.Requests {
    /// <summary>Holds a request to commit an action to a Neco Bank Account (IE Close, Logs)</summary>
    public class BankAccountActionRequest:INecoRequest {

        /// <summary>ID of the session executing this request</summary>
        public System.Guid SessionID { get; set; }

        /// <summary>ID of the bank account to close</summary>
        public string BankAccountID { get; set; }

    }
}
