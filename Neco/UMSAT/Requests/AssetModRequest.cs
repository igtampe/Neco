using System;
using Igtampe.Neco.Common.Requests;

namespace Igtampe.Neco.Common.UMSAT.Requests {
    /// <summary>Request to modify or create an asset</summary>
    public class AssetModRequest:INecoRequest {

        /// <summary>ID of the Session executing this request</summary>
        public Guid SessionID { get; set; }

        /// <summary>ID of the asset to modify </summary>
        public Guid AssetID { get; set; }

        /// <summary>Asset name</summary>
        public string Name { get; set; }

        /// <summary>Asset description</summary>
        public string Description { get; set; }

        /// <summary>Asset specific location</summary>
        public string SpecificLocation { get; set; }

        /// <summary>Asset Completion State</summary>
        public bool Complete { get; set; }

        /// <summary>ID of the tied LandView Plot</summary>
        public Guid PlotID { get; set; }
    }
}
