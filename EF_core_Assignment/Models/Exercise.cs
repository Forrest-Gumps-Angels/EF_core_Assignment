using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EF_core_Assignment.Models
{
    public class Exercise
    {
        [MaxLength(64)]
        public string lecture { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int number { get; set; }


        [MaxLength(128)]
        public string help_where { get; set; }
        public bool open { get; set; }

        public int teacherAuId { get; set; }
        public Teacher Teacher { get; set; }

        public int studentAuId { get; set; }
        public Student Student { get; set; }

        public int courseID { get; set; }
        public Course Course { get; set; }


        // Do later 

        public override string ToString()
        {
            return string.Format($"Exercise({number}, {lecture}, {help_where})");
        }

    }
}

