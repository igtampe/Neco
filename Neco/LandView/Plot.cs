using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace Igtampe.Neco.Common.LandView {
    
    /// <summary>A plot of land located in a <see cref="District"/></summary>
    public class Plot:ILandViewItem {

        /// <summary>Status of a Plot</summary>
        public enum PlotStatus { 
            /// <summary>Plot is unclaimed and is available for purchase from the <see cref="District"/></summary>
            UNCLAIMED, 

            /// <summary>Plot that is for sale by <see cref="Owner"/></summary>
            FOR_SALE, 
            
            /// <summary>Plot that is owned and is not for  sale</summary>
            NOT_FOR_SALE, 
            
            /// <summary>Plot that has been built upon and can no longer be resold</summary>
            BUILT}

        /// <summary>ID of this plot</summary>
        public Guid ID { get; set; }

        /// <summary>District this Plot belongs to</summary>
        public District District { get; set; }

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
                    Ps.Add(new Point(X, Y));
                }

                return Ps.ToArray();
            }
            set {
                List<string> Ps = new();
                foreach (Point P in value) { Ps.Add($"{P.X},{P.Y}"); }
                Points = string.Join(";", Ps.ToArray());
            }
        }

        /// <summary>Owner of this plot</summary>
        public User Owner { get; set; }

        /// <summary>Name of this plot</summary>
        public string Name { get; set; } = "";

        /// <summary>Status of this plot</summary>
        public PlotStatus Status { get; set; } = PlotStatus.UNCLAIMED;

        /// <summary>Area of this plot in square meters</summary>
        /// <returns></returns>
        public double Area() {
            //Provided by https://stackoverflow.com/questions/2034540/calculating-area-of-irregular-polygon-in-c-sharp
            return Math.Abs(GraphicalPoints.Take(GraphicalPoints.Length - 1)
                .Select((p, i) => (GraphicalPoints[i + 1].X - p.X) * (GraphicalPoints[i + 1].Y + p.Y))
                .Sum() / 2);
        }

        /// <summary>Height of this plot in meters</summary>
        /// <returns></returns>
        public int Height() { return GraphicalPoints.Max(P => P.Y) - GraphicalPoints.Min(P => P.Y); }

        /// <summary>Width of this plot in meters</summary>
        /// <returns></returns>
        public int Width() { return GraphicalPoints.Max(P => P.X) - GraphicalPoints.Min(P => P.X); }

        /// <summary>Compares this Plot to another object</summary>
        /// <param name="obj"></param>
        /// <returns>True if and only if the object is a Plot and the <see cref="ID"/> matches with this one's</returns>
        public override bool Equals(object obj) {
            if (obj is Plot C) { return C.ID == ID; }
            return false;
        }

        /// <summary>Gets a hash code for this Plot. Delegates to <see cref="ID"/></summary>
        /// <returns></returns>
        public override int GetHashCode() { return ID.GetHashCode(); }

        /// <summary>Creates a string representation of this Plot</summary>
        /// <returns>{ID} : {Name}</returns>
        public override string ToString() { return $"{ID} : {Name}"; }

    }
}
