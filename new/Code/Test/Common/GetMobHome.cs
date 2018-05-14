using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Common
{

    public class GetMobHome
    {

       public static string url = "http://api2.ofpay.com/mobinfo.do";
         
        /// <summary>
        /// 查询手机号运营商
        /// </summary>
        /// <param name="mobile">手机号</param>  
        public static string GetSupplier(string mobile)
        {
            string param = "mobilenum=" + mobile;
            string[] result = request(url, param).Split('|');

            if (result.Length != 3) { return ""; }

            return result[2];
        }

        /// <summary>
        /// 查询手机号城市
        /// </summary>
        /// <param name="mobile">手机号</param>  
        public static string GetCity(string mobile)
        {
            string param = "mobilenum=" + mobile;
            string[] result = request(url, param).Split('|');

            if (result.Length != 3) { return ""; }

            return result[1];
        }



        /// <summary>
        /// 发送HTTP请求
        /// </summary>
        /// <param name="url">请求的URL</param>
        /// <param name="param">请求的参数</param>
        /// <returns>请求结果</returns>
        public static string request(string Url, string postDataStr)
        {

            try
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
                request.Method = "GET";
                request.ContentType = "text/html;charset=UTF-8";
                request.Headers.Add("apikey", "889a429c377a883391185653481b3a15");

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.Default);
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();

                return retString;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static String decodeUnicode(String theString)
        {

            char aChar;


            int len = theString.Length;

            StringBuilder outBuffer = new StringBuilder(len);

            for (int x = 0; x < len; )
            {

                aChar = theString.ToCharArray()[x++];

                if (aChar == '\\')
                {

                    aChar = theString.ToCharArray()[x++];

                    if (aChar == 'u')
                    {

                        // Read the xxxx    

                        int value = 0;

                        for (int i = 0; i < 4; i++)
                        {

                            aChar = theString.ToCharArray()[x++];

                            switch (aChar)
                            {

                                case '0':

                                case '1':

                                case '2':

                                case '3':

                                case '4':

                                case '5':

                                case '6':
                                case '7':
                                case '8':
                                case '9':
                                    value = (value << 4) + aChar - '0';
                                    break;
                                case 'a':
                                case 'b':
                                case 'c':
                                case 'd':
                                case 'e':
                                case 'f':
                                    value = (value << 4) + 10 + aChar - 'a';
                                    break;
                                case 'A':
                                case 'B':
                                case 'C':
                                case 'D':
                                case 'E':
                                case 'F':
                                    value = (value << 4) + 10 + aChar - 'A';
                                    break;
                                default:
                                    throw new Exception(
                                      "Malformed   \\uxxxx   encoding.");
                            }

                        }
                        outBuffer.Append((char)value);
                    }
                    else
                    {
                        if (aChar == 't')
                            aChar = '\t';
                        else if (aChar == 'r')
                            aChar = '\r';

                        else if (aChar == 'n')

                            aChar = '\n';

                        else if (aChar == 'f')

                            aChar = '\f';

                        outBuffer.Append(aChar);

                    }

                }
                else

                    outBuffer.Append(aChar);

            }

            return outBuffer.ToString();

        }

        /// <summary>  
        /// 反序列化XML为类实例  
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="xmlObj"></param>  
        /// <returns></returns>

        public static T DeserializeXML<T>(string xmlObj, Encoding encoding)  
        {  
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream(encoding.GetBytes(xmlObj)))
            {
                using (StreamReader sr = new StreamReader(ms, encoding))
                {
                    return (T)serializer.Deserialize(sr);
                }
            }

            //using (StringReader reader = new StringReader(xmlObj))
            //{
            //    return (T)serializer.Deserialize(reader);
            //}  
        } 
    }
     
}
