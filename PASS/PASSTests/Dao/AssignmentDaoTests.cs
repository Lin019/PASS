using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace PASS.Dao.Tests
{
    [TestClass()]
    public class AssignmentDaoTests
    {
        AssignmentDao _assignmentDao = new AssignmentDao();
        [TestMethod()]
        public void CreateUserTest()
        {
            _assignmentDao.CreateAssignment(2, "作業系統_HW1", "排班計算", "ZIP", DateTime.Now, false, "1");

        }

        [TestMethod()]
        public void DeleteAssignmentTest()
        {
            _assignmentDao = new AssignmentDao();
            _assignmentDao.CreateAssignment(999, "UnitTest-Delete", "UnitTest-Delete", "UnitTest-Delete", DateTime.Now, false, "1");
            string actual = _assignmentDao.DeleteAssignment(999);
            Assert.AreEqual("success", actual);
        }
    }
}