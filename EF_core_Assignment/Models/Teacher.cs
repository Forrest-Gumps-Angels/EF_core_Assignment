using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EF_core_Assignment.Models
{
    public class Teacher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AuID { get; set; }
        [MaxLength(64)]
        public string name { get; set; }


        public List<Exercise> Exercises { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public List<Assignment> Assignments { get; set; }


        public override string ToString()
        {
            return string.Format($"Teacher({name}, {AuID})");
        }

    }
}

