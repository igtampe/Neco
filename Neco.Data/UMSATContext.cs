using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Igtampe.Neco.Common.UMSAT;

namespace Igtampe.Neco.Data {
    public class UMSATContext: DbContext {

        public UMSATContext(DbContextOptions<UMSATContext> options) : base(options) { }

        public DbSet<Asset> Assets { get; set; }

    }
}
