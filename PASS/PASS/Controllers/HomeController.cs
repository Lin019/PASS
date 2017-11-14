using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using PASS.Services;


namespace PASS.Controllers
{
    public class HomeController : Controller
    {
        public MemberService _memberService = new MemberService();
        public ActionResult Index()
        {
<<<<<<< HEAD
            ViewBag.Title = "登入";

            return View();
        }

        public ActionResult Course()
        {
            ViewBag.Title = "我的課程";
            return View();
        }
        public ActionResult Assignment()
=======
            return View();
        }

        //新增帳號
        [HttpPost]
        public JsonResult CreateUser(string id, string account, string password, string name, string email, int type)
>>>>>>> 54a66c37b625e9f97fe300895684fb95e332c762
        {
            /*string id, string account, string password, string name, string email, int type*/
            /*string  id = "998";
            string account = "FUCK";
            string password = "123";
            string  name = "55";
            string email = "123@456";
            int type = 1;*/
            return Json(_memberService.CreateUser(id, account, password, name, email, type));
        }
        //測試 取得會員資料
        [HttpPost]
        public JsonResult GetMemberInfo()
        {

            return Json(_memberService.GetMemberInfo());
        }
    }
}
