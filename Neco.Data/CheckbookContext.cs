using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Igtampe.Neco.Common;

namespace Igtampe.Neco.Data {
    public class CheckbookContext: DbContext {

        public CheckbookContext(DbContextOptions<CheckbookContext> options) : base(options) { }
         
        public DbSet<CheckbookItem> Contracts { get; set; }

    }
}
