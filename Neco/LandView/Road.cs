using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Igtampe.Neco.Common.LandView {

    /// <summary>Holds a road to display on a LandView map</summary>
    public class Road {

        /// <summary>ID of this road</summary>
        public Guid ID { get; set; }

        /// <summary>Name of this road</summary>
        public string Name { get; set; }

        /// <summary>comma separated point strings for storage</summary>
        public string Points { get; set; } //I hope this will save but it probably won't :shrug:

        /// <summary>Graphical points that define beginning, turns, and end of this road</summary>
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

        /// <summary>Country this Road belongs to</summary>
        public Country Country { get; set; }

    }
}
