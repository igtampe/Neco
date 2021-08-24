using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igtampe.Neco.Common {

    /// <summary>Type of user in NECO</summary>
    public class UserType {

        /// <summary>ID of this User Type</summary>
        public Guid Id { get; set; }

        /// <summary>Name of this User Type (IE Standard, Corporate, Government)</summary>
        public string Name { get; set; } = "";
    }
}
