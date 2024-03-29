﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Igtampe.Neco.Common.Income {

    /// <summary>An apartment complex</summary>
    public class Apartment : IncomeItem {

        /// <summary>Type of this income item. Helps the frontend determine what this is</summary>
        [NotMapped]
        public override int Type { get; set; } = 0;

        /// <summary>Studio units in this appartment</summary>
        [Range(0, int.MaxValue)]
        public int SUnits { get; set; } = 0;

        /// <summary>1 Bedroom units in this appartment</summary>
        [Range(0, int.MaxValue)]
        public int B1Units { get; set; } = 0;

        /// <summary>2 Bedroom units in this appartment</summary>
        [Range(0, int.MaxValue)]
        public int B2Units { get; set; } = 0;

        /// <summary>3 Bedroom units in this appartment</summary>
        [Range(0, int.MaxValue)]
        public int B3Units { get; set; } = 0;

        /// <summary>Penthouse units in this appartment</summary>
        [Range(0, int.MaxValue)]
        public int PUnits { get; set; } = 0;

        /// <summary>Studio monthly rent in this appartment</summary>
        [Range(0, int.MaxValue)]
        public int SRent { get; set; } = 500;

        /// <summary>1 Bedroom monthly rent in this appartment</summary>
        [Range(0, int.MaxValue)]
        public int B1Rent { get; set; } = 750;

        /// <summary>2 Bedroom monthly rent in this appartment</summary>
        [Range(0, int.MaxValue)]
        public int B2Rent { get; set; } = 1000;

        /// <summary>3 Bedroom monthly rent in this appartment</summary>
        [Range(0, int.MaxValue)]
        public int B3Rent { get; set; } = 1250;

        /// <summary>Penthouse rent in this appartment</summary>
        [Range(0, int.MaxValue)]
        public int PRent { get; set; } = 1250;

        /// <summary>Income of this appartment</summary>
        /// <returns>(SRent * SUnits) + (B1Rent * B1Units) + (B2Rent * B2Units) + (B3Rent * B3Units) + (PRent * PUnits)</returns>
        public override long Income() => (SRent * SUnits) + (B1Rent * B1Units) + (B2Rent * B2Units) + (B3Rent * B3Units) + (PRent * PUnits);

    }
}
