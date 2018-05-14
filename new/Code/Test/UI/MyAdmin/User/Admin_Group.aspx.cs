using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using System.Text;

public partial class User_Admin_Group : PageBase
{
    public Db.MenuDal dal = new Db.MenuDal();
    public Db.UserInfoGroupDal group = new Db.UserInfoGroupDal();
    public string menuUrl = "menubid=" + HttpContext.Current.Request["menubid"] + "&menusid=" + HttpContext.Current.Request["menusid"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            this.menuList.DataSource = group.GetList("");
            this.menuList.DataBind();
            int id = Common.TypeHelper.ObjectToInt(Request["id"], 0);
            if (id>0)
            {

                Model.UserInfoGroupModel model = group.GetModel(id);
                if (model != null)
                {
                    this.txtgroupName.Text = model.GroupName;

                    this.hidId.Value = id.ToString();

                    DataTable dt = dal.GetList(" and bid=0 and statusid=1 ");
                    StringBuilder str = new StringBuilder();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        

                           str.Append("<div class='col-xs-12 widget-container-col ui-sortable'>");
						    str.Append("<div class='widget-box ui-sortable-handle'> ");
						     str.Append("<div class='widget-header'> ");
                             str.Append("<h5 class='widget-title smaller'>" + dt.Rows[i]["MenuName"].ToString() + "</h5> ");
                            str.Append("</div> ");

											  str.Append("<div class='widget-body'> ");
											  str.Append("	<div class='widget-main padding-6'> ");

                                           


                                                                DataTable dt2 = group.GetSelectMenuBid(model.GroupId, dt.Rows[i]["MENUID"].ToString());
                                                                
                                                                for (int j = 0; j < dt2.Rows.Count; j++)
                                                                {
                                                                    str.Append("<div class='checkbox' style='padding-left:10px;'>");
                                                                    str.Append("<span>" + dt2.Rows[j]["MenuName"].ToString() + "</span> <a href='admin_group.aspx?id=" + id + "&perid=" + dt2.Rows[j]["PerId"].ToString() + "'><img src='../img/del.png' style='border:0' /></a> ");
                                                                    str.Append("</div>");

                                                                }

                                                                dt2 = group.GetSelectMenu(model.GroupId, dt.Rows[i]["MENUID"].ToString());
                                                                for (int j = 0; j < dt2.Rows.Count; j++)
                                                                {
                                                                    str.Append("<div class='checkbox' style='padding-left:30px;'>");
                                                                    str.Append("<span><input type='checkbox' name='menu_select_id' value='" + dt2.Rows[j]["MenuId"].ToString() + "'>" + dt2.Rows[j]["MenuName"].ToString() + "</span> ");
                                                                    str.Append("</div>");
                                                                }

                                                               

                                                  
												  str.Append("</div> ");
											  str.Append("</div> ");
										  str.Append("</div> ");
									  str.Append("</div> ");
               
      
                      
                    }
                    this.qx.InnerHtml = str.ToString();
                }
            }
            else
            {
                DataTable dt = dal.GetList(" and bid=0 and statusid=1 ");
                StringBuilder str = new StringBuilder();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str.Append("<h4 class='widget-title'>" + dt.Rows[i]["MenuName"].ToString() + "</h4>");
                    DataTable dt2 = dal.GetList(" and statusid =1 and bid=" + dt.Rows[i]["MenuId"].ToString());

                    str.Append("<div style='width:500px; background:#fff;  '>");
                    for (int j = 0; j < dt2.Rows.Count; j++)
                    {
                        str.Append("<div class='list2'><input type='checkbox' name='menu_select_id' value='" + dt2.Rows[j]["MenuId"].ToString() + "'>" + dt2.Rows[j]["MenuName"].ToString() + "</div>");

                    }
                    str.Append("</div>");
                    str.Append("<p style='clear:both'></p>");
                }
                this.qx.InnerHtml = str.ToString();

            }



            if (Request["perid"]!=null)
            {
                int pid = Common.TypeHelper.ObjectToInt(Request["perid"], 0);
               
                int j = group.DelPer(pid);
                if (j>0)
                {
                    JScript.Loction("a", "Admin_group.aspx?action=update&id=" + id , this.Page);
                }
            }

            if (Request["action"] == "del") {
                if (group.Del(id) > 0)
                {
                    JScript.Loction("a", "admin_group.aspx?", this.Page);
                }
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        
        string  menu_list_id = Request["menu_select_id"];
        int id = Common.TypeHelper.ObjectToInt(this.hidId.Value, 0);
        if (id>0)
        { 
                Model.UserInfoGroupModel model = new Model.UserInfoGroupModel();
                model.GroupName = this.txtgroupName.Text;
                model.GroupId = id;
                if (string.IsNullOrEmpty(menu_list_id)==false)
                { 

                    string[] menu_arr_list = menu_list_id.Split(',');

                    Model.PermissionModel m = null;

                    foreach (var item in menu_arr_list)
                    {
                        int mid = Common.TypeHelper.ObjectToInt(item, 0);
                        int mbid = dal.GetModel(mid).Bid;
                        m = new Model.PermissionModel();
                        m.MenuId = mid;
                        m.MenuBid = mbid;

                        model.PerList.Add(m);
                    }
                }
                int j = group.Update(model);
                if (j > 0)
                {
                    JScript.alert("ok", "操作成功", "Admin_Group.aspx?id="+id+"&action=update", this.Page);
                }
                else
                {
                    JScript.alert("ok", "出现异常", this.Page);
                }

            
        }

        else
        {
            if (menu_list_id == "")
            {
                JScript.alertBack("ok", "请选择菜单", this.Page);
            }
            else
            {
                Model.UserInfoGroupModel model = new Model.UserInfoGroupModel();
                model.GroupName = this.txtgroupName.Text;
                string[] menu_arr_list = menu_list_id.Split(',');
                Model.PermissionModel m = null;
                foreach (var item in menu_arr_list)
                {
                    int mid = Common.TypeHelper.ObjectToInt(item, 0);
                    int mbid = dal.GetModel(mid).Bid;
                    m = new Model.PermissionModel();
                    m.MenuId = mid;
                    m.MenuBid = mbid;

                    model.PerList.Add(m);
                }

                int j = group.Add(model);
                if (j > 0)
                {
                    JScript.alert("ok", "操作成功", "Admin_Group.aspx?", this.Page);
                }
                else
                {
                    JScript.alert("ok", "出现异常", this.Page);
                }

            }
        
        }


       


    }


}