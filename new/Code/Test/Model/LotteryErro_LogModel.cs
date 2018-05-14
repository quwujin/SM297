using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class LotteryErro_LogModel
    {
         #region Basic Property
		 
           public int Id { get; set; }
           public string Ip { get; set; }
           public string Mob { get; set; }
           public string Code { get; set; }
           public string OpenId { get; set; }
           public string CreateTime { get; set; }
           public int Types { get; set; }
           public string LotErro { get; set; }
           public string Pros { get; set; }
           public string City { get; set; }
           public string Note { get; set; }

         #endregion 
    }
}
