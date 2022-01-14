namespace Igtampe.Neco.API.Requests {

    /// <summary>Request relating to the creation of a transaction</summary>
    public class TransactionRequest {

        /// <summary>Account from which money will be taken</summary>
        public string Origin { get; set; } = "";

        /// <summary>Account to which money will be deposited</summary>
        public string Destination { get; set; } = "";

        /// <summary>Amount of money to transfer</summary>
        public long Amount { get; set; } = 0;

        /// <summary>Name of the transaction</summary>
        public string Name { get; set; } = "";

        /// <summary>Whether or not to certify this transaction</summary>
        public bool Certify { get; set; } = false;

    }
}
