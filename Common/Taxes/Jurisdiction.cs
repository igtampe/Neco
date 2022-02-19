using System.Text.Json.Serialization;
using Igtampe.Neco.Common.Banking;
using Igtampe.Neco.Common.IDGenerators;
using Igtampe.Neco.Common.Income;

namespace Igtampe.Neco.Common.Taxes {

    /// <summary>Represnetation of any tax jurisdiction that can contain tax brackets</summary>
    public class Jurisdiction : ManuallyGeneratableIdentifiable<string>, Nameable, Depictable {

        private readonly static IDGenerator<string> Gen = new NumericalGenerator(5);

        /// <summary>Name of this jurisdiction</summary>
        public string Name { get; set; } = "";

        /// <summary>Image URL for the Flag of this jurisdiction</summary>
        public string ImageURL { get; set; } = "";

        /// <summary>Type of this jurisdiction</summary>
        public JurisdictionType? Type { get; set; }

        /// <summary>Population of this jurisdiction (useful for Corporation income calculation)</summary>
        public int Population { get; set; } = 0;

        /// <summary>ID of the tied account</summary>
        public string? TiedAccountID => TiedAccount?.ID;

        /// <summary>Account tied to this jurisdiction (where taxes will be paid to)</summary>
        [JsonIgnore]
        public Account? TiedAccount { get; set; }

        /// <summary>List of all accounts located in this jurisdiction</summary>
        [JsonIgnore]
        public List<Account> AccountsLocatedIn { get; set; } = new();

        /// <summary>Brackets this jurisdiction contains</summary>
        [JsonIgnore]
        public List<Bracket> Brackets { get; set; } = new();

        /// <summary>Jurisdiction this Jurisdiction is in <br/><br/> (IE: If this is a State, the parent would be its country)</summary>
        public Jurisdiction? ParentJurisdiction { get; set; }

        /// <summary>Jurisdiction this jurisdiction contains <br/><br/> (IE: If this is a country, its children would be its districts)</summary>
        [JsonIgnore]
        public List<Jurisdiction> ChildJurisdictions { get; set; } = new();

        /// <summary>Gets the top parent of this district</summary>
        /// <returns></returns>
        public Jurisdiction GetTopParent() {
            Jurisdiction? J = this;
            
            do {

                if (J.ParentJurisdiction is null) { return J; }
                J = J.ParentJurisdiction;

            } while (J is not null);

            throw new InvalidOperationException("I have no idea how I got here");
        }

        /// <summary>Calculate the tax of an Income and IncomeType</summary>
        /// <param name="Income"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public (long, Bracket?) CalculateTax(long Income, IncomeType Type) {
            Bracket? B = Brackets.FirstOrDefault(O => O.Start <= Income && Income < O.End && O.IncomeType == Type);
            return B is null ? (0, null) : (Convert.ToInt64(B.Rate * Income), B);
        }

        /// <summary>Generator for Jurisdiction IDs</summary>
        public override IDGenerator<string> IDGenerator => Gen;
    }
}
