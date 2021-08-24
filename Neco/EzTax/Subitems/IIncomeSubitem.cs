using System;

namespace Igtampe.Neco.Common.EzTax.Subitems {

    /// <summary>Income subitem for an IncomeItem </summary>
    public interface IIncomeSubitem {

        /// <summary>ID of this subitem</summary>
        public Guid ID { get; set; }

        /// <summary>IncomeItem this Subitem belongs to</summary>
        public IncomeItem IncomeItem { get; set; }

        /// <summary>Name of this IncomeSubItem</summary>
        public string Name { get; set; }

        /// <summary>Gets this item's income</summary>
        /// <returns></returns>
        public int Income();

    }
}
