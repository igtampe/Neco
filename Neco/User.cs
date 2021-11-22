﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Igtampe.Neco.Common {

    /// <summary>User in the NECO system</summary>
    public class User {

        /// <summary>UMSWEB ID of this user</summary>
        [MinLength(5)]
        [MaxLength(5)]
        [Key]
        public string ID { get; set; }

        /// <summary>Name of this user</summary>
        public string Name { get; set; } = "";

        /// <summary><see cref="UserType"/> of this user</summary>
        public UserType Type { get; set; }

        /// <summary>Accoutns this user owns</summary>
        public List<BankAccount> Accounts { get; set; }

        /// <summary>Notifications this user has</summary>
        public List<Notification> Notifications { get; set; }

        /// <summary>Compares this User to another object</summary>
        /// <param name="obj"></param>
        /// <returns>True if and only if the other object is a user and their ID matches</returns>
        public override bool Equals(object obj) {
            if (obj is User U) { return U.ID == ID; }
            return false;
        }

        /// <summary>Gets a hashcode for this user. Delegates to <see cref="ID"/></summary>
        /// <returns></returns>
        public override int GetHashCode() { return ID.GetHashCode(); }

        /// <summary>returns a colon seperated list of <see cref="ID"/> and <see cref="Name"/></summary>
        /// <returns></returns>
        public override string ToString() { return $"{ID}:{Name}"; }

    }
}
