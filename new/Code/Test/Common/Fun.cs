using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Common
{
    public class Fun
    {
        /// <summary>
        /// 转化为整型
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetMacAddress()
        {
            try
            {
                string mac = "";
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        mac = mo["MacAddress"].ToString();
                        break;
                    }
                }
                moc = null;
                mc = null;
                return mac;
            }
            catch
            {
                return "";
            }
            finally
            {

            }
        }

        


        public static string GetMac(string clientip)
        {
            string mac = "";
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = "nbtstat";
            process.StartInfo.Arguments = "-a " + clientip;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            int length = output.IndexOf("MAC Address =");
            if (length > 0)
            {
                mac = output.Substring(length + 14, 17);
            }
            return mac;



        }
        public static string Post(string url, string json)
        {

            WebClient client = new WebClient();
            Uri newUri;
            string token = GetAccessToken();
            string createMenuURL = url + "=" + token;
            newUri = new Uri(createMenuURL);
            client.Encoding = Encoding.UTF8;
            string result = client.UploadString(newUri, json);
            return result;

        }


        public static string GetAccessToken()
        {
            string url = "https://api.weixin.qq.com/cgi-bin/token";
            string param = "grant_type=client_credential&appid=" + APPID + "&secret=" + APPS;
            string res = webRequestPost(url, param);
            if (res != "")
            {
                return JsonToDataTable(res).Rows[0][0].ToString();
            }
            else
            {
                return "";
            }


        }


        public static DataTable JsonToDataTable(string strJson)
        {
            //取出表名   
            var rg = new Regex(@"(?<={)[^:]+(?=:\[)", RegexOptions.IgnoreCase);
            string strName = rg.Match(strJson).Value;
            DataTable tb = null;
            //去除表名   
            // strJson = strJson.Substring(strJson.IndexOf("[") + 1);
            //strJson = strJson.Substring(0, strJson.IndexOf("]"));

            //获取数据   
            rg = new Regex(@"(?<={)[^}]+(?=})");
            MatchCollection mc = rg.Matches(strJson);
            for (int i = 0; i < mc.Count; i++)
            {
                string strRow = mc[i].Value;
                string[] strRows = strRow.Split(',');

                //创建表   
                if (tb == null)
                {
                    tb = new DataTable();
                    tb.TableName = strName;
                    foreach (string str in strRows)
                    {
                        var dc = new DataColumn();
                        string[] strCell = str.Split(':');
                        dc.ColumnName = strCell[0];
                        dc.ColumnName = dc.ColumnName.Replace("\"", "");
                        tb.Columns.Add(dc);
                    }
                    tb.AcceptChanges();
                }

                //增加内容   
                DataRow dr = tb.NewRow();
                for (int r = 0; r < strRows.Length; r++)
                {
                    string str = "";
                    if (strRows[r].Split(':')[1].Trim().IndexOf("http") > -1)
                    {
                        dr[r] = strRows[r].Split(':')[1].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "") + ":" + strRows[r].Split(':')[2].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "").Replace("\\", "");
                    }
                    else
                    {
                        dr[r] = strRows[r].Split(':')[1].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "");
                    }


                }
                tb.Rows.Add(dr);
                tb.AcceptChanges();
            }

            return tb;
        }












        public static string webRequestPost(string url, string param)
        {
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(param);


            System.Net.HttpWebRequest req = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url + "?" + param);
            req.Method = "Post";
            req.Timeout = 120 * 1000;
            req.ContentType = "application/x-www-form-urlencoded;";
            req.ContentLength = bs.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(bs, 0, bs.Length);
                reqStream.Flush();
            }
            using (System.Net.WebResponse wr = req.GetResponse())
            {
                //在这里对接收到的页面内容进行处理 
                Stream strm = wr.GetResponseStream();
                StreamReader sr = new StreamReader(strm, System.Text.Encoding.UTF8);
                string line;
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                while ((line = sr.ReadLine()) != null)
                {
                    sb.Append(line + System.Environment.NewLine);
                }
                sr.Close();
                strm.Close();
                return sb.ToString();
            }
        }


        public static string MyRequestPost(string url, string param)
        {
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(param);


            System.Net.HttpWebRequest req = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url + "?" + param);
            req.Method = "Post";
            req.Timeout = 120 * 1000;
            req.ContentType = "application/x-www-form-urlencoded;";
            req.ContentLength = bs.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(bs, 0, bs.Length);
                reqStream.Flush();
            }
            using (System.Net.WebResponse wr = req.GetResponse())
            {
                //在这里对接收到的页面内容进行处理 
                Stream strm = wr.GetResponseStream();
                StreamReader sr = new StreamReader(strm, System.Text.Encoding.UTF8);
                string line;
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                while ((line = sr.ReadLine()) != null)
                {
                    sb.Append(line + System.Environment.NewLine);
                }
                sr.Close();
                strm.Close();
                return sb.ToString();
            }
        }




        public static string APPID = ConfigurationManager.AppSettings["appid"];
        public static string APPS = ConfigurationManager.AppSettings["apps"];
        public static string APPURL = ConfigurationManager.AppSettings["url"];


        public static string replace(string str)
        {
            if (str == null) { str = ""; }
            str = str.Replace("&", "&amp;");
            str = str.Replace("'", "''");
            str = str.Replace("\"", "&quot;");
            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt;");
            str = str.Replace("\n", "<br>");
            return str;
        }


        public static string RndCode(int length, int randindex)
            {
                if (randindex >= 1000000) randindex = 1;
                char[] arrChar = new char[]{'0','1','2','3','4','5','6','7','8','9'};
                System.Text.StringBuilder num = new System.Text.StringBuilder();
                Random rnd = new Random(DateTime.Now.Millisecond + randindex);
                for (int i = 0; i < length; i++)
                {
                    num.Append(arrChar[rnd.Next(0, arrChar.Length)].ToString());
                }
                return num.ToString();
            
            }

        public static string RndCodes3(int length, int randindex)
        {
            if (randindex >= 1000000) randindex = 1;
            char[] arrChar = new char[] {'0', '1', '2' };
            System.Text.StringBuilder num = new System.Text.StringBuilder();
            Random rnd = new Random(DateTime.Now.Millisecond + randindex);
            for (int i = 0; i < length; i++)
            {
                num.Append(arrChar[rnd.Next(0, arrChar.Length)].ToString());
            }
            return num.ToString();

        }


        public static string RndCodes2(int length, int randindex)
        {
            if (randindex >= 1000000) randindex = 1;
            char[] arrChar = new char[] { '0', '1' };
            System.Text.StringBuilder num = new System.Text.StringBuilder();
            Random rnd = new Random(DateTime.Now.Millisecond + randindex);
            for (int i = 0; i < length; i++)
            {
                num.Append(arrChar[rnd.Next(0, arrChar.Length)].ToString());
            }
            return num.ToString();

        }


        public static string replaces(string str)
        {
            if (str == null) { str = ""; }
            str = str.Replace("&amp;", "&");
            str = str.Replace("&quot;", "\"");
            str = str.Replace("''", "'");
            str = str.Replace("&lt;", "<");
            str = str.Replace("&gt;", ">");
            str = str.Replace("<br>", "\n");
            return str;
        }

        public static string Contents(string str)
        {
            string rstr = "";
            if (str == "" || str == null)
            {
                str = "无内容";
            }
            else {
                str.Replace("\n", "<br/>").ToString().Replace("\r", "<br/>").ToString().Replace(" ", "&nbsp").ToString();
            }
            return str;
        }
        
        public static T ConvertTo<T>(object val, T defaultVal)
        {
            if (Convert.IsDBNull(val) || val == null)
                return defaultVal;
            else
            {
                try
                {
                    return (T)Convert.ChangeType(val, typeof(T));
                }
                catch
                {
                    return defaultVal;
                }
            }
        }


        public static string cmid(string t, int n) {
            string tt="";
            int lens = t.Length;
            if (lens>n)
            {
                tt=t.Substring(0,n)+"...";
            }
            else
	        {
                 tt=t;
	        }
            return tt;
        }

        public static string MenuUrl()
        { 
            return   "menubid=" + HttpContext.Current.Request["menubid"] + "&menusid=" + HttpContext.Current.Request["menusid"];
        }
        public static bool IsCint(string text)
        {

            if (text == "" || text == null)
            {
                return false;
            }
            else
            {
                string pattern = @"^\-?[0-9]+$";
                if (System.Text.RegularExpressions.Regex.IsMatch(text, pattern))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }



        public static void AddCookies(string Key, string Value)
        {
             HttpCookie aCookie = new HttpCookie(Key);
             aCookie.Value =Value;
              aCookie.Expires = DateTime.Now.AddDays(1);
              HttpContext.Current.Response.Cookies.Add(aCookie);
           // HttpContext.Current.Session[Key] = Value;
        }


        public static string GetCookies(string Key)
        {

            /*  string v = "";
              if (HttpContext.Current.Session[Key] != null)
              {
                  v = HttpContext.Current.Session[Key].ToString();
              }
              else
              {
                  v = "";
              }
              return v;
              /*
           * */
            string v = "";
             if (HttpContext.Current.Request.Cookies[Key] != null)
                {
                    v = HttpContext.Current.Request.Cookies[Key].Value;
                }
             else
             {
                 v = "";
             }
             return v;
          

        }


        /// <summary>
        /// 转化为长整
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static long CLong(string text)
        {

            if (text == "" || text == null)
            {
                return 0;
            }
            else
            {
                string pattern = @"^\-?[0-9]+$";
                if (System.Text.RegularExpressions.Regex.IsMatch(text, pattern))
                {
                    return long.Parse(text);
                }
                else
                {
                    return 0;
                }
            }
        }

        public  static  string RepaceString(string str,string old,string news)
        {
            if(news==null)
            {
                return str;
            }
            else
            {
                return str.Replace(old, news).ToString();
            }
        }

        public static double Cdecimal(string text)
        {
            if (text == "" || text == null)
            {
                return 0.00;
            }
            else
            {
                string pattern = @"/^[+|-]?\d*\.?\d*$/";
                if (System.Text.RegularExpressions.Regex.IsMatch(text, pattern))
                {
                    return double.Parse(text);
                }
                else
                {
                    return 0.00;
                }
            }
        }

        public static double CdecimalInt(string text)
        {
            if (text == "" || text == null)
            {
                return 0;
            }
            else
            {
                string pattern = @"^(0|[1-9]\d*)$|^(0|[1-9]\d*)\.(\d+)$";
                if (System.Text.RegularExpressions.Regex.IsMatch(text, pattern))
                {
                    return double.Parse(text);
                }
                else
                {
                    return 0;
                }
            }
        }

        
               
        /// <summary>
        /// 处理string型的sql参数空值
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static object ConvertStringSqlParamVal(string val)
        {
            if (string.IsNullOrEmpty(val))
                return DBNull.Value;
            else
                return val;
        }

        public static string GetIp() {
            string ip = "";
            if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null) // using proxy 
            {
                ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString(); // Return real client IP. 
            }
            else// not using proxy or can't get the Client IP 
            {
                ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString(); //While it can't get the Client IP, it will return proxy IP. 
            }
            return ip;
        }


       

    }
}
