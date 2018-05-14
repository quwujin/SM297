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
    /// <summary>
    /// 获取资源库资源
    /// </summary>
    public class EswAPIHelper
    {

        private static readonly string EswAPIUrl = "https://eapi.eswapi.com/Api/Common/GetCodeV2.ashx";

        public static ResultMessage GetStockCode(string Mobile, int typeid, int projectid, string ordercode, int qty = 1)
        {

            Data RequestData = new Data();
            RequestData.SetValue("appkey", System.Configuration.ConfigurationManager.AppSettings["PSI_AppKey"]);
            RequestData.SetValue("mob", Mobile);
            RequestData.SetValue("typeid", typeid);
            RequestData.SetValue("projectid", projectid);
            RequestData.SetValue("qty", qty);
            RequestData.SetValue("ordercode", ordercode);
            RequestData.SetValue("timestamp", RequestData.GenerateTimeStamp());
            RequestData.SetValue("sign", RequestData.MakeSign());

            ResultMessage resultMessage = new ResultMessage();

            try
            {
                string retrunData = doPost(EswAPIUrl, RequestData.ToUrl());

                resultMessage = JsonDeserialize<ResultMessage>(retrunData);
            }
            catch (Exception ex)
            {
                resultMessage.IsSuccess = false;
                resultMessage.Message = ex.Message; 

            }

            return resultMessage;
        }

        #region 解析
        /// <summary>
        /// JSON鍙嶅簭鍒楀寲
        /// </summary>
        public static T JsonDeserialize<T>(string jsonString)
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

        /// <summary>
        /// 灏嗘椂闂村瓧绗︿覆杞负Json鏃堕棿
        /// </summary>
        private static string ConvertDateStringToJsonDate(Match m)
        {
            string result = string.Empty;
            DateTime dt = DateTime.Parse(m.Groups[0].Value);
            dt = dt.ToUniversalTime();
            TimeSpan ts = dt - DateTime.Parse("1970-01-01");
            result = string.Format("\\/Date({0}+0800)\\/", ts.TotalMilliseconds);
            return result;
        }

        #endregion

        #region 请求
        /// <summary>
        /// 通过WebClient类Post数据到远程地址，需要Basic认证；
        /// 调用端自己处理异常
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="paramStr">name=张三&age=20</param>
        /// <param name="encoding">请先确认目标网页的编码方式</param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string doPost(string uri, string paramStr)
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

        #endregion

        public class ResultMessage {

            public bool IsSuccess { get; set; }
            public string Message { get; set; }
            public string Text { get; set; }
            public string ReturnUrl { get; set; }
            public ResultData ResultData { get; set; } 
        
        }

        public class ResultData
        {
            public string codes { get; set; }
            public string cardno { get; set; } 
        }


    }
}
