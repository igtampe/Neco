using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Igtampe.Neco.Common.UMSAT;

namespace Igtampe.Neco.Data {
    public class UMSATContext: DbContext {

        public UMSATContext(string ConnectionString) : base(ConnectionString) { }

        public DbSet<Asset> Assets { get; set; }

    }
}
