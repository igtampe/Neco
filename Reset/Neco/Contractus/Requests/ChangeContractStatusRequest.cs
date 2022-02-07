using System;
using Igtampe.Neco.Common.Requests;

namespace Igtampe.Neco.Common.Contractus.Requests {
    
    /// <summary>Request to change a given contract's status</summary>
    public class ChangeContractStatusRequest:INecoRequest {

        /// <summary>ID of the session executing this request</summary>
        public Guid SessionID { get; set; }

        /// <summary>ID of the contract</summary>
        public Guid ContractID { get; set; }

        /// <summary>State to which to change the contract to</summary>
        public ContractStatus NewStatus { get; set; }

    }
}
