using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Operation_LogModel
    {
         #region Basic Property
		 
           public int Id { get; set; }
           public string Mobile { get; set; }
           public string OrderCode { get; set; }
           public int LStatus { get; set; }
           public int Status { get; set; }
           public string OperationType { get; set; }
           public string Description { get; set; }
           public DateTime CreateTime { get; set; }
           public DateTime UpdateTime { get; set; }
           public string UserName { get; set; }
           public string Remark { get; set; }
           public string HideContent { get; set; }

         #endregion 
    }
}
