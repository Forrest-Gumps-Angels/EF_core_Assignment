﻿// <auto-generated />
using System;
using EF_core_Assignment.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EF_core_Assignment.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20200413072722_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EF_core_Assignment.Models.Assignment", b =>
                {
                    b.Property<int>("AssignmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AssignmentName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("StudentAuID")
                        .HasColumnType("int");

                    b.Property<int>("courseId")
                        .HasColumnType("int");

                    b.Property<int>("teacherAuId")
                        .HasColumnType("int");

                    b.HasKey("AssignmentId");

                    b.HasIndex("StudentAuID");

                    b.HasIndex("courseId");

                    b.HasIndex("teacherAuId");

                    b.ToTable("Assignment");
                });

            modelBuilder.Entity("EF_core_Assignment.Models.Attends_shadowtab", b =>
                {
                    b.Property<int>("studentAuId")
                        .HasColumnType("int");

                    b.Property<int>("courseId")
                        .HasColumnType("int");

                    b.Property<bool>("active")
                        .HasColumnType("bit");

                    b.Property<int>("semester")
                        .HasColumnType("int");

                    b.HasKey("studentAuId", "courseId");

                    b.HasIndex("courseId");

                    b.ToTable("Attends_shadowtab");
                });

            modelBuilder.Entity("EF_core_Assignment.Models.Course", b =>
                {
                    b.Property<int>("courseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("courseId");

                    b.ToTable("courses");
                });

            modelBuilder.Entity("EF_core_Assignment.Models.Exercise", b =>
                {
                    b.Property<string>("lecture")
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.Property<int>("number")
                        .HasColumnType("int");

                    b.Property<string>("help_where")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<int>("studentAuId")
                        .HasColumnType("int");

                    b.Property<int>("teacherAuId")
                        .HasColumnType("int");

                    b.HasKey("lecture", "number");

                    b.HasIndex("studentAuId");

                    b.HasIndex("teacherAuId");

                    b.ToTable("exercises");
                });

            modelBuilder.Entity("EF_core_Assignment.Models.HelpRequest_shadowtab", b =>
                {
                    b.Property<int>("AssignmentId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("AssignmentId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("HelpRequest_shadowtab");
                });

            modelBuilder.Entity("EF_core_Assignment.Models.Student", b =>
                {
                    b.Property<int>("AuID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.HasKey("AuID");

                    b.ToTable("students");
                });

            modelBuilder.Entity("EF_core_Assignment.Models.Teacher", b =>
                {
                    b.Property<int>("AuID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(64)")
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
                        .WithMany("StudentAssignments")
                        .HasForeignKey("AssignmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EF_core_Assignment.Models.Student", "Student")
                        .WithMany("StudentAssignments")
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
