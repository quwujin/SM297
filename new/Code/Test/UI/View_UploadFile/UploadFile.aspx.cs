using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFramework.ESLog;

public partial class UploadFile : NBase
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
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

            string OpenId = orderSession.OpenId;

            #region 验证OpenId
            if (Common.ValidateHelper.IsOpenid(OpenId) == false)
            {
                ESLogMethod.ESLogInstance.Debug("OpenId异常", OpenId);

                Response.Redirect("/default.aspx");
                Response.End();
                return;
            }
            #endregion

        }
    }
}