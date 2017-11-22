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
        AssignmentDao _AssignmentDao = new AssignmentDao();
        [TestMethod()]
        public void CreateUserTest()
        {

            _AssignmentDao.CreateAssignment(2, "作業系統_HW1", "排班計算", "ZIP", DateTime.Now, false, "1");

        }
    }
}