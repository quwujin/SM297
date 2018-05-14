using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class SessionModel
    {
        public int Id { get; set; }
        public string  OpenId { get; set; }
        public string  Mob { get; set; }
        public string  Code { get; set; }
        public string  OrderKey { get; set; }
        public string  dt { get; set; }
        public int States { get; set; }
        public string Nickname { get; set; }
        public string HeadImgurl { get; set; }
        public int FileId { get; set; }
        public string FileName { get; set; }

    }
}
