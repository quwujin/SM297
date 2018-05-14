using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class PagerModel
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        public string SqlWhere { get; set; }

        /// <summary>
        /// 返回列
        /// </summary>
        public string ReturnFileds { get; set; }

        /// <summary>
        /// 子语句要查的字段  
        /// </summary>
        public string SelectFileds { get; set; }

        /// <summary>
        /// 每页条数
        /// </summary>
        public int PageSize { get; set; }


        /// <summary>
        /// 计算总数时返回的列
        /// </summary>
        public string CountFields { get; set; }


        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }


        /// <summary>
        /// 1为统计总数 0为查询
        /// </summary>
        public int doCount { get; set; }

 


        /// <summary>
        /// 一些关联的查询语句
        /// </summary>
        public string @JoinTable { get; set; }

        /// <summary>
        /// 查询结查的排序方式 如 order by id asc
        /// </summary>
        public string OrderString { get; set; }
 
    }
}
