using Microsoft.VisualStudio.TestTools.UnitTesting;
using PASS.Services;
using PASS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PASS.Dao;

namespace PASS.Services.Tests
{
    [TestClass()]
    public class AssignmentServiceTests
    {
        [TestMethod()]
        public void AssignmentServiceTest()
        {
            CourseService newCourse = new CourseService();
            CourseDao courseDao = new CourseDao();

            courseDao.CreateOneCourseforTEST(999,"123", "1230", "123");// 新增課程
            Course course = newCourse.GetOneCourse(999);

            AssignmentService assignmentTest = new AssignmentService();

            //新增作業
            assignmentTest.CreateAssignment("測試作業_1", "測試用作業你不應該在資料庫看到這段話", "zip", DateTime.Now, false, "999");
            List<Assignment> assignments = assignmentTest.GetOneCourseAssignment("999");

            Assert.AreEqual(assignments[0]._assignmentName, "測試作業_1");
            Assert.AreEqual(assignments[0]._assignmentDescription, "測試用作業你不應該在資料庫看到這段話");

            assignmentTest.UpdateAssignment(assignments[0]._assignmentId, "測試作業_2", assignments[0]._assignmentDescription, assignments[0]._assignmentFormat, assignments[0]._assignmentDeadline, assignments[0]._assignmentLate);
            assignments.Add(assignmentTest.GetOneAssignment(assignments[0]._assignmentId));
            assignments.RemoveAt(0);

            Assert.AreEqual(assignments[0]._assignmentName, "測試作業_2");
            assignmentTest.DeleteAssignment(assignments[0]._assignmentId);
            try { assignments = assignmentTest.GetOneCourseAssignment(assignments[0]._assignmentId.ToString()); }
            catch (Exception e)
            {
                Assert.AreEqual("Assignment not found", e.Message);
            }
            courseDao.DeleteOneCourse(999);
            AssignmentDao assignmentDao =new AssignmentDao();
            assignmentDao.DeleteAssignment("測試作業_1");
        }
    }
}