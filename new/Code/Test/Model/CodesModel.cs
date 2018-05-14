using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class CodesModel
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public int PyId { get; set; }
        public DateTime StartDate{get;set;}
        public DateTime EndDate{get;set;}
        public string MCode{get;set;}
        public string MPassword{get;set;}
        public int IsUsed{get;set;}
        public DateTime CreateTime { get; set; }
        public DateTime UsedTime { get; set; }
        public string Mob { get; set; }
        public string Price { get; set; }
        public int Num { get; set; }
        public string Options { get; set; }//选项
        public string Types { get; set; }//类型
        public string Limits { get; set; }//限制
        public string Notes { get; set; }
    }
}
