using System;
using MySql.Data.MySqlClient;
using System.Web.Configuration;
using PASS.Models;
using System.Collections.Generic;

namespace PASS.Dao
{
    public class AssignmentDao
    {
        private string GetDBConnectionString()
        {
            return WebConfigurationManager.ConnectionStrings["PASSDatabase"].ConnectionString;
        }

        /// <summary>
        ///  新增作業
        /// </summary>
        /// <param name="assignmentId"> 課程id</param>
        /// <param name="assignmentName"> 課程名子</param>
        /// <param name="assignmentDescription">課程敘述</param>
        /// <param name="assignmentFormat">課程格式</param>
        /// <param name="assignmentDeadline">作業期限</param>
        /// <param name="assignmentLate">作業缺交</param>
        /// <param name="courseId">課程ID</param>
        /// <returns>
        /// 成功回傳 success
        /// 失敗 fail
        /// </returns>
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

        //刪除作業
        public string DeleteAssignment(int assignmentId)
        {
            using (MySqlConnection connection = new MySqlConnection(GetDBConnectionString()))
            {
                using (MySqlCommand cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "DELETE FROM assignment WHERE assignment_ID = " + assignmentId;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        return "success";
                    }
                    catch (Exception e)
                    {
                        return "fail(" + e.Message + ")";
                    }
                }
            }
        }

        //讀取指定作業資訊
        public Assignment GetOneAssignment(int assignmentID)
        {
            string sql = "SELECT assignment_ID , assignment_Name,assignment_Description, assignment_Format, assignment_Deadline, assignment_Late, course_ID FROM assignment WHERE assignment_ID=@assignmentID";
            using (var connection = new MySqlConnection(GetDBConnectionString()))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@assignmentID", assignmentID);
                MySqlDataReader reader = cmd.ExecuteReader(); //execure the reader
                Assignment result = null;
                while (reader.Read())
                {
                    int id = reader.GetInt16(0);
                    string name = reader.GetString(1);
                    string description = reader.GetString(2);
                    string format = reader.GetString(3);
                    DateTime deadline = reader.GetDateTime(4);
                    bool late = reader.GetBoolean(5);
                    string courseID = reader.GetString(6);
                    result= new Assignment(id, name, description, format, deadline, late, courseID);
                }
                return result;
            }
        }
        //更改一作業內容
        public void UpdateOneAssignment(int id, string name, string description, string format, DateTime deadline, bool late, string courseID)
        {
            string sql = "UPDATE assignment SET assignment_Name=@name, assignment_Description=@description, assignment_Format=@format, assignment_Deadline=@deadline, assignment_late=@late, course_ID=@courseID WHERE assignment_ID=@id;";
            using (var connection = new MySqlConnection(GetDBConnectionString()))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@format", format);
                cmd.Parameters.AddWithValue("@deadline", deadline);
                cmd.Parameters.AddWithValue("@late", late);
                cmd.Parameters.AddWithValue("@courseID", courseID);
                if (cmd.ExecuteNonQuery() == 0) throw new Exception("Assignment not exist");
                return;
            }
        }

        /// <summary>
        /// 讀取這門課程所有作業
        /// </summary>
        /// <param name="courseID">課程ID</param>
        /// <returns>
        /// 成功回傳資料
        /// 失敗回傳 Course not found
        /// </returns>

        public List <Assignment> GetOneCourseAssignment(string courseID)
        {
            string sql = "SELECT * FROM assignment WHERE course_ID=@courseID";
            using (var connection = new MySqlConnection(GetDBConnectionString()))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@courseID", courseID);
                MySqlDataReader reader = cmd.ExecuteReader(); //execure the reader
                if (!reader.HasRows) throw new Exception("Course not found");
                List<Assignment> assignment = new List<Assignment>() ;
                while (reader.Read())
                {
                    int assignmentId = reader.GetInt16(0);
                    string name = reader.GetString(1);
                    string description = reader.GetString(2);
                    string format = reader.GetString(3);
                    DateTime deadline = reader.GetDateTime(4);
                    bool IsLate = reader.GetBoolean(5);
                    assignment.Add( new Assignment(assignmentId ,name, description, format, deadline, IsLate,courseID));
                }
                return assignment;
            }
        }
    }
}