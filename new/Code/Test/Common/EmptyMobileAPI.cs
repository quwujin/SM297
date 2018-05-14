using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 手机号空号检测
    /// </summary>
    public class EmptyMobileAPI
    {



        public static string EmptyMobile(string mobile)
        {

            try
            {

                Dictionary<String, Object> Params = new Dictionary<String, Object>();
                Params.Add("apiName", "S1447925");
                Params.Add("password", "pwd3971603");
                Params.Add("mobile", mobile);

                string responseStr = doPost("https://kh_bd.253.com//feign/apiMobileTest/findByMobile", ToUrl(Params));

                ResponseData response = Common.JsonHelper.JsonDeserialize<ResponseData>(responseStr);

                if (response != null && response.resultCode == "000000" && response.resultObj != null && response.resultObj.Count > 0)
                {
                    return Enum.GetName(typeof(status), Common.TypeHelper.ObjectToInt(response.resultObj[0].status, 1));
                }
                //return response.resultMsg;
            }
            catch (Exception ex) {
               

            }

            return "查询失败";
        }

        //{
        //    "resultMsg": "成功",
        //    "resultCode": "000000",
        //    "resultObj": [
        //        {
        //            "mobile": "13817367247",
        //            "lastTime": 1509361607000,
        //            "area": "上海-上海",
        //            "numberType": "中国移动 GSM",
        //            "chargesStatus": "1",
        //            "status": "1"
        //        }
        //    ]
        //}


        public class ResponseData
        {
            public string resultMsg { get; set; }
            public string resultCode { get; set; }
            public List<resultObj> resultObj { get; set; }
        }

        public class resultObj
        {
            public string mobile { get; set; }
            public string lastTime { get; set; }
            public string area { get; set; }
            public string numberType { get; set; }
            public string chargesStatus { get; set; }
            public string status { get; set; }
        }

        public enum status {
            空号 = 0,
            实号 = 1,
            停机 = 2,
            库无 = 3,
            沉默号 = 4,
        }

        /**
       * @Dictionary格式转化成url参数格式
       * @ return url格式串, 该串不包含sign字段值
       */
        private static string ToUrl(Dictionary<String, Object> Params)
        {
            string buff = "";
            foreach (KeyValuePair<string, object> pair in Params)
            {
                if (pair.Value == null)
                {
                    throw new Exception("Data内部含有值为null的字段!");
                }

                if (pair.Value.ToString() != "")
                {
                    buff += pair.Key + "=" + pair.Value + "&";
                }
            }
            buff = buff.Trim('&');
            return buff;
        }

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

    }
}
