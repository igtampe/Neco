using Microsoft.EntityFrameworkCore;
using Igtampe.Neco.Common.EzTax;
using Igtampe.Neco.Common.EzTax.Subitems;

namespace Igtampe.Neco.Data {

    /// <summary>Context for handling EzTax Items</summary>
    public class EzTaxContext:DbContext {

        /// <summary>Creates an EzTax Context</summary>
        public EzTaxContext() : base() { }

        /// <summary>Overrides onConfiguring to use <see cref="Constants.ConnectionString"/></summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { optionsBuilder.UseSqlServer(Constants.ConnectionString); }

        /// <summary>Table of all EzTax income items</summary>
        public DbSet<IncomeItem> IncomeItems { get; set; }
        
        /// <summary>Table of all Tax Brackets</summary>
        public DbSet<TaxBracket> TaxBrackets { get; set; }

        /// <summary>Table of all Tax Jurisdictions</summary>
        public DbSet<TaxJurisdiction> TaxJurisdictions { get; set; }

        /// <summary>Table of all Tax User Infos</summary>
        public DbSet<TaxUserInfo> TaxUserInfos { get; set; }
        
        /// <summary>Table of all Apartments</summary>
        public DbSet<Apartment> Apartments { get; set; }

        /// <summary>Table of all Businesses</summary>
        public DbSet<Business> Businesses { get; set; }

        /// <summary>Table of all Hotels</summary>
        public DbSet<Hotel> Hotels { get; set; }

    }
}
