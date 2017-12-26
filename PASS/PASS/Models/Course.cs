using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PASS.Models
{
    public class Course
    {
        public int _courseID { get; set; }
        public string _courseName { get; set; }
        public string _courseDescription { get; set; }
        public string _instructorID { get; set; }

        public Course (int courseID, string courseName, string courseDescription, string instructorID)
        {
            _courseID = courseID;
            _courseName = courseName;
            _courseDescription = courseDescription;
            _instructorID = instructorID;
        }
    }
}