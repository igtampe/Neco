using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Igtampe.Neco.Common.LandView {
    
    /// <summary>Districts in landview held in a <see cref="Country"/>, that holds <see cref="Plot"/>s</summary>
    public class District:LandViewItem {

        /// <summary>ID of this </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid ID { get; set; }

        /// <summary>Name of this District</summary>
        public string Name { get; set; } = "";

        /// <summary>Country this district belongs to</summary>
        public Country Country { get; set; }

        /// <summary>comma separated point strings for storage</summary>
        public string Points { get; set; } //I hope this will save but it probably won't :shrug:

        /// <summary>Graphical points that define the corners of this district</summary>
        [NotMapped]
        public override Point[] GraphicalPoints { 
            get {
                if (string.IsNullOrWhiteSpace(Points)) { return Array.Empty<Point>(); }
                List<Point> Ps = new();
                foreach (string P in Points.Split(';')) {
                    int X = int.Parse(P.Split(',')[0]);
                    int Y = int.Parse(P.Split(',')[1]);
                    Ps.Add(new Point(X,Y));
                }

                return Ps.ToArray();
            } 
            set {
                List<string> Ps = new();
                foreach (Point P in value) {Ps.Add($"{P.X},{P.Y}");}
                Points = string.Join(";",Ps.ToArray());
            }
        }

        /// <summary>Collection of plots that are in this district</summary>
        [JsonIgnore]
        public List<Plot> Plots { get; set; }

        /// <summary>Price per square meter of unclaimed terrain</summary>
        [Range(0, int.MaxValue)]
        public int PricePerSquareMeter { get; set; } = 0;

        /// <summary>Bank account of this district to handle accepting taxes and accept land payments</summary>
        public BankAccount DistrictBankAccount { get; set; }

        /// <summary>Compares this District to another object</summary>
        /// <param name="obj"></param>
        /// <returns>True if and only if the object is a District and the <see cref="ID"/> matches with this one's</returns>
        public override bool Equals(object obj) {
            if (obj is District C) { return C.ID == ID; }
            return false;
        }

        /// <summary>Gets a hash code for this District. Delegates to <see cref="ID"/></summary>
        /// <returns></returns>
        public override int GetHashCode() { return ID.GetHashCode(); }

        /// <summary>Creates a string representation of this District</summary>
        /// <returns>{ID} : {Name}</returns>
        public override string ToString() { return $"{ID} : {Name}"; }

    }
}
