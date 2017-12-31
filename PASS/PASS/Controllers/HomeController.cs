using System;
using System.Web.Mvc;
using PASS.Services;
using PASS.Models;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Web.UI;

namespace PASS.Controllers
{
    public class HomeController : Controller
    {
        public MemberService _memberService;
        public CourseService _courseService;
        public AssignmentService _assignmentService;
        public AssignmentUploadService _assignmentUploadService;
        public StatisticalReportService _statisticalReportService;

        public HomeController()
        {
            _memberService = new MemberService();
            _courseService = new CourseService();
            _assignmentService = new AssignmentService();
            _assignmentUploadService = new AssignmentUploadService();
            _statisticalReportService = new StatisticalReportService();
        }

        public ActionResult Index()
        {
            ViewBag.Title = "登入";
            return View();
        }

        public ActionResult Assignment()
        {
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
            try { courses = _courseService.GetInstructorCourseOrStudentElective(instructorID); }
            catch (Exception e)
            {
                return Json(e.Message);
            }

            return Json(courses);
        }

        //顯示所有修課學生
        public JsonResult GetOneCourseStudents(int courseID)
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
        public JsonResult UpdateCourseDescription(int courseID, string description)
        {
            Course course;
            try { course = _courseService.GetOneCourse(courseID); }
            catch (Exception e)
            {
                return Json(e.Message);
            }
            try { _courseService.UpdateOneCourse(courseID, course._courseName, description, course._instructorID); }
            catch (Exception e)
            {
                return Json(e.Message);
            }

            return Json("成功");
        }

        //新增TA
        public JsonResult NewCourseTA(int courseID, string TAID)
        {
            try { _courseService.SetOneCourseTA(courseID, TAID); }
            catch (Exception e)
            {
                return Json("新增失敗，原因：" + e.Message + "\n課程ID：" + courseID + "，TA ID：" + TAID);
            }
            return Json("新增成功！");
        }

        //刪除TA
        public JsonResult DeleteCourseTA(int courseID, string TAID)
        {
            try { _courseService.DeleteCourseTA(courseID, TAID); }
            catch (Exception e)
            {
                return Json("刪除失敗，原因：" + e.Message + "\n課程ID：" + courseID + "，TA ID：" + TAID);
            }
            return Json("刪除成功！");
        }

        //編輯TA
        public JsonResult UpdateCourseTA(int courseID, string TAID1, string TAID2)
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
        public ActionResult _CourseCard()
        {
            return PartialView();
        }

        //顯示學生個人對該作業的繳交狀況
        public JsonResult GetOneAssignmentSubmitInfo(int assignmentID)
        {
            Assignment assignment;
            SubmitInfo submitInfo;
            string studentID;
            try { assignment = _assignmentService.GetOneAssignment(assignmentID); }
            catch (Exception e) { return Json("讀取失敗，原因：" + e.Message); }
            try { studentID = _memberService.GetOneMemberInfo()._id; }
            catch { return Json("請先登入"); }
            try { submitInfo = _assignmentUploadService.DownloadAssignmentInfo(studentID, assignmentID); }
            catch (Exception e) { return Json(e.Message); }

            ArrayList list = new ArrayList();
            list.Add(assignment);
            list.Add(submitInfo);

            return Json(list);
        }

        //取得所有學生繳交狀況
        public JsonResult GetStudentAllSubmitStatus(int courseID)
        {
            string studentID;

            try { studentID = _memberService.GetOneMemberInfo()._id; }
            catch { return Json("請先登入"); }

            try { return Json(_assignmentUploadService.GetOneStudentSubmitStatusList(studentID, courseID)); }
            catch (Exception e) { return Json(e.Message); }
        }

        //顯示該作業所有繳交狀況 (只能列出有交的)
        public JsonResult GetOneAssignmentAllSubmitInfo(int assignmentID)
        {
            Assignment assignment;
            try { assignment = _assignmentService.GetOneAssignment(assignmentID); }
            catch (Exception e) { return Json("讀取失敗，原因：" + e.Message); }

            int courseID;
            courseID = int.Parse(assignment._courseId);

            List<IdAndName> students;
            try { students = _courseService.GetOneCourseStudents(courseID); }
            catch (Exception e) { return Json(e.Message); }

            List<SubmitInfo> submits;
            try { submits = _assignmentUploadService.GetOneAssignmentSubmitList(assignmentID); }
            catch (Exception e) { return Json(e.Message); };

            return Json(submits);
        }

        //取得課程資料
        public JsonResult GetCourse(int courseId)
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
                //courseData.Add("讀取失敗，原因：" + e.Message);
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

        //驗證會員資料
        [HttpPost]
        public JsonResult VerifyOneMemberInfo(string userID)
        {
            try
            {
                return Json(_memberService.GetOneMemberInfo(userID));
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
        public JsonResult Package(int courseID)
        {
            Course course;
            Member instructor;
            IdAndName instructorIdName;
            List<IdAndName> TA;
            try
            {
                course = _courseService.GetOneCourse(courseID);//用課程ID找教授ID
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
                TA = _courseService.GetOneCourseTA(courseID);//用課程找TA_id
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

        //讀取作業
        public JsonResult GetOneCourseAssignments(string courseID)
        {
            List<Assignment> assignments;
            try
            {
                assignments = _assignmentService.GetOneCourseAssignment(courseID);
            }
            catch (Exception e) { return Json(e.Message.ToString()); }

            return Json(assignments);
        }

        //讀取作業卡片partial view
        public ActionResult _AssignmentCard()
        {
            return PartialView();
        }

        //更新作業內容
        public JsonResult UpdateAssignment(int ID, string name, string description, string format, string deadlineString, bool late)
        {
            DateTime deadline;
            try
            {
                deadline = Convert.ToDateTime(deadlineString);
            }
            catch { return Json("時間轉換失敗"); }
            try { _assignmentService.UpdateAssignment(ID, name, description, format, deadline, late); }
            catch (Exception e) { return Json(e.Message); }

            return Json("作業修改成功！");
        }

        //新增作業
        public JsonResult CreateAssignment(string name, string description, string format, string deadlineString, bool late, string courseID)
        {
            DateTime deadline;
            try
            {
                deadline = Convert.ToDateTime(deadlineString);
            }
            catch { return Json("時間轉換失敗"); }

            try
            {
                _assignmentService.CreateAssignment(name, description, format, deadline, late, courseID);
            }
            catch (Exception e)
            {
                return Json(e.Message.ToString());
            }
            return Json("新增作業：" + name + "成功");
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
                return Json("作業刪除失敗，原因：" + e.Message.ToString());
            }
            return Json("作業刪除成功！");
        }

        //上傳作業
        public ActionResult Upload(HttpPostedFileBase file, int assignmentID)
        {
            string studentID;

            if (file != null)
            {
                try { studentID = _memberService.GetOneMemberInfo()._id; }
                catch { return Json("請先登入"); }

                try { _assignmentUploadService.UploadAssignment(studentID, assignmentID, file); }
                catch (Exception e)
                {
                    Redirect("/Home/Assignment?ID=" + assignmentID + "&Type=0");
                }
            }

            Redirect("/Home/Assignment?ID=" + assignmentID + "&Type=0");
            return Redirect("/Home/Assignment?ID=" + assignmentID + "&Type=0");
        }

        //登入
        [HttpPost]
        public JsonResult Login(string id, string password)
        {
            //_memberService.CreateUser("103590098","william6931","test","4545",1);
            try { _memberService.Login(id, password); }
            catch (Exception e)
            {
                return Json(e.Message);
            }
            return Json("true");
        }
        
        //下載作業
        [HttpGet]
        public ActionResult Download(int assignmentID)
        {
            /*string studentID = "103590038";
            int assignmentID = 1024;*/
            string studentID;
            try { studentID = _memberService.GetOneMemberInfo()._id; }
            catch { return Json("請先登入"); }

            SubmitInfo submit = _assignmentUploadService.DownloadAssignmentInfo(studentID, assignmentID);
            string MyDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filePath = MyDocumentsPath + submit._submitUrl + "\\" + submit._submitName;
            string mimeString = MimeMapping.GetMimeMapping(submit._submitName);
            return File(filePath, mimeString, submit._submitName);
        }

        //解壓縮全部作業後壓縮成一ZIP下載
        [HttpGet]
        public ActionResult UnzipDownload1(int assignmentID)
        {
            string filePath = _assignmentUploadService.UnzipIntoFolder(assignmentID);
            string fileName = Path.GetFileName(filePath);
            string mimeString = MimeMapping.GetMimeMapping(filePath);
            return File(filePath, mimeString, fileName);
        }

        //所有作業壓縮ZIP後下載
        [HttpGet]
        public ActionResult ZipDownload1(int assignmentID)
        {
            string filePath = _assignmentUploadService.ZipOneAssignmentSubmit(assignmentID);
            string fileName = Path.GetFileName(filePath);
            string mimeString = MimeMapping.GetMimeMapping(filePath);
            return File(filePath, mimeString, fileName);
        }
        
        //新增課程
        [HttpPost]
        public JsonResult SetCourse(string courseName, string courseDescription, string instructorID)
        {
            try { _courseService.CreateOneCourse(courseName,courseDescription,instructorID); }
            catch (Exception e)
            {
                return Json(e.Message);
            }
            return Json("true");
        }

        //刪除課程
        public JsonResult DeleteCourse(int courseID)
        {
            try { _courseService.DeleteOneCourse(courseID); }
            catch (Exception e)
            {
                return Json(e.Message);
            }
            return Json("true");
        }

        //取得作業報表
        public JsonResult GetAssignmnetReport(int assignmentID)
        {
            AverageSubmitAndScore firstData;
            try { firstData = _statisticalReportService.GetOneAssignmentReport(assignmentID); }
            catch (Exception e) { return Json(e.Message); }

            ScoreDistributed secondData;
            try { secondData = _statisticalReportService.GetOneAssignmentScoreDistributed(assignmentID); }
            catch (Exception e) { return Json(e.Message); }

            ArrayList list = new ArrayList();
            list.Add(firstData);
            list.Add(secondData);

            return Json(list);
        }

        //取得課程報表
        public JsonResult GetCourseReport(int courseID)
        {
            try { return Json(_statisticalReportService.GetCourseAssignmentsReport(courseID)); }
            catch (Exception e) { return Json(e.Message); }
        }
        //取得學生個人該課程所有作業繳交狀況
        public JsonResult GetOneStudentSubmitStatusList(string courseId, int assignmentId)
        {
            return Json(_assignmentUploadService.GetOneStudentSubmitStatusList(courseId, assignmentId));
        }

        //顯示該作業所有學生繳交狀態，沒有就未繳交
        public JsonResult GetCourseSubmit(string courseId, int assignmentId)
        {
           return Json(_assignmentUploadService.GetCourseSubmit(courseId, assignmentId));
        }
    }
}