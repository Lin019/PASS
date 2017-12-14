using System;
using System.Web.Mvc;
using PASS.Services;
using PASS.Models;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace PASS.Controllers
{
    public class HomeController : Controller
    {
        public MemberService _memberService;
        public CourseService _courseService;
        public AssignmentService _assignmentService;
        public AssignmentUploadService _assignmentUploadService;
        public HomeController()
        {
            _memberService = new MemberService();
            _courseService = new CourseService();
            _assignmentUploadService = new AssignmentUploadService();
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

        //取得該教授所有的課程
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

        public JsonResult GetOneCourseStudents(string courseID)
        {
            List<IdAndName> students;
            try { students = _courseService.GetOneCourseStudents(courseID); }
            catch (Exception e)
            {
                return Json(e.Message);
            }

            return Json(students);
        }

        //更新課程資訊
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
                return Json("新增失敗，原因：" + e.Message + "\n課程ID：" + courseID + "，TA ID：" + TAID);
            }
            return Json("新增成功！");
        }

        //刪除TA
        public JsonResult DeleteCourseTA(string courseID, string TAID)
        {
            try { _courseService.DeleteCourseTA(courseID, TAID); }
            catch (Exception e)
            {
                return Json("刪除失敗，原因：" + e.Message + "\n課程ID：" + courseID + "，TA ID：" + TAID);
            }
            return Json("刪除成功！");
        }

        //編輯TA
        public JsonResult UpdateCourseTA(string courseID, string TAID1, string TAID2)
        {
            try { _courseService.DeleteCourseTA(courseID, TAID1); }
            catch (Exception e)
            {
                return Json("刪除失敗，原因：" + e.Message + "\n課程ID：" + courseID + "，TA ID：" + TAID1);
            }
            try { _courseService.SetOneCourseTA(courseID, TAID2); }
            catch (Exception e)
            {
                return Json("新增失敗，原因：" + e.Message + "\n課程ID：" + courseID + "，TA ID：" + TAID2);
            }
            return Json("編輯成功！");
        }

        //取得課程卡片partial view
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
            List<IdAndName> TA;
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
        //上傳作業
        /*[HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                _assignmentUploadService.UploadAssignment("103590038",1024, file, Server);
            }
            return RedirectToAction("Index");
        }*/
        //下載作業
        
        [HttpGet]
        public virtual ActionResult Download(string studentID, int assignmentID)
        {
            /*string studentID = "103590038";
            int assignmentID = 1024;*/
            SubmitInfo submit = _assignmentUploadService.DownloadAssignmentInfo(studentID, assignmentID);
            string MyDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filePath = MyDocumentsPath + submit._submitUrl + "\\" + submit._submitName;
            string mimeString = MimeMapping.GetMimeMapping(submit._submitName);
            return File(filePath, mimeString, submit._submitName);
        }
        [HttpGet]
        public ActionResult UnzipDownload1()
        {
            string studentID = "103590038";
            int assignmentID = 1024;
            SubmitInfo submit = _assignmentUploadService.DownloadAssignmentInfo(studentID, assignmentID);
            string MyDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filePath = MyDocumentsPath + submit._submitUrl + "\\" + submit._submitName;
            var fileStream = new FileStream(filePath, FileMode.Open);
            var zipInputStream = new ZipInputStream(fileStream);
            var entry = zipInputStream.GetNextEntry();
            string mimeString = MimeMapping.GetMimeMapping(entry.Name);
            return File(zipInputStream, mimeString, Url.Encode(entry.Name));
        }
    }
}
