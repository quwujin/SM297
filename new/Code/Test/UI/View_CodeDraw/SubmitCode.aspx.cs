using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFramework.ESLog;

public partial class SubmitCode : NBase
{
    Db.PassCodeInfoDal PassCodeInfoDal = new Db.PassCodeInfoDal();
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
            string Code = orderSession.Code;
            string IP = Common.ClientIP.GetIp();

            #region 验证OpenId
            if (Common.ValidateHelper.IsOpenid(OpenId) == false)
            {
                ESLogMethod.ESLogInstance.Debug("OpenId异常", OpenId, Code);

                Response.Redirect("/default.aspx");
                Response.End();
                return;
            }
            #endregion
             
            #region 检查IP
            if (Db.Security.IpAccessControlDal.CheckIpIsOK(false, CacheBase.IPSettingModel, IP, "", "") == false)
            {
                ESLogMethod.ESLogInstance.Debug("IP模型限制超限", IP, Code);

                WebFramework.GeneralMethodBase.Ero();
                Response.End();
                return;
            }
            #endregion

            #region 验证Code

            #region 验证Code格式
            if (Common.ValidateHelper.IsCode(Code) == false)
            {
                ESLogMethod.ESLogInstance.Debug("激活码格式错误", Code);

                WebFramework.GeneralMethodBase.Ero();
                Response.End();
                return;
            }
            #endregion

            #region 验证Code状态
            Model.PassCodeInfoModel PassCodeModel = PassCodeInfoDal.GetModelByCode(Code);

            if (PassCodeModel.Id <= 0)
            { 
                ESLogMethod.ESLogInstance.Debug("激活码不存在", Code);

                WebFramework.GeneralMethodBase.Ero();
                Response.End();
                return;
            }

            if (PassCodeModel.StatusId != 0) {
                  
                //实际情况按项目需求来
                //Response.Redirect("SubmitOk.aspx");
                //Response.End();
                //return;
            }
            #endregion

            #endregion

        }
         
    } 


}