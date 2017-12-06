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
    public class MemberDaoTests
    {
        MemberDao _memberDaoTest = new MemberDao();
        [TestMethod()]
        public void CreateUserTest()
        {
            _memberDaoTest.CreateUser("", "", "", "", "", 1);  
        }
    }
}