namespace Igtampe.Neco.Common {

    /// <summary>Type of user in NECO</summary>
    public class UserType {

        /// <summary>ID of this User Type</summary>
        public System.Guid ID { get; set; }

        /// <summary>Name of this User Type (IE Standard, Corporate, Government)</summary>
        public string Name { get; set; } = "";

        /// <summary>Check if an object is equal to this user type</summary>
        /// <param name="obj"></param>
        /// <returns>True if and only if the object is a UserType, and if its ID matches with this one's</returns>
        public override bool Equals(object obj) {
            if (obj is UserType UT) { return UT.ID == ID; }
            return false;
        }

        /// <summary>Gets hashcode for this usertype. Delegates to <see cref="ID"/></summary>
        /// <returns></returns>
        public override int GetHashCode() { return ID.GetHashCode(); }

        /// <summary>Returns the name of this user type</summary>
        /// <returns></returns>
        public override string ToString() {return Name; }

    }
}
