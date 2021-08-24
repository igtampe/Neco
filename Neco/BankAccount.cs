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

        /// <summary>Balance in this bank account</summary>
        public long Balance { get; set; } = 0;

        /// <summary>Boolean to check if this bank account is overdrafted</summary>
        /// <returns></returns>
        public bool IsOverdrafted() { return Balance < 0; }

    }
}
