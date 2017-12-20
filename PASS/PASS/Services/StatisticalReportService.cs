using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PASS.Dao;
using PASS.Models;


namespace PASS.Services
{
    public class StatisticalReportService
    {
        public AverageSubmitAndScore GetOneAssignmentReport(int assignmentID)
        {
            AverageSubmitAndScore result = new AverageSubmitAndScore();
            AssignmentDao assignmentDao = new AssignmentDao();
            int courseID =Convert.ToInt16(assignmentDao.GetOneAssignment(assignmentID)._courseId);
            CourseDao courseDao = new CourseDao();
            int studentCount = courseDao.GetOneCourseStudents(courseID.ToString()).Count();
            SubmitDao submitDao = new SubmitDao();
            int submitStudentCount = submitDao.GetOneAssignmentSubmitStudentList(assignmentID).Count();
            result.submitRate = studentCount / submitStudentCount;

        }
    }
}