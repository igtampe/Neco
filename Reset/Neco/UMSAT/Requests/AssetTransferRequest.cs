using System;
using Igtampe.Neco.Common.Requests;

namespace Igtampe.Neco.Common.UMSAT.Requests {
    /// <summary>Request to transfer asset ownership</summary>
    public class AssetTransferRequest:INecoRequest {

        /// <summary>ID of the Session executing this request</summary>
        public Guid SessionID { get; set; }

        /// <summary>ID of the asset to transfer ownership</summary>
        public Guid AssetID { get; set; }

        /// <summary>User ID which will be the new owner of this asset after the transfer</summary>
        public string NewOwnerID { get; set; }

    }
}


