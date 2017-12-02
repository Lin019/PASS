using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace PASS.Dao
{
    public class AssignmentDao
    {

        private string GetDBConnectionString()
        {
            return WebConfigurationManager.ConnectionStrings["PASSDatabase"].ConnectionString;
        }

        public bool GetOneAssignmentLate(int assignmentID)
        {
            string sql = "SELECT assignment_Late FROM assignment WHERE assignment_ID=@ID;";
            using (var connection = new MySqlConnection(GetDBConnectionString()))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@ID", assignmentID);
                MySqlDataReader reader = cmd.ExecuteReader(); //execure the reader
                if (!reader.HasRows) throw new Exception("ID not found");
                bool ans = new bool();
                while(reader.Read())
                {
                    ans= reader.GetBoolean(0);
                }
                return ans;
            }
        }
    }
}