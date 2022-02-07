using Igtampe.Neco.Common;
using Igtampe.Neco.Common.Contractus;
using Igtampe.Neco.Common.EzTax;
using Igtampe.Neco.Common.EzTax.Subitems;
using Igtampe.Neco.Common.LandView;
using Igtampe.Neco.Common.UMSAT;

namespace Igtampe.Neco.Test.Common {

    /// <summary>Holds utils to check atomic properties (except IDs and datetimes and data ignored by the database) of Neco objects are equal</summary>
    public static class EqualUtils {

        public static bool UserTypeEquals(UserType O1, UserType O2) { 
            return O1.Name == O2.Name &&
                O1.Taxation == O2.Taxation;
        }

        public static bool UserEquals(User O1, User O2) {
            return O1.Name == O2.Name;
        }

        public static bool TransactionEquals(Transaction O1, Transaction O2) {
            return O1.Amount == O2.Amount &&
                O1.Name == O2.Name &&
                O1.State == O2.State;
        }

        public static bool NotificationEquals(Notification O1, Notification O2) {
            return O1.Read == O2.Read &&
                O1.Text == O2.Text;
        }

        public static bool CheckboookItemEquals(CheckbookItem O1, CheckbookItem O2) {
            return O1.Comment == O2.Comment &&
                O1.Type == O2.Type &&
                O1.Variant == O2.Variant;
        }

        public static bool CertifiedItemEquals(CertifiedItem O1, CertifiedItem O2) {
            return O1.Text == O2.Text;
        }

        public static bool BankAccountTypeEquals(BankAccountType O1, BankAccountType O2) {
            return O1.InterestRate == O2.InterestRate &&
                O1.Name == O2.Name;
        }

        public static bool BankAccountDetailEquals(BankAccountDetail O1, BankAccountDetail O2) {
            return O1.Balance == O2.Balance;
        }

        public static bool BankAccountEquals(BankAccount O1, BankAccount O2) {
            return O1.Closed == O2.Closed;
        }

        public static bool BankEquals(Bank O1, Bank O2) {
            return O1.Name == O2.Name;
        }

        public static bool AssetEquals(Asset O1, Asset O2) {
            return O1.Name == O2.Name &&
                O1.Complete == O2.Complete &&
                O1.Description == O2.Description &&
                O1.SpecificLocaiton == O2.SpecificLocaiton;
        }

        public static bool RoadEquals(Road O1, Road O2) {
            return O1.Name == O2.Name &&
                O1.Thickness == O2.Thickness &&
                O1.Points == O2.Points;
        }

        public static bool PlotEquals(Plot O1, Plot O2) {
            return O1.Name == O2.Name &&
                O1.Points == O2.Points &&
                O1.Status == O2.Status;
        }

        public static bool DistrictEquals(District O1, District O2) {
            return O1.Name == O2.Name &&
                O1.Points == O2.Points &&
                O1.PricePerSquareMeter == O2.PricePerSquareMeter;
        }
        
        public static bool CountryEquals(Country O1, Country O2) {
            return O1.Name == O2.Name &&
                O1.Width == O2.Width &&
                O1.Height == O2.Height &&
                O1.PricePerSquareMeter == O2.PricePerSquareMeter;
        }
        public static bool TaxReportEquals(TaxReport O1, TaxReport O2) {
            return O1.CSVReport == O2.CSVReport &&
                O1.ExtraIncome == O2.ExtraIncome &&
                O1.ExtraIncomeTaxable == O2.ExtraIncomeTaxable &&
                O1.GrandTotalIncome == O2.GrandTotalIncome &&
                O1.GrandTotalTax == O2.GrandTotalTax &&
                O1.Report == O2.Report &&
                O1.StaticIncome == O2.StaticIncome;
        }

        public static bool TaxJurisdictionEquals(TaxJurisdiction O1, TaxJurisdiction O2) {
            return O1.Name == O2.Name;
        }

        public static bool TaxBracketEquals(TaxBracket O1, TaxBracket O2) {
            return O1.Name == O2.Name &&
                O1.Rate == O2.Rate &&
                O1.Start == O2.Start &&
                O1.End == O2.End;
        }

        public static bool IncomeItemEquals(IncomeItem O1, IncomeItem O2) {
            return O1.Name == O2.Name &&
                O1.MiscIncome == O2.MiscIncome;
        }

        public static bool ApartmentEquals(Apartment O1, Apartment O2) {
            return O1.Name == O2.Name &&
                O1.B1Rent == O2.B1Rent &&
                O1.B2Rent == O2.B2Rent &&
                O1.B3Rent == O2.B3Rent &&
                O1.PRent == O2.PRent &&
                O1.B1Units == O2.B1Units &&
                O1.B2Units == O2.B2Units &&
                O1.B3Units == O2.B3Units &&
                O1.PUnits == O2.PUnits &&
                O1.SRent == O2.SRent &&
                O1.SUnits == O2.SUnits;
        }

        public static bool BusinessEquals(Business O1, Business O2) {
            return O1.Name == O2.Name &&
                O1.AvgSpend == O2.AvgSpend &&
                O1.Chairs == O2.Chairs &&
                O1.CustPerHour == O2.CustPerHour &&
                O1.HoursOpen == O2.HoursOpen;
        }
        
        public static bool HotelEquals(Hotel O1, Hotel O2) {
            return O1.Name == O2.Name &&
                O1.MiscIncome == O2.MiscIncome &&
                O1.RoomRate == O2.RoomRate &&
                O1.Rooms == O2.Rooms &&
                O1.SuiteRate == O2.SuiteRate &&
                O1.Suites == O2.Suites;
        }

        public static bool ContractEquals(Contract O1, Contract O2) {
            return O1.Name == O2.Name &&
                O1.Description == O2.Description &&
                O1.Status == O2.Status &&
                O1.Amount == O2.Amount;
        }

    }
}
