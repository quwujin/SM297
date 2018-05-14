using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
    
    public class GetShortUrl
    {

        /// <summary>
        /// 通过新浪API获取短链； 默认获取新浪失败则获取微信
        /// </summary>
        /// <param name="uri">长链</param> 
        /// <returns></returns>
        public static string shortSinaUrl(string uri)
        {

            string ApiUrl = "http://api.t.sina.com.cn/short_url/shorten.json";
            string paramStr = "source=1681459862&url_long=" + HttpUtility.UrlEncode(uri);
             
            try
            {
                string Resultdata = doPost(ApiUrl, paramStr); 
                entry userJson = JsonDeserialize<entry>(Resultdata.TrimStart('[').TrimEnd(']'));

                if (userJson.type == "0")
                {
                    return userJson.url_short;
                }

                return shortWeixinUrl(uri);
            }
            catch (Exception e) {
                return shortWeixinUrl(uri);
            } 
            
        }

        /// <summary>
        /// 通过微信API获取短链； 
        /// </summary>
        /// <param name="uri">长链</param> 
        /// <returns></returns>
        public static string shortWeixinUrl(string uri)
        {

            string ApiUrl = "https://api.weixin.qq.com/cgi-bin/shorturl?access_token=";
            string paramStr = "{\"action\":\"long2short\",\"long_url\":\"" + uri +"\"}";
             
            try
            {

                string Resultdata = doPost(ApiUrl + GetAccessToKen(0), paramStr);
                //LogTool.LogCommon.WebFramework.GeneralMethodBase.WebDebugLog(Resultdata);
                WeixinEntry userJson = JsonDeserialize<WeixinEntry>(Resultdata);

                if (userJson.errcode != "0") {

                    Resultdata = doPost(ApiUrl + GetAccessToKen(1), paramStr);
                    //LogTool.LogCommon.WebFramework.GeneralMethodBase.WebDebugLog(Resultdata);

                    userJson = JsonDeserialize<WeixinEntry>(Resultdata);

                    if (userJson.errcode != "0")
                    {
                        return "";
                    }
                }

                return userJson.short_url;
            }
            catch (Exception e)
            {
                return "";
            }

        }

        #region 辅助类
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


        private static string GetRequestPost(string url, string param)
        {
            HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(url);

            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
            byte[] arrB = encode.GetBytes(param.ToString());

            webrequest.Method = "POST";
            webrequest.ContentType = "application/x-www-form-urlencoded";
            webrequest.ContentLength = arrB.Length;
            //webrequest.ClientCertificates.Add(GetX509Certificate2());//获取证书
            Stream outStream = webrequest.GetRequestStream();
            outStream.Write(arrB, 0, arrB.Length);
            outStream.Close();

            //接收HTTP做出的响应
            WebResponse myResp = webrequest.GetResponse();
            Stream ReceiveStream = myResp.GetResponseStream();
            StreamReader readStream = new StreamReader(ReceiveStream, encode);

            Char[] read = new Char[256];
            int count = readStream.Read(read, 0, 256);
            string strs = "";
            while (count > 0)
            {
                strs += new String(read, 0, count);
                count = readStream.Read(read, 0, 256);
            }
            readStream.Close();
            myResp.Close();
            return strs;
        }

        private static string ACCESS_TOKEN_URL = "http://wxygetme.esmartwave.com/api/wxAccessToken.ashx";
        private static string ACCESS_TOKEN_URL2 = "http://wxygetme.esmartwave.com/api/wxAccessToken.ashx?getnewtoken=getnew";

        private static string GetAccessToKen(int types)
        {
            string result = "";
            if (types == 1)
            {
                result = GetRequestPost(ACCESS_TOKEN_URL2, "");
            }
            else
            {
                result = GetRequestPost(ACCESS_TOKEN_URL, "");
            }
            //LogTool.LogCommon.WebFramework.GeneralMethodBase.WebDebugLog(result);

            return result;

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

        #endregion


    }


    public class entry
    {
        public string url_short { get; set; }
        public string type { get; set; }
        public string url_long { get; set; }
    }

    public class WeixinEntry
    {
        public string errcode { get; set; }
        public string errmsg { get; set; }
        public string short_url { get; set; }
    }   
}
