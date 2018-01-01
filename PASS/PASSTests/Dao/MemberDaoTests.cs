using Microsoft.VisualStudio.TestTools.UnitTesting;
using PASS.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASS.Dao.Tests
{
    [TestClass()]
    public class MemberDaoTests
    {
        MemberDao _memberDaoTest = new MemberDao();
        [TestMethod()]
        public void CreateUserTest()
        {
            //arrange
            string id = "103590032";
            string password = "dd";
            string name = "劉建宏";
            string email = "123@yahoo";
            int authority = 0;
            //act
            string actual = _memberDaoTest.CreateUser(id, password, name, email, authority);
            //assert
            Assert.AreEqual("success", actual);

            //act
            actual = _memberDaoTest.CreateUser(id, password, name, email, authority);
            //assert
            Assert.AreEqual("fail", actual);

            actual = _memberDaoTest.DeleteUser(id);//刪除保持資料庫乾淨*/
        }


        [TestMethod()]
        public void GetOneMemberInfoTest()
        {
            //arrange
            string id = "103590002";

            //act
            string name = _memberDaoTest.GetOneMemberInfo(id)._memberName;

            //assert 
            Assert.AreEqual(name, "洪子軒");
        }

        [TestMethod()]
        public void SetOneMemberInfoTest()
        {
            //arrange
            string id = "1035900g5";
            string password = "123456";
            string name = "陳東東";
            string email = "123@yahoo";

            //act
            _memberDaoTest.SetOneMemberInfo(id, password, name, email);
            //assert

        }
    }
}
