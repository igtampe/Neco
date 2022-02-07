using System;
using Igtampe.Neco.Common.Requests;

namespace Igtampe.Neco.Common.Contractus.Requests {

    /// <summary>Request to add a bid to a given contract</summary>
    public class AddBidRequest: INecoRequest {
        
        /// <summary>ID of the session executing this request</summary>
        public Guid SessionID { get; set; }

        /// <summary>ID of the contract this request is for</summary>
        public Guid ContractID { get; set; }

        /// <summary>New Bid to post for this contract</summary>
        public long Bid { get; set; }

    }
}
