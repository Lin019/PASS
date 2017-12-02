using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PASS.Models
{
    public class Course
    {
        public string _courseID { get; set; }
        public string _courseName { get; set; }
        public string _courseDescription { get; set; }
        public string _instructorID { get; set; }

        public Course (string courseID, string courseName, string courseDescription, string instructorID)
        {
            _courseID = courseID;
            _courseName = courseName;
            _courseDescription = courseDescription;
            _instructorID = instructorID;
        }
    }
}