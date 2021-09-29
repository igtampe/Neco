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

            //In order to calculate area, let's *translate* this polygon to make sure all coords are positive:
            int XAdjust = -1 * Math.Min(0,GraphicalPoints.Min(P => P.X));
            int YAdjust = -1 * Math.Min(0,GraphicalPoints.Min(P => P.Y));

            //Provided/modified by http://csharphelper.com/blog/2014/07/calculate-the-area-of-a-polygon-in-c/

            // Add the first point to the end.
            int num_points = GraphicalPoints.Length;
            PointF[] pts = new PointF[num_points + 1];
            for (int i = 0; i < GraphicalPoints.Length; i++) {
                pts[i] = new(GraphicalPoints[i].X + XAdjust, GraphicalPoints[i].Y + YAdjust);
            }

            pts[num_points] = new(GraphicalPoints[0].X + XAdjust, GraphicalPoints[0].Y + YAdjust);

            // Get the areas.
            float area = 0;
            for (int i = 0; i < num_points; i++) {
                area +=
                    (pts[i + 1].X - pts[i].X) *
                    (pts[i + 1].Y + pts[i].Y) / 2;
            }

            // Return the result.
            return Math.Abs(area);
        }

        /// <summary>Creates a Clone of this district</summary>
        /// <returns>A Shallow copy of this district</returns>
        public object Clone() { return MemberwiseClone(); }

    }
}
