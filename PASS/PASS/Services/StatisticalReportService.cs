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
        
        //取得一作業繳交率平均分數
        public AverageSubmitAndScore GetOneAssignmentReport(int assignmentID)
        {
            AverageSubmitAndScore result = new AverageSubmitAndScore();
            AssignmentDao assignmentDao = new AssignmentDao();
            int courseID = Convert.ToInt16(assignmentDao.GetOneAssignment(assignmentID)._courseId);
            CourseDao courseDao = new CourseDao();
            float studentCount = courseDao.GetOneCourseStudents(courseID).Count();
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

        //取得作業分數分布
        public ScoreDistributed GetOneAssignmentScoreDistributed(int assignmentID)
        {
            ScoreDistributed result = new ScoreDistributed();
            SubmitDao submitDao = new SubmitDao();
            List<SubmitInfo> submitStudentList = submitDao.GetOneAssignmentSubmitList(assignmentID);
            for(int i=0;i<submitStudentList.Count();i++)
            {
                if (submitStudentList[i]._submitScore < 0)
                    throw new Exception(submitStudentList[i]._submitName + " is not score yet");
                if (submitStudentList[i]._submitScore < 11) result._score0to10++;
                else if (submitStudentList[i]._submitScore < 21) result._score11to20++;
                else if (submitStudentList[i]._submitScore < 31) result._score21to30++;
                else if (submitStudentList[i]._submitScore < 41) result._score31to40++;
                else if (submitStudentList[i]._submitScore < 51) result._score41to50++;
                else if (submitStudentList[i]._submitScore < 61) result._score51to60++;
                else if (submitStudentList[i]._submitScore < 71) result._score61to70++;
                else if (submitStudentList[i]._submitScore < 81) result._score71to80++;
                else if (submitStudentList[i]._submitScore < 91) result._score81to90++;
                else if (submitStudentList[i]._submitScore < 101) result._score91to100++;
            }
            return result;
        }
    }
}