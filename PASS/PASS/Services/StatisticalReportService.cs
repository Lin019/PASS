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
            int courseID = Convert.ToInt16(assignmentDao.GetOneAssignment(assignmentID)._courseId);
            CourseDao courseDao = new CourseDao();
            float studentCount = courseDao.GetOneCourseStudents(courseID.ToString()).Count();
            SubmitDao submitDao = new SubmitDao();
            float submitStudentCount = submitDao.GetOneAssignmentSubmitStudentList(assignmentID).Count();
            result.submitRate = (submitStudentCount / studentCount) * 100;
            List<SubmitInfo> submitList = submitDao.GetOneAssignmentSubmitList(assignmentID);
            int scoreSum = 0;
            while (submitList.Count() > 0)
            {
                scoreSum += submitList[0]._submitScore;
                submitList.RemoveAt(0);
            }
            result.scoreRate = scoreSum / submitStudentCount;
            return result;
        }
    }
}