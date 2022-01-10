using System;
using System.Collections.Generic;
using System.Linq;

namespace Igtampe.Neco.Common.EzTax {

    /// <summary>EZTax User Info, designed to be used by the frontend</summary>
    public class TaxUserInfo {
        /// <summary>Items from this user</summary>
        public ICollection<IncomeItem> Items { get; set; }

        /// <summary>Calculates this user's Total Monthly Income</summary>
        /// <returns></returns>
        public long TotalMonthlyIncome() { return Items.Sum(I => I.TotalMonthlyIncome()); }

    }
}
