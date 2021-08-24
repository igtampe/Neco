using Microsoft.EntityFrameworkCore;
using Igtampe.Neco.Common;

namespace Igtampe.Neco.Data {

    /// <summary>Context used for Authorization</summary>
    public class AuthContext: DbContext {

        /// <summary>Creates an AuthContext</summary>
        public AuthContext() : base() { }

        /// <summary>Overrides onConfiguring to use <see cref="Constants.ConnectionString"/></summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { optionsBuilder.UseSqlServer(Constants.ConnectionString); }

        /// <summary>A view of the Users Table that returns <see cref="UserAuth"/></summary>
        public DbSet<UserAuth> Users { get; set; }

    }
}
