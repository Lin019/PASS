using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Web.Configuration;
using PASS.Models;

namespace PASS.Dao
{
    //處理會員資料庫
    public class MemberDao
    {
        //private string _dbConnectionString;
        public MemberDao()
        {
            //取得連接資料庫的string
            //_dbConnectionString = WebConfigurationManager.ConnectionStrings["PASSDatabase"].ConnectionString;
        }
        private string GetDBConnectionString()
        {
            return WebConfigurationManager.ConnectionStrings["PASSDatabase"].ConnectionString;
        }
        //從資料庫取會員資訊
        /*public List<Member> GetMemberInfo()
        {
            string sql = @"SELECT memberID AS ID,
	                              memberAccount AS Account,
	                              memberPassword as Password,
                                  memberName as Name,
                                  memberEmail as Email,
                                  memberType as Type
                        from member";
            using (var connection = new MySqlConnection(GetDBConnectionString()))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                List<Member> result = new List<Member>();
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                MySqlDataReader reader = cmd.ExecuteReader(); //execure the reader
                while (reader.Read())
                {
                    int id = reader.GetInt16(0);
                    string account = reader.GetString(1);
                    string password = reader.GetString(2);
                    string name = reader.GetString(3);
                    string email = reader.GetString(4);
                    int type = reader.GetInt16(5);
                    result.Add(new Member(id, account, password, name, email, type));
                }
                return result;
            }
        }*/

        //取得單一會員資料
        public Member GetOneMemberInfo(string memberID)
        {
           string sql = @"SELECT  User_ID AS ID,
	                              User_Password as Password,
                                  User_Name as Name,
                                  User_Email as Email,
                                  User_Authority as Type
                        FROM user
                        WHERE User_ID=" + memberID;
            using (var connection = new MySqlConnection(GetDBConnectionString()))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                MySqlDataReader reader = cmd.ExecuteReader(); //execure the reader
                if (!reader.HasRows) throw new Exception("ID not found");
                Member member = null;
                while (reader.Read())
                {
                    string id = reader.GetString(0);
                    string password = reader.GetString(1);
                    string name = reader.GetString(2);
                    string email = reader.GetString(3);
                    int type = reader.GetInt16(4);
                    member = new Member(id, password, name, email, type);
                }
                return member;
            }
        }
        
        //設定單一會員個人資訊
        public void SetOneMemberInfo(String id, string password, string name, string email)
        {
            string sql = "UPDATE user SET User_Password=@password, User_Name=@name, User_Email=@email WHERE User_ID=@ID;";
            using (var connection = new MySqlConnection(GetDBConnectionString()))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@ID", id);
                if (cmd.ExecuteNonQuery() == 0) throw new Exception("ID not exist");
                return;
            }
        }
        
        //新增帳號
        public string  CreateUser(string id,string account,string password,string name ,string email ,int type)
        {
            MySqlCommand cmd;
            //string sql = @"INSERT INTO member (memberID , memberAccount, memberPassword, memberName, memberEmail, memberType) VALUES (@ID,@Account,@Password,@Name,@Email,@Type)";
            string sql = @"INSERT INTO member (memberID , memberAccount, memberPassword, memberName, memberEmail, memberType) VALUES (@ID,@Account,@Password,@Name,@Email,@Type)";
            using (var connection = new MySqlConnection(GetDBConnectionString()))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                List<Member> result = new List<Member>();
                cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.AddWithValue("@Account", account);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Type",type);
                try
                {
                    cmd.ExecuteReader(); //execure the reader
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return e.Message.ToString();
                }
                return "success";
            }
            
        }
        
    }

}