using System.ComponentModel.DataAnnotations;

namespace Igtampe.Neco.Common.Income {

    /// <summary>A hotel</summary>
    public class Hotel : IncomeItem {
        /// <summary>Number of rooms in this hotel</summary>
        [Range(0, int.MaxValue)]
        public int Rooms { get; set; } = 0;

        /// <summary>Number of suites in this hotel</summary>
        [Range(0, int.MaxValue)]
        public int Suites { get; set; } = 0;

        /// <summary>Nightly rate of a room in this hotel</summary>
        [Range(0, int.MaxValue)]
        public int RoomRate { get; set; } = 200;

        /// <summary>Nightly rate of a suite at this hotel</summary>
        [Range(0, int.MaxValue)]
        public int SuiteRate { get; set; } = 400;

        /// <summary>Income of this hotel. Assumes 50% Profit</summary>
        /// <returns></returns>
        public override long Income() => (RateToMonthlyIncome(RoomRate) * Rooms) + (RateToMonthlyIncome(SuiteRate) * Suites) + MiscIncome;

        /// <summary>Converts a nightly rate to monthly income. Assumes 50% profit</summary>
        /// <param name="Rate"></param>
        /// <returns></returns>
        private static int RateToMonthlyIncome(int Rate) => ((Rate / 2) * 365) / 12;

    }
}
