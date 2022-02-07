using Igtampe.Neco.Common.Taxes;
using Igtampe.Neco.Common.Banking;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Igtampe.Neco.Common.Income {

    /// <summary>Basic usable, but extendable income item</summary>
    public class IncomeItem : AutomaticallyGeneratableIdentifiable, Nameable, Describable, Locatable, Dateable {

        /// <summary>Type of this income item. Helps the frontend determine what this is</summary>
        [NotMapped]
        public virtual int Type { get; set; } = -1;

        /// <summary>Name of the income item</summary>
        public string Name { get; set; } = "";
        
        /// <summary>Description of the income item</summary>
        public string Description { get; set; } = "";

        /// <summary>Address of this income item</summary>
        public string? Address { get; set; }

        /// <summary>District of this income item</summary>
        public Jurisdiction? Jurisdiction { get; set; }

        /// <summary>Account where this income will be deposited to</summary>
        public Account? Account { get; set; }

        /// <summary>Miscellaneous income of this item</summary>
        [Range(0, long.MaxValue)]
        public long MiscIncome { get; set; } = 0;

        /// <summary>Date this item was created</summary>
        public DateTime DateCreated { get; set; } = DateTime.Now;

        /// <summary>Date this item was last updated</summary>
        public DateTime DateUpdated { get; set; } = DateTime.Now;

        /// <summary>Whether or not this income item is approved and provides income</summary>
        public bool Approved { get; set; } = false;

        /// <summary>Calculated income for this incomeitem. Shortcut for the json serialization</summary>
        [NotMapped]
        public long CalculatedIncome => Income();

        /// <summary>Total income of this item</summary>
        public virtual long Income() => MiscIncome;
    }
}
