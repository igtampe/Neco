using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Igtampe.Neco.Common.Requests;

namespace Igtampe.Neco.Common.LandView.Requests {
    /// <summary>Request to create a plot on the Landview system</summary>
    public class CreatePlotRequest:INecoRequest {

        /// <summary>ID of the session executing this request</summary>
        public Guid SessionID { get; set; }

        /// <summary>Points that define the area of the plot</summary>
        public List<Point> DefiningPoints { get; set; }

        /// <summary>Bank Account this plot will be bought with</summary>
        public Guid BankAccountID { get; set; }

    }
}
