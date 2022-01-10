using System;
using Igtampe.Neco.Common.Requests;

namespace Igtampe.Neco.Common.UMSAT.Requests {
    /// <summary>Request to delete an UMSAT item</summary>
    public class AssetDeleteRequest:INecoRequest {

        /// <summary>ID of the Session executing request</summary>
        public Guid SessionID { get; set; }

        /// <summary>ID of the Asset thats</summary>
        public Guid AssetID { get; set; }

    }
}
