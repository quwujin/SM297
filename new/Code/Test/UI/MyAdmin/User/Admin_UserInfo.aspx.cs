using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;

public partial class User_Admin_UserInfo : PageBase
{
    public Db.UserInfoDal dal = new Db.UserInfoDal();
    public string menuUrl = "menubid=" + HttpContext.Current.Request["menubid"] + "&menusid=" + HttpContext.Current.Request["menusid"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            this.menuList.DataSource = dal.GetList(" order by userid desc ");
            this.menuList.DataBind();
            int id = Common.TypeHelper.ObjectToInt(Request["id"], 0);
            if (Request["action"] == "del")
            {
                if (dal.Del(id) > 0)
                {
                    JScript.alert("ok", "删除成功", "Admin_UserInfo.aspx?" + menuUrl, this.Page);
                }
            }
        }
    }





   
}