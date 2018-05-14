using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminOperationLog : PageBase
{
    Db.Operation_LogDal dal = new Db.Operation_LogDal();
    public string ColumnName = "a.Id;a.Mobile;a.OrderCode;a.OperationType;a.Description;a.LStatus;a.Status;a.CreateTime;a.UpdateTime;a.UserName;a.Remark;a.HideContent";
    public string RowsName = "编号;手机号;订单号;操作类型;操作简介;订单原状态;修改后状态;创建时间;修改时间;操作人;备注;内容";

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
                if (i.ToString() != "5" && i.ToString() != "9")
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
        Common.NPOIHelper.ExportByWeb(new Db.OrderInfoDal().GetExcelList(sql, RowsName, ColumnName, joinTab, "Operation_Log", NoColum), "", "操作日志-" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls");
		
		
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
            if (_rows.Length / 2 == i-1)
                _title += "</tr><tr>";

            _title += "<td><input type='checkbox' id='rowsBox" + i + "' class='rows-box' value='" + i + "' />" + _rows[i] + "</td>";

        }

        return _title + "</tr>";
    }
    
}