namespace Igtampe.Neco.Common.Requests {

    /// <summary>A Neco request to certify an Item</summary>
    public class CertificationRequest {

        /// <summary>SessionID of the certification origin</summary>
        public System.Guid SessionID { get; set; }

        /// <summary>Text to certify</summary>
        public string Text { get; set; }

    }
}
