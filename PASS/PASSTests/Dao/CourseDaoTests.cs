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
            _courseDaoTest.CreateOneCourseforTEST("1", "軟公", "軟體老公公", "000590087");
            List<IdAndName> TAs = new List<IdAndName>();
            TAs = _courseDaoTest.GetOneCourseTA("1");
            Assert.AreEqual("103590023", TAs[0]._id);
            Assert.AreEqual("SM", TAs[1]._memberName);
            _courseDaoTest.DeleteOneCourse("1");
        }

        [TestMethod()]
        public void GetOneInstructorCourseTest()
        {
            _courseDaoTest.CreateOneCourseforTEST("1", "軟公", "軟體老公公", "000590087");
            List<Course> courses = null;
            courses = _courseDaoTest.GetOneInstructorCourse("000590087");
            Assert.AreEqual(courses[0]._courseID, "1");
            Assert.AreEqual(courses[0]._courseDescription, "軟體老公公");
            Assert.AreEqual(courses[0]._courseName, "軟公");
            Assert.AreEqual(courses[0]._instructorID, "000590087");
            _courseDaoTest.DeleteOneCourse("1");
        }

        [TestMethod()]
        public void CreateOneCourseTest()
        {
            _courseDaoTest.CreateOneCourseforTEST("1", "軟工", "測資", "103590018");
            Course course = null;
            course = _courseDaoTest.GetOneCourse("1");
            Assert.AreEqual(course._courseID, "1");
            Assert.AreEqual(course._courseDescription, "測資");
            Assert.AreEqual(course._courseName, "軟工");
            Assert.AreEqual(course._instructorID, "103590018");
            _courseDaoTest.UpdateOneCourse("1", "軟體工學", "測資二代", "103590019");
            course = _courseDaoTest.GetOneCourse("1");
            Assert.AreEqual(course._courseID, "1");
            Assert.AreEqual(course._courseDescription, "測資二代");
            Assert.AreEqual(course._courseName, "軟體工學");
            Assert.AreEqual(course._instructorID, "103590019");
            _courseDaoTest.DeleteOneCourse("1");
            try { course = _courseDaoTest.GetOneCourse("1"); }
            catch (Exception e)
            {
                Assert.AreEqual("Course not found", e.Message.ToString());
            }
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

        [TestMethod()]
        public void GetOneCourseStudentsTest()
        {
            List<IdAndName> idName = _courseDaoTest.GetOneCourseStudents("1");
            Assert.AreEqual("103590023", idName[0]._id);
            Assert.AreEqual("LAI", idName[0]._memberName);
            Assert.AreEqual("103590034", idName[1]._id);
            Assert.AreEqual("BONIS", idName[1]._memberName);
        }

        [TestMethod()]
        public void GetOneStudentElectiveTest()
        {
            _courseDaoTest.GetOneStudentElective("103590023");
            Assert.Fail();
        }
    }
}