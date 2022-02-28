using Igtampe.Neco.V2N.Forms;

namespace Igtampe.Neco.V2N {
    internal static class Program {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm() { Icon=Properties.Resources.V2N });
        }
    }
}