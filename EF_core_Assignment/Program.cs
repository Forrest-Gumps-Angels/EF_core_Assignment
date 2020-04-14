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
        static bool running = true;
        static bool running2 = true;

        static void Main(string[] args)
        {
            Console.WriteLine("Press B for browsing database and A to add to database: ");
            var consoleKeyInfo1 = Console.ReadKey().KeyChar;

            using (var context = new AppDbContext())
            {
                switch(char.ToUpper(consoleKeyInfo1))
                {
                    case 'B':
                        SeedDatabase(context);
                        Browse();
                        break;
                    case 'A':
                        SeedDatabase(context);
                        Add();
                        break;
                }           
            }
        }

        static private void Browse()
        {
            while (running)
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

        static private void Add()
        {
            while (running2)
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
            switch(char.ToUpper(key))
            {
                case 'Q':
                    running2 = false;
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
                    semester = ((Func<int>) (()=> 
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

        static private void Dispatcher(AppDbContext context, char key)
        {

            switch(char.ToUpper(key))
            {
                case 'Q':
                    running = false;
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
                    .Include(assign => assign.exerciseAssignment_Links)
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

                    foreach (var ealink in assignment.exerciseAssignment_Links)
                    {
                        foreach (var exercise in context.exercises.Where(e => e.number == ealink.ExerciseNumber))
                        {
                            Console.WriteLine($"\t\t{exercise}");
                        }
                    }




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
                    .Include(assign => assign.exerciseAssignment_Links)
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

                    foreach (var ealink in assignment.exerciseAssignment_Links)
                    {
                        foreach (var exercise in context.exercises.Where(e => e.number == ealink.ExerciseNumber))
                        {
                            Console.WriteLine($"\t\t{exercise}");
                        }
                    }

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
                        .Where(a => a.AssignmentId == helpreq.AssignmentId)
                        .Include(a => a.exerciseAssignment_Links)
                        .ToList())
                    {
                        System.Console.WriteLine($"\tThe assignment information is: {ass}");

                        Console.WriteLine($"\n\tThe exercise information is: ");

                        foreach (var ealink in ass.exerciseAssignment_Links)
                        {
                            foreach (var exercise in context.exercises.Where(e => e.number == ealink.ExerciseNumber))
                            {
                                Console.WriteLine($"\t\t{exercise}");
                            }
                        }
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


        private static void SeedDatabase(AppDbContext context)
        {
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();


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

            //HelpRequest request1 = new HelpRequest()
            //{
            //    Course = course1,
            //    courseId = course1.courseId,
            //    Exercise = exercise1,
            //    exerciseId = exercise1.number,
            //    HelpRequestId = 1,
            //    Student = student1,
            //    studentAuId = student1.AuID,
            //    Teacher = teacher1,
            //    teacherAuId = teacher1.AuID
            //};

            //HelpRequest request2 = new HelpRequest()
            //{
            //    Course = course2,
            //    courseId = course2.courseId,
            //    Exercise = exercise2,
            //    exerciseId = exercise2.number,
            //    HelpRequestId = 2,
            //    Student = student2,
            //    studentAuId = student2.AuID,
            //    Teacher = teacher2,
            //    teacherAuId = teacher2.AuID
            //};

            //HelpRequest request3 = new HelpRequest()
            //{
            //    Course = course3,
            //    courseId = course3.courseId,
            //    Exercise = exercise3,
            //    exerciseId = exercise3.number,
            //    HelpRequestId = 3,
            //    Student = student3,
            //    studentAuId = student3.AuID,
            //    Teacher = teacher3,
            //    teacherAuId = teacher3.AuID
            //};


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

            // Exercises to Assignments \\
            exercise1.exerciseAssignment_Links = new List<ExerciseAssignment_link>()
            {
                                        new ExerciseAssignment_link()
                                        {
                                            Assignment = assignment1,
                                        AssignmentId = assignment1.AssignmentId,
                                        Exercise = exercise1,
                                        ExerciseNumber = exercise1.number
                                        }};

            exercise2.exerciseAssignment_Links = new List<ExerciseAssignment_link>()
            {
                                        new ExerciseAssignment_link()
                                        {
                                            Assignment = assignment2,
                                        AssignmentId = assignment2.AssignmentId,
                                        Exercise = exercise2,
                                        ExerciseNumber = exercise2.number
                                        }};

            exercise3.exerciseAssignment_Links = new List<ExerciseAssignment_link>()
            {
                                        new ExerciseAssignment_link()
                                        {
                                            Assignment = assignment3,
                                        AssignmentId = assignment3.AssignmentId,
                                        Exercise = exercise3,
                                        ExerciseNumber = exercise3.number
                                        }};

            // Exercises to Teacher \\
            exercise1.Teacher = teacher1;
            exercise2.Teacher = teacher2;
            exercise3.Teacher = teacher3;

            // Exercises to Courses \\
            exercise1.courseID = course1.courseId;
            exercise1.Course = course1;

            exercise2.courseID = course2.courseId;
            exercise2.Course = course2;

            exercise3.courseID = course3.courseId;
            exercise3.Course = course3;

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

            // Course to exercise \\
            course1.Exercises = new List<Exercise>() { exercise1 };
            course2.Exercises = new List<Exercise>() { exercise2 };
            course3.Exercises = new List<Exercise>() { exercise3 };

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

            // Assignments to exercises
            assignment1.exerciseAssignment_Links = exercise1.exerciseAssignment_Links;
            assignment2.exerciseAssignment_Links = exercise2.exerciseAssignment_Links;
            assignment3.exerciseAssignment_Links = exercise3.exerciseAssignment_Links;


            /////////////////
            //   CONTEXT   //
            /////////////////
            try
            {
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
                System.Console.WriteLine("Data has been seeded");
            } 
            catch
            {
                System.Console.WriteLine("Data already exists");
            }
        }
    }    
}



