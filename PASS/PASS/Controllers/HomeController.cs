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
        public MemberService _memberService;
        public HomeController()
        {
            _memberService = new MemberService();
        }
        public ActionResult Index()
        {
            return View();
        }

        //設置會員資料
        [HttpPost]
        public JsonResult SetOneMemberInfo(int id, string password, string name, string email)
        {
            try { _memberService.SetOneMemberInfo(id, password, name, email); }
            catch (Exception e)
            {
                return Json(e.Message.ToString());
            }
            return Json("true");
        }
        //取得會員資料
        [HttpPost]
        public JsonResult GetOneMemberInfo()
        {
            
            try
            {
                _memberService.Login(23, 103590023, "103590038");
                return Json(_memberService.GetOneMemberInfo(5));
            }
            catch (Exception e)
            {
                return Json(e.Message.ToString());
            }
            //return Json("nothing");
        }
    }
}
