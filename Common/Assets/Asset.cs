using Igtampe.Neco.Common.Banking;
using Igtampe.Neco.Common.Income;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Igtampe.Neco.Common.Assets {
    
    /// <summary>Asset Status Enum</summary>
    public enum AssetStatus {
        /// <summary>A Created, but unprocessed asset</summary>
        UNPROCESSED = 0,

        /// <summary>An asset that has been processed and finalized</summary>
        PROCESSED = 1,

        /// <summary>An asset that is for sale</summary>
        FOR_SALE = 2,
    };

    /// <summary>Any physically existing asset a Neco account can own</summary>
    public class Asset : AutomaticallyGeneratableIdentifiable, Nameable, Describable {

        /// <summary>Type of this asset is. Helps the frontend determine what this is</summary>
        [NotMapped]
        public virtual int Type => -1;

        /// <summary>Name of this Asset</summary>
        public string Name { get; set; } = "";

        /// <summary>Description/Notes of this Asset</summary>
        public string Description { get; set; } = "";

        /// <summary>Status of this asset</summary>
        public AssetStatus Status { get; set; } = AssetStatus.UNPROCESSED;

        /// <summary>ID of the owner (for the frontend)</summary>
        [NotMapped]
        public string AccountID => Owner?.ID ?? "";

        /// <summary>Owner of this asset</summary>
        [JsonIgnore]
        public Account? Owner { get; set; }

        /// <summary>Income Items related to this asset</summary>
        [JsonIgnore]
        public List<IncomeItem> RelatedIncomeItems { get; set; } = new();

    }
}
