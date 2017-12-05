using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PASS.Models
{
    public class Assignment
    {

        public int _assignmentId { get; set; }
        public string _assignmentName { get; set; }
        public string _assignmentDescription { get; set; }
        public string _assignmentFormat { get; set; }
        public DateTime _assignmentDeadline { get; set; }
        public bool _assignmentLate { get; set; }
        public string _courseId { get; set; }
        public Assignment(int assignmentId, string assignmentName, string assignmentDescription,string assignmentFormat,DateTime assignmentDeadline, bool assignmentLate, string courseId)
        {
            _assignmentId = assignmentId;
            _assignmentName = assignmentName;
            _assignmentDescription = assignmentDescription;
            _assignmentFormat = assignmentFormat;
            _assignmentDeadline = assignmentDeadline;
            _assignmentLate = assignmentLate;
            _courseId = courseId;
        }

    }
       
}