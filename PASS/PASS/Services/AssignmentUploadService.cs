using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using PASS.Dao;
namespace PASS.Services
{
    public class AssignmentUploadService
    {
        private AssignmentDao _assignmentDao = new AssignmentDao();
        //上傳作業
        public void UploadAssignment(int assignmentID,HttpPostedFileBase file,HttpServerUtilityBase server)
        {
            if (_assignmentDao.GetOneAssignmentLate(assignmentID)) throw new Exception("Time out");
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(server.MapPath("~/FileUploads"), fileName);
                    file.SaveAs(path);
                }
            }
            else
                throw new Exception("File isn't selected");
        }
    }
}