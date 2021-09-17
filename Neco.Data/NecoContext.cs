using Igtampe.Neco.Common;
using Igtampe.Neco.Common.Contractus;
using Igtampe.Neco.Common.EzTax;
using Igtampe.Neco.Common.EzTax.Subitems;
using Igtampe.Neco.Common.LandView;
using Igtampe.Neco.Common.UMSAT;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Igtampe.Neco.Data {

    /// <summary>Context that has every table. Used for generating database tables and for testing.</summary>
    public class NecoContext: DbContext {

        /// <summary>Creates an EverythingContext</summary>
        public NecoContext() : base() { }

        /// <summary>Overrides onConfiguring to use <see cref="Constants.ConnectionString"/></summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { optionsBuilder.UseSqlServer(Constants.ConnectionString); }

        /// <summary>Overrides on model creation to remove the plural convention</summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            //This will singularize all table names
            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes()) {
                entityType.SetTableName(entityType.DisplayName());
            }
        }//Auth

        /// <summary>View of the Users table that returns <see cref="Common.UserAuth"/></summary>
        public DbSet<UserAuth> UserAuth { get; set; }

        //Checkbook

        /// <summary>Table that contains all checkbook items</summary>
        public DbSet<CheckbookItem> CheckbookItem { get; set; }

        //Neco

        /// <summary>Table of all user types</summary>
        public DbSet<UserType> UserType { get; set; }

        /// <summary>Table of all users</summary>
        public DbSet<User> User { get; set; }

        /// <summary>Table of all transactions</summary>
        public DbSet<Transaction> Transaction { get; set; }

        /// <summary>Table of all Notifications</summary>
        public DbSet<Notification> Notification { get; set; }

        /// <summary>Table of all certified items</summary>
        public DbSet<CertifiedItem> CertifiedItem { get; set; }

        /// <summary>Table of all Bank Account Types</summary>
        public DbSet<BankAccountType> BankAccountType { get; set; }

        /// <summary>Table of all Bank Accounts</summary>
        public DbSet<BankAccount> BankAccount { get; set; }
        
        /// <summary>Table of all Bank Accounts</summary>
        public DbSet<BankAccountDetail> BankAccountDetail { get; set; }

        /// <summary>Table of all Banks</summary>
        public DbSet<Bank> Bank { get; set; }

        //Contract

        /// <summary>Table of all contracts</summary>
        public DbSet<Contract> Contract { get; set; }

        //EzTax

        /// <summary>Table of all EZTax Income Items</summary>
        public DbSet<IncomeItem> IncomeItem { get; set; }

        /// <summary>Table of all Tax Brackets</summary>
        public DbSet<TaxBracket> TaxBracket { get; set; }

        /// <summary>Table of all Tax Jurisdictions</summary>
        public DbSet<TaxJurisdiction> TaxJurisdiction { get; set; }

        /// <summary>Table of all Tax User Infos</summary>
        public DbSet<TaxUserInfo> TaxUserInfo { get; set; }

        /// <summary>Table of all Apartments</summary>
        public DbSet<Apartment> Apartment { get; set; }

        /// <summary>Table of all Businesses</summary>
        public DbSet<Business> Business { get; set; }

        /// <summary>Table of all hotels</summary>
        public DbSet<Hotel> Hotel { get; set; }

        /// <summary>Table of all Tax Reports</summary>
        public DbSet<TaxReport> Report { get; set; }

        //LandView

        /// <summary>Table of all Countries</summary>
        public DbSet<Country> Country { get; set; }

        /// <summary>Table of all Districts</summary>
        public DbSet<District> District { get; set; }

        /// <summary>Table of all Plots</summary>
        public DbSet<Plot> Plot { get; set; }

        //UMSAT
        /// <summary>Table of all Assets</summary>
        public DbSet<Asset> Asset { get; set; }

    }
}
