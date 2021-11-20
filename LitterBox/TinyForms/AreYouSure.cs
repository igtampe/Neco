using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Igtampe.TinyForms {
    /// <summary>Quick code snippet to ask if the user is sure they want to do something</summary>
    public static class AreYouSure {

        /// <summary>Asks the user if he's sure they want to do the given action</summary>
        /// <param name="Text"></param>
        /// <returns>True if the user wants to do this</returns>
        public static bool Ask(string Action = "do this") { return MessageBox.Show($"Are you sure you want to {Action}?", "Sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes; }

    }
}
