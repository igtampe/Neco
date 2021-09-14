namespace Igtampe.Neco.Common {

    /// <summary>Holds NECO Bank account details that are confidential</summary>
    public class BankAccountDetail {

        /// <summary>ID of this bank account</summary>
        public System.Guid Id { get; set; }

        /// <summary>Balance in this bank account</summary>
        public long Balance { get; set; } = 0;

        /// <summary>Boolean to check if this bank account is overdrafted</summary>
        /// <returns></returns>
        public bool IsOverdrafted() { return Balance < 0; }

        /// <summary>Compares this BankAccountDetails item to another object</summary>
        /// <param name="obj"></param>
        /// <returns>True if and only if the object is a BankAccountDetails item and the <see cref="Id"/> matches with this one's</returns>
        public override bool Equals(object obj) {
            if (obj is BankAccountDetail C) { return C.Id == Id; }
            return false;
        }

        /// <summary>Gets a hash code for this BankAccountDetails item. Delegates to <see cref="Id"/></summary>
        /// <returns></returns>
        public override int GetHashCode() { return Id.GetHashCode(); }

        /// <summary>Creates a string representation of this BankAccountDetails item</summary>
        /// <returns>{Id} : Account with balance {Balance:N0}p</returns>
        public override string ToString() { return $"{Id} : Account with balance {Balance:N0}p"; }

    }
}
