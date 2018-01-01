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
        public void GetOneStudentSubmitStatusListTest()
        {
            _assignmentUploadService = new AssignmentUploadService();
            List<Pair> actual = _assignmentUploadService.GetOneStudentSubmitStatusList("103590034", 1);
            List<Pair> expect = new List<Pair>();
            Assert.AreEqual(actual[0].First, 1024);
            Assert.AreEqual(actual[0].Second, "已繳交");
            
        }

        [TestMethod()]
        public void GetCourseSubmitTest()
        {
            _assignmentUploadService = new AssignmentUploadService();
            _assignmentUploadService.GetCourseSubmit(1,1024);
        }
    }
}