using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using PASS.Dao;
using PASS.Models;
using PASS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
namespace PASS.Services.Tests
{
  
    [TestClass()]
    public class StatisticalReportServiceTests
    {
        private string GetDBConnectionString()
        {
            return WebConfigurationManager.ConnectionStrings["PASSDatabase"].ConnectionString;
        }

        CourseDao _courseDaoTest = new CourseDao();
        AssignmentDao _assignmentDao = new AssignmentDao();
        SubmitDao _submitDao = new SubmitDao();
        private void CreateAssignment(string assignment_ID,string assignmentName, string assignmentDescription, string assignmentFormat, DateTime assignmentDeadline, bool assignmentLate, string courseId)
        {
            string sql = @"INSERT INTO assignment (assignment_ID,assignment_Name,assignment_Description, assignment_Format,  assignment_Deadline,assignment_Late,course_ID) VALUES ( @assignment_ID,@assignmentName,@assignmentDescription, @assignmentFormat, @assignmentDeadline,@assignmentLate,@courseId)";
            using (var connection = new MySqlConnection(GetDBConnectionString()))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@assignment_ID", assignment_ID);
                cmd.Parameters.AddWithValue("@assignmentName", assignmentName);
                cmd.Parameters.AddWithValue("@assignmentDescription", assignmentDescription);
                cmd.Parameters.AddWithValue("@assignmentFormat", assignmentFormat);
                cmd.Parameters.AddWithValue("@assignmentDeadline", assignmentDeadline);
                cmd.Parameters.AddWithValue("@assignmentLate", assignmentLate);
                cmd.Parameters.AddWithValue("@courseId", courseId);
                cmd.ExecuteReader(); //execure the reader
            }
        }
        private void CreateData(string move)
        {   
            if (move =="")
            {
                _courseDaoTest.CreateOneCourseforTEST(123, "請刪", "請刪", "1035566558");
                CreateAssignment("2018", "123", "123", "123", DateTime.Now, true, "123");
                _submitDao.SubmitAssignment("103590038", "測試用hw1", DateTime.Now, "123", 2018);
                _submitDao.SubmitAssignment("103590018", "測試用hw1/2", DateTime.Now, "456", 2018);
                _submitDao.SetOneStudentAssignmentScore("103590038", 2018, 100);
                _submitDao.SetOneStudentAssignmentScore("103590018", 2018, 50);

            }
            if(move== "clean")
            {
                _courseDaoTest.DeleteOneCourse(123);
                _assignmentDao.DeleteAssignment(2018);
                
            }
            
        }
       
        
        [TestMethod()]
        public void GetOneAssignmentReportTest()
        {
           
            //arrange
            CreateData("");
            StatisticalReportService report = new StatisticalReportService();

            //act
            AverageSubmitAndScore act = new AverageSubmitAndScore();
              act  = report.GetOneAssignmentReport(2018);

            //assert
            Assert.AreEqual(100, act.submitRate);
            Assert.AreEqual(75, act.scoreRate);

            //刪除資料庫
            CreateData("clean");
        }

        [TestMethod()]
        public void GetOneAssignmentScoreDistributedTest()
        {
            //arrange
            CreateData("");
            StatisticalReportService report = new StatisticalReportService();
            //act
            List<AverageSubmitAndScore> act = new List<AverageSubmitAndScore>();
            act = report.GetCourseAssignmentsReport(123);
            //
            Assert.AreEqual(100, act[0].submitRate);
            CreateData("clean");
        }

        [TestMethod()]
        public void GetCourseAssignmentsReportTest()
        {
            //arrange
            CreateData("");
            StatisticalReportService report = new StatisticalReportService();
            //act
            ScoreDistributed act = new ScoreDistributed();
            act = report.GetOneAssignmentScoreDistributed(2018);
            
            //
            Assert.AreEqual(act._score91to100, 1);
            CreateData("clean");
        }


    }
}