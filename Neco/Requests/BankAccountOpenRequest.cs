using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igtampe.Neco.Common.Requests {
    /// <summary>Holds a request to Open a Neco bank account</summary>
    public class BankAccountOpenRequest {

        /// <summary>ID of the session executing this request</summary>
        public Guid SessionID { get; set; }

        /// <summary>ID of the bank account type to open</summary>
        public Guid BankAccountTypeID { get; set; }

    }
}
