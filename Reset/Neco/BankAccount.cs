using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Igtampe.Neco.Common {
    /// <summary>Holds the basic information of a bank account, including a shorthand ID </summary>
    public class BankAccount {

        /// <summary>Shorthand ID of this bank account</summary>
        [MinLength(9)]
        [MaxLength(9)]
        public string ID { get; set; }

        /// <summary>Bank this bank account belongs to</summary>
        public Bank Bank { get; set; }

        /// <summary>Details of this bank account</summary>
        public BankAccountDetail Details {get; set;}

        /// <summary>Bank Account type of this bank account</summary>
        public BankAccountType Type { get; set; }

        /// <summary>Owner of this bank account</summary>
        [JsonIgnore]
        public User Owner { get; set; } //Move Owner back here since it's JSON ignore and may be necessary to lookup bank accoutns for a user

        /// <summary>Indicates whether this bank account is closed or not</summary>
        public bool Closed { get; set; }

        /// <summary>Compares this BankAccount item to another object</summary>
        /// <param name="obj"></param>
        /// <returns>True if and only if the object is a BankAccount item and the <see cref="ID"/> matches with this one's</returns>
        public override bool Equals(object obj) {
            if (obj is BankAccount C) { return C.ID == ID; }
            return false;
        }

        /// <summary>Gets a hash code for this BankAccount item. Delegates to <see cref="ID"/></summary>
        /// <returns></returns>
        public override int GetHashCode() { return ID.GetHashCode(); }

        /// <summary>Creates a string representation of this BankAccount item</summary>
        /// <returns>{ID} : {Owner?.Name}'s bank Account in {Bank?.Name}</returns>
        public override string ToString() { return $"{ID} : {Owner?.Name}'s bank Account in {Bank?.Name}"; }
    }
}
