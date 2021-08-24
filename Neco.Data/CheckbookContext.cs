using Microsoft.EntityFrameworkCore;
using Igtampe.Neco.Common;

namespace Igtampe.Neco.Data {
    
    /// <summary>Context for handling Checkbook2000 items </summary>
    public class CheckbookContext: DbContext {

        /// <summary>Creates a checkbook context</summary>
        public CheckbookContext() : base() { }

        /// <summary>Overrides onConfiguring to use <see cref="Constants.ConnectionString"/></summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { optionsBuilder.UseSqlServer(Constants.ConnectionString); }

        /// <summary>Table that contains all checkbook items</summary>
        public DbSet<CheckbookItem> CheckbookItems { get; set; }

    }
}
