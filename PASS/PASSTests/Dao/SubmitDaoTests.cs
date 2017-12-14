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
    public class SubmitDaoTests
    {
        SubmitDao _submitDao = new SubmitDao();
        [TestMethod()]
        public void GetOneAssignmentSubmitStudentListTest()
        {
            Assert.AreEqual(_submitDao.GetOneAssignmentSubmitStudentList(1024)[0], "103590023");
        }
    }
}