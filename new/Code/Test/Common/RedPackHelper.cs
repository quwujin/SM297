using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
    public class RedPackHelper
    {

        private static string GetCode(int num)
        {
            string a = "0123456789";
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < num; i++)
            {
                sb.Append(a[new Random(Guid.NewGuid().GetHashCode()).Next(0, a.Length - 1)]);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 发送红包
        /// </summary>
        /// <param name="acid">项目编号</param>
        /// <param name="hid">红包编号</param>
        /// <param name="openid">用户openid</param>
        /// <param name="orderid">订单编号（yyyyMMdd+10位一天内不能重复的数字）</param>
        /// <param name="money">红包金额(1元:100,最小100)</param>
        /// <param name="dt">当前时间戳</param>
        /// <returns></returns>
        public result send(int acid, int hid, string openid, string orderid, int money, string ckey, string hkey)
        {
            result returndata = new result();
             
            string vkey = "ZZCXXCZ090115";
            string dt = DateTime.Now.ToString("yyyyMMddhhmmssfff");
            //string openid = "";
            //orderid = DateTime.Now.ToString("yyyyMMddh") + GetCode(10);//yyyymmdd+10位一天内不能重复的数字;
            //orderid = DateTime.Now.ToString("yyyyMMdd") + GetCode(10);//yyyymmdd+10位一天内不能重复的数字;
            //int money = 1;
            //int acid =1;
            //int hid = 1;
            //string ckey = "OIM1u72sz01";//项目密钥
            //string hkey = "olskeg891LW";//红包密钥`

            string sign1 = Common.getMD5.MD5(vkey + dt + openid + orderid).ToUpper();
            string sign = Common.getMD5.MD5(vkey + ckey + hkey + dt + openid + orderid + acid + hid + money).ToUpper();
            string url = "http://redpack.esmartwave.com/Controller/RedPackAPI.ashx?sign=" + sign + "&sign1=" + sign1 + "&openid=" + openid + "&orderid=" + orderid + "&acid=" + acid + "&hid=" + hid + "&money=" + money + "&dt=" + dt;

            //String message=do=Xml.DocXml(userName, MD5Encode(password), msgid, phone, content, sign, subcode, sendtime);

            string data = "";
            try
            {
                data = WebNet.doPost(url, "");
                returndata = Common.JsonHelper.JsonDeserialize<result>(data);
            }
            catch(Exception ex)
            {
                returndata.SendStatus = false;
                returndata.SendType = 1;
                returndata.MSG = "请求API错误：" + ex.ToString();
            }

            if (returndata.SendType == 1)
            {
                Email.EmailTool.sendEmail(ConfigurationManager.AppSettings["LogErrorEmailTo"], HttpContext.Current.Request.Url.Host, returndata.MSG + ",红包订单号：" + orderid + ",Openid：" + openid, "");
            } 

            return returndata;
        }

        public class result
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
