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
            Taxes.Jurisdiction? J = Jurisdiction?.GetTopParent();
            if (J is null || J.Type != Taxes.JurisdictionType.COUNTRY) {
                Console.WriteLine($"Could not find {Name}'s parent jurisdiction or the parent jurisdiction found was not a country: \"{J?.Name}\" was found");
                //throw new InvalidOperationException("Cannot calculate base percentage. Jurisdiction was not found or was not a country.");
            }
            double? BasePercentage = (J?.GetTopParent().Population * 1.0) / US_POPULATION * 1000;
            
            //Buildings and Mergers
            if (RLENetYearly > 10000000000) {
                BasePercentage += .005 * Buildings;
                BasePercentage += .01 * Mergers;
            } else if (RLENetYearly > 3000000000) { 
                BasePercentage += .01 * Buildings;
                BasePercentage += .015 * Mergers;
            } else { 
                BasePercentage += .015 * Buildings;
                BasePercentage += .02 * Mergers;
            }

            //Other boosters
            if (MetroAds) { BasePercentage += .01; }
            if(AirportAds) { BasePercentage += .03; }
            if (International) { BasePercentage += .05; }

            return BasePercentage ?? 0;

        }

        /// <summary>Gets the Income of this corporation</summary>
        /// <returns></returns>
        public override long Income() => base.Income() + Convert.ToInt64(GetIncomePercentage() * (RLENetYearly));
    }
}
