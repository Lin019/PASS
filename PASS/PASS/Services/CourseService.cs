using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PASS.Dao;
using PASS.Models;

namespace PASS.Services
{
    public class CourseService
    {
        private CourseDao _courseDao;

        public CourseService()
        {
            _courseDao = new CourseDao();
        }
     
        //取得一教授授課課程或學生修課課程
        public List<Course> GetInstructorCourseOrStudentElective(string userID)
        {
            int authority = Convert.ToInt16(HttpContext.Current.Session["Authority"]);
            if (authority == 2) return _courseDao.GetOneStudentElective(userID);
            else if (authority == 3) return _courseDao.GetOneInstructorCourse(userID);
            else
                throw new Exception("User is not Instructor or Student");
        }
        //取得一課程
        public Course GetOneCourse(string courseID)
        {
            return _courseDao.GetOneCourse(courseID);
        }
        //新增一課程
        public void CreateOneCourse(string courseName, string courseDescription, string instructorID)
        {
            _courseDao.CreateOneCourse(courseName, courseDescription, instructorID);
        }
        //修改一課程
        public void UpdateOneCourse(string courseID, string courseName, string courseDescription, string instructorID)
        {
            _courseDao.UpdateOneCourse(courseID, courseName, courseDescription, instructorID);
        }
        //刪除一課程
        public void DeleteOneCourse(string courseID)
        {
            _courseDao.DeleteOneCourse(courseID);
        }
        //新增課程TA
        public void SetOneCourseTA(string courseID, string taID)
        {
            _courseDao.SetOneCourseTA(courseID, taID);
        }
        //取得一課程TA
        public List<IdAndName> GetOneCourseTA(string courseID)
        {
            return _courseDao.GetOneCourseTA(courseID);
        }
        //刪除一課程TA
        public void DeleteCourseTA(string courseID, string taID)
        {
            _courseDao.DeleteCourseTA(courseID, taID);
        }
    }
}