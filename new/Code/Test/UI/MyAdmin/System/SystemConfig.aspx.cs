using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MyAdmin_System_SystemConfig : PageBase
{
    public int GroupId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        GroupId = userseesion.GroupId;


    }
}