using EF_core_Assignment.Data;
using EF_core_Assignment.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EF_core_Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            using (var context = new AppDbContext())
            {
                System.Console.WriteLine("Should we seed data? Y/n");
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
                if (consoleKeyInfo.KeyChar == 'Y')
                {
                    SeedDatabase(context);
                }

            }

        }


        private static void SeedDatabase(AppDbContext context)
        {
            // STUDENTS \\
            Student student1 = new Student()
            {
                AuID = 201805227,
                name = "Morten Hansen",
                email = "201805227@post.au.dk"
            };

            Student student2 = new Student()
            {
                AuID = 201806493,
                name = "Rasmus Sørensen",
                email = "201806493@post.au.dk"
            };

            Student student3 = new Student()
            {
                AuID = 201817065,
                name = "Viktor Andersen",
                email = "201817065@post.au.dk"
            };

            // COURSES \\
            Course course1 = new Course()
            {
                courseId = 1,
                name = "DAB"
            };


            Course course2 = new Course()
            {
                courseId = 2,
                name = "NGK"
            };

            Course course3 = new Course()
            {
                courseId = 3,
                name = "SWD",
            };

            // TEACHERS \\
            Teacher teacher1 = new Teacher()
            {
                AuID = 201812345,
                name = "Henrik"
            };

            Teacher teacher2 = new Teacher()
            {
                AuID = 201712345,
                name = "Michael"
            };

            Teacher teacher3 = new Teacher()
            {
                AuID = 201612345,
                name = "Poul Ejnar"
            };


            // EXERCISES \\
            Exercise exercise1 = new Exercise()
            {
                lecture = "7.2 ER Core - Query + Manipulation",
                number = 2,
                help_where = "I need help with my goddamn EF Core!!"
            };

            Exercise exercise2 = new Exercise()
            {
                lecture = "WebAPI",
                number = 10,
                help_where = "How to consume REST API in Xamarin Application?"
            };

            Exercise exercise3 = new Exercise()
            {
                lecture = "Week 07.1: GoF Template Method + GoF Strategy",
                number = 9,
                help_where = "Is there a connection between GoF and the Fantastic Four?"
            };


            // ASSIGNMENTS \\
            Assignment assignment1 = new Assignment()
            {
                AssignmentId = 1,
                AssignmentName = "Assignment 2 - Entity Framework Core"
            };

            Assignment assignment2 = new Assignment()
            {
                AssignmentId = 2,
                AssignmentName = "NGK Lab10: WebAPI"

            };

            Assignment assignment3 = new Assignment()
            {
                AssignmentId = 3,
                AssignmentName = "Lab exercise: SuperSorter"
            };


            ///////////////////
            // CONNECTIONS //
            /////////////////


            // Courses to students \\

            course1.studentsInCourse = new List<Attends_shadowtab>() {
                                        new Attends_shadowtab() {
                                                active = true,
                                                courseId = course1.courseId,
                                                Course = course1,
                                                semester = 4,
                                                Student = student1,
                                                studentAuId = student1.AuID }};



            course2.studentsInCourse = new List<Attends_shadowtab>() {
                                        new Attends_shadowtab() {
                                                active = true,
                                                courseId = course2.courseId,
                                                Course = course2,
                                                semester = 4,
                                                Student = student2,
                                                studentAuId = student2.AuID }};

            course3.studentsInCourse = new List<Attends_shadowtab>() {
                                        new Attends_shadowtab() {
                                                active = true,
                                                courseId = course3.courseId,
                                                Course = course3,
                                                semester = 4,
                                                Student = student3,
                                                studentAuId = student3.AuID }};

            // Students to courses \\
            student1.attendsCourses = course1.studentsInCourse;

            student2.attendsCourses = course2.studentsInCourse;

            student3.attendsCourses = course3.studentsInCourse;

            // Students to Assignments \\
            student1.StudentReq = new List<HelpRequest_shadowtab>()
            {
                                        new HelpRequest_shadowtab()
                                        {
                                            Student = student1,
                                            StudentId = student1.AuID,
                                            Assignment = assignment1,
                                            AssignmentId = assignment1.AssignmentId }};

            student2.StudentReq = new List<HelpRequest_shadowtab>()
            {
                                        new HelpRequest_shadowtab()
                                        {
                                            Student = student2,
                                            StudentId = student2.AuID,
                                            Assignment = assignment2,
                                            AssignmentId = assignment2.AssignmentId }};

            student3.StudentReq = new List<HelpRequest_shadowtab>()
            {
                                        new HelpRequest_shadowtab()
                                        {
                                            Student = student3,
                                            StudentId = student3.AuID,
                                            Assignment = assignment3,
                                            AssignmentId = assignment3.AssignmentId }};

            // Students to Exercises \\
            student1.Exercises = new List<Exercise> { exercise1 };
            student2.Exercises = new List<Exercise> { exercise2 };
            student3.Exercises = new List<Exercise> { exercise3 };


            // Exercises to Students \\
            exercise1.Student = student1;
            exercise2.Student = student2;
            exercise3.Student = student3;

            // Exercises to Teacher \\
            exercise1.Teacher = teacher1;
            exercise2.Teacher = teacher2;
            exercise3.Teacher = teacher3;

            // Teacher to Exercises \\
            teacher1.Exercises = new List<Exercise>() { exercise1 };
            teacher2.Exercises = new List<Exercise>() { exercise2 };
            teacher3.Exercises = new List<Exercise>() { exercise3 };

            // Teacher to Assignment \\
            teacher1.Assignments = new List<Assignment>() { assignment1 };
            teacher2.Assignments = new List<Assignment>() { assignment2 };
            teacher3.Assignments = new List<Assignment>() { assignment3 };

            // Teacher to Course \\
            teacher1.Course = course1;
            teacher1.CourseId = course1.courseId;

            teacher2.Course = course2;
            teacher2.CourseId = course2.courseId;

            teacher3.Course = course3;
            teacher3.CourseId = course3.courseId;

            // Course to Assignments \\
            course1.Assignments = new List<Assignment>() { assignment1 };
            course2.Assignments = new List<Assignment>() { assignment2 };
            course3.Assignments = new List<Assignment>() { assignment3 };

            // Course to Teacher \\
            course1.Teachers = new List<Teacher>() { teacher1 };
            course2.Teachers = new List<Teacher>() { teacher2 };
            course3.Teachers = new List<Teacher>() { teacher3 };

            // Assignment to Student \\
            assignment1.AssignmentReq = student1.StudentReq;

            assignment2.AssignmentReq = student2.StudentReq;

            assignment3.AssignmentReq = student3.StudentReq;

            // Assignment to Teacher \\
            assignment1.Teacher = teacher1;
            assignment1.teacherAuId = teacher1.AuID;

            assignment2.Teacher = teacher2;
            assignment2.teacherAuId = teacher2.AuID;

            assignment3.Teacher = teacher3;
            assignment3.teacherAuId = teacher3.AuID;

            // Assignment to Course \\
            assignment1.Course = course1;
            assignment1.courseId = course1.courseId;
            assignment2.Course = course2;
            assignment2.courseId = course2.courseId;
            assignment3.Course = course3;
            assignment3.courseId = course3.courseId;

            /////////////////
            //   CONTEXT   //
            /////////////////

            context.Add(student1);
            context.Add(student2);
            context.Add(student3);

            context.Add(course1);
            context.Add(course2);
            context.Add(course3);

            context.Add(teacher1);
            context.Add(teacher2);
            context.Add(teacher3);

            context.Add(exercise1);
            context.Add(exercise2);
            context.Add(exercise3);

            context.Add(assignment1);
            context.Add(assignment2);
            context.Add(assignment3);

            context.SaveChanges();
            System.Console.WriteLine("Data saved");
        }
    }    
}



