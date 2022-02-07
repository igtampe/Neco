using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Igtampe.Neco.Common {

    /// <summary>Holds a notification for a User</summary>
    public class Notification {

        /// <summary>ID of this notification</summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid ID { get; set; }

        /// <summary>Text contained in this notification</summary>
        public string Text { get; set; } = "";

        /// <summary>Time at which this notification occurred</summary>
        public DateTime Time { get; set; } = DateTime.Now;

        /// <summary>User this notification belongs to</summary>
        [JsonIgnore]
        public User User { get; set; }

        /// <summary>Whether or not a user has seen this notification or not</summary>
        public bool Read { get; set; } = false;

        /// <summary>Compares this notification to another object</summary>
        /// <param name="obj"></param>
        /// <returns>True if and only if other object is a notification and its <see cref="ID"/> matches</returns>
        public override bool Equals(object obj) {
            if (obj is Notification N) { return N.ID == ID; }
            return false;
        }

        /// <summary>Gets a hashcode for this notification. Delegates to <see cref="ID"/></summary>
        /// <returns></returns>
        public override int GetHashCode() { return ID.GetHashCode();}

        /// <summary>Creates a string representation of this Notification</summary>
        /// <returns>{Id} : [{Time}] {Text}</returns>
        public override string ToString() {
            return $"{ID} : [{Time}] {Text}";
        }

    }
}
