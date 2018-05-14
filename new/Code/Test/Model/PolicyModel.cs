using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class PolicyModel
    {
        public int Id{get;set;}
        public int SId{get;set;}
        public string Title{get;set;}
        public decimal Price{get;set;}
        public string Notes { get; set; }
    }
}
