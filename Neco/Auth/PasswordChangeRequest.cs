namespace Igtampe.Neco.Common.Auth {

    /// <summary>Request to change a password</summary>
    public class PasswordChangeRequest {

        /// <summary>ID of the user to change the password of</summary>
        public string UserID { get; set; }

        /// <summary>User's current password</summary>
        public string CurrentPassword {get; set;}

        /// <summary>New Password</summary>
        public string NewPassword { get; set; }

        /// <summary>Compares this PasswordChangeRequest to another object</summary>
        /// <param name="obj"></param>
        /// <returns>True if and only if the object is a TaxBracket and the <see cref="ID"/> matches with this one's</returns>
        public override bool Equals(object obj) {
            if (obj is PasswordChangeRequest C) {
                return C.UserID == UserID &&
                    C.CurrentPassword == CurrentPassword &&
                    C.NewPassword == NewPassword;
            }
            return false;
        }

        /// <summary>Gets a hash code for this TaxBracket. Delegates to <see cref="ID"/></summary>
        /// <returns></returns>
        public override int GetHashCode() { return UserID.GetHashCode(); }

        /// <summary>Creates a string representation of this TaxBracket</summary>
        /// <returns>"ID} : {Name}, From {Start} to {End} at {Rate}%</returns>
        public override string ToString() { return $"{UserID} Password Change Request"; }


    }
}
