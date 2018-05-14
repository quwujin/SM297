using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;

public partial class Login : System.Web.UI.Page
{
    public string title = "";
    public Db.UserInfoDal user = new Db.UserInfoDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            title = WebFramework.GeneralMethodBase.GetKeyConfig(10).Val;
        }
    }
     
    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (this.safecode.Text != "esmart")
        {
            this.loginerr.InnerHtml = "安全码不正确";
        }
        else
        {
            string _user = this.txtUserName.Text;
            string _pass = this.txtPassWord.Text;

            Model.UserInfoModel login = new Model.UserInfoModel();

            login.UserName = _user.Trim();
            string p = _pass.Trim();
            p = getMD5.MD5(p);
            login.PassWord = p;

            if (user.Login(login) > 0)
            {
                Model.UserInfoModel model = user.GetModel(login.UserName);

                SetSession(model); 

                new Db.Login_LogDal().Add(new Model.Login_LogModel() {  LoginIp = Fun.GetIp(), LoginTime = DateTime.Now, UserName = login.UserName,Notes="登录成功" });

                #region 验证账号密码是否一致
                if (_user == _pass)
                {
                    Common.JScript.alert("a", "密码安全性低，请先修改密码后登录！", "../user/updatepwd.aspx", this.Page);
                    return;
                }
                #endregion

                Response.Redirect("default.aspx");
            }
            else
            {
                if (new Db.Login_LogDal().CheckCount(string.Format(" and LoginIp='{0}' and DateDiff(dd,LoginTime,getdate())=0", "登录失败-账号密码错误")) > 10)
                {
                    this.loginerr.InnerHtml = "账号密码错误次数已超限，已被锁定";
                    return;
                }

                new Db.Login_LogDal().Add(new Model.Login_LogModel() { Notes = "登录失败-账号密码错误", LoginTime = DateTime.Now, UserName = login.UserName,LoginIp=Fun.GetIp() });
                
                this.loginerr.InnerHtml = "账号密码错误，或账号被锁定";
            }

        }
    }

    public void SetSession(Model.UserInfoModel orderSession)
    {
        Model.UserInfoModel OrderUser = new Model.UserInfoModel();
        OrderUser.UserId = orderSession.UserId;
        OrderUser.UserName = orderSession.UserName;
        OrderUser.LevelId = orderSession.LevelId;
        OrderUser.GroupId = orderSession.GroupId;
        OrderUser.GroupName = orderSession.GroupName;
        OrderUser.PassWord = orderSession.PassWord;
        OrderUser.RealName = Common.Des.Encode(System.Web.HttpUtility.HtmlEncode(OrderUser.UserId + OrderUser.UserName + OrderUser.LevelId + OrderUser.GroupId + OrderUser.GroupName));
        Session["UserSysSession"] = OrderUser;
    } 

}