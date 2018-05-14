using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class WxKeysClassModel
    {

        public int Id { get; set; }
        public string ClassName { get; set; }
        public string Keys { get; set; }
        public int StatusId { get; set; }


        public string Types { get; set; }

        public string Contents { get; set; }
        public string Sid { get; set; }
        public string Mid { get; set; }
        public string Mp3 { get; set; }
        public string Pic { get; set; }
        public List<string> KeyList { get; set; }

        //public List<WxKeysModel> WxKeysList { get; set; }
    }





    public class WxKeysModel
    {
        public int Id { get; set; }
        public int ClassId { get; set; }

        public string Types { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string Pic { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Contents { get; set; }

        /// <summary>
        /// 单图文ID
        /// </summary>
        public string Sid { get; set; }
        /// <summary>
        /// 多图文ID
        /// </summary>
        public string Mid { get; set; }

        /// <summary>
        /// 语音文件
        /// </summary>
        public string Mp3 { get; set; }
    }
}
