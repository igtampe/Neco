using Igtampe.Neco.Common;
using Igtampe.Neco.Common.Income;

namespace Igtampe.Neco.API.Requests {

    /// <summary>Abstract class for any type of request relating to any type of income item</summary>
    /// <typeparam name="E">Income Item this request corresponds to</typeparam>
    public abstract class IncomeItemRequest<E> : Nameable, Describable where E : IncomeItem {

        /// <summary>Name of the income item</summary>
        public string Name { get; set; } = "";

        /// <summary>Description of the income item</summary>
        public string Description { get; set; } = "";

        /// <summary>Address of this income item</summary>
        public string? Address { get; set; }

        /// <summary>District of this income item</summary>
        public string JurisdictionID { get; set; } = "";

        /// <summary>Account where this income will be deposited to</summary>
        public string AccountID { get; set; } = "";

        /// <summary>Miscellaneous income of this item</summary>
        public long MiscIncome { get; set; } = 0;

    }
}
