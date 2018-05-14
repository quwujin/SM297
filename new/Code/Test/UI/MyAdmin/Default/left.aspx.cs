using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MyAdmin_Default_left : PageBase
{
    Db.UserInfoDal dal = new Db.UserInfoDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int menubid = Common.TypeHelper.ObjectToInt(Request["menubid"], 0);
            int gid = Common.TypeHelper.ObjectToInt(userseesion.GroupId, 0);
            DataTable dt = dal.GetMenuListByUser(gid);
            StringBuilder str = new StringBuilder();
            str.AppendLine("<div class='big'><a   href='../Default/Main.aspx' target='right'>返回桌面</a></div>");

            string url = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                url = dt.Rows[i]["MenuUrl"].ToString();

                str.AppendLine("<div class='big'><a >" + dt.Rows[i]["MenuName"].ToString() + "</a>");

                DataTable dts = dal.GetMenuListForSid(gid, Common.TypeHelper.ObjectToInt(dt.Rows[i]["MenuId"].ToString(), 0));
                str.AppendLine("</div>");

                str.AppendLine("<div class='slist'>");
                for (int ii = 0; ii < dts.Rows.Count; ii++)
                {
                    str.AppendLine(" <a   href='" + dts.Rows[ii]["MenuUrl"].ToString() + "' target='right'>-" + dts.Rows[ii]["MenuName"].ToString() + "</a>");
                }
                str.AppendLine("</div>");

            }


            this.menus.InnerHtml = str.ToString();
        }

        

    }
}