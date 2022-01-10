using System.IO;

namespace Igtampe.Neco.Data {
    internal static class Constants {

        private static string ConString;

        private const string ConStringFile = "ConString.txt";
        private const string DefaultConString = @"Data Source=Localhost;Initial Catalog=Neco;Integrated Security=True";

        /// <summary>Connection string used by all contexts</summary>
        internal static string ConnectionString { 

            get {
                if (!File.Exists(ConStringFile)) { File.WriteAllText(ConStringFile,DefaultConString); }
                if (string.IsNullOrEmpty(ConString)) {ConString = File.ReadAllText(ConStringFile);}
                return ConString;
            } 
            
            set { ConString = value; } 
        
        }

    }
}
