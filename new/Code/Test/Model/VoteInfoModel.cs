using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class VoteInfoModel
    {
         #region Basic Property
		 
           public int Id { get; set; }
           public string OrderCode { get; set; }
           public DateTime VoteDate { get; set; }
           public string VoteDt { get; set; }
           public string Ip { get; set; }
           public string VoteName { get; set; }
           public string VoteOpid { get; set; }
           public int VoteId { get; set; }
           public int States { get; set; }
           public string OpenId { get; set; }
           public string NickName { get; set; }
           public string NickImg { get; set; }

         #endregion 
    }
}
