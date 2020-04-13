﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EF_core_Assignment.Models
{
    public class Student
    {
        [Required]
        [Key]
        public int AuID { get; set; }
        [MaxLength(64)]
        public string name { get; set; }
        [MaxLength(64)]
        public string email { get; set; }

        public List<Exercise> Exercises { get; set; }
        public List<Attends_shadowtab> attendsCourses { get; set; }
        public List<Assignment> Assignments { get; set; }

        //Junction table between Student and Assignment with N-N relationship
        public List<HelpRequest_shadowtab> StudentReq { get; set; }


        // Do later 

        //public override string ToString()
        //{
        //    return string.Format("Laptop({0}, {1}, {2}, {3}, {4}, {6}, {5})", LaptopId, Speed, Hd, Price, Price, Product, Screen);
        //}

    }
}

