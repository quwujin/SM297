using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ConfigLogModel
    {
         #region Basic Property
		 
           public int Id { get; set; }
           public int ConfigId { get; set; }
           public int UserId { get; set; }
           public string Title { get; set; }
           public string Note { get; set; }
           public DateTime CTime { get; set; }

         #endregion 
    }
}
