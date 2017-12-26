using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using PASS.Dao;
using PASS.Models;
using System.Net.Mail;
using System.Net;
using System.Web.UI;

namespace PASS.Services
{
    public class AssignmentUploadService
    {
        private AssignmentDao _assignmentDao = new AssignmentDao();
        private SubmitDao _submitDao = new SubmitDao();
        //上傳作業
        public void UploadAssignment(string studentID, int assignmentID, HttpPostedFileBase file)
        {
            Assignment assignment = _assignmentDao.GetOneAssignment(assignmentID);
            if (DateTime.Compare(assignment._assignmentDeadline, DateTime.Now) < 0)
            {
                if (!assignment._assignmentLate) throw new Exception("Time out");
            }
            MemberDao member = new MemberDao();
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    if (Path.GetExtension(file.FileName) != "." + assignment._assignmentFormat.ToLower()) throw new Exception("File extension wrong");
                    var fileName = studentID + "_" + assignment._assignmentName + Path.GetExtension(file.FileName);
                    string MyDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    string passPath = "\\PASS" + "\\" + assignment._courseId.ToString() + "\\" + assignment._assignmentName + "\\";
                    MyDocumentsPath += passPath;
                    if (!Directory.Exists(MyDocumentsPath))
                        Directory.CreateDirectory(@MyDocumentsPath);
                    var path = Path.Combine(MyDocumentsPath, fileName);
                    file.SaveAs(path);
                    SendAssignmentUploadMail(assignment._assignmentName, member.GetOneMemberInfo(studentID)._memberEmail);//寄確認信
                    _submitDao.SubmitAssignment(studentID, fileName, DateTime.Now, passPath, assignment._assignmentId);
                }
            }
            else
                throw new Exception("File isn't selected");
        }

        //下載作業資訊
        public SubmitInfo DownloadAssignmentInfo(string studentID, int assignmentID)
        {
            return _submitDao.GetOneSubmitInfo(studentID, assignmentID);
        }

        //將一作業所有繳交解壓縮到一資料夾，然後壓縮該資料夾並回傳其URL
        public string UnzipIntoFolder(int assignmentID)
        {
            List<string> studentID = new List<string>();
            studentID = _submitDao.GetOneAssignmentSubmitStudentList(assignmentID);
            string returnString = null;
            while (studentID.Count() > 0)
            {
                SubmitInfo submit = DownloadAssignmentInfo(studentID[0], assignmentID);
                string fileExtension = Path.GetExtension(submit._submitName).ToLower();
                if (fileExtension != ".zip" && fileExtension != ".rar") throw new Exception("File is not ZIP or RAR");
                string MyDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string filePath = MyDocumentsPath + submit._submitUrl + submit._submitName;
                String assetName = Path.GetFileNameWithoutExtension(submit._submitName);
                string zipPath = MyDocumentsPath + submit._submitUrl + "Unzip\\" + assetName;
                if (!File.Exists(filePath)) throw new Exception("File not found");
                if (Directory.Exists(zipPath)) Directory.Delete(@zipPath, true);
                if (!Directory.Exists(zipPath)) Directory.CreateDirectory(@zipPath);
                System.IO.Compression.ZipFile.ExtractToDirectory(filePath, zipPath);
                studentID.RemoveAt(0);
                if (studentID.Count() == 0)
                {
                    returnString = MyDocumentsPath + submit._submitUrl + "homework.zip";
                    if (File.Exists(returnString)) File.Delete(returnString);
                    System.IO.Compression.ZipFile.CreateFromDirectory(MyDocumentsPath + submit._submitUrl + "Unzip\\", returnString);
                    string unzipPath = MyDocumentsPath + submit._submitUrl + "Unzip\\";
                    if (Directory.Exists(unzipPath)) Directory.Delete(@unzipPath, true);
                }
            }
            return returnString;
        }
        //將一非壓縮檔作業壓縮後下載
        public string ZipOneAssignmentSubmit(int assignmentID)
        {
            string MyDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string returnString = null;
            string zipString = null;
            Assignment thisAssignment = _assignmentDao.GetOneAssignment(assignmentID);
            List<string> studentID = new List<string>();
            studentID = _submitDao.GetOneAssignmentSubmitStudentList(assignmentID);
            if (studentID.Count() > 0)
            {
                zipString = MyDocumentsPath + "\\PASS\\" + thisAssignment._courseId + "\\" + _assignmentDao.GetOneAssignment(assignmentID)._assignmentName;
                returnString = zipString+ ".zip";
            }
            else
                throw new Exception("No one submit");
            if (!Directory.Exists(zipString)) throw new Exception("Submit folder not exist");
            if (File.Exists(returnString)) File.Delete(@returnString);
            System.IO.Compression.ZipFile.CreateFromDirectory(zipString, returnString);
            return returnString;
        }

        /// <summary>
        /// 寄確認信
        /// </summary>
        /// <param name="assignmentName">作業名稱</param>
        /// <param name="studentEmail">學生EMAIL</param>
        private void SendAssignmentUploadMail(string assignmentName, string studentEmail)
        {
            String SenderGmail = "passntut@gmail.com";

            MailMessage Message = new MailMessage();//MailMessage(寄信者, 收信者) 
            Message.From = new MailAddress(SenderGmail); ;
            // 新增收件人 

            Message.To.Add(studentEmail);
            Message.IsBodyHtml = true;
            Message.BodyEncoding = System.Text.Encoding.UTF8;//E-mail編碼 	 
            Message.Subject = "您的 " + assignmentName + " 已繳交";//E-mail主旨 
            Message.Body = "[E]  " + DateTime.Now.ToString() + "您的" + assignmentName + "已繳交";//E-mail內容 
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.Credentials = new NetworkCredential(SenderGmail, "esdrevyehvutbvtb");
            client.EnableSsl = true;
            client.Send(Message);
        }

        //取得學生個人該課程所有作業繳交狀況
        public List<Pair> GetOneStudentSubmitStatusList(string studentID, int courseID)
        {
            List<Assignment> assignments = _assignmentDao.GetOneCourseAssignment(courseID.ToString());
            List<Pair> submitStatusList = new List<Pair>();
            for (int i = 0; i < assignments.Count; i++)
            {
                try
                {
                    _submitDao.GetOneSubmitInfo(studentID, assignments[i]._assignmentId);
                    submitStatusList.Add(new Pair(assignments[i]._assignmentId, "已繳交"));
                }
                catch(Exception ex)
                {
                    if (ex.Message == "Submit not found")
                    {
                        submitStatusList.Add(new Pair(assignments[i]._assignmentId, "未繳交"));
                    }
                    else
                    {
                        throw ex;
                    }
                }
            }
            return submitStatusList;
        }
    }
}