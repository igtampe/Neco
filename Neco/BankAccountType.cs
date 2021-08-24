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

    }
}
