using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Igtampe.Neco.Common.Contractus;

namespace Igtampe.Neco.Data {
    public class ContractusContext: DbContext {

        public ContractusContext(DbContextOptions<ContractusContext> options) : base(options) { }
         
        public DbSet<Contract> Contracts { get; set; }

    }
}
