using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class OrderLogModel
    {
        public int Id { get; set; }
        public int OId { get; set; }
        public string OrderCode { get; set; }
        public string Mob { get; set; }
        public DateTime UpTime { get; set; }
        public int LStatus { get; set; }
        public int Status { get; set; }
        public string Notes { get; set; }
    }
}
