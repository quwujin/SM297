using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class UserInfoModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public int GroupId { get; set; }
        public string Email { get; set; }
        public DateTime LastLoginTime { get; set; }
        public DateTime CreateTime { get; set; }
        public int LoginCount { get; set; }
        public int StatusId { get; set; }
        public string Mob{get;set;}
        public string RealName { get; set; }
        public int LevelId { get; set; }
        public string GroupName { get; set; }
        public int PostId { get; set; }
        public int RoleId { get; set; }


    }
}
