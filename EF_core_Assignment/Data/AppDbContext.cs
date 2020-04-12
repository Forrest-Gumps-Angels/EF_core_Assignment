using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EF_core_Assignment.Data
{
    public class AppDbContext : DbContext
    {

        public DbSet<Pc> pcs { get; set; }
        public DbSet<Laptop> laptops { get; set; }
        public DbSet<Printer> printers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
                => options.UseSqlite("Data Source=products.db");

    }
}
