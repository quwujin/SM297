using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ClearCacheBase : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       string str= CacheBase.ClearCacheObjectInfo;
       //Response.Write(CacheBase.ClearObjectInfo);
     //  Response.Write(CacheBase.ClearCacheObjectInfo);

    }
}