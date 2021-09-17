using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Igtampe.Neco.Common.Requests;

namespace Igtampe.Neco.Common.Contractus.Requests {
    /// <summary>Request to create and post a new contract</summary>
    public class NewContractRequest:INecoRequest {

        /// <summary>ID of the session executing this request</summary>
        public Guid SessionID { get; set; }

        /// <summary>Name of the contract to be created</summary>
        public string Name { get; set; }

        /// <summary>Description of the contract to be created</summary>
        public string Description { get; set; }
    }
}
