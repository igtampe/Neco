using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igtampe.Neco.Common.EzTax {

    /// <summary>Holds an EzTax income item</summary>
    public class IncomeItem {

        /// <summary>ID of this income item</summary>
        public Guid ID { get; set; }

        /// <summary>User this income item belongs to</summary>
        public User User { get; set; }

        /// <summary>Subitems in this Item</summary>
        public ICollection<Subitems.IIncomeSubitem> Subitems { get; set; }

        /// <summary>Other miscellaneous income in this income item</summary>
        [Range(0, long.MaxValue)]
        public long MiscIncome { get; set; } = 0;

        /// <summary>Local Jurisdiction of this IncomeItem (IE: Newpond)</summary>
        public TaxJurisdiction LocalJurisdiction { get; set; }

        /// <summary>Federal Jurisdiction of this IncomeItem (IE: UMS)</summary>
        public TaxJurisdiction FederalJurisdiction { get; set; }

        /// <summary>Returns the sum of all incomes from subitems contained in this item, and miscincome</summary>
        /// <returns></returns>
        public long TotalMonthlyIncome() { return Subitems.Sum(I => I.Income()) + MiscIncome; } //MIRATE QUE BELLEZA

        /// <summary>Calculates the monthly tax to <see cref="LocalJurisdiction"/></summary>
        /// <returns></returns>
        public long LocalTax() { return LocalJurisdiction.CalculateTax(User, TotalMonthlyIncome()); }

        /// <summary>Calculates the monthly tax to <see cref="FederalJurisdiction"/></summary>
        /// <returns></returns>
        public long FederalTax() { return FederalJurisdiction.CalculateTax(User, TotalMonthlyIncome()); }

        /// <summary>Calculates the total monthly tax for this item</summary>
        /// <returns></returns>
        public long TotalMonthlyTax() {
            long TMI = TotalMonthlyIncome();
            return LocalJurisdiction.CalculateTax(User, TMI) + FederalJurisdiction.CalculateTax(User, TMI);
        }

    }
}
