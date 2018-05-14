using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NearStor : NBase
{ 
    protected void Page_Load(object sender, EventArgs e)
    {

        #region 上下线控制
        string txt = WebFramework.GeneralMethodBase.CheckStartOrEnd();
        if (string.IsNullOrEmpty(txt) == false)
        {
            this.lbErr.Text = txt;
        }
        #endregion

        #region 系统维护开关
        string WhTxt = WebFramework.GeneralMethodBase.IsCheckWH();
        if (string.IsNullOrEmpty(WhTxt) == false)
        {
            this.lbErr.Text = WhTxt;
            return;
        }
        #endregion

        Model.SessionModel orderSession = WebFramework.SessionManage.SessionMethod.SessionInstance.GetSession();
         
        string opid = orderSession.OpenId;

        #region 验证openid
        if (Common.ValidateHelper.IsOpenid(opid) == false)
        {
            WebFramework.GeneralMethodBase.WebDebugLog(opid, "openid异常:opid:" + opid);
            Response.Redirect("/default.aspx");
            Response.End();
            return;
        }
        #endregion

    }
     
}