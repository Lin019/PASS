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
        public void GetOneCourseTATest()
        {
            List<string> TAs = new List<string>();
            TAs = _courseDaoTest.GetOneCourseTA("1");
            Assert.AreEqual("103590023 LAI", TAs[0]);
            Assert.AreEqual("103590038 SM", TAs[1]);
        }

        [TestMethod()]
        public void GetOneCourseTest()
        {
            Course course = null;
            course = _courseDaoTest.GetOneCourse("1");
            Assert.AreEqual(course._courseID, "1");
            Assert.AreEqual(course._courseDescription, "軟體老公公");
            Assert.AreEqual(course._courseName, "軟公");
            Assert.AreEqual(course._instructorID, "000590087");
        }

        [TestMethod()]
        public void GetOneInstructorCourseTest()
        {
            List<Course> courses = null;
            courses = _courseDaoTest.GetOneInstructorCourse("000590087");
            Assert.AreEqual(courses[0]._courseID, "1");
            Assert.AreEqual(courses[0]._courseDescription, "軟體老公公");
            Assert.AreEqual(courses[0]._courseName, "軟公");
            Assert.AreEqual(courses[0]._instructorID, "000590087");
        }

        [TestMethod()]
        public void UpdateOneCourseTest()
        {
            _courseDaoTest.UpdateOneCourse("1", "軟Aa@123工", "軟卵恆", "000590087");
            Course course = null;
            course = _courseDaoTest.GetOneCourse("1");
            Assert.AreEqual(course._courseID, "1");
            Assert.AreEqual(course._courseDescription, "軟卵恆");
            Assert.AreEqual(course._courseName, "軟Aa@123工");
            Assert.AreEqual(course._instructorID, "000590087");
        }

        [TestMethod()]
        public void DeleteOneCourseTest()
        {
            try
            {
                _courseDaoTest.DeleteOneCourse("1");
                _courseDaoTest.GetOneCourse("1");
            }
            catch (Exception e)
            {
                Assert.AreEqual("Course not found", e.Message.ToString());
            }

        }

        [TestMethod()]
        public void CreateOneCourseTest()
        {
            _courseDaoTest.CreateOneCourse("軟Aa@123", "軟體老公工", "000590087");
            Course course = null;
            course = _courseDaoTest.GetOneCourse("1");
            Assert.AreEqual(course._courseID, "1");
            Assert.AreEqual(course._courseDescription, "軟體老公工");
            Assert.AreEqual(course._courseName, "軟Aa@123");
            Assert.AreEqual(course._instructorID, "000590087");
        }

        [TestMethod()]
        public void SetOneCourseTATest()
        {
            try
            {
                //_courseDaoTest.SetOneCourseTA("1", "103590032");
                //_courseDaoTest.SetOneCourseTA("1", "103590032");
                _courseDaoTest.SetOneCourseTA("1", "000590087");
            }
            catch (Exception e)
            {
                //Assert.AreEqual("TA already exists", e.Message.ToString());
                Assert.AreEqual("User is not student", e.Message.ToString());
            }
        }

        [TestMethod()]
        public void DeleteCourseTATest()
        {
            try
            {
                _courseDaoTest.DeleteCourseTA("1", "103590032");
                _courseDaoTest.DeleteCourseTA("1", "103590032");
            }
            catch (Exception e)
            {
                Assert.AreEqual("TA not exists", e.Message.ToString());
            }
        }
    }
}