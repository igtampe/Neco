using System.Drawing;

namespace Igtampe.Neco.Common.LandView {
    
    /// <summary>Interface that defines LandViewItems that are drawable on screen</summary>
    public interface ILandViewItem : System.ICloneable {
        
        /// <summary>Graphical points that represent a polygon for this landview item</summary>
        public Point[] GraphicalPoints { get; }

        /// <summary>Gets this landviewitem's leftmost X coordinate</summary>
        /// <returns></returns>
        public int LeftmostX();

        /// <summary>Gets this landviewitem's Topmost y coordinate</summary>
        /// <returns></returns>
        public int TopmostY();

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
