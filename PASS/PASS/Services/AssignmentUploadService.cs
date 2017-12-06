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
        //上傳作業
        public void UploadAssignment(int assignmentID,HttpPostedFileBase file,HttpServerUtilityBase server)
        {
            Assignment assignment = _assignmentDao.GetOneAssignment(assignmentID);
            if (assignment._assignmentLate) throw new Exception("Time out");
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    string MyDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\PASS";
                    MyDocumentsPath += "\\" + assignment._courseId.ToString() + "\\" + assignment._assignmentName;
                    if (!Directory.Exists(MyDocumentsPath))
                        Directory.CreateDirectory(@MyDocumentsPath);
                    var path = Path.Combine(MyDocumentsPath, fileName);
                    file.SaveAs(path);
                }
            }
            else
                throw new Exception("File isn't selected");
        }
    }
}