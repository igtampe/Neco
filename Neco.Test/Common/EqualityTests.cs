using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Igtampe.Neco.Common;
using Igtampe.Neco.Common.Contractus;
using Igtampe.Neco.Common.EzTax;
using Igtampe.Neco.Common.EzTax.Subitems;
using Igtampe.Neco.Common.LandView;
using Igtampe.Neco.Common.UMSAT;

namespace Igtampe.Neco.Test.Common {

    /// <summary>
    /// Tests the equality operations on all objects (most of which just check IDs
    /// </summary>
    public class EqualityTests {

        private Guid ID1;
        private Guid ID2;

        [SetUp]
        public void Setup() {
            //Setup two IDs that will be used for testing later

            ID1 = Guid.NewGuid();
            do { ID2 = Guid.NewGuid(); } while (ID1 == ID2); //Make absolutely 100% sure that we ahve two different guids
        
        } 

        [Test]
        public void UserTypeEquals() {
            UserType T1 = new() { ID = ID1 };
            UserType T2 = new() { ID = ID1 };
            UserType T3 = new() { ID = ID2 };

            Assert.AreEqual(T1, T1);
            Assert.AreEqual(T1, T2);
            Assert.AreNotEqual(T1, T3);
        }

        [Test]
        public void UserAuthEquals() {
            UserAuth T1 = new() { ID = "57174", Pin="0000" };
            UserAuth T2 = new() { ID = "57174", Pin = "0000" };
            UserAuth T3 = new() { ID = "57173", Pin = "0000" };
            UserAuth T4 = new() { ID = "57174", Pin = "0001" };
            UserAuth T5 = new() { ID = "57173", Pin = "0001" };

            Assert.AreEqual(T1, T1);
            Assert.AreEqual(T1, T2);
            Assert.AreNotEqual(T1, T3);
            Assert.AreNotEqual(T1, T4);
            Assert.AreNotEqual(T1, T5);
        }

        [Test]
        public void UserEquals() {
            User T1 = new() { ID = "57174" };
            User T2 = new() { ID = "57174" };
            User T3 = new() { ID = "57175" };

            Assert.AreEqual(T1, T1);
            Assert.AreEqual(T1, T2);
            Assert.AreNotEqual(T1, T3);
        }

        [Test]
        public void TransactionEquals() {
            Transaction T1 = new() { ID = ID1 };
            Transaction T2 = new() { ID = ID1 };
            Transaction T3 = new() { ID = ID2 };

            Assert.AreEqual(T1, T1);
            Assert.AreEqual(T1, T2);
            Assert.AreNotEqual(T1, T3);
        }

        [Test]
        public void NotificationEquals() {
            Notification T1 = new() { ID = ID1 };
            Notification T2 = new() { ID = ID1 };
            Notification T3 = new() { ID = ID2 };

            Assert.AreEqual(T1, T1);
            Assert.AreEqual(T1, T2);
            Assert.AreNotEqual(T1, T3);
        }

        [Test]
        public void CheckboookItemEquals() {
            CheckbookItem T1 = new() { ID = ID1 };
            CheckbookItem T2 = new() { ID = ID1 };
            CheckbookItem T3 = new() { ID = ID2 };

            Assert.AreEqual(T1, T1);
            Assert.AreEqual(T1, T2);
            Assert.AreNotEqual(T1, T3);
        }

        [Test]
        public void CertifiedItemEquals() {
            CertifiedItem T1 = new() { ID = ID1 };
            CertifiedItem T2 = new() { ID = ID1 };
            CertifiedItem T3 = new() { ID = ID2 };

            Assert.AreEqual(T1, T1);
            Assert.AreEqual(T1, T2);
            Assert.AreNotEqual(T1, T3);
        }

        [Test]
        public void BankAccountTypeEquals() {
            BankAccountType T1 = new() { ID = ID1 };
            BankAccountType T2 = new() { ID = ID1 };
            BankAccountType T3 = new() { ID = ID2 };

            Assert.AreEqual(T1, T1);
            Assert.AreEqual(T1, T2);
            Assert.AreNotEqual(T1, T3);
        }

        [Test]
        public void BankAccountDetailEquals() {
            BankAccountDetail T1 = new() { ID = ID1 };
            BankAccountDetail T2 = new() { ID = ID1 };
            BankAccountDetail T3 = new() { ID = ID2 };

            Assert.AreEqual(T1, T1);
            Assert.AreEqual(T1, T2);
            Assert.AreNotEqual(T1, T3);
        }

        [Test]
        public void BankAccountEquals() {
            BankAccount T1 = new() { ID = "123456789" };
            BankAccount T2 = new() { ID = "123456789" };
            BankAccount T3 = new() { ID = "123456780" };

            Assert.AreEqual(T1, T1);
            Assert.AreEqual(T1, T2);
            Assert.AreNotEqual(T1, T3);
        }

        [Test]
        public void BankEquals() {
            Bank T1 = new() { ID = "UMSNB" };
            Bank T2 = new() { ID = "UMSNB" };
            Bank T3 = new() { ID = "GBANK" };

            Assert.AreEqual(T1, T1);
            Assert.AreEqual(T1, T2);
            Assert.AreNotEqual(T1, T3);
        }

        [Test]
        public void AssetEquals() {
            Asset T1 = new() { ID = ID1 };
            Asset T2 = new() { ID = ID1 };
            Asset T3 = new() { ID = ID2 };

            Assert.AreEqual(T1, T1);
            Assert.AreEqual(T1, T2);
            Assert.AreNotEqual(T1, T3);
        }

        [Test]
        public void RoadEquals() {
            Road T1 = new() { ID = ID1 };
            Road T2 = new() { ID = ID1 };
            Road T3 = new() { ID = ID2 };

            Assert.AreEqual(T1, T1);
            Assert.AreEqual(T1, T2);
            Assert.AreNotEqual(T1, T3);
        }

        [Test]
        public void PlotEquals() {
            Plot T1 = new() { ID = ID1 };
            Plot T2 = new() { ID = ID1 };
            Plot T3 = new() { ID = ID2 };

            Assert.AreEqual(T1, T1);
            Assert.AreEqual(T1, T2);
            Assert.AreNotEqual(T1, T3);
        }

        [Test]
        public void DistrictEquals() {
            District T1 = new() { ID = ID1 };
            District T2 = new() { ID = ID1 };
            District T3 = new() { ID = ID2 };

            Assert.AreEqual(T1, T1);
            Assert.AreEqual(T1, T2);
            Assert.AreNotEqual(T1, T3);
        }

        [Test]
        public void CountryEquals() {
            Country T1 = new() { ID = ID1 };
            Country T2 = new() { ID = ID1 };
            Country T3 = new() { ID = ID2 };

            Assert.AreEqual(T1, T1);
            Assert.AreEqual(T1, T2);
            Assert.AreNotEqual(T1, T3);
        }

        [Test]
        public void TaxReportEquals() {
            TaxReport T1 = new() { ID = ID1 };
            TaxReport T2 = new() { ID = ID1 };
            TaxReport T3 = new() { ID = ID2 };

            Assert.AreEqual(T1, T1);
            Assert.AreEqual(T1, T2);
            Assert.AreNotEqual(T1, T3);
        }

        [Test]
        public void TaxJurisdictionEquals() {
            TaxJurisdiction T1 = new() { ID = ID1 };
            TaxJurisdiction T2 = new() { ID = ID1 };
            TaxJurisdiction T3 = new() { ID = ID2 };

            Assert.AreEqual(T1, T1);
            Assert.AreEqual(T1, T2);
            Assert.AreNotEqual(T1, T3);
        }

        [Test]
        public void TaxBracketEquals() {
            TaxBracket T1 = new() { ID = ID1 };
            TaxBracket T2 = new() { ID = ID1 };
            TaxBracket T3 = new() { ID = ID2 };

            Assert.AreEqual(T1, T1);
            Assert.AreEqual(T1, T2);
            Assert.AreNotEqual(T1, T3);
        }

        [Test]
        public void IncomeItemEquals() {
            IncomeItem T1 = new() { ID = ID1 };
            IncomeItem T2 = new() { ID = ID1 };
            IncomeItem T3 = new() { ID = ID2 };

            Assert.AreEqual(T1, T1);
            Assert.AreEqual(T1, T2);
            Assert.AreNotEqual(T1, T3);
        }

        [Test]
        public void ApartmentEquals() {
            Apartment T1 = new() { ID = ID1 };
            Apartment T2 = new() { ID = ID1 };
            Apartment T3 = new() { ID = ID2 };

            Assert.AreEqual(T1, T1);
            Assert.AreEqual(T1, T2);
            Assert.AreNotEqual(T1, T3);
        }

        [Test]
        public void BusinessEquals() {
            Business T1 = new() { ID = ID1 };
            Business T2 = new() { ID = ID1 };
            Business T3 = new() { ID = ID2 };

            Assert.AreEqual(T1, T1);
            Assert.AreEqual(T1, T2);
            Assert.AreNotEqual(T1, T3);
        }

        [Test]
        public void HotelEquals() {
            Hotel T1 = new() { ID = ID1 };
            Hotel T2 = new() { ID = ID1 };
            Hotel T3 = new() { ID = ID2 };

            Assert.AreEqual(T1, T1);
            Assert.AreEqual(T1, T2);
            Assert.AreNotEqual(T1, T3);
        }

        [Test]
        public void ContractEquals() {
            Contract T1 = new() { ID = ID1 };
            Contract T2 = new() { ID = ID1 };
            Contract T3 = new() { ID = ID2 };

            Assert.AreEqual(T1, T1);
            Assert.AreEqual(T1, T2);
            Assert.AreNotEqual(T1, T3);
        }

    }
}
