namespace Igtampe.Neco.Common.Requests {
    /// <summary>Holds a Neco Request to create and send a new checkbook item</summary>
    public class CheckbookSendItemRequest:INecoRequest {

        /// <summary>ID of the session executing this request</summary>
        public System.Guid SessionID { get; set; }

        /// <summary>Request for the internal transaction in this checkbook item</summary>
        public TransactionRequest TransactRequest { get; set; }

        /// <summary>Type of item being requested</summary>
        public CheckbookItem.ItemType ItemType { get; set; }

        /// <summary>Graphical variant of item being sent</summary>
        public int Variant { get; set; }

        /// <summary>Comment of the item being sent</summary>
        public string Comment { get; set; }

    }
}
