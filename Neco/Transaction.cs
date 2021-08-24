using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igtampe.Neco.Common {

    /// <summary>Holds information for a NECO Transaction</summary>
    public class Transaction {

        /// <summary>ID of this transaction</summary>        
        public Guid Id { get; set; }

        /// <summary>Amount  transfered by this transaction</summary>
        [Range(0,long.MaxValue)]
        public long Amount { get; set; }

        /// <summary>Time at which this transaction took place</summary>
        public DateTime Time { get; set; } = DateTime.Now;

        /// <summary>User this transaction originates from</summary>
        public User FromUser { get; set; }

        /// <summary>User this transaction destinates to</summary>
        public User ToUser { get; set; }

        /// <summary>Account from which the amount in this transaction will be deducted</summary>
        public BankAccount FromAccount { get; set; }

        /// <summary>Account from which the amount in this transaction will be added to</summary>
        public BankAccount ToBankAccount { get; set; }

        /// <summary>Whether or not this transaction is taxable</summary>
        public bool Taxable { get; set; } = true;

        /// <summary>Whether or not this transaciton has been executed</summary>
        public bool Executed { get; set; } = false;

    }
}
