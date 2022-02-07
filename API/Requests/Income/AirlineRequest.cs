using System.ComponentModel.DataAnnotations;
using Igtampe.Neco.Common.Income;

namespace Igtampe.Neco.API.Requests {

    /// <summary>An Airline that has gates</summary>
    public class AirlineRequest : CorporationRequest<Airline> {

        /// <summary>Number of active small gates this airline has</summary>
        [Range(0, int.MaxValue)]
        public int GatesSM { get; set; } = 0;

        /// <summary>Number of active medium gates this airline has</summary>
        [Range(0, int.MaxValue)]
        public int GatesMD { get; set; } = 0;

        /// <summary>Number of active large gates this airline has</summary>
        [Range(0, int.MaxValue)]
        public int GatesLG { get; set; } = 0;

    }
}
