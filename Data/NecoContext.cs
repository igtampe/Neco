using Microsoft.EntityFrameworkCore;
using Igtampe.Neco.Common;
using Igtampe.Neco.Common.Taxes;
using Igtampe.Neco.Common.Income;
using Igtampe.Neco.Common.Banking;
using Igtampe.Neco.Common.Assets;

namespace Igtampe.Neco.Data {

    /// <summary>DBContext to get all objects relating to Neco from the DB</summary>
    public class NecoContext : PostgresContext {

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            //Configure the thing

            modelBuilder.Entity<Jurisdiction>()
                .HasMany(J => J.AccountsLocatedIn)
                .WithOne(A => A.Jurisdiction);
                
            base.OnModelCreating(modelBuilder);
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        //This Pragma is disabled because all of these dbsets will work once configured which is by the time anything's going to use it :)

        public DbSet<User> User { get; set; }

        public DbSet<Bracket> Bracket { get; set; }

        public DbSet<Jurisdiction> Jurisdiction { get; set; }

        public DbSet<TaxReport> TaxReport { get; set; }

        public DbSet<IncomeItem> IncomeItem { get; set; }

        public DbSet<Airline> Airline { get; set; }
        
        public DbSet<Apartment> Apartment { get; set; }

        public DbSet<Business> Business { get; set; }

        public DbSet<Corporation> Corporation { get; set; }

        public DbSet<Hotel> Hotel { get; set; }

        public DbSet<Account> Account { get; set; }

        public DbSet<Bank> Bank { get; set; }

        public DbSet<Transaction> Transaction { get; set; }

        public DbSet<Notification> Notification { get; set; }

        public DbSet<Image> Image { get; set; }

        public DbSet<Asset> Asset { get; set; }

        public DbSet<Building> Building { get; set; }

        public DbSet<Unit> Unit { get; set; }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    }
}
