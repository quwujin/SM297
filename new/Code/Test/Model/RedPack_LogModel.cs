using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class RedPack_LogModel
    {
         #region Basic Property
		 
           public int Id { get; set; }
           public string Acid { get; set; }
           public string Hid { get; set; }
           public string Openid { get; set; }
           public string Orderid { get; set; }
           public string Money { get; set; }
           public DateTime Ctime { get; set; }
           public string Note { get; set; }

         #endregion 
    }
}
