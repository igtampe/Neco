using System.ComponentModel.DataAnnotations;
using Igtampe.Neco.Common.Income;

namespace Igtampe.Neco.API.Requests {

    /// <summary>A hotel</summary>
    public class HotelRequest : IncomeItemRequest<Hotel> {

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

    }
}
