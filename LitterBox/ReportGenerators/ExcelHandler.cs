using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace Igtampe.LitterBox.ReportGenerators {
    public class ExcelHandler {

        /// <summary>Coordinate for Excel</summary>
        public struct ExcelCoord {

            private readonly string TheCoord;

            public ExcelCoord(string Coord) => TheCoord = Coord;

            public ExcelCoord(string col, int row) => TheCoord=col + row;

            public ExcelCoord(int col, int row) => TheCoord = IntToCol(col) + row;

            public override string ToString() => TheCoord;

        }

        public Excel.Application App { get; private set; }

        public Excel._Worksheet Worksheet { get; private set; }

        public Excel.Range Range { get; protected set; }

        public ExcelHandler() {

            App = new();
            //App.Visible = true;

            App.Workbooks.Add();
            Worksheet = (Excel.Worksheet)App.ActiveSheet;

        }

        public void MergeCells(ExcelCoord C1, ExcelCoord C2, object Value=null) {
            Range = Worksheet.Range[C1.ToString(), C2.ToString()];
            Range.Merge();
            if (Value!=null) { Range.Value2 = Value; }
        }

        public void Select(ExcelCoord C1, ExcelCoord? C2 = null) => Range = C2 != null 
            ? Worksheet.Range[C1.ToString(), C2.ToString()] 
            : Worksheet.Range[C1.ToString()];

        public void SetCell(ExcelCoord C, object Value) {
            Select(C);
            Range.Value2 = Value;
        }

        public void SetNumberFormat(string Format) => Range.NumberFormat = Format;

        public void AutoFit() => Range.Columns.AutoFit();

        public void CreateTable(ExcelCoord C1, ExcelCoord C2, string Name, string Style) {
            Select(C1, C2);
            var Table = Worksheet.ListObjects.AddEx(Excel.XlListObjectSourceType.xlSrcRange, Range,null, Excel.XlYesNoGuess.xlYes);
            Table.Name = Name;
            Table.TableStyle = Style;
        }

        public void SuperHeader(ExcelCoord C1, ExcelCoord C2, string Text) {
            MergeCells(C1, C2, Text);
            Range.Font.Color = Color.White;
            Range.Interior.Color = Color.Black;
            Range.Font.Bold = true;
            Range.Font.Italic = true;
        }

        public void BlackColumn(ExcelCoord C1, ExcelCoord C2) {
            Select(C1, C2);
            Range.Interior.Color = Color.Black;
            Range.ColumnWidth = 3;
        }

        public object GetCell(ExcelCoord C) {
            Select(C);
            return Range.Value2;
        }

        /// <summary>
        /// Converts an int to an excel column name. <br/><br/>
        /// Code provided by Graham on Stack Overflow <br/>(https://stackoverflow.com/questions/181596/how-to-convert-a-column-number-e-g-127-into-an-excel-column-e-g-aa)</summary>
        /// <param name="columnNumber"></param>
        /// <returns></returns>
        public static string IntToCol(int columnNumber) {
            string columnName = "";

            while (columnNumber > 0) {
                int modulo = (columnNumber - 1) % 26;
                columnName = Convert.ToChar('A' + modulo) + columnName;
                columnNumber = (columnNumber - modulo) / 26;
            }

            return columnName;
        }

    }
}
