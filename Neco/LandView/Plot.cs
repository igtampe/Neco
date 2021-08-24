using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Igtampe.Neco.Common.LandView {
    
    /// <summary>A plot of land located in a <see cref="District"/></summary>
    public class Plot:ILandViewItem {

        /// <summary>Status of a Plot</summary>
        public enum PlotStatus { 
            /// <summary>Plot is unclaimed and is available for purchase from the <see cref="District"/></summary>
            UNCLAIMED, 

            /// <summary>Plot that is for sale by <see cref="TiedAccount"/></summary>
            FOR_SALE, 
            
            /// <summary>Plot that is owned and is not for  sale</summary>
            NOT_FOR_SALE, 
            
            /// <summary>Plot that has been built upon and can no longer be resold</summary>
            BUILT}

        /// <summary>ID of this plot</summary>
        public Guid ID { get; set; }

        /// <summary>District this Plot belongs to</summary>
        public District District { get; set; }

        /// <summary>Points that define the corners of this plot</summary>
        public Point[] Points { get; set; }

        /// <summary>Owner of this plot</summary>
        public BankAccount TiedAccount { get; set; }

        /// <summary>Name of this plot</summary>
        public string Name { get; set; } = "";

        /// <summary>Status of this plot</summary>
        public PlotStatus Status { get; set; } = PlotStatus.UNCLAIMED;

        /// <summary>Price per square meter of this plot</summary> 
        [Range(0,int.MaxValue)]
        public int PricePerSquareMeter { get; set; }

        /// <summary>Area of this plot in square meters</summary>
        /// <returns></returns>
        public double Area() {
            //Provided by https://stackoverflow.com/questions/2034540/calculating-area-of-irregular-polygon-in-c-sharp
            return Math.Abs(Points.Take(Points.Length - 1)
                .Select((p, i) => (Points[i + 1].X - p.X) * (Points[i + 1].Y + p.Y))
                .Sum() / 2);
        }

        /// <summary>Height of this plot in meters</summary>
        /// <returns></returns>
        public int Height() { return Points.Max(P => P.Y) - Points.Min(P => P.Y); }

        /// <summary>Width of this plot in meters</summary>
        /// <returns></returns>
        public int Width() { return Points.Max(P => P.X) - Points.Min(P => P.X); }

    }
}
