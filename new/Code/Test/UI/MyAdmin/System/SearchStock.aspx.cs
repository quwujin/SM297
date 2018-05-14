using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MyAdmin_System_SearchStock : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        int Sid = Common.TypeHelper.ObjectToInt(this.txtSid.Text, 0);

        if (Sid == 0) {
            Common.JScript.alert("a", "请输入资源Id", "SearchStock.aspx", this.Page);
            return;
        }

        Common.StockResult SearchData=Common.SearchStock.GetStock(Sid, userseesion.UserName + "-" + WebFramework.GeneralMethodBase.GetKeyConfig(22).Val);

        string Text = "";

        if (SearchData.list != null && SearchData.list.Count > 0)
        {

            foreach (Common.StockListResult model in SearchData.list)
            {
                Text += "资源编号：" + model.SupplierId;
                Text += "  资源名称：" + model.SupplierName;
                Text += "  资源剩余量：" + model.Qty;
                Text += "<br/>";
            }

        }
        else {
            Text = "查询失败";
        }

        this.LbSurplus.Text = Text;

    }


    public string GetStockList() { 

        string ReturnData="";

        var StockList = WebFramework.GeneralMethodBase.GetKeyConfig(28).Val.Split('|');
        var ProjectNo= WebFramework.GeneralMethodBase.GetKeyConfig(22).Val;

        for (int i = 0; i < StockList.Length; i++)
        {
            int EvenId = Common.TypeHelper.ObjectToInt(StockList[i]);

            if (EvenId > 0) {

                Common.StockListResult Result = Common.SearchStock.GetStock(EvenId, userseesion.UserName + "-" + ProjectNo).list[0];

                ReturnData += "<tr>";
                ReturnData += "<td>" + Result.SupplierId + "</td>";
                ReturnData += "<td>" + Result.SupplierName+ "</td>";
                ReturnData += "<td>" + Result.Qty + "</td>";
                ReturnData += "</tr>";
            
            }

            
        }

        return ReturnData;
    }
}