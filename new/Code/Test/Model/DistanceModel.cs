using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class DistanceModel
    {
         #region Basic Property
		 
           public int Id { get; set; }
           public Decimal Lng { get; set; }
           public Decimal Lat { get; set; }
           public string Province { get; set; }
           public string City { get; set; }
           public string District { get; set; }
           public string Address { get; set; }
           public string StorName { get; set; }
           public string Channel { get; set; }
           public DateTime CreateTime { get; set; }
           public int StatusId { get; set; }
           public string Describe { get; set; }
           public string Note { get; set; }

         #endregion 
    }
}
