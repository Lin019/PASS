using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PASS.Models
{
    public class IdAndName
    {
        public IdAndName(string id ,string name)
        {
            _id = id;
            _memberName = name;
        }
        
        public  string _id { get; set; }
        public string _memberName { get; set; }
    }
    
}