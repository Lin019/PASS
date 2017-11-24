using PASS.Dao;

namespace PASS.Services
{
    public class AssignmentService
    {
        private AssignmentDao _assignmentDao;

        public AssignmentService()
        {
            _assignmentDao = new AssignmentDao();
        }

        public string DeleteAssignment(int assignmentID)
        {
            return _assignmentDao.DeleteAssignment(assignmentID);
        }
    }
}