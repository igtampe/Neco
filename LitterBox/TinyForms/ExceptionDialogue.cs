using System;
using System.Windows.Forms;

namespace Igtampe.TinyForms {

    /// <summary>Shows an Exception Dialog</summary>
    public static class ExceptionDialogue {

        /// <summary>Shows an exception</summary>
        /// <param name="E"></param>
        /// <param name="Header"></param>
        /// <param name="Footer"></param>
        public static void Show(Exception E, string Header = "", string Footer = "") {
            MessageBox.Show($"{Header}\n\n{E.Source}:{E.Message}\n\n{E.StackTrace}\n\n{Footer}", "Uh oh spaghettios"
            , MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        }

    }
}
