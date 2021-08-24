﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Igtampe.Neco.Common.EzTax.Subitems {

    /// <summary>Business subitem that contains details for a business of some kind (Restaurant or store)</summary>
    public class Business:IIncomeSubitem {

        /// <summary>ID of this business</summary>
        public Guid ID { get; set; }

        /// <summary>Head IncomeItem</summary>
        public IncomeItem IncomeItem { get; set; }
        
        /// <summary>Name of this Business</summary>
        public string Name { get; set; } = "";

        /// <summary>Chairs in this business. 1 if the business is a store</summary>
        [Range(0, int.MaxValue)]
        public int Chairs { get; set; } = 0;

        /// <summary>Average spending of a customer in this business</summary>
        [Range(0, int.MaxValue)]
        public int AvgSpend { get; set; } = 0;

        /// <summary>Average customers per hour in this business</summary>
        [Range(0, int.MaxValue)]
        public int CustPerHour { get; set; } = 0;

        /// <summary>Hours this business is open a day (should be between 0 and 24)</summary>
        [Range(0, int.MaxValue)]
        public int HoursOpen { get; set; } = 0;

        /// <summary>Income of this business. Assumes 50% Proffit</summary>
        /// <returns>(((AvgSpend / 2) * CustPerHour * HoursOpen) * Chairs) * 30</returns>
        public int Income() { return (((AvgSpend / 2) * CustPerHour * HoursOpen) * Chairs) * 30; }

    }
}