using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igtampe.Neco.Common {

    /// <summary>User used for Authentication</summary>
    public class UserAuth {

        /// <summary>UMSWEB ID of this user</summary>
        [MaxLength(5)]
        [MinLength(5)]
        public string Id { get; set; }

        /// <summary>Pin of this user</summary>
        [MaxLength(4)]
        [MinLength(4)]
        public string Pin { internal get; set; }

        /// <summary>Checks the provided pin against this user's pin</summary>
        /// <param name="Pin"></param>
        /// <returns>Returns true if the provided pin matches this user's pin</returns>
        public bool CheckPin(string Pin) { return Pin == this.Pin; }

    }
}
