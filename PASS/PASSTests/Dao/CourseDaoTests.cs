using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using PASS.Models;
using System;
using System.Collections.Generic;
using System.Web.Configuration;

namespace PASS.Dao.Tests
{
    [TestClass()]
    public class CourseDaoTests
    {
        CourseDao _courseDao = new CourseDao();
        [TestMethod()]
        public void GetOneCourseTATest()
        {
            List<IdAndName> TAs = new List<IdAndName>();
            TAs = _courseDao.GetOneCourseTA(1);
            Assert.AreEqual("103590038", TAs[0]._id);
            Assert.AreEqual("SM", TAs[0]._memberName);
        }

        [TestMethod()]
        public void GetOneInstructorCourseTest()
        {
            List<Course> courses = _courseDao.GetOneInstructorCourse("000590087");
            Assert.AreEqual(courses[0]._courseID, 2);
            Assert.AreEqual(courses[0]._courseDescription, "本課程目標旨在介紹學生軟體工程的概念，並瞭解相關的技術與應用。學生將會學習，開發一個高品質軟體所需要的軟體工程原理、觀念、方法、技術、與步驟。本課程將介紹下列主題：專案流程、專案管理與規劃、.系統需求分析、系統設計方法、系統測試與驗證、系統維護方法等。");
            Assert.AreEqual(courses[0]._courseName, "軟體工程");
            Assert.AreEqual(courses[0]._instructorID, "000590087");
        }

        [TestMethod()]
        public void CreateOneCourseTest()
        {
            _courseDao.CreateOneCourse("測試課程名稱", "測試課程描述", "000590087");
            List<Course> courses = _courseDao.GetOneInstructorCourse("000590087");
            for (int i = 0; i < courses.Count; i++)
            {
                if (courses[i]._courseName == "測試課程名稱" && courses[i]._courseDescription == "測試課程描述" && courses[i]._instructorID == "000590087")
                {
                    _courseDao.DeleteOneCourse(courses[i]._courseID);
                    Assert.AreEqual(1, 1);
                    break;
                }
                if (i == courses.Count - 1)
                {
                    Assert.Fail();
                }
            }
        }

        [TestMethod()]
        public void SetOneCourseTATest()
        {
            try
            {
                //_courseDaoTest.SetOneCourseTA("1", "103590032");
                //_courseDaoTest.SetOneCourseTA("1", "103590032");
                _courseDao.SetOneCourseTA(1, "000590087");
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
                _courseDao.DeleteCourseTA(1, "103590032");
                _courseDao.DeleteCourseTA(1, "103590032");
            }
            catch (Exception e)
            {
                Assert.AreEqual("TA not exists", e.Message.ToString());
            }
        }

        [TestMethod()]
        public void GetOneCourseStudentsTest()
        {
            List<IdAndName> idName = _courseDao.GetOneCourseStudents(1);
            Assert.AreEqual("103590023", idName[0]._id);
            Assert.AreEqual("LAI", idName[0]._memberName);
            Assert.AreEqual("103590034", idName[1]._id);
            Assert.AreEqual("BONIS", idName[1]._memberName);
        }

        [TestMethod()]
        public void GetOneStudentElectiveTest()
        {
            _courseDao.GetOneStudentElective("103590023");
            Assert.Fail();
        }
    }
}