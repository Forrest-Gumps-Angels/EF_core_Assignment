using System;
using System.Collections.Generic;
using System.Text;

namespace EF_core_Assignment.Models
{
    public class Exercise
    {
        public int ExerciseId { get; set; }
        public string help_where { get; set; }
        public string lecture { get; set; }

        // Missing munber.. Number.. something.. See ef diagram

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public int AuId { get; set; }
        public Student Student { get; set; }


        // Do later 

        //public override string ToString()
        //{
        //    return string.Format("Laptop({0}, {1}, {2}, {3}, {4}, {6}, {5})", LaptopId, Speed, Hd, Price, Price, Product, Screen);
        //}

    }
}

