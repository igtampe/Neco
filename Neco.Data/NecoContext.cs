using System;
using Igtampe.Neco.Common;
using Igtampe.Neco.Common.Contractus;
using Igtampe.Neco.Common.EzTax;
using Igtampe.Neco.Common.EzTax.Subitems;
using Igtampe.Neco.Common.LandView;
using Igtampe.Neco.Common.UMSAT;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Igtampe.Neco.Data {

    /// <summary>Mode to run the Neco Context in</summary>
    public enum NecoContextMode { 
        
        /// <summary>Have the Neco Context decide automatically which database to connect to</summary>
        AUTOMATIC=-1,

        /// <summary>Run the Neco Context connecting to a SQL Server DB  (Usually to run it on a Local instance of SQL Server)</summary>
        SQL_SERVER = 0,

        /// <summary>Run the Neco Context connecting to a Postgres DB (Usually to run it on Heroku)</summary>
        POSTGRES = 1,

        /// <summary>Run the Neco Context connecting to a Database in Memory (Usually to run it for a test)</summary>
        IN_MEMORY = 2
        
    }

    /// <summary>Context that has every table. Used for generating database tables and for testing.</summary>
    public class NecoContext: DbContext {


        /// <summary>Indicates whether or not this Neco context is in Postgres Mode</summary>
        public NecoContextMode Mode { get; private set; } = NecoContextMode.AUTOMATIC;

        /// <summary>URL to the Database this context is connected to</summary>
        private string DBURL;

        /// <summary>Flag to indicate if the in memory database has been setup</summary>
        public static bool InMemorySetupComplete = false;

        /// <summary>Creates a NecoContext</summary>
        public NecoContext() : base() { }

        /// <summary>Creates a NecoContext with an overridden NecoContextMode and URL</summary>
        public NecoContext(NecoContextMode Mode) : base() {
            this.Mode = Mode;
        }

        /// <summary>Creates a NecoContext with an overridden NecoContextMode and URL</summary>
        public NecoContext(NecoContextMode Mode, string URL) : base() {
            this.Mode = Mode;
            DBURL = URL;
        }


        /// <summary>Overrides onConfiguring to use <see cref="Constants.ConnectionString"/></summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {

            if (Mode == NecoContextMode.AUTOMATIC) {

                //We must determine what mode to run this on

                //Check if we have a de-esta cosa for the Database URL (IE For Postgres)
                DBURL = Environment.GetEnvironmentVariable("DATABASE_URL");

                if (DBURL != null) { Mode = NecoContextMode.POSTGRES; } 
                else {
                    Mode = NecoContextMode.SQL_SERVER;
                    DBURL = Constants.ConnectionString;
                }

            }

            switch (Mode) {
                case NecoContextMode.POSTGRES:
                    optionsBuilder.UseNpgsql(ConvertPostgresURLToConnectionString(DBURL));
                    break;
                case NecoContextMode.SQL_SERVER:
                    optionsBuilder.UseSqlServer(DBURL);
                    break;
                case NecoContextMode.IN_MEMORY:
                    optionsBuilder.UseInMemoryDatabase("Neco");
                    break;
                default:
                    throw new InvalidOperationException("Invalid Neco Context Mode was used");
            }
        }

        /// <summary>Overrides on model creation to remove the plural convention</summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            //This will singularize all table names
            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes()) {
                entityType.SetTableName(entityType.DisplayName());
            }
        }

        public static string ConvertPostgresURLToConnectionString(string DBURL) {
            //OK so now we have this
            //postgres://user:password@host:port/database

            //Drop the beginning 
            string PURL = DBURL.Replace("postgres://", "");

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

            return @$"
                        Host={Host}; Port={Port}; 
                        Username={Username}; Password={Password};
                        Database={Database};
                        Pooling=true;
                        SSL Mode=Require;
                        TrustServerCertificate=True;
                    ";
        }

        //Setup
        public void SetupInMemoryDB() {
            if (Mode != NecoContextMode.IN_MEMORY) { throw new InvalidOperationException("This Neco context is not running in InMemory mode"); }
            if (InMemorySetupComplete) { return; }

            //apply the migrations to the in memory database
        }
        
        //Auth

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
