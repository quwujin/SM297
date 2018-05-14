using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Cj_LogModel
    {
         #region Basic Property
		 
           public int Id { get; set; }
           public string OrderCode { get; set; }
           public string OpenId { get; set; }
           public string Mob { get; set; }
           public string Jx { get; set; }
           public string Jp { get; set; }
           public int States { get; set; }
           public DateTime CTime { get; set; }
           public string Ip { get; set; }
           public string Pros { get; set; }
           public string City { get; set; }
           public string Adds { get; set; }
           public string Code { get; set; }
           public string Note { get; set; }

         #endregion 
    }
}
