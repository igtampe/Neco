using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igtampe.Neco.Common.Requests {
    /// <summary>Holds a request to commit an action to a Neco Bank Account (IE Close, Logs)</summary>
    public class BankAccountActionRequest {

        /// <summary>ID of the session executing this request</summary>
        public Guid SessionID { get; set; }

        /// <summary>ID of the bank account to close</summary>
        public string BankAccountID { get; set; }

    }
}
