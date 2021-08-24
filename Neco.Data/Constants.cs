using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igtampe.Neco.Data {
    internal static class Constants {

        private static string ConString;

        /// <summary>Connection string used by all contexts</summary>
        internal static string ConnectionString { 
            
            get {
                if (string.IsNullOrEmpty(ConString)) {
                    if (File.Exists("ConString.txt")) { ConString = File.ReadAllText("ConString.txt"); } 
                    else { ConString= @"Data Source=Localhost;Initial Catalog=Neco;Integrated Security=True"; }
                }
                return ConString;
            } 
            
            set { ConString = value; } 
        
        }

    }
}
