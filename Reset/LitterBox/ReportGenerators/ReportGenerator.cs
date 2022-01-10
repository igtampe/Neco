using System;
using System.Threading.Tasks;
using Igtampe.Neco.Data;

namespace Igtampe.LitterBox.ReportGenerators {
    public abstract class ReportGenerator {

        /// <summary>NeconDB to get all data</summary>
        protected NecoContext NecoDB { get; }

        /// <summary>Handler to handle all Excel operations</summary>
        protected ExcelHandler ExcelHandler { get; set; }

        /// <summary>Status form</summary>
        private ReportGeneratorStatusForm RForm { get; }

        public ReportGenerator(NecoContext necoDB) { 
            NecoDB = necoDB;
            ExcelHandler = new();
            RForm = new();
        }

        /// <summary>Generates a report</summary>
        /// <param name="OutputFile">Output file for the XLSX document</param>
        public abstract Task GenerateReport(string OutputFile);

        protected void LaunchRForm() => RForm.Show();

        protected void UpdateStatus(string Text) => RForm.UpdateText(Text);

        protected void UpdateProgress(int Progress) => RForm.SetProgress(Progress);

        protected void SetMarquee() => RForm.SetProgress(-1);

        protected void AppendToLog(string Text) => RForm.AppendToLogbox(Text);

        protected void AppendExceptionToLogbox(Exception E) => RForm.AppendToLogbox($"Exception at {E.Source} {E.TargetSite}: {E.Message}\n\n{E.StackTrace}");

        protected void MarkDone(bool Success = true, string Header = "Done!", string Message = "The Necos are pleased to present you this report") => RForm.MarkDone(Success,Header,Message);

        protected bool CancelationPending() => RForm.CancelPending;

    }
}
