using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MyAdmin_Zp_Info : PageBase
{

    public Db.InfoDal dal = new Db.InfoDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
     
       
            int id = Common.TypeHelper.ObjectToInt(Request["id"], 0);
            if (id > 0)
            {

                Model.InfoModel m = dal.GetModel(id);
                this.hidvid.Value = m.Id.ToString();
                this.txtTitle.Text = m.Title;
                this.txtNotes.Text = m.Notes;

            }

            this.infoList.DataSource = dal.GetList("");
            this.infoList.DataBind();
        }
  
    }

    protected void sub_Click(object sender, EventArgs e)
    {
        Model.InfoModel m = new Model.InfoModel();
        m.Id = Common.TypeHelper.ObjectToInt(this.hidvid.Value, 0);
        m.Title = this.txtTitle.Text;
        m.Notes = this.txtNotes.Text;
        if (m.Id>0)
        {
           int i= dal.Update(m);
            if (i>0)
            {
                Common.JScript.alert("a","操作成功","info.aspx?id="+m.Id, this.Page);
            }
        }
        else
        {
           int j= dal.Add(m);
            if (j > 0)
            {
                Common.JScript.alert("a", "操作成功", "info.aspx?id=" + j, this.Page);
            }
        } 
    }
}