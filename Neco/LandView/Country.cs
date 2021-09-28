using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Text.Json.Serialization;

namespace Igtampe.Neco.Common.LandView {

    /// <summary>Country for LandView, which holds <see cref="District"/>s</summary>
    public class Country:ILandViewItem {

        /// <summary>ID of this country</summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid ID { get; set; }

        /// <summary>Name of this country</summary>
        public string Name { get; set; } = "";

        /// <summary>Districts in this country</summary>
        [JsonIgnore]
        public List<District> Districts{ get; set; }

        /// <summary>Roads in this country</summary>
        [JsonIgnore]
        public List<Road> Roads { get; set; }

        /// <summary>Width of this country in meters</summary>
        public int Width { get; set; } = 0;

        /// <summary>Height of this country in meters</summary>
        public int Height { get; set; } = 0;

        /// <summary>Price per square meter of unclaimed terrain</summary>
        [Range(0,int.MaxValue)]
        public int PricePerSquareMeter { get; set; } = 0;

        /// <summary>Neco User to direct payments of land to and handle accepting taxes</summary>
        public BankAccount FederalBankAccount { get; set; }

        /// <summary>Graphical points of the country</summary>
        public Point[] GraphicalPoints { get {

                Point[] Points = { new(-Width / 2, -Height / 2),
                                   new(Width / 2, -Height / 2),
                                   new(Width / 2, Height / 2),
                                   new(-Width / 2, Height / 2)};

                return Points;
            } 
        }

        /// <summary>Returns leftmost X of this country</summary>
        /// <returns></returns>
        public int LeftmostX() { return -Width / 2; }

        /// <summary>Returns topmost Y of this country</summary>
        /// <returns></returns>
        public int TopmostY() { return -Height / 2; }

        /// <summary>Area of this country in square meters</summary>
        /// <returns></returns>
        public double Area() { return Width * Height; }

        /// <summary>Height of this country in meters</summary>
        /// <returns></returns>
        int ILandViewItem.Height() { return Height; }

        /// <summary>Width of this country in meters</summary>
        /// <returns></returns>
        int ILandViewItem.Width() { return Width; }

        /// <summary>Compares this Country to another object</summary>
        /// <param name="obj"></param>
        /// <returns>True if and only if the object is a Country and the <see cref="ID"/> matches with this one's</returns>
        public override bool Equals(object obj) {
            if (obj is Country C) { return C.ID == ID; }
            return false;
        }

        /// <summary>Gets a hash code for this Country. Delegates to <see cref="ID"/></summary>
        /// <returns></returns>
        public override int GetHashCode() { return ID.GetHashCode(); }

        /// <summary>Creates a string representation of this Country</summary>
        /// <returns>{ID} : {Name}</returns>
        public override string ToString() { return $"{ID} : {Name}"; }

        /// <summary>Creates a Clone of this country</summary>
        /// <returns>A Shallow copy of this country</returns>
        public object Clone() { return MemberwiseClone();}

    }
}
