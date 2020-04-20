using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EF_core_Assignment.Models
{
    public class Course
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int courseId { get; set; }
        public string name { get; set; }

        public List<Attends_shadowtab> studentsInCourse { get; set; }

        public List<Assignment> Assignments { get; set; }
        public List<Teacher> Teachers { get; set; }
        public List<Exercise> Exercises { get; set; }

        public override string ToString()
        {
            return string.Format($"Course({name}, {courseId})");
        }
    }
}

