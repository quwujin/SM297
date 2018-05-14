using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model 
{
    public class ConfigModel
    {
         #region Basic Property

        public int Id { get; set; }
        public int TId { get; set; }
        public int KId { get; set; }
           public string Title { get; set; }
           public string Val { get; set; }
           public int Types { get; set; }
           public int States { get; set; }
           public int Sort { get; set; }
           public string Remark { get; set; }

         #endregion 
    }
}
