using Igtampe.Neco.Common;

namespace Igtampe.ViBE {

    /// <summary>A ViBE User</summary>
    public class User : Identifiable<string>, Nameable {

        /// <summary>Name of the ViBE user (from name.dll)</summary>
        public string Name { get; set; } = "";

        /// <summary>If the user is government or not (determined by if their name has (Gov.) at the end</summary>
        public bool IsGov => Name.EndsWith("(Gov.)");

        /// <summary>If the user is corporate or not (determined by if their name has (Corp.) at the end</summary>
        public bool IsCorp => Name.EndsWith("(Corp.)");

        /// <summary>Pin of this ViBE user (from pin.dll)</summary>
        public string Pin { get; set; } = "";

        /// <summary>Balance of this user's UMSNB Account. Null if the user has no UMSNB account</summary>
        public long? UMSNB { get; set; } = null;

        /// <summary>Balance of this user's GBANK Account. Null if the user has no GBANK account</summary>
        public long? GBANK { get; set; } = null;

        /// <summary>Balance of this user's RIVER account. Null if the user has no RIVER account</summary>
        public long? RIVER { get; set; } = null;

        /// <summary>Checks if this user has any accounts. Checks returns true if at least one of the balances is not null </summary>
        public bool HasAccounts => !(UMSNB is null && GBANK is null && RIVER is null);

        public long TotalWealth => UMSNB ?? 0 + GBANK ?? 0 + RIVER ?? 0;

        /// <summary>List of <see cref="IncomeItem"/>s associated to this User</summary>
        public List<IncomeItem> IncomeItems { get; set; } = new();

        /// <summary>Retreives data for user ID from files and folders located in UsersDirectory and IRFDirectory</summary>
        /// <param name="ID"></param>
        /// <param name="UsersDirectory"></param>
        /// <param name="IRFDirectory"></param>
        /// <returns></returns>
        public static async Task<User> FromFiles(string ID, string UsersDirectory, string IRFDirectory) {

            if (!Directory.Exists(UsersDirectory) || !Directory.Exists(Path.Combine(UsersDirectory, ID))) { throw new DirectoryNotFoundException("Could not find UsersDirectory"); }
            if (!IsViBEUserFolder(Path.Combine(UsersDirectory, ID))) { throw new ArgumentException($"Path found at {ID} was not a ViBE Folder", nameof(ID)); }

            User U = new() {
                ID = ID, 
                Name = (await File.ReadAllLinesAsync(Path.Combine(UsersDirectory, ID, "Name.dll")))[0],
                Pin = (await File.ReadAllLinesAsync(Path.Combine(UsersDirectory, ID, "Pin.dll")))[0],
                
                UMSNB = !Directory.Exists(Path.Combine(UsersDirectory, ID, "UMSNB"))
                    ? null
                    : long.Parse(await File.ReadAllTextAsync(Path.Combine(UsersDirectory, ID, "UMSNB", "BALANCE.DLL"))),
                
                GBANK = !Directory.Exists(Path.Combine(UsersDirectory, ID, "GBANK"))
                    ? null
                    : long.Parse(await File.ReadAllTextAsync(Path.Combine(UsersDirectory, ID, "GBANK", "BALANCE.DLL"))),
                
                RIVER = !Directory.Exists(Path.Combine(UsersDirectory, ID, "RIVER"))
                    ? null
                    : long.Parse(await File.ReadAllTextAsync(Path.Combine(UsersDirectory, ID, "RIVER", "BALANCE.DLL"))),
                
                IncomeItems = await IncomeItem.IncomeItemsFromIRF(Path.Combine(IRFDirectory, $"{ID}.IncomeRegistry.csv"))

        };

            return U;
        }

        public static bool IsViBEUserFolder(string Directory) => File.Exists(Path.Combine(Directory, "NAME.DLL")) && File.Exists(Path.Combine(Directory,"PIN.DLL"));

    }
}
