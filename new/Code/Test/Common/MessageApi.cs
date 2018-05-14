using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common
{

    public static class MessageApi
    {

        private static string ApiUrl = "http://MessageApi.iseedling.com/Controller/SMSController.ashx";

        #region 发送短信主方法
        public static ResultData SendMessage(string Conten, string Mobile, string SendType, int Supplie, int PorjectId, string ProjectKey)
        {
            ResultData result = new ResultData();

            string EncodeConten = System.Web.HttpUtility.UrlEncode(Conten);//内容编码

            #region 添加参数
            SortedDictionary<string, string> SignDictionary = new SortedDictionary<string, string>();

            SignDictionary.Add("conten", EncodeConten);
            SignDictionary.Add("mobile", Mobile);
            SignDictionary.Add("porjectId", PorjectId.ToString());
            SignDictionary.Add("sendtype", Enum.GetName(typeof(Common.MessageApi.SendType), Common.TypeHelper.ObjectToInt(SendType, 0)));
            SignDictionary.Add("supplie", Supplie.ToString());
            SignDictionary.Add("timestamp", GetTimeStamp());
            SignDictionary.Add("secret", ProjectKey);

            #endregion

            #region 拼接参数
            string Parameter = ToUrl(SignDictionary);
            #endregion

            #region 获取Sign
            string Sign = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(Parameter, "MD5").ToUpper();
            #endregion

            Parameter = Parameter + "&sign=" + Sign + "&isencode=1";

            try
            {
                result = JsonDeserialize<ResultData>(doPost(ApiUrl, Parameter));
                return result;
            }
            catch (Exception ex) {
                return new ResultData() { erronum = -1, msg = ex.Message };
            }
        }
        #endregion

        #region 供应商
        public enum Supplie
        {
            助通 = 1,
            建周 = 2,
            创蓝 = 3,
            科讯通vip = 4,
            科讯通esmart = 5,
            科讯通yuanhui = 6,
            科讯通shuomiao = 7,
        }
        #endregion

        #region 发送类型
        public enum SendType
        {
            通知 = 1,
            验证码 = 2, 
        }
        #endregion

        #region 辅助方法
        private static string doPost(string uri, string paramStr)
        {
            Encoding encoding = Encoding.UTF8;
            if (encoding == null)
                encoding = Encoding.UTF8;

            string result = string.Empty;

            WebClient wc = new WebClient();

            // 采取POST方式必须加的Header
            wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            byte[] postData = encoding.GetBytes(paramStr);


            byte[] responseData = wc.UploadData(uri, "POST", postData); // 得到返回字符流
            return encoding.GetString(responseData);// 解码                  
        }

        /**
          * @Dictionary格式转化成url参数格式
          * @ return url格式串, 该串不包含sign字段值
          */
        private static string ToUrl(SortedDictionary<string, string> Dictionary)
        {
            string buff = "";
            foreach (KeyValuePair<string, string> pair in Dictionary)
            {  
                buff += pair.Key + "=" + pair.Value + "&";
            }
            buff = buff.Trim('&');
            return buff;
        }

        private static T JsonDeserialize<T>(string jsonString)
        {
            string p = @"\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}";
            MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertDateStringToJsonDate);
            Regex reg = new Regex(p);
            jsonString = reg.Replace(jsonString, matchEvaluator);
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }

        private static string ConvertDateStringToJsonDate(Match m)
        {
            string result = string.Empty;
            DateTime dt = DateTime.Parse(m.Groups[0].Value);
            dt = dt.ToUniversalTime();
            TimeSpan ts = dt - DateTime.Parse("1970-01-01");
            result = string.Format("\\/Date({0}+0800)\\/", ts.TotalMilliseconds);
            return result;
        }

        //生成时间戳
        private static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
        #endregion

        public class ResultData
        {
            public int erronum { get; set; }
            public string msg { get; set; }
        }

    }

    


}
