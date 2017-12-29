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
            List<SubmitInfo> submitInfo = new List<SubmitInfo>();
            //actual 
            string studentID = "103590055";
            string submitName = "測試用_HW1";
            DateTime sumitDatetime = DateTime.Now;
            string submitURL = @"\PASS\2\測試用_HW1";
            int assignmentID = 1099;
            _submitDao.SubmitAssignment(studentID, submitName, sumitDatetime, submitURL, assignmentID);
            //act
            submitInfo = _submitDao.GetOneAssignmentSubmitList(1099);
            //assert
            Assert.AreEqual(submitInfo[0]._submitUrl, @"\PASS\2\測試用_HW1");
            Assert.AreEqual(submitInfo[0]._submitName, "測試用_HW1");
            Assert.AreEqual(_submitDao.GetOneAssignmentSubmitStudentList(1099)[0], "103590055");

            //刪除資料庫
            _submitDao.DeleteAssignmentSubmit(assignmentID.ToString());
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
            int assignmentID = 1099;
            _submitDao.SubmitAssignment(studentID, submitName, sumitDatetime, submitURL, assignmentID);
            //act
            submitInfo = _submitDao.GetOneAssignmentSubmitList(1099);
            //assert
            Assert.AreEqual(submitInfo[0]._submitUrl, @"\PASS\2\作業系統_HW1");
            Assert.AreEqual(submitInfo[0]._submitName, "計算機概論_HW1");

            //刪除資料庫
            _submitDao.DeleteAssignmentSubmit(assignmentID.ToString());

        }

        [TestMethod()]
        public void SetOneStudentAssignmentScoreTest()
        {
            int score = 100;
            SubmitDao _submitDao = new SubmitDao();
            string studentID = "103590055";
            int assignmentID = 1027;

            string act = _submitDao.SetOneStudentAssignmentScore(studentID, assignmentID, score);

            Assert.AreEqual(act, "Success");
        }

        [TestMethod()]
        public void DeleteAssignmentSubmitTest()
        {
            //actual 
            SubmitDao _submitDao = new SubmitDao();
            string studentID = "103590055";
            string submitName = "計算機概論_HW1";
            DateTime sumitDatetime = DateTime.Now;
            string submitURL = @"\PASS\2\作業系統_HW1";
            int assignmentID = 1099;
            _submitDao.SubmitAssignment(studentID, submitName, sumitDatetime, submitURL, assignmentID);
            //act
            _submitDao.GetOneAssignmentSubmitList(1099);
           
            _submitDao.DeleteAssignmentSubmit("1099");
        }
    }
}