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
        public DbSet<Assignment> assignments { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            //Creating composite keys 
            mb.Entity<Exercise>()
            .HasKey(a => new { a.lecture, a.number });

            //Creating N - N relationship
            mb.Entity<Attends_shadowtab>()
            .HasKey(a => new { a.studentAuId, a.courseId });
            mb.Entity<Attends_shadowtab>()
                .HasOne(att => att.Student)
                .WithMany(stud => stud.attendsCourses)
                .HasForeignKey(att => att.studentAuId);
            mb.Entity<Attends_shadowtab>()
                .HasOne(att => att.Course)
                .WithMany(c => c.studentsInCourse)
                .HasForeignKey(att => att.courseId);

            /*
             * Creating N-N relation between Student and
             * Assignment via junction table
             */
            mb.Entity<HelpRequest_shadowtab>()
                .HasKey(p => new { p.AssignmentId, p.StudentId });

            mb.Entity<HelpRequest_shadowtab>()
                .HasOne(hps => hps.Student)
                .WithMany(s => s.StudentReq)
                .HasForeignKey(hps => hps.StudentId);

            mb.Entity<HelpRequest_shadowtab>()
                .HasOne(hps => hps.Assignment)
                .WithMany(a => a.AssignmentReq)
                .HasForeignKey(hps => hps.AssignmentId);
        }



        protected override void OnConfiguring(DbContextOptionsBuilder options)
                => options.UseSqlite("Data Source=helprequests.db").EnableSensitiveDataLogging();

    }
}
