using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Security
{
    public class IpAccessControlSettingModel
    {
         #region Basic Property
		 
           public int Id { get; set; }
           public string LogType { get; set; }
           public bool IPAccessEnable { get; set; }
           public int IPAccessMaxCount { get; set; }
           public int IPAccessControlTime { get; set; }
           public int IPAccessControlLockTime { get; set; }

         #endregion 
    }
}
