using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCFund.Models;

namespace ABCFund.Data
{
    public class ABCFundContext : DbContext
    {
        public ABCFundContext(DbContextOptions<ABCFundContext> options)
               : base(options)
        {
        }

        public DbSet<ABCFund.Models.Periodic> Periodic { get; set; }

        public DbSet<ABCFund.Models.Holding> Holding { get; set; }

        public DbSet<ABCFund.Models.Historical> Historical { get; set; }

        public DbSet<ABCFund.Models.Investor> Investor { get; set; }

        public DbSet<ABCFund.Models.Stock> Stock { get; set; }

        public DbSet<ABCFund.Models.User> User { get; set; }

        public DbSet<ABCFund.Models.Fund> Fund { get; set; }
    }
}
