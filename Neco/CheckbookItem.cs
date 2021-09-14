using System;

namespace Igtampe.Neco.Common {

    /// <summary>Holds a Checkbook 2000 Item</summary>
    public class CheckbookItem {

        /// <summary>ID of this item</summary>
        public Guid ID { get; set; }

        /// <summary>Available type of checkbook items</summary>
        public enum ItemType { 
            /// <summary>Item is a bill and must be approved by the Origin</summary>
            BILL, 

            /// <summary>Item is a check and must be approved by the destination</summary>
            CHECK 
        }

        /// <summary>Attatched transaction to this checkbook item</summary>
        public Transaction AttachedTransaciton { get; set; }

        /// <summary>Type of transaction held in this Checkbook Item</summary>
        public ItemType Type { get; set; } = ItemType.CHECK;

        /// <summary>Graphical Variant of the given Checkbook item</summary>
        public int Variant { get; set; } = 0;

        /// <summary>Comment provided for this checkbook item</summary>
        public string Comment { get; set; } = "";

        /// <summary>Compares this Checkbook item to another object</summary>
        /// <param name="obj"></param>
        /// <returns>True if and only if the object is a checkbook item and the <see cref="ID"/> matches with this one's</returns>
        public override bool Equals(object obj) {
            if (obj is CheckbookItem C) { return C.ID == ID; }
            return false;
        }
         
        /// <summary>Gets a hash code for this Checkbook item. Delegates to <see cref="ID"/></summary>
        /// <returns></returns>
        public override int GetHashCode() { return ID.GetHashCode();}

        /// <summary>Creates a string representation of this checkbook item</summary>
        /// <returns>{Id} : {AttachedTransaciton}</returns>
        public override string ToString() { return $"{ID} : {AttachedTransaciton}"; }

    }
}