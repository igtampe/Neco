using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igtampe.Neco.Common {

    /// <summary>Item in the Certification ledger for NECO</summary>
    public class CertifiedItem {

        /// <summary>ID of this certified item</summary>
        public Guid Id { get; set; }

        /// <summary>Human readable text in case the object fails to load</summary>
        public string Text { get; set; } = "";

        /// <summary>User that certified this item</summary>
        public User CertifiedBy { get; set; }

        /// <summary>Date and time at which this was certified</summary>
        public DateTime Date { get; set; } = DateTime.Now;

    }
}
