using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Igtampe.Neco.Common;
using Igtampe.Neco.Common.Contractus;
using Igtampe.Neco.Common.EzTax;
using Igtampe.Neco.Common.EzTax.Subitems;
using Igtampe.Neco.Common.LandView;
using Igtampe.Neco.Common.UMSAT;


namespace Igtampe.Neco.Data {
    public class EverythingContext:DbContext {

        public EverythingContext(string ConnectionString) : base(ConnectionString) { }

        //Auth
        public DbSet<UserAuth> UserAuths { get; set; }

        //Neco
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<CertifiedItem> CertifiedItems { get; set; }
        public DbSet<BankAccountType> BankAccountTypes { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Bank> Banks { get; set; }

        //Contract
        public DbSet<Contract> Contracts { get; set; }

        //EzTax
        public DbSet<IncomeItem> Items { get; set; }
        public DbSet<TaxBracket> Brackets { get; set; }
        public DbSet<TaxJurisdiction> Jurisdictions { get; set; }
        public DbSet<TaxUserInfo> UserInfos { get; set; }

        public DbSet<Apartment> Appartments { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Hotel> Hotels { get; set; }

        //LandView
        public DbSet<Country> Countries { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Plot> Plots { get; set; }

        //UMSAT
        public DbSet<Asset> Assets { get; set; }

    }
}
