using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Web.Configuration;
using PASS.Models;

namespace PASS.Dao
{
    public class AssignmentDao
    {
        private string GetDBConnectionString()
        {
            return WebConfigurationManager.ConnectionStrings["PASSDatabase"].ConnectionString;
        }
        public string CreateAssignment(int assignmentId, string assignmentName, string assignmentDescription, string assignmentFormat, DateTime assignmentDeadline,bool assignmentLate, string courseId)
        {
            string sql = @"INSERT INTO assignment (assignment_ID , assignment_Name,assignment_Description, assignment_Format,  assignment_Deadline,assignment_Late,course_ID) VALUES (@assignmentID , @assignmentName,@assignmentDescription, @assignmentFormat, @assignmentDeadline,@assignmentLate,@courseId)";
            using (var connection = new MySqlConnection(GetDBConnectionString()))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@assignmentID", assignmentId);
                cmd.Parameters.AddWithValue("@assignmentName", assignmentName);
                cmd.Parameters.AddWithValue("@assignmentDescription", assignmentDescription);
                cmd.Parameters.AddWithValue("@assignmentFormat", assignmentFormat);
                cmd.Parameters.AddWithValue("@assignmentDeadline", assignmentDeadline);
                cmd.Parameters.AddWithValue("@assignmentLate", assignmentLate);
                cmd.Parameters.AddWithValue("@courseId", courseId);
                try
                {
                    cmd.ExecuteReader(); //execure the reader
                    return "success";
                }
                catch (Exception e)
                {
                    return "fail";
                    throw e;
                }
               
            }

        }
    }
}