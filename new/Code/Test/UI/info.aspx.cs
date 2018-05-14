using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Info : NBase
{
    public Db.InfoDal dal = new Db.InfoDal();

    public Model.InfoModel mm = new Model.InfoModel();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          mm = dal.GetModel(1);
        }
    }
}