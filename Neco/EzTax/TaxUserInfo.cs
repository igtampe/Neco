using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igtampe.Neco.Common.EzTax {

    /// <summary>EZTax User Info</summary>
    public class TaxUserInfo {

        /// <summary>ID of this userinfo</summary>
        public Guid Id { get; set; }

        /// <summary>User this UserInfo belongs to</summary>
        public User User { get; set; }

        /// <summary>Items from this user</summary>
        public ICollection<IncomeItem> Items { get; set; }

        /// <summary>Calculates this user's Total Monthly Income</summary>
        /// <returns></returns>
        public long TotalMonthlyIncome() { return Items.Sum(I => I.TotalMonthlyIncome()); }

        /// <summary>Calcualtes this user's total monthly tax</summary>
        /// <returns></returns>
        public long TotalMonthlyTax() { return Items.Sum(I => I.TotalMonthlyTax()) ; }
        
    }
}
