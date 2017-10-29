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
            return View();
        }

        //測試 取得會員資料
        [HttpPost]
        public JsonResult GetMemberInfo()
        {
            return Json(_memberService.GetMemberInfo());
        }
    }
}
