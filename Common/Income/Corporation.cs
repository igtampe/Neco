using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Igtampe.Neco.Common.Income {

    /// <summary>Corporation that generates monthly income</summary>
    public class Corporation : IncomeItem {

        /// <summary>US population (Useful in determining the base percentage for income calculation)</summary>
        private const long US_POPULATION = 329500000;

        /// <summary>Type of this income item. Helps the frontend determine what this is</summary>
        [NotMapped]
        public override int Type { get; set; } = 3;

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

        /// <summary>Gets corporate income percentage of RLE</summary>
        /// <returns></returns>
        protected virtual double GetIncomePercentage() {
            double? BasePercentage = (Jurisdiction?.GetTopParent().Population * 1.0) / US_POPULATION * 100;
            
            //Buildings and Mergers
            if (RLENetYearly > 10000000000) {
                BasePercentage += .0005 * Buildings;
                BasePercentage += .001 * Mergers;
            } else if (RLENetYearly > 3000000000) { 
                BasePercentage += .001 * Buildings;
                BasePercentage += .0015 * Mergers;
            } else { 
                BasePercentage += .0015 * Buildings;
                BasePercentage += .002 * Mergers;
            }

            //Other boosters
            if (MetroAds) { BasePercentage += .001; }
            if(AirportAds) { BasePercentage += .003; }
            if (International) { BasePercentage += .005; }

            return BasePercentage ?? 0;

        }

        /// <summary>Gets the Income of this corporation</summary>
        /// <returns></returns>
        public override long Income() => base.Income() + Convert.ToInt64(GetIncomePercentage() * (RLENetYearly / 12));
    }
}
