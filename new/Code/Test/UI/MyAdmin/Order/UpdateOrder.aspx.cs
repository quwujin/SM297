using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MyAdmin_Order_UpdateOrder : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write("订单异常");
        Response.End();
        return;
         
        #region 验证用户登录信息

        Model.UserInfoModel UserSession = GetUserSession();

        if (UserSession == null || new Db.UserInfoDal().Exists(GetUserSession().UserName) == false)
        {
            Response.Write("账户登录已失效，请重新登录~！");
            Response.End();
            return;
        }
        #endregion
         
        Db.OrderInfoDal odal = new Db.OrderInfoDal();
        int id = Common.TypeHelper.ObjectToInt(Request["id"], 0);
        Model.OrderInfoModel model = odal.GetModel(id);
        string ty = Request["ty"]; 
        string msg = "";
        string hideMsg = "";

        #region 操作日志订单
        Db.Operation_LogDal logDal = new Db.Operation_LogDal();
        Model.Operation_LogModel mdlog = new Model.Operation_LogModel();

        mdlog.CreateTime = DateTime.Now;
        mdlog.Description = "";
        mdlog.LStatus = model.States;
        mdlog.Mobile = model.Mob;
        mdlog.OperationType = "";
        mdlog.OrderCode = model.OrderCode;
        mdlog.Status = 0;
        mdlog.UpdateTime = DateTime.Now;
        mdlog.UserName = UserSession.UserName;
        mdlog.Remark = "";
        mdlog.HideContent = "";
        #endregion

        #region 批量删除
        if (ty == "del")
        {
            string Delid = Request["Delid"];

            if (string.IsNullOrEmpty(Delid))
            {
                Response.Write("请勾选订单！");
                Response.End();
                return;
            }
            string[] idlist = Delid.Split(',');

            List<Model.OrderInfoModel> orderlist = new List<Model.OrderInfoModel>();

            foreach (string Did in idlist)
            {
                Model.OrderInfoModel ordel = new Model.OrderInfoModel();
                ordel.Id = Common.TypeHelper.ObjectToInt(Did, 0);
                if (ordel.Id > 0)
                {
                    orderlist.Add(ordel);
                }
            }
            int delcont = odal.Del(orderlist);

            if (delcont > 0)
            {
                #region 记录操作日志
                mdlog.Description = "Delid:" + Delid;
                mdlog.OperationType = "批量删除";

                logDal.Add(mdlog);
                #endregion

                Response.Write("删除成功！");
                Response.End();
                return;
            }
            else
            {
                Response.Write("删除失败！");
                Response.End();
                return;
            }

        }
        #endregion

        #region 批量作废
        if (ty == "zflist")
        {
            string Delid = Request["Delid"];

            if (string.IsNullOrEmpty(Delid))
            {
                Response.Write("请勾选订单！");
                Response.End();
                return;
            }
            string[] idlist = Delid.Split(',');

            List<Model.OrderInfoModel> orderlist = new List<Model.OrderInfoModel>();

            foreach (string Did in idlist)
            {
                Model.OrderInfoModel ordel = new Model.OrderInfoModel();
                ordel.Id = Common.TypeHelper.ObjectToInt(Did, 0);
                if (ordel.Id > 0)
                {
                    orderlist.Add(ordel);
                }
            }
            int delcont = odal.Update(orderlist);
            if (delcont > 0)
            {
                #region 记录操作日志
                mdlog.Description = "Delid:" + Delid;
                mdlog.OperationType = "批量作废";

                logDal.Add(mdlog);
                #endregion

                Response.Write("作废成功！");
                Response.End();
                return;
            }
            else
            {
                Response.Write("作废失败！");
                Response.End();
                return;
            }

        }
        #endregion

        #region 批量OCR预审勾选小票
        if (ty == "recorded")
        {
            string Delid = Request["Delid"];

            if (string.IsNullOrEmpty(Delid))
            {
                Response.Write("请勾选订单！");
                Response.End();
                return;
            }

            BDImgApi.APIKey.appUserName = WebFramework.GeneralMethodBase.GetKeyConfig(25).Val;// 
            BDImgApi.APIKey.appKey = WebFramework.GeneralMethodBase.GetKeyConfig(26).Val; ;// 
            BDImgApi.APIKey.appSecret = WebFramework.GeneralMethodBase.GetKeyConfig(27).Val; ;// 

            //Task<string> task1 = Task.Factory.StartNew<string>(() =>
            //{
                string ResultStr = "";
                try
                { 
                    string[] idlist = Delid.Split(',');

                    List<Model.OrderInfoModel> orderlist = new List<Model.OrderInfoModel>();

                    foreach (string Did in idlist)
                    {
                        int OId= Common.TypeHelper.ObjectToInt(Did, 0);
                        if (OId > 0)
                        {
                            Model.OrderInfoModel ordel = odal.GetModel(OId);
                            if (ordel.Id > 0)
                            {
                                BDImgApi.GetApiResult Result = new BDImgApi.GetApiResult();
                                Model.FileInfoModel FileModel = new Db.FileInfoDal().GetModel(ordel.FilesId);

                                if (FileModel.Id > 0)
                                    ResultStr += Result.GetMatchingImg(FileModel.Hashdata, "1", FileModel.FileName, ordel.OrderCode, false).errMsg + ",";
                            }
                        }
                        //Thread.Sleep(20000);  
                    }
                    //return ResultStr; 
                }
                catch (Exception ex)
                {
                    //return ResultStr; 
                }

            //});
             
             
            #region 记录操作日志
            mdlog.Description = "Idlist:" + Delid;
            mdlog.OperationType = "批量OCR预审勾选小票";

            logDal.Add(mdlog);
            #endregion

            Response.Write("成功");
            Response.End();
            return;

        }
        #endregion

        #region 批量OCR预审未录入小票
        if (ty == "recordedAll")
        { 

            BDImgApi.APIKey.appUserName = WebFramework.GeneralMethodBase.GetKeyConfig(25).Val;// 
            BDImgApi.APIKey.appKey = WebFramework.GeneralMethodBase.GetKeyConfig(26).Val; ;// 
            BDImgApi.APIKey.appSecret = WebFramework.GeneralMethodBase.GetKeyConfig(27).Val; ;// 
             
            string ResultStr = "";
            try
            {
                List<Model.OrderInfoModel>  Notorderlist = odal.GetModelNotOCRList();

                foreach (Model.OrderInfoModel NotModel in Notorderlist)
                { 
                    if (NotModel.Id > 0)
                    { 
                            BDImgApi.GetApiResult Result = new BDImgApi.GetApiResult();
                            Model.FileInfoModel FileModel = new Db.FileInfoDal().GetModel(NotModel.FilesId);

                            if (FileModel.Id > 0)
                                ResultStr += Result.GetMatchingImg(FileModel.Hashdata, "1", FileModel.FileName, NotModel.OrderCode, false).errMsg + ",";
                    }
                } 
            }
            catch (Exception ex)
            { 

            } 

            #region 记录操作日志
            mdlog.Description = "" + ResultStr;
            mdlog.OperationType = "批量OCR预审未录入小票";

            logDal.Add(mdlog);
            #endregion

            Response.Write("成功");
            Response.End();
            return;

        }
        #endregion
         
        #region 恢复订单
        if (model.Id > 0 && ty == "back" && model.States == -1)
        {
            if (UserSession.GroupId != 2)
            {
                Response.Write("请联系管理员执行此操作");
                Response.End();
                return;
            }

            model.States = 0; 

            mdlog.Status = model.States;
            mdlog.OperationType = "恢复订单状态";
            model.UpdateTime = DateTime.Now;

            int i = odal.EditOrder(model, mdlog);
            if (i > 0)
            {
                Response.Write("修改成功");
                Response.End();
                return;
            }
            Response.Write("恢复失败");
            Response.End();
            return;
        
        } 
        #endregion

        #region 审核订单
        if (model.Id > 0 && model.States == 0)
        { 
            #region 作废
            if (ty == "zf")
            {
                model.States = -1;
                msg = WebFramework.GeneralMethodBase.GetMsg(1);

                mdlog.OperationType = "作废订单";
            }
            #endregion

            #region 无法审核
            else if (ty == "no")
            {
                model.Note = "无法审核";
                mdlog.OperationType = "无法审核订单";

                //if (model.Note == "无法审核") {
                //    model.Note = "";
                //    mdlog.OperationType = "修改无法审核订单状态";
                //}

            }
            #endregion

            #region 添加备注
            else if (ty == "note")
            {
                string nums = Request["nums"]; //流水号
                string store = Request["store"];//门店名称
                string price = Request["price"];//金额
                string times = Request["times"];//日期

                #region 验证输入信息
                if (string.IsNullOrEmpty(nums) == false)
                {
                    if (Common.ValidateHelper.IsCode(nums) == false)
                    {
                        Response.Write("请输入正确的流水号！");
                        Response.End();
                        return;
                    }
                }
                if (string.IsNullOrEmpty(store) == false)
                {
                    if (Common.ValidateHelper.IsAddrs(store) == false)
                    {
                        Response.Write("请输入正确的门店信息！");
                        Response.End();
                        return;
                    }
                }
                if (string.IsNullOrEmpty(price) == false)
                {
                    if (Common.ValidateHelper.IsNumeric(price) == false)
                    {
                        Response.Write("请输入正确的金额！");
                        Response.End();
                        return;
                    }
                }
                if (string.IsNullOrEmpty(times) == false)
                {
                    if (Common.ValidateHelper.IsDate(times) == false)
                    {
                        Response.Write("请输入正确的时间！");
                        Response.End();
                        return;
                    }
                }
                #endregion

                model.Title = store;
                model.Texts = nums;
                model.Tdate = times;
                model.Age = price;

                mdlog.Description = model.Title + "-" + model.Texts + "-" + model.Tdate + "-" + model.Age;
                mdlog.OperationType = "添加备注";

                if (Convert.ToInt32(new BDImgApi.GetApiResult().CheckProductByWhere(string.Format(" and SerialNumber='{0}' and Note not in ('{1}')", nums, model.OrderCode))) > 0)
                {
                    Response.Write("该流水号小票云审核已有订单录入");
                    Response.End();
                    return;
                }

                if (odal.CheckCount(string.Format(" and Texts='{0}' and Id not in ({1})", nums, model.Id)) > 0)
                {
                    Response.Write("该流水号已存在");
                    Response.End();
                    return;
                }

                int upcont = odal.UpdateAdds(model, mdlog);

                if (upcont > 0)
                {
                    Response.Write("添加备注成功");
                    Response.End();
                    return;
                }
            }
            #endregion

            #region 审核通过
            else if (ty == "ok")
            {
                if (model.Jx == "三等奖")
                {
                    #region 发送红包
                    Common.RedPackHelper rp = new Common.RedPackHelper();

                    int moeny = model.RedPackMoney;

                    if (WebFramework.GeneralMethodBase.GetKeyConfig(3).States == 1)
                    {
                        moeny = 100;
                    }

                    if (WebFramework.GeneralMethodBase.GetKeyConfig(6).Val.ToLower() == "false") //红包开关
                    {
                        Response.Write("微信红包发放未开启，请联系管理员");
                        Response.End();
                        return;
                    }

                    int acid = Common.TypeHelper.ObjectToInt(WebFramework.GeneralMethodBase.GetKeyConfig(29).Val, 0);
                    int hid = acid;
                    string ckey = WebFramework.GeneralMethodBase.GetKeyConfig(30).Val;
                    string hkey = WebFramework.GeneralMethodBase.GetKeyConfig(31).Val;

                    Common.RedPackHelper.result result = rp.send(acid, hid, model.OpenId, model.HbOrderCode, moeny, ckey, hkey);

                    if (result.SendStatus == false)
                    {
                        Response.Write(result.MSG);
                        Response.End();
                        return;
                    }

                    #endregion
                }
                else
                {
                    Response.Write("奖项无需审核发奖");
                    Response.End();
                    return;
                }

                model.States = 1;
                mdlog.OperationType = "审核通过订单";
            }
            #endregion

            else
            {
                Response.Write("订单异常");
                Response.End();
                return;
            }

            mdlog.Status = model.States;
            mdlog.Description = hideMsg;//隐藏串码 短信
            mdlog.HideContent = msg;

            model.Account = UserSession.UserName;
            model.UpdateTime = DateTime.Now;

            int i = odal.EditOrder(model, mdlog);
            if (i > 0)
            {
                #region 发送短信 
                if (msg.Length > 10 && model.States == 1 && ty == "ok" && model.PrizeCode.Length > 5)
                {
                   // Common.MessageApi.SendZtMessage(msg, model.Mob, Common.TypeHelper.ObjectToInt(WebFramework.GeneralMethodBase.GetKeyConfig(20).Val, 0), WebFramework.GeneralMethodBase.GetKeyConfig(21).Val);
                }
                #endregion

                Response.Write("修改成功");
            }
            else
            {
                Response.Write("修改失败");
            }
            Response.End();
            return;

        }
        #endregion

        Response.Write("订单已审核");
        Response.End();
        return;
    } 

    #region 获取用户登录信息
    public Model.UserInfoModel GetUserSession()
    {
        Model.UserInfoModel OrderUser = new Model.UserInfoModel();
        if (Session["UserSysSession"] == null)
        {
            return null;
        }
        OrderUser = (Model.UserInfoModel)Session["UserSysSession"];
        if (System.Web.HttpUtility.UrlDecode(Common.Des.Decode(OrderUser.RealName)).Length <= 0)
        {
            return null;
        }
        return OrderUser;
    }
    #endregion
     
}