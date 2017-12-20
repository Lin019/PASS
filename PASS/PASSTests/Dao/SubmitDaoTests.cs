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
    public class SubmitDaoTests
    {
       
        [TestMethod()]
        public void GetOneAssignmentSubmitStudentListTest()
        {
            SubmitDao _submitDao = new SubmitDao();
            Assert.AreEqual(_submitDao.GetOneAssignmentSubmitStudentList(1024)[0], "103590023");
        }

        [TestMethod()]
        public void GetOneAssignmentSubmitListTest()
        {
            SubmitDao _submitDao = new SubmitDao();
            List<SubmitInfo> submitInfo = new List<SubmitInfo>();
            //actual 

            string studentID = "103590055";
            string submitName = "計算機概論_HW1";
            DateTime sumitDatetime = DateTime.Now;
            string submitURL = @"\PASS\2\作業系統_HW1";
            int assignmentID = 1027;
            _submitDao.SubmitAssignment(studentID, submitName, sumitDatetime, submitURL, assignmentID);
            //act
            submitInfo=_submitDao.GetOneAssignmentSubmitList(1027);
            //assert
            Assert.AreEqual(submitInfo[0]._submitUrl, @"\PASS\2\作業系統_HW1");
            Assert.AreEqual(submitInfo[0]._submitName, "計算機概論_HW1");

            

        }
    }
}