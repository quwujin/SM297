using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class WebApiInterface_LogModel
    {
         #region Basic Property
		 
           public int Id { get; set; }
           public int OrderId { get; set; }
           public string ResponseData { get; set; }
           public DateTime CreateTime { get; set; }
           public int StatusId { get; set; }
           public string Remark { get; set; }

         #endregion 
    }
}
