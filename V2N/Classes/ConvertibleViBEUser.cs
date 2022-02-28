using Igtampe.ViBE;
using Igtampe.Neco.Common.Taxes;

namespace Igtampe.Neco.V2N.Classes {
    public class ConvertibleViBEUser : User {

        public Jurisdiction? Location { get; set; }

        public bool MergeAllAccounts { get; set; } = true;

        public V2NConverter.MergeAllAccountsInto MergeInto { get; set; } = V2NConverter.MergeAllAccountsInto.HIGHEST_BALANCE;

        /// <summary>Whether or not to convert this user</summary>
        public bool Convert { get; set; } = false;

        public bool CanConvert() => Location is not null;

        public static ConvertibleViBEUser FromViBEUser(User U) => new() {
            ID = U.ID, Name = U.Name, Pin = U.Pin,
            UMSNB = U.UMSNB, GBANK = U.GBANK, RIVER = U.RIVER,
            IncomeItems = U.IncomeItems,
        };

    }
}
