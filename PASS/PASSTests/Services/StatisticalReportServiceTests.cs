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

        [TestMethod()]
        public void GetOneAssignmentScoreDistributedTest()
        {
            StatisticalReportService reportService = new StatisticalReportService();
            reportService.GetOneAssignmentScoreDistributed(1024);
            Assert.Fail();
        }

        [TestMethod()]
        public void GetCourseAssignmentsReportTest()
        {
            StatisticalReportService reportService = new StatisticalReportService();
            reportService.GetCourseAssignmentsReport(2);
            Assert.Fail();
        }
    }
}