using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using System.Text.RegularExpressions;

public partial class User_Create_UserInfo : PageBase
{
    public Db.UserInfoDal dal = new Db.UserInfoDal();
    Db.CommonFunctionDal dl = new Db.CommonFunctionDal();
    public string menuUrl = "menubid=" + HttpContext.Current.Request["menubid"] + "&menusid=";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        //    dl.DataBindDropDownList("PostName", "PostId", "UserInfo_Post", " ", "   OrderId desc", this.ddlPostId, 0);
            dl.DataBindDropDownList("GroupName", "GroupId", "UserInfo_Group", userseesion.GroupId == 22 ? " and GroupId=23" : "", " GroupId  ", this.ddlGroupId, 0);
         //   dl.DataBindDropDownList("LevelName", "Id", "UserInfo_Level", " ", "   Id ", this.ddlLevelId, 0);
           // dl.DataBindDropDownList("RoleName", "RoleId", "UserInfo_Role", " ", "   OrderId desc ", this.ddlRoleId, 0);

            int id = Common.TypeHelper.ObjectToInt(Request["id"], 0);
         
            if (id>0)
            {
                this.txt.Text = "不修改则为空";
             
                Model.UserInfoModel model = dal.GetModel(id);
                this.txtUserName.Text = model.UserName;
                this.hidId.Value = model.UserId.ToString();
               // this.txtPassWord.Attributes.Add("Value",  model.PassWord );
                txtPassWord2.Text = model.PassWord;
                RequiredFieldValidator2.Enabled = false;
             //   this.ddlPostId.SelectedValue = model.PostId.ToString();
                
                this.ddlGroupId.SelectedValue = model.GroupId.ToString();
             //   this.ddlRoleId.SelectedValue = model.RoleId.ToString();
                this.ddlStatusId.SelectedValue = model.StatusId.ToString();
             //   this.ddlLevelId.SelectedValue = model.LevelId.ToString();
              //  this.txtEmail.Text = model.Email.ToString();
             //   this.txtMob.Text = model.Mob;
                this.txtUserName.ReadOnly = true;
            }

            
        }
    }





    protected void Button1_Click(object sender, EventArgs e)
    {

        ///要跳转回的菜单
        menuUrl = menuUrl + dl.GeMenuSid("../User/Admin_UserInfo.aspx", Request["menubid"]);
        Model.UserInfoModel model = new Model.UserInfoModel();
        int id = Common.TypeHelper.ObjectToInt(this.hidId.Value, 0);
        model.UserId = id;
        model.UserName = this.txtUserName.Text;
       
        if (this.txtPassWord.Text == "")
        {
            model.PassWord =  this.txtPassWord2.Text ;
        }
        else
        {
            model.PassWord = this.txtPassWord.Text;
        }
        if (Regex.IsMatch(model.PassWord, @"^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z]{8,12}$") == false)
        {
            JScript.alertBack("ok", "请填写英文数字组合密码（长度8-12位）", this.Page);
            return;
        }

        model.PassWord = Common.getMD5.MD5(model.PassWord);
        model.Email = "";// this.txtEmail.Text;
        model.CreateTime = DateTime.Now;
        model.LevelId = 0;// Common.TypeHelper.ObjectToInt(this.ddlLevelId.SelectedValue, 0);
        model.Mob = "";// this.txtMob.Text;
        model.RealName = this.txtUserName.Text;
        model.StatusId = Common.TypeHelper.ObjectToInt( this.ddlStatusId.SelectedValue,0);
        model.GroupId = Common.TypeHelper.ObjectToInt(this.ddlGroupId.SelectedValue, 0);
        model.PostId = 0;// Common.TypeHelper.ObjectToInt(this.ddlPostId.SelectedValue, 0);
        model.RoleId = 0;// Common.TypeHelper.ObjectToInt(this.ddlRoleId.SelectedValue, 0);
        model.LoginCount = 0;

        if (id>0)
        {
           if (dal.Update(model)>0)
            {
                JScript.alert("ok", "修改成功,点击确定返回列表", "Admin_UserInfo.aspx?",this.Page);
            }
           
        }
        else
        {
            

            if (dal.Exists(model.RealName) == true)
            {
                JScript.alertBack("ok", "已存在的用户名", this.Page);
            }
            else
            {
                    if (dal.Add(model) > 0)
                    {
                        if (userseesion.GroupId == 22)
                        {
                            JScript.alertBack("ok", "添加成功", this.Page);
                        }
                        else
                        {
                            JScript.alert("ok", "添加成功,点击确定返回列表", "Admin_UserInfo.aspx?", this.Page);
                        }
                    }
            }
        }
        
    }
}