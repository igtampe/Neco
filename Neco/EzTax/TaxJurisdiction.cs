using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igtampe.Neco.Common.EzTax {
    
    /// <summary>Holds a Jurisdiction that can tax a Neco user</summary>
    public class TaxJurisdiction {

        /// <summary>ID of this jurisdiction</summary>
        public Guid ID { get; set; }

        /// <summary>Name of this jurisdiction</summary>
        public string Name { get; set; } = "";

        /// <summary>Brackets in this Jurisdiction</summary>
        public ICollection<TaxBracket> Brackets { get; set; }

        /// <summary>Account to which taxes must be paid out to</summary>
        public BankAccount Account { get; set; }

        /// <summary>Calculates tax on User U with income I in this jurisdiction</summary>
        /// <param name="U"></param>
        /// <param name="I"></param>
        /// <returns></returns>
        public long CalculateTax(User U, long I) {
            TaxBracket T = Brackets.FirstOrDefault(P => P.Type.Equals(U.Type) && P.Start >= I && I < P.End); //LOOK AT THIS BEAUTY HOW HAD I NOT USED LINQ BEFORE
            if(T == null) return 0;
            return Convert.ToInt64(I * T.Rate);
        }

    }
}
