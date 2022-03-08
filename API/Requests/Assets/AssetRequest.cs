using Igtampe.Neco.Common;
using Igtampe.Neco.Common.Assets;

namespace Igtampe.Neco.API.Requests {

    /// <summary>Abstract class for any requests relating to Assets</summary>
    /// <typeparam name="E"></typeparam>
    public abstract class AssetRequest<E> : Nameable, Describable where E : Asset {
        
        /// <summary>Name of this asset</summary>
        public string Name { get; set; } = "";

        /// <summary>Description of this asset</summary>
        public string Description { get; set; } = "";

        /// <summary>Status of this asset</summary>
        public AssetStatus Status { get; set; } = AssetStatus.UNPROCESSED;

        ///// <summary>ID of he account that owns this asset</summary>
        //public string AccountID { get; set; } = "";
        
        //Account ID will not be used here. Instead assets will be transfered using another API Route

    }
}
