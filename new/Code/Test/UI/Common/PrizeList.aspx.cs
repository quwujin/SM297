using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PrizeList : NBase
{
    public int pcount = 0;
    public string code = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (string.IsNullOrEmpty(Request["GetResult"])==false && Request["GetResult"] == "PrizeList")
        {
            Response.Write(BindPrizeList());
            Response.End();
        }
        if (string.IsNullOrEmpty(Request["GetResult"])==false && Request["GetResult"] == "MobPrizeList")
        {
            Response.Write(BindMobPrizeList());
            Response.End();
        }

        //#region 获取session
        //orderSession = GetSession();
        //if (orderSession == null)
        //{
        //    WebFramework.GeneralMethodBase.WebDebugLog("session不通过-PrizeList");
        //    Ero();
        //    Response.End();
        //    return;
        //}

        //code = orderSession.Code;

        //#endregion

        int psize = 5;
        pcount = new Db.OrderInfoDal().CheckCount("") / psize + 1;
    }


    protected string BindPrizeList()
    {
        string str="";

        DataTable listdata = new Db.OrderInfoDal().GetTopList(" and Jx<>'参与奖' and States=1");
        foreach (DataRow row in listdata.Rows)
        {
            string mob = row["Mob"].ToString().Substring(0, 3) + "****" + row["Mob"].ToString().Substring(7, 4);
            string jx = row["Jx"].ToString();
            str += "<li><b>" + mob + "</b><p>" + jx + "</p></li>";
        }

        return str;
    }
    protected string BindMobPrizeList()
    {

        string mob = Request["mob"];

        if (Common.ValidateHelper.IsMobile(mob)==false) {

            return "mob_err";
        } 
        string str = "";

        DataTable listdata = new Db.OrderInfoDal().GetList(" and Mob='" + mob + "' and Jx<>'参与奖' and States=1");
        if (listdata.Rows.Count <= 0) {
            return "has_no";
        }

        foreach (DataRow row in listdata.Rows)
        {
            //string mob = row["Mob"].ToString().Substring(0, 3) + "****" + row["Mob"].ToString().Substring(7, 4);
            string jx = row["Jx"].ToString();

            str += "<li><b>" + mob + "</b><p>" + jx + "</p></li>";
        }

        return str;
    }
}