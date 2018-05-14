using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// WeiXinOpenId 的摘要说明
/// </summary>
public class WeiXinOpenId
{
	public WeiXinOpenId()
	{
		//
		// TODO: 在此处添加构造函数逻辑
        // 
    }

    #region 调用示例
    //WxResultMsg baseopid = WeiXinOpenId.GetOpenId("cs",false) as WxResultMsg; //cs:填写后台配置Acid  false:获取微信昵称-true：获取openid
    //Response.Write(baseopid.userinfo.nickname);
    #endregion

    private static string wxUrl = "http://wxhb.esmartwave.com/NewWeixin/GetOpenId.aspx";

    private static string RedirectURL
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

    /// <summary>
    /// 获取微信openid
    /// </summary>
    /// <param name="isbase"></param>
    /// <returns></returns>
    public static WxResultMsg GetOpenId(string acid,bool isbase=true)
    {
        acid = acid.ToLower();

        WxResultMsg result = new WxResultMsg();

        string code = HttpContext.Current.Request["code"];

        if (string.IsNullOrEmpty(code) == false)
        {
            if (isbase)
            {
                string paramsstr = "getresult=snsapi_base&acid={0}&code={1}";
                paramsstr = string.Format(paramsstr, acid, code);
                string postresult = doPost(wxUrl, paramsstr);

                result = JsonDeserialize<WxResultMsg>(postresult);

                if (result.states == 0)
                {
                    return result;
                }
                //LogTool.LogCommon.WebFramework.GeneralMethodBase.WebDebugLog(result.error); //错误信息  
            }
            else {
                string paramsstr = "getresult=snsapi_userinfo&acid={0}&code={1}";
                paramsstr = string.Format(paramsstr, acid, code);
                string postresult = doPost(wxUrl, paramsstr);
                result = JsonDeserialize<WxResultMsg>(postresult);
                if (result.states == 0)
                {
                    return result;
                } 
                
            } 
            return result;
        }

        if (isbase)
        {
            HttpContext.Current.Response.Redirect(wxUrl + "?getresult=code&acid=" + acid + "&redirecturl=" + HttpUtility.UrlEncode(RedirectURL));
        }
        else
        {
            HttpContext.Current.Response.Redirect(wxUrl + "?getresult=code&isbase=false&acid=" + acid + "&redirecturl=" + HttpUtility.UrlEncode(RedirectURL));
        } 
        return null;
    }   
       
    #region 辅助方法
    private static string ConvertDateStringToJsonDate(Match m)
    {
        string result = string.Empty;
        DateTime dt = DateTime.Parse(m.Groups[0].Value);
        dt = dt.ToUniversalTime();
        TimeSpan ts = dt - DateTime.Parse("1970-01-01");
        result = string.Format("\\/Date({0}+0800)\\/", ts.TotalMilliseconds);
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
    #endregion

}


public class WxResultMsg
{
    public int states { get; set; }
    public string error { get; set; }
    public WxSnsapiBase snsapibase { get; set; }
    public WxUserInfo userinfo { get; set; }
}

public class WxSnsapiBase 
{
    public string openid { get; set; } 
    public string access_token { get; set; }
    public int expires_in { get; set; }
    public string refresh_token { get; set; } 
    public string scope { get; set; }

}

public class WxUserInfo 
{
    public string openid { get; set; } 
    public string nickname { get; set; }
    public int sex { get; set; }
    public string language { get; set; }
    public string city { get; set; }
    public string province { get; set; }
    public string country { get; set; }
    public string headimgurl { get; set; }

}