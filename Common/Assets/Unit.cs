namespace Igtampe.Neco.Common.Assets {
    /// <summary>Individual units for sale within buildings</summary>
    public class Unit : Asset {

        /// <summary>Building this unit belongs to</summary>
        public Building? Building { get; set; }

    }
}
