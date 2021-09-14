using System;
using System.ComponentModel.DataAnnotations;

namespace Igtampe.Neco.Common.Contractus {
    /// <summary>Holds a Contractus Contract</summary>
    public class Contract {

        /// <summary>ID Of this contract</summary>
        public Guid ID { get; set; }

        /// <summary>Name of this contract</summary>
        public string Name { get; set; } = "";

        /// <summary>Description of this contract</summary>
        public string Description { get; set; } = "";

        /// <summary>User who's posted this contract</summary>
        public User FromUser { get; set; }
        
        /// <summary>Top Bidder for this contract </summary>
        public User TopBidder { get; set; }

        /// <summary>Amount bidded by the <see cref="TopBidder"/></summary>
        [Range(1, long.MaxValue)]
        public long Amount { get; set; } = long.MaxValue;

        /// <summary>Whether or not this contract is still up for auction or not</summary>
        public bool UpForAuction { get; set; } = true;

        /// <summary>Whether or not this contract has been completed </summary>
        public bool Completed { get; set; } = false;

        /// <summary>Compares this Contract to another object</summary>
        /// <param name="obj"></param>
        /// <returns>True if and only if the object is a Contract and the <see cref="ID"/> matches with this one's</returns>
        public override bool Equals(object obj) {
            if (obj is Contract C) { return C.ID == ID; }
            return false;
        }

        /// <summary>Gets a hash code for this Contract. Delegates to <see cref="ID"/></summary>
        /// <returns></returns>
        public override int GetHashCode() { return ID.GetHashCode(); }

        /// <summary>Creates a string representation of this Contract</summary>
        /// <returns>"ID} : {Name}</returns>
        public override string ToString() { return $"{ID} : {Name}"; }


    }
}
