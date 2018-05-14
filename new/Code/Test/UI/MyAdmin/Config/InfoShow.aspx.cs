using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MyAdmin_Config_InfoShow : System.Web.UI.Page
{
    public Db.InfoDal dal = new Db.InfoDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        int id = Common.TypeHelper.ObjectToInt(Request["id"], 0);
        if (id > 0)
        {

            Model.InfoModel m = dal.GetModel(id);
           
            this.txtTitle.Text = m.Title;
            this.txtNotes.Text = m.Notes;

        }

    }
}