using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MyAdmin_AdminPrizeLog : PageBase
{
    Db.Cj_LogDal dal = new Db.Cj_LogDal();
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
       
        if (this.tbMob.Text != "")
        {
            sql += " and a.Mob like '%" + this.tbMob.Text.Trim() + "%'";
        }
	   
	    if (this.ddlState.SelectedValue != "不限")
        {
            sql += " and a.States = " + Common.TypeHelper.ObjectToInt(this.ddlState.SelectedValue, 0);
        }
      
        string stime = this.tbSt1.Text;
        string etime = this.tbSt2.Text;
        DateTime d = Convert.ToDateTime("2000-01-01");
        if (stime != "")
        {
            string t1 = Common.TypeHelper.ObjectToDateTime(stime, d).ToShortDateString();
            sql += " and a.CTIME >= '" + t1 + " 00:00:00'";
        }
        if (etime != "")
        {
            string t2 = Common.TypeHelper.ObjectToDateTime(etime, d).ToShortDateString();
            sql += " and a.CTIME <= '" + t2 + " 23:59:59'";
        }
      
        AspNetPager1.PageSize = 20;
        int count = dal.GetCount(sql, "");
        AspNetPager1.RecordCount = count;
        int page = AspNetPager1.CurrentPageIndex;
        this.menuList.DataSource = dal.GetList(sql, page, AspNetPager1.PageSize);
        this.menuList.DataBind();

         
    }
     
   
    public string getMenu(string sid,string id)
    {
        string str="";
        if (sid == "0")
        { 
            str += " <input type=\"button\" value=\"已完成\" class=\"bt\" onclick=\"send(" + id + ",'ok');\" /><br />";
            str += " <input type=\"button\" value=\"作废\" class=\"bt\" onclick=\"send(" + id + ",'zf');\" />";
        }
        if (sid == "1")
        {
            str += "<a class='ok'>已完成</>";
        }
        if (sid == "-1")
        {
            str += "<a class='zf'>已作废</>";
        }
        return str;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        string sql = "";
       
        if (this.tbMob.Text != "")
        {
            sql += " and a.Mob like '%" + this.tbMob.Text.Trim() + "%'";
        }
        if (this.ddlState.SelectedValue != "不限")
        {
            sql += " and a.States = " + Common.TypeHelper.ObjectToInt(this.ddlState.SelectedValue, 0);
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

      

        string ckName = "编号,订单号,OpenId,手机号,奖项,奖品,Ip,省,市,地址,串码,备注,订单时间,状态";
        string ckField = "a.Id,a.OrderCode,a.OpenId,a.Mob,a.Jx,a.Jp,a.Ip,a.Pros,a.City,a.Adds,a.Code,a.Note,a.CTime,a.States";
        string joinTab = "";//left join orderinfo as I on I.id=a.Id
        Common.NPOIHelper.ExportByWeb(dal.GetExcelList(sql, ckName, ckField, joinTab), "","抽奖订单"+ DateTime.Now.ToString("yyyyMMddHHmm") + ".xls");
		
		
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