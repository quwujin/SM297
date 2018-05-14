using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class BehaviorLogModel
    {
         #region Basic Property
		 
           public int Id { get; set; }
           public string Ip { get; set; }
           public int BehaviorType { get; set; }
           public string FailureReason { get; set; }
           public string LockValue { get; set; }
           public DateTime CreateTime { get; set; }
           public string Remark { get; set; }

         #endregion 
    }
}
