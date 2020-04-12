using System;
using System.Collections.Generic;
using System.Text;

namespace EF_core_Assignment.Models
{
    public class Assignment
    {
        public int AssignmentId { get; set; }

        public string AssignmentName { get; set; }


        public List<Student> Students { get; set; }

        public int courseId { get; set; }
        public Course Course { get; set; }

        public int teacherAuId { get; set; }
        public Teacher Teacher { get; set; }

        public int studentAuId { get; set; }
        public Student Student { get; set; }





        // Do later 

        //public override string ToString()
        //{
        //    return string.Format("Laptop({0}, {1}, {2}, {3}, {4}, {6}, {5})", LaptopId, Speed, Hd, Price, Price, Product, Screen);
        //}

    }
}

