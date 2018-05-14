using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Log : PageBase
{
    Db.Login_LogDal dal = new Db.Login_LogDal();
 
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

        if (this.txtUserName.Text!="")
        {
            sql += " and a.username like '%" + txtUserName.Text + "%'";

        }

        string stime = this.tbSt1.Text;
        string etime = this.tbSt2.Text;
        DateTime d = Convert.ToDateTime("2000-01-01");
        if (stime != "")
        {
            string t1 = Common.TypeHelper.ObjectToDateTime(stime, d).ToShortDateString();
            sql += " and a.LoginTime >= '" + t1 + " 00:00:00'";
        }
        if (etime != "")
        {
            string t2 = Common.TypeHelper.ObjectToDateTime(etime, d).ToShortDateString();
            sql += " and a.LoginTime <= '" + t2 + " 23:59:59'";
        }
      
        AspNetPager1.PageSize =20;
        int count = dal.GetCount(sql, "");
        AspNetPager1.RecordCount = count;
        int page = AspNetPager1.CurrentPageIndex;
        this.menuList.DataSource = dal.GetList(sql, page, AspNetPager1.PageSize);
        this.menuList.DataBind();


        
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