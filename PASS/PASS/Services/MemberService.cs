using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PASS.Dao;
using PASS.Models;

namespace PASS.Services
{
    //會員系統服務
    public class MemberService
    {
        public MemberDao _memberDao = new MemberDao();

        public List<Member> GetMemberInfo()
        {
            return _memberDao.GetMemberInfo();
        }
        public string  CreateUser(string id, string account, string password, string name, string email, int type)
        {
            return _memberDao.CreateUser(id, account, password, name, email, type);
        }
    }
}