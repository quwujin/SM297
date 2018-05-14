using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MyAdmin_Report_ReportOrder : PageBase
{
    Db.PassCodeInfoDal dal = new Db.PassCodeInfoDal();

    public string hots = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string sql = " select a.*,(select COUNT(1) from OrderInfo where DATEPART(hh, CreateTime) = a.hours) as ok from hoursinfo a";
            DataTable dt = Db.ConDal.GetList(sql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == dt.Rows.Count - 1)
                    {
                        hots += "['" + dt.Rows[i][1].ToString() + "点'," + dt.Rows[i][2] + "]";
                    }
                    else
                    {
                        hots += "['" + dt.Rows[i][1].ToString() + "点'," + dt.Rows[i][2] + "],";
                    }

                }
            }

        }
    }

 
     
 



    
   
    
}