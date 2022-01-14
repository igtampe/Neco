using System.ComponentModel.DataAnnotations;

namespace Igtampe.Neco.Common.Banking {

    /// <summary>A Transaction between two bank accounts</summary>
    public class Transaction : AutomaticallyGeneratableIdentifiable, Nameable, Certifiable {

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

        /// <summary>Generates a certification for this transaction</summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public CertifiedItem GenerateCertification(User Certifier) => new() { CertifiedBy = Certifier, Date = DateTime.Now, Text = $"{Name}: Transfered {Amount:n0} from {Origin?.ID} to {Destination?.ID}" };

    }
}
