using PASS.Dao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using PASS.Models;
using System.Collections.Generic;

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
            int assignmentId = 2;
            string name = "作業系統_HW1";
            string fileFeatures = "教訓金刀";
            string fileFormat = "Zip;";
            DateTime dateTime = DateTime.Now;
            bool assignmentLate = false;
            string courseId = "1";

            //act
            string actual = _assignmentDao.CreateAssignment(assignmentId, name, fileFeatures, fileFormat, dateTime, assignmentLate, courseId);

            //assert
            Assert.AreEqual(actual, "success");

            //act
            actual = _assignmentDao.CreateAssignment(assignmentId, name, fileFeatures, fileFormat, dateTime, assignmentLate, courseId);

            //assert
            Assert.AreEqual(actual, "fail");

            _assignmentDao.DeleteAssignment(2);


        }

        [TestMethod()]
        public void DeleteAssignmentTest()
        {
            _assignmentDao = new AssignmentDao();
            _assignmentDao.CreateAssignment(999, "UnitTest-Delete", "UnitTest-Delete", "UnitTest-Delete", DateTime.Now, false, "1");
            string actual = _assignmentDao.DeleteAssignment(999);
            Assert.AreEqual("success", actual);
        }

        [TestMethod()]
        public void GetOneAssignmentTest()
        {
            Assignment result = _assignmentDao.GetOneAssignment(1);
            Assert.AreEqual("軟工_HW1", result._assignmentName);
            Assert.AreEqual("要你命作業一", result._assignmentDescription);
            Assert.AreEqual("PDF", result._assignmentFormat);
            Assert.AreEqual(Convert.ToDateTime("2017/11/29 23:59:59"), result._assignmentDeadline);
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
            Assert.AreEqual(actual[0]._assignmentId.ToString(), "1");
        }

        public void UpdateOneAssignmentTest()
        {
            _assignmentDao.UpdateOneAssignment(1, "軟工_HW2", "要你命作業二", "RAR", Convert.ToDateTime("2017/12/25 23:59:59"), true, "2");
            Assignment result = _assignmentDao.GetOneAssignment(1);
            Assert.AreEqual("軟工_HW2", result._assignmentName);
            Assert.AreEqual("要你命作業二", result._assignmentDescription);
            Assert.AreEqual("RAR", result._assignmentFormat);
            Assert.AreEqual(Convert.ToDateTime("2017/12/25 23:59:59"), result._assignmentDeadline);
            Assert.AreEqual(true, result._assignmentLate);
            Assert.AreEqual("2", result._courseId);
            _assignmentDao.UpdateOneAssignment(1, "軟工_HW1", "要你命作業一", "PDF", Convert.ToDateTime("2017/11/29 23:59:59"), false, "2");

        }
    }
}