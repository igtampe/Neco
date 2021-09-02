using System;

namespace Igtampe.Neco.Common.EzTax.Subitems {

    /// <summary>Income subitem for an IncomeItem </summary>
    public abstract class IncomeSubitem {

        /// <summary>ID of this subitem</summary>
        public Guid ID { get; set; }

        /// <summary>IncomeItem this Subitem belongs to</summary>
        public IncomeItem IncomeItem { get; set; }

        /// <summary>Name of this IncomeSubItem</summary>
        public string Name { get; set; }

        /// <summary>Gets this item's income</summary>
        /// <returns></returns>
        public abstract int Income();

        /// <summary>Compares this IncomeSubItem to another object</summary>
        /// <param name="obj"></param>
        /// <returns>True if and only if the object is the same type of incomesubitem and the <see cref="ID"/> matches with this one's</returns>
        public override bool Equals(object obj) {
            if (obj is IncomeSubitem C) {
                return C.ID == ID && GetType().Equals(C.GetType()); 
            }
            return false;
        }

        /// <summary>Gets a hash code for this IncomeSubeItem. Delegates to <see cref="ID"/></summary>
        /// <returns></returns>
        public override int GetHashCode() { return ID.GetHashCode(); }

        /// <summary>Creates a string representation of this IncomeSubItem</summary>
        /// <returns>"ID} : {Name}</returns>
        public override string ToString() { return $"{ID} : {Name}"; }


    }
}
