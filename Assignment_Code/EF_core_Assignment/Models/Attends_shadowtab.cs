using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EF_core_Assignment.Models
{
    public class Attends_shadowtab
    {
        public bool active { get; set; }
        public int semester { get; set; }

        public int studentAuId { get; set; }
        public Student Student { get; set; }

        public int courseId { get; set; }
        public Course Course { get; set; }
    }
}

