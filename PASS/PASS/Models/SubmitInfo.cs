using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PASS.Models
{
    public class SubmitInfo
    {
        public string _studentId { get; set; }
        public string _submitName { get; set; }
        public DateTime _submitTime { get; set; }
        public string _submitUrl { get; set; }
        public int _submitScore { get; set; }
        public int _assignmentID { get; set; }
        
        public SubmitInfo(string studentId, string submitName, DateTime submitTime, string submitUrl, int submitScore, int assignmentID)
        {
            _studentId = studentId;
            _submitName = submitName;
            _submitTime = submitTime;
            _submitUrl = submitUrl;
            _submitScore = submitScore;
            _assignmentID = assignmentID;
        }
    }
}