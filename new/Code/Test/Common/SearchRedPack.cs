using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Common
{
    public static class SearchRedPack
    {
        public static string SearchOrder(string ordercode, int WeChatType = 1)
        {
            string vkey = "ZZCXXCZ090115";
            string orderid = ordercode;
            string dt = DateTime.Now.ToString("yyyyMMddhhmmssfff");
            string sign = Common.getMD5.MD5(vkey + dt + orderid).ToUpper();
            string url = "http://RedPack.esmartwave.com/Controller/RedPackSearchAPI.ashx?WeChatType=" + WeChatType + "&sign=" + sign + "&orderid=" + orderid + "&dt=" + dt;

            try
            {
                string result = Common.WebNet.doPost(url, "");

                ReturnMsg rm = Common.JsonHelper.JsonDeserialize<ReturnMsg>(result);

                if (rm.SendStatus == false) {
                    return rm.MSG;
                }

                rm.MSG = rm.MSG.Replace("xml", "RedPackInfo");

                RedPackInfo xml = Deserialize(typeof(RedPackInfo), rm.MSG) as RedPackInfo;

                if (xml.return_code.ToUpper() == "SUCCESS" && xml.result_code.ToUpper() == "SUCCESS")
                {
                    //SENDING:发放中 
                    //SENT:已发放待领取 
                    //FAILED：发放失败 
                    //RECEIVED:已领取 
                    //RFUND_ING:退款中 
                    //REFUND:已退款

                    return Enum.GetName(typeof(StatusMsg), (int)Enum.Parse(typeof(Status), xml.status));
                } 
                return "查询失败";
            }
            catch {
                return "查询失败";
            } 
        }

        #region 反序列化
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="xml">XML字符串</param>
        /// <returns></returns>
        private static object Deserialize(Type type, string xml)
        {
            try
            {
                //<xml> 内的xml名字要和type名字一样
                using (StringReader sr = new StringReader(xml))
                {
                    XmlSerializer xmldes = new XmlSerializer(type);
                    return xmldes.Deserialize(sr);
                }
            }
            catch (Exception e)
            {

                return null;
            }
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="type"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        private static object Deserialize(Type type, Stream stream)
        {
            XmlSerializer xmldes = new XmlSerializer(type);
            return xmldes.Deserialize(stream);
        }
        #endregion

        #region 序列化
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        private static string Serializer(Type type, object obj)
        {
            MemoryStream Stream = new MemoryStream();
            XmlSerializer xml = new XmlSerializer(type);
            try
            {
                //序列化对象
                xml.Serialize(Stream, obj);
            }
            catch (InvalidOperationException)
            {
                return "";
            }
            Stream.Position = 0;
            StreamReader sr = new StreamReader(Stream);
            string str = sr.ReadToEnd();

            sr.Dispose();
            Stream.Dispose();

            return str;
        }

        #endregion

        private enum Status
        {
            SENDING = 0,
            SENT = 1,
            FAILED = 2,
            RECEIVED = 3,
            RFUND_ING = 4,
            REFUND = 5,
        }

        public enum WeChatType
        {
            源慧科技 = 1,
            百事1 = 2,
            百事2 = 3,
            硕苗科技 = 4,
            月伴宝贝 = 5,
            有思文化 = 6,
        }

        private enum StatusMsg
        {
            发放中 = 0,
            已发放待领取 = 1,
            发放失败 = 2,
            已领取 = 3,
            退款中 = 4,
            已退款 = 5,
        }

        public class RedPackInfo
        {
            public bool IsSend { get; set; }
            public string return_code { get; set; }
            public string return_msg { get; set; }
            public string result_code { get; set; }
            public string err_code { get; set; }
            public string err_code_des { get; set; }
            public string mch_billno { get; set; }
            public string mch_id { get; set; }
            public string status { get; set; }//红包状态 
            public string send_time { get; set; }//红包发送时间
            public string refund_time { get; set; }//红包退款时间 
            public string refund_amount { get; set; }//红包退款金额
            public string openid { get; set; }//领取红包的Openid
            public string rcv_time { get; set; }//领取红包的时间  
            
        
        }

        public class ReturnMsg
        {
            //返回具体信息
            public string MSG { get; set; }
            //向上兼容
            public string STATUS { get; set; }
            //发送bool值
            public bool SendStatus { get; set; }
            //返回类型 1系统返回，2微信返回
            public int SendType { get; set; }
        }

    }
}
