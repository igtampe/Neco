using Microsoft.EntityFrameworkCore;
using Igtampe.Neco.Common;
using Igtampe.Neco.Common.Contractus;
using Igtampe.Neco.Common.EzTax;
using Igtampe.Neco.Common.EzTax.Subitems;
using Igtampe.Neco.Common.LandView;
using Igtampe.Neco.Common.UMSAT;


namespace Igtampe.Neco.Data {

    /// <summary>Context that has every table. Used for generating database tables and for testing.</summary>
    public class EverythingContext:DbContext {

        /// <summary>Creates an EverythingContext</summary>
        public EverythingContext() : base() { }

        /// <summary>Overrides onConfiguring to use <see cref="Constants.ConnectionString"/></summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { optionsBuilder.UseSqlServer(Constants.ConnectionString);}

        //Auth

        /// <summary>View of the Users table that returns <see cref="UserAuth"/></summary>
        public DbSet<UserAuth> UserAuths { get; set; }

        //Neco

        /// <summary>Table of all user types</summary>
        public DbSet<UserType> UserTypes { get; set; }

        /// <summary>Table of all users</summary>
        public DbSet<User> Users { get; set; }

        /// <summary>Table of all transactions</summary>
        public DbSet<Transaction> Transactions { get; set; }

        /// <summary>Table of all Notifications</summary>
        public DbSet<Notification> Notifications { get; set; }

        /// <summary>Table of all certified items</summary>
        public DbSet<CertifiedItem> CertifiedItems { get; set; }

        /// <summary>Table of all Bank Account Types</summary>
        public DbSet<BankAccountType> BankAccountTypes { get; set; }
        
        /// <summary>Table of all Bank Accounts</summary>
        public DbSet<BankAccount> BankAccounts { get; set; }

        /// <summary>Table of all Banks</summary>
        public DbSet<Bank> Banks { get; set; }

        //Contract

        /// <summary>Table of all contracts</summary>
        public DbSet<Contract> Contracts { get; set; }

        //EzTax

        /// <summary>Table of all EZTax Income Items</summary>
        public DbSet<IncomeItem> IncomeItems { get; set; }

        /// <summary>Table of all Tax Brackets</summary>
        public DbSet<TaxBracket> TaxBrackets { get; set; }
        
        /// <summary>Table of all Tax Jurisdictions</summary>
        public DbSet<TaxJurisdiction> TaxJurisdictions { get; set; }

        /// <summary>Table of all Tax User Infos</summary>
        public DbSet<TaxUserInfo> TaxUserInfos { get; set; }

        /// <summary>Table of all Apartments</summary>
        public DbSet<Apartment> Appartments { get; set; }

        /// <summary>Table of all Businesses</summary>
        public DbSet<Business> Businesses { get; set; }

        /// <summary>Table of all hotels</summary>
        public DbSet<Hotel> Hotels { get; set; }

        //LandView

        /// <summary>Table of all Countries</summary>
        public DbSet<Country> Countries { get; set; }

        /// <summary>Table of all Districts</summary>
        public DbSet<District> Districts { get; set; }

        /// <summary>Table of all Plots</summary>
        public DbSet<Plot> Plots { get; set; }

        //UMSAT
        /// <summary>Table of all Assets</summary>
        public DbSet<Asset> Assets { get; set; }

    }
}
