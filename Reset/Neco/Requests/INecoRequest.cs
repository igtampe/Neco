namespace Igtampe.Neco.Common.Requests {
    /// <summary>Defines the absolute basic needs of a Neco Request</summary>
    public interface INecoRequest {

        /// <summary>ID of the session making this request</summary>
        public System.Guid SessionID { get; set; }

    }
}
