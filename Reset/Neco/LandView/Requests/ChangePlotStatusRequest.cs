using System;
using Igtampe.Neco.Common.Requests;

namespace Igtampe.Neco.Common.LandView.Requests {
    
    /// <summary>Request to change a plot's current status</summary>
    public class ChangePlotStatusRequest:INecoRequest {

        /// <summary>ID of the session executing this request</summary>
        public Guid SessionID { get; set; }

        /// <summary>ID of the plot to change status of</summary>
        public Guid PlotID { get; set; }

        /// <summary>Status to change the plot's status to</summary>
        public PlotStatus NewStatus { get; set; }

    }
}
