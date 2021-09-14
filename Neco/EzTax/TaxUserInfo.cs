using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Igtampe.Neco.Common.EzTax {

    /// <summary>EZTax User Info</summary>
    public class TaxUserInfo {

        /// <summary>ID of this userinfo</summary>
        public Guid ID { get; set; }

        /// <summary>User this UserInfo belongs to</summary>
        public User User { get; set; }

        /// <summary>Items from this user</summary>
        [JsonIgnore]
        public ICollection<IncomeItem> Items { get; set; }

        /// <summary>Calculates this user's Total Monthly Income</summary>
        /// <returns></returns>
        public long TotalMonthlyIncome() { return Items.Sum(I => I.TotalMonthlyIncome()); }

        /// <summary>Calcualtes this user's total monthly tax</summary>
        /// <returns></returns>
        public long TotalMonthlyTax() { return Items.Sum(I => I.TotalMonthlyTax()) ; }

        /// <summary>Compares this TaxUserInfo to another object</summary>
        /// <param name="obj"></param>
        /// <returns>True if and only if the object is a TaxUserInfo and the <see cref="ID"/> matches with this one's</returns>
        public override bool Equals(object obj) {
            if (obj is TaxUserInfo C) { return C.ID == ID; }
            return false;
        }

        /// <summary>Gets a hash code for this TaxUserInfo. Delegates to <see cref="ID"/></summary>
        /// <returns></returns>
        public override int GetHashCode() { return ID.GetHashCode(); }

        /// <summary>Creates a string representation of this TaxUserInfo</summary>
        /// <returns>{Id} : Tax info for user {User}</returns>
        public override string ToString() { return $"{ID} : Tax info for user {User}"; }


    }
}
