using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class WebNet
    {
        public static string Post(string url, string json)
        {

            WebClient client = new WebClient();
            Uri newUri;
            //  string token = GetAccessToken();
            //  string createMenuURL = url + "=" + token;
            string createMenuURL = url ;
            newUri = new Uri(createMenuURL);
            client.Encoding = Encoding.UTF8;
            string result = client.UploadString(newUri, json);
            return result;

        }
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
        public static string doPostGbk(string uri, string paramStr)
        {
            Encoding encoding = Encoding.GetEncoding("GBK");
            if (encoding == null)
                encoding = Encoding.GetEncoding("GB2312");

            string result = string.Empty;

            WebClient wc = new WebClient();

            // 采取POST方式必须加的Header
            wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            byte[] postData = encoding.GetBytes(paramStr);


            byte[] responseData = wc.UploadData(uri, "POST", postData); // 得到返回字符流
            return encoding.GetString(responseData);// 解码                  
        }
    }
}
