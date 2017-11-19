using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using PASS.Services;
using PASS.Models;

namespace PASS.Controllers
{
    public class HomeController : Controller
    {
        public MemberService _memberService;
        public CourseService _courseService;

        public HomeController()
        {
            _memberService = new MemberService();
            _courseService = new CourseService();
        }

        public ActionResult Index()
        {
            ViewBag.Title = "登入";
            return View();
        }

        public ActionResult Site()
        {
            ViewBag.Title = "我的課程";
            return View();
        }

        public ActionResult Course()
        {
            return View();
        }

        //重新導向指定頁面
        public JsonResult RedirectPage(string data)
        {
            return Json(new { result = "Redirect", url = Url.Action(data, "Home") }, JsonRequestBehavior.AllowGet);
        }

        //取得該教授所有的課程
        public JsonResult QueryInstructorCourses(string instructorID)
        {
            List<Course> courses;
            try { courses = _courseService.GetOneInstructorCourse(instructorID); }
            catch (Exception e)
            {
                return Json(e.Message);
            } 

            return Json(courses);
        }

        //取得課程卡片partial view
        public ActionResult _CourseCard()
        {
            return PartialView();
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

        /// <summary>
        /// 新增帳號
        /// </summary>
        /// <param name="id">學號</param>
        /// <param name="password">密碼</param>
        /// <param name="name">姓名</param>
        /// <param name="email">電子郵件</param>
        /// <param name="type">身分</param>
        /// <returns>
        /// 成功回傳 json檔 "success"字串
        /// 失敗回傳 json檔 "fail" 字串 +拋例外
        /// </returns>
        [HttpPost]
        public JsonResult CreateUser(string id, string password, string name, string email, int authority)
        {

            return Json(_memberService.CreateUser(id, password, name, email, authority));
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
        }
    }
}
