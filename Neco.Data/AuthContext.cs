using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Igtampe.Neco.Common;

namespace Igtampe.Neco.Data {
    public class AuthContext: DbContext {

        public AuthContext(DbContextOptions<AuthContext> options) : base(options) { }

        public DbSet<UserAuth> Users { get; set; }

    }
}
