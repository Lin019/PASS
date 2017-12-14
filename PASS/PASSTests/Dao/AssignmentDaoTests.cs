using PASS.Dao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using PASS.Models;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace PASS.Dao.Tests
{
    [TestClass()]
    public class AssignmentDaoTests
    {
        AssignmentDao _assignmentDao = new AssignmentDao();
        [TestMethod()]
        public void CreateUserTest()
        {
            //arrange
            string name = "作業系統_HW2";
            string fileFeatures = "教訓金刀";
            string fileFormat = "Zip";
            DateTime dateTime = DateTime.Now;
            bool assignmentLate = false;
            string courseId = "999";

            //act
            string actual = _assignmentDao.CreateAssignment(name, fileFeatures, fileFormat, dateTime, assignmentLate, courseId);

            //assert
            Assert.AreEqual(actual, "success");
            _assignmentDao.DeleteAssignment(name);

        }
       

        
       
        [TestMethod()]
        public void DeleteAssignmentTest()
        {
            _assignmentDao = new AssignmentDao();
            _assignmentDao.CreateAssignment("UnitTest-Delete", "UnitTest-Delete", "UnitTest-Delete", DateTime.Now, false, "1");
            string actual = _assignmentDao.DeleteAssignment("UnitTest-Delete");
            Assert.AreEqual("success", actual);
        }

        [TestMethod()]
        public void GetOneAssignmentTest()
        {

            Assignment result = _assignmentDao.GetOneAssignment(1011);
            Assert.AreEqual("作業系統_HW1", result._assignmentName);
            Assert.AreEqual("教訓金刀", result._assignmentDescription);
            Assert.AreEqual("Zip", result._assignmentFormat);
            Assert.AreEqual(Convert.ToDateTime("2017-11-29 19:06:07"), result._assignmentDeadline);
            Assert.AreEqual(false, result._assignmentLate);
            Assert.AreEqual("2", result._courseId);
        }

        [TestMethod()]

        public void GetOneCourseAssignmentTest()
        {
            //arrange
            string courseId = "2";
            //act 
            List<Assignment> actual = _assignmentDao.GetOneCourseAssignment(courseId);
            //assert
            Assert.AreEqual(actual[0]._assignmentId.ToString(), "1011");
        }
        [TestMethod()]
        public void UpdateOneAssignmentTest()
        {
            _assignmentDao.UpdateOneAssignment(1011, "軟工_HW2", "要你命作業二", "RAR", Convert.ToDateTime("2017/12/25 23:59:59"), true, "2");
            Assignment result = _assignmentDao.GetOneAssignment(1);
            Assert.AreEqual("軟工_HW2", result._assignmentName);
            Assert.AreEqual("要你命作業二", result._assignmentDescription);
            Assert.AreEqual("RAR", result._assignmentFormat);
            Assert.AreEqual(Convert.ToDateTime("2017/12/25 23:59:59"), result._assignmentDeadline);
            Assert.AreEqual(true, result._assignmentLate);
            Assert.AreEqual("2", result._courseId);
            _assignmentDao.UpdateOneAssignment(1, "軟工_HW1", "要你命作業一", "PDF", Convert.ToDateTime("2017/11/29 23:59:59"), false, "2");

        }

        [TestMethod()]
        public void CreateAssignmentTest()
        {
            string name = "作業系統_HW2";
            string fileFeatures = "教訓金刀";
            string fileFormat = "Zip";
            DateTime dateTime = DateTime.Now;
            bool assignmentLate = false;
            string courseId = "2";
            //act
            string actual = _assignmentDao.CreateAssignment(name, fileFeatures, fileFormat, dateTime, assignmentLate, courseId);
            Assert.Fail();
        }
    }
}