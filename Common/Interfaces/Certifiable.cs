namespace Igtampe.Neco.Common {

    /// <summary>Interface for any item that's certifiable</summary>
    public interface Certifiable {

        /// <summary>Generates a Certification for this item</summary>
        /// <returns></returns>
        public CertifiedItem GenerateCertification(User Certifier);

    }
}
