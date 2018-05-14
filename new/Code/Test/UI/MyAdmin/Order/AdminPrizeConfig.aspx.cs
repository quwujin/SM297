using Common;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MyAdmin_AdminPrizeConfig : PageBase
{
    Db.AwardsStatisticsDal dal = new Db.AwardsStatisticsDal();
    private int id = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            bd();
             
        }
    }

    void bd()
    {

        string sql = "";
          
        AspNetPager1.PageSize = 20;
        int count = dal.GetCount(sql, "");
        AspNetPager1.RecordCount = count;
        int page = AspNetPager1.CurrentPageIndex;
        this.menuList.DataSource = dal.GetList(sql, page, AspNetPager1.PageSize);
        this.menuList.DataBind();



    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        bd();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        bd();
    }
     
    protected void menuList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            id = int.Parse(e.CommandArgument.ToString());
        }
        else if (e.CommandName == "Cancel")
        {
            id = -1;
        }
        else if (e.CommandName == "Update")
        {
            string PrizeName = ((TextBox)this.menuList.Items[e.Item.ItemIndex].FindControl("txtEditPrizeName")).Text.Trim();
            string PresetValue = "";// ((TextBox)this.menuList.Items[e.Item.ItemIndex].FindControl("txtEditPresetValue")).Text.Trim(); 

            int UserId = int.Parse(e.CommandArgument.ToString());

            Model.AwardsStatisticsModel m = dal.GetModel(UserId);

            if (m.Id > 0)
            {
                m.PrizeName = PrizeName;
                m.PresetValue = PresetValue;

                dal.UpdatePrizeNameById(m);
            }

        }
        else if (e.CommandName == "Delete")
        {

        }

        bd();
    }
    protected void menuList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            System.Data.DataRowView record = (System.Data.DataRowView)e.Item.DataItem;
            int userId = int.Parse(record["Id"].ToString());
            if (userId != id)
            {
                ((Panel)e.Item.FindControl("plItem")).Visible = true;
                ((Panel)e.Item.FindControl("plEdit")).Visible = false;
            }
            else
            {
                ((Panel)e.Item.FindControl("plItem")).Visible = false;
                ((Panel)e.Item.FindControl("plEdit")).Visible = true;
            }
        }
    }

}