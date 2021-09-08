using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Igtampe.Neco.Common.EzTax.Subitems {

    /// <summary>Subitem that can hold breakdown of items for a hotel with suites and rooms</summary>
    public class Hotel:IncomeSubitem {

        /// <summary>ID of this subitem</summary>
        public override Guid ID { get; set; }

        /// <summary>IncomeItem this Subitem belongs to</summary>
        [JsonIgnore]
        public override IncomeItem IncomeItem { get; set; }

        /// <summary>Name of this IncomeSubItem</summary>
        public override string Name { get; set; }

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

        /// <summary>Misc monthly income for this hotel</summary>
        [Range(0, int.MaxValue)]
        public int MiscIncome { get; set; } = 0;

        /// <summary>Income of this hotel. Assumes 50% Profit</summary>
        /// <returns></returns>
        public override int Income() { return (RateToMonthlyIncome(RoomRate) * Rooms) + (RateToMonthlyIncome(SuiteRate) * Suites); } 

        /// <summary>Converts a nightly rate to monthly income. Assumes 50% profit</summary>
        /// <param name="Rate"></param>
        /// <returns></returns>
        private static int RateToMonthlyIncome(int Rate) {return ((Rate / 2) * 365) / 12;}

    }
}
