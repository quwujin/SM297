using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class FileInfoModel
    {
         #region Basic Property
		 
           public int Id { get; set; }
           public string Hashdata { get; set; }
           public string FileName { get; set; }
           public string Type { get; set; }
           public string Size { get; set; }
           public string SaveName { get; set; }
           public int States { get; set; }
           public DateTime CreateTime { get; set; }
           public string Note { get; set; }

         #endregion 
    }
}
