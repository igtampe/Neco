using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Igtampe.Neco.Common.Contractus;

namespace Igtampe.Neco.Data {
    public class ContractusContext: DbContext {

        public ContractusContext(string ConnectionString) : base(ConnectionString) { }
         
        public DbSet<Contract> Contracts { get; set; }

    }
}
