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
        private string _dbConnectionString;
        public MemberDao()
        {
            //取得連接資料庫的string
            _dbConnectionString = WebConfigurationManager.ConnectionStrings["PASSDatabase"].ConnectionString;
        }

        //從資料庫取會員資訊
        public List<Member> GetMemberInfo()
        {
            string sql = @"SELECT memberID AS ID,
	                              memberAccount AS Account,
	                              memberPassword as Password,
                                  memberName as Name,
                                  memberEmail as Email,
                                  memberType as Type
                        from member";
            using (var connection = new MySqlConnection(_dbConnectionString))
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
        }

        //取得單一會員資料
        public Member GetOneMemberInfo(int oneMemberID)
        {
           string sql = @"SELECT memberID AS ID,
	                              memberAccount AS Account,
	                              memberPassword as Password,
                                  memberName as Name,
                                  memberEmail as Email,
                                  memberType as Type
                        FROM member
                        WHERE memberID=" + oneMemberID.ToString();
            using (var connection = new MySqlConnection(_dbConnectionString))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                MySqlDataReader reader = cmd.ExecuteReader(); //execure the reader
                Member member = null;
                while (reader.Read())
                {
                    int id = reader.GetInt16(0);
                    string account = reader.GetString(1);
                    string password = reader.GetString(2);
                    string name = reader.GetString(3);
                    string email = reader.GetString(4);
                    int type = reader.GetInt16(5);
                    member = new Member(id, account, password, name, email, type);
                }
                return member;
            }
        }
    }

}