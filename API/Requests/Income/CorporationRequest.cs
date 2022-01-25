using System.ComponentModel.DataAnnotations;
using Igtampe.Neco.Common.Income;

namespace Igtampe.Neco.API.Requests {

    /// <summary>Corporation that generates monthly income</summary>
    public class CorporationRequest<E> : IncomeItemRequest<E> where E : Corporation { //This flexibility is here because of Airline

        /// <summary>Name of the real life equivalent company to base this on</summary>
        public string RLE { get; set; } = "";

        /// <summary>Net yearly income of the real life equivalent (In US Dollars)</summary>
        [Range(0, long.MaxValue)]
        public long RLENetYearly { get; set; } = 0;

        /// <summary>Additional buildings this company contains</summary>
        [Range(0, int.MaxValue)]
        public int Buildings { get; set; } = 0;

        /// <summary>Company mergers</summary>
        [Range(0, int.MaxValue)]
        public int Mergers { get; set; } = 0;

        /// <summary>Whether or not this company has purchased metro ads</summary>
        public bool MetroAds { get; set; } = false;

        /// <summary>Whether or not this company has purchased airport ads</summary>
        public bool AirportAds { get; set; } = false;

        /// <summary>Whether or not this company is a multinational</summary>
        public bool International { get; set; }
    }
}
