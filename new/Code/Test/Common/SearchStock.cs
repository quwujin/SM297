using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Common
{
    public static class SearchStock
    {
       //{"status":"true","list":[{"SupplierId":"270","SupplierName":"美团20元优惠券（满20.01使用）","Qty":"105"}]}

        public static StockResult GetStock(int sid, string username)
        { 
            //string sid = Common.Fun.ConvertTo<string>(Request["sid"], "0");
            //string username = "ben.zhu";
            string timestamp = DateTimeToInt(DateTime.Now).ToString();
            string key = "ESMART_PSI_1766";
             
            Dictionary<string, string> list = new Dictionary<string, string>();
            list.Add("username", username);
            list.Add("timestamp", timestamp.ToString());
            list.Add("sid", sid.ToString());
            string q = GetAscString(list);

            list.Add("appkey", key);
            string AscString = GetAscString(list);


            string sign = Common.getMD5.MD5(AscString).ToUpper();


            string url = "http://my.eswapi.com/PublicApi/GetStock.aspx";

            string pp = q + "&sign=" + sign + "";

            StockResult result = new StockResult() { status = "false", list = null };
            try
            {
                result = Common.JsonHelper.JsonDeserialize<StockResult>(Common.Fun.webRequestPost(url, pp));
            }
            catch (Exception ex) { 

            }
            return result;
        
        }

        public static DateTime IntToDateTime(int timestamp)
        {
            return TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)).AddSeconds(timestamp);
        }

        public static string DateTimeToInt(DateTime datetime)
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        public static string GetAscString(IDictionary<string, string> dictionary)
        {
            if (dictionary == null)
                throw new ArgumentException("获取签名时，参数字典为NULL！");
            // 第一步：把字典按Key的字母顺序排序
            IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(dictionary);
            IEnumerator<KeyValuePair<string, string>> dem = sortedParams.GetEnumerator();

            // 第二步：把所有参数名和参数值串在一起
            var query = new StringBuilder();
            while (dem.MoveNext())
            {
                string key = dem.Current.Key;
                string value = dem.Current.Value;
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                {
                    query.Append(key).Append("=").Append(value).Append("&"); ;
                }
            }
            //去掉最后一个&
            if (query.Length > 0)
            {
                query.Remove(query.Length - 1, 1);
            }


            return query.ToString();
        }

    }

    public class StockResult {
        public string status { get; set; }
        public List<StockListResult> list { get; set; }
    }

    public class StockListResult
    {
        public string SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Qty { get; set; }

    }

}
