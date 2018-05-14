using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MyAdminDictionary : PageBase
{
    Db.DictConfigDal dal = new Db.DictConfigDal();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.menuList.DataSource = dal.GetList(" and bid =0 order by orderid desc");
            this.menuList.DataBind();

            this.ddlBid.DataSource = dal.GetList(" and bid =0 order by orderid desc");
            this.ddlBid.DataTextField = "Title";
            this.ddlBid.DataValueField = "Id";
            this.ddlBid.DataBind();
            this.ddlBid.Items.Add(new ListItem("一级类", "0"));
            this.ddlBid.SelectedValue = "0";


            //删除
            if (Request["action"] == "del")
            {
                int id = Common.TypeHelper.ObjectToInt(Request["id"], 0);
                if (dal.IsHave(id)>0)
                {
                    JScript.alert("p", "该菜单下有子类，不能删除", "Admin_movie_dconfig.aspx", this.Page);
                }
                else
                {
                    if (dal.Del(id) > 0)
                    {
                        JScript.Loction("p", "Admin_movie_dconfig.aspx" , this.Page);
                    }
                }
                
            }


            ///添加
            if (Request["action"] == "add")
            {
                int bid = Common.TypeHelper.ObjectToInt(Request["bid"], 0);
                this.ddlBid.SelectedValue = bid.ToString();

            }


            //修改

            if (Request["action"] == "update")
            {
                int id =Common.TypeHelper.ObjectToInt(Request["id"], 0);
                Model.DictConfigModel m = dal.GetModel(id);
                this.hidid.Value = m.Id.ToString();
                this.ddlBid.SelectedValue = m.Bid.ToString();
                this.txtTitle.Text = m.Title.ToString();
                this.txtVal.Text = m.Val.ToString();
                this.txtOrderId.Text = m.OrderId.ToString();
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Model.DictConfigModel model = new Model.DictConfigModel();
        model.Title = this.txtTitle.Text ;
        model.Val =this.txtVal.Text;
        model.OrderId = Common.TypeHelper.ObjectToInt(this.txtOrderId.Text, 100);
        model.Bid = Common.TypeHelper.ObjectToInt(this.ddlBid.SelectedValue, 0);
        int id = Common.TypeHelper.ObjectToInt(this.hidid.Value, 0);
        model.Id = id;

        if (id>0)
        {
            if (dal.Update(model) > 0)
            {
                JScript.Loction("p", "Admin_movie_dconfig.aspx", this.Page);
            }
        }
        else
        {
            if (dal.Add(model) > 0)
            {
                JScript.Loction("p", "Admin_movie_dconfig.aspx", this.Page);
            }
        }
        
    }

    protected void replist_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            Label bid = (Label)e.Item.FindControl("bid");
            string bbid = bid.Text.ToString();
  
            Repeater rptProduct = (Repeater)e.Item.FindControl("menuList_sid");
            DataRowView rowv = (DataRowView)e.Item.DataItem;
            rptProduct.DataSource = dal.GetList(" and bid=" + bbid);
            rptProduct.DataBind();
        }
    }

   
}