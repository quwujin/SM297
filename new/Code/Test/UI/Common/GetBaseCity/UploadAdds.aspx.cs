using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UploadAdds : NBase
{ 
    Db.OrderInfoDal oddal = new Db.OrderInfoDal();

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

            Model.SessionModel orderSession = WebFramework.SessionManage.SessionMethod.SessionInstance.GetSession();


            string opid = orderSession.OpenId;

            #region 验证openid
            if (Common.ValidateHelper.IsOpenid(opid)==false)
            {
                WebFramework.GeneralMethodBase.WebDebugLog(opid,"openid异常:opid:" + opid);
                Response.Redirect("/default.aspx");
                Response.End();
                return;
            }
            #endregion 
             
        }
    }

    public string GetProv()
    {
        string str = "";

        Db.ProvinceCityDal provdal = new Db.ProvinceCityDal();

        List<Model.ProvinceModel> provList = provdal.GetProvinceModelList("");
        foreach (Model.ProvinceModel model in provList)
        {
            str += "<li><input type=\"button\" class=\"sel_btn\" data=\"" + model.ProvinceID + "\" value=\"" + model.ProvinceName + "\" /></li>"; 
        }
         
        return str;

        
    }
}