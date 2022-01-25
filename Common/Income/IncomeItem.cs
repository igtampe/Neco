using Igtampe.Neco.Common.Taxes;
using Igtampe.Neco.Common.Banking;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Igtampe.Neco.Common.Income {

    /// <summary>Basic usable, but extendable income item</summary>
    public class IncomeItem : AutomaticallyGeneratableIdentifiable, Nameable, Describable, Locatable, Certifiable, Dateable {

        /// <summary>Name of the income item</summary>
        public string Name { get; set; } = "";
        
        /// <summary>Description of the income item</summary>
        public string Description { get; set; } = "";

        /// <summary>Address of this income item</summary>
        public string? Address { get; set; }

        /// <summary>District of this income item</summary>
        public Jurisdiction? Jurisdiction { get; set; }

        /// <summary>Account where this income will be deposited to</summary>
        [JsonIgnore]
        public Account? Account { get; set; }

        /// <summary>Miscellaneous income of this item</summary>
        [Range(0, long.MaxValue)]
        public long MiscIncome { get; set; } = 0;

        /// <summary>Date this item was created</summary>
        public DateTime DateCreated { get; set; } = DateTime.Now;

        /// <summary>Date this item was last updated</summary>
        public DateTime DateUpdated { get; set; } = DateTime.Now;

        /// <summary>Creates a certification for this income item</summary>
        /// <param name="Certifier"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public CertifiedItem GenerateCertification(User Certifier) => new() { CertifiedBy = Certifier, Date = DateTime.Now, Text = $"{Name} was certified with {Income():n0} Income" };

        /// <summary>Total income of this item</summary>
        public virtual long Income() => MiscIncome;
    }
}
