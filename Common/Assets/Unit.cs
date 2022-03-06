using System.ComponentModel.DataAnnotations.Schema;

namespace Igtampe.Neco.Common.Assets {
    /// <summary>Individual units for sale within buildings</summary>
    public class Unit : Asset {

        /// <summary>Type of this asset is. Helps the frontend determine what this is</summary>
        [NotMapped]
        public override int Type => 1;

        /// <summary>Building this unit belongs to</summary>
        public Building? Building { get; set; }

    }
}
