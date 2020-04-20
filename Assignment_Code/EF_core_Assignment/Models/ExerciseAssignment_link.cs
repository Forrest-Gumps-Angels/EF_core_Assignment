using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EF_core_Assignment.Models
{
    public class ExerciseAssignment_link
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ExerciseNumber { get; set; }
        public Exercise Exercise { get; set; }

        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; }
    }
}
