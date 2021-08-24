using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Igtampe.Neco.Common;

namespace Igtampe.Neco.Data {
    public class CheckbookContext: DbContext {

        public CheckbookContext(string ConnectionString) : base(ConnectionString) { }
         
        public DbSet<CheckbookItem> Contracts { get; set; }

    }
}
