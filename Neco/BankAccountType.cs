using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igtampe.Neco.Common {

    /// <summary>Defines a type of available bank account</summary>
    public class BankAccountType {

        /// <summary>ID of this bank account type</summary>
        public Guid Id { get; set; }

        /// <summary>Name of this bank account type (IE UMSNB Savings)</summary>
        public string Name { get; set; }

        /// <summary>Bank this type belongs to</summary>
        public Bank Bank { get; set; }

        /// <summary>Interest rate of this bank account type</summary>
        [Range(0.0, 1.0)]
        public double InterestRate { get; set; } = 0.0;

        /// <summary>Compares this Bank Account type item to another object</summary>
        /// <param name="obj"></param>
        /// <returns>True if and only if the object is a BankAccountType item and the <see cref="Id"/> matches with this one's</returns>
        public override bool Equals(object obj) {
            if (obj is BankAccountType C) { return C.Id == Id; }
            return false;
        }

        /// <summary>Gets a hash code for this BankAccountType item. Delegates to <see cref="Id"/></summary>
        /// <returns></returns>
        public override int GetHashCode() { return Id.GetHashCode(); }

        /// <summary>Creates a string representation of this BankAccountType item</summary>
        /// <returns>{Name}</returns>
        public override string ToString() { return $"{Name}"; }

    }
}
