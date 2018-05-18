using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class prize :NBase
{
    Db.InfoDal infodal = new Db.InfoDal();
    Db.OrderInfoDal dal = new Db.OrderInfoDal();
    public Model.InfoModel mm = new Model.InfoModel();
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

        string OpenId = orderSession.OpenId;

        #region 验证OpenId
        if (Common.ValidateHelper.IsOpenid(OpenId) == false)
        {
            WebFramework.GeneralMethodBase.WebDebugLog(OpenId, "OpenId异常:" + OpenId);

            Response.Redirect("/default.aspx");
            Response.End();
            return;
        }
        #endregion
        mm = infodal.GetModel(1);
        Model.OrderInfoModel model = dal.GetModel(" and ordercode='" + orderSession.OrderKey + "'");
        if (model.Id > 0)
        {
            if (model.Jx == "一等奖")
            {
                this.Hprize.Value = "1";
                //this.img1.Visible = true;
            }
            else if (model.Jx == "二等奖")
            {
                this.Hprize.Value = "2";
                //this.img2.Visible = true;
            }
            else
            {
                this.Hprize.Value = "0";
                //this.img3.Visible = true;
            }
        }
    }
}