using System;
using Igtampe.Neco.Common.Requests;

namespace Igtampe.Neco.Common.LandView.Requests {

    /// <summary>Request to transfer plot ownership</summary>
    public class TransferPlotOwnershipRequest:INecoRequest {

        /// <summary>ID of the Session executing this request</summary>
        public Guid SessionID { get; set; }

        /// <summary>ID of the plot to transfer ownership</summary>
        public Guid PlotID { get; set; }

        /// <summary>User ID which will be the new owner of this plot after the transfer</summary>
        public string NewOwnerID { get; set; }

    }
}
