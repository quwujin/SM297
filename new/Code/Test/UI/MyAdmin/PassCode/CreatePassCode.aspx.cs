using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MyAdmin_PassCode_CreatePassCode : PageBase
{
    Db.PassCodeInfoDal dal = new Db.PassCodeInfoDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        int qty = Common.TypeHelper.ObjectToInt(this.txtQty.Text, 1000);

        for (int i = 1; i <= qty; i++)
        {

            if (this.DropDownList1.SelectedValue=="19")
            {
                byte[] buffer = Guid.NewGuid().ToByteArray();
                long l = BitConverter.ToInt64(buffer, 0);

                Model.PassCodeInfoModel m = new Model.PassCodeInfoModel();
                m.Notes = this.txtNotes.Text;
                m.Codes = l.ToString();
                m.CreateTime = DateTime.Now;
                m.CustomerId =0 ;
                m.EventId =0;
                m.ActiveIp = this.ddlCity.SelectedValue;
                m.StatusId = 0;
                m.OpenId = "";
                dal.Add(m);
            }
            else if (this.DropDownList1.SelectedValue == "4+2")
            {

                //  byte[] buffer = Guid.NewGuid().ToByteArray();
                //long l = BitConverter.ToInt64(buffer, 0);

                string code = "";
                int one = Common.TypeHelper.ObjectToInt(GetCodeWxz(1), 0);
                int two = Common.TypeHelper.ObjectToInt(GetCodeWxz(1), 0);
                while (one==two)
                {
                    two = Common.TypeHelper.ObjectToInt(GetCodeWxz(1), 0);
                }
                for (int ii = 0; ii < 6; ii++)
                {
                    if (ii == one || ii == two)
                    {
                        code += GetCode(1);
                    }
                    else
                    {
                        code += GetCode2(1);
                    }
                }

                while (dal.CheckCount(" and Codes='" + code + "'") > 0)
                {
                    code = "";
                    one = Common.TypeHelper.ObjectToInt(GetCodeWxz(1), 0);
                    two = Common.TypeHelper.ObjectToInt(GetCodeWxz(1), 0);
                    while (one == two)
                    {
                        two = Common.TypeHelper.ObjectToInt(GetCodeWxz(1), 0);
                    }
                    for (int ii = 0; ii < 6; ii++)
                    {
                        if (ii == one || ii == two)
                        {
                            code += GetCode(1);
                        }
                        else
                        {
                            code += GetCode2(1);
                        }
                    }
                }
                Model.PassCodeInfoModel m = new Model.PassCodeInfoModel();
                m.Notes = this.txtNotes.Text;
                m.Codes = code;
                m.CreateTime = DateTime.Now;
                m.CustomerId = 0;
                m.EventId =0;
                m.ActiveIp = this.ddlCity.SelectedValue;
                m.StatusId = 0;
                m.OpenId = "";
                dal.Add(m);
                //Response.Write(code);

            }
            else if (this.DropDownList1.SelectedValue == "6")
            {

                //  byte[] buffer = Guid.NewGuid().ToByteArray();
                //long l = BitConverter.ToInt64(buffer, 0);

                string code = GetCode3(6);
                while (dal.CheckCount(" and Codes='" + code + "'") > 0)
                {
                    code = GetCode3(6);
                }
                Model.PassCodeInfoModel m = new Model.PassCodeInfoModel();
                m.Notes = this.txtNotes.Text;
                m.Codes = code;
                m.CreateTime = DateTime.Now;
                m.CustomerId = 0;
                m.EventId = 0;
                m.ActiveIp = this.ddlCity.SelectedValue;
                m.StatusId = 0;
                m.OpenId = "";
                dal.Add(m);

            }
            else if (this.DropDownList1.SelectedValue == "16(4+4+4+4)")
            {
                string code = GetCoden(4) + GetCode2n(4) + GetCoden(4) + GetCode2n(4);
                while (dal.CheckCount(" and Codes='" + code + "'") > 0)
                {
                    code = GetCoden(4) + GetCode2n(4) + GetCoden(4) + GetCode2n(4);
                }
                Model.PassCodeInfoModel m = new Model.PassCodeInfoModel();
                m.Notes = this.txtNotes.Text;
                m.Codes = code;
                m.CreateTime = DateTime.Now;
                m.CustomerId =0;
                m.EventId =0;
                m.ActiveIp = this.ddlCity.SelectedValue;
                m.StatusId = 0;
                m.OpenId = "";
                dal.Add(m);
            }
            else if (this.DropDownList1.SelectedValue == "8英文")
            {

                //  byte[] buffer = Guid.NewGuid().ToByteArray();
                //long l = BitConverter.ToInt64(buffer, 0);

                string code = GetCoden(8);
                while (dal.CheckCount(" and Codes='" + code + "'") > 0)
                {
                    code = GetCoden(8);
                }
                Model.PassCodeInfoModel m = new Model.PassCodeInfoModel();
                m.Notes = this.txtNotes.Text;
                m.Codes = code;
                m.CreateTime = DateTime.Now;
                m.CustomerId = 0;
                m.EventId =0;
                m.ActiveIp = this.ddlCity.SelectedValue;
                m.StatusId = 0;
                m.OpenId = "";
                dal.Add(m);

            }
            else if (this.DropDownList1.SelectedValue == "9英文")
            {

                //  byte[] buffer = Guid.NewGuid().ToByteArray();
                //long l = BitConverter.ToInt64(buffer, 0);

                string code = GetCoden(9);
                while (dal.CheckCount(" and Codes='" + code + "'") > 0)
                {
                    code = GetCoden(9);
                }
                Model.PassCodeInfoModel m = new Model.PassCodeInfoModel();
                m.Notes = this.txtNotes.Text;
                m.Codes = code;
                m.CreateTime = DateTime.Now;
                m.CustomerId = 0;
                m.EventId = 0;
                m.ActiveIp = this.ddlCity.SelectedValue;
                m.StatusId = 0;
                m.OpenId = "";
                dal.Add(m);

            }
            else if (this.DropDownList1.SelectedValue == "8+4")
            {

                //  byte[] buffer = Guid.NewGuid().ToByteArray();
                //long l = BitConverter.ToInt64(buffer, 0);

                string code = GetCoden(8) + GetCode2n(4);
                while (dal.CheckCount(" and Codes='" + code + "'") > 0)
                {
                    code = GetCoden(4) + GetCode2n(4);
                }
                Model.PassCodeInfoModel m = new Model.PassCodeInfoModel();
                m.Notes = this.txtNotes.Text;
                m.Codes = code;
                m.CreateTime = DateTime.Now;
                m.CustomerId = 0;
                m.EventId = 0;
                m.ActiveIp = this.ddlCity.SelectedValue;
                m.StatusId = 0;
                m.OpenId = "";
                dal.Add(m);

            }
            else
            {


                //  byte[] buffer = Guid.NewGuid().ToByteArray();
                //long l = BitConverter.ToInt64(buffer, 0);

                string code = GetCode(4) + GetCode2(6);
                while (dal.CheckCount(" and Codes='" + code + "'") > 0)
                {
                    code = GetCode(4) + GetCode2(6);
                }
                Model.PassCodeInfoModel m = new Model.PassCodeInfoModel();
                m.Notes = this.txtNotes.Text;
                m.Codes = code;
                m.CreateTime = DateTime.Now;
                m.CustomerId = 0;
                m.EventId = 0;
                m.ActiveIp = this.ddlCity.SelectedValue;
                m.StatusId = 0;
                m.OpenId = "";
                dal.Add(m);

            }
            
            
            


        }

        Common.JScript.alert("a", "操作成功", "AdminPassCode.aspx", this.Page);

    }

    static string GetCoden(int num)
    {
        string a = "ABCDEFGHJKLMNPQRSTUVWXYZ";
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < num; i++)
        {
            sb.Append(a[new Random(Guid.NewGuid().GetHashCode()).Next(0, a.Length)]);
        }

        return sb.ToString();
    }

    static string GetCode2n(int num)
    {
        string a = "23456789";
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < num; i++)
        {
            sb.Append(a[new Random(Guid.NewGuid().GetHashCode()).Next(0, a.Length)]);
        }

        return sb.ToString();
    }
    
    static string GetCode(int num)
    {
        string a = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < num; i++)
        {
            sb.Append(a[new Random(Guid.NewGuid().GetHashCode()).Next(0, a.Length)]);
        }

        return sb.ToString();
    }

    static string GetCode2(int num)
    {
        string a = "0123456789";
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < num; i++)
        {
            sb.Append(a[new Random(Guid.NewGuid().GetHashCode()).Next(0, a.Length)]);
        }

        return sb.ToString();
    }


    static string GetCodeWxz(int num)
    {
        string a = "012345";
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < num; i++)
        {
            sb.Append(a[new Random(Guid.NewGuid().GetHashCode()).Next(0, a.Length)]);
        }

        return sb.ToString();
    }


    static string GetCode3(int num)
    {
        string a = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < num; i++)
        {
            sb.Append(a[new Random(Guid.NewGuid().GetHashCode()).Next(0, a.Length)]);
        }

        return sb.ToString();
    }


    protected void ddlcustomerid_SelectedIndexChanged(object sender, EventArgs e)
    {
       
       
    }
}