using System;

namespace Igtampe.Neco.Common {

    /// <summary>Item in the Certification ledger for NECO</summary>
    public class CertifiedItem {

        /// <summary>ID of this certified item</summary>
        public Guid ID { get; set; }

        /// <summary>Human readable text in case the object fails to load</summary>
        public string Text { get; set; } = "";

        /// <summary>User that certified this item</summary>
        public User CertifiedBy { get; set; }

        /// <summary>Date and time at which this was certified</summary>
        public DateTime Date { get; set; } = DateTime.Now;

        /// <summary>Compares this Certified item to another object</summary>
        /// <param name="obj"></param>
        /// <returns>True if and only if the object is a certified item and the <see cref="ID"/> matches with this one's</returns>
        public override bool Equals(object obj) {
            if (obj is CertifiedItem C) { return C.ID == ID; }
            return false;
        }

        /// <summary>Gets a hash code for this Certified item. Delegates to <see cref="ID"/></summary>
        /// <returns></returns>
        public override int GetHashCode() { return ID.GetHashCode(); }

        /// <summary>Creates a string representation of this certified item</summary>
        /// <returns>{Id} : [{Date}] {Text}</returns>
        public override string ToString() { return $"{ID} : [{Date}] {Text}"; }

    }
}
