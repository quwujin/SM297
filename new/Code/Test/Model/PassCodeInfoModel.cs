using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class PassCodeInfoModel
    {
         #region Basic Property
		 
           public int Id { get; set; }
           public string Codes { get; set; }
           public string CodeIndex { get; set; }
           public string CodeName { get; set; }
           public string OpenId { get; set; }
           public int StatusId { get; set; }
           public DateTime CreateTime { get; set; }
           public DateTime ActiveTime { get; set; }
           public string Mob { get; set; }
           public string ActiveIp { get; set; }
           public int EventId { get; set; }
           public int CustomerId { get; set; }
           public string Notes { get; set; }

         #endregion 
    }
}
