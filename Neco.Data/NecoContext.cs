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

        /// <summary>Indicates whether or not this neco context is in Postgres Mode</summary>
        public bool PostgresMode { get; private set; } = false;

        /// <summary>Indicates whether or not to force no postgres</summary>
        private readonly bool ForceSQLServer = false;

        /// <summary>Override for SQL Server URL. <b></b></summary>
        private readonly string SQLServerURL;

        /// <summary>Override for Postgres server URL. <b></b></summary>
        private readonly string PostgresURL;

        /// <summary>Creates an EverythingContext</summary>
        public NecoContext() : base() { }

        /// <summary>Creates an EverythingContext</summary>
        public NecoContext(string SQLServerURL) : base() {
            this.SQLServerURL = SQLServerURL;
        }

        /// <summary>Creates an EverythingContext</summary>
        public NecoContext(string SQLServerURL, string PostgresURL) : base() {
            this.SQLServerURL = SQLServerURL;
            this.PostgresURL = PostgresURL;
        }

        /// <summary>Creates an EverythingContext</summary>
        public NecoContext(bool ForceSQLServer) : base() {
            this.ForceSQLServer = ForceSQLServer;
        }

        /// <summary>Creates an EverythingContext</summary>
        public NecoContext(bool ForceSQLServer, string SQLServerURL) : base() {
            this.ForceSQLServer = ForceSQLServer;
            this.SQLServerURL = SQLServerURL;
        }

        /// <summary>Overrides onConfiguring to use <see cref="Constants.ConnectionString"/></summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {

            string PURL = string.IsNullOrWhiteSpace(PostgresURL) ? System.Environment.GetEnvironmentVariable("DATABASE_URL") : PostgresURL;

            if (!string.IsNullOrWhiteSpace(PURL) && !ForceSQLServer) {

                //OK so now we have this
                //postgres://user:password@host:port/database

                //Drop the beginning 
                PURL = PURL.Replace("postgres://", "");

                //Split the beginning and end into two parts at the @
                string[] PurlSplit = PURL.Split('@');

                //We should now have:
                //user:password
                string Username = PurlSplit[0].Split(':')[0];
                string Password = PurlSplit[0].Split(':')[1];

                //And:
                //host:port/database

                //Split this again by /
                PurlSplit = PurlSplit[1].Split('/');

                //Now we should have
                //host:port
                string Host = PurlSplit[0].Split(':')[0];
                string Port = PurlSplit[0].Split(':')[1];

                //Database
                string Database = PurlSplit[1];

                optionsBuilder.UseNpgsql(@$"
                    Host={Host}; Port={Port}; 
                    Username={Username}; Password={Password};
                    Database={Database};
                    Pooling=true;
                    SSL Mode=Require;
                    TrustServerCertificate=True;
                ");

                PostgresMode = true;

            } else {

                //We do not have a URL to connect to a postgres db. Fallback to the local or configured sql server database
                optionsBuilder.UseSqlServer(string.IsNullOrWhiteSpace(SQLServerURL) ? Constants.ConnectionString : SQLServerURL);
            }
        }

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

        /// <summary>Table of all Apartments</summary>
        public DbSet<Apartment> Apartment { get; set; }

        /// <summary>Table of all Businesses</summary>
        public DbSet<Business> Business { get; set; }

        /// <summary>Table of all hotels</summary>
        public DbSet<Hotel> Hotel { get; set; }

        /// <summary>Table of all Tax Reports</summary>
        public DbSet<TaxReport> TaxReport { get; set; }

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
