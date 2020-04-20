using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EF_core_Assignment.Models
{
    public class Assignment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AssignmentId { get; set; }

        public string AssignmentName { get; set; }

        public bool open { get; set; }


        public int courseId { get; set; }
        public Course Course { get; set; }

        public Teacher Teacher { get; set; }
        public int teacherAuId { get; set; }

        //Junction table between Student and Assignment with N-N relationship
        public List<HelpRequest_shadowtab> AssignmentReq { get; set; }

        




        public override string ToString()
        {
            return string.Format($"Assignment({AssignmentName}, {AssignmentId})");
        }

    }
}

