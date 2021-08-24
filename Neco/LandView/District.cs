using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Igtampe.Neco.Common.LandView {
    
    /// <summary>Districts in landview held in a <see cref="Country"/>, that holds <see cref="Plot"/>s</summary>
    public class District:ILandViewItem {

        /// <summary>ID of this </summary>
        public Guid ID { get; set; }

        /// <summary>Name of this District</summary>
        public string Name { get; set; } = "";

        /// <summary>Country this district belongs to</summary>
        public Country Country { get; set; }

        /// <summary>Graphical points that define the corners of this district</summary>
        public Point[] Points { get; set; } //I hope this will save but it probably won't :shrug:

        /// <summary>Collection of plots that are in this district</summary>
        public ICollection<Plot> Plots { get; set; }

        /// <summary>Price per square meter of unclaimed terrain</summary>
        [Range(0, int.MaxValue)]
        public int PricePerSquareMeter { get; set; } = 0;

        /// <summary>District sales tax on all land sales in this district</summary>
        [Range(0.0, 1.0)]
        public double DistrictSalesTax { get; set; } = 0.0;

        /// <summary>Bank account of this district to handle accepting taxes and accept land payments</summary>
        public BankAccount DistrictBankAccount { get; set; }

        /// <summary>Area of this District</summary>
        /// <returns></returns>
        public double Area() {
            //Provided by https://stackoverflow.com/questions/2034540/calculating-area-of-irregular-polygon-in-c-sharp
            return Math.Abs(Points.Take(Points.Length - 1)
                .Select((p, i) => (Points[i + 1].X - p.X) * (Points[i + 1].Y + p.Y))
                .Sum() / 2);
        }

        /// <summary>Height of this district</summary>
        /// <returns></returns>
        public int Height() {
            return Points.Max(P => P.Y) - Points.Min(P => P.Y);
        }

        /// <summary>Width of this district</summary>
        /// <returns></returns>
        public int Width() {
            return Points.Max(P => P.X) - Points.Min(P => P.X);
        }
    }
}
