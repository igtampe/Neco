namespace Igtampe.Neco.Common.Requests {
    /// <summary>Holds a Neco Transaction Request</summary>
    public class TransactionRequest {

        /// <summary>Session that's executing this request</summary>
        public System.Guid SessionID { get; set; }

        /// <summary>Bank this transaction will originate from</summary>
        public string FromBankID { get; set; }

        /// <summary>Bank this transaction will destinate to</summary>
        public string ToBankID { get; set; }

        /// <summary>Amount to transfer in this transaction</summary>
        public long Amount { get; set; }
    }
}
