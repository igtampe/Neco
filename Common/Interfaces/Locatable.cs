using Igtampe.Neco.Common.Taxes;

namespace Igtampe.Neco.Common {

    /// <summary>Interface for any object that's locatable</summary>
    public interface Locatable {

        /// <summary>Address of this object</summary>
        public string? Address { get; set; }

        /// <summary>Jurisdiction the object is located in</summary>
        public Jurisdiction? Jurisdiction { get; set; }
    }
}
