using System;
using System.Collections.Generic;
using System.Linq;
 using System.Web;
using System.Runtime.Serialization.Json;
using System.IO;
 using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Data;

namespace Common
{
    public class JsonHelper
    {
        //public static string JsonSerializer<T>(T t)
        //{ 
        //    MemoryStream ms = JsonSerializerToStream<T>(t); 
        //    string jsonString = Encoding.UTF8.GetString(ms.ToArray());
        //    ms.Close(); 
        //    string p = @"\\/Date\((\d+)\+\d+\)\\/";
        //    MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertJsonDateToDateString);
        //    Regex reg = new Regex(p);
        //    jsonString = reg.Replace(jsonString, matchEvaluator);
        //    return jsonString;
        //}

        public static MemoryStream JsonSerializerToStream<T>(T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(); 
            ser.WriteObject(ms, t);
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }

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
        /// 灏咼son搴忓垪鍖栫殑鏃堕棿鐢�/Date(1294499956278+0800)杞负瀛楃涓�
        /// </summary>
        private static string ConvertJsonDateToDateString(Match m)
        {
            string result = string.Empty;
            DateTime dt = new DateTime(1970, 1, 1);
            dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value));
            dt = dt.ToLocalTime();
            result = dt.ToString("yyyy-MM-dd HH:mm:ss");
            return result;
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



        /// <summary>
        /// unicode解码
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public static string DecodeUnicode(Match match)
        {
            if (!match.Success)
            {
                return null;
            }

            char outStr = (char)int.Parse(match.Value.Remove(0, 2), System.Globalization.NumberStyles.HexNumber);
            return new string(outStr, 1);
        }

        public static string GetJsonString(object data)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            var jsonString = js.Serialize(data);

            //解码Unicode，也可以通过设置App.Config（Web.Config）设置来做，这里只是暂时弥补一下，用到的地方不多
            MatchEvaluator evaluator = new MatchEvaluator(DecodeUnicode);
            var json = Regex.Replace(jsonString, @"\\u[0123456789abcdef]{4}", evaluator);//或：[\\u007f-\\uffff]，\对应为\u000a，但一般情况下会保持\
            json = Regex.Replace(json, @"\\/Date\((\d+)\)\\/", match =>
            {
                DateTime dt = new DateTime(1970, 1, 1);
                dt = dt.AddMilliseconds(long.Parse(match.Groups[1].Value));
                dt = dt.ToLocalTime(); //本地时间
                return dt.ToString(); ;
            });
            return json; 

        }

        /// <summary> 
        /// DataTable转为json 
        /// </summary> 
        /// <param name="dt">DataTable</param> 
        /// <returns>json数据</returns> 
        public static string DataTableToJson(DataTable dt)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dt.Rows)
            {
                Dictionary<string, object> result = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    result.Add(dc.ColumnName, dr[dc]);
                }
                list.Add(result);
            }

            return GetJsonString(list);
        }
 

    }

    public class JsonResponse
    {
        public bool Success { get; set; }
        public string VCode { get; set; }
        public int StatusCode { get; set; }
        public string Msg { get; set; }

        public JsonResponse()
        {
            Success = true;
        }
    }

}
