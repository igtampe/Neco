using System;
using System.Drawing;
using System.Linq;

namespace Igtampe.Neco.Common.LandView {
    
    /// <summary>Interface that defines LandViewItems that are drawable on screen</summary>
    public abstract class LandViewItem : System.ICloneable {
        
        /// <summary>Graphical points that represent a polygon for this landview item</summary>
        public abstract Point[] GraphicalPoints { get; set;  }

        /// <summary>Gets this landviewitem's leftmost X coordinate</summary>
        /// <returns></returns>
        public virtual int LeftmostX() { return GraphicalPoints.Min(P => P.X); }

        /// <summary>Gets this landviewitem's Topmost y coordinate</summary>
        /// <returns></returns>
        public virtual int TopmostY() { return GraphicalPoints.Min(P => P.Y); }

        /// <summary>Width of this item</summary>
        /// <returns></returns>
        public virtual int Width() { return GraphicalPoints.Max(P => P.X) - GraphicalPoints.Min(P => P.X); }

        /// <summary>Height of this item</summary>
        /// <returns></returns>
        public virtual int Height() { return GraphicalPoints.Max(P => P.Y) - GraphicalPoints.Min(P => P.Y); }

        /// <summary>Area of this item</summary>
        /// <returns></returns>
        public virtual double Area() {
            //Provided by https://stackoverflow.com/questions/2034540/calculating-area-of-irregular-polygon-in-c-sharp
            return Math.Abs(GraphicalPoints.Take(GraphicalPoints.Length - 1)
                .Select((p, i) => (GraphicalPoints[i + 1].X - p.X) * (GraphicalPoints[i + 1].Y + p.Y))
                .Sum() / 2);
        }

        /// <summary>Creates a Clone of this district</summary>
        /// <returns>A Shallow copy of this district</returns>
        public object Clone() { return MemberwiseClone(); }

    }
}
