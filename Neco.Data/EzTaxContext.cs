using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Igtampe.Neco.Common;
using Igtampe.Neco.Common.EzTax;
using Igtampe.Neco.Common.EzTax.Subitems;

namespace Igtampe.Neco.Data {
    public class EzTaxContext:DbContext {

        public EzTaxContext(DbContextOptions<EzTaxContext> Options) : base(Options) { }

        public DbSet<IncomeItem> Items { get; set; }
        public DbSet<TaxBracket> Brackets { get; set; }
        public DbSet<TaxJurisdiction> Jurisdictions { get; set; }
        public DbSet<TaxUserInfo> UserInfos { get; set; }
        
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Hotel> Hotels { get; set; }

    }
}
