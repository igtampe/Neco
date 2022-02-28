using System.ComponentModel.DataAnnotations;

namespace Igtampe.Neco.Common.Banking {

    /// <summary>A Transaction between two bank accounts</summary>
    public class Transaction : AutomaticallyGeneratableIdentifiable, Nameable  {

        /// <summary>Origin of this transaction</summary>
        public Account? Origin { get; set; }

        /// <summary>Destination of this transaction</summary>
        public Account? Destination { get; set; }

        /// <summary>Amount transfered in this transaction</summary>
        [Range(0,int.MaxValue)]
        public long Amount { get; set; } = 0;

        /// <summary>Date and time this transaction was executed on</summary>
        public DateTime Date { get; set; } = DateTime.Now;

        /// <summary>Name of this transaction</summary>
        public string Name { get; set; } = "";


        /// <summary>Returns a string representation of this transaction</summary>
        /// <returns></returns>
        public override string ToString() => $"Transaction {ID}: {Origin?.ID ?? "Unknown Account"}==[{Amount}]==>{Destination?.ID ?? "Unknown Account"} on {Date}";

    }
}
