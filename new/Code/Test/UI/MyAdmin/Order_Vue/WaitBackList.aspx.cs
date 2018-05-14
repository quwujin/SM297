using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MyAdmin_Order_Vue_WaitBackList : PageBase
{
    Db.OrderInfoDal OrderInfoDal = new Db.OrderInfoDal();

    int BackDay = Common.TypeHelper.StringToInt(WebFramework.GeneralMethodBase.GetKeyConfig(47).Val);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bd();
             

        }
    }

    void bd()
    {
        
        if (BackDay > 0)
        {

            string sql = string.Format(" and FilesId=0 and DateStamp<={0} and IsBack=0", DateTime.Now.AddDays(-BackDay).ToString("yyyyMMdd"));

            string stime = this.tbSt1.Text;
            string etime = this.tbSt2.Text;
            DateTime d = Convert.ToDateTime("2000-01-01");
            if (stime != "")
            {
                string t1 = Common.TypeHelper.ObjectToDateTime(stime, d).ToShortDateString();
                sql += " and a.CreateTime >= '" + t1 + " 00:00:00'";
            }
            if (etime != "")
            {
                string t2 = Common.TypeHelper.ObjectToDateTime(etime, d).ToShortDateString();
                sql += " and a.CreateTime <= '" + t2 + " 23:59:59'";
            }

            AspNetPager1.PageSize = 10;
            int count = OrderInfoDal.GetCount(sql, "");
            AspNetPager1.RecordCount = count;
            int page = AspNetPager1.CurrentPageIndex;
            this.menuList.DataSource = OrderInfoDal.GetList(sql, page, AspNetPager1.PageSize);
            this.menuList.DataBind();
        }  
    }
     

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        bd();
    } 

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (BackDay > 0)
        {
            Model.Operation_LogModel mdlog = new Model.Operation_LogModel();
            
            mdlog.CreateTime = DateTime.Now;
            mdlog.Description = "";
            mdlog.LStatus = 1;
            mdlog.Mobile = "";
            mdlog.OperationType = "批量回库";
            mdlog.OrderCode = "";
            mdlog.Status = 0;
            mdlog.UpdateTime = DateTime.Now;
            mdlog.UserName = userseesion.UserName;
            mdlog.Remark = "";
            mdlog.HideContent = "";

            if (OrderInfoDal.BackAll(DateTime.Now.AddDays(-BackDay).ToString("yyyyMMdd"), mdlog) > 0)
            {
                JScript.alert("a", "操作成功", "", this.Page);
            }
            else {
                JScript.alert("a", "操作失败", "", this.Page);
            }
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        bd();
    }
}