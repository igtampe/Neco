using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Igtampe.Neco.Common.LandView;

namespace Igtampe.LandViewPlotter {
    public static class LandViewUtils {

        /// <summary>Validates a Points string for use on a <see cref="Plot"/>,<see cref="District"/>, or a <see cref="Road"/></summary>
        /// <param name="Points"></param>
        /// <param name="MinLength">Minimum number of points. 2 for roads, 3 for plots and districts</param>
        /// <returns></returns>
        public static bool ValidatePoints(string Points, int MinLength) { 

            //Ensure blank ones can be actually saved.
            if (Points.Length == 0) { return true; }
            if (Points.Length < MinLength) { return false; }

            //First let's split this by semicolon
            foreach (string ppair in Points.Split(";")) {
                //Split it again by colon
                string[] ppairsplit = ppair.Split(",");
                if (ppairsplit.Count() != 2) { return false; } //If we have more than three coords then no
                if (!int.TryParse(ppairsplit[0], out int _)) { return false; } //Test to ensure both can be
                if (!int.TryParse(ppairsplit[1], out int _)) { return false; } //parsed as ints
            }
            return true;
        }

    }
}
