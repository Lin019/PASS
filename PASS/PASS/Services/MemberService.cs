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
        private MemberDao _memberDao;
        public MemberService()
        {
            _memberDao = new MemberDao();
        }
        //取的全部member
        /*public List<Member> GetMemberInfo()
        {
            return _memberDao.GetMemberInfo();
        }*/
        //取得指定member
        public Member GetOneMemberInfo()
        {
            if (HttpContext.Current.Session["userID"] == null) throw new Exception("Not login yet");
            string memberID = HttpContext.Current.Session["account"].ToString();
            return _memberDao.GetOneMemberInfo(memberID);
        }

        //修改個人資料
        public void SetOneMemberInfo(string id, string password, string name, string email)
        {
            _memberDao.SetOneMemberInfo(id, password, name, email);
            return;
        }
        //登入
        public void Login(string id, string password)
        {
            HttpContext.Current.Session.Add("userID", id);
            try
            {
                Member loginMember = GetOneMemberInfo();
                if (loginMember._memberPassword != password)
                    throw new Exception("Incorrect Password");
            }
            catch(Exception e)
            {
                HttpContext.Current.Session.Remove("userID");
                throw e;
            }
            HttpContext.Current.Session.Add("isLogin",true);
        }
        
        public string  CreateUser(string id, string account, string password, string name, string email, int type)
        {
            return _memberDao.CreateUser(id, account, password, name, email, type);
        }
    }
}