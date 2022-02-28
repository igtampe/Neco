using System.Text.Json.Serialization;

namespace Igtampe.Neco.Common {

    /// <summary></summary>
    public class Notification : AutomaticallyGeneratableIdentifiable {

        /// <summary>Text of this notification</summary>
        public string Text { get; set; } = "";

        /// <summary>Date and Time this notification was sent</summary>
        public DateTime Date { get; set; } = DateTime.Now;

        /// <summary>User that this notification belongs to</summary>
        [JsonIgnore]
        public User? User { get; set; }

        /// <summary>String representation of this notification with all relevant data</summary>
        /// <returns></returns>
        public override string ToString() => $"Notification \'{ID}\' for {User?.ID ?? "Unknown User"}: [{Date}] {Text}";
    }
}
