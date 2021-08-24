namespace Igtampe.Neco.Common.Auth {

    /// <summary>Request to change a password</summary>
    public class PasswordChangeRequest {

        /// <summary>ID of the user to change the password of</summary>
        public string UserID { get; set; }

        /// <summary>User's current password</summary>
        public string CurrentPassword {get; set;}

        /// <summary>New Password</summary>
        public string NewPassword { get; set; }
    }
}
