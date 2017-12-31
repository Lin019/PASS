using PASS.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Web.UI;

namespace PASS.Services.Tests
{
    [TestClass()]
    public class AssignmentUploadServiceTests
    {
        AssignmentUploadService _assignmentUploadService = new AssignmentUploadService();
        [TestMethod()]
        public void UnzipTest()
        {
            _assignmentUploadService.UnzipIntoFolder(1024);
            Assert.Fail();
        }

        [TestMethod()]
        public void GetOneStudentSubmitStatusListTest()
        {
            _assignmentUploadService = new AssignmentUploadService();
            List<Pair> actual = _assignmentUploadService.GetOneStudentSubmitStatusList("103590034", 1);
            List<Pair> expect = new List<Pair>();
            expect.Add(new Pair(1024, "已繳交"));
            expect.Add(new Pair(1044, "未繳交"));
            expect.Add(new Pair(1046, "未繳交"));
            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(actual[i].First, expect[i].First);
                Assert.AreEqual(actual[i].Second, expect[i].Second);
            }
        }

        [TestMethod()]
        public void GetCourseSubmitTest()
        {
            _assignmentUploadService = new AssignmentUploadService();
            _assignmentUploadService.GetCourseSubmit(1,1024);
        }
    }
}