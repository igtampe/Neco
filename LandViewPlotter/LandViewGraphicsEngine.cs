using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Igtampe.Neco.Common;
using Igtampe.Neco.Common.LandView;

namespace Igtampe.LandViewPlotter {

    /// <summary>Holds functiosn to generate and draw images and graphcis for landview items</summary>
    public static class LandViewGraphicsEngine {

        private static readonly FontFamily FFamily = new("Arial");


        /// <summary>Creates an image with a country, all its districts, and all its roads</summary>
        /// <param name="C"></param>
        /// <returns></returns>
        public static Image GenerateDetailedCountryImage(Country C) {

            Image I = GenerateCountryImage(C);
            Graphics GRM = Graphics.FromImage(I);
            Point Origin = GetCountryOrigin(C);
            foreach (District D in C.Districts) {
                DrawPlots(D, GRM, Origin);
            }

            return I;
        }

        /// <summary>Creates an image with a Country and all its districts and roads</summary>
        /// <param name="C"></param>
        /// <returns></returns>
        public static Image GenerateCountryImage(Country C) {
            try {
                if (C.Width == 0 || C.Height == 0) { return GenerateErrorImage($"Country {C.Name} ({C.ID})\nHas a width or height of 0 and cannot be drawn."); }
                Image I = GenerateCanvas(C);

                //Find the point that's 0,0
                Point Origin = GetCountryOrigin(C);

                //Draw a little cross at the center
                Graphics GRM = Graphics.FromImage(I);

#if (DEBUG)
                DrawOriginCrosshair(Origin, GRM);
#endif

                //Draw each district

                DrawDistricts(C, GRM, Origin);
                DrawRoads(C, GRM, Origin);

                return I;
            } catch (Exception E) {
                return GenerateErrorImage($"{E.Source} at {E.TargetSite}:\n{E.Message}\n{E.StackTrace}");
            }

        }

        /// <summary>Creates an image with a district and all its plots</summary>
        /// <param name="D"></param>
        /// <returns></returns>
        public static Image GenerateDistrictImage(District D) {
            try {
                if (D.Width() == 0 || D.Height() == 0) { return GenerateErrorImage($"District {D.Name} ({D.ID})\nHas a width or height of 0 and cannot be drawn."); }
                if (!LandViewUtils.ValidatePoints(D.Points, 3)) { return GenerateErrorImage($"District {D.Name} ({D.ID})\nHas an invalid Points field and cannot be drawn"); }
                Image I = GenerateCanvas(D);

                //Find the point that's 0,0
                Point Origin = GetDistrictOrigin(D);

                //Draw a little cross at the center
                Graphics GRM = Graphics.FromImage(I);
#if (DEBUG)
                DrawOriginCrosshair(Origin, GRM);
#endif
                DrawDistrictOutline(D, GRM, Origin, new(new SolidBrush(Color.Black), 10));
                DrawPlots(D, GRM, Origin);
                DrawRoads(D.Country, GRM, Origin);

                return I;
            } catch (Exception E) {
                return GenerateErrorImage($"{E.Source} at {E.TargetSite}:\n{E.Message}\n{E.StackTrace}");
            }
        }

        /// <summary>Generates an image with a Plot</summary>
        /// <param name="P"></param>
        /// <returns></returns>
        public static Image GeneratePlotImage(Plot P) {
            try {
                if (P.Width() == 0 || P.Height() == 0) { return GenerateErrorImage($"Plot {P.Name} ({P.ID})\nHas a width or height of 0 and cannot be drawn."); }
                if (!LandViewUtils.ValidatePoints(P.Points,3)) { return GenerateErrorImage($"Plot {P.Name} ({P.ID})\nHas an invalid Points field and cannot be drawn"); }
                Image I = GenerateCanvas(P);

                Graphics GRM = Graphics.FromImage(I);
                Point Origin = GetPlotOrigin(P);
                Pen PlotPen = new(Color.Black, 5);
                Font NameFont = new(FFamily, 12, FontStyle.Bold, GraphicsUnit.Point);
                Font InfoFont = new(FFamily, 8, FontStyle.Regular, GraphicsUnit.Point);
                DrawPlot(P, GRM, Origin, PlotPen, Color.Black, Color.White, NameFont, InfoFont);

                return I;
            } catch (Exception E) {
                return GenerateErrorImage($"{E.Source} at {E.TargetSite}:\n{E.Message}\n{E.StackTrace}");
            }
        }

        #region District

        /// <summary>Draws the outline of a given district</summary>
        /// <param name="D">District to draw</param>
        /// <param name="GRM">Graphcis to draw onto</param>
        /// <param name="Origin">Point that defines 0,0 on the given graphcis</param>
        /// <param name="DistrictPen">Pen to draw the outline with</param>
        public static void DrawDistrictOutline(District D, Graphics GRM, Point Origin, Pen DistrictPen) {
            for (int i = 0; i < D.GraphicalPoints.Length - 1; i++) {
                GRM.DrawLine(DistrictPen, OffsetPoint(Origin, D.GraphicalPoints[i]), OffsetPoint(Origin, D.GraphicalPoints[i + 1]));
            }

            //Draw a line from the last point to the first point
            GRM.DrawLine(DistrictPen, OffsetPoint(Origin, D.GraphicalPoints.First()), OffsetPoint(Origin, D.GraphicalPoints.Last()));
        }

        /// <summary>Draws a district label for a given district</summary>
        /// <param name="D">District to draw</param>
        /// <param name="GRM">Graphcis to draw onto</param>
        /// <param name="Origin">Point that defines 0,0 on the given graphics</param>
        /// <param name="Background">Background color of the label</param>
        /// <param name="Foreground">Foreground color of the label</param>
        /// <param name="NameFont">Font used to draw the name</param>
        /// <param name="InfoFont">Font used to draw the info</param>
        public static void DrawDistrictLabel(District D, Graphics GRM, Point Origin, Color Background, Color Foreground, Font NameFont, Font InfoFont) {
            //Find the center of this thing
            Point DCenter = new(
                D.LeftmostX() + Origin.X + D.Width() / 2,
                D.TopmostY() + Origin.Y + D.Height() / 2);

            DrawLabel(GRM, DCenter, D.Name, $"{D.Plots.Count} Plot(s) | {D.Area():n0}m²", Foreground, Background, NameFont, InfoFont, 2, 4);
        }

        /// <summary>Draws a district outline and label for the given district</summary>
        /// <param name="D">District to draw</param>
        /// <param name="GRM">Graphics to Draw onto</param>
        /// <param name="Origin">Point that defines 0,0 on the given graphics</param>
        /// <param name="DistrictPen">Pen to draw the outline with</param>
        /// <param name="LabelBackground">Background color of the Label</param>
        /// <param name="LabelForeground">Foreground color of the label</param>
        /// <param name="NameFont">Font used to draw the name</param>
        /// <param name="InfoFont">Font used to draw information</param>
        public static void DrawDistrict(District D, Graphics GRM, Point Origin, Pen DistrictPen, Color LabelBackground, Color LabelForeground, Font NameFont, Font InfoFont) {
            DrawDistrictOutline(D, GRM, Origin, DistrictPen);
            DrawDistrictLabel(D, GRM, Origin, LabelBackground, LabelForeground, NameFont, InfoFont);
        }

        /// <summary>Draws all districts in a country</summary>
        /// <param name="C">Country with all the districts to draw</param>
        /// <param name="GRM">Graphics to draw to</param>
        /// <param name="Origin">Point of the given graphics that defines 0,0</param>
        public static void DrawDistricts(Country C, Graphics GRM, Point Origin) {
            Pen DistrictPen = new(Color.Black, 10);
            Font NameFont = new(FFamily, 24, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            Font InfoFont = new(FFamily, 12, FontStyle.Regular, GraphicsUnit.Point);
            Color LabelForeground = Color.White;
            Color LabelBackground = Color.Black;

            foreach (District D in C.Districts) {
                DrawDistrict(D, GRM, Origin, DistrictPen, LabelBackground, LabelForeground, NameFont, InfoFont);
            }
        }

        #endregion

        #region Plot

        /// <summary>Draws the outline of a given Plot</summary>
        /// <param name="P">Plot to draw</param>
        /// <param name="GRM">Graphcis to draw onto</param>
        /// <param name="Origin">Point that defines 0,0 on the given graphcis</param>
        /// <param name="PlotPen">Pen to draw the outline with</param>
        public static void DrawPlotOutline(Plot P, Graphics GRM, Point Origin, Pen PlotPen) {
            for (int i = 0; i < P.GraphicalPoints.Length - 1; i++) {
                GRM.DrawLine(PlotPen, OffsetPoint(Origin, P.GraphicalPoints[i]), OffsetPoint(Origin, P.GraphicalPoints[i + 1]));
            }

            //Draw a line from the last point to the first point
            GRM.DrawLine(PlotPen, OffsetPoint(Origin, P.GraphicalPoints.First()), OffsetPoint(Origin, P.GraphicalPoints.Last()));
        }

        /// <summary>Draws a Plot label for a given plot</summary>
        /// <param name="P">Plot to draw</param>
        /// <param name="GRM">Graphcis to draw onto</param>
        /// <param name="Origin">Point that defines 0,0 on the given graphics</param>
        /// <param name="Background">Background color of the label</param>
        /// <param name="Foreground">Foreground color of the label</param>
        /// <param name="NameFont">Font used to draw the name</param>
        /// <param name="InfoFont">Font used to draw the info</param>
        public static void DrawPlotLabel(Plot P, Graphics GRM, Point Origin, Color Background, Color Foreground, Font NameFont, Font InfoFont) {
            //Find the center of this thing
            Point PCenter = new(
                P.LeftmostX() + Origin.X + P.Width() / 2,
                P.TopmostY() + Origin.Y + P.Height() / 2);

            //We're going to write some TEXT!
            DrawLabel(GRM, PCenter, P.Name, $"{P.Area():n0}m²", Foreground, Background, NameFont, InfoFont, 2, 4);
        }

        /// <summary>Draws a plot outline and label for the given plot</summary>
        /// <param name="P">Plot to draw</param>
        /// <param name="GRM">Graphics to Draw onto</param>
        /// <param name="Origin">Point that defines 0,0 on the given graphics</param>
        /// <param name="PlotPen">Pen to draw the outline with</param>
        /// <param name="LabelBackground">Background color of the Label</param>
        /// <param name="LabelForeground">Foreground color of the label</param>
        /// <param name="NameFont">Font used to draw the name</param>
        /// <param name="InfoFont">Font used to draw information</param>
        public static void DrawPlot(Plot P, Graphics GRM, Point Origin, Pen PlotPen, Color LabelBackground, Color LabelForeground, Font NameFont, Font InfoFont) {
            DrawPlotOutline(P, GRM, Origin, PlotPen);
            if (P.Width() < 50 || P.Height() < 50) { return; } //if it's too small, don't make the label
            DrawPlotLabel(P, GRM, Origin, LabelBackground, LabelForeground, NameFont, InfoFont);
        }

        /// <summary>Draws all plots in a district</summary>
        /// <param name="D">District with all the plots to draw</param>
        /// <param name="GRM">Graphics to draw to</param>
        /// <param name="Origin">Point of the given graphics that defines 0,0</param>
        public static void DrawPlots(District D, Graphics GRM, Point Origin) {
            Pen PlotPen = new(Color.Black, 5);
            Font NameFont = new(FFamily, 12, FontStyle.Bold, GraphicsUnit.Point);
            Font InfoFont = new(FFamily, 8, FontStyle.Regular, GraphicsUnit.Point);
            //Draw each plot
            foreach (Plot P in D.Plots) {
                DrawPlot(P, GRM, Origin, PlotPen, Color.Black, Color.White, NameFont, InfoFont);
            }
        }

        #endregion

        #region Road

        /// <summary>Draws a single road onto a graphics</summary>
        /// <param name="R">Road to draw</param>
        /// <param name="GRM">Graphics to draw with</param>
        /// <param name="Origin">Point on the given graphcis that represents 0,0</param>
        /// <param name="C">Color to draw the road in</param>
        public static void DrawRoad(Road R, Graphics GRM, Point Origin, Color C) {
            Pen RoadPen = new(C, R.Width);
            for (int i = 0; i < R.GraphicalPoints.Length - 1; i++) {
                GRM.DrawLine(RoadPen, OffsetPoint(Origin, R.GraphicalPoints[i]), OffsetPoint(Origin, R.GraphicalPoints[i + 1]));
            }
        }

        /// <summary>Draws roads onto a graphics</summary>
        /// <param name="C">Country to get all the roads from</param>
        /// <param name="GRM">Graphics to draw with</param>
        /// <param name="Origin">Point on the given graphcis  that represents 0,0</param>
        public static void DrawRoads(Country C, Graphics GRM, Point Origin) {
            //Draw each road
            foreach (Road R in C.Roads) { DrawRoad(R, GRM, Origin, Color.DarkGray); }
        }

        #endregion

        #region Utilities

        /// <summary>Generates a blank canvas the size of the given country</summary>
        /// <param name="C"></param>
        /// <returns></returns>
        public static Image GenerateCanvas(ILandViewItem L) {
            if (L.Width() == 0 || L.Height() == 0) { return new Bitmap(1, 1); }
            Image I = new Bitmap(L.Width(), L.Height());
            Graphics GRM = Graphics.FromImage(I);
            GRM.FillRectangle(new SolidBrush(Color.White), 0, 0, I.Width, I.Height);
            GRM.Dispose();
            return I;
        }

        /// <summary>Returns a point that represents 0,0 for a given country's Graphics object</summary>
        /// <param name="C"></param>
        /// <returns></returns>
        public static Point GetCountryOrigin(Country C) { return new(C.Width / 2, C.Height / 2); }

        /// <summary>Returns a point that represents 0,0 for a given District's graphics object</summary>
        /// <param name="D"></param>
        /// <returns></returns>
        public static Point GetDistrictOrigin(District D) { return new(-D.LeftmostX(), -D.TopmostY()); }

        /// <summary>Returns a point that represents 0,0 for a given Plot's graphics</summary>
        /// <param name="P"></param>
        /// <returns></returns>
        public static Point GetPlotOrigin(Plot P) { return new(-P.LeftmostX(), -P.TopmostY()); }

        /// <summary>Draws a small crosshair on where the origin is</summary>
        /// <param name="Origin">Point that represents 0,0 on the given graphics</param>
        /// <param name="GRM">Graphics to draw the crosshair onto</param>
        public static void DrawOriginCrosshair(Point Origin, Graphics GRM) {
            Pen P = new(Color.Black, 3);

            GRM.DrawLine(P, OffsetPoint(Origin, 0, -6), OffsetPoint(Origin, 0, 6));
            GRM.DrawLine(P, OffsetPoint(Origin, -6, 0), OffsetPoint(Origin, 6, 0));
        }

        /// <summary>Draws a two line, two font label centered on the given point, on the given Graphics</summary>
        /// <param name="GRM">Graphics object to draw on</param>
        /// <param name="LabelCenter">Point to center the label on</param>
        /// <param name="NameLabel">String to write on the first line</param>
        /// <param name="InfoLabel">String to write on the second line</param>
        /// <param name="NameFont">Font for the first line</param>
        /// <param name="InfoFont">Font for the second line</param>
        /// <param name="Padding">Padding around the label</param>
        /// <param name="Spacing">Spacing between the first and second line of text</param>
        public static void DrawLabel(Graphics GRM, Point LabelCenter, string NameLabel, string InfoLabel, Color Foreground, Color Background, Font NameFont, Font InfoFont, int Padding, int Spacing) {
            SizeF NameSize = GRM.MeasureString(NameLabel, NameFont);
            SizeF InfoSize = GRM.MeasureString(InfoLabel, InfoFont);

            Size CardSize = new(Convert.ToInt32(Math.Max(NameSize.Width, InfoSize.Width)) + (2 * Padding),
                                        Convert.ToInt32(NameSize.Height + InfoSize.Height) + Spacing + (2 * Padding));

            //Get the point at which to draw this thing
            Point LabelDrawPoint = OffsetPoint(LabelCenter, CardSize.Width / -2, CardSize.Height / -2);

            //Draw a rectangle in black
            GRM.FillRectangle(new SolidBrush(Background), LabelDrawPoint.X, LabelDrawPoint.Y, CardSize.Width, CardSize.Height);

            //Draw the Label
            GRM.DrawString(NameLabel, NameFont, new SolidBrush(Foreground), OffsetPoint(LabelDrawPoint, Padding, Padding));

            //Draw the PlotLabel
            GRM.DrawString(InfoLabel, InfoFont, new SolidBrush(Foreground), OffsetPoint(LabelDrawPoint, Padding, Padding + Convert.ToInt32(NameSize.Height) + Spacing));

            //Now we're done
        }

        /// <summary>Offsets a point by the coordinates in a different point</summary>
        /// <param name="P"></param>
        /// <param name="OtherP"></param>
        /// <returns>A copy of the point offset by the other point's x and y</returns>
        public static Point OffsetPoint(Point P, Point OtherP) { return OffsetPoint(P, OtherP.X, OtherP.Y); }

        /// <summary>Offsets a point by a given DX and DY</summary>
        /// <param name="P"></param>
        /// <param name="DX"></param>
        /// <param name="DY"></param>
        /// <returns>Copy of the original point, offset by the given amount</returns>
        public static Point OffsetPoint(Point P, int DX, int DY) {
            Point P2 = new(P.X, P.Y); //create a duplicate
            P2.Offset(DX, DY); //Offset it
            return P2; //return it
        }

        public static Image GenerateErrorImage(string Error) {

            Image E = new Bitmap(Properties.Resources.LandviewGenerationError);
            Graphics GRM = Graphics.FromImage(E);


            Font NameFont = new(FFamily, 14, FontStyle.Regular, GraphicsUnit.Point);

            GRM.FillRectangle(new SolidBrush(Color.Black), 87, 408, 852, 260);
            GRM.DrawString(Error, NameFont, new SolidBrush(Color.White), 90, 411);

            return E;

        }

        #endregion

    }
}
