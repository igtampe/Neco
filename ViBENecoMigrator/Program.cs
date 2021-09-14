using System;
using System.IO;
using Igtampe.BasicRender;
using Igtampe.BasicGraphics;
using Igtampe.Neco.Common;
using Igtampe.Neco.Common.EzTax;
using Igtampe.Neco.Common.EzTax.Subitems;
using Igtampe.Neco.Common.UMSAT;
using System.Linq;
using System.Data.OleDb;
using Igtampe.Neco.Data;

using System.Collections.Generic;

namespace ViBENecoMigrator {
    static class Program {
        private static ProgressBar LocalProgressBar;
        private static ProgressBar MainProgressBar;

        private static NecoContext C;

        private static readonly string[] SpinnerCycle = { "|", "/", "-", "\\" };

        private static readonly Cycler SpinnerCycler = new(SpinnerCycle);

        private static int X;
        private static int TopTextHeight;

        static void Main(string[] args) {

            #region Visual Setup
            Console.Clear();
            Graphic V2NGraphic = HiColorGraphic.LoadFromResource(Properties.Resources.V2N);

            //Determine the position to display the gaphic
            X = (Console.WindowWidth - V2NGraphic.GetWidth()) / 2;
            TopTextHeight = 1 + V2NGraphic.GetHeight() + 1;
            V2NGraphic.Draw(X, 1);
            Draw.CenterText("ViBE to NEco Migration Tool", TopTextHeight);

            LocalProgressBar = new ProgressBar(V2NGraphic.GetWidth(), X, TopTextHeight + 5);
            MainProgressBar = new ProgressBar(V2NGraphic.GetWidth(), X, TopTextHeight + 9);

            #endregion

            #region Setup Constants

            UpdateLocal("Setting up EverythingContext", 0);
            UpdateMain("Setting up", 0);

            C = new NecoContext();

            //Banks
            UpdateLocal("Banks", 0);
            UpdateMain("Setting up some Constants", 0.05);

            Bank UMSNB = new() { ID = "UMSNB", Name = "The UMS National Bank", AccountTypes = new List<BankAccountType>() };
            BankAccountType UMSNBChecking = new() { ID = Guid.NewGuid(), Bank = UMSNB, Name = "UMSNB EveryDay(tm) Checking", InterestRate = 0.02 };
            UMSNB.AccountTypes.Add(UMSNBChecking);
            Spin();

            Bank GBANK = new() { ID = "GBANK", Name = "G-Bank", AccountTypes = new List<BankAccountType>() };
            BankAccountType GBANKChecking = new() { ID = Guid.NewGuid(), Bank = GBANK, Name = "G-Bank Checking", InterestRate = 0.00 };
            GBANK.AccountTypes.Add(GBANKChecking);
            Spin();

            Bank RIVER = new() { ID = "RIVER", Name = "Riverside Bank", AccountTypes = new List<BankAccountType>() };
            BankAccountType RIVERChecking = new() { ID = Guid.NewGuid(), Bank = RIVER, Name = "Riverside Checking", InterestRate = 0.05 };
            GBANK.AccountTypes.Add(RIVERChecking);
            Spin();

            C.Add(UMSNB);
            C.Add(GBANK);
            C.Add(RIVER);
            Spin();

            UpdateLocal("User Types", .5);
            UserType STANDARD = new() { ID = Guid.NewGuid(), Name = "Standard" };
            UserType CORPORATE = new() { ID = Guid.NewGuid(), Name = "Corporate" };
            UserType GOVERNMENT = new() { ID = Guid.NewGuid(), Name = "Government" };
            Spin();

            C.Add(STANDARD);
            C.Add(CORPORATE);
            C.Add(GOVERNMENT);
            Spin();

            #endregion

            #region Users

            UpdateMain("Get Users", 0.1);
            UpdateLocal("Getting Users", 0);

            if (!Directory.Exists("ImportData/Users")) { return; }

            double perUser = 1.0 / Directory.GetDirectories("ImportData/Users").Length;

            User AccUMSGov = null;
            User AccNewpondGov = null;
            User AccUrbiaGov = null;
            User AccParadisusGov = null;
            User AccLaertesGov = null;
            User AccNOstenGov = null;
            User AccSOstenGov = null;
            Spin();

            string CD;

            foreach (var userfolder in Directory.GetDirectories("ImportData/Users")) {

                CD = $"{userfolder}";

                //Get the user
                User U = new();
                UserAuth UA = new();
                Spin();

                //Get ID and Username
                U.ID = userfolder.Substring(userfolder.Length - 5, 5);
                UA.ID = U.ID;
                UA.Pin = GetFirstLine($"{CD}/pin.dll");
                U.Name = GetFirstLine($"{CD}/Name.dll");
                Spin();

                //Get account type
                if (U.Name.EndsWith(" (Gov.)")) {
                    U.Name = U.Name.Replace(" (Gov.)", "");
                    U.Type = GOVERNMENT;
                } else if (U.Name.EndsWith(" (Corp.)")) {
                    U.Name = U.Name.Replace(" (Corp.)", "");
                    U.Type = CORPORATE;
                } else { U.Type = STANDARD; }
                Spin();

                U.Accounts = new List<BankAccount>();

                if (Directory.Exists($"{CD}/UMSNB")) {
                    BankAccount Acc = new();
                    Acc.ID = U.ID.Substring(0,4)+"1";
                    Acc.Type = UMSNBChecking;
                    Acc.Bank = UMSNB;

                    Acc.Details = new ();
                    Acc.Details.ID = Guid.NewGuid();
                    Acc.Details.Balance = long.Parse(GetFirstLine($"{CD}/UMSNB/Balance.dll"));
                    Acc.Details.Owner = U;
                    U.Accounts.Add(Acc);
                }
                Spin();


                if (Directory.Exists($"{CD}/GBANK")) {
                    BankAccount Acc = new();
                    Acc.ID = U.ID.Substring(0, 4) + "2";
                    Acc.Type = GBANKChecking;
                    Acc.Bank = GBANK;

                    Acc.Details = new();
                    Acc.Details.ID = Guid.NewGuid();
                    Acc.Details.Balance = long.Parse(GetFirstLine($"{CD}/GBANK/Balance.dll"));
                    Acc.Details.Owner = U;
                    U.Accounts.Add(Acc);
                }
                Spin();

                if (Directory.Exists($"{CD}/RIVER")) {
                    BankAccount Acc = new();
                    Acc.ID = U.ID.Substring(0, 4) + "1";
                    Acc.Type = RIVERChecking;
                    Acc.Bank = RIVER;

                    Acc.Details = new();
                    Acc.Details.ID = Guid.NewGuid();
                    Acc.Details.Balance = long.Parse(GetFirstLine($"{CD}/RIVER/Balance.dll"));
                    Acc.Details.Owner = U;
                    U.Accounts.Add(Acc);
                }
                Spin();

                C.Add(U);
                C.Add(UA);

                //Save accounts that we'll need later
                switch (U.ID) {
                    case "33118":
                        AccUMSGov = U;
                        break;
                    case "86700":
                        AccNewpondGov = U;
                        break;
                    case "86701":
                        AccParadisusGov = U;
                        break;
                    case "86702":
                        AccUrbiaGov = U;
                        break;
                    case "86703":
                        AccLaertesGov = U;
                        break;
                    case "86704":
                        AccNOstenGov = U;
                        break;
                    case "86705":
                        AccSOstenGov = U;
                        break;
                    default:
                        break;
                }

                Spin();

                UpdateLocal($"Got {U.Name}", LocalProgressBar.Percent + perUser);

            }

            #endregion

            #region EzTax

            Directory.GetDirectories("../.."); //This changes directory so we have to move back up

            //Do this after the accounts.
            //EzTaxJurisdictions
            UpdateMain("Getting EzTax Stuff", .40);
            UpdateLocal("EzTax Jurisdictions", 0);

            //Define these hardcoded since we have them
            TaxJurisdiction UMSJurisdiction = new() { ID = Guid.NewGuid(), Name = "The UMS", Account = AccUMSGov.Accounts.FirstOrDefault(), Brackets = new List<TaxBracket>() };
            Spin();
            TaxJurisdiction NewJurisdiction = new() { ID = Guid.NewGuid(), Name = "Newpond", Account = AccNewpondGov.Accounts.FirstOrDefault(), Brackets = new List<TaxBracket>() };
            Spin();
            TaxJurisdiction ParJurisdiction = new() { ID = Guid.NewGuid(), Name = "Paradisus", Account = AccParadisusGov.Accounts.FirstOrDefault(), Brackets = new List<TaxBracket>() };
            Spin();
            TaxJurisdiction UrbJurisdiction = new() { ID = Guid.NewGuid(), Name = "Urbia", Account = AccUrbiaGov.Accounts.FirstOrDefault(), Brackets = new List<TaxBracket>() };
            Spin();
            TaxJurisdiction LaeJurisdiction = new() { ID = Guid.NewGuid(), Name = "Laertes", Account = AccLaertesGov.Accounts.FirstOrDefault(), Brackets = new List<TaxBracket>() };
            Spin();
            TaxJurisdiction NosJurisdiction = new() { ID = Guid.NewGuid(), Name = "North Osten", Account = AccNOstenGov.Accounts.FirstOrDefault(), Brackets = new List<TaxBracket>() };
            Spin();
            TaxJurisdiction SosJurisdiction = new() { ID = Guid.NewGuid(), Name = "South Osten", Account = AccSOstenGov.Accounts.FirstOrDefault(), Brackets = new List<TaxBracket>() };
            Spin();


            //OK now the tax brackets
            UpdateLocal("Getting Tax Brackets", .25);

            //For this we're going to need to open a file
            //actually should we I mean by this point
            //There will be no changes to the Tax code by the time NECO goes live so I won't do that

            //FED: Personal Taxed,0.05,5000000,       
            TaxBracket FedPersonal = new() {
                ID = Guid.NewGuid(), Name = "Federal Personal Taxed",
                Jurisdiction = UMSJurisdiction, Type = STANDARD, Rate = 0.05,
                Start = 5000000, End = long.MaxValue
            };
            Spin();

            //FED: Personal Untaxed,0.00,      0,5000000
            TaxBracket FedPersonalUntaxed = new() {
                ID = Guid.NewGuid(), Name = "Federal Personal Untaxed",
                Jurisdiction = UMSJurisdiction, Type = STANDARD, Rate = 0.00,
                Start = 0, End = 5000000
            };
            Spin();

            //
            //FCorp: Corporate Taxed,0.02,500000000,       
            TaxBracket FedCorporate = new() {
                ID = Guid.NewGuid(), Name = "Federal Corporate Taxed",
                Jurisdiction = UMSJurisdiction, Type = CORPORATE, Rate = 0.02,
                Start = 500000000, End = long.MaxValue
            };
            Spin();

            //FCorp: Corporate Untaxed,0.00,        0,500000000
            TaxBracket FedCorporateUntaxed = new() {
                ID = Guid.NewGuid(), Name = "Federal Corporate Untaxed",
                Jurisdiction = UMSJurisdiction, Type = CORPORATE, Rate = 0.00,
                Start = 0, End = 500000000
            };
            Spin();

            //
            //New: Newpond Flat Personal,0.001,0,
            TaxBracket NewpondPersonal = new() {
                ID = Guid.NewGuid(), Name = "Newpond Flat Personal",
                Jurisdiction = NewJurisdiction, Type = STANDARD, Rate = 0.001,
                Start = 0, End = long.MaxValue
            };
            Spin();

            //
            //NCorp: Newpond Flat Corporate,0.001,0,
            TaxBracket NewpondCorporate = new() {
                ID = Guid.NewGuid(), Name = "Newpond Flat Corporate",
                Jurisdiction = NewJurisdiction, Type = CORPORATE, Rate = 0.001,
                Start = 0, End = long.MaxValue
            };
            Spin();

            //
            //Urb: Urbia Flat Personal,0.005,0,
            TaxBracket UrbiaPersonal = new() {
                ID = Guid.NewGuid(), Name = "Urbia Flat Personal",
                Jurisdiction = UrbJurisdiction, Type = STANDARD, Rate = 0.005,
                Start = 0, End = long.MaxValue
            };
            Spin();

            //
            //UCorp: Urbia Flat Corporate,0.005,0,
            TaxBracket UrbiaCorporate = new() {
                ID = Guid.NewGuid(), Name = "Urbia Flat Corporate",
                Jurisdiction = UrbJurisdiction, Type = CORPORATE, Rate = 0.005,
                Start = 0, End = long.MaxValue
            };
            Spin();

            //
            //Par: Paradisus Untaxed,0,0,
            TaxBracket ParadisusUntaxed = new() {
                ID = Guid.NewGuid(), Name = "Paradisus Untaxed",
                Jurisdiction = ParJurisdiction, Type = STANDARD, Rate = 0.00,
                Start = 0, End = long.MaxValue
            };
            Spin();

            //
            //PCorp: Paradisus Untaxed,0,0,
            TaxBracket ParadisusCorpUntaxed = new() {
                ID = Guid.NewGuid(), Name = "Paradisus Corp Untaxed",
                Jurisdiction = ParJurisdiction, Type = CORPORATE, Rate = 0.00,
                Start = 0, End = long.MaxValue
            };
            Spin();

            //
            //Lae: Laertes Untaxed,0,0,
            TaxBracket LaertesUntaxed = new() {
                ID = Guid.NewGuid(), Name = "Laertes Untaxed",
                Jurisdiction = LaeJurisdiction, Type = STANDARD, Rate = 0.00,
                Start = 0, End = long.MaxValue
            };
            Spin();

            //
            //LCorp: Laertes Corporate Untaxed,0,0,
            TaxBracket LaertesCorpUntaxed = new() {
                ID = Guid.NewGuid(), Name = "Laertes Corp Untaxed",
                Jurisdiction = LaeJurisdiction, Type = CORPORATE, Rate = 0.00,
                Start = 0, End = long.MaxValue
            };
            Spin();

            //
            //NOsten: North Osten Personal Flat,0.005,0,
            TaxBracket NOstenPersonalFlat = new() {
                ID = Guid.NewGuid(), Name = "North Osten Flat Personal",
                Jurisdiction = NosJurisdiction, Type = STANDARD, Rate = 0.005,
                Start = 0, End = long.MaxValue
            };
            Spin();

            //
            //NOCorp: North Osten Flat Corporate Tax,0.01,0,
            TaxBracket NOstenCorpFlat = new() {
                ID = Guid.NewGuid(), Name = "North Osten Flat Corporate",
                Jurisdiction = NosJurisdiction, Type = CORPORATE, Rate = 0.01,
                Start = 0, End = long.MaxValue
            };
            Spin();

            //
            //SOsten: South Osten Personal Flat,0.002,0,
            TaxBracket SOstenPersonalFlat = new() {
                ID = Guid.NewGuid(), Name = "South Osten Flat Personal",
                Jurisdiction = SosJurisdiction, Type = STANDARD, Rate = 0.002,
                Start = 0, End = long.MaxValue
            };
            Spin();

            //
            //SOCorp: South Osten Corporate Flat,0.002,0,
            TaxBracket SOstenCorpFlat = new() {
                ID = Guid.NewGuid(), Name = "South Osten Flat Corporate",
                Jurisdiction = SosJurisdiction, Type = CORPORATE, Rate = 0.002,
                Start = 0, End = long.MaxValue
            };
            Spin();

            UMSJurisdiction.Brackets.Add(FedPersonal);
            UMSJurisdiction.Brackets.Add(FedCorporate);
            UMSJurisdiction.Brackets.Add(FedPersonalUntaxed);
            UMSJurisdiction.Brackets.Add(FedCorporateUntaxed);
            Spin();

            NewJurisdiction.Brackets.Add(NewpondPersonal);
            NewJurisdiction.Brackets.Add(NewpondCorporate);
            Spin();

            ParJurisdiction.Brackets.Add(ParadisusUntaxed);
            ParJurisdiction.Brackets.Add(ParadisusCorpUntaxed);
            Spin();

            UrbJurisdiction.Brackets.Add(UrbiaPersonal);
            UrbJurisdiction.Brackets.Add(UrbiaCorporate);
            Spin();

            LaeJurisdiction.Brackets.Add(LaertesUntaxed);
            LaeJurisdiction.Brackets.Add(LaertesCorpUntaxed);
            Spin();

            NosJurisdiction.Brackets.Add(NOstenPersonalFlat);
            NosJurisdiction.Brackets.Add(NOstenCorpFlat);
            Spin();

            SosJurisdiction.Brackets.Add(SOstenPersonalFlat);
            SosJurisdiction.Brackets.Add(SOstenCorpFlat);
            Spin();

            C.Add(UMSJurisdiction);
            C.Add(NewJurisdiction);
            C.Add(ParJurisdiction);
            C.Add(UrbJurisdiction);
            C.Add(LaeJurisdiction);
            C.Add(NosJurisdiction);
            C.Add(SosJurisdiction);
            Spin();

            C.Add(FedPersonal);
            C.Add(FedCorporate);
            C.Add(FedPersonalUntaxed);
            C.Add(FedCorporateUntaxed);
            C.Add(NewpondPersonal);
            C.Add(NewpondCorporate);
            C.Add(ParadisusUntaxed);
            C.Add(ParadisusCorpUntaxed);
            C.Add(UrbiaPersonal);
            C.Add(UrbiaCorporate);
            C.Add(LaertesUntaxed);
            C.Add(LaertesCorpUntaxed);
            C.Add(NOstenPersonalFlat);
            C.Add(NOstenCorpFlat);
            C.Add(SOstenPersonalFlat);
            C.Add(SOstenCorpFlat);
            Spin();

            //Now then the income items
            UpdateLocal("Getting User Incomes", 0.5);
            CD = $"ImportData/UploadedReports";

            perUser = 0.5 / Directory.GetFiles($"{CD}").Length; //0.5 since half of a percent

            foreach (string csv in Directory.GetFiles($"{CD}")) {
                Spin();
                string[] csvname = csv.Split("."); //verify that this is in fact a CSV
                if (csvname.Length != 3) { continue; }

                //Find an attatched user
                User U = C.User.Find(csvname[0].Substring(csvname[0].Length - 5, 5));
                if (U is null) { continue; }

                //Create a TaxUserInfo for that user
                TaxUserInfo TUI = new() { ID = Guid.NewGuid(), User = U, Items = new List<IncomeItem>() };

                string[] csvAllLines = File.ReadAllLines($"{csv}");

                double perItem = perUser / csvAllLines.Length;

                foreach (string item in csvAllLines) {
                    Spin();
                    string[] currentline = item.Split(',');
                    if (currentline.Length < 2) { continue; }
                    if (currentline.Length == 2) {
                        //Legacy Item
                        TUI.Items.Add(new IncomeItem() { ID = Guid.NewGuid(), Name = currentline[0], FederalJurisdiction = UMSJurisdiction, LocalJurisdiction = null, MiscIncome = long.Parse(currentline[1]), User = U });
                    } else {
                        //New Item

                        string Name = currentline[0];
                        Apartment Apt = new() {
                            SUnits = int.Parse(currentline[2]), B1Units = int.Parse(currentline[3]),
                            B2Units = int.Parse(currentline[4]), B3Units = int.Parse(currentline[5]),
                            PUnits = int.Parse(currentline[6]), SRent = int.Parse(currentline[7]),
                            B1Rent = int.Parse(currentline[8]), B2Rent = int.Parse(currentline[9]),
                            B3Rent = int.Parse(currentline[10]), PRent = int.Parse(currentline[11]),
                            ID = Guid.NewGuid(), Name = $"{Name} Apartment"
                        };

                        Hotel Hotel = new() {
                            Rooms = int.Parse(currentline[12]), Suites = int.Parse(currentline[13]),
                            RoomRate = int.Parse(currentline[14]), SuiteRate = int.Parse(currentline[15]),
                            MiscIncome = int.Parse(currentline[16]), Name = $"{Name} Hotel", ID = Guid.NewGuid()
                        };

                        Business Business = new() {
                            Chairs = int.Parse(currentline[17]), AvgSpend = int.Parse(currentline[18]),
                            CustPerHour = int.Parse(currentline[19]), HoursOpen = int.Parse(currentline[20]),
                            ID = Guid.NewGuid(), Name = $"{Name} Business"
                        };

                        long MiscIncome = long.Parse(currentline[21]);

                        TaxJurisdiction Jurisdiction = null;

                        switch (currentline[22]) {
                            case "Newpond":
                                Jurisdiction = NewJurisdiction;
                                break;
                            case "Paradisus":
                                Jurisdiction = ParJurisdiction;
                                break;
                            case "Urbia":
                                Jurisdiction = UrbJurisdiction;
                                break;
                            case "Laertes":
                                Jurisdiction = LaeJurisdiction;
                                break;
                            case "South Osten":
                                Jurisdiction = SosJurisdiction;
                                break;
                            case "North Osten":
                                Jurisdiction = NosJurisdiction;
                                break;
                            default:
                                break;
                        }

                        IncomeItem I = new() {
                            ID = Guid.NewGuid(), Name = Name, FederalJurisdiction = UMSJurisdiction,
                            LocalJurisdiction = Jurisdiction, MiscIncome = MiscIncome, User = U,
                            Subitems = new List<IncomeSubitem>()
                        };

                        if (Apt.Income() > 0) {
                            Apt.IncomeItem = I;
                            I.Subitems.Add(Apt);
                            C.Add(Apt);
                        }

                        if (Hotel.Income() > 0) {
                            Hotel.IncomeItem = I;
                            I.Subitems.Add(Hotel);
                            C.Add(Hotel);
                        }

                        if (Business.Income() > 0) {
                            Business.IncomeItem = I;
                            I.Subitems.Add(Business);
                            C.Add(Business);
                        }

                        TUI.Items.Add(I);

                    }
                    UpdateLocal($"Added item {currentline[0]}", LocalProgressBar.Percent + perItem);
                }

                C.Add(TUI);

            }

            #endregion

            #region UMSAT
#pragma warning disable CA1416 // Validate platform compatibility

            UpdateLocal("Getting UMSAT Items", 0.0);
            UpdateMain("UMSAT", 0.75);

            using (OleDbConnection UMSATCon = new("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ImportData/UMSAssetTrack.accdb")) {
                Spin();
                UMSATCon.Open();
                OleDbCommand cmd = UMSATCon.CreateCommand();
                cmd.CommandText = "SELECT AName, OwnerID, City, ImageURL, Status, DOC, DOLM, Complete From Registry";
                cmd.Connection = UMSATCon;
                OleDbDataReader Reader = cmd.ExecuteReader();
                Spin();

                while (Reader.Read()) {
                    if (!DateTime.TryParse((string)Reader[5], out DateTime DOC)) { DOC = DateTime.MinValue; }
                    if (!DateTime.TryParse((string)Reader[6], out DateTime DOLU)) { DOLU = DateTime.Now; }
                    Asset A = new() {
                        ID = Guid.NewGuid(), Name = (string)Reader[0], Owner = C.User.Find((string)Reader[1]),
                        SpecificLocaiton = (string)Reader[2], Image = new System.Net.WebClient().DownloadData((string)Reader[3]),
                        Description = (string)Reader[4], CreationDate = DOC,  UpdateDate = DOLU, Complete = bool.Parse((string)Reader[7]), 
                        IncomeItem = null, Plot = null
                    };
                    UpdateLocal($"Processed record {A.Name}", 0);

                    Spin();
                    C.Add(A);
                }

            }

            UpdateLocal("Done", 1);
            UpdateMain("Done", 1);


#pragma warning restore CA1416 // Validate platform compatibility. This code will only ever be run on windows so shhhh
            #endregion

            #region Landview
            //Landview will be handled by the plotter since there is some considerable amount of agrimensura that needs to be done
            #endregion

            Draw.CenterText($"{C.ChangeTracker.Entries().Count()} Entries to change. Press a key to execute",TopTextHeight+11);
            RenderUtils.Pause();
            Draw.ClearLine(TopTextHeight + 11);

            UpdateLocal("", 0);
            UpdateMain("", 0);

            C.SaveChanges();

            Draw.CenterText($"Success! All entities were saved properly", TopTextHeight + 11);
            RenderUtils.Pause();

        }

        private static string GetFirstLine(string Filename) {return File.ReadAllLines(Filename)[0];}

        private static void Spin() { Draw.CenterText(SpinnerCycler.Cycle(),TopTextHeight+2); }

        private static void UpdateLocal(string Status, double Percent ) {
            if (Percent < LocalProgressBar.Percent) { Draw.ClearLine(TopTextHeight + 5); }
            LocalProgressBar.Percent = Percent;
            LocalProgressBar.DrawBar();
            Draw.ClearLine(TopTextHeight + 3);
            Draw.Sprite(Status, ConsoleColor.Black, ConsoleColor.White, X, TopTextHeight + 3);
        }

        private static void UpdateMain(string Status, double Percent) {
            if (Percent < MainProgressBar.Percent) { Draw.ClearLine(TopTextHeight + 9); }
            MainProgressBar.Percent = Percent;
            MainProgressBar.DrawBar();
            Draw.ClearLine(TopTextHeight + 7);
            Draw.Sprite(Status, ConsoleColor.Black, ConsoleColor.White, X, TopTextHeight + 7);
        }



    }
}
