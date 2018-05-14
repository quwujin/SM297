using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class DzCodesModel
    {
        public int Id{get;set;}
        public string Codes{get;set;}
        public int Price{get;set;}
        public DateTime CTime{get;set;}
        public DateTime DTime { get; set; }
        public string Mob { get; set; }
        public string PCodes { get; set; }
        public int States { get; set; }
        public string Notes { get; set; }
    }
}
