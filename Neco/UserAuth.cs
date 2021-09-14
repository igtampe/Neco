using System.ComponentModel.DataAnnotations;

namespace Igtampe.Neco.Common {

    /// <summary>User used for Authentication</summary>
    public class UserAuth {

        /// <summary>UMSWEB ID of this user</summary>
        [MaxLength(5)]
        [MinLength(5)]
        public string Id { get; set; }

        /// <summary>Pin of this user</summary>
        [MaxLength(4)]
        [MinLength(4)]
        public string Pin { get; set; }

        /// <summary>Checks the provided pin against this user's pin</summary>
        /// <param name="Pin"></param>
        /// <returns>Returns true if the provided pin matches this user's pin</returns>
        public bool CheckPin(string Pin) { return Pin == this.Pin; }

        /// <summary>Checks if this and another <see cref="UserAuth"/> object are the same</summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) {
            if (obj is UserAuth E) { return E.Id == Id && Pin == E.Pin;}
            return false;
        }

        /// <summary>Gets a Hashcode for this UserAuth (Delegated to <see cref="Id"/>)</summary>
        /// <returns></returns>
        public override int GetHashCode() { return Id.GetHashCode(); }

        /// <summary>Returns ID for this user auth</summary>
        /// <returns></returns>
        public override string ToString() { return Id; }

    }
}
