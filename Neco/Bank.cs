using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igtampe.Neco.Common {
    
    /// <summary> A Neco Bank</summary>
    public class Bank {

        /// <summary>ID of this bank (Short name)</summary>
        [MaxLength(5)]
        [MinLength(5)]
        public string Id { get; set; }

        /// <summary>Long Name of this bank</summary>
        public string Name { get; set; } = "";

        /// <summary>Accounts in this bank</summary>
        public ICollection<BankAccount> Accounts { get; set; }

        /// <summary>Bank Account types available in this bank</summary>
        public ICollection<BankAccountType> AccountTypes { get; set; }

    }
}
