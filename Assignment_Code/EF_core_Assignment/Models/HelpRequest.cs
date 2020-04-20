using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EF_core_Assignment.Models
{
    public class HelpRequest
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HelpRequestId { get; set; }

        public int studentAuId { get; set; }
        public Student Student { get; set; }

        public int exerciseId { get; set; }
        public Exercise Exercise { get; set; }

        public int teacherAuId { get; set; }
        public Teacher Teacher { get; set; }

        public int courseId { get; set; }
        public Course Course { get; set; }

        public override string ToString()
        {
            return string.Format($"Course({teacherAuId}, {courseId})");
        }


    }
}