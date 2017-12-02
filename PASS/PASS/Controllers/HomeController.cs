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

        public ActionResult CourseStudents()
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

        public JsonResult UpdateCourseDescription(string ID, string description)
        {
            Course course;
            try { course = _courseService.GetOneCourse(ID); }
            catch (Exception e)
            {
                return Json(e.Message);
            }
            try { _courseService.UpdateOneCourse(ID, course._courseName, description, course._instructorID); }
            catch (Exception e)
            {
                return Json(e.Message);
            }

            return Json("成功");
        }

        //新增TA
        public JsonResult NewCourseTA(string courseID, string TAID)
        {
            try { _courseService.SetOneCourseTA(courseID, TAID); }
            catch (Exception e)
            {
                return Json(e.Message);
            }
            return Json("成功！");
        }

        //刪除TA
        public JsonResult DeleteCourseTA(string courseID, string TAID)
        {
            try { _courseService.DeleteCourseTA(courseID, TAID); }
            catch (Exception e)
            {
                return Json(e.Message);
            }
            return Json("成功！");
        }

        //編輯TA
        public JsonResult UpdateCourseTA(string courseID, string TAID)
        {
            DeleteCourseTA(courseID, TAID);
            try { _courseService.SetOneCourseTA(courseID, TAID); }
            catch (Exception e)
            {
                return Json(e.Message);
            }
            return Json("成功！");
        }

        //取得課程卡片partial view (不確定要不要留)
        public PartialViewResult GetCourseCard()
        {
            return PartialView("_CourseCard");
        }

        //取得課程資料
        public JsonResult GetCourse(string courseId)
        {
            Course course;
            try { course = _courseService.GetOneCourse(courseId); }
            catch (Exception e)
            {
                return Json(e.Message);
            }

            ArrayList courseData = new ArrayList();
            courseData.Add(course);
            try { courseData.Add(_courseService.GetOneCourseTA(courseId)); }
            catch (Exception e)
            {
            }

            return Json(courseData);
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
        /// 回傳 課程物件 教授姓名 ID  TA學號和名字的 List Json檔
        /// 失敗回傳 FAIL
        /// </returns>
        public JsonResult Package(string CourseID)
        {
            Course course;
            Member instructor;
            IdAndName instructorIdName;
            List <IdAndName> TA;
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
                instructorIdName = new IdAndName(instructor._id, instructor._memberName);
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
            arrayList.Add(instructorIdName);
            arrayList.Add(TA);
            return Json(arrayList);
        }
    }
}
