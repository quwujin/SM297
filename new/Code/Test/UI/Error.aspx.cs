using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Error : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ty=Request["type"]; 
        if (!string.IsNullOrEmpty(ty)) {
            int count = ty.Length - ty.Replace("R", String.Empty).Length;
            int count1 = ty.Length - ty.Replace("E", String.Empty).Length;
            if (count == 3)
            {
                this.Err.Visible = true;
            }
            else if (count1 == 4)
            {
                this.End.Visible = true;
            }
            else {
                Response.End();
                return;
            }
        }
    }
}