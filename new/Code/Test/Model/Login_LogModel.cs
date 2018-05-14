using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Login_LogModel
    {
         #region Basic Property
		 
           public int LogId { get; set; }
           public DateTime LoginTime { get; set; }
           public string LoginIp { get; set; }
           public string UserName { get; set; }
           
           public string Notes { get; set; }
         #endregion 
    }
}
