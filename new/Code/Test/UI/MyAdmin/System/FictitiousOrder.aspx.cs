using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MyAdmin_System_FictitiousOrder : PageBase
{
    Db.FictitiousOrderDal dal = new Db.FictitiousOrderDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            bd();
        }
    }

    void bd()
    {

        string sql = "";

        if (this.ddlJx.SelectedValue != "不限")
        {
            sql += " and a.Jx = '" + this.ddlJx.SelectedItem.Value + "'";
        }
         

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
        int count = dal.GetCount(sql, "");
        AspNetPager1.RecordCount = count;
        int page = AspNetPager1.CurrentPageIndex;
        this.menuList.DataSource = dal.GetList(sql, page, AspNetPager1.PageSize);
        this.menuList.DataBind();



    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        string sql = "";
         
        if (this.ddlState.SelectedValue != "不限")
        {
            sql += " and a.States = " + Common.TypeHelper.ObjectToInt(this.ddlState.SelectedValue, 0);
        }
        if (this.ddlJx.SelectedValue != "不限")
        {
            sql += " and a.Jx = '" + this.ddlJx.SelectedItem.Value + "'";
        } 

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

        sql += " order by a.Id DESC";

        string joinTab = "";
         
        Common.NPOIHelper.ExportByWeb(dal.GetExcelList(sql), "", "参与数据.xlsx");


    }


    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        bd();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        bd();
    }

}