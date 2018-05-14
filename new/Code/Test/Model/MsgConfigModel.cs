using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class MsgConfigModel
    {
         #region Basic Property
		 
           public int Id { get; set; }
           public int SupplierId { get; set; }
           public string MsgType { get; set; }
           public string MsgTitle { get; set; }
           public string MsgTemp { get; set; }

         #endregion 
    }
}
