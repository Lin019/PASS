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
        //取得一作業報表
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
        //取得一課程的報表
        public List<AverageSubmitAndScore> GetCourseAssignmentsReport(int courseID)
        {
            AssignmentDao assignmentDao = new AssignmentDao();
            List<Assignment> assignmentIDs = assignmentDao.GetOneCourseAssignment(courseID.ToString());
            int assignmentsCount = assignmentIDs.Count();
            List<AverageSubmitAndScore> result = new List<AverageSubmitAndScore>();
            while(assignmentIDs.Count()>0)
            {
                result.Add(GetOneAssignmentReport(assignmentIDs[0]._assignmentId));
                assignmentIDs.RemoveAt(0);
            }
            return result;
        }
    }
}