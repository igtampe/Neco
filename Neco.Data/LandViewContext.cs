using Microsoft.EntityFrameworkCore;
using Igtampe.Neco.Common.LandView;

namespace Igtampe.Neco.Data {
    /// <summary>Context to use to handle LandView Items</summary>
    public class LandViewContext: DbContext {

        /// <summary>Creates a LandView Context</summary>
        public LandViewContext() : base() { }

        /// <summary>Overrides onConfiguring to use <see cref="Constants.ConnectionString"/></summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { optionsBuilder.UseSqlServer(Constants.ConnectionString); }

        /// <summary>Table of all countries</summary>
        public DbSet<Country> Countries { get; set; }

        /// <summary>Table of all Districts</summary>
        public DbSet<District> Districts { get; set; }

        /// <summary>Table of all plots</summary>
        public DbSet<Plot> Plots { get; set; }

    }
}
