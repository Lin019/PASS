using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Web.Configuration;
using PASS.Models;

namespace PASS.Dao
{
    public class CourseDao
    {
        //取得連接資料庫的string
        private string GetDBConnectionString()
        {
            return WebConfigurationManager.ConnectionStrings["PASSDatabase"].ConnectionString;
        }

        //取得一位教授授課課程
        public List<Course> GetOneInstructorCourse(string instructorID)
        {
            string sql = "SELECT course_ID, course_Name, course_Description, instructor_ID FROM course WHERE instructor_ID=@instructorID";
            using (var connection = new MySqlConnection(GetDBConnectionString()))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@instructorID", instructorID);
                MySqlDataReader reader = cmd.ExecuteReader(); //execure the reader
                if (!reader.HasRows) throw new Exception("Course not found");
                List<Course> courses = new List<Course>();
                while (reader.Read())
                {
                    string courseID = reader.GetString(0);
                    string name = reader.GetString(1);
                    string description = reader.GetString(2);
                    string instructor_ID = reader.GetString(3);
                    Course oneCourse = new Course(courseID, name, description, instructor_ID);
                    courses.Add(oneCourse);
                }
                return courses;
            }
        }
        //取得單一課程
        public Course GetOneCourse(string courseID)
        {
            string sql = "SELECT course_ID, course_Name, course_Description, instructor_ID FROM course WHERE course_ID=@courseID";
            using (var connection = new MySqlConnection(GetDBConnectionString()))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@courseID", courseID);
                MySqlDataReader reader = cmd.ExecuteReader(); //execure the reader
                if (!reader.HasRows) throw new Exception("Course not found");
                Course course =  null;
                while (reader.Read())
                {
                    string course_ID = reader.GetString(0);
                    string name = reader.GetString(1);
                    string description = reader.GetString(2);
                    string instructorID = reader.GetString(3);
                    course = new Course(courseID, name, description, instructorID);
                }
                return course;
            }
        }
        //新增一個課程
        public void CreateOneCourse(string courseName, string courseDescription, string instructorID)
        {
            string sql = "INSERT INTO course(course_Name, course_Description, instructor_ID) VALUES(@courseName, @courseDescription, @instructorID)";
            using (var connection = new MySqlConnection(GetDBConnectionString()))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@courseName", courseName);
                cmd.Parameters.AddWithValue("@courseDescription", courseDescription);
                cmd.Parameters.AddWithValue("@instructorID", instructorID);
                cmd.ExecuteNonQuery();
            }
        }
        //修改一個課程
        public void UpdateOneCourse(string courseID, string courseName, string courseDescription, string instructorID)
        {
            string sql = "UPDATE course SET course_Name=@courseName, course_Description=@courseDescription, instructor_ID=@instructorID WHERE course_ID=@courseID";
            using (var connection = new MySqlConnection(GetDBConnectionString()))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@courseID", courseID);
                cmd.Parameters.AddWithValue("@courseName", courseName);
                cmd.Parameters.AddWithValue("@courseDescription", courseDescription);
                cmd.Parameters.AddWithValue("@instructorID", instructorID);
                if (cmd.ExecuteNonQuery() == 0) throw new Exception("Course not exists");
            }
        }
        //刪除一課程
        public void DeleteOneCourse(string courseID)
        {
            string sql = "DELETE FROM course WHERE course_ID=@courseID";
            using (var connection = new MySqlConnection(GetDBConnectionString()))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@courseID", courseID);
                if (cmd.ExecuteNonQuery() == 0) throw new Exception("Course not exists");
            }
        }
        //設定一課程TA
        public void SetOneCourseTA(string courseID, string taID)
        {
            string sql = "INSERT INTO ta(ta_ID, course_ID) VALUES(@taID,@courseID)";
            using (var connection = new MySqlConnection(GetDBConnectionString()))
            {
                string checkSQL = "SELECT * FROM ta WHERE course_ID=@courseID AND ta_ID=@taID";
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                MySqlCommand cmd = new MySqlCommand(checkSQL, connection);
                cmd.Parameters.AddWithValue("@taID", taID);
                cmd.Parameters.AddWithValue("@courseID", courseID);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows) throw new Exception("TA already exists");
                reader.Close();
                checkSQL = "SELECT user_Authority FROM user WHERE user_ID=@taID";
                cmd = new MySqlCommand(checkSQL, connection);
                cmd.Parameters.AddWithValue("@taID", taID);
                reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    int authority = reader.GetInt16(0);
                    if (authority != 2) throw new Exception("User is not student");
                }
                reader.Close();
                cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@courseID", courseID);
                cmd.Parameters.AddWithValue("@taID", taID);
                cmd.ExecuteNonQuery();
            }
        }
        //取得單一課程TA
        public List<string> GetOneCourseTA(string courseID)
        {
            string sql = "SELECT user_ID, user_Name FROM user WHERE user_ID in (SELECT ta_ID FROM ta WHERE course_ID=@courseID)";
            using (var connection = new MySqlConnection(GetDBConnectionString()))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@courseID", courseID);
                MySqlDataReader reader = cmd.ExecuteReader(); //execure the reader
                if (!reader.HasRows) throw new Exception("TA not found");
                List<string> TAs = new List<string>();
                while (reader.Read())
                {
                    string ta = reader.GetString(0)+" "+ reader.GetString(1);
                    TAs.Add(ta);
                }
                return TAs;
            }
        }
        //刪除一課程之TA
        public void DeleteCourseTA(string courseID, string taID)
        {
            string sql = "DELETE FROM ta WHERE course_ID=@courseID AND ta_ID=@taID";
            using (var connection = new MySqlConnection(GetDBConnectionString()))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@courseID", courseID);
                cmd.Parameters.AddWithValue("@taID", taID);
                if (cmd.ExecuteNonQuery() == 0) throw new Exception("TA not exists");
            }
        }
    }
}