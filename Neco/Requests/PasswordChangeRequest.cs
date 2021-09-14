namespace Igtampe.Neco.Common.Requests {

    /// <summary>Request to change a password</summary>
    public class PasswordChangeRequest {

        /// <summary>ID of the session this PCR comes from</summary>
        public System.Guid SessionID {get; set;}

        /// <summary>User's current password</summary>
        public string CurrentPassword {get; set;}

        /// <summary>New Password</summary>
        public string NewPassword { get; set; }

        /// <summary>Compares this PasswordChangeRequest to another object</summary>
        /// <param name="obj"></param>
        /// <returns>True if and only if the object is a Password Change Request, and all fields match</returns>
        public override bool Equals(object obj) {
            if (obj is PasswordChangeRequest C) {
                return C.SessionID == SessionID &&
                    C.CurrentPassword == CurrentPassword &&
                    C.NewPassword == NewPassword;
            }
            return false;
        }

        /// <summary>Gets a hash code for this TaxBracket. Delegates to <see cref="SessionID"/></summary>
        /// <returns></returns>
        public override int GetHashCode() { return SessionID.GetHashCode(); }

        /// <summary>Creates a string representation of this TaxBracket</summary>
        /// <returns>"ID} : {Name}, From {Start} to {End} at {Rate}%</returns>
        public override string ToString() { return $"{SessionID} Password Change Request"; }


    }
}
