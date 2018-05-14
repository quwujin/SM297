using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_LoginOut : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        Response.Cookies["iUserName"].Value = null;
        Response.Cookies["iUserName"].Domain = null;
        Response.Cookies["iUserName"].Expires = DateTime.Now.AddDays(-1);


        Response.Cookies["iUserId"].Value = null;
        Response.Cookies["iUserId"].Expires = DateTime.Now.AddDays(-1);



        Response.Cookies["iGroupName"].Value = null;
        Response.Cookies["iGroupName"].Expires = DateTime.Now.AddDays(-1);


        Response.Cookies["iGroupId"].Value = null;
        Response.Cookies["iGroupId"].Expires = DateTime.Now.AddDays(-1);


        Response.Write("<script>window.top.location.href='../Default/Login.aspx'</script>");
    }
}