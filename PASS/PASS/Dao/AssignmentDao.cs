﻿using System;
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
    }
}