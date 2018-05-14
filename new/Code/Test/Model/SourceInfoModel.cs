using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class SourceInfoModel
    {
         #region Basic Property
		 
           public int Id { get; set; }
           public string Ip { get; set; }
           public string Pros { get; set; }
           public string City { get; set; }
           public string SourceUrl { get; set; }
           public DateTime Dtime { get; set; }
           public string Type { get; set; }

         #endregion 
    }
}
