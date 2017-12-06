using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Web.Configuration;
using PASS.Models;

namespace PASS.Dao
{
    public class SubmitDao
    {
        private string GetDBConnectionString()
        {
            return WebConfigurationManager.ConnectionStrings["PASSDatabase"].ConnectionString;
        }
        public void SubmitAssignment(string studentID, string submitName, DateTime sumitDatetime,string submitURL,int assignmentID)
        {
            string sql = "INSERT INTO submit (student_ID,submit_Name,submit_Datetime,submit_Url,assignment_ID) VALUES ( @studentID,@submitName, @submitDatetime, @submitUrl,@assignmentID)";
            using (var connection = new MySqlConnection(GetDBConnectionString()))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@studentID", studentID);
                cmd.Parameters.AddWithValue("@submitName", submitName);
                cmd.Parameters.AddWithValue("@submitDatetime", sumitDatetime);
                cmd.Parameters.AddWithValue("@submitUrl", submitURL);
                cmd.Parameters.AddWithValue("@assignmentID",assignmentID);
                if (cmd.ExecuteNonQuery() == 0) throw new Exception("Assignment not exist");
                return;
            }
        }

    }
}