using System;
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

        /// <summary>Compares this Asset to another object</summary>
        /// <param name="obj"></param>
        /// <returns>True if and only if the object is an Asset and the <see cref="ID"/> matches with this one's</returns>
        public override bool Equals(object obj) {
            if (obj is Asset C) { return C.ID == ID; }
            return false;
        }

        /// <summary>Gets a hash code for this Asset. Delegates to <see cref="ID"/></summary>
        /// <returns></returns>
        public override int GetHashCode() { return ID.GetHashCode(); }

        /// <summary>Creates a string representation of this Asset</summary>
        /// <returns>{ID} : {Name}</returns>
        public override string ToString() { return $"{ID} : {Name}"; }

    }
}
