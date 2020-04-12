using EF_core_Assignment.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EF_core_Assignment.Data
{
    public class AppDbContext : DbContext
    {

        public DbSet<Exercise> exercises { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<Course> courses { get; set; }
        public DbSet<Teacher> teachers { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Exercise>()
            .HasKey(a => new { a.lecture, a.number });
            mb.Entity<Attends>()
            .HasKey(a => new { a.studentAuId, a.courseId });
        }



        protected override void OnConfiguring(DbContextOptionsBuilder options)
                => options.UseSqlite("Data Source=helprequests.db");

    }
}
