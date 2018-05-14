using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class WxMsgModel
    {
        public int Id{get;set;}
        public string ToUserName{get;set;}
        public string FromUserName { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime DTime { get; set; }
        public string MsgType{get;set;}
        public string PicUrl{get;set;}
        public string MediaId{get;set;}
        public string MsgId{get;set;}
        public string Contents{get;set;}
        public string Format{get;set;}
        public string ThumbMediaId{get;set;}
        public string Location_X{get;set;}
        public string Location_Y{get;set;}
        public string Scale{get;set;}
        public string Label{get;set;}
        public string Title { get; set; }
        public int States { get; set; }
    }
}
