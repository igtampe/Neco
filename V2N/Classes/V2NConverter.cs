using Igtampe.Neco.Common.Banking;
using Igtampe.Neco.Common.Taxes;
using Igtampe.Neco.Common.Income;

namespace Igtampe.Neco.V2N.Classes {
    public class V2NConverter {

        public enum MergeAllAccountsInto {

            /// <summary>Merge into whichever account has the highest balance</summary>
            HIGHEST_BALANCE = -1,

            /// <summary>Merge into the UMSNB (or equivalent) bank account</summary>
            UMSNB = 0,

            /// <summary>Merge into the GBANK (or equivalent) bank account</summary>
            GBANK = 1,

            /// <summary>Merge into the RIVER (or equivalent) bank account</summary>
            RIVER = 2
        }

        private readonly Dictionary<string, Jurisdiction> JurisdictionDictionary;

        private readonly Bank UMSNB;
        private readonly Bank GBANK;
        private readonly Bank RIVER;

        public V2NConverter(Bank UMSNB, Bank GBANK, Bank RIVER, Jurisdiction Newpond, Jurisdiction Paradisus, Jurisdiction Urbia, Jurisdiction Laertes, Jurisdiction SouthOsten, Jurisdiction NorthOsten) {

            this.UMSNB = UMSNB;
            this.GBANK = GBANK;
            this.RIVER = RIVER;

            JurisdictionDictionary = new();
            JurisdictionDictionary.Add(PossibleDistricts[0], Newpond);
            JurisdictionDictionary.Add(PossibleDistricts[1], Paradisus);
            JurisdictionDictionary.Add(PossibleDistricts[2], Urbia);
            JurisdictionDictionary.Add(PossibleDistricts[3], Laertes);
            JurisdictionDictionary.Add(PossibleDistricts[4], SouthOsten);
            JurisdictionDictionary.Add(PossibleDistricts[5], NorthOsten);

        }

        /// <summary>Array of all possible districts in </summary>
        public static readonly string[] PossibleDistricts = { "Newpond", "Paradisus", "Urbia", "Laertes", "South Osten", "North Osten" };

        public Common.User ViBEUserToNecoUser(ConvertibleViBEUser VUser) => VUser.Location is null
            ? throw new ArgumentException("Location")
            : ViBEUserToNecoUser(VUser, VUser.Location, VUser.MergeAllAccounts, VUser.MergeInto);

        /// <summary>Converter for ViBE users to Neco Users</summary>
        /// <param name="VUser">ViBE User to convert</param>
        /// <param name="MergeAllAccounts">Merge all accounts into a single account</param>
        /// <param name="MergeInto">Merge into what account</param>
        /// <returns></returns>
        public Common.User ViBEUserToNecoUser(ViBE.User VUser, Jurisdiction Location, bool MergeAllAccounts = true, MergeAllAccountsInto MergeInto = MergeAllAccountsInto.HIGHEST_BALANCE) {

            //Create the basic 
            Common.User NUser = new() { ID = VUser.ID, Name = VUser.Name, IsGov = VUser.IsGov, Password=VUser.Pin };
            if (!VUser.HasAccounts) { return NUser; } //if the user has no accounts assume they also have no income and just return

            if (MergeAllAccounts) {

                //This is a single line. God help future me potentially needing to understand this
                NUser.Accounts.Add(ViBEAccountToNecoAccount(VUser, VUser.TotalWealth,
                    (MergeInto != MergeAllAccountsInto.HIGHEST_BALANCE ? MergeInto : DetermineHighestBalance(VUser)) switch {
                        MergeAllAccountsInto.UMSNB => UMSNB,
                        MergeAllAccountsInto.GBANK => GBANK,
                        MergeAllAccountsInto.RIVER => RIVER,
                        _ => throw new InvalidOperationException("This isn't supposed to be POSSIBLE"),
                    }
                    , Location));

            } else {

                //Add all three accounts
                if (VUser.UMSNB is not null) { NUser.Accounts.Add(ViBEAccountToNecoAccount(VUser, VUser.UMSNB ?? 0, UMSNB, Location)); }
                if (VUser.GBANK is not null) { NUser.Accounts.Add(ViBEAccountToNecoAccount(VUser, VUser.GBANK ?? 0, GBANK, Location)); }
                if (VUser.RIVER is not null) { NUser.Accounts.Add(ViBEAccountToNecoAccount(VUser, VUser.RIVER ?? 0, RIVER, Location)); }
            }

            //Now comes income management
            //Assuming we have accounts by now, Accounts[0] Has to exist
            foreach(ViBE.IncomeItem Item in VUser.IncomeItems) {
                NUser.Accounts[0].IncomeItems.AddRange(ViBEIncomeItemToNecoIncomeItems(Item));
            }

            //Finally we should be good to go actually.
            return NUser;
        }

        public static Account ViBEAccountToNecoAccount(ViBE.User VUser, long Balance, Bank Bank, Jurisdiction Jurisdiction) {
            Account A = new() {
                Address = "",
                Balance = Balance,
                Bank = Bank,
                Closed = false,

                IncomeType = VUser.IsGov
                ? IncomeType.GOVERNMENT
                : VUser.IsCorp
                    ? IncomeType.CORPORATE
                    : IncomeType.PERSONAL,

                Jurisdiction = Jurisdiction,
                Name = $"{VUser.ID}\\{Bank.ID}",
                PubliclyListed = false
            };

            A.ID = A.IDGenerator.Generate();
            return A;
        }

        public List<Common.Income.IncomeItem> ViBEIncomeItemToNecoIncomeItems(ViBE.IncomeItem IRFItem) {

            List<Common.Income.IncomeItem> Items = new();

            //If there is no income that is not misc, just return.
            if (IRFItem.Income == 0) { return Items; }

            if (IRFItem.Hotel.Income != 0) { Items.Add(ViBEHoteltoNecoHotel(IRFItem)); }
            if (IRFItem.Apartment.Income != 0) { Items.Add(ViBEApartmenttoNecoApartment(IRFItem)); }
            if (IRFItem.Business.Income != 0) { Items.Add(ViBEBusinessToNecoBusines(IRFItem)); }

            return Items;

        }

        public Hotel ViBEHoteltoNecoHotel(ViBE.IncomeItem IRFItem) => new() {
            Address = "", Approved = true, DateCreated = DateTime.UtcNow, DateUpdated = DateTime.UtcNow,
            Description = "Auto-generated Hotel from the ViBE to Neco Converter based on an uploaded IRF",
            Name = IRFItem.Name, Jurisdiction = JurisdictionDictionary[IRFItem.Location],
            Rooms = IRFItem.Hotel.Rooms, RoomRate = IRFItem.Hotel.RoomRate,
            Suites= IRFItem.Hotel.Suites, SuiteRate = IRFItem.Hotel.SuiteRate,
        };

        public Apartment ViBEApartmenttoNecoApartment(ViBE.IncomeItem IRFItem) => new() {
            Address = "", Approved = true, DateCreated = DateTime.UtcNow, DateUpdated = DateTime.UtcNow,
            Description = "Auto-generated Apartment from the ViBE to Neco Converter based on an uploaded IRF",
            Name = IRFItem.Name, Jurisdiction = JurisdictionDictionary[IRFItem.Location],
            B1Rent=IRFItem.Apartment.BR1Rent, B1Units=IRFItem.Apartment.BR1Units,
            B2Rent=IRFItem.Apartment.BR2Rent, B2Units=IRFItem.Apartment.BR2Units,
            B3Rent=IRFItem.Apartment.BR3Rent, B3Units=IRFItem.Apartment.BR3Units,
            PRent=IRFItem.Apartment.PHRent, PUnits=IRFItem.Apartment.PHUnits,
            SRent=IRFItem.Apartment.StudioRent, SUnits=IRFItem.Apartment.StudioUnits
        };

        public Business ViBEBusinessToNecoBusines(ViBE.IncomeItem IRFItem) => new() {
            Address = "", Approved = true, DateCreated = DateTime.UtcNow, DateUpdated = DateTime.UtcNow,
            Description = "Auto-generated Apartment from the ViBE to Neco Converter based on an uploaded IRF",
            Name = IRFItem.Name, Jurisdiction = JurisdictionDictionary[IRFItem.Location],
            AvgSpend=IRFItem.Business.AvgSpend, CustPerHour=IRFItem.Business.CustomersPerHour,
            HoursOpen=IRFItem.Business.HoursOpen, PointsOfSale=IRFItem.Business.Chairs            
        };

        /// <summary>Returns the mergallaccountsinfo that is needed by determining which account has the highest balance</summary>
        /// <param name="VUser"></param>
        /// <returns></returns>
        public static MergeAllAccountsInto DetermineHighestBalance(ViBE.User VUser) {

            //MAKE A DICTIONARY OF THIS STUFF
            Dictionary<MergeAllAccountsInto, long?> TheDict = new();
            TheDict.Add(MergeAllAccountsInto.UMSNB, VUser.UMSNB);
            TheDict.Add(MergeAllAccountsInto.GBANK, VUser.GBANK);
            TheDict.Add(MergeAllAccountsInto.RIVER, VUser.RIVER);

            //This has got to be the silliest function I've ever written but it should do exactly what I need it to do :shrug:
            return TheDict
                .Where(A => A.Value != null) //only where the account exists
                .OrderByDescending(A => A.Value) //Order by descending balance
                .Select(A => A.Key) //Select the type of MergeAll that we need
                .FirstOrDefault(); //and get the first

        }
    }
}
