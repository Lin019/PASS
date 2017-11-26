using PASS.Dao;
using System;

namespace PASS.Services
{
    public class AssignmentService
    {
        private AssignmentDao _assignmentDao;

        public AssignmentService()
        {
            _assignmentDao = new AssignmentDao();
        }
        public string CreateAssignment(int assignmentId, string assignmentName, string assignmentDescription, string assignmentFormat, DateTime assignmentDeadline,bool assignmentLate, string courseId)
        {
            return _assignmentDao.CreateAssignment(assignmentId, assignmentName, assignmentDescription, assignmentFormat, assignmentDeadline, assignmentLate, courseId);
        }
        public string DeleteAssignment(int assignmentID)
        {
            return _assignmentDao.DeleteAssignment(assignmentID);
        }
    }
}