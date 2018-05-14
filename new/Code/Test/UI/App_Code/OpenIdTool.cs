using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;

/// <summary>
/// OpenIdTool 的摘要说明
/// </summary>
public class OpenIdTool
{
    protected static string wxAPIUrl = "http://wxqingfeng7b59.esmartwave.com/api/wxAuthorization.ashx";


    public OpenIdTool()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    public static string CurrentURL
    {
        get
        {
            if (HttpContext.Current != null)
            {
                Uri url = HttpContext.Current.Request.Url;
                return string.Format("{0}://{1}{2}", url.Scheme, url.Host, url.PathAndQuery);
            }
            return string.Empty;
        }
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
    /// 获取微信openid
    /// </summary>
    /// <param name="cid"></param>
    /// <param name="isbase"></param>
    /// <returns></returns>
    public static wxOauthBase GetOpenId(string cid, bool isbase)
    {
        wxOauthBase _result = new wxOauthBase();

        string datakey = HttpContext.Current.Request["code"];
        if (string.IsNullOrEmpty(datakey) == false)
        {
            if (isbase)
            {
                #region 只拿openid
                string paramsstr = "type=base&cid=$cid$&datakey=$datakey$";
                paramsstr = paramsstr.Replace("$cid$", cid);
                paramsstr = paramsstr.Replace("$datakey$", datakey);
                string postresult = HttpService.Post(paramsstr, wxAPIUrl);
                _result = JsonDeserialize<wxOauthAccess_token>(postresult);

                #endregion

            }
            else
            {
                #region  openid and name
                string paramsstr = "type=info&cid=$cid$&datakey=$datakey$";
                paramsstr = paramsstr.Replace("$cid$", cid);
                paramsstr = paramsstr.Replace("$datakey$", datakey);
                string postresult = HttpService.Post(paramsstr, wxAPIUrl);
                //LogTool.LogCommon.WebFramework.GeneralMethodBase.WebDebugLog(postresult);
                _result = JsonDeserialize<wxOauthUserinfo>(postresult);
                #endregion
            }

            return _result;
        }


        #region 跳转获取授权
        if (string.IsNullOrEmpty(_result.openid))
        {
            if (isbase)
            {
                System.Web.HttpContext.Current.Response.Redirect(wxAPIUrl + "?redirecturl=" + HttpUtility.UrlEncode(CurrentURL));
            }
            else
            {
                System.Web.HttpContext.Current.Response.Redirect(wxAPIUrl + "?isbase=false&redirecturl=" + HttpUtility.UrlEncode(CurrentURL));
            }
        }
        #endregion


        return _result;
    }


    public static bool CheckOpenIdIsSubscribe(string cid, string openId)
    {
        bool _result = false;
        string paramsstr = "type=getuserinfo&cid=$cid$&datakey=$datakey$";
        paramsstr = paramsstr.Replace("$cid$", cid);
        paramsstr = paramsstr.Replace("$datakey$", openId);
        var postresult = HttpService.Post(paramsstr, wxAPIUrl);


        var openidobj = JsonDeserialize<wxUserinfo>(postresult);
        if (string.IsNullOrEmpty(openidobj.openid) == false)
        {
            if (openidobj.subscribe == 1)
            {
                _result = true;
            }
        }
        else
        {
            System.Web.HttpContext.Current.Response.Write("获取Id失败03");
            System.Web.HttpContext.Current.Response.End();
        }
        return _result;
    }

    public static bool UpdateOpenIdGroupInfo(string cid, string openId, int groupid)
    {
        bool _result = false;
        string paramsstr = "type=togroupid&cid=$cid$&datakey=$datakey$&groupid=$groupid$";
        paramsstr = paramsstr.Replace("$cid$", cid);
        paramsstr = paramsstr.Replace("$datakey$", openId);
        paramsstr = paramsstr.Replace("$groupid$", groupid.ToString());
        var postresult = HttpService.Post(paramsstr, wxAPIUrl);
        var openidobj = JsonDeserialize<GroupReurnBase>(postresult);
        _result = openidobj.errcode == "0";
        return _result;
    }

}

public class GroupReurnBase
{

    public string errcode { get; set; }
    public string errmsg { get; set; }
}


public class wxUserinfo
{
    public int subscribe { get; set; }
    public string openid { get; set; }
    public string nickname { get; set; }
    public string sex { get; set; }
    public string province { get; set; }
    public string city { get; set; }
    public string country { get; set; }
    public string headimgurl { get; set; }
    public string unionid { get; set; }
    public long subscribe_time { get; set; }
    public string remark { get; set; }
    public string groupid { get; set; }
}

public class wxOauthUserinfo : wxOauthBase
{
    public string nickname { get; set; }
    public string sex { get; set; }
    public string province { get; set; }
    public string city { get; set; }
    public string country { get; set; }
    public string headimgurl { get; set; }
    public string unionid { get; set; }
    public IList<string> privilege { get; set; }
}

public class wxOauthBase
{
    public string openid { get; set; }
}

public class wxOauthAccess_token : wxOauthBase
{
    public string access_token { get; set; }
    public int expires_in { get; set; }
    public string refresh_token { get; set; }

    public string scope { get; set; }
    public int errcode { get; set; }
    public string errmsg { get; set; }
}
