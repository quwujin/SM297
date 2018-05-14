using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class CrmUserModel
    {
        public int Id{get;set;}
        public string UName{get;set;}
        public string UPwd{get;set;}
        public string Codes{get;set;}
        public int PId{get;set;}
        public int CId{get;set;}
        public int States{get;set;}
        public int Types { get; set; }
        public string Notes { get; set; }
        public DateTime CTime { get; set; }
        public DateTime LTime { get; set; }
    }
}
