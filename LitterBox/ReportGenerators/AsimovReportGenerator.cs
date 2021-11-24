using Igtampe.Neco.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Igtampe.Neco.Common;
using Igtampe.Neco.Common.EzTax;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace Igtampe.LitterBox.ReportGenerators {
    public class AsimovReportGenerator : ReportGenerator {

        public struct AsimovReportRow { 
            
            public int Index { get; set; }
            public string ID { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public long[] Accounts { get; set; }
            public long[] Incomes { get; set; }
            public long[] Taxes { get; set; }

            public List<string> ToRow() {
                List<string> R = new() { Index + "", ID, Name, Type };
                foreach (long L in Accounts) { R.Add(L + ""); }
                R.Add(Accounts.Sum()+"");
                R.Add("");
                foreach (long L in Incomes) { R.Add(L + ""); }
                R.Add(Incomes.Sum() + "");
                R.Add("");
                foreach (long L in Taxes) { R.Add(L + ""); }
                R.Add(Taxes.Sum() + "");
                return R;
            }

        }

        public AsimovReportGenerator(NecoContext necoDB) : base(necoDB) {}

        public override async Task GenerateReport(string OutputFile) {
            LaunchRForm();

            try { await RealGenerateRerpot(OutputFile); } catch (Exception E) {

                AppendExceptionToLogbox(E);
                MarkDone(false, "Uh oh spaghettios", "The Neco's have encountered a problem. Do not worry, they are safe.");
                return;
            }

            if (CancelationPending()) {
                MarkDone(false, "Cancelled", "The Necos have all come home");
                return;
            }

            MarkDone();
            return;
        }

        private async Task RealGenerateRerpot(string OutputFile) {

            UpdateStatus("Getting Banks");
            List<Bank> Banks = await NecoDB.Bank.ToListAsync();
            if (CancelationPending()) { return; }

            UpdateStatus("Getting Jurisdictions");
            List<TaxJurisdiction> Jurisdictions = await NecoDB.TaxJurisdiction.ToListAsync();
            if (CancelationPending()) { return; }

            UpdateStatus("Getting Users with all data");
            List<User> Users = await NecoDB.User
                .Include(U => U.Accounts).ThenInclude(A => A.Details)
                .Include(U => U.Accounts).ThenInclude(A => A.Bank)
                .Include(U => U.Type)
                .ToListAsync();
            if (CancelationPending()) { return; }

            //Now then
            UpdateStatus("Preparing Report");
            List<AsimovReportRow> Report = new();
            int Index = 0;
            double progress = 0;
            double peruserincrement = 1.0 / Users.Count;
            double perstepincrement = peruserincrement / 4.0;
            foreach (User U in Users) {

                UpdateProgress(Convert.ToInt32(progress * 100));
                UpdateStatus($"Processing {U.ID}: Adding ID Info");
                if (CancelationPending()) { return; }
                progress += perstepincrement;

                AsimovReportRow RR = new() {
                    Index = Index++, ID = U.ID, Name = U.Name, Type = U.Type.Name,
                    Accounts = new long[Banks.Count], Incomes = new long[Jurisdictions.Count], Taxes = new long[Jurisdictions.Count] };

                UpdateProgress(Convert.ToInt32(progress * 100));
                UpdateStatus($"Processing {U.ID}: Adding Bank Information");
                if (CancelationPending()) { return; }
                progress += perstepincrement;

                for (int i = 0; i < Banks.Count; i++) {
                    //Add the sum of the bank balances where the ID of the bank is the same as the bank
                    RR.Accounts[i] = U.Accounts.Where(B => B.Bank.ID == Banks[i].ID).Sum(A => A.Details.Balance);
                    AppendToLog($"Found in {Banks[i].ID} Accounts: {RR.Accounts[i]:n0}p");
                }

                UpdateProgress(Convert.ToInt32(progress * 100));
                UpdateStatus($"Processing {U.ID}: Generating Tax Report");
                if (CancelationPending()) { return; }
                progress += perstepincrement;

                TaxReport TR = await TaxReportGenerator.ReportGenerator(NecoDB, U);

                UpdateProgress(Convert.ToInt32(progress * 100));
                UpdateStatus($"Processing {U.ID}: Adding Income Information");
                if (CancelationPending()) { return; }
                progress += perstepincrement;

                for (int i = 0; i < Jurisdictions.Count; i++) {

                    if (!TR.IncomeBreakdownDictionary.TryGetValue(Jurisdictions[i], out RR.Incomes[i])) { RR.Incomes[i] = 0; }
                    if (!TR.PaymentBreakdownDictionary.TryGetValue(Jurisdictions[i], out RR.Taxes[i])) { RR.Taxes[i] = 0; }
                    AppendToLog($"Found in {Jurisdictions[i]} Income: {RR.Incomes[i]:n0}p");
                    AppendToLog($"Found in {Jurisdictions[i]} Tax: {RR.Taxes[i]:n0}p");

                }

                Report.Add(RR);
                TR.Report = "";
                TR.CSVReport = "";
                TR.PaymentBreakdownDictionary.Clear();
                TR.IncomeBreakdownDictionary.Clear();
                TR = null;
            }

            //Now then we have to actually prepare the report

            ExcelHandler = new();

            UpdateStatus("Preparing Excel Report");
            UpdateProgress(-1);
            if (CancelationPending()) { return; }
            ExcelHandler.App.Visible = true;

            //Titles
            AppendToLog("Preparing Titles");
            ExcelHandler.MergeCells(new("A1"), new("L1"), "ASIMOV REPORT");
            ExcelHandler.Range.Font.Bold = true;
            ExcelHandler.Range.Font.Size = 20;

            ExcelHandler.MergeCells(new("A2"), new("L2"), $"Generated on {DateTime.Now}");
            ExcelHandler.Range.Font.Italic = true;

            //Headers
            //Prepare the headers
            AppendToLog("Preparing Headers");
            List<string> Headers = new() { "INDEX","ID","NAME","TYPE" };
            Banks.ForEach(B => Headers.Add(B.ID));
            Headers.Add("Balance");
            Headers.Add("");
            Jurisdictions.ForEach(J => Headers.Add(J.Name));
            Headers.Add("Total Income");
            Headers.Add("");
            Jurisdictions.ForEach(J => Headers.Add(J.Name));
            Headers.Add("Total Tax");

            UpdateStatus("Writing Headers");
            if (CancelationPending()) { return; }
            progress = 0;
            double perHeaderIncrement = 1.0/Headers.Count;
            //Write the headers
            for (int i = 0; i < Headers.Count; i++) {
                UpdateProgress(Convert.ToInt32(progress * 100));
                ExcelHandler.SetCell(new(i+1, 5), Headers[i]);
                ExcelHandler.Range.Font.Color = Color.White;
                ExcelHandler.Range.Interior.Color = Color.Black;
                progress += perHeaderIncrement;
            }

            UpdateStatus("Writing Data");
            if (CancelationPending()) { return; }
            progress = 0;

            //Write the data
            for (int row = 6; row < Report.Count+6; row++) {
                UpdateProgress(Convert.ToInt32(progress * 100));
                if (CancelationPending()) { return; }
                List<string> R = Report[row-6].ToRow();
                for (int col = 0; col < R.Count; col++) { ExcelHandler.SetCell(new(col+1,row), R[col]); }
                AppendToLog($"Wrote data for {Users[row-6].Name}");
                progress += peruserincrement;
            }

            UpdateStatus("Writing Totals");
            if (CancelationPending()) { return; }
            UpdateProgress(-1);

            int TotalRow = 6 + Report.Count;
            int LastCol = Banks.Count + 1 + (Jurisdictions.Count + 1)*2+4+2;

            //Write the totals
            ExcelHandler.SetCell(new("A",TotalRow), "TOTAL");
            for (int i = 5; i < LastCol+1; i++) {
                ExcelHandler.SetCell(new(i, TotalRow), $"=SUM({ExcelHandler.IntToCol(i)}{TotalRow - 1}:{ExcelHandler.IntToCol(i)}6)");
            }

            UpdateStatus("Number Formats");

            //Number Formats
            ExcelHandler.Select(new("E6"), new(LastCol, TotalRow));
            ExcelHandler.SetNumberFormat("$#,##0.00");

            //Table
            ExcelHandler.CreateTable(new("A5"), new(LastCol, TotalRow - 1),"MainTable", "TableStyleDark2");

            UpdateStatus("Autofit");

            //Autofit
            ExcelHandler.AutoFit();

            UpdateStatus("Superheaders");

            //Super Headers
            ExcelHandler.SuperHeader(new("A4"), new("D4"), "IDENTIFIERS");
            
            int BankCol = 4 + Banks.Count + 1;
            ExcelHandler.SuperHeader(new("E4"), new(BankCol,4), "BANK BALANCES");

            int IncomeCol = BankCol + 2 + Jurisdictions.Count;
            ExcelHandler.SuperHeader(new(BankCol+2, 4), new(IncomeCol,4), "INCOME");

            int TaxCol = IncomeCol + 2 + Jurisdictions.Count;
            ExcelHandler.SuperHeader(new(IncomeCol + 2, 4), new(TaxCol, 4), "TAX");

            //Coloring
            ExcelHandler.Select(new("A",TotalRow),new(LastCol,TotalRow));
            ExcelHandler.Range.Font.Color = Color.White;
            ExcelHandler.Range.Interior.Color = Color.Black;

            UpdateStatus("Black Columns");

            //Columns
            ExcelHandler.BlackColumn(new(BankCol+1, 4), new(BankCol+1, TotalRow));
            ExcelHandler.BlackColumn(new(IncomeCol + 1, 4), new(IncomeCol + 1, TotalRow));
            ExcelHandler.BlackColumn(new(TaxCol + 1, 4), new(TaxCol + 1, TotalRow));

            UpdateStatus("Wrapping up");

            //Visibility
            ExcelHandler.Worksheet.SaveAs2(OutputFile);
            ExcelHandler.App.UserControl = true;

            MarkDone();
        }
    }
}
