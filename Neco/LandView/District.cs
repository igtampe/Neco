using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Igtampe.Neco.Common.LandView {
    
    /// <summary>Districts in landview held in a <see cref="Country"/>, that holds <see cref="Plot"/>s</summary>
    public class District:ILandViewItem {

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
        public Point[] GraphicalPoints { 
            get {
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

        /// <summary>Area of this District</summary>
        /// <returns></returns>
        public double Area() {
            //Provided by https://stackoverflow.com/questions/2034540/calculating-area-of-irregular-polygon-in-c-sharp
            return Math.Abs(GraphicalPoints.Take(GraphicalPoints.Length - 1)
                .Select((p, i) => (GraphicalPoints[i + 1].X - p.X) * (GraphicalPoints[i + 1].Y + p.Y))
                .Sum() / 2);
        }

        /// <summary>Height of this district</summary>
        /// <returns></returns>
        public int Height() {
            return GraphicalPoints.Max(P => P.Y) - GraphicalPoints.Min(P => P.Y);
        }

        /// <summary>Width of this district</summary>
        /// <returns></returns>
        public int Width() {
            return GraphicalPoints.Max(P => P.X) - GraphicalPoints.Min(P => P.X);
        }

        /// <summary>Returns the X of the leftmost point</summary>
        /// <returns></returns>
        public int LeftmostX() {
            return GraphicalPoints.Min(P => P.X);
        }

        /// <summary>Returns the Y of the topmost point</summary>
        /// <returns></returns>
        public int TopmostY() {
            return GraphicalPoints.Min(P => P.Y);
        }

        /// <summary>Returns a point representing a (not necessarily existing) top left most point (Used for the plotter)</summary>
        /// <returns></returns>
        public Point Origin() {
            return new(LeftmostX(), TopmostY());
        }

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
