﻿using System;
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
        public MemberDao _memberDao;
        public MemberService()
        {
            _memberDao = new MemberDao();
        }
        //取的全部member
        public List<Member> GetMemberInfo()
        {
            return _memberDao.GetMemberInfo();
        }
        //取得指定member
        public Member GetOneMemberInfo(int memberID)
        {
            return _memberDao.GetOneMemberInfo(memberID);
        }
        //修改個人資料
        public void SetOneMemberInfo(int id, string password, string name, string email)
        {
            _memberDao.SetOneMemberInfo(id, password, name, email);
            return;
        }
        
    }
}