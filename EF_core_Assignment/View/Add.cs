using EF_core_Assignment.Data;
using EF_core_Assignment.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EF_core_Assignment.View
{
    public class Add
    {
        static public void Execute()
        {
            while (Program.running)
            {
                System.Console.WriteLine("Add information for Course(C), Student(S), Teacher(T), Assignment(A), Exercise(E), Review(R), Help Request(H) and Quit(Q)");
                var consoleKeyInfo3 = Console.ReadKey();
                using (var context = new AppDbContext())
                {
                    DispatcherAdd(context, consoleKeyInfo3.KeyChar);
                }
            }
        }

        static private void DispatcherAdd(AppDbContext context, char key)
        {
            switch (char.ToUpper(key))
            {
                case 'Q':
                    Program.running = false;
                    break;
                case 'C':
                    AddCourse(context);
                    break;
                case 'S':
                    AddStudent(context);
                    break;
                case 'T':
                    AddTeacher(context);
                    break;
                case 'A':
                    AddAssignment(context);
                    break;
                case 'E':
                    AddExercise(context);
                    break;
                case 'R':
                    //AddReview(context);
                    break;
                case 'H':
                    AddHelpRequest(context);
                    break;
                default:
                    Console.WriteLine("Input not accepted.");
                    break;
            }
        }

        static private void AddCourse(AppDbContext context)
        {

            Console.WriteLine("Adding Course...");
            var course = new Course();

            Console.WriteLine("Name: ");
            var Cname = Console.ReadLine();
            course.name = Cname;

            Console.WriteLine("ID: ");
            var ID = Console.ReadLine();
            course.courseId = int.Parse(ID);

            Console.WriteLine("Select Students (Y when done): ");

            var SelectedStudents = ListSelector<Student>(context.students, true);
            var attendList = new List<Attends_shadowtab>();
            foreach (var stud in SelectedStudents)
            {
                attendList.Add(new Attends_shadowtab()
                {
                    active = true,
                    Course = course,
                    courseId = course.courseId,
                    semester = ((Func<int>)(() =>
                    {
                        Console.Write($"Input semester for {stud}:");
                        int.TryParse(Console.ReadLine(), out int n);
                        Console.WriteLine();
                        return n;
                    }))(),
                    Student = stud,
                    studentAuId = stud.AuID
                });
            }

            Console.WriteLine("Select Assignments (Y when done): ");
            var SelectedAssignments = ListSelector<Assignment>(context.assignments, true);
            course.Assignments = SelectedAssignments;

            Console.WriteLine("Select Teachers (Y when done): ");
            var SelectedTeacher = ListSelector<Teacher>(context.teachers, true);
            course.Teachers = SelectedTeacher;

            Console.WriteLine("Select Exercises (Y when done): ");
            var SelectedExercises = ListSelector<Exercise>(context.exercises, true);
            course.Exercises = SelectedExercises;

            try
            {
                context.Add(course);
                context.SaveChanges();
                Console.WriteLine($"Successfully added Course: {course}");
            }
            catch
            {
                Console.WriteLine($"Error adding: {course}");
            }
        }



        static private void AddStudent(AppDbContext context)
        {
            Console.WriteLine("Adding Student...");
            var student = new Student();

            Console.WriteLine("Name: ");
            var Cname = Console.ReadLine();
            student.name = Cname;

            Console.WriteLine("Email: ");
            var Cemail = Console.ReadLine();
            student.email = Cemail;

            Console.WriteLine("AuId: ");
            var ID = Console.ReadLine();
            student.AuID = int.Parse(ID);

            Console.WriteLine("Select Assignment (Y when done): ");

            var SelectedAssignment = ListSelector<Assignment>(context.assignments, true);
            var helpRequest_Shadowtabs = new List<HelpRequest_shadowtab>();
            foreach (var assign in SelectedAssignment)
            {
                helpRequest_Shadowtabs.Add(new HelpRequest_shadowtab()
                {
                    Assignment = assign,
                    AssignmentId = assign.AssignmentId,
                    Student = student,
                    StudentId = student.AuID
                });
            }

            Console.WriteLine("Select Exercises (Y when done): ");
            var SelectedExercises = ListSelector<Exercise>(context.exercises, true);
            student.Exercises = SelectedExercises;

            Console.WriteLine("Select Courses (Y when done): ");

            var SelectedCourses = ListSelector<Course>(context.courses, true);
            var AttendList = new List<Attends_shadowtab>();
            foreach (var course in SelectedCourses)
            {
                AttendList.Add(new Attends_shadowtab()
                {
                    active = true,
                    Course = course,
                    courseId = course.courseId,
                    semester = ((Func<int>)(() =>
                    {
                        Console.Write($"Input semester for {student}:");
                        int.TryParse(Console.ReadLine(), out int n);
                        Console.WriteLine();
                        return n;
                    }))(),
                    Student = student,
                    studentAuId = student.AuID
                });
            }


            try
            {
                context.Add(student);
                context.SaveChanges();
                Console.WriteLine($"Successfully added Student: {student}");
            }
            catch
            {
                Console.WriteLine($"Error adding: {student}");
            }
        }

        static private void AddTeacher(AppDbContext context)
        {
            Console.WriteLine("Adding Teacher...");
            var teacher = new Teacher();

            Console.WriteLine("Name: ");
            var Cname = Console.ReadLine();
            teacher.name = Cname;

            Console.WriteLine("AuId: ");
            var ID = Console.ReadLine();
            teacher.AuID = int.Parse(ID);


            Console.WriteLine("Select Exercises (Y when done): ");
            var SelectedExercises = ListSelector<Exercise>(context.exercises, true);
            teacher.Exercises = SelectedExercises;

            Console.WriteLine("Select Assignments (Y when done): ");
            var SelectedAssignments = ListSelector<Assignment>(context.assignments, true);
            teacher.Assignments = SelectedAssignments;

            Console.WriteLine("Select Course (Y if none): ");
            var SelectedCourse = ListSelector<Course>(context.courses, false);
            teacher.CourseId = SelectedCourse[0].courseId;
            teacher.Course = SelectedCourse[0];

            try
            {
                context.Add(teacher);
                context.SaveChanges();
                Console.WriteLine($"Successfully added Teacher: {teacher}");
            }
            catch
            {
                Console.WriteLine($"Error adding: {teacher}");
            }
        }

        static private void AddAssignment(AppDbContext context)
        {
            Console.WriteLine("Adding Assignment...");
            var assignment = new Assignment();

            Console.WriteLine("Name: ");
            var Cname = Console.ReadLine();
            assignment.AssignmentName = Cname;

            Console.WriteLine("Assignment ID: ");
            var ID = Console.ReadLine();
            assignment.AssignmentId = int.Parse(ID);


            Console.WriteLine("Select Teacher (Y if none): ");
            var SelectedTeacher = ListSelector<Teacher>(context.teachers, false);
            assignment.teacherAuId = SelectedTeacher[0].AuID;
            assignment.Teacher = SelectedTeacher[0];

            Console.WriteLine("Select Course (Y when done): ");
            var SelectedCourse = ListSelector<Course>(context.courses, true);
            assignment.courseId = SelectedCourse[0].courseId;
            assignment.Course = SelectedCourse[0];


            var SelectedStudents = ListSelector<Student>(context.students, true);
            var helpRequest_Shadowtabs = new List<HelpRequest_shadowtab>();
            foreach (var stud in SelectedStudents)
            {
                helpRequest_Shadowtabs.Add(new HelpRequest_shadowtab()
                {
                    Assignment = assignment,
                    AssignmentId = assignment.AssignmentId,
                    Student = stud,
                    StudentId = stud.AuID
                });
            }


            try
            {
                context.Add(assignment);
                context.SaveChanges();
                Console.WriteLine($"Successfully added Assignment: {assignment}");
            }
            catch
            {
                Console.WriteLine($"Error adding: {assignment}");
            }

        }

        static private void AddExercise(AppDbContext context)
        {
            Console.WriteLine("Adding Exercise...");
            var exercise = new Exercise();

            Console.WriteLine("Lecture: ");
            var Cname = Console.ReadLine();
            exercise.lecture = Cname;

            Console.WriteLine("Number: ");
            var ID = Console.ReadLine();
            exercise.number = int.Parse(ID);

            Console.WriteLine("Help where?: ");
            var help = Console.ReadLine();
            exercise.help_where = help;


            Console.WriteLine("Select Student (Y if none): ");
            var SelectedStudent = ListSelector<Student>(context.students, false);
            exercise.studentAuId = SelectedStudent[0].AuID;
            exercise.Student = SelectedStudent[0];

            Console.WriteLine("Select Teacher (Y if none): ");
            var SelectedTeacher = ListSelector<Teacher>(context.teachers, false);
            exercise.teacherAuId = SelectedTeacher[0].AuID;
            exercise.Teacher = SelectedTeacher[0];

            Console.WriteLine("Select Course (Y if none): ");
            var SelectedCourse = ListSelector<Course>(context.courses, false);
            exercise.courseID = SelectedCourse[0].courseId;
            exercise.Course = SelectedCourse[0];

            try
            {
                context.Add(exercise);
                context.SaveChanges();
                Console.WriteLine($"Successfully added Exercise: {exercise}");
            }
            catch
            {
                Console.WriteLine($"Error writing: {exercise}");
            }
        }

        // Do not know about this one???
        //static private void AddReview(AppDbContext context)
        //{

        //}

        static private void AddHelpRequest(AppDbContext context)
        {
            Console.WriteLine("This will guide you through setting up a help request.. ");
            Console.WriteLine("(It is a prerequisite that you have been setup up correctly in the system. \n" +
                "Eg. that yu have created a Student for yourself and added correct courses)");
            var exercise = new Exercise();


            Console.WriteLine("Please select yourself below (Y when done): ");
            var SelectedStudent = ListSelector<Student>(context.students, false);

            Console.WriteLine("Please select the specific assignment (Y when done): ");
            var SelectedAssignment = ListSelector<Assignment>(context.assignments, false);

            Console.WriteLine("Please specify the exercise number: ");
            int.TryParse(Console.ReadLine(), out int exercisenumber);

            Console.WriteLine("What lecture?: ");
            var lecture = Console.ReadLine();

            Console.WriteLine("Please describe the problem: ");
            var help_where = Console.ReadLine();

            exercise.studentAuId = SelectedStudent[0].AuID;
            exercise.Student = SelectedStudent[0];
            exercise.help_where = help_where;
            exercise.lecture = lecture;
            exercise.number = exercisenumber;
            exercise.Teacher = SelectedAssignment[0].Teacher;
            exercise.teacherAuId = SelectedAssignment[0].teacherAuId;

            try
            {
                context.Add(exercise);
                context.SaveChanges();
                Console.WriteLine($"Successfully added Exercise: {exercise}");
            }
            catch
            {
                Console.WriteLine($"Error writing: {exercise}");
            }


        }

        static private List<T> ListSelector<T>(DbSet<T> dbset, bool continuous) where T : class
        {

            var outList = new List<T>();
            var selecting = true;
            var genericList = dbset.ToList();

            for (int i = 0; i < genericList.Count(); i++)
            {
                Console.WriteLine($"[{i}] {genericList[i]}");
            }

            while (selecting)
            {
                var input = Console.ReadLine();
                if (input == "Y") selecting = false;
                else if (int.TryParse(input, out int n))
                {
                    if (n < genericList.Count())
                    {
                        outList.Add(genericList[n]);
                        Console.WriteLine($"You just added: {genericList[n]}");
                        selecting = continuous;
                    }
                    else Console.WriteLine("Index is invalid");
                }
                else
                {
                    Console.WriteLine("Try again...");
                }

            }

            return outList;
        }
    }
}
