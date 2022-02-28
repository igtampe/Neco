using Igtampe.Neco.Common;

namespace Igtampe.ViBE {

    /// <summary>Port of the ViBE EzTax income item IRF class (but with some minor modifications) and some EZTAX Code</summary>
    public class IncomeItem : Nameable {

        //--------------------------------[Structures]--------------------------------
        #region Structures

        /// <summary>Details of an appartment</summary>
        public struct ApartmentDetails {

            public int StudioUnits { get; set; }
            public int BR1Units { get; set; }
            public int BR2Units { get; set; }
            public int BR3Units { get; set; }
            public int PHUnits { get; set; }
            public int StudioRent { get; set; }
            public int BR1Rent { get; set; }
            public int BR2Rent { get; set; }
            public int BR3Rent { get; set; }
            public int PHRent { get; set; }

            /// <summary>Total Apartment Income</summary>
            public long Income => (StudioRent * StudioUnits) + (BR1Rent * BR1Units) + (BR2Rent * BR2Units) + (BR3Rent * BR3Units) + (PHRent * PHUnits);

            public ApartmentDetails(int S, int BR1, int BR2, int BR3, int PH, int SRent, int BR1Rent, int BR2Rent, int BR3Rent, int PHRent) {
                StudioUnits = S;
                BR1Units = BR1;
                BR2Units = BR2;
                BR3Units = BR3;
                PHUnits = PH;

                StudioRent = SRent;
                this.BR1Rent = BR1Rent;
                this.BR2Rent = BR2Rent;
                this.BR3Rent = BR3Rent;
                this.PHRent = PHRent;
            }

            public override string ToString() {
                return Income == 0
                    ? $"N/A"
                    : $"{(StudioUnits > 0 ? $"{StudioUnits} at {StudioRent:N0}p/month, " : "")}" +
                    $"{(BR1Units > 0 ? $"{BR1Units} at {BR1Rent:N0}p/month, " : "")}" +
                    $"{(BR2Units > 0 ? $"{BR2Units} at {BR2Rent:N0}p/month, " : "")}" +
                    $"{(BR3Units > 0 ? $"{BR3Units} at {BR3Rent:N0}p/month, " : "")}" +
                    $"{(PHUnits > 0 ?  $"{PHUnits} at  {PHRent:N0}p/month, " : "")}";
            }
        }

        /// <summary>Details of a Hotel</summary>
        public struct HotelDetails {
            public int Rooms { get; set; }
            public int Suites { get; set; }
            public int RoomRate { get; set; }
            public int SuiteRate { get; set; }

            private static long RateToIncome(int Rate) => Rate / 2 * 365 / 12;

            public HotelDetails(int R, int S, int RR, int SR, int MI) {
                Rooms = R;
                Suites = S;
                RoomRate = RR;
                SuiteRate = SR;
                _ = MI; //Discard Misc Income we're ignoring that

            }

            public long Income => (RateToIncome(RoomRate) * Rooms) + (RateToIncome(SuiteRate) * Suites);

            public override string ToString() {
                return Income == 0
                    ? $"N/A"
                    : $"{(Rooms > 0 ? $"{Rooms} at {RoomRate:N0}p/night, " : "")}" +
                    $"{(Suites > 0 ? $"{Suites} at {SuiteRate:N0}p/month, " : "")}";
            }
        }

        /// <summary>Details of a Business</summary>
        public struct BusinessDetails {
            public int Chairs { get; set; }
            public int AvgSpend {get;set;}
            public int HoursOpen {get;set;}
            public int CustomersPerHour {get;set;}

            public BusinessDetails(int C, int A, int CH, int HO) {
                Chairs = C;
                AvgSpend = A;
                CustomersPerHour = CH;
                HoursOpen = HO;
            }

            public long Income => AvgSpend / 2 * Chairs * HoursOpen * CustomersPerHour * 30;

            public override string ToString() {
                return Income == 0
                    ? $"N/A"
                    : $"{Chairs} Chair(s) with {CustomersPerHour} customers/hour with {AvgSpend:n0}p/visit open {HoursOpen} hours per day";
            }
        }

        #endregion

        public string Name { get; set; } = "";

        public string Location { get; set; } = "";

        public ApartmentDetails Apartment { get; set; }

        public HotelDetails Hotel { get; set; }

        public BusinessDetails Business { get; set; }

        public long Income => Apartment.Income + Hotel.Income + Business.Income;

        public static async Task<List<IncomeItem>> IncomeItemsFromIRF(string IRFFIle) {

            List <IncomeItem> RList = new();

            if (!File.Exists(IRFFIle)) { return RList; }
            
            string[] Lines = await File.ReadAllLinesAsync(IRFFIle);

            foreach (string Line in Lines) {

                //Split the line
                string[] currentline = Line.Split(",");

                if (currentline.Length == 2) {
                    //Legacy item
                    //Legacy items are ignored because its all misc income
                } else {
                    //New Item
                    string Name = currentline[0];

                    ApartmentDetails ApartmentDetails = new (int.Parse(currentline[2]), int.Parse(currentline[3]), int.Parse(currentline[4]), 
                        int.Parse(currentline[5]), int.Parse(currentline[6]), int.Parse(currentline[7]), int.Parse(currentline[8]), int.Parse(currentline[9]), 
                        int.Parse(currentline[10]), int.Parse(currentline[11]));

                    HotelDetails HotelDetails = new (int.Parse(currentline[12]), int.Parse(currentline[13]), int.Parse(currentline[14]), int.Parse(currentline[15]), int.Parse(currentline[16]));
                    BusinessDetails BusinessDetails = new (int.Parse(currentline[17]), int.Parse(currentline[18]), int.Parse(currentline[19]), int.Parse(currentline[20]));

                    //Dim CurrentMiscIncome As Long = currentline(21) //Ignore misc income
                    string Location = currentline[22];

                    RList.Add(new() {
                        Name = Name,
                        Apartment = ApartmentDetails,
                        Hotel = HotelDetails,
                        Business = BusinessDetails,
                        Location = Location

                    });

                }
            }

            return RList;
            
        }
    }
}
