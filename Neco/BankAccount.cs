using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igtampe.Neco.Common {

    /// <summary>Holds a NECO Bank Account</summary>
    public class BankAccount {

        /// <summary>ID of this bank account</summary>
        public Guid Id { get; set; }

        /// <summary>Bank this bank account belongs to</summary>
        public Bank Bank { get; set; }

        /// <summary>Bank Account type of this bank account</summary>
        public BankAccountType Type { get; set; }

        /// <summary>Owner of this bank account</summary>
        public User Owner { get; set; }

        /// <summary>Balance in this bank account</summary>
        public long Balance { get; set; } = 0;

        /// <summary>Boolean to check if this bank account is overdrafted</summary>
        /// <returns></returns>
        public bool IsOverdrafted() { return Balance < 0; }

        /// <summary>Compares this BankAccount item to another object</summary>
        /// <param name="obj"></param>
        /// <returns>True if and only if the object is a BankAccount item and the <see cref="Id"/> matches with this one's</returns>
        public override bool Equals(object obj) {
            if (obj is BankAccount C) { return C.Id == Id; }
            return false;
        }

        /// <summary>Gets a hash code for this BankAccount item. Delegates to <see cref="Id"/></summary>
        /// <returns></returns>
        public override int GetHashCode() { return Id.GetHashCode(); }

        /// <summary>Creates a string representation of this BankAccount item</summary>
        /// <returns>{Id} : {Owner?.Name}'s {Type?.Name} account with balance {Balance:N0}p</returns>
        public override string ToString() { return $"{Id} : {Owner?.Name}'s {Type?.Name} account with balance {Balance:N0}p"; }

    }
}
