using Common;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MyAdmin_AdminMessageConfig : PageBase
{
    Db.MsgConfigDal dal = new Db.MsgConfigDal();
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

    protected void Button2_Click(object sender, EventArgs e)
    {

        Model.MsgConfigModel m = new Model.MsgConfigModel();

        m.MsgTitle = this.txtTitle.Text;
        m.MsgType = Common.TypeHelper.ObjectToInt(this.ddlMsgType.SelectedItem.Value, 0).ToString();
        m.SupplierId = Common.TypeHelper.ObjectToInt(this.ddlSupplierId.SelectedItem.Value, 0);
        m.MsgTemp = this.txtMessage.Text;

        if (string.IsNullOrEmpty(m.MsgTitle))
        {
            Common.JScript.alert("a", "请填写标题", "AdminMessageConfig.aspx", this.Page);
            return;
        }
        if (m.SupplierId == 0)
        {
            Common.JScript.alert("a", "请选择运营商", "AdminMessageConfig.aspx", this.Page);
            return;
        }
        if (m.MsgType == "0")
        {
            Common.JScript.alert("a", "请选择发送类型", "AdminMessageConfig.aspx", this.Page);
            return;
        }
        if (string.IsNullOrEmpty(m.MsgTemp))
        {
            Common.JScript.alert("a", "请填写内容", "AdminMessageConfig.aspx", this.Page);
            return;
        }

        dal.Add(m);

        Response.Redirect("AdminMessageConfig.aspx");
        return;

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
            string Title = ((TextBox)this.menuList.Items[e.Item.ItemIndex].FindControl("txtEditTitle")).Text.Trim();
            string MsgType = ((DropDownList)this.menuList.Items[e.Item.ItemIndex].FindControl("txtMsgType")).SelectedValue.ToString();
            string SupplierId = ((DropDownList)this.menuList.Items[e.Item.ItemIndex].FindControl("txtSupplierId")).SelectedValue.ToString();
            string MsgTemp1 = ((TextBox)this.menuList.Items[e.Item.ItemIndex].FindControl("txtEditMessage")).Text.Trim(); 

            int UserId = int.Parse(e.CommandArgument.ToString());

            Model.MsgConfigModel m = dal.GetModel(UserId);

            if (m.Id > 0)
            {
                m.MsgType = MsgType;
                m.SupplierId = Common.TypeHelper.ObjectToInt(SupplierId, 0);
                m.MsgTemp = MsgTemp1;
                m.MsgTitle = Title;

                dal.Update(m);
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