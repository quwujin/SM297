using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using System.Text;

public partial class User_Admin_Post : PageBase
{
    
    public Db.UserInfPostDal group = new Db.UserInfPostDal();
    public string menuUrl = "menubid=" + HttpContext.Current.Request["menubid"] + "&menusid=" + HttpContext.Current.Request["menusid"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            this.menuList.DataSource = group.GetList("");
            this.menuList.DataBind();
            int id = Common.TypeHelper.ObjectToInt(Request["id"], 0);
            if (id>0)
            {

                Model.UserInfoPostModel model = group.GetModel(id);
                if (model != null)
                {
                    this.txtgroupName.Text = model.PostName;
                    this.txtOrderId.Text = model.OrderId.ToString();
                    this.hidId.Value = id.ToString();
 
                }
            }
         



             

            if (Request["action"] == "del") {
                if (group.Del(id) > 0)
                {
                    JScript.Loction("a", "admin_Post.aspx?"+menuUrl, this.Page);
                }
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        int id = Common.TypeHelper.ObjectToInt(this.hidId.Value, 0);
        Model.UserInfoPostModel model = new Model.UserInfoPostModel();
        model.PostName = this.txtgroupName.Text;
        model.OrderId = Common.TypeHelper.ObjectToInt(this.txtOrderId.Text,10);
        model.PostId = id;
        
       
        if (id>0)
        {
                int j = group.Update(model);
                if (j > 0)
                {
                    JScript.alert("ok", "操作成功", "Admin_Post.aspx?id="+id+"&action=update&" + menuUrl, this.Page);
                }
                else
                {
                    JScript.alert("ok", "出现异常", this.Page);
                }
        }

        else
        {
           
                int j = group.Add(model);
                if (j > 0)
                {
                    JScript.alert("ok", "操作成功", "Admin_Post.aspx?" + menuUrl, this.Page);
                }
                else
                {
                    JScript.alert("ok", "出现异常", this.Page);
                }

            }
        
        }

}