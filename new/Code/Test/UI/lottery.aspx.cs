using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lottery : NBase
{
    Db.InfoDal infodal = new Db.InfoDal();
    Db.OrderInfoDal dal = new Db.OrderInfoDal();
    public Model.InfoModel mm = new Model.InfoModel();
    protected void Page_Load(object sender, EventArgs e)
    {
        mm = infodal.GetModel(1);
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
        mm = infodal.GetModel(1);
        #region 验证OpenId
        if (Common.ValidateHelper.IsOpenid(OpenId) == false)
        {
            WebFramework.GeneralMethodBase.WebDebugLog(OpenId, "OpenId异常:" + OpenId);

            Response.Redirect("/default.aspx");
            Response.End();
            return;
        }
        #endregion

    }
}