using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Igtampe.Neco.Common;

namespace Igtampe.Neco.Data {
    public class NecoContext:DbContext {

        public NecoContext(DbContextOptions<NecoContext> options) : base(options) { }

        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<CertifiedItem> CertifiedItems { get; set; }
        public DbSet<BankAccountType> BankAccountTypes { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Bank> Banks { get; set; }

    }
}
