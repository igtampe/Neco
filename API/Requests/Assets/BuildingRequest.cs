using Igtampe.Neco.Common.Assets;

namespace Igtampe.Neco.API.Requests {

    /// <summary>Request to create or update building data</summary>
    public class BuildingRequest : AssetRequest<Building> {

        /// <summary>Type of this building</summary>
        public BuildingTypes BuildingType { get; set; } = BuildingTypes.OTHER;

        /// <summary>Address of this building</summary>
        public string Address { get; set; } = "";

        /// <summary>Jurisdiction this building is in</summary>
        public string JurisdictionID { get; set; } = "";

        /// <summary>Coordinates of this Building</summary>
        public Coordinates Coordinates { get; set; } = new(0, 0, 0);

        /// <summary>Number of beds in this building</summary>
        public int Beds { get; set; } = 0;

    }
}
