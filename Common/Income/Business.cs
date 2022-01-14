using System.ComponentModel.DataAnnotations;

namespace Igtampe.Neco.Common.Income {

    /// <summary>A business that is set up at an individual location (Like a store or a restaurant)</summary>
    public class Business : IncomeItem {

        /// <summary>Points of Sale for this business (IE: Lines in a supermarket, chairs in a restaurant)</summary>
        [Range(0, int.MaxValue)]
        public int PointsOfSale { get; set; } = 0;

        /// <summary>Average spending of a customer per point of sale</summary>
        [Range(0, int.MaxValue)]
        public int AvgSpend { get; set; } = 0;

        /// <summary>Average customers per hour per point of sale</summary>
        [Range(0, int.MaxValue)]
        public int CustPerHour { get; set; } = 0;

        /// <summary>Hours this business is open a day (should be between 0 and 24)</summary>
        [Range(0, 24)]
        public int HoursOpen { get; set; } = 0;

        /// <summary>Income of this business. Assumes 50% Profit</summary>
        /// <returns></returns>
        public override long Income() => base.Income() + (AvgSpend / 2 * CustPerHour * HoursOpen * PointsOfSale * 30);

    }
}
