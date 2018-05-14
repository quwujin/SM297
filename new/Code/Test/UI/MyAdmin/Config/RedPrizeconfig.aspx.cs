using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MyAdmin_Config_RedPrizeconfig : PageBase
{
    Db.ZpConfigDal dal = new Db.ZpConfigDal();
    public int id = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        id = 2;

        if (!IsPostBack)
        {
            if (id > 0)
            {
               

                Model.ZpConfigModel model = dal.GetModel(id);
                this.txtzjl1.Text = model.Zjl1.ToString();
                this.txtzjl2.Text = model.Zjl2.ToString();
                this.txtzjl3.Text = model.Zjl3.ToString();
                this.txtzjl4.Text = model.Zjl4.ToString();
                this.txtzjl5.Text = model.Zjl5.ToString();
                this.txtzjl6.Text = model.Zjl6.ToString();
                this.txtzjl7.Text = model.Zjl7.ToString();
                this.txtzjl8.Text = model.Zjl8.ToString();
                this.txtzjl9.Text = model.Zjl9.ToString();
                this.txtzjl10.Text = model.Zjl10.ToString();
                this.txtzjl11.Text = model.Zjl11.ToString();
                this.txtzjl12.Text = model.Zjl12.ToString();
                this.txtzjl13.Text = model.Zjl13.ToString();
                this.txtzjl14.Text = model.Zjl14.ToString();
                this.txtzjl15.Text = model.Zjl15.ToString();
                this.txtzjl16.Text = model.Zjl16.ToString();
                this.txtzjl17.Text = model.Zjl17.ToString();
                this.txtzjl18.Text = model.Zjl18.ToString();
                this.txtzjl19.Text = (100 - model.Zjl1 - model.Zjl2 - model.Zjl3 - model.Zjl4 - model.Zjl5 - model.Zjl6).ToString();
                 
                 
                Db.OrderInfoDal ordal = new Db.OrderInfoDal();

                //int one_daycont = ordal.CheckCount(" and Jx='一等奖'");//总
                //int one_daycont2 = ordal.CheckCount(" and Jx='一等奖' and datediff(day,[createtime],getdate())=1");//昨
                //int one_daycont3 = ordal.CheckCount(" and Jx='一等奖' and datediff(day,[createtime],getdate())=0");//今
                //this.tday1.InnerText = "昨：" + one_daycont2 + "  今：" + one_daycont3 + "  总：" + one_daycont;

                //int two_daycont = ordal.CheckCount(" and Jx='二等奖'");//总
                //int two_daycont2 = ordal.CheckCount(" and Jx='二等奖' and datediff(day,[createtime],getdate())=1");//昨
                //int two_daycont3 = ordal.CheckCount(" and Jx='二等奖' and datediff(day,[createtime],getdate())=0");//今
                //this.tday2.InnerText = "昨：" + two_daycont2 + "  今：" + two_daycont3 + "  总：" + two_daycont;

                //int three_daycont = ordal.CheckCount(" and Jx='三等奖'");//总
                //int three_daycont2 = ordal.CheckCount(" and Jx='三等奖' and datediff(day,[createtime],getdate())=1");//昨
                //int three_daycont3 = ordal.CheckCount(" and Jx='三等奖' and datediff(day,[createtime],getdate())=0");//今
                //this.tday3.InnerText = "昨：" + three_daycont2 + "  今：" + three_daycont3 + "  总：" + three_daycont;

                //int four_daycont = ordal.CheckCount(" and Jx='四等奖'");//总
                //int four_daycont2 = ordal.CheckCount(" and Jx='四等奖' and datediff(day,[createtime],getdate())=1");//昨
                //int four_daycont3 = ordal.CheckCount(" and Jx='四等奖' and datediff(day,[createtime],getdate())=0");//今
                //this.tday4.InnerText = "昨：" + four_daycont2 + "  今：" + four_daycont3 + "  总：" + four_daycont;

                //int five_daycont = ordal.CheckCount(" and Jx='参与奖'");//总
                //int five_daycont2 = ordal.CheckCount(" and Jx='参与奖' and datediff(day,[createtime],getdate())=1");//昨
                //int five_daycont3 = ordal.CheckCount(" and Jx='参与奖' and datediff(day,[createtime],getdate())=0");//今
                //this.tday5.InnerText = "昨：" + five_daycont2 + "  今：" + five_daycont3 + "  总：" + five_daycont;

            }
            
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Model.ZpConfigModel model = new Model.ZpConfigModel();
        model.Zjl1 =Common.TypeHelper.ObjectToInt(this.txtzjl1.Text,0);
        model.Zjl2 = Common.TypeHelper.ObjectToInt(this.txtzjl2.Text, 0);
        model.Zjl3 = Common.TypeHelper.ObjectToInt(this.txtzjl3.Text, 0);
        model.Zjl4 = Common.TypeHelper.ObjectToInt(this.txtzjl4.Text, 0);
        model.Zjl5 = Common.TypeHelper.ObjectToInt(this.txtzjl5.Text, 0);
        model.Zjl6 = Common.TypeHelper.ObjectToInt(this.txtzjl6.Text, 0);
        model.Zjl7 = Common.TypeHelper.ObjectToInt(this.txtzjl7.Text, 0);
        model.Zjl8 = Common.TypeHelper.ObjectToInt(this.txtzjl8.Text, 0);
        model.Zjl9 = Common.TypeHelper.ObjectToInt(this.txtzjl9.Text, 0);
        model.Zjl10 = Common.TypeHelper.ObjectToInt(this.txtzjl10.Text, 0);
        model.Zjl11 = Common.TypeHelper.ObjectToInt(this.txtzjl11.Text, 0);
        model.Zjl12 = Common.TypeHelper.ObjectToInt(this.txtzjl12.Text, 0);
        model.Zjl13 = Common.TypeHelper.ObjectToInt(this.txtzjl13.Text, 0);
        model.Zjl14 = Common.TypeHelper.ObjectToInt(this.txtzjl14.Text, 0);
        model.Zjl15 = Common.TypeHelper.ObjectToInt(this.txtzjl15.Text, 0);
        model.Zjl16 = Common.TypeHelper.ObjectToInt(this.txtzjl16.Text, 0);
        model.Zjl17 = Common.TypeHelper.ObjectToInt(this.txtzjl17.Text, 0);
        model.Zjl18 = Common.TypeHelper.ObjectToInt(this.txtzjl18.Text, 0);
        model.Id = 2;

        if ((model.Zjl1 + model.Zjl2 + model.Zjl3 + model.Zjl4 + model.Zjl5 + model.Zjl6) > 100)
        {
            JScript.alert("a", "中奖率设置错误", "RedPrizeconfig.aspx", this.Page);
            return;
        }

        if (dal.Update(model) > 0)
        {
            #region 记录操作日志
            new Db.Operation_LogDal().Add(new Model.Operation_LogModel()
            {
                CreateTime = DateTime.Now,
                Description = "Id" + model.Id + "，一等奖：" + model.Zjl1 + "，二等奖：" + model.Zjl2 + "，三等奖：" + model.Zjl3 + "，四等奖：" + model.Zjl4 + "，五等奖：" + model.Zjl5 + "，六等奖：" + model.Zjl6,
                LStatus = 0,
                Mobile = "",
                OperationType = "修改中奖率",
                OrderCode = "",
                Status = 0,
                UpdateTime = DateTime.Now,
                UserName = userseesion.UserName,
                Remark="",
                HideContent=""
            });
            #endregion

            JScript.alert("a", "操作成功", "RedPrizeconfig.aspx" , this.Page);
        }
        else {
            JScript.alert("a", "修改失败", "RedPrizeconfig.aspx" , this.Page);
        } 
    }
}