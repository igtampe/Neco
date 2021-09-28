using System.Drawing;

namespace Igtampe.Neco.Common.LandView {
    
    /// <summary>Interface that defines LandViewItems that are drawable on screen</summary>
    public interface ILandViewItem : System.ICloneable {
        
        public Point[] GraphicalPoints { get; }

        /// <summary>Width of this item</summary>
        /// <returns></returns>
        public int Width();

        /// <summary>Height of this item</summary>
        /// <returns></returns>
        public int Height();

        /// <summary>Area of this item</summary>
        /// <returns></returns>
        public double Area();

    }
}
