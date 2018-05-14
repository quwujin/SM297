using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class WxMessageModel
    {

        public int Id { get; set; }
        /// <summary>
        /// 消息类型文本，图片，单图文，多图文
        /// </summary>
        public string MsgType { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Contents { get; set; }

        /// <summary>
        /// 标 题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 连接
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 图文连接
        /// </summary>
        public string HUrl { get; set; }

        /// <summary>
        /// 多图组编号
        /// </summary>et
        public string GroupCode { get; set; }

        /// <summary>
        /// 图像地址
        /// </summary>
        public string PicUrl { get; set; }
        /// <summary>
        /// 是否正文显示
        /// </summary>
        public int PShow { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        public int OrderId { get; set; }
    }
}
