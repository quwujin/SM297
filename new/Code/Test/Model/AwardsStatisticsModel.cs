using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class AwardsStatisticsModel
    {
         #region Basic Property
		 
           public int Id { get; set; }
           public int AwardsId { get; set; }
           public string AwardsName { get; set; }
           public string DateStamp { get; set; }
           public int YesterdayTotal { get; set; }
           public int TodayTotal { get; set; }
           public int AllTotal { get; set; }
           public int BackTotal { get; set; }
           public int AwardsType { get; set; }
           public string PrizeName { get; set; }
           public string Angle { get; set; }
           public string PresetValue { get; set; }
           public DateTime CreateTime { get; set; }
           public DateTime UpdateTime { get; set; }
           public int StatusId { get; set; }
           public string Remark { get; set; }

         #endregion 
    }
}
