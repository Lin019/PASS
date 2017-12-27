using Microsoft.VisualStudio.TestTools.UnitTesting;
using PASS.Controllers;
using PASS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace PASS.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        HomeController _homeController = new HomeController();
        [TestMethod()]
        public void PackageTest()
        {
            //arrage
            string courseId = "2";
            //act
            JsonResult actual = _homeController.Package(courseId);
            //assert

        }

        [TestMethod()]
        public void GetCourseSubmitTest()
        {
            JsonResult actual =_homeController.GetCourseSubmit("1", 1024);
            //assert
        }
        /*  [TestMethod()]
public void CreateUserTest()
{

string id = "9";
string account = "FUCK";
string password = "123";
string name = "55";
string email = "123@456";
int type = 1;
MemberService _memberService = new MemberService();
_memberService.CreateUser(id, account, password, name, email, type);
Assert.AreEqual(_memberService.CreateUser(id, account, password, name, email, type), "success");

}*/
    }
}