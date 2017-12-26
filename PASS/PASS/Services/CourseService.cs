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
     
        //取得一教授授課課程
        public List<Course> GetOneInstructorCourse(string instructorID)
        {
            return _courseDao.GetOneInstructorCourse(instructorID);
        }
        //取得一學生修課課程
        public List<Course> GetOneStudentElective(string studentID)
        {
            return _courseDao.GetOneStudentElective(studentID);
        }
        //取得一課程
        public Course GetOneCourse(int courseID)
        {
            return _courseDao.GetOneCourse(courseID);
        }
        //新增一課程
        public void CreateOneCourse(string courseName, string courseDescription, string instructorID)
        {
            _courseDao.CreateOneCourse(courseName, courseDescription, instructorID);
        }
        //修改一課程
        public void UpdateOneCourse(int courseID, string courseName, string courseDescription, string instructorID)
        {
            _courseDao.UpdateOneCourse(courseID, courseName, courseDescription, instructorID);
        }
        //刪除一課程
        public void DeleteOneCourse(int courseID)
        {
            _courseDao.DeleteOneCourse(courseID);
        }
        //新增課程TA
        public void SetOneCourseTA(int courseID, string taID)
        {
            _courseDao.SetOneCourseTA(courseID, taID);
        }
        //取得一課程TA
        public List<IdAndName> GetOneCourseTA(int courseID)
        {
            return _courseDao.GetOneCourseTA(courseID);
        }
        //刪除一課程TA
        public void DeleteCourseTA(int courseID, string taID)
        {
            _courseDao.DeleteCourseTA(courseID, taID);
        }

        //取得一課程所有學生
        public List<IdAndName> GetOneCourseStudents(int courseID)
        {
            return _courseDao.GetOneCourseStudents(courseID);
        }
    }
}