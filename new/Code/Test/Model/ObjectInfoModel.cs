using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ObjectInfoModel
    {
         #region Basic Property
		 
           public int Id { get; set; }
           public string ObjectName { get; set; }
           public int IsTest { get; set; }
           public DateTime OnlineTime { get; set; }
           public DateTime OnOffTime { get; set; }
           public int MobCount { get; set; }
           public int OpenIdCount { get; set; }
           public int IpCount { get; set; }
           public string NoStartText { get; set; }
           public string CStext { get; set; }
           public string EndText { get; set; }
           public string WHtext { get; set; }
           public string Note { get; set; }

         #endregion 
    }
}
