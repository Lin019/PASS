using Microsoft.VisualStudio.TestTools.UnitTesting;
using PASS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;
using PASS.Models;

namespace PASS.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        HomeController _homeControllerTest = new HomeController();
        [TestMethod()]
        public void SetOneMemberInfoTest()
        {
            try
            {
                _homeControllerTest.SetOneMemberInfo("6", "id5","name", "id5@gmail.com");
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "ID not exist");
            }
            try
            {
                JsonResult b = _homeControllerTest.GetOneMemberInfo();
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "ID not found");
            }
            _homeControllerTest.SetOneMemberInfo("5", "321", "id5", "id5@gmail.com");
            JsonResult a = _homeControllerTest.GetOneMemberInfo();
            Member m = a.Data as Member;
            Assert.AreEqual(m._id, 5);
            Assert.AreEqual(m._memberPassword, "321");
            Assert.AreEqual(m._memberName, "id5");
            Assert.AreEqual(m._memberEmail, "id5@gmail.com");
        }

        [TestMethod()]
        public void GetOneMemberInfoTest()
        {
            try
            {
                JsonResult b = _homeControllerTest.GetOneMemberInfo();
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "ID not found");
            }
            _homeControllerTest.SetOneMemberInfo("5", "321", "id5", "id5@gmail.com");
            JsonResult a = _homeControllerTest.GetOneMemberInfo();
            Member m = a.Data as Member;
            Assert.AreEqual(m._id, 5);
            Assert.AreEqual(m._memberPassword, "321");
            Assert.AreEqual(m._memberName, "id5");
            Assert.AreEqual(m._memberEmail, "id5@gmail.com");
        }
    }
}