using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ABCFund.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
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
