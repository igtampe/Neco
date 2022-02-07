﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Igtampe.Neco.Common.Income {

    /// <summary>An Airline that has gates</summary>
    public class Airline:Corporation {

        /// <summary>Type of this income item. Helps the frontend determine what this is</summary>
        [NotMapped]
        public override int Type { get; set; } = 4;

        /// <summary>Number of active small gates this airline has</summary>
        [Range(0, int.MaxValue)]
        public int GatesSM { get; set; } = 0;

        /// <summary>Number of active medium gates this airline has</summary>
        [Range(0, int.MaxValue)]
        public int GatesMD { get; set; } = 0;

        /// <summary>Number of active large gates this airline has</summary>ad
        [Range(0, int.MaxValue)]
        public int GatesLG { get; set; } = 0;

        /// <summary>Gets corporate income percentage of RLE</summary>
        /// <returns></returns>
        protected override double GetIncomePercentage()=> base.GetIncomePercentage() + (GatesSM * .002) + (GatesMD * .004) + (GatesLG * .005); 

    }
}
