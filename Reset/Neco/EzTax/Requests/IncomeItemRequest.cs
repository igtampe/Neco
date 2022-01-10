using System;
using Igtampe.Neco.Common.Requests;

namespace Igtampe.Neco.Common.EzTax.Requests {
    /// <summary>Request to create or modify an incomeitem</summary>
    public class IncomeItemRequest: INecoRequest {

        /// <summary>ID of the session executing this request</summary>
        public Guid SessionID { get; set; }

        /// <summary>Item to modify or create</summary>
        public IncomeItem Item { get; set; }

    }
}
