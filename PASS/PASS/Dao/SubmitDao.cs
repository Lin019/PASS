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
            string sql = null;
            try
            {
                if (GetOneSubmitInfo(studentID, assignmentID) != null)
                    sql = "UPDATE submit SET submit_Name=@submitName,submit_Datetime=@submitDatetime,submit_Url=@submitUrl WHERE (student_ID=@studentID AND assignment_ID=@assignmentID)";
            }
            catch(Exception e)
            {
                if (e.Message == "Submit not found")
                     sql = "INSERT INTO submit (student_ID,submit_Name,submit_Datetime,submit_Url,assignment_ID) VALUES ( @studentID,@submitName, @submitDatetime, @submitUrl,@assignmentID)";
                else
                    throw e;
            }
            using (var connection = new MySqlConnection(GetDBConnectionString()))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@studentID", studentID);
                cmd.Parameters.AddWithValue("@submitName", submitName);
                cmd.Parameters.AddWithValue("@submitDatetime", sumitDatetime);
                cmd.Parameters.AddWithValue("@submitUrl", submitURL);
                cmd.Parameters.AddWithValue("@assignmentID", assignmentID);
                if (cmd.ExecuteNonQuery() == 0) throw new Exception("Assignment not exist");
                return;
            }

        }
        public SubmitInfo GetOneSubmitInfo(string studentID,int assignmentID)
        {
            string sql = "SELECT student_ID, submit_name, submit_Datetime, submit_Url, submit_Score, assignment_ID FROM submit WHERE (student_ID=@studentID AND assignment_ID=@assignmentID)";
            using (var connection = new MySqlConnection(GetDBConnectionString()))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@studentID", studentID);
                cmd.Parameters.AddWithValue("@assignmentID", assignmentID);
                MySqlDataReader reader = cmd.ExecuteReader(); //execure the reader
                if (!reader.HasRows) throw new Exception("Submit not found");
                SubmitInfo submitInfo = null;
                while (reader.Read())
                {
                    string id = reader.GetString(0);
                    string name = reader.GetString(1);
                    DateTime dateTime = reader.GetDateTime(2);
                    string url = reader.GetString(3);
                    int score = reader.GetInt16(4);
                    string assignmentid = reader.GetString(5);
                    submitInfo = new SubmitInfo(id, name, dateTime, url, score, Convert.ToInt16(assignmentid));
                }
                return submitInfo;
            }
        }
        //取得一作業所有有交的學生
        public List<string> GetOneAssignmentSubmitStudentList(int assignmentID)
        {
            List<string> result = new List<string>();
            string sql = "SELECT student_ID FROM submit WHERE assignment_ID=@assignmentID";
            using (var connection = new MySqlConnection(GetDBConnectionString()))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@assignmentID", assignmentID);
                MySqlDataReader reader = cmd.ExecuteReader(); //execure the reader
                if (!reader.HasRows) throw new Exception("Assignment not submit yet");
                while (reader.Read())
                {
                    result.Add(reader.GetString(0));
                }
                return result;
            }
        }

    }
}