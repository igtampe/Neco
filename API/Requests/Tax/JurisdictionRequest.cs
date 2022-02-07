using Igtampe.Neco.Common;
using Igtampe.Neco.Common.Taxes;

namespace Igtampe.Neco.API.Requests {

    /// <summary>Any request relating to a Jurisdiction</summary>
    public class JurisdictionRequest : Nameable, Depictable {

        /// <summary>Name of this jurisdiction</summary>
        public string Name { get; set; } = "";

        /// <summary>Image URL for the Flag of this jurisdiction</summary>
        public string ImageURL { get; set; } = "";

        /// <summary>Type of this jurisdiction</summary>
        public JurisdictionType? Type { get; set; }

        /// <summary>Population of this jurisdiction (useful for Corporation income calculation)</summary>
        public int Population { get; set; } = 0;

        /// <summary>ID of the account tied to this jurisdiction</summary>
        public string TiedAccountID { get; set; } = "";

        /// <summary>ID of the parent of this jurisdiction</summary>
        public string ParentJurisdictionID { get; set; } = "";
    }
}
