﻿// <auto-generated />
using System;
using EF_core_Assignment.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EF_core_Assignment.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3");

            modelBuilder.Entity("EF_core_Assignment.Models.Assignment", b =>
                {
                    b.Property<int>("AssignmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AssignmentName")
                        .HasColumnType("TEXT");

                    b.Property<int?>("StudentAuID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("courseId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("teacherAuId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AssignmentId");

                    b.HasIndex("StudentAuID");

                    b.HasIndex("courseId");

                    b.HasIndex("teacherAuId");

                    b.ToTable("assignments");
                });

            modelBuilder.Entity("EF_core_Assignment.Models.Attends_shadowtab", b =>
                {
                    b.Property<int>("studentAuId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("courseId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("active")
                        .HasColumnType("INTEGER");

                    b.Property<int>("semester")
                        .HasColumnType("INTEGER");

                    b.HasKey("studentAuId", "courseId");

                    b.HasIndex("courseId");

                    b.ToTable("Attends_shadowtab");
                });

            modelBuilder.Entity("EF_core_Assignment.Models.Course", b =>
                {
                    b.Property<int>("courseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .HasColumnType("TEXT");

                    b.HasKey("courseId");

                    b.ToTable("courses");
                });

            modelBuilder.Entity("EF_core_Assignment.Models.Exercise", b =>
                {
                    b.Property<string>("lecture")
                        .HasColumnType("TEXT")
                        .HasMaxLength(64);

                    b.Property<int>("number")
                        .HasColumnType("INTEGER");

                    b.Property<int>("courseID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("help_where")
                        .HasColumnType("TEXT")
                        .HasMaxLength(128);

                    b.Property<int>("studentAuId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("teacherAuId")
                        .HasColumnType("INTEGER");

                    b.HasKey("lecture", "number");

                    b.HasIndex("courseID");

                    b.HasIndex("studentAuId");

                    b.HasIndex("teacherAuId");

                    b.ToTable("exercises");
                });

            modelBuilder.Entity("EF_core_Assignment.Models.HelpRequest_shadowtab", b =>
                {
                    b.Property<int>("AssignmentId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StudentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AssignmentId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("HelpRequest_shadowtab");
                });

            modelBuilder.Entity("EF_core_Assignment.Models.Student", b =>
                {
                    b.Property<int>("AuID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("email")
                        .HasColumnType("TEXT")
                        .HasMaxLength(64);

                    b.Property<string>("name")
                        .HasColumnType("TEXT")
                        .HasMaxLength(64);

                    b.HasKey("AuID");

                    b.ToTable("students");
                });

            modelBuilder.Entity("EF_core_Assignment.Models.Teacher", b =>
                {
                    b.Property<int>("AuID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CourseId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .HasColumnType("TEXT")
                        .HasMaxLength(64);

                    b.HasKey("AuID");

                    b.HasIndex("CourseId");

                    b.ToTable("teachers");
                });

            modelBuilder.Entity("EF_core_Assignment.Models.Assignment", b =>
                {
                    b.HasOne("EF_core_Assignment.Models.Student", null)
                        .WithMany("Assignments")
                        .HasForeignKey("StudentAuID");

                    b.HasOne("EF_core_Assignment.Models.Course", "Course")
                        .WithMany("Assignments")
                        .HasForeignKey("courseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EF_core_Assignment.Models.Teacher", "Teacher")
                        .WithMany("Assignments")
                        .HasForeignKey("teacherAuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EF_core_Assignment.Models.Attends_shadowtab", b =>
                {
                    b.HasOne("EF_core_Assignment.Models.Course", "Course")
                        .WithMany("studentsInCourse")
                        .HasForeignKey("courseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EF_core_Assignment.Models.Student", "Student")
                        .WithMany("attendsCourses")
                        .HasForeignKey("studentAuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EF_core_Assignment.Models.Exercise", b =>
                {
                    b.HasOne("EF_core_Assignment.Models.Course", "Course")
                        .WithMany("Exercises")
                        .HasForeignKey("courseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EF_core_Assignment.Models.Student", "Student")
                        .WithMany("Exercises")
                        .HasForeignKey("studentAuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EF_core_Assignment.Models.Teacher", "Teacher")
                        .WithMany("Exercises")
                        .HasForeignKey("teacherAuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EF_core_Assignment.Models.HelpRequest_shadowtab", b =>
                {
                    b.HasOne("EF_core_Assignment.Models.Assignment", "Assignment")
                        .WithMany("AssignmentReq")
                        .HasForeignKey("AssignmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EF_core_Assignment.Models.Student", "Student")
                        .WithMany("StudentReq")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EF_core_Assignment.Models.Teacher", b =>
                {
                    b.HasOne("EF_core_Assignment.Models.Course", "Course")
                        .WithMany("Teachers")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}