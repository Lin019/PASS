using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using PASS.Dao;
using PASS.Models;
using System.Net.Mail;
using System.Net;

namespace PASS.Services
{
    public class AssignmentUploadService
    {
        private AssignmentDao _assignmentDao = new AssignmentDao();
        private SubmitDao _submitDao = new SubmitDao();
        //上傳作業
        public void UploadAssignment(string studentID,int assignmentID, HttpPostedFileBase file, HttpServerUtilityBase server)
        {
            Assignment assignment = _assignmentDao.GetOneAssignment(assignmentID);
            MemberDao member = new MemberDao();
            if (assignment._assignmentLate) throw new Exception("Time out");
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    if (Path.GetExtension(file.FileName) != "."+assignment._assignmentFormat.ToLower()) throw new Exception("File extension wrong");
                    var fileName = studentID+"_" + assignment._assignmentName + Path.GetExtension(file.FileName);
                    string MyDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) ;
                    string passPath = "\\PASS" + "\\" + assignment._courseId.ToString() + "\\" + assignment._assignmentName;
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

    }
}