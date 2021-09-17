using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using Igtampe.Neco.Common.EzTax.Subitems;

namespace Igtampe.Neco.Common.EzTax {

    /// <summary>Holds an EzTax income item</summary>
    public class IncomeItem {

        /// <summary>ID of this income item</summary>
        public Guid ID { get; set; }

        /// <summary>Name of this IncomeItem</summary>
        public string Name { get; set; }

        /// <summary>User this income item belongs to</summary>
        public User User { get; set; }

        /// <summary>Subitems in this Item</summary>
        [NotMapped]
        [JsonIgnore] //While this won't be used in the backend due to serialization and mapping issues, it will be left for the frontend
        public ICollection<IncomeSubitem> Subitems { 
            get {
                List<IncomeSubitem> L = new();
                L.AddRange(Apartments);
                L.AddRange(Businesses);
                L.AddRange(Hotels);
                return L; //This is REALLY stretching the definition of a Property but OK.
            } 
        }

        /// <summary>Apartments in this IncomeItem</summary>
        public ICollection<Apartment> Apartments { get; set; }

        /// <summary>Businesses in this IncomeItem</summary>
        public ICollection<Business> Businesses { get; set; }

        /// <summary>Hotels in this IncomeItem</summary>
        public ICollection<Hotel> Hotels { get; set; }

        /// <summary>Other miscellaneous income in this income item</summary>
        [Range(0, long.MaxValue)]
        public long MiscIncome { get; set; } = 0;

        /// <summary>Local Jurisdiction of this IncomeItem (IE: Newpond)</summary>
        public TaxJurisdiction LocalJurisdiction { get; set; }

        /// <summary>Federal Jurisdiction of this IncomeItem (IE: UMS)</summary>
        public TaxJurisdiction FederalJurisdiction { get; set; }

        /// <summary>Returns the sum of all incomes from subitems contained in this item, and miscincome</summary>
        /// <returns></returns>
        public long TotalMonthlyIncome() { return Subitems.Sum(I => I.Income()) + MiscIncome; } //MIRATE QUE BELLEZA

        /// <summary>Compares this IncomeItem to another object</summary>
        /// <param name="obj"></param>
        /// <returns>True if and only if the object is a IncomeItem and the <see cref="ID"/> matches with this one's</returns>
        public override bool Equals(object obj) {
            if (obj is IncomeItem C) { return C.ID == ID; }
            return false;
        }

        /// <summary>Gets a hash code for this IncomeItem. Delegates to <see cref="ID"/></summary>
        /// <returns></returns>
        public override int GetHashCode() { return ID.GetHashCode(); }

        /// <summary>Creates a string representation of this IncoemItem</summary>
        /// <returns>{ID} : {Name}</returns>
        public override string ToString() { return $"{ID} : {Name}"; }


    }
}
