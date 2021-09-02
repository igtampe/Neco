using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igtampe.Neco.Common {

    /// <summary>Holds a notification for a User</summary>
    public class Notification {

        /// <summary>ID of this notification</summary>
        public Guid Id { get; set; }

        /// <summary>Text contained in this notification</summary>
        public string Text { get; set; } = "";

        /// <summary>Time at which this notification occurred</summary>
        public DateTime Time { get; set; } = DateTime.Now;

        /// <summary>User this notification belongs to</summary>
        public User User { get; set; }

        /// <summary>Whether or not a user has seen this notification or not</summary>
        public bool Read { get; set; } = false;

        /// <summary>Compares this notification to another object</summary>
        /// <param name="obj"></param>
        /// <returns>True if and only if other object is a notification and its <see cref="Id"/> matches</returns>
        public override bool Equals(object obj) {
            if (obj is Notification N) { return N.Id == Id; }
            return false;
        }

        /// <summary>Gets a hashcode for this notification. Delegates to <see cref="Id"/></summary>
        /// <returns></returns>
        public override int GetHashCode() { return Id.GetHashCode();}

        /// <summary>Creates a string representation of this Notification</summary>
        /// <returns>{Id} : [{Time}] {Text}</returns>
        public override string ToString() {
            return $"{Id} : [{Time}] {Text}";
        }

    }
}
