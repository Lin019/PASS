using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using PASS.Services;
using PASS.Models;
using Newtonsoft.Json;
using System.Collections;

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

        //取得該教授所有的課程 (WIP: 2維Json)
        public JsonResult QueryInstructorCourses(string instructorID)
        {
            List<Course> courses;
            try { courses = _courseService.GetOneInstructorCourse(instructorID); }
            catch (Exception e)
            {
                return Json(e.Message);
            }
            
            return Json(new { } );
        }

        //取得課程卡片partial view (不確定要不要留)
        public PartialViewResult GetCourseCard()
        {
            return PartialView("_CourseCard");
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
        }

        /// <summary>
        /// [0]是課程物件 [1]是教授物件 [3]是TA物件
        /// </summary>
        /// <param name="Course">課程ID</param>
        /// <returns>
        /// 回傳 課程描述 教授名字 TA學號和名字的Json檔
        /// 失敗回傳 FAIL
        /// </returns>
        public JsonResult Package(string CourseID)
        {
            Course course;
            Member instructor;
            List<TA> TA;
            try
            {
                 course = _courseService.GetOneCourse(CourseID);//用課程ID找教授ID
            }
            catch
            {
                return Json("course fail");
            }
            try
            {
                 instructor = _memberService.GetOneMemberInfo(course._instructorID);//教授ID找教授名字
            }
            catch
            {
                return Json("instructor fail");
            }
            try
            {
                TA = _courseService.GetOneCourseTA(CourseID);//用課程找TA_id
            }
            catch
            {
                return Json("TA  fail");
            }
            ArrayList arrayList = new ArrayList();
            arrayList.Add(course);
            arrayList.Add(instructor);
            arrayList.Add(TA);
            return Json(arrayList);
        }
    }
}
