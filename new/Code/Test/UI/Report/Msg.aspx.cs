using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Msg : System.Web.UI.Page
{
    Db.OrderInfoDal orderDal = new Db.OrderInfoDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (string.IsNullOrEmpty(Request["GetType"]))//非后台查看  验证签名
            {
                string Key = "";
                string dt = string.IsNullOrEmpty(Request["dt"]) ? "" : Request["dt"];
                string sign = string.IsNullOrEmpty(Request["sign"]) ? "" : Request["sign"];

                string sign2 = Common.getMD5.MD5(Key + dt).ToUpper();
                if (sign2 != sign)
                {
                    Response.Write("签名不一致");
                    Response.End();
                    return;
                }
            }

            string address_to = ""; 
            address_to += "test@esmartwave.com;";
             
            if (string.IsNullOrEmpty(Request["GetType"]) == false) //后台查看 直接输出内容
            {
                Response.Write(GetHtml("<br/>"));
                return;
            }

            string ProjectNo = WebFramework.GeneralMethodBase.GetKeyConfig(22).Val;
            string ProjectName = WebFramework.GeneralMethodBase.GetKeyConfig(10).Val;
            Common.Email.EmailTool.sendEmail(address_to, "【日报】" + ProjectNo + "-" + ProjectName + "-" + DateTime.Now.ToString("MM/dd"), GetHtml(""), "");

            Response.Write("调用成功");

        }
    }

    protected string GetHtml(string Style)
    {
          
        StringBuilder bodyText = new StringBuilder();

        string DateStamp = DateTime.Now.ToString("yyyyMMdd");

        bodyText.Append("活动概况：" + "\r\n" + Style + Style);
        bodyText.Append("活动描述：参与活动上传生成二次元照，先中奖后上传小票" + "\r\n" + Style);
        bodyText.Append("活动时间：2017年6月28日-2017年7月25日" + "\r\n" + Style);
        bodyText.Append("活动参与情况：截止" + DateTime.Now.ToShortDateString() + " 23:59:59  \r\n");
        bodyText.Append("今日参与活动：" + GetCount(0, "") + "\r\n" + Style);
        bodyText.Append("今日通过审核：" + GetCount(0, " and States=1") + "\r\n" + Style);
        bodyText.Append("昨日参与活动：" + GetCount(1, "") + "\r\n" + Style);
        bodyText.Append("昨日通过审核：" + GetCount(1, " and States=1") + "\r\n" + Style);
        bodyText.Append("总参与：" + GetCount(2, "") + "\r\n" + Style);
        bodyText.Append("总审核通过：" + GetCount(2, " and States=1") + "\r\n" + Style);

        List<Model.AwardsStatisticsModel> list = new Db.AwardsStatisticsDal().GetModelTopList(3, " and AwardsType=1 and (id=1 or id=2 or id=7)");
        foreach (Model.AwardsStatisticsModel model in list) {
            bodyText.Append(model.AwardsName + "发放总数：" + model.AllTotal + ",今日" + model.AwardsName + "发放数：" + model.TodayTotal + ",昨日" + model.AwardsName + "发放数：" + model.YesterdayTotal + "\r\n" + Style);
        }

        bodyText.Append("\r\n \r\n " + Style + Style);

        #region 参与总数排名
        DataTable IpRepeaterList = orderDal.GetGroupByTypeList(5, "Ip");
        for (int i=0; i < IpRepeaterList.Rows.Count; i++) {
            bodyText.Append("Ip参与总数排名：" + IpRepeaterList.Rows[i]["Ip"] + "  总数：" + IpRepeaterList.Rows[i]["Total"] + "\r\n" + Style);
        }
        bodyText.Append("\r\n "  + Style);

        DataTable MobRepeaterList = orderDal.GetGroupByTypeList(5, "Mob");
        for (int i = 0; i < MobRepeaterList.Rows.Count; i++)
        {
            bodyText.Append("手机号参与总数排名：" + MobRepeaterList.Rows[i]["Mob"] + "  总数：" + MobRepeaterList.Rows[i]["Total"] + "\r\n" + Style);
        }
        bodyText.Append("\r\n"  + Style);

        DataTable OpenIdRepeaterList = orderDal.GetGroupByTypeList(5, "OpenId");
        for (int i = 0; i < OpenIdRepeaterList.Rows.Count; i++)
        {
            bodyText.Append("OpenId参与总数排名：" + OpenIdRepeaterList.Rows[i]["OpenId"] + "  总数：" + OpenIdRepeaterList.Rows[i]["Total"] + "\r\n" + Style);
        }
        #endregion

        bodyText.Append("\r\n \r\n " + Style + Style);
        bodyText.Append("项目体检结果： " + WebFramework.OrderService.OrderMethod.OrderInstance.ObjectPhysical());

        return bodyText.ToString();
    }

    public int GetCount(int DayType,string sqlwhere) {

        int count = 0;

        switch (DayType) { 
            case 0:
                count = orderDal.CheckCount(" and DateStamp='" + DateTime.Now.ToString("yyyyMMdd") + "' " + sqlwhere);
                break;
            case 1:
                count = orderDal.CheckCount(" and DateStamp='" + DateTime.Now.AddDays(-1).ToString("yyyyMMdd") + "' " + sqlwhere);
                break;
            default:
                count = orderDal.CheckCount("" + sqlwhere);
                break;
        }

        return count;
    }


}