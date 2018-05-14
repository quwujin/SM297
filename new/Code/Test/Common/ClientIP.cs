using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Common
{
    public class ClientIP
    {
        public static string GetIp()
        { 
            var context = HttpContext.Current;
            string result = context.Request.ServerVariables["X-Forwarded-For"];

            if (string.IsNullOrEmpty(result))
            {
                result = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];//获取包括使用了代理服务器的地址列表。
            }
            if (string.IsNullOrEmpty(result))
            {
                result = context.Request.ServerVariables["REMOTE_ADDR"];//最后一个代理服务器地址。
            }
            if (string.IsNullOrEmpty(result))
            {
                result = context.Request.UserHostAddress;
            }

            if (Common.ValidateHelper.IsIP(result) == false) {
                result = context.Request.UserHostAddress;
            }

            return result;

        }
        public static string GetAdds(string ip)
        {
            string adds = Common.WebNet.doPostGbk("http://int.dpool.sina.com.cn/iplookup/iplookup.php?ip=" + ip, "");
            return adds;
        }
        public static string[] GetArrayAdds(string ip)
        {
            string[] citys={"","","",""};
            try
            {
                string adds = Common.WebNet.doPostGbk("http://int.dpool.sina.com.cn/iplookup/iplookup.php?ip=" + ip, "");
                //1\t-1\t-1\t中国\t上海\t上海\t\t\t\t
                adds = adds.Replace("-1", "");
                adds = adds.Replace("1", "");
                //adds = "中国 上海 上海";
                adds = adds.Replace("\t\t\t\t", "");
                adds = adds.Replace("\t\t\t", "");
                adds = adds.Replace("\t\t", "");
                adds = adds.Replace("\t", ",");
                citys[3] = adds;

                citys[0] = adds.Split(',')[0];
                if (adds.Split(',').Length > 1)
                {
                    citys[1] = adds.Split(',')[1];
                }
                if (adds.Split(',').Length > 2)
                {
                    citys[2] = adds.Split(',')[2];
                }

                if (citys[1].Length <= 0 || citys[2].Length <= 0) {

                    Rootobject rootobject = GetAddsByBaidu(ip);
                    if (rootobject != null)
                    {
                        return new string[] { rootobject.content.address_detail.province, rootobject.content.address_detail.city };
                    }
                }
            }
            catch (Exception ex) { }
            return new string[] { citys[1], citys[2] };
        }

        /// <summary>
        /// 百度API
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static Rootobject GetAddsByBaidu(string ip)
        {
            try
            {
                string url = " http://api.map.baidu.com/location/ip?ip=" + ip + "&ak=wTI1QtSV8PemuqGRz3d9p7bPGtAZHTxG&coor=bd09ll";
                string adds = WebNet.doPostGbk(url, "");
                adds = HttpUtility.UrlDecode(adds);
                Rootobject IPobj = JsonHelper.JsonDeserialize<Rootobject>(adds);
                if (IPobj.status == 0)
                {
                    return IPobj;
                }
                else
                {
                    HbIPApiType(IPobj.status);
                    //LogTxt.WriteLog(HbIPApiType(IPobj.status), 3);
                    return null;
                }
            }
            catch (Exception ex)
            {
                //LogTxt.WriteLog(ex.Message, 3);
                return null;
            }


        }
        public static string HbIPApiType(int statusid)
        {
            // 0   正常
            //1   服务器内部错误 该服务响应超时或系统内部错误，如遇此问题，请到官方论坛进行反馈
            //10  上传内容超过8M Post上传数据不能超过8M
            //101 AK参数不存在 请求消息没有携带AK参数
            //102 MCODE参数不存在，mobile类型mcode参数必需 对于Mobile类型的应用请求需要携带mcode参数，该错误码代表服务器没有解析到mcode
            //200 APP不存在，AK有误请检查再重试 根据请求的ak，找不到对应的APP
            //201 APP被用户自己禁用，请在控制台解禁
            //202 APP被管理员删除 恶意APP被管理员删除
            //203 APP类型错误 当前API控制台支持Server(类型1), Mobile(类型2, 新版控制台区分为Mobile_Android(类型21)及Mobile_IPhone（类型22）及Browser（类型3），除此之外的其他类型被认为是APP类型错误
            //210 APP IP校验失败  在申请Server类型应用的时候选择IP校验，需要填写IP白名单，如果当前请求的IP地址不在IP白名单或者不是0.0.0.0 / 0就认为IP校验失败
            //211 APP SN校验失败  SERVER类型APP有两种校验方式：IP校验和SN校验，当用户请求的SN和服务端计算出来的SN不相等的时候，提示SN校验失败
            //220 APP Referer校验失败 浏览器类型的APP会校验referer字段是否存在，且在referer白名单里面，否则返回该错误码
            //230 APP Mcode码校验失败  服务器能解析到mcode，但和数据库中不一致，请携带正确的mcode
            //240 APP 服务被禁用   用户在API控制台中创建或设置某APP的时候禁用了某项服务
            //250 用户不存在   根据请求的user_id, 数据库中找不到该用户的信息，请携带正确的user_id
            //251 用户被自己删除 该用户处于未激活状态
            //252 用户被管理员删除    恶意用户被加入黑名单
            //260 服务不存在   服务器解析不到用户请求的服务名称
            //261 服务被禁用   该服务已下线
            //301 永久配额超限，限制访问 配额超限，如果想增加配额请联系我们
            //302 天配额超限，限制访问  配额超限，如果想增加配额请联系我们
            //401 当前并发量已经超过约定并发配额，限制访问    并发控制超限，请控制并发量请联系我们
            //402 当前并发量已经超过约定并发配额，并且服务总并发量也已经超过设定的总并发配额，限制访问  并发控制超限，请控制并发量请联系我们
            switch (statusid)
            {
                case 0:
                    return "正常";
                case 1:
                    return "服务器内部错误";
                case 10:
                    return "上传内容超过8M";
                case 101:
                    return "AK参数不存在";
                case 102:
                    return "MCODE参数不存在";
                case 200:
                    return "APP不存在";
                case 201:
                    return "APP被管理员删除";
                case 202:
                    return "APP被管理员删除";
                case 203:
                    return "APP类型错误";
                case 210:
                    return "IP校验失败";
                case 211:
                    return "SN校验失败";
                case 220:
                    return "Referer校验失败";
                case 230:
                    return "Mcode码校验失败";
                case 240:
                    return "服务被禁用";
                case 250:
                    return "用户不存在";
                case 251:
                    return "用户被自己删除";
                case 252:
                    return "用户被管理员删除";
                case 260:
                    return "服务不存在";
                case 261:
                    return "服务被禁用";
                case 301:
                    return "永久配额超限";
                case 302:
                    return "天配额超限";
                case 401:
                    return "当前并发量已经超过约定并发配额";
                case 402:
                    return "当前并发量已经超过约定并发配额";
                default:
                    return "未知错误";
                    break;
            }
            return "";
        }


        public class Rootobject
        {
            public string address { get; set; }
            public Content content { get; set; }
            public int status { get; set; }
        }

        public class Content
        {
            public string address { get; set; }
            public Address_Detail address_detail { get; set; }
            public Point point { get; set; }
        }

        public class Address_Detail
        {
            public string city { get; set; }
            public int city_code { get; set; }
            public string district { get; set; }
            public string province { get; set; }
            public string street { get; set; }
            public string street_number { get; set; }
        }

        public class Point
        {
            public string x { get; set; }
            public string y { get; set; }
        }
    }

     
}
