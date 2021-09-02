using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Igtampe.Neco.Common.LandView {

    /// <summary>Country for LandView, which holds <see cref="District"/>s</summary>
    public class Country:ILandViewItem {

        /// <summary>ID of this country</summary>
        public Guid ID { get; set; }

        /// <summary>Name of this country</summary>
        public string Name { get; set; } = "";

        /// <summary>Districts in this country</summary>
        [JsonIgnore]
        public ICollection<District> Districts{ get; set; }

        /// <summary>Roads in this country</summary>
        [JsonIgnore]
        public ICollection<Road> Roads { get; set; }

        /// <summary>Width of this country in meters</summary>
        public int Width { get; set; } = 0;

        /// <summary>Height of this country in meters</summary>
        public int Height { get; set; } = 0;

        /// <summary>Price per square meter of unclaimed terrain</summary>
        [Range(0,int.MaxValue)]
        public int PricePerSquareMeter { get; set; } = 0;

        /// <summary>Sales tax rate on all land purchases</summary>
        [Range(0.0,1.0)]
        public double FederalSalesTax { get; set; } = 0.0;

        /// <summary>Neco User to direct payments of land to and handle accepting taxes</summary>
        public BankAccount FederalBankAccount { get; set; }

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


    }
}
