using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;
using System.Text;

public partial class inc_Top : System.Web.UI.Page
{
    Db.UserInfoDal dal = new Db.UserInfoDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int menubid =Common.TypeHelper.ObjectToInt(Request["menubid"], 0);
            if (Request.Cookies["iGroupId"]!=null && Request.Cookies["iGroupId"].Value!="")
	        {

                int gid = Common.TypeHelper.ObjectToInt(Request.Cookies["iGroupId"].Value, 0);
                DataTable dt = dal.GetMenuListByUser(gid);
                StringBuilder str = new StringBuilder();
                str.AppendLine("<div class='t' href='../Default/Main.aspx?menubid=0' >返回桌面</div>");
                string url = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    url = dt.Rows[i]["MenuUrl"].ToString();
                   if( url.IndexOf('?')>-1)
                   {
                       url = url + "&menubid=" + dt.Rows[i]["MenuId"].ToString();
                      
                   }
                    else
	               {
                       url = url+"?menubid=" + dt.Rows[i]["MenuId"].ToString();
	               }

                   str.AppendLine("<div  class='t' href='" + url + "' >" + dt.Rows[i]["MenuName"].ToString() + "</div>");
                }
                this.menu.InnerHtml = str.ToString();
	        }
           
        }
    }
}