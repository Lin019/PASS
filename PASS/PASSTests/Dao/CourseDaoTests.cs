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

        /* [TestMethod()]
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
         */
        [TestMethod()]
        public void GetOneInstructorCourseTest()
        {
            //arrage
            string InstructorId = "000590002";
            //act
            List<Course> actual = _courseDaoTest.GetOneInstructorCourse(InstructorId);
            //assert
            Assert.AreEqual(actual[0]._courseName, "軟體工程");
        }

        [TestMethod()]
        public void GetOneStudentElectiveTest()
        {
            //arrage
            string studentId = "103590001";
            //act
            List<Course> actual = _courseDaoTest.GetOneStudentElective(studentId);
            //assert
            Assert.AreEqual(actual[0]._courseName, "軟體工程");
        }

        [TestMethod()]
        public void GetOneCourseTest()
        {
            //arrage
            string courseId = "2";
            //act
            Course actual = _courseDaoTest.GetOneCourse(courseId);
            //assert
            Assert.AreEqual(actual._courseName, "軟體工程");
        }



        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void UpdateOneCourseTest()
        {
            //arrage
            string courseId = "100002";
            string courseName = "測試用";
            string courseDescription = "勿刪";
            string InstructorId = "9999888";
            //actual
            _courseDaoTest.UpdateOneCourse(courseId, courseName, courseDescription, InstructorId);

            _courseDaoTest.UpdateOneCourse("9999999999555", courseName, courseDescription, InstructorId);
        }




        [TestMethod()]
        
        public void SetOneCourseTATest()
        {
            //arrange
            string courseID = "100002";
            string studentID = "103590038";

            //act
            _courseDaoTest.SetOneCourseTA(courseID, studentID);
        

            //保持資料庫乾淨
            _courseDaoTest.DeleteCourseTA(courseID, studentID);

            
        }

        [TestMethod()]
       
        public void GetOneCourseTATest()
        {
            //arrange
            string courseID = "100002";
            string studentID = "103590038";
            _courseDaoTest.SetOneCourseTA(courseID, studentID);

            //act
            List<IdAndName> act = _courseDaoTest.GetOneCourseTA(courseID);

            Assert.AreEqual(act[0]._id, studentID);

            _courseDaoTest.DeleteCourseTA(courseID, studentID);

        }

        [TestMethod()]
        public void DeleteCourseTATest()
        {
            //arrange
            string courseID = "100002";
            string studentID = "103590039";
            _courseDaoTest.SetOneCourseTA(courseID, studentID);

            //act
            _courseDaoTest.DeleteCourseTA(courseID, studentID);
        }


        [TestMethod()]
        public void GetOneCourseStudentsTest()
        {
            //arrange
            string courseID = "2";
            string studenID = "103590001";
            //act
            List<IdAndName> act = _courseDaoTest.GetOneCourseStudents(courseID);
            //assert
            Assert.AreEqual(act[0]._id, studenID);

        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void DeleteOneCourseTest()
        {
            //arrange
            _courseDaoTest.CreateOneCourseforTEST("55", "ccc", "ccc", "123456789");
            //act
            _courseDaoTest.DeleteOneCourse("55");
           _courseDaoTest.DeleteOneCourse("5564646");

        }

        [TestMethod()]
        public void CreateOneCourseforTESTTest()
        {
            //arrange
            _courseDaoTest.CreateOneCourseforTEST("55", "ccc", "ccc", "123456789");
            //act
            _courseDaoTest.DeleteOneCourse("55");
        }
    }
}