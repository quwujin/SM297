using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;

public partial class User_Admin_Menu : PageBase
{
    public Db.MenuDal dal = new Db.MenuDal();
    public string menuUrl = "menubid=" + HttpContext.Current.Request["menubid"] + "&menusid=" + HttpContext.Current.Request["menusid"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.menuList.DataSource = dal.GetList(" and bid =0 order by orderid desc");
            this.menuList.DataBind();

            this.ddlBid.DataSource = dal.GetList(" and bid =0 order by orderid desc");
            this.ddlBid.DataTextField = "MenuName";
            this.ddlBid.DataValueField = "MenuId";
            this.ddlBid.DataBind();
            this.ddlBid.Items.Add(new ListItem("一级菜单", "0"));
            this.ddlBid.SelectedValue = "0";


            //删除
            if (Request["action"] == "del")
            {
                int id = Common.TypeHelper.ObjectToInt(Request["id"], 0);
                if (dal.IsHave(id)>0)
                {
                    JScript.alert("p", "该菜单下有子菜单，不能删除", "admin_Menu.aspx?"+menuUrl, this.Page);
                }
                else
                {
                    if (dal.Del(id) > 0)
                    {

                        JScript.Loction("p", "admin_Menu.aspx?" + menuUrl, this.Page);
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
                int id = Common.TypeHelper.ObjectToInt(Request["id"], 0);
                Model.MenuInfoModel model = dal.GetModel(id);
                this.txtMenuName.Text = model.MenuName;
                this.txtMenuUrl.Text = model.MenuUrl;
                this.txtOrderId.Text = model.OrderId.ToString();
                this.ddlBid.SelectedValue = model.Bid.ToString();
                this.radStatusId.SelectedValue = model.StatusId.ToString();
                this.hidId.Value = id.ToString();
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Model.MenuInfoModel model = new Model.MenuInfoModel();
        model.MenuName = this.txtMenuName.Text ;
        model.MenuUrl =this.txtMenuUrl.Text;
        model.OrderId = Common.TypeHelper.ObjectToInt(this.txtOrderId.Text, 100);
        model.Bid = Common.TypeHelper.ObjectToInt(this.ddlBid.SelectedValue, 0);
        model.StatusId = Common.TypeHelper.ObjectToInt(this.radStatusId.SelectedValue, 0);
        int id = Common.TypeHelper.ObjectToInt(this.hidId.Value, 0);
        model.MenuId = id;

        if (id>0)
        {
            if (dal.Update(model) > 0)
            {
                JScript.Loction("p", "admin_Menu.aspx?action=add&" + menuUrl, this.Page);
            }
        }
        else
        {
            if (dal.Add(model) > 0)
            {
                JScript.Loction("p", "admin_Menu.aspx?action=add&" + menuUrl, this.Page);
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