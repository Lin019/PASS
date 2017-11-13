using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PASS.Models
{
    //會員資料
    public class Member
    {
        public string _id { get; set; }
        public string _memberPassword { get; set; }
        public string _memberName { get; set; }
        public string _memberEmail { get; set; }
        public int _memberType { get; set; }

        public Member(string id, string password, string name, string email, int type)
        {
            _id = id;
            _memberPassword = password;
            _memberName = name;
            _memberEmail = email;
            _memberType = type;
        }
    }
}