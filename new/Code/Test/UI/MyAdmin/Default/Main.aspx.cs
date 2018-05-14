using Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default_Main : PageBase
{ 

    protected string TITLE = "";
    Db.OrderInfoDal ordal = new Db.OrderInfoDal();

    public int totalOrders = 0;//总订单量
    public int preOrders = 0;//昨日订单量
    public int todayOrders = 0;//今日订单量
    public int waitOrders = 0;//等待审核订单量
    public int AlreadyOrders = 0;//已审核订单量
    public int zfOrders = 0;//已作废订单量

    public int prize1 = 0;//一等奖中奖量
    public int prize2 = 0;//二等奖中奖量
    public int prize3 = 0;//三等奖中奖量
    public int prize4 = 0;//四等奖中奖量
    public int prize5 = 0;//五等奖中奖量
    public string hots = "";//热点参与统计

    public int status0 = 0;//待审核订单
    public int status1 = 0;//已审核订单
    public int status2 = 0;//已作废订单

    public string Physical = "";

    public int BackDay = 0;
    public int BackCount = 0;
     
    Db.OrderInfoDal OrderDal = new Db.OrderInfoDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
             
            Bind();

            totalOrders = GetCount(2, "");
            preOrders = GetCount(1, "");
            todayOrders = GetCount(0, "");
            status0 = GetCount(2, " and States=0");
            status1 = GetCount(2, " and States=1");
            status2 = GetCount(2, " and States=-1");

            //prize1 = GetCount(100, " and jx='一等奖'");
            //prize2 = GetCount(100, " and jx='二等奖'");
            //prize3 = GetCount(100, " and jx='三等奖'");
            //prize4 = GetCount(100, " and jx='四等奖'");

            #region 24小时订单统计
            //string sql = " select a.*,(select COUNT(1) from OrderInfo where DATEPART(hh, CreateTime) = a.hours) as ok from hoursinfo a";
            //DataTable dt = Db.ConDal.GetList(sql);
            //if (dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        if (i == dt.Rows.Count - 1)
            //        {
            //            hots += "['" + dt.Rows[i][1].ToString() + "点'," + dt.Rows[i][2] + "]";
            //        }
            //        else
            //        {
            //            hots += "['" + dt.Rows[i][1].ToString() + "点'," + dt.Rows[i][2] + "],";
            //        }

            //    }
            //}
            #endregion

            #region 项目参与排名统计
            //this.IpRepeaterList.DataSource = OrderDal.GetGroupByTypeList(5, "Ip");
            //this.IpRepeaterList.DataBind();

            //this.MobRepeaterList.DataSource = OrderDal.GetGroupByTypeList(5, "Mob");
            //this.MobRepeaterList.DataBind();

            //this.OpenIdRepeaterList.DataSource = OrderDal.GetGroupByTypeList(5, "OpenId");
            //this.OpenIdRepeaterList.DataBind();
            #endregion

            #region 项目体检
            Physical = WebFramework.OrderService.OrderMethod.OrderInstance.ObjectPhysical();//"手机号每日参与数量异常"; 
            this.PhysicalTxt.Text = Physical != "项目正常" ? "<span style='color:red;font-size:large'>" + Physical + "</span>" : Physical;
            #endregion

            #region 回库统计总数
            BackTotal();
            #endregion

            
            if (WebFramework.GeneralMethodBase.GetKeyConfig(3).States != 1)
            {
                this.Button2.Visible = false;
            }
        }

    }

    protected void BackTotal()
    {

        BackDay = Common.TypeHelper.StringToInt(WebFramework.GeneralMethodBase.GetKeyConfig(47).Val);

        if (BackDay > 0)
        {
            BackCount = OrderDal.CheckCount(string.Format(" and FilesId=0 and DateStamp<={0} and IsBack=0", DateTime.Now.AddDays(-BackDay).ToString("yyyyMMdd")));
        }
    }

    protected void Bind()
    {
        Db.InfoDal dal = new Db.InfoDal();

        this.infoList.DataSource = dal.GetList("");
        this.infoList.DataBind();

        Db.DictConfigDal Dictdal = new Db.DictConfigDal();

        //this.DictionaryList.DataSource = Dictdal.GetList(" and bid=1");
        //this.DictionaryList.DataBind();

        TITLE = WebFramework.GeneralMethodBase.GetKeyConfig(10).Val;
        this.txtTITLE.Text = TITLE;
        txtStart_Time.Text = DateTime.Parse(WebFramework.GeneralMethodBase.GetKeyConfig(8).Val).ToString("yyyy年MM月dd日 HH时mm分ss秒");
        txtEnd_Time.Text = DateTime.Parse(WebFramework.GeneralMethodBase.GetKeyConfig(9).Val).ToString("yyyy年MM月dd日 HH时mm分ss秒");
        this.IsTest.Text = WebFramework.GeneralMethodBase.GetKeyConfig(3).States == 1 ? "是" : "否";



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

    protected void Button1_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    Db.WebApiInterface_LogDal logDal = new Db.WebApiInterface_LogDal();

        //    Model.WebApiInterface_LogModel logmodel = logDal.GetModelByIdDesc();

        //    List<Model.OrderInfoModel> List = new Db.OrderInfoDal().GetModelByTopList(500, string.Format(" and Id>{0}", logmodel.Id));

        //    while (List.Count != 0)
        //    {

        //        foreach (Model.OrderInfoModel model in List)
        //        {
        //            new Db.WebApiInterface_LogDal().Add(new Model.WebApiInterface_LogModel()
        //            {
        //                CreateTime = DateTime.Now,
        //                OrderId = model.Id,
        //                Remark = "",
        //                ResponseData = WebFramework.OrderService.OrderMethod.OrderInstance.AddOrderApi(model),
        //                StatusId = 0
        //            });

        //        }

        //        logmodel = logDal.GetModelByIdDesc();

        //        List = new Db.OrderInfoDal().GetModelByTopList(500, string.Format(" and Id>{0}", logmodel.Id));
        //    }

        //    Common.JScript.alert("a", "导入成功", this.Page);
        //}
        //catch (Exception ex)
        //{
        //    Common.JScript.alert("a", "导入失败", this.Page);
        //}

    }

    protected void Button2_Click(object sender, EventArgs e)
    {

        if (WebFramework.GeneralMethodBase.GetKeyConfig(3).States != 1)
        {
            Common.JScript.alert("a", "项目已上线，无法删除数据", this.Page);
            return;
        }
        if (userseesion.GroupId != 2)
        {
            Common.JScript.alert("a", "非管理员，无法删除数据", this.Page);
            return;
        }

        string exctSql = "update PassCodeInfo set StatusId=0,Mob='';";
        exctSql += "truncate table OrderInfo;";
        exctSql += "delete GenerationOrderId;";
        exctSql += "truncate table OrderLog;";
        exctSql += "update AwardsStatistics set TodayTotal=0,AllTotal=0,YesterdayTotal=0,BackTotal=0,DateStamp='';";

        new Db.CommonFunctionDal().GetList(exctSql);

        Common.JScript.alert("a", "清除成功", this.Page);

    }
     

}