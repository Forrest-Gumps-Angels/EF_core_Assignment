﻿using EF_core_Assignment.Models;
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
        //public DbSet<HelpRequest> helpRequests { get; set; }


        protected override void OnModelCreating(ModelBuilder mb)
        {


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


            // On delete cascade fix
            mb.Entity<Assignment>()
                .HasOne(a => a.Teacher)
                .WithMany(t => t.Assignments)
                .HasForeignKey(a => a.teacherAuId)
                .OnDelete(DeleteBehavior.NoAction);

            mb.Entity<Exercise>()
                .HasOne(a => a.Teacher)
                .WithMany(t => t.Exercises)
                .HasForeignKey(a => a.teacherAuId)
                .OnDelete(DeleteBehavior.NoAction);

            mb.Entity<Teacher>()
                .HasOne(a => a.Course)
                .WithMany(c => c.Teachers)
                .HasForeignKey(c => c.CourseId)
                .OnDelete(DeleteBehavior.NoAction);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
                => options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=efcoreass2v2;Integrated Security=True").EnableSensitiveDataLogging();
    }
}
