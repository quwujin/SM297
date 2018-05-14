using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class DelayedAwardsModel
    {
         #region Basic Property
		 
           public int Id { get; set; }
           public int OrderId { get; set; }
           public int StatusId { get; set; }
           public DateTime CreateTime { get; set; }
           public DateTime DelayedTime { get; set; }
           public DateTime UpdateTime { get; set; }
           public string Remark { get; set; }

         #endregion 
    }
}
