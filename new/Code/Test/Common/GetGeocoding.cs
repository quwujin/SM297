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
    /// <summary>
    /// 通过百度API获取地址经纬度； 
    /// </summary>
    /// <param name="Address">地址</param>
    /// <param name="city">省</param> 
    /// <returns></returns>
    public class GetGeocoding
    {

        public static Location GetLngLat(string Address, string city = "")
        {
            //city:地址所在的城市名。用于指定上述地址所在的城市，当多个城市都有上述地址时，该参数起到过滤作用。

            string ApiUrl = "http://api.map.baidu.com/geocoder/v2/";

            string address = HttpUtility.UrlEncode(Address);

            string paramStr = "output=json&ak=537af81275e3b62d0e8b5a860b745c9d&address=" + address + (string.IsNullOrEmpty(city) == false ? "&city=" + city : "");

            try
            {
                string ApiResult = doPost(ApiUrl, paramStr);

                Geocoding ApiResultData = JsonDeserialize<Geocoding>(ApiResult);

                if (ApiResultData.status == 0) {
                    return ApiResultData.result.location;
                }
            }
            catch (Exception e) {
                return null;
            }

            return null;
        }

        #region 辅助方法

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

    public class Geocoding {
        public int status { get; set; } //返回结果状态值， 成功返回0，  
        public Result result { get; set; } 
    }

    public class Result {
        public Location location { get; set; }
        public int precise { get; set; } //位置的附加信息，是否精确查找。1为精确查找，即准确打点；0为不精确，即模糊打点。
        public int confidence { get; set; } //可信度，描述打点准确度
        public string level { get; set; } //地址类型
    }

    public class Location
    {
        public decimal lat { get; set; } //纬度值
        public decimal lng { get; set; } //经度值
    }



}
