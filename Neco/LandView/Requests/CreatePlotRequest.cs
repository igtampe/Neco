using System;
using System.Drawing;
using Igtampe.Neco.Common.Requests;

namespace Igtampe.Neco.Common.LandView.Requests {
    /// <summary>Request to create a plot on the Landview system</summary>
    public class CreatePlotRequest:INecoRequest {

        /// <summary>ID of the session executing this request</summary>
        public Guid SessionID { get; set; }

        /// <summary>ID of the district this Plot should belong to</summary>
        public Guid CountryID { get; set; }

        /// <summary>Name of the plot</summary>
        public string Name { get; set; }

        /// <summary>Points that define the area of the plot</summary>
        public Point[] DefiningPoints { get; set; }

        /// <summary>Bank Account this plot will be bought with</summary>
        public string BankAccountID { get; set; }

    }
}
