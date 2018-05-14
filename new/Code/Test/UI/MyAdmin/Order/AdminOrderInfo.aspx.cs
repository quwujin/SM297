using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MyAdmin_AdminOrderInfo : PageBase
{
    Db.OrderInfoDal dal = new Db.OrderInfoDal();
    public string cancelReason = "[]";//作废原因数组
    public int orderQty = 0;
    public int orderQty1 = 0;
    public int orderQty0 = 0;
    public int orderQty2 = 0;
    public string ColumnName = "a.Id;a.OrderCode;a.OpenId;a.NickName;a.HeadImgurl;f.FileName;a.Jx;a.Jp;a.Name;a.Mob;a.CreateTime;a.States;a.Number;a.PrizeCode;a.IDCard;a.Code;a.Sources;a.RedPackMoney;a.HbOrderCode;a.Ip;a.IpAddress;a.Types;a.Adds;a.MobHome;a.Province;a.City;a.Area;a.Account;a.UpdateTime;a.Note;a.Title;a.Texts;a.Age;a.Tdate;f.Hashdata";
    public string RowsName = "编号;订单号;OpenId;微信昵称;微信头像;小票;奖项;奖品;姓名;手机号;订单时间;状态;Number;产品;身份证号;激活码;查询码;红包金额;红包订单号;IP;IP地归;Types;地址;手机号地归;省;市;性别;审核人;审核时间;备注;门店信息;流水号;金额;时间;Hashdata";

    protected void Page_Load(object sender, EventArgs e)
    {

        #region 测试模式时或者管理员 显示删除订单按钮 
        if (userseesion.GroupId == 2)//2管理员 22运营经理
        {
            this.delbt.Visible = true;
        }
         
        #endregion

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

            this.DropDownListName.Items.Insert(0, new ListItem("选择搜索项", "选择搜索项"));
            #endregion

            //上传小票项目 如有作废原因
            //cancelReason = "['作废原因1','作废原因2']"; //云审核的作废按钮点击会调用send(id,'zf',num,'作废原因1') num:数组序号
 
        }

        orderQty = dal.CheckCount(" ");
        orderQty1 = dal.CheckCount(" and States=1");
        orderQty2 = dal.CheckCount(" and States=-1");
        orderQty0 = dal.CheckCount(" and States=0");
    }

    void bd()
    {

        string sql = "";

        int status = Common.Fun.ConvertTo<int>(Request["status"], 100);


        if (this.DropDownListName.SelectedValue != "选择搜索项" && string.IsNullOrEmpty(this.tbCheckName.Text) == false)
        {
            sql += " and " + this.DropDownListName.SelectedItem.Value + " like '%" + this.tbCheckName.Text + "%'";
        }
        if (status ==100)
        {
            if (this.ddlState.SelectedValue != "不限")
            {
                sql += " and a.States = " + Common.TypeHelper.ObjectToInt(this.ddlState.SelectedValue, 0);
            }
        }
        else
        {
            sql += " and a.States = " + status;
        }
       
        if (this.ddlJx.SelectedValue != "不限")
        {
            sql += " and a.Jx = '" + this.ddlJx.SelectedItem.Value + "'";
        }

        if (this.ddlIsOCR.SelectedValue != "不限")
        {
            if (this.ddlIsOCR.SelectedValue == "是")
            {
                sql += " and ap.note is not null ";
            }
            else {
                sql += " and ap.note is null ";
            }
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
        int count = dal.GetCount(sql, "", true);//true 左连接小票表   非小票项目可不传
        AspNetPager1.RecordCount = count;
        int page = AspNetPager1.CurrentPageIndex;
        this.menuList.DataSource = dal.GetList(sql, page, AspNetPager1.PageSize, true);
        this.menuList.DataBind();


        
    }
     
    public string getMenu(string sid,string id,string note)
    {
        string str="";
        if (sid == "0")
        {
            #region 订单操作按钮
            str += "";

            //btn-minier
            str += " <botton type=\"button\" id='successid" + id + "' class=\"btn  btn-success\" onclick=\"send(" + id + ",'ok');\" ><i class='ace-icon fa fa-check'></i>审核通过</botton><br>";
            str += " <botton type=\"button\" id='fail" + id + "' class=\"btn  btn-danger\" onclick=\"send(" + id + ",'zf');\" style='margin-top:10px;' ><i class='ace-icon fa fa-trash-o'></i>作废订单</botton><br>"; 
        

            #region 无法审核按钮
            if (note == "")
            {
                str += " <botton type=\"button\" value=\"无法审核\" class=\"btn btn-info\" style='margin-top:10px;' onclick=\"send(" + id + ",'no');\" /> <i class='ace-icon fa fa-exclamation-circle'></i>无法审核</botton>";
            }
            #endregion

       
            #endregion
        }
        if (sid == "1")
        {
            str += "<span   class='btn btn-xs btn-primary btn-white'>已完成</span>";
        }
        if (sid == "-1")
        { 
            str += "<span   class='zf btn btn-xs btn-primary btn-white'>已作废</span>";
            if(userseesion.GroupId==2)
                str += " <botton type=\"button\" class=\"btn  btn-success\" onclick=\"send(" + id + ",'back');\" ><i class='ace-icon fa fa-check'></i>恢复订单</botton><br>";
        }
        return str;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        string sql = "";

        if (this.DropDownListName.SelectedValue != "不限" && string.IsNullOrEmpty(this.tbCheckName.Text) == false)
        {
            sql += " and " + this.DropDownListName.SelectedItem.Value + " like '%" + this.tbCheckName.Text + "%'";
        }

        if (this.ddlState.SelectedValue != "不限")
        {
            sql += " and a.States = " + Common.TypeHelper.ObjectToInt(this.ddlState.SelectedValue, 0);
        }
        if (this.ddlJx.SelectedValue != "不限")
        {
            sql += " and a.Jx = '" + this.ddlJx.SelectedItem.Value+"'";
        }
        if (this.ddlIsOCR.SelectedValue != "不限")
        {
            if (this.ddlIsOCR.SelectedValue == "是")
            {
                sql += " and ap.note is not null ";
            }
            else
            {
                sql += " and ap.note is null ";
            }
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

        #region 转换类型
        if (userseesion.GroupId != 2 && userseesion.GroupId != 22)
        {
            ColumnName = ColumnName.Replace("a.Mob;", "left(a.Mob,3)+'****'+right(a.Mob,4);");
            ColumnName = ColumnName.Replace("a.PrizeCode;", "case len(a.PrizeCode) when 0 then '' else left(a.PrizeCode,3)+'****'+right(a.PrizeCode,4) end;");
            ColumnName = ColumnName.Replace("a.Name;", "case len(a.Name) when 0 then '' else left(a.Name,1)+'*'+right(a.Name,1) end;");
        }
        ColumnName = ColumnName.Replace("a.Id;", "CONVERT(varchar(50),a.Id);");
        ColumnName = ColumnName.Replace("a.CreateTime;", "CONVERT(varchar(100),a.CreateTime,21);");
        ColumnName = ColumnName.Replace("a.UpdateTime;", "CONVERT(varchar(100),a.UpdateTime,21);");
        ColumnName = ColumnName.Replace("a.States;", "case a.States when 0 then '未审核' when 1 then '已审核' when -1 then '已作废' end;");
        ColumnName = ColumnName.Replace("a.Number;", "CONVERT(varchar(50),a.Number);");
        ColumnName = ColumnName.Replace("a.Types;", "CONVERT(varchar(50),a.Types);");
        #endregion

        Common.NPOIHelper.ExportByWeb(dal.GetExcelList(sql, RowsName, ColumnName, joinTab, "OrderInfo", NoColum, true), "", "参与数据.xlsx");//true 表示导出OCR数据 非小票项目可不填
         
		
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