﻿using System.ComponentModel.DataAnnotations;
using Igtampe.Neco.Common.Income;

namespace Igtampe.Neco.API.Requests {

    /// <summary>An apartment complex</summary>
    public class ApartmentRequest : IncomeItemRequest<Apartment> {

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

    }
}
