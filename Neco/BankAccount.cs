using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igtampe.Neco.Common {
    /// <summary>Holds the basic information of a bank account, including a shorthand ID </summary>
    public class BankAccount {

        /// <summary>Shorthand ID of this bank account</summary>
        [MinLength(5)]
        [MaxLength(5)]
        public string ID { get; set; }

        /// <summary>Bank this bank account belongs to</summary>
        public Bank Bank { get; set; }

        /// <summary>Details of this bank account</summary>
        public BankAccountDetail Details {get; set;}

        /// <summary>Bank Account type of this bank account</summary>
        public BankAccountType Type { get; set; }

        /// <summary>Compares this BankAccount item to another object</summary>
        /// <param name="obj"></param>
        /// <returns>True if and only if the object is a BankAccount item and the <see cref="ID"/> matches with this one's</returns>
        public override bool Equals(object obj) {
            if (obj is BankAccount C) { return C.ID == ID; }
            return false;
        }

        /// <summary>Gets a hash code for this BankAccount item. Delegates to <see cref="Id"/></summary>
        /// <returns></returns>
        public override int GetHashCode() { return ID.GetHashCode(); }

        /// <summary>Creates a string representation of this BankAccount item</summary>
        /// <returns>{ID} : Bank Account in {Bank.Name}</returns>
        public override string ToString() { return $"{ID} : Bank Account in {Bank.Name}"; }
    }
}
