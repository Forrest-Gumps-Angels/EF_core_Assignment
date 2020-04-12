using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EF_core_Assignment.Models
{
    public class Exercise
    {
        // Defined as composite key in fluent api
        [MaxLength(64)]
        public string lecture { get; set; }
        public int number { get; set; }


        [MaxLength(128)]
        public string help_where { get; set; }



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

