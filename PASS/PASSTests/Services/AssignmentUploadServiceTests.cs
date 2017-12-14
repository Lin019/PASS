using Microsoft.VisualStudio.TestTools.UnitTesting;
using PASS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASS.Services.Tests
{
    [TestClass()]
    public class AssignmentUploadServiceTests
    {
        AssignmentUploadService _assignmentUploadService = new AssignmentUploadService();
        [TestMethod()]
        public void UnzipTest()
        {
            _assignmentUploadService.UnzipIntoFolder("103590038",1024);
            Assert.Fail();
        }
    }
}