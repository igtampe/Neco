using Microsoft.EntityFrameworkCore;
using Igtampe.Neco.Common.Contractus;

namespace Igtampe.Neco.Data {

    /// <summary>Context for handling Contractus items</summary>
    public class ContractusContext: DbContext {

        /// <summary>Creates a contractus context</summary>
        public ContractusContext() : base() { }

        /// <summary>Overrides onConfiguring to use <see cref="Constants.ConnectionString"/></summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { optionsBuilder.UseSqlServer(Constants.ConnectionString); }

        /// <summary>Table which holds all contracts</summary>
        public DbSet<Contract> Contracts { get; set; }

    }
}
