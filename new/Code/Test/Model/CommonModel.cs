using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class CommonModel
    {
        /// <summary>
        /// SQL 语句里面查询的 Text字段
        /// </summary>
        public string ReturnText { set; get; }
        /// <summary>
        /// SQL 语句里面查询的Value字段
        /// </summary>
        public string ReturnValue { set; get; }
        /// <summary>
        /// SQL 语句里面查询的表名
        /// </summary>
        public string TableName { set; get; }
        /// <summary>
        /// SQL 语句的条件(可以留空)
        /// </summary>
        public string WhereStr { set; get; }
        /// <summary>
        /// 排序字符串(可以留空)
        /// </summary>
        public string OrderStr { set; get; }
    }
}
