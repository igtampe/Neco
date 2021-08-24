using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igtampe.Neco.Common.EzTax {

    /// <summary>Tax Bracket for a <see cref="TaxJurisdiction"/></summary>
    public class TaxBracket {

        /// <summary>ID of this Bracket</summary>
        public Guid ID { get; set; }

        /// <summary>Jurisdiction this tax bracket belongs to</summary>
        public TaxJurisdiction Jurisdiction { get; set; }

        /// <summary>Name of this tax bracket</summary>
        public string Name { get; set; } = "";

        /// <summary>Income amount at which this bracket begins (Inclusive)</summary>
        [Range(0, int.MaxValue)]
        public long Start { get; set; } = 0;

        /// <summary>Income amount at which this bracket ends (Exclusive)</summary>
        [Range(0, long.MaxValue)]
        public long End { get; set; } = long.MaxValue;

        /// <summary><see cref="UserType"/> This bracket belongs to (IE Corporate)</summary>
        public UserType Type { get; set; }

        /// <summary>Rate of this Tax Bracket</summary>
        [Range(0.0, 1.0)]
        public double Rate { get; set; } = 0.0;

    }
}
