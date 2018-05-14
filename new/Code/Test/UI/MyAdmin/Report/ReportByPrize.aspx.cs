using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MyAdmin_Report_ReportByPrize : PageBase
{

    protected string TITLE = "";
    Db.OrderInfoDal ordal = new Db.OrderInfoDal();

    public int totalOrders = 0;//总订单量
    public int preOrders = 0;//昨日订单量
    public int todayOrders = 0;//今日订单量

    public int prize1 = 0;//一等奖中奖量
    public int prize2 = 0;//二等奖中奖量
    public int prize3 = 0;//三等奖中奖量
    public int prize4 = 0;//四等奖中奖量
    public int prize5 = 0;//五等奖中奖量
    public string hots = "";//热点参与统计

    public int status0 = 0;//待审核订单
    public int status1 = 0;//已审核订单
    public int status2 = 0;//已作废订单
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            totalOrders = GetCount(2, "");
            preOrders = GetCount(1, "");
            todayOrders = GetCount(0, "");

            prize1 = GetCount(100, " and jx='一等奖'");
            prize2 = GetCount(100, " and jx='二等奖'");
            prize3 = GetCount(100, " and jx='三等奖'");
            prize4 = GetCount(100, " and jx='四等奖'");

        }
    }

    public int GetCount(int DayType, string sqlwhere)
    {

        int count = 0;

        switch (DayType)
        {
            case 0:
                count = ordal.CheckCount(" and datediff(day,[createtime],getdate())=0 " + sqlwhere);
                break;
            case 1:
                count = ordal.CheckCount(" and datediff(day,[createtime],getdate())=1 " + sqlwhere);
                break;
            default:
                count = ordal.CheckCount("" + sqlwhere);
                break;
        }

        return count;
    }
}