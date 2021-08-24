using Microsoft.EntityFrameworkCore;
using Igtampe.Neco.Common;

namespace Igtampe.Neco.Data {

    /// <summary>Context for use of main Neco operations and items</summary>
    public class NecoContext:DbContext {

        /// <summary>Creates a NecoContext</summary>
        public NecoContext() : base() { }

        /// <summary>Overrides onConfiguring to use <see cref="Constants.ConnectionString"/></summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { optionsBuilder.UseSqlServer(Constants.ConnectionString); }

        /// <summary>Table of all User Types</summary>
        public DbSet<UserType> UserTypes { get; set; }

        /// <summary>Table of all users</summary>
        public DbSet<User> Users { get; set; }

        /// <summary>Table of all transactions</summary>
        public DbSet<Transaction> Transactions { get; set; }
        
        /// <summary>Table of all Notifications</summary>
        public DbSet<Notification> Notifications { get; set; }
        
        /// <summary>Table of all Certified Items </summary>
        public DbSet<CertifiedItem> CertifiedItems { get; set; }

        /// <summary>Table of all Bank Account Types</summary>
        public DbSet<BankAccountType> BankAccountTypes { get; set; }

        /// <summary>Table of all Bank Accounts</summary>
        public DbSet<BankAccount> BankAccounts { get; set; }

        /// <summary>Table of all Banks</summary>
        public DbSet<Bank> Banks { get; set; }

    }
}
