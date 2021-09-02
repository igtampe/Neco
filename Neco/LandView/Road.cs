﻿using System;
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

        /// <summary>Compares this Road to another object</summary>
        /// <param name="obj"></param>
        /// <returns>True if and only if the object is a Road and the <see cref="ID"/> matches with this one's</returns>
        public override bool Equals(object obj) {
            if (obj is Road C) { return C.ID == ID; }
            return false;
        }

        /// <summary>Gets a hash code for this Road. Delegates to <see cref="ID"/></summary>
        /// <returns></returns>
        public override int GetHashCode() { return ID.GetHashCode(); }

        /// <summary>Creates a string representation of this Road</summary>
        /// <returns>{ID} : {Name}</returns>
        public override string ToString() { return $"{ID} : {Name}"; }

    }
}