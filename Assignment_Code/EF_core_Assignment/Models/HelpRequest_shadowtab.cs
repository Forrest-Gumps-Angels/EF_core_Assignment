using System;
using System.Collections.Generic;
using System.Text;

namespace EF_core_Assignment.Models
{
    public class HelpRequest_shadowtab
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; }
    }
}
