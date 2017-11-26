using System;
using System.Web.Mvc;
using PASS.Services;



namespace PASS.Controllers
{
    public class HomeController : Controller
    {
        public MemberService _memberService;
        public AssignmentService _assignmentService;
        public HomeController()
        {
            _memberService = new MemberService();
        }
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
        }

        //新增作業
        [HttpPost]
        public JsonResult CreatAssignment(int assignmentId, string assignmentName, string assignmentDescription, string assignmentFormat, DateTime assignmentDeadline, bool assignmentLate, string courseId)
        {
            try
            {
                _assignmentService.CreateAssignment( assignmentId, assignmentName, assignmentDescription, assignmentFormat, assignmentDeadline,assignmentLate, courseId);
            }
            catch (Exception e)
            {
                return Json(e.Message.ToString());
            }
            return Json("true");
        }
        //刪除作業
        [HttpPost]
        public JsonResult DeleteAssignment(string id)
        {
            try
            {
                _assignmentService.DeleteAssignment(int.Parse(id));
            }
            catch (Exception e)
            {
                return Json(e.Message.ToString());
            }
            return Json("true");
        }
    }
}
