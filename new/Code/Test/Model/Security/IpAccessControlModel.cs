using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Security
{
    public class IpAccessControlModel
    {
         #region Basic Property
		 
           public int Id { get; set; }
           public string IpAddress { get; set; }
           public string LogType { get; set; }
           public int AccessCount { get; set; }
           public bool Islocked { get; set; }
           public DateTime FistDateTime { get; set; }
           public DateTime UpdateDate { get; set; }

         #endregion 
    }
}
