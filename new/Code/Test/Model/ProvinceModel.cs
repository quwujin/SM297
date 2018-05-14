using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ProvinceModel
    {
        public int ProvinceID { get; set; }
        public string CityName { get; set; }
        public int CityId { get; set; }
        public int ZipCode { get; set; }
        public int SupplierId { get; set; }
        public string ProvinceName { get; set; }
        public int DistrictID { get; set; }
        public string DistrictName { get; set; }
    }
}
