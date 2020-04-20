using EF_core_Assignment.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EF_core_Assignment.View
{
    public class Browse
    {
        static private void Dispatcher(AppDbContext context, char key)
        {

            switch (char.ToUpper(key))
            {
                case 'Q':
                    Program.running = false;
                    break;

                case 'T':
                    InfoFromTeacher(context);
                    break;
                case 'C':
                    InfoFromCourse(context);
                    break;
                case 'S':
                    InfoFromStudent(context);
                    break;
                case 'P':
                    Statistics(context);
                    break;
            }
        }

        static public void Execute()
        {
            while (Program.running)
            {

                System.Console.WriteLine("Find information from Teacher auid(T), Course name(C), Student auid(S) or get Statistics(P). Quit(Q)");
                var consoleKeyInfo2 = Console.ReadKey();
                using (var context = new AppDbContext())
                {
                    Dispatcher(context, consoleKeyInfo2.KeyChar);
                }
            }

            System.Console.WriteLine("Quiting...");
        }

        static void InfoFromTeacher(AppDbContext context)
        {
            System.Console.WriteLine("AuID of the given teacher. Eg. \"201812345\"");
            var auid = Console.ReadLine();
            int.TryParse(auid, out int auid_int);


            foreach (var teacher in context.teachers.Where(t => t.AuID == auid_int).ToList())
            {
                System.Console.WriteLine($"\nThe requests for teacher: {teacher}");


                foreach (var assignment in context.assignments
                    .Where(a => a.teacherAuId == teacher.AuID)
                    .Include(assign => assign.Course)
                    .Include(assign => assign.AssignmentReq)
                    .ToList())
                {
                    System.Console.WriteLine($"\tThe assignment information is: {assignment}");
                    System.Console.WriteLine($"\tThe course information is: {assignment.Course}");

                    Console.WriteLine($"\n\tThe students are: ");

                    //WORKS!
                    foreach (var asReq in assignment.AssignmentReq)
                    {
                        foreach (var stud in context.students.Where(s => s.AuID == asReq.StudentId))
                        {
                            Console.WriteLine($"\t\t{stud}");
                        }
                    }

                    Console.WriteLine($"\n\tThe exercise information is: ");

                //    foreach (var ealink in assignment.exerciseAssignment_Links)
                //    {
                //        foreach (var exercise in context.exercises.Where(e => e.number == ealink.ExerciseNumber))
                //        {
                //            Console.WriteLine($"\t\t{exercise}");
                //        }
                //    }
                }
            }

            Console.WriteLine();
        }

        static void InfoFromCourse(AppDbContext context)
        {
            System.Console.WriteLine("Coursename of the given course. Eg. \"NGK\"");
            var coursename = Console.ReadLine();

            foreach (var course in context.courses.Where(a => a.name.Contains(coursename)).ToList())
            {
                System.Console.WriteLine($"\nThe requests for course: {course}");


                foreach (var assignment in context.assignments
                    .Where(a => a.courseId == course.courseId)
                    .Include(assign => assign.Teacher)
                    .Include(assign => assign.AssignmentReq)
                    .ToList())
                {
                    System.Console.WriteLine($"\tThe assignment information is: {assignment}");
                    System.Console.WriteLine($"\tThe responsible teacher information is: {assignment.Teacher}");

                    Console.WriteLine($"\n\tThe students are: ");

                    //WORKS!
                    foreach (var asReq in assignment.AssignmentReq)
                    {
                        foreach (var stud in context.students.Where(s => s.AuID == asReq.StudentId))
                        {
                            Console.WriteLine($"\t\t{stud}");
                        }
                    }

                    Console.WriteLine($"\n\tThe exercise information is: ");

                        //foreach (var ealink in assignment.exerciseAssignment_Links)
                        //{
                        //    foreach (var exercise in context.exercises.Where(e => e.number == ealink.ExerciseNumber))
                        //    {
                        //        Console.WriteLine($"\t\t{exercise}");
                        //    }
                        //}

                    }
            }

            Console.WriteLine();
        }

        static void InfoFromStudent(AppDbContext context)
        {
            System.Console.WriteLine("AuID of the given student. Eg. \"201806493\"");
            var auid = Console.ReadLine();
            int.TryParse(auid, out int auid_int);

            foreach (var student in context.students
                .Where(s => s.AuID == auid_int)
                .Include(s => s.StudentReq)
                .ToList())
            {
                System.Console.WriteLine($"\nThe requests for student: {student}");

                foreach (var helpreq in student.StudentReq)
                {
                    foreach (var ass in context.assignments
                        .Where(a => a.AssignmentId == helpreq.AssignmentId).ToList())
                    {
                        System.Console.WriteLine($"\tThe assignment information is: {ass}");

                        Console.WriteLine($"\n\tThe exercise information is: ");

                        //foreach (var ealink in ass.exerciseAssignment_Links)
                        //{
                        //    foreach (var exercise in context.exercises.Where(e => e.number == ealink.ExerciseNumber))
                        //    {
                        //        Console.WriteLine($"\t\t{exercise}");
                        //    }
                        //}
                    }
                }

            }

            Console.WriteLine();
        }


        static void Statistics(AppDbContext context)
        {
            System.Console.WriteLine("Statistics");


            foreach (var course in context.courses
                .Include(c => c.Exercises)
                .ToList())
            {
                System.Console.WriteLine($"Number of open requests in {course.name}: {course.Exercises.Count()}");
            }

            Console.WriteLine();
        }
    }
}
