using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using System.Text;

public partial class inc_LeftMenu : System.Web.UI.UserControl
{
    public Db.UserInfoDal menudal = new Db.UserInfoDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int menu_bid = Common.TypeHelper.ObjectToInt(Request["menubid"], 0);
            int menu_sid = Common.TypeHelper.ObjectToInt(Request["menusid"], 0);
             if (menu_bid>0 && Request.Cookies["iGroupId"]!=null && Request.Cookies["iGroupId"].Value!="")
	            {
                    int gid = Common.TypeHelper.ObjectToInt(Request.Cookies["iGroupId"].Value, 0);
                 DataTable dt = menudal.GetMenuListForSid(gid, menu_bid);
                 StringBuilder menu_str = new StringBuilder();
                 string url = "";
                 string sel="";
                 for (int i = 0; i < dt.Rows.Count; i++)
                 {
                     sel = menu_sid == Common.TypeHelper.ObjectToInt(dt.Rows[i]["MenuId"], 0) ? "2" : "";
                     url = dt.Rows[i]["MenuUrl"].ToString();
                     url= url.IndexOf('?') > -1 ?   url + "&menubid=" + menu_bid+"&menusid="+dt.Rows[i]["MenuId"]+"":url + "?menubid=" + menu_bid + "&menusid=" + dt.Rows[i]["MenuId"] + "";
                  
                     menu_str.AppendLine("<div class='leftmenu"+sel+"'><a   href='"+url+"'>"+dt.Rows[i]["MenuName"].ToString()+"</a></div>");
                 }
                 this.menu.InnerHtml = menu_str.ToString();
                
                 Db.MenuDal m = new Db.MenuDal();
                 this.bname.Text = m.GetModel(menu_bid).MenuName;
	            }
           
        }
    }
}