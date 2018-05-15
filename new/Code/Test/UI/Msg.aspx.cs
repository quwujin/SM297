using System;
using System.Collections.Generic;
using System.Configuration;
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
            //todo 日报发送修改
            if (string.IsNullOrEmpty(Request["GetType"]))//非后台查看  验证签名
            {
                string Key = "ZFKV3T145G";
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
            address_to += "wujin.qu@esmartwave.com;mello.cao@esmartwave.com;edith.chen@esmartwave.com;maymay.xuan@esmartwave.com;uno.lv@esmartwave.com";//lucy.wang@esmartwave.com;

            if (string.IsNullOrEmpty(Request["GetType"]) == false) //后台查看 直接输出内容
            {
                Response.Write(GetHtml("<br/>"));
                return;
            }

            Common.Email.EmailTool.sendEmail(address_to, "【日报】-SM297-士力架世界杯活动", GetHtml(""), "");
           
            Response.Write("调用成功");

        }
    }

    protected string GetHtml(string Style)
    {
          
        StringBuilder bodyText = new StringBuilder();

        string DateStamp = DateTime.Now.ToString("yyyyMMdd");

        string jiezhiriqi = DateTime.Now.AddDays(-1).ToShortDateString();


        bodyText.Append("活动概况：" + "\r\n" + Style + Style);
        bodyText.Append("活动描述：上传小票抽奖" + "\r\n" + Style);
        bodyText.Append("活动时间：2018年6月1日-2018年7月15日" + "\r\n" + Style);
        bodyText.Append("活动参与情况：截止" + jiezhiriqi + " 23:59:59  \r\n");
        bodyText.Append("今日参与活动：" + GetCount(0, "") + "\r\n" + Style);
        bodyText.Append("今日通过审核：" + GetCount(0, " and States=1") + "\r\n" + Style);
        bodyText.Append("昨日参与活动：" + GetCount(1, "") + "\r\n" + Style);
        bodyText.Append("昨日通过审核：" + GetCount(1, " and States=1") + "\r\n" + Style);
        bodyText.Append("总参与：" + GetCount(2, " and createtime<'"+DateTime.Now.ToString("yyyy-MM-dd")+"'") + "\r\n" + Style);
        bodyText.Append("总审核通过：" + GetCount(2, " and States=1 and createtime<'" + DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + "'") + "\r\n" + Style);


        List<string> list = new List<string>();
        list.Add("机票");
        list.Add("足球");
        //list.Add("三等奖");
        foreach (var item in list)
        {
            int zongfafang = GetCount(2, " and states=1 and jx='" + item + "'");
            int jinrifafang = GetCount(0, " and states=1 and jx='" + item + "'");
            int zuorifafang= GetCount(1, " and states=1 and jx='" + item + "'");

            bodyText.Append(item + "发放总数：" + zongfafang + ",今日" + item + "发放数：" + jinrifafang + ",昨日" + item + "发放数：" + zuorifafang + "\r\n" + Style);
        }

        //List<Model.AwardsStatisticsModel> list = new Db.AwardsStatisticsDal().GetModelTopList(3, " and AwardsType=1 and (id=1 or id=2 or id=7)");
        //foreach (Model.AwardsStatisticsModel model in list) {
        //    bodyText.Append(model.AwardsName + "发放总数：" + model.AllTotal + ",今日" + model.AwardsName + "发放数：" + model.TodayTotal + ",昨日" + model.AwardsName + "发放数：" + model.YesterdayTotal + "\r\n" + Style);
        //}

        bodyText.Append("\r\n \r\n " + Style + Style);


        //bodyText.Append("\r\n \r\n " + Style + Style);
        //bodyText.Append("项目体检结果： " + WebFramework.GeneralMethodBase.ObjectPhysical());

        return bodyText.ToString();
    }

    public int GetCount(int DayType,string sqlwhere) {

        int count = 0;

        switch (DayType) { 
            case 0:
                count = orderDal.CheckCount(" and DateStamp='" + DateTime.Now.AddDays(-1).ToString("yyyyMMdd") + "' " + sqlwhere);
                break;
            case 1:
                count = orderDal.CheckCount(" and DateStamp='" + DateTime.Now.AddDays(-2).ToString("yyyyMMdd") + "' " + sqlwhere);
                break;
            default:
                count = orderDal.CheckCount(" and createtime<'"+DateTime.Now.ToString("yyyy-MM-dd")+"'" + sqlwhere);
                break;
        }

        return count;
    }


}