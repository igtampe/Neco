using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Igtampe.Neco.Common.EzTax;
using Igtampe.Neco.Common.LandView;

namespace Igtampe.Neco.Common.UMSAT {

    /// <summary>UMSAT Item</summary>
    public class Asset {

        /// <summary>ID of this item</summary>
        public Guid ID { get; set; }

        /// <summary>Name of this Item</summary>
        public string Name { get; set; } = "";

        /// <summary>Description of this item</summary>
        public string Description { get; set; } = "";

        /// <summary>Specific location of this item (Like a neighborhood or county name)</summary>
        public string SpecificLocaiton { get; set; } = "";

        /// <summary>Full location of this item</summary>
        /// <returns></returns>
        public string Location() { return $"{SpecificLocaiton},{IncomeItem.LocalJurisdiction.Name},{IncomeItem.FederalJurisdiction.Name}"; }

        /// <summary>Owner of this item</summary>
        public User Owner { get; set; }

        /// <summary>Whether or not this item is complete</summary>
        public bool Complete { get; set; } = false;

        /// <summary>IncomeItem related to this asset</summary>
        public IncomeItem IncomeItem { get; set; }

        /// <summary>Plot of land this item sits on</summary>
        public Plot Plot { get; set; }

        /// <summary>Image of this item</summary>
        public Byte[] Image { get; set; }

        /// <summary>Date of record creation</summary>
        public DateTime CreationDate { get; set; } = DateTime.Now;

        /// <summary>Date of last update</summary>
        public DateTime UpdateDate { get; set; } = DateTime.Now;

    }
}
