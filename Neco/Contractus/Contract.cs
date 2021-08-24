using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igtampe.Neco.Common.Contractus {
    /// <summary>Holds a Contractus Contract</summary>
    public class Contract {

        /// <summary>ID Of this contract</summary>
        public Guid ID { get; set; }

        /// <summary>Name of this contract</summary>
        public string Name { get; set; } = "";

        /// <summary>Description of this contract</summary>
        public string Description { get; set; } = "";

        /// <summary>User who's posted this contract</summary>
        public User FromUser { get; set; }
        
        /// <summary>Top Bidder for this contract </summary>
        public User TopBidder { get; set; }

        /// <summary>Amount bidded by the <see cref="TopBidder"/></summary>
        [Range(1, long.MaxValue)]
        public long Amount { get; set; } = long.MaxValue;

        /// <summary>Whether or not this contract is still up for auction or not</summary>
        public bool UpForAuction { get; set; } = true;

        /// <summary>Whether or not this contract has been completed </summary>
        public bool Completed { get; set; } = false;

    }
}
