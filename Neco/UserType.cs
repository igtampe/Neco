namespace Igtampe.Neco.Common {

    /// <summary>Determines the taxability of transactions to or from accounts held by users of this user type</summary>
    public enum TaxationType {

        /// <summary>Transactions are taxable to and from this account<br/>
        /// Users of this type should be taxed by EzTax<br/>
        /// This taxation type should be used by general users</summary>
        Taxable,

        /// <summary>Transactions are NOT taxable if this account is the destination<br/><br/>
        /// IE: Money sent to this account is NOT COUNTED TOWARDS INCOME of the sender.<br/><br/>
        /// Users of this type should NOT be taxed by EzTax<br/>
        /// This taxation type should be used by Charities</summary>
        NonTaxableDestination,

        /// <summary>Transactions from this account are NOT taxable<br/>
        /// IE: Money received from this account is not counted towards income of the recipient<br/>
        /// Users of this type should NOT be taxed by EzTax<br/>
        /// This taxation type should be used by Governments</summary>
        NontaxableOrigin
    }

    /// <summary>Type of user in NECO</summary>
    public class UserType {

        /// <summary>ID of this User Type</summary>
        public System.Guid ID { get; set; }

        /// <summary>Name of this User Type (IE Standard, Corporate, Government)</summary>
        public string Name { get; set; } = "";

        /// <summary>Identifies how to handle taxes to and from this account</summary>
        public TaxationType Taxation { get; set; }

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
