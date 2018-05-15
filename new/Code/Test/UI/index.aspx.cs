using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFramework.SessionManage;

public partial class index : NBase
{
    Db.InfoDal infodal = new Db.InfoDal();
    Db.OrderInfoDal dal = new Db.OrderInfoDal();
    public Model.InfoModel mm = new Model.InfoModel();
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
                WebFramework.GeneralMethodBase.WebDebugLog(OpenId, "OpenId异常:" + OpenId);

                Response.Redirect("/default.aspx");
                Response.End();
                return;
            }

            #endregion

            mm = infodal.GetModel(1);

            #region default的代码优化到这儿
            //todo  1.订单生成上传小票填写手机号2.大转盘抽奖3.二等奖领奖地址界面
            //states -1：已作废；0：待审核；1：已完成；2：未上传；3：已抽奖

            //todo  1.初次进入，进入首页，生成订单 上传小票，未抽奖  states = 2
            //todo  
            //todo  3.抽完奖    states = 3
            //todo  4.若是二等奖  未填写地址信息  states = 31
            //todo  5.填写完地址信息，待审核    states = 0
            //todo  6.若是一等奖，待审核    states = 0


            //填完手机号未抽奖  
            Model.OrderInfoModel model2 = dal.GetModel(" and openid='" + OpenId + "' and states=2");
            if (model2.Id > 0)
            {
                orderSession.OrderKey = model2.OrderCode;
                SessionMethod.SessionInstance.SetSession(orderSession);
                //WebFramework.GeneralMethodBase.SetSession(orderSession);
                Response.Redirect("lottery.aspx");
            }
            //抽中二等奖，未填写地址信息    地址页信息 
            Model.OrderInfoModel model3 = dal.GetModel(" and openid='" + OpenId + "' and states=3");
            if (model3.Id > 0)
            {
                orderSession.OrderKey = model3.OrderCode;
                SessionMethod.SessionInstance.SetSession(orderSession);
                //WebFramework.GeneralMethodBase.SetSession(orderSession);
                Response.Redirect("prize.aspx");
            }
           

            #endregion
        }
    }
}