using System;
using System.Collections.Generic;
using System.Data;
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
    public string AwardsOptions = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            //上传小票项目 如有作废原因
            cancelReason = "[\"作废原因1\",\"作废原因2\"]"; //云审核的作废按钮点击会调用vm.MessageBox('Fail', config.orderid, Num, Reason);  num:数组序号 Reason:作废原因

            #region 获取奖项
            Db.AwardsStatisticsDal AwardDal = new Db.AwardsStatisticsDal();
            DataTable AwardList = AwardDal.GetByColumnList("AwardsName as value,AwardsName as label", " and AwardsType=1 and len(PrizeName)>0");
            AwardsOptions = Common.JsonHelper.DataTableToJson(AwardList);
            #endregion
             
        }

        orderQty = dal.CheckCount(" ");
        orderQty1 = dal.CheckCount(" and States=1");//已完成
        orderQty2 = dal.CheckCount(" and States=-1");//作废
        orderQty0 = dal.CheckCount(" and States=0");//待审核
    }
     
     
}