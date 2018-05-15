using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFramework.SessionManage;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        { 
             
            #region 获取Openid
            GetOpenId(Request.Url.Host == "localhost");
            #endregion 
 
            //串码抽奖
            //Response.Redirect("View_CodeDraw/SubmitCode.aspx");

            //上传小票
            Response.Redirect("index.aspx");

        }

        
    }

    #region 获取OpenId
    /// <summary>
    /// 获取OpenId
    /// </summary>
    /// <param name="istest">true-返回固定OpenId false-返回获取OpenId</param>
    /// <param name="isbase">true-获取OpenId false-获取微信昵称</param>
    /// <returns></returns>
    public string GetOpenId(bool istest = false, bool isbase = true)
    {
        string Codes = string.IsNullOrEmpty(Request["codes"]) ? "" : Request["codes"];

        if (istest)
        {
            SessionMethod.SessionInstance.SetSession("倘若", "otzXTjqt6B6xXVjozCblakccTUyw", "...", Codes);
            return "otzXTjqt6B6xXVjozCblakccTUyw";
        }

        Model.SessionModel session = SessionMethod.SessionInstance.GetSession();

        if (session != null && Common.ValidateHelper.IsOpenid(session.OpenId))
        {
            if (Codes.Length > 0) { SessionMethod.SessionInstance.SetSession("", session.OpenId, "", Codes); }
            return session.OpenId;
        }

        WxResultMsg baseopid = WeiXinOpenId.GetOpenId(WebFramework.GeneralMethodBase.GetHost().Replace("demo", "").ToLower(), isbase) as WxResultMsg;

        if (isbase && baseopid.snsapibase != null && Common.ValidateHelper.IsOpenid(baseopid.snsapibase.openid))
        {
            SessionMethod.SessionInstance.SetSession("", baseopid.snsapibase.openid, "", Codes);
            return baseopid.snsapibase.openid;
        }
        else if (isbase == false && baseopid.userinfo != null && Common.ValidateHelper.IsOpenid(baseopid.userinfo.openid))
        {
            SessionMethod.SessionInstance.SetSession(baseopid.userinfo.nickname.Replace("'", ""), baseopid.userinfo.openid, baseopid.userinfo.headimgurl, Codes);
            return baseopid.userinfo.openid;
        }
        else
        {
            HttpContext.Current.Response.End();
            return "";
        }
    }
    #endregion

}