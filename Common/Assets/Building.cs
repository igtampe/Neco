using Igtampe.Neco.Common.Taxes;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Igtampe.Neco.Common.Assets {

    /// <summary>A set of XYZ Coordiantes</summary>
    public struct Coordinates {

        /// <summary>X coordinates</summary>
        public int X { get; set; } = 0;
        
        /// <summary>Y Coordinates</summary>
        public int Y { get; set; } = 0;
        
        /// <summary>Z Coordinates</summary>
        public int Z { get; set; } = 0;

        /// <summary>Teleport command to this coordinate</summary>
        public string TeleportCommand => $"/tp @p {X} {Y} {Z}";

        /// <summary>Creates a set of coordinates</summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Z"></param>
        public Coordinates(int X, int Y, int Z) {  this.X= X; this.Y= Y; this.Z= Z; }
    }

    /// <summary>Types of building assets that you can have</summary>
    public enum BuildingTypes { 
    
        /// <summary>Other buildings, or throroughly mixed use buildings</summary>
        OTHER = 0,

        /// <summary>Apartments</summary>
        APARTMENT = 1,

        /// <summary>Neighborhoods. A collection of houses</summary>
        NEIGHBORHOOD = 2,

        /// <summary>An individual house</summary>
        HOUSE = 3,

        /// <summary>Hotels</summary>
        HOTEL = 4,

        /// <summary>Office buildings</summary>
        OFFICE = 5,

        /// <summary>A Warehouse or other industrial building</summary>
        WAREHOUSE = 6,

    }

    /// <summary>An ownable building</summary>
    public class Building : Asset, Locatable {

        /// <summary>Type of this asset is. Helps the frontend determine what this is</summary>
        [NotMapped]
        public override int Type => 0;

        /// <summary>Type of this building</summary>
        public BuildingTypes BuildingType { get; set; } = BuildingTypes.OTHER;

        /// <summary>Address of this building</summary>
        public string? Address { get; set; } = "";

        /// <summary>Jurisdiction this building is located in</summary>
        public Jurisdiction? Jurisdiction { get; set; }

        /// <summary>X coordinates</summary>
        [JsonIgnore]
        public int X { get; set; } = 0;

        /// <summary>Y Coordinates</summary>
        [JsonIgnore]
        public int Y { get; set; } = 0;

        /// <summary>Z Coordinates</summary>
        [JsonIgnore]
        public int Z { get; set; } = 0;

        /// <summary>Coordinates of this item</summary>
        [NotMapped]
        public Coordinates Coordinates { 
            get => new(X, Y, Z);
            set {  X = value.X; Y = value.Y; Z = value.Z;  }
        }

        /// <summary>Number of beds in this building. Used to get population information </summary>
        public int Beds { get; set; } = 0;

        /// <summary>List of all units in this building</summary>
        [JsonIgnore]
        public List<Unit> Units { get; set; } = new();

    }
}
