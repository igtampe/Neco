using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igtampe.Neco.Data {

    /// <summary>Context for using Neco on Cosmos rather than on SQL Server</summary>
    public class NecoCosmosContext:NecoContext {

        /// <summary>Overrides onConfiguring to use <see cref="Constants.ConnectionString"/></summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            string[] CosmosString = File.ReadAllLines("Cosmos.txt");
            if (CosmosString.Length < 1) { throw new InvalidOperationException("I need one of these lines at least"); }
            optionsBuilder.UseCosmos(CosmosString[0],"neco"); 
        }

    }
}
