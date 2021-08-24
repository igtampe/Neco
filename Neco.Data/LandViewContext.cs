using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Igtampe.Neco.Common.LandView;

namespace Igtampe.Neco.Data {
    public class LandViewContext: DbContext {

        public LandViewContext(DbContextOptions<LandViewContext> options) : base(options) { }

        public DbSet<Country> Countries { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Plot> Plots { get; set; }

    }
}
