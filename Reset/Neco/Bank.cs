using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Igtampe.Neco.Common {
    
    /// <summary> A Neco Bank</summary>
    public class Bank {

        /// <summary>ID of this bank (Short name)</summary>
        [MaxLength(5)]
        [MinLength(5)]
        public string ID { get; set; }

        /// <summary>Long Name of this bank</summary>
        public string Name { get; set; } = "";

        /// <summary>Accounts in this bank</summary>
        [JsonIgnore]
        public List<BankAccount> Accounts { get; set; }

        /// <summary>Bank Account types available in this bank</summary>
        public List<BankAccountType> AccountTypes { get; set; }

        /// <summary>Compares this Bank to another object</summary>
        /// <param name="obj"></param>
        /// <returns>True if and only if the object is a Bank and the <see cref="ID"/> matches with this one's</returns>
        public override bool Equals(object obj) {
            if (obj is Bank C) { return C.ID == ID; }
            return false;
        }

        /// <summary>Gets a hash code for this Bank. Delegates to <see cref="ID"/></summary>
        /// <returns></returns>
        public override int GetHashCode() { return ID.GetHashCode(); }

        /// <summary>Creates a string representation of this Bank</summary>
        /// <returns>{Id} : {Name}</returns>
        public override string ToString() { return $"{ID} : {Name}"; }
    }
}
