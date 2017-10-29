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
    }
}