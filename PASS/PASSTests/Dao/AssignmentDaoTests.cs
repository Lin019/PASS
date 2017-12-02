using Microsoft.VisualStudio.TestTools.UnitTesting;
using PASS.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASS.Dao.Tests
{
    [TestClass()]
    public class AssignmentDaoTests
    {
        AssignmentDao _assignmentDao = new AssignmentDao();
        [TestMethod()]
        public void GetOneAssignmentLateTest()
        {
            Assert.AreEqual(false, _assignmentDao.GetOneAssignmentLate(1024));
        }
    }
}