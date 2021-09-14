using System;
using System.ComponentModel.DataAnnotations;

namespace Igtampe.Neco.Common {

    /// <summary>Holds information for a NECO Transaction</summary>
    public class Transaction {

        /// <summary>ID of this transaction</summary>        
        public Guid ID { get; set; }

        /// <summary>Amount  transfered by this transaction</summary>
        [Range(0,long.MaxValue)]
        public long Amount { get; set; }

        /// <summary>Time at which this transaction took place</summary>
        public DateTime Time { get; set; } = DateTime.Now;

        /// <summary>Account from which the amount in this transaction will be deducted</summary>
        public BankAccount FromAccount { get; set; }

        /// <summary>Account from which the amount in this transaction will be added to</summary>
        public BankAccount ToAccount { get; set; }

        /// <summary>Whether or not this transaction is taxable</summary>
        public bool Taxable { get; set; } = true;

        /// <summary>Whether or not this transaciton has been executed</summary>
        public bool Executed { get; set; } = false;

        /// <summary>Indicates whether the transaction was executed and failed</summary>
        public bool Failed { get; set; } = false;

        /// <summary>Compares this Transaction to another object</summary>
        /// <param name="obj"></param>
        /// <returns>return true if the object is a transaction, and if its <see cref="ID"/> matches with this one's</returns>
        public override bool Equals(object obj) {
            if (obj is Transaction T) { return T.ID == ID; }
            return false;
        }

        /// <summary>Gets a hashcode for this transaction. Delegates to <see cref="ID"/></summary>
        /// <returns></returns>
        public override int GetHashCode() { return ID.GetHashCode(); }

        /// <summary>Creates a string representation of this transaction</summary>
        /// <returns>{Id} : {Amount} from {FromUser?.Id} to {ToUser?.Id}</returns>
        public override string ToString() { return $"{ID} : {Amount} from {FromAccount?.ID} to {ToAccount?.ID}";}

    }
}
