using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace PASS.Controllers
{
    public class HomeController : Controller
    {
<<<<<<< HEAD
        public MemberService _memberService;
        public HomeController()
        {
            _memberService = new MemberService();
        }
=======
>>>>>>> 503610ed64b1b35ebbefba93eb4d4231ea2d38b4
        public ActionResult Index()
        {
            ViewBag.Title = "登入";

            return View();
        }

        public ActionResult Course()
        {
            ViewBag.Title = "我的課程";
            return View();
        }
        public ActionResult Assignment()
<<<<<<< HEAD
        {
            return View();
        }
        //設置會員資料
        [HttpPost]
        public JsonResult SetOneMemberInfo(string id, string password, string name, string email)
        {
            try { _memberService.SetOneMemberInfo(id, password, name, email); }
            catch (Exception e)
            {
                return Json(e.Message);
            }
            return Json("true");
        }

        //新增帳號
        [HttpPost]
        public JsonResult CreateUser(string id, string account, string password, string name, string email, int type)
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

        //取得會員資料
        [HttpPost]
        public JsonResult GetOneMemberInfo()
        {
            try
            {
                return Json(_memberService.GetOneMemberInfo());
            }
            catch (Exception e)
            {
                return Json(e.Message.ToString());
            }
=======
        {
            return View();
>>>>>>> 503610ed64b1b35ebbefba93eb4d4231ea2d38b4
        }
    }
}
