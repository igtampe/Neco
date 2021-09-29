using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Igtampe.Neco.Common.LandView {
    public static class LandViewUtils {

        /// <summary>Validates a Points string for use on a <see cref="Plot"/>,<see cref="District"/>, or a <see cref="Road"/></summary>
        /// <param name="Points"></param>
        /// <param name="MinLength">Minimum number of points. 2 for roads, 3 for plots and districts</param>
        /// <returns></returns>
        public static bool ValidatePoints(string Points, int MinLength) {
            if (string.IsNullOrWhiteSpace(Points)) { return false; }
            //Ensure blank ones can be actually saved.
            if (Points.Length == 0) { return true; }
            if (Points.Length < MinLength) { return false; }

            //First let's split this by semicolon
            foreach (string ppair in Points.Split(";")) {
                //Split it again by colon
                string[] ppairsplit = ppair.Split(",");
                if (ppairsplit.Length != 2) { return false; } //If we have more than three coords then no
                if (!int.TryParse(ppairsplit[0], out int _)) { return false; } //Test to ensure both can be
                if (!int.TryParse(ppairsplit[1], out int _)) { return false; } //parsed as ints
            }
            return true;
        }

        /// <summary>Checks if any of the plots in its district intersect with the given plot</summary>
        /// <param name="P"></param>
        /// <returns></returns>
        public static Plot GetIntersectingPlot(Plot P) { return GetIntersectingPlot(P.District.Plots,P); }

        /// <summary>Gets a plot from the existing collection of plots that intersect with given new plot <br/>
        /// This method is meant for finding if any existing plots intersect with a given plot</summary>
        /// <param name="Existing"></param>
        /// <param name="New"></param>
        /// <returns>The ILandviewItem that intersects, or null if none was found</returns>
        public static Plot GetIntersectingPlot(ICollection<Plot> Existing, Plot New) {
            foreach (var Item in Existing) {
                if (Item.Equals(New)) { continue; } //If the new item already exists, then let's ignore it. We can't intersect with ourselves.
                if (Intersects(Item, New)) { return Item; }
            }

            return null;
        }

        /// <summary>Calculates a Plot's district in a given country</summary>
        /// <param name="C"></param>
        /// <param name="P"></param>
        /// <returns>The smallest district that the plot is completely in, or null if the plot is not in the country, or is not *completely* in any district</returns>
        public static District CalculatePlotDistrict(Country C, Plot P) {

            //If the plot is not in the country, return null
            if (!Contains(C, P)) { return null; }

            //Now then
            return GetSmallestDistrictContaining(C.Districts, P);
        }

        /// <summary>
        /// Gets the smallest district (by area) From the list of potential containers that contains the given Item.<br/>
        /// This method is meant for finding the smallest district that contains a plot
        /// </summary>
        /// <param name="Containers"></param>
        /// <param name="Item"></param>
        /// <returns>the item, or null if none was found</returns>
        public static District GetSmallestDistrictContaining(ICollection<District> AllDistricts, LandViewItem Item) {

            List<District> Containers = new();

            //Now then:
            foreach (var Container in AllDistricts) {
                if (Contains(Container, Item)) { Containers.Add(Container); }
            }

            //If there are no containers, return null
            if (Containers.Count == 0) { return null; }

            //If there is one container, return that container
            if (Containers.Count == 1) { return Containers.First(); }

            //if there is mroe than one, return the container that 
            return Containers.Aggregate((curMin, x) => (curMin == null || x.Area() < curMin.Area() ? x : curMin));

        }

        /// <summary>Calculates if L1 contains L2</summary>
        /// <param name="L1">Shape that could contain L2</param>
        /// <param name="L2">Shape that could be contained by L1</param>
        /// <returns>Whether or not L1 contains L2</returns>
        public static bool Contains(LandViewItem L1, LandViewItem L2) {
            if (!FastContains(L1, L2)) { return false; } //If R1 does not contain R2, then we can immediately stop.
            return DeepContains(L1, L2);
        }

        /// <summary>Calculates if a rectangle around L1 contains a rectangle around L2. Used to discard, not to assert</summary>
        /// <param name="L1"></param>
        /// <param name="L2"></param>
        /// <returns></returns>
        public static bool FastContains(LandViewItem L1, LandViewItem L2) {
            //Let's divide this into levels. First let's make two rectangles and see if they contain each other
            Rectangle R1 = LandviewItemBoundingRectangle(L1);
            Rectangle R2 = LandviewItemBoundingRectangle(L2);

            //See if R1 contains R2
            return R1.Contains(R2); 
        }

        /// <summary>Calculates if all points from L2 are in L1</summary>
        /// <param name="L1"></param>
        /// <param name="L2"></param>
        /// <returns></returns>
        public static bool DeepContains(LandViewItem L1, LandViewItem L2) {
            //If it does we still need to check. For contains, we need to check that *every* point in L2 falls in L1
            foreach (Point P in L2.GraphicalPoints) {
                if (!ContainsPoint(L1, P)) { return false; }
            }

            //OK All points from L2 are contained in L1, we can confidently say yes:
            return true;
        }

        /// <summary>Calclates if L1 and L2 intersect</summary>
        /// <param name="L1"></param>
        /// <param name="L2"></param>
        /// <returns></returns>
        public static bool Intersects(LandViewItem L1, LandViewItem L2) {

            //First find if it is contained
            //Actually FastContains does nothing for us. If it's fast contained we can't say its contained, and if it isn't fastcontained (IE if we can say it
            //isn't contained), that doesn't mean it doesn't intersect

            //Something similar to Deepcontains can help us determine if it *does* intersect:

            //Instead of normal deepcontains, let's check if *any* points land in the first landview item's area:
            foreach (Point P in L2.GraphicalPoints) {
                if (ContainsPoint(L1, P)) { return true; }
            }

            //Now do a fast calculation:
            Rectangle R1 = LandviewItemBoundingRectangle(L1);
            Rectangle R2 = LandviewItemBoundingRectangle(L2);

            //If the rectangles do not intersect, then we can be sure that this does not intersect
            if (!R1.IntersectsWith(R2)) { return false; }

            //If they do we need to do the nitty gritty

            //For each line in L1
            for (int i = 0; i < L1.GraphicalPoints.Length-1; i++) {
                //The line is GraphicalPOints[i] to GraphicalPoints[i+1]

                PointF P1 = L1.GraphicalPoints[i];
                PointF P2 = L1.GraphicalPoints[i+1];

                //For each line in L2
                for (int j = 0; j < L2.GraphicalPoints.Length-1; j++) {
                    PointF P3 = L2.GraphicalPoints[j];
                    PointF P4 = L2.GraphicalPoints[j + 1];
                    if (LinesIntersect(P1, P2, P3, P4)) { return true; } //if any lines intersect then we intersect
                }
            }

            return false;
        
        }

        //The following code is taken from http://csharphelper.com/blog/2014/07/determine-whether-a-point-is-inside-a-polygon-in-c/
        #region Contains
        /// <summary>Calculates if L contains P</summary>
        /// <param name="L"></param>
        /// <param name="P"></param>
        /// <returns></returns>
        public static bool ContainsPoint(LandViewItem L, Point P) {
            // Get the angle between the point and the
            // first and last vertices.
            int max_point = L.GraphicalPoints.Length - 1;
            float total_angle = GetAngle(
                L.GraphicalPoints[max_point].X, L.GraphicalPoints[max_point].Y,
                P.X, P.Y,
                L.GraphicalPoints[0].X, L.GraphicalPoints[0].Y);

            // Add the angles from the point
            // to each other pair of vertices.
            for (int i = 0; i < max_point; i++) {
                total_angle += GetAngle(
                    L.GraphicalPoints[i].X, L.GraphicalPoints[i].Y,
                    P.X, P.Y,
                    L.GraphicalPoints[i + 1].X, L.GraphicalPoints[i + 1].Y);
            }

            // The total angle should be 2 * PI or -2 * PI if
            // the point is in the polygon and close to zero
            // if the point is outside the polygon.
            return (Math.Abs(total_angle) > 1);

        }

        ///<summary>Return the angle ABC.
        /// Return a value between PI and -PI.
        /// Note that the value is the opposite of what you might
        /// expect because Y coordinates increase downward.</summary>
        public static float GetAngle(float Ax, float Ay,
            float Bx, float By, float Cx, float Cy) {
            // Get the dot product.
            float dot_product = DotProduct(Ax, Ay, Bx, By, Cx, Cy);

            // Get the cross product.
            float cross_product = CrossProductLength(Ax, Ay, Bx, By, Cx, Cy);

            // Calculate the angle.
            return (float)Math.Atan2(cross_product, dot_product);
        }

        /// <summary>Return the dot product AB · BC.
        /// Note that AB · BC = |AB| * |BC| * Cos(theta).</summary>
        private static float DotProduct(float Ax, float Ay,
            float Bx, float By, float Cx, float Cy) {
            // Get the vectors' coordinates.
            float BAx = Ax - Bx;
            float BAy = Ay - By;
            float BCx = Cx - Bx;
            float BCy = Cy - By;

            // Calculate the dot product.
            return (BAx * BCx + BAy * BCy);
        }

        ///<summary>Return the cross product AB x BC.
        /// The cross product is a vector perpendicular to AB
        /// and BC having length |AB| * |BC| * Sin(theta) and
        /// with direction given by the right-hand rule.
        /// For two vectors in the X-Y plane, the result is a
        /// vector with X and Y components 0 so the Z component
        /// gives the vector's length and direction.</summary>
        public static float CrossProductLength(float Ax, float Ay,
            float Bx, float By, float Cx, float Cy) {
            // Get the vectors' coordinates.
            float BAx = Ax - Bx;
            float BAy = Ay - By;
            float BCx = Cx - Bx;
            float BCy = Cy - By;

            // Calculate the Z coordinate of the cross product.
            return (BAx * BCy - BAy * BCx);
        }

        #endregion

        #region Intersects

        /// <summmary>Find the point of intersection between
        /// the lines p1 --> p2 and p3 --> p4.</summmary>
        public static bool LinesIntersect(
            PointF p1, PointF p2, PointF p3, PointF p4) {
            
            // Get the segments' parameters.
            float dx12 = p2.X - p1.X;
            float dy12 = p2.Y - p1.Y;
            float dx34 = p4.X - p3.X;
            float dy34 = p4.Y - p3.Y;

            // Solve for t1 and t2
            float denominator = (dy12 * dx34 - dx12 * dy34);
            float t1 = ((p1.X - p3.X) * dy34 + (p3.Y - p1.Y) * dx34) / denominator;

            // The lines are parallel (or close enough to it).
            if (float.IsInfinity(t1)) { return false; }

            float t2 = ((p3.X - p1.X) * dy12 + (p1.Y - p3.Y) * dx12) / -denominator;

            // The segments intersect if t1 and t2 are between 0 and 1.
            return ((t1 >= 0) && (t1 <= 1) && (t2 >= 0) && (t2 <= 1));
        }

        #endregion

        /// <summary>Creates a bounding rectangle that contains the given landview item</summary>
        /// <param name="L"></param>
        /// <returns></returns>
        public static Rectangle LandviewItemBoundingRectangle(LandViewItem L) {
            if (L is Road) { throw new ArgumentException("L is a Road"); }
            return new(L.LeftmostX(), L.TopmostY(), L.Width(), L.Height()); 
        }
    }
}
