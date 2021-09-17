using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Igtampe.Neco.Common {

    /// <summary>Defines a type of available bank account</summary>
    public class BankAccountType {

        /// <summary>ID of this bank account type</summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid ID { get; set; }

        /// <summary>Name of this bank account type (IE UMSNB Savings)</summary>
        public string Name { get; set; }

        /// <summary>Bank this type belongs to</summary>
        [JsonIgnore]
        public Bank Bank { get; set; }

        /// <summary>Interest rate of this bank account type</summary>
        [Range(0.0, 1.0)]
        public double InterestRate { get; set; } = 0.0;

        /// <summary>Compares this Bank Account type item to another object</summary>
        /// <param name="obj"></param>
        /// <returns>True if and only if the object is a BankAccountType item and the <see cref="ID"/> matches with this one's</returns>
        public override bool Equals(object obj) {
            if (obj is BankAccountType C) { return C.ID == ID; }
            return false;
        }

        /// <summary>Gets a hash code for this BankAccountType item. Delegates to <see cref="ID"/></summary>
        /// <returns></returns>
        public override int GetHashCode() { return ID.GetHashCode(); }

        /// <summary>Creates a string representation of this BankAccountType item</summary>
        /// <returns>{Name}</returns>
        public override string ToString() { return $"{Name}"; }

    }
}
