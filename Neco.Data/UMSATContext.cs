using Microsoft.EntityFrameworkCore;
using Igtampe.Neco.Common.UMSAT;

namespace Igtampe.Neco.Data {

    /// <summary>Context to use to handle all UMSAT items</summary>
    public class UMSATContext: DbContext {

        /// <summary>Creates an UMSAT Context</summary>
        public UMSATContext() : base() { }

        /// <summary>Overrides onConfiguring to use <see cref="Constants.ConnectionString"/></summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { optionsBuilder.UseSqlServer(Constants.ConnectionString); }

        /// <summary>Table of all UMSAT Assets</summary>
        public DbSet<Asset> Assets { get; set; }

    }
}
