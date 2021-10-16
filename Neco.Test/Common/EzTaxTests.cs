using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Igtampe.Neco.Common;
using Igtampe.Neco.Common.EzTax;
using Igtampe.Neco.Common.EzTax.Subitems;

namespace Igtampe.Neco.Test.Common {
    public class EzTaxTests {

        //We're going to setup a *whole* simulation

        //Users
        User Person;
        User Corporation;
        User TheState;
        User TheChurch;

        //Bank accounts
        BankAccount PersonAccount;
        BankAccount CorporationAccount;
        BankAccount TheStateAccount;
        BankAccount TheChurchAccount;

        //Bank stuff
        Bank TheBank;
        BankAccountType TheBanksType;

        //User Types
        UserType Corporate;
        UserType Personal;
        UserType Government;
        UserType Charity;
        
        //Simulation Items and Transactions
        List<IncomeItem> Items;
        List<Transaction> PersonTransactions;
        List<Transaction> CorporateTransactions;

        //Jurisdictions
        TaxJurisdiction FederalJurisdiction;
        TaxJurisdiction LocalJurisdiction;

        [SetUp]
        public void Setup() {

            //Setup User Types

            Corporate = new() {
                ID = Guid.NewGuid(),
                Name = "Corporate",
                Taxation = TaxationType.Taxable
            };

            Personal = new() {
                ID = Guid.NewGuid(),
                Name = "Personal",
                Taxation = TaxationType.Taxable
            };
            
            Government = new() {
                ID = Guid.NewGuid(),
                Name = "Government",
                Taxation = TaxationType.NontaxableOrigin
            };

            Charity = new() {
                ID = Guid.NewGuid(),
                Name = "Charity",
                Taxation = TaxationType.NonTaxableDestination
            };

            // Setup Jurisdictions 

            FederalJurisdiction = new() {
                ID = Guid.NewGuid(),
                Name = "Test",
                Brackets = new List<TaxBracket>{
                    new(){
                        ID = Guid.NewGuid(),
                        Name="Low Personal",
                        Jurisdiction=FederalJurisdiction,
                        Rate=0.05,
                        Start=1000,
                        End=10000,
                        Type = Personal
                    },
                    new(){
                        ID = Guid.NewGuid(),
                        Name="High Personal",
                        Jurisdiction=FederalJurisdiction,
                        Rate=0.15,
                        Start=10000,
                        End=1000000,
                        Type = Personal
                    },
                    new(){
                        ID = Guid.NewGuid(),
                        Name="Low Corporate",
                        Jurisdiction=FederalJurisdiction,
                        Rate=0.02,
                        Start=1000,
                        End=10000,
                        Type = Corporate
                    },
                    new(){
                        ID = Guid.NewGuid(),
                        Name="High Corporate",
                        Jurisdiction=FederalJurisdiction,
                        Rate=0.07,
                        Start=10000,
                        End=1000000,
                        Type = Corporate
                    },
                }
            };

            LocalJurisdiction = new() {
                ID = Guid.NewGuid(),
                Name = "Local Test",
                Brackets = new List<TaxBracket>{
                    new(){
                        ID = Guid.NewGuid(),
                        Name="Corporate Flat Tax",
                        Jurisdiction=LocalJurisdiction,
                        Rate=0.05,
                        Start=0,
                        End=int.MaxValue,
                        Type = Corporate
                    }
                }
            };

            //Setup User Accounts

            Person = new() {
                ID = "57174",
                Name = "Chopo",
                Type = Personal,
                Accounts = new List<BankAccount>()
            };

            Corporation = new() {
                ID = "11321",
                Name = "Velvet Corp.",
                Type = Corporate,
                Accounts = new List<BankAccount>()
            };

            TheState = new() {
                ID = "33118",
                Name = "The UMS Government",
                Type = Government,
                Accounts = new List<BankAccount>()
            };

            TheChurch = new() {
                ID = "77777",
                Name = "The Church of the Buttered Apple God",
                Type = Charity,
                Accounts = new List<BankAccount>()
            };

            //Setup Bank Account

            TheBank = new() {
                ID = "UMSNB",
                Name = "The UMS National Bank",
                Accounts = new List<BankAccount>(),
                AccountTypes = new List<BankAccountType>()
            };

            //Setup Type:

            TheBanksType = new() {
                ID = Guid.NewGuid(),
                Bank = TheBank,
                InterestRate = 0,
                Name = "This account type doesn't exist but I went far too deep into this simulation. Oops"
            };

            TheBank.AccountTypes.Add(TheBanksType);

            //Setup User Bank Accounts

            PersonAccount = new() {
                ID = "123456789",
                Closed = false,
                Details = new() { Balance = 999999999 },
                Owner=Person,
                Bank = TheBank,
                Type = TheBanksType
            };

            CorporationAccount = new() {
                ID = "123456789",
                Closed = false,
                Details = new() { Balance = 999999999 },
                Owner = Corporation,
                Bank = TheBank,
                Type = TheBanksType
            };

            TheStateAccount = new() {
                ID = "123456789",
                Closed = false,
                Details = new() { Balance = 999999999 },
                Owner = TheState,
                Bank = TheBank,
                Type = TheBanksType
            };

            TheChurchAccount = new() {
                ID = "123456789",
                Closed = false,
                Details = new() { Balance = 999999999 },
                Owner = TheChurch,
                Bank = TheBank,
                Type = TheBanksType
            };

            Person.Accounts.Add(PersonAccount);
            Corporation.Accounts.Add(CorporationAccount);
            TheState.Accounts.Add(TheStateAccount);
            TheChurch.Accounts.Add(TheChurchAccount);

            TheBank.Accounts.Add(PersonAccount);
            TheBank.Accounts.Add(CorporationAccount);
            TheBank.Accounts.Add(TheStateAccount);
            TheBank.Accounts.Add(TheChurchAccount);

            Items = new() {
                new() {
                    ID = Guid.NewGuid(),
                    Name = "Misc Income",
                    User = Person,
                    MiscIncome = 500,
                    FederalJurisdiction = FederalJurisdiction,
                    LocalJurisdiction = LocalJurisdiction
                },
                new() {
                    ID = Guid.NewGuid(),
                    Name = "Apartment",
                    User = Person,
                    Apartments = new List<Apartment> {
                        new() {
                            Name="Apartment",
                            B1Units=2,
                            B2Units=1,
                            B1Rent = 200,
                            B2Rent = 400
                        }
                    },
                    MiscIncome = 500,
                    FederalJurisdiction = FederalJurisdiction,
                    LocalJurisdiction = LocalJurisdiction
                },
                new() {
                    ID = Guid.NewGuid(),
                    Name = "Hotel",
                    User = Person,
                    Hotels = new List<Hotel> {
                        new(){ 
                            Name="Hotel",
                            Rooms=5,
                            RoomRate=200,
                            Suites=5,
                            SuiteRate=1000,
                            MiscIncome=50
                        }
                    },
                    MiscIncome = 500,
                    FederalJurisdiction = FederalJurisdiction,
                    LocalJurisdiction = LocalJurisdiction
                },
                new() {
                    ID = Guid.NewGuid(),
                    Name = "Business",
                    User = Person,
                    Businesses = new List<Business> {
                        new() { 
                            Name="Pasta and nothing else",
                            Chairs=1,
                            AvgSpend=50,
                            CustPerHour=5,
                            HoursOpen=8
                        }
                    },
                    MiscIncome = 500,
                    FederalJurisdiction = FederalJurisdiction,
                    LocalJurisdiction = LocalJurisdiction
                },
            };

            PersonTransactions = new() {
                new() {
                    Name="GOVERNMENT ASSISTANCE",
                    Amount = 50000,
                    FromAccount = TheStateAccount,
                    ToAccount = PersonAccount,
                    State = TransactionState.COMPLETED,
                    Time = DateTime.Now,
                },
                new() {
                    Name="LAND PURCHASE PLOT A1",
                    Amount = 500,
                    FromAccount = PersonAccount,
                    ToAccount = TheStateAccount,
                    State = TransactionState.COMPLETED,
                    Time = DateTime.Now,
                },
                new() {
                    Name = "PRIEST INCOME",
                    Amount = 500,
                    FromAccount = TheChurchAccount,
                    ToAccount = PersonAccount,
                    State = TransactionState.COMPLETED,
                    Time = DateTime.Now,
                },
                new() {
                    Name="DONATION",
                    Amount = 5000,
                    FromAccount = PersonAccount,
                    ToAccount = TheChurchAccount,
                    State = TransactionState.COMPLETED,
                    Time = DateTime.Now,
                },
                new() {
                    Name="INCOME",
                    Amount = 5000,
                    FromAccount = CorporationAccount,
                    ToAccount = PersonAccount,
                    State = TransactionState.COMPLETED,
                    Time = DateTime.Now,
                },
                new() {
                    Name="BREATHABLE AIR FEE",
                    Amount = 500,
                    FromAccount = PersonAccount,
                    ToAccount = CorporationAccount,
                    State = TransactionState.COMPLETED,
                    Time = DateTime.Now,
                },
            };

            CorporateTransactions = new() {
                new() {
                    Name="HUSH MONEY",
                    Amount = 50000,
                    FromAccount = TheStateAccount,
                    ToAccount = CorporationAccount,
                    State = TransactionState.COMPLETED,
                    Time = DateTime.Now,
                },
                new() {
                    Name="LAND PURCHASE",
                    Amount = 500,
                    FromAccount = CorporationAccount,
                    ToAccount = TheStateAccount,
                    State = TransactionState.COMPLETED,
                    Time = DateTime.Now,
                },
                new() {
                    Name = "ALSO HUSH MONEY BUT HOLY",
                    Amount = 500,
                    FromAccount = TheChurchAccount,
                    ToAccount = CorporationAccount,
                    State = TransactionState.COMPLETED,
                    Time = DateTime.Now,
                },
                new() {
                    Name="PUBLIC DONATION FOR PR REASONS",
                    Amount = 5000,
                    FromAccount = CorporationAccount,
                    ToAccount = TheChurchAccount,
                    State = TransactionState.COMPLETED,
                    Time = DateTime.Now,
                },
                new() {
                    Name = "PAYOUT",
                    Amount = 500,
                    FromAccount = CorporationAccount,
                    ToAccount = PersonAccount,
                    State = TransactionState.COMPLETED,
                    Time = DateTime.Now,
                },
                new() {
                    Name = "AIR FEE",
                    Amount = 4500,
                    FromAccount = PersonAccount,
                    ToAccount = CorporationAccount,
                    State = TransactionState.COMPLETED,
                    Time = DateTime.Now,
                },
            };

        }

        [Test]
        public void IncomeItemCalculation() {
            IncomeItem Item = Items[0];

            Assert.AreEqual(500, Item.TotalMonthlyIncome(), "Income calculation failed for Business");
        }

        [Test]
        public void ApartmentIncomeCalculation() {
            IncomeItem Item = Items[1];

            Assert.AreEqual(500+800, Item.TotalMonthlyIncome(),"Income calculation failed for Apartments");

        }

        [Test]
        public void HotelIncomeCalculation() {
            IncomeItem Item = Items[2];

            Assert.AreEqual(500+91245+50, Item.TotalMonthlyIncome(), "Income calculation failed for Hotel");
        }

        [Test]
        public void BusinessIncomeCalculation() {
            IncomeItem Item = Items[3];

            Assert.AreEqual(500 + 30000, Item.TotalMonthlyIncome(), "Income calculation failed for Business");
        }

        [Test]
        public void PersonalJurisdictionTaxCalculation() {
            long NoTax = FederalJurisdiction.CalculateTax(Person, 0).Item1;
            long SomeTax = FederalJurisdiction.CalculateTax(Person, 5000).Item1;
            long MostTax = FederalJurisdiction.CalculateTax(Person, 10500).Item1;

            Assert.AreEqual(0, NoTax, "Tax was calculated for someone with no income");
            Assert.AreEqual(250, SomeTax, "Incorrect tax calculated");
            Assert.AreEqual(1575, MostTax, "Incorrect tax calculated");

            long LocalNoTax   = LocalJurisdiction.CalculateTax(Person, 0).Item1;
            long LocalSomeTax = LocalJurisdiction.CalculateTax(Person, 5000).Item1;
            long LocalMostTax = LocalJurisdiction.CalculateTax(Person, 10500).Item1;

            Assert.AreEqual(0, LocalNoTax, "Tax was calculated for someone with no income");
            Assert.AreEqual(0, LocalSomeTax, "Incorrect tax calculated");
            Assert.AreEqual(0, LocalMostTax, "Incorrect tax calculated");
        }

        [Test]
        public void CorporateJurisdictionTaxCalculation() {
            long NoTax = FederalJurisdiction.CalculateTax(Corporation, 0).Item1;
            long SomeTax = FederalJurisdiction.CalculateTax(Corporation, 5000).Item1;
            long MostTax = FederalJurisdiction.CalculateTax(Corporation, 10500).Item1;

            Assert.AreEqual(0, NoTax, "Tax was calculated for someone with no income");
            Assert.AreEqual(100, SomeTax, "Incorrect tax calculated");
            Assert.AreEqual(735, MostTax, "Incorrect tax calculated");

            long LocalNoTax = LocalJurisdiction.CalculateTax(Corporation, 0).Item1;
            long LocalSomeTax = LocalJurisdiction.CalculateTax(Corporation, 5000).Item1;
            long LocalMostTax = LocalJurisdiction.CalculateTax(Corporation, 10500).Item1;

            Assert.AreEqual(0, LocalNoTax, "Tax was calculated for someone with no income");
            Assert.AreEqual(250, LocalSomeTax, "Incorrect tax calculated");
            Assert.AreEqual(525, LocalMostTax, "Incorrect tax calculated");

        }

        [Test]
        public void GenerateReportTest() {
            TaxReport PersonalReport = TaxReport.GenerateTaxReport(Person, Items, PersonTransactions, FederalJurisdiction);
            TaxReport CorporateReport = TaxReport.GenerateTaxReport(Corporation,Items,CorporateTransactions,FederalJurisdiction);

            Assert.AreEqual(55500, PersonalReport.ExtraIncome);
            Assert.AreEqual(500, PersonalReport.ExtraIncomeTaxable);
            Assert.AreEqual(124095, PersonalReport.StaticIncome);
            Assert.AreEqual(179595, PersonalReport.GrandTotalIncome);
            Assert.AreEqual(18689, PersonalReport.GrandTotalTax);

            Assert.AreEqual(55000, CorporateReport.ExtraIncome);
            Assert.AreEqual(0,  CorporateReport.ExtraIncomeTaxable);
            Assert.AreEqual(124095,CorporateReport.StaticIncome);
            Assert.AreEqual(179095,CorporateReport.GrandTotalIncome);
            Assert.AreEqual(8687 + 6205, CorporateReport.GrandTotalTax);
        }
    }
}
