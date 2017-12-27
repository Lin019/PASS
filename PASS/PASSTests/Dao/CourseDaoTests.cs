using Microsoft.VisualStudio.TestTools.UnitTesting;
using PASS.Dao;
using PASS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASS.Dao.Tests
{
    [TestClass()]
    public class CourseDaoTests
    {
        CourseDao _courseDaoTest = new CourseDao();

        [TestMethod()]
        public void CreateOneCourseTest()
        {
            _courseDaoTest.CreateOneCourseforTEST("99999", "測試課程3", "Do not delete this", "999999999");
            Course course = null;
            course = _courseDaoTest.GetOneCourse("99999");
            Assert.AreEqual(course._courseDescription, "Do not delete this");
            Assert.AreEqual(course._courseName, "測試課程3");
            Assert.AreEqual(course._instructorID, "999999999");
            _courseDaoTest.UpdateOneCourse("99999", "測試資料33", "Do not delete me", "999999999");
            course = _courseDaoTest.GetOneCourse("99999");
            Assert.AreEqual(course._courseDescription, "Do not delete me");
            Assert.AreEqual(course._courseName, "測試資料33");
            List<Course> teacherCourse = _courseDaoTest.GetOneInstructorCourse("999999999");
            Assert.AreEqual(teacherCourse[2]._courseDescription, "Do not delete me");
            Assert.AreEqual(teacherCourse[2]._courseName, "測試資料33");
            List<IdAndName> students = _courseDaoTest.GetOneCourseStudents("999");
            Assert.AreEqual("103590023", students[0]._id);
            List<Course> studentCourse = _courseDaoTest.GetOneStudentElective("103590023");
            Assert.AreEqual("測試用課程", studentCourse[0]._courseName);
            _courseDaoTest.SetOneCourseTA("99999", "103590038");
            Assert.AreEqual("103590038", _courseDaoTest.GetOneCourseTA("99999")[0]._id);
            _courseDaoTest.DeleteCourseTA("99999", "103590038");
            try { _courseDaoTest.GetOneCourseTA("99999"); }
            catch (Exception e)
            { Assert.AreEqual("TA not found", e.Message); }
            _courseDaoTest.DeleteOneCourse("99999");
            try { _courseDaoTest.GetOneCourse("99999"); }
            catch (Exception e)
            { Assert.AreEqual("Course not found", e.Message); }
        }

    }
}