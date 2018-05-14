using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MyAdmin_Default_Default : PageBase
{

    Db.UserInfoDal dal = new Db.UserInfoDal();
    public string title = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int menubid = Common.TypeHelper.ObjectToInt(Request["menubid"], 0);
            int gid = Common.TypeHelper.ObjectToInt(userseesion.GroupId, 0);
            DataTable dt = dal.GetMenuListByUser(gid);
            StringBuilder str = new StringBuilder();

            str.AppendLine("<li><a href=\"/myadmin/report/report.aspx\" target='main'><span class=\"icon color5\"><i class=\"fa fa-bar-chart\"></i></span>Dashboard</a></li>");
            str.AppendLine("<li><a href=\"main.aspx\" target='main'><span class=\"icon color5\"><i class=\"fa fa-home\"></i></span>My Home</a></li>");
             
            string url = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                url = dt.Rows[i]["MenuUrl"].ToString();
                string MenuName = dt.Rows[i]["MenuName"].ToString();

                str.AppendLine("<li><a href=\"#\"><span class=\"icon color7\">" + dt.Rows[i]["MenuUrl"].ToString() + "</span>" + MenuName);
                
                DataTable dts = dal.GetMenuListForSid(gid, Common.TypeHelper.ObjectToInt(dt.Rows[i]["MenuId"].ToString(), 0));
                if (dts.Rows.Count > 0)
                {
                    str.AppendLine("<span class=\"caret\"></span>");
                }
                str.AppendLine("</a>");

                str.AppendLine("<ul>");
                for (int ii = 0; ii < dts.Rows.Count; ii++)
                {
                    str.AppendLine(" <li><a href='" + dts.Rows[ii]["MenuUrl"].ToString() + "' target='main'>" + dts.Rows[ii]["MenuName"].ToString() + "</a></li>");
                }
                str.AppendLine("</ul>");
                str.AppendLine("</li>");

                if (MenuName == "短信配置管理" || MenuName == "隐私控制" )
                {
                    str.AppendLine("</ul>");
                    str.AppendLine("<ul class=\"sidebar-panel nav\">");
                }
            }

            this.menus.InnerHtml = str.ToString();

            title =WebFramework.GeneralMethodBase.GetKeyConfig(10).Val; 
        }


    }
}