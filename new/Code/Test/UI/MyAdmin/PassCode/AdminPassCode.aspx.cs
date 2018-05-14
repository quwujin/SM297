using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MyAdmin_PassCode_AdminPassCode : PageBase
{
    Db.PassCodeInfoDal dal = new Db.PassCodeInfoDal();
    public string ColumnName = "a.Id;a.Codes;a.CodeIndex;a.CodeName;a.OpenId;a.StatusId;a.CreateTime;a.ActiveTime;a.Mob;a.ActiveIp;a.EventId;a.CustomerId;a.Notes";
    public string RowsName = "编号;激活码;明码;名称;OpenId;状态;订单时间;激活时间;手机号;ActiveIp;EventId;CustomerId;备注";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bd();

            #region 添加搜索项
            string[] rows = RowsName.Split(';');
            string[] colums = ColumnName.Split(';');

            for (int i = rows.Length - 1; i > 0; i--)
            { 
                    this.DropDownListName.Items.Insert(0, new ListItem(rows[i], colums[i]));
            }

            this.DropDownListName.Items.Insert(0, new ListItem("不限", "不限"));
            #endregion
        }
    }

    void bd()
    {

        string sql = "";

        if (this.DropDownListName.SelectedValue != "不限" && string.IsNullOrEmpty(this.tbCheckName.Text) == false)
        {
            sql += " and " + this.DropDownListName.SelectedItem.Value + " like '%" + this.tbCheckName.Text + "%'";
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

 

        AspNetPager1.PageSize = 50;
        int count = dal.GetCount(sql, "");
        AspNetPager1.RecordCount = count;
        int page = AspNetPager1.CurrentPageIndex;
        this.menuList.DataSource = dal.GetList(sql, page, AspNetPager1.PageSize);
        this.menuList.DataBind();


    }
     
    protected void Button2_Click(object sender, EventArgs e)
    {

        string sql = "";

        if (this.DropDownListName.SelectedValue != "不限" && string.IsNullOrEmpty(this.tbCheckName.Text) == false)
        {
            sql += " and " + this.DropDownListName.SelectedItem.Value + " like '%" + this.tbCheckName.Text + "%'";
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

        string NoColum = this.HiddenFieldNum.Value;

        sql += " order by a.Id DESC";

        string joinTab = "";//left join orderinfo as I on I.id=a.Id
        Common.NPOIHelper.ExportByWeb(new Db.OrderInfoDal().GetExcelList(sql, RowsName, ColumnName, joinTab, "PassCodeInfo", NoColum), "", "激活码-" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls");

        
    }



    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        bd();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        bd();
    }

    public string GetTitle()
    {
        string _title = "";

        string[] _rows = RowsName.Split(';');

        for (int i = 0; i < _rows.Length; i++)
        {
            _title += "<th class='rows-index-" + i + "'>" + _rows[i] + "</th>";
        }

        return _title;
    }

    public string GetCheckRows()
    {
        string _title = "<tr>";

        string[] _rows = RowsName.Split(';');

        for (int i = 0; i < _rows.Length; i++)
        {
            if (_rows.Length / 2 == i - 1)
                _title += "</tr><tr>";

            _title += "<td><input type='checkbox' id='rowsBox" + i + "' class='rows-box' value='" + i + "' />" + _rows[i] + "</td>";

        }

        return _title + "</tr>";
    }
    
}