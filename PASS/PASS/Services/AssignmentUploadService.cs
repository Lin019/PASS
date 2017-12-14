using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using PASS.Dao;
using PASS.Models;
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

            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    if (Path.GetExtension(file.FileName) != "." + assignment._assignmentFormat.ToLower()) throw new Exception("File extension wrong");
                    var fileName = studentID + "_" + assignment._assignmentName + Path.GetExtension(file.FileName);
                    string MyDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    string passPath = "\\PASS" + "\\" + assignment._courseId.ToString() + "\\" + assignment._assignmentName+"\\";
                    MyDocumentsPath += passPath;
                    if (!Directory.Exists(MyDocumentsPath))
                        Directory.CreateDirectory(@MyDocumentsPath);
                    var path = Path.Combine(MyDocumentsPath, fileName);
                    file.SaveAs(path);
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
        //將作業解壓縮到一資料夾
        public string UnzipIntoFolder(string studentID,int assignmentID)
        {
            SubmitInfo submit = DownloadAssignmentInfo(studentID, assignmentID);
            string MyDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filePath = MyDocumentsPath + submit._submitUrl + submit._submitName;
            String assetName = Path.GetFileNameWithoutExtension(submit._submitName);
            string zipPath = MyDocumentsPath + submit._submitUrl+"Unzip\\" + assetName;
            //System.IO.Compression.ZipFile.CreateFromDirectory(filePath, zipPath);
            if (!Directory.Exists(filePath)) throw new Exception("File not found");
            if (!Directory.Exists(zipPath)) Directory.CreateDirectory(@zipPath);
            System.IO.Compression.ZipFile.ExtractToDirectory(filePath, zipPath);
            //↑有可能出現檔案重複Exception
            return zipPath;
        }
    }
}