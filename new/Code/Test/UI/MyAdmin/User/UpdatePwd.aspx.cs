using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

public partial class User_UpdatePwd : PageBase
{

    public Db.UserInfoDal dal = new Db.UserInfoDal();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txtUserName.Text = HttpUtility.UrlDecode(userseesion.UserName);

        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string pwd = Common.getMD5.MD5(this.txtOldPwd.Text);
        string newPwd = this.txtNewPwd2.Text;
        string uname = userseesion.UserName.ToString();
       
        string oldpwd = dal.GetModel(HttpUtility.UrlDecode(uname)).PassWord;
        if (pwd!=oldpwd)
        {
            JScript.alertBack("a","旧密码不正确",this.Page);
        }
        else
        {
            if (Regex.IsMatch(newPwd, @"^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z]{8,12}$") == false)
            {
                JScript.alertBack("ok", "请填写英文数字组合密码（长度8-12位）", this.Page);
                return;
            }

            Model.UserInfoModel model = new Model.UserInfoModel();
            model.UserName = HttpUtility.UrlDecode(uname);
            model.PassWord= getMD5.MD5(newPwd);
           
            if (dal.UpdatePwd(model)>0)
            {
                JScript.alert("a","修改成功","../default/login.aspx",this.Page);
            }  

        }
    }

}