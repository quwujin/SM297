using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;

public partial class User_Create_Menu : PageBase
{
    public Db.MenuDal dal = new Db.MenuDal();
    public string menuUrl = "menubid=" + HttpContext.Current.Request["menubid"] + "&menusid=" + HttpContext.Current.Request["menusid"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["action"]=="add")
        {
            Model.MenuInfoModel model = new Model.MenuInfoModel();
            model.MenuName = Request["menuName"];
            model.MenuUrl = Request["menuUrl"];
            model.OrderId = Common.TypeHelper.ObjectToInt(Request["OrderId"], 0);
            model.Bid =Common.TypeHelper.ObjectToInt( Request["bid"],0);
            model.StatusId = Common.TypeHelper.ObjectToInt(Request["StatusId"], 0);

            int i=dal.Add(model);
            if (i>0)
            {
                JScript.alert("ok", "操作成功", this.Page,"admin_menu.aspx?"+menuUrl);
            }
            else
            {

            }
            
        }
    }
}