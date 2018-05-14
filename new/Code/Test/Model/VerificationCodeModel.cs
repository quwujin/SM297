using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class VerificationCodeModel
    {
         #region Basic Property
		 
           public int Id { get; set; }
           public string Code { get; set; }
           public string Mobile { get; set; }
           public string OpenId { get; set; }
           public DateTime CreateTime { get; set; }
           public string TimeStamp { get; set; }
           public string Ip { get; set; }
           public int StatusId { get; set; }
           public DateTime ExpiryTime { get; set; }
           public string Remark { get; set; }

         #endregion 
    }
}
