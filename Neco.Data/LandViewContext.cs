﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Igtampe.Neco.Common.LandView;

namespace Igtampe.Neco.Data {
    public class LandViewContext: DbContext {

        public LandViewContext(string ConnectionString) : base(ConnectionString) { }

        public DbSet<Country> Countries { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Plot> Plots { get; set; }

    }
}
