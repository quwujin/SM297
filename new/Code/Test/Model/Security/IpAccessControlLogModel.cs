using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Security
{
    public class IpAccessControlLogModel
    {
         #region Basic Property
		 
           public int Id { get; set; }
           public string IpAddress { get; set; }
           public string LogType { get; set; }
           public DateTime LockedDate { get; set; }
           public DateTime CreateOn { get; set; }
           public string LockValue { get; set; }
           public string LockReason { get; set; }
           public string SourceURL { get; set; }

         #endregion 
    }
}
