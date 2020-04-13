using System;
using System.Collections.Generic;
using System.Text;

namespace EF_core_Assignment.Models
{
    public class Assignment
    {
        public int AssignmentId { get; set; }

        public string AssignmentName { get; set; }


        public int courseId { get; set; }
        public Course Course { get; set; }

        public int teacherAuId { get; set; }
        public Teacher Teacher { get; set; }

        //Junction table between Student and Assignment with N-N relationship
        public List<HelpRequest_shadowtab> AssignmentReq { get; set; }

        // Do later 

        //public override string ToString()
        //{
        //    return string.Format("Laptop({0}, {1}, {2}, {3}, {4}, {6}, {5})", LaptopId, Speed, Hd, Price, Price, Product, Screen);
        //}

    }
}

