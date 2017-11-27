using PASS.Dao;
using PASS.Models;
using System;
using System.Collections.Generic;

namespace PASS.Services
{
    public class AssignmentService
    {
        private AssignmentDao _assignmentDao;

        
        public AssignmentService()
        {
            _assignmentDao = new AssignmentDao();
        }

        //新增作業
        public string CreateAssignment(int assignmentId, string assignmentName, string assignmentDescription, string assignmentFormat, DateTime assignmentDeadline,bool assignmentLate, string courseId)
        {
            return _assignmentDao.CreateAssignment(assignmentId, assignmentName, assignmentDescription, assignmentFormat, assignmentDeadline, assignmentLate, courseId);
        }

        //刪除作業
        public string DeleteAssignment(int assignmentID)
        {
            return _assignmentDao.DeleteAssignment(assignmentID);
        }

        //取得一門課所有作業
        public List<Assignment> GetOneCourseAssignment(string courseId)
        {
            return _assignmentDao.GetOneCourseAssignment(courseId);
        }
    }
}