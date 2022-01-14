namespace Igtampe.Neco.API.Requests {

    /// <summary>Request to change a password</summary>
    public class ResetPasswordRequest {

        /// <summary>User ID of the user to reset the password to</summary>
        public string? User { get; set; }

        /// <summary>New Password</summary>
        public string? New { get; set; }
    }
}
