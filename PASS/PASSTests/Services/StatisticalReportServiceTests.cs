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
    public class StatisticalReportServiceTests
    {
        [TestMethod()]
        public void GetOneAssignmentReportTest()
        {
            StatisticalReportService reportService = new StatisticalReportService();
            reportService.GetOneAssignmentReport(1024);
            Assert.Fail();
        }
    }
}