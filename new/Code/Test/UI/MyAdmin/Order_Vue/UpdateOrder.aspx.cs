using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MyAdmin_Order_UpdateOrder : System.Web.UI.Page
{
    Db.OrderInfoDal OrderDal = new Db.OrderInfoDal();
    Db.Operation_LogDal logDal = new Db.Operation_LogDal();
    Db.MsgConfigDal MsgConfigDal = new Db.MsgConfigDal();
    public Model.UserInfoModel UserSession = null;
    Model.OrderInfoModel model = new Model.OrderInfoModel();
    Model.Operation_LogModel mdlog = new Model.Operation_LogModel();
    Model.ReturnValue returnValue = new Model.ReturnValue();

    private static readonly object SysLock = new object();
    private bool IsBack = Common.TypeHelper.ToBool(WebFramework.GeneralMethodBase.GetKeyConfig(48).Val);

    protected void Page_Load(object sender, EventArgs e)
    { 
        returnValue.ErrMessage = "";
        returnValue.Success = true;

        #region 验证用户登录信息

        UserSession = WebFramework.GeneralMethodBase.GetUserSession();

        if (UserSession == null || new Db.UserInfoDal().Exists(WebFramework.GeneralMethodBase.GetUserSession().UserName) == false)
        {
            returnValue.Success = false;
            returnValue.ErrMessage = "登录信息失效！";

            Response.Write(Common.JsonHelper.GetJsonString(returnValue));
            Response.End();
            return;
        }
        #endregion

        lock (SysLock)
        {
            string GetType = Request["GetType"];

            if (string.IsNullOrEmpty(GetType) == false)
            {
                returnValue = ResultData(GetType);
            }
        }

        Response.Write(Common.JsonHelper.GetJsonString(returnValue));
        Response.End();
        return; 

    } 
     
    public Model.ReturnValue ResultData(string GetType) {

          
        #region 操作日志订单

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

        if (GetType == "SaveRemark")
        {
            //设置备注
            returnValue = SaveRemark();
        }
        if (GetType == "Success")
        {
            //审核通过
            returnValue = Success();
        }
        if (GetType == "Fail")
        {
            //审核作废
            returnValue = Fail();
        }
        if (GetType == "Unable")
        {
            //无法审核
            returnValue = Unable();
        }
        if (GetType == "Recovery")
        {
            //恢复订单
            returnValue = Recovery();
        }
        if (GetType == "BatchDelete")
        {
            //批量删除
            returnValue = BatchDelete();
        }
        if (GetType == "BatchFail")
        {
            //批量作废
            returnValue = BatchFail();
        }

        if (GetType == "BatchRecorded")
        {
            //批量OCR预审勾选小票
            returnValue = BatchRecorded();
        }
        if (GetType == "BatchRecordedAll")
        {
            //批量OCR预审未录入小票
            returnValue = BatchRecordedAll();
        }
        if (GetType == "DelayedReissue")
        {
            //延时订单发送失败补发
            returnValue = DelayedReissue();
        }
        if (GetType == "ReissueHb")
        {
            //红包退款补发
            returnValue = ReissueHb();
        }
         
        if(returnValue.Success)
            returnValue = InitData(returnValue);
        
        return returnValue;
    }

    public Model.ReturnValue InitData(Model.ReturnValue returnValue) {
         
        int PageIndex = Common.TypeHelper.ObjectToInt(Request["PageIndex"], 0);
        int PageSize = Common.TypeHelper.ObjectToInt(Request["PageSize"], 10);

        string CheckInputText = Request["CheckInputText"];
        string SearchOptionText = Request["SearchOptionText"];
        //string StatusOptionText = Request["StatusOptionText"];
        string PrizeOptionText = Request["PrizeOptionText"];
        string OcrOptionText = Request["OcrOptionText"];
        string StarTimeText = Request["StarTimeText"];
        string EndTimeText = Request["EndTimeText"];
        string OrderStatusType = Request["OrderStatusType"];

        string sqlwhere = "";

        if (string.IsNullOrEmpty(CheckInputText) == false && string.IsNullOrEmpty(SearchOptionText) == false)
        {
            sqlwhere += " and " + SearchOptionText + " like '%" + Common.ReplaceErorrSql.StripSQLInjection(CheckInputText) + "%'";
        }
        if (string.IsNullOrEmpty(OrderStatusType) == false && OrderStatusType != "1")
        {
            sqlwhere += " and a.States =" + (OrderStatusType == "2" ? "0" : OrderStatusType == "3" ? "1" : "-1") + "";
        }
        if (string.IsNullOrEmpty(PrizeOptionText) == false) {
            sqlwhere += " and a.Jx='" + PrizeOptionText + "'";
        }
        if (string.IsNullOrEmpty(OcrOptionText) == false && Request["GetType"] != "Export")
        {
            if (OcrOptionText == "是")
            {
                sqlwhere += " and ap.note is not null ";
            }
            else
            {
                sqlwhere += " and ap.note is null ";
            } 
        }
        if (string.IsNullOrEmpty(StarTimeText) == false)
        {
            sqlwhere += " and a.CreateTime >= '" + StarTimeText + " 00:00:00'";
        }
        if (string.IsNullOrEmpty(EndTimeText) == false)
        {
            sqlwhere += " and a.CreateTime <= '" + EndTimeText + " 23:59:59'";
        }

        if ( Request["GetType"] == "Export")
        {
            string ExportData = string.IsNullOrEmpty(Request["ExportData"]) ? "" : Request["ExportData"].TrimEnd(',');

            Common.NPOIHelper.ExportByWeb(OrderDal.GetVueExcelList(sqlwhere, ExportData, "", "OrderInfo",true), "", DateTime.Now.ToString("yyyyMMdd") + ".xlsx");
            return null;
        }
        else
        {
            DataTable OrderDt = OrderDal.GetList(sqlwhere, PageIndex, PageSize, true);

            #region 查询订单红包是否过期
            if (string.IsNullOrEmpty(Request["IsRefund"]) == false && Request["IsRefund"] == "是")
            {
                OrderDt.Columns.Add("isRefund", typeof(int));//消费者是否退款
                for (int row = 0; row < OrderDt.Rows.Count; row++)
                {
                    string Jx = OrderDt.Rows[row]["Jx"].ToString();
                    string HbOrderCode = OrderDt.Rows[row]["HbOrderCode"].ToString();
                    string SearchStatus= Common.SearchRedPack.SearchOrder(HbOrderCode);
                    OrderDt.Rows[row]["isRefund"] = (HbOrderCode.Length > 0 && (SearchStatus == "已退款" || SearchStatus == "发放失败")) ? 1 : 0;
                }
            }
            #endregion

            returnValue.EffectRows = OrderDal.GetCount(sqlwhere, "", true);//true 左连接小票表   非小票项目可不传
            returnValue.ObjectValue = Common.JsonHelper.DataTableToJson(OrderDt);
        }

        return returnValue;

    }

    public Model.ReturnValue SaveRemark()
    {
        int id = Common.TypeHelper.ObjectToInt(Request["OrderId"], 0);

        model = OrderDal.GetModel(id);

        if (model.Id <= 0)
        {
            returnValue.ErrMessage = "订单不存在";
            returnValue.Success = false;
            return returnValue;
        }
         
        string nums = Request["Nums"]; //流水号
        string store = Request["Store"];//门店名称
        string price = Request["Price"];//金额
        string times = Request["Times"];//日期

        #region 验证输入信息
        if (string.IsNullOrEmpty(nums) == false)
        {
            if (Common.ValidateHelper.IsCode(nums) == false)
            {
                returnValue.Success = false;
                returnValue.ErrMessage = "请输入正确的流水号！";
                return returnValue;
            }
        }
        if (string.IsNullOrEmpty(store) == false)
        {
            if (Common.ValidateHelper.IsAddrs(store) == false)
            {
                returnValue.Success = false;
                returnValue.ErrMessage = "请输入正确的门店信息！";
                return returnValue; 
            }
        }
        if (string.IsNullOrEmpty(price) == false)
        {
            if (Common.ValidateHelper.IsNumeric(price) == false)
            {
                returnValue.Success = false;
                returnValue.ErrMessage = "请输入正确的金额！";
                return returnValue;  
            }
        }
        if (string.IsNullOrEmpty(times) == false)
        {
            if (Common.ValidateHelper.IsDate(times) == false)
            {
                returnValue.Success = false;
                returnValue.ErrMessage = "请输入正确的时间！";
                return returnValue;   
            }
        }
        #endregion

        model.Title = store;
        model.Texts = nums;
        model.Tdate = times;
        model.Age = price;

        mdlog.Description = model.Title + "-" + model.Texts + "-" + model.Tdate + "-" + model.Age;
        mdlog.OperationType = "添加备注";
        mdlog.OrderCode = model.OrderCode;
        mdlog.Mobile = model.Mob; 

        if (Convert.ToInt32(new BDImgApi.GetApiResult().CheckProductByWhere(string.Format(" and SerialNumber='{0}' and Note not in ('{1}')", nums, model.OrderCode))) > 0)
        {
            returnValue.Success = false;
            returnValue.ErrMessage = "该流水号小票云审核已有订单录入！";
            return returnValue; 
        }

        if (OrderDal.CheckCount(string.Format(" and Texts='{0}' and Id not in ({1})", nums, model.Id)) > 0)
        {
            returnValue.Success = false;
            returnValue.ErrMessage = "该流水号已存在！";
            return returnValue; 
        }

        int upcont = OrderDal.UpdateAdds(model, mdlog);

        if (upcont > 0)
        {
            returnValue.Success = true;
            return returnValue; 
        }
         
        returnValue.Success = false;
        returnValue.ErrMessage = "保存失败";

        return returnValue;

    }

    public Model.ReturnValue Success()
    {
        int id = Common.TypeHelper.ObjectToInt(Request["OrderId"], 0);

        model = OrderDal.GetModel(id);

        if (model.Id <= 0)
        {
            returnValue.ErrMessage = "订单不存在";
            returnValue.Success = false;
            return returnValue;
        }

        if (model.States != 0) {
            returnValue.Success = false;
            returnValue.ErrMessage = "订单已审核";
            return returnValue;
        }

        #region 延时发奖(失败则正常发放)
        if (Common.TypeHelper.StringToInt(WebFramework.GeneralMethodBase.GetKeyConfig(49).Val) > 0 && model.IsGrant != -1)
        {
            DelayedOrder(model);

            return returnValue; 
        }
        #endregion
        
        string hideMsg = "";//隐藏串码 短信
        Model.MsgConfigModel msgModel = null;

        if (model.Jx == "一等奖")
        {
            msgModel = MsgConfigDal.GetModel(3);

            #region 发送红包
            Common.RedPackHelper rp = new Common.RedPackHelper();

            int moeny = model.RedPackMoney;

            if (WebFramework.GeneralMethodBase.GetKeyConfig(3).States == 1)
            {
                moeny = 100;
            }

            if (WebFramework.GeneralMethodBase.GetKeyConfig(6).Val.ToLower() == "false") //红包开关
            {
                returnValue.Success = false;
                returnValue.ErrMessage = "微信红包发放未开启，请联系管理员";
                return returnValue;
            }

            int acid = Common.TypeHelper.ObjectToInt(WebFramework.GeneralMethodBase.GetKeyConfig(29).Val, 0);
            int hid = Common.TypeHelper.ObjectToInt(WebFramework.GeneralMethodBase.GetKeyConfig(52).Val, 0);
            string ckey = WebFramework.GeneralMethodBase.GetKeyConfig(30).Val;
            string hkey = WebFramework.GeneralMethodBase.GetKeyConfig(31).Val;

            Common.RedPackHelper.result result = rp.send(acid, hid, model.OpenId, model.HbOrderCode, moeny, ckey, hkey);

            if (result.SendStatus == false)
            {
                returnValue.Success = false;
                returnValue.ErrMessage = result.MSG;
                return returnValue;
            }

            #endregion

        }
        else
        {
            returnValue.Success = false;
            returnValue.ErrMessage = "奖项无需审核发奖";
            return returnValue; 
        }

        model.States = 1; 
        model.Account = UserSession.UserName;
        model.UpdateTime = DateTime.Now;

        mdlog.Status = model.States;
        mdlog.Description = hideMsg;
        mdlog.HideContent = msgModel.MsgTemp;
        mdlog.OrderCode = model.OrderCode;
        mdlog.Mobile = model.Mob;
        mdlog.OperationType = "审核通过订单";

        int i = OrderDal.EditOrder(model, mdlog);
        if (i > 0)
        {
            #region 发送短信
            if (msgModel != null && msgModel.MsgTemp.Length > 10 && model.PrizeCode.Length > 5)
            {
                Common.MessageApi.SendMessage(msgModel.MsgTemp, model.Mob, msgModel.MsgType, msgModel.SupplierId, Common.TypeHelper.ObjectToInt(WebFramework.GeneralMethodBase.GetKeyConfig(20).Val, 0), WebFramework.GeneralMethodBase.GetKeyConfig(21).Val);
            }
            #endregion

            #region 大数据录入-请在订单完成时调用该方法
            WebFramework.OrderService.OrderMethod.OrderInstance.AddOrderApi(model);
            #endregion

            returnValue.Success = true;
            return returnValue;
        } 

        returnValue.Success = false;
        returnValue.ErrMessage = "保存失败";

        return returnValue;

    }

    public Model.ReturnValue DelayedOrder(Model.OrderInfoModel model)
    {
        if (model.Id <= 0)
        {
            returnValue.ErrMessage = "订单不存在";
            returnValue.Success = false;
            return returnValue;
        }

        if (model.States != 0)
        {
            returnValue.Success = false;
            returnValue.ErrMessage = "订单已审核";
            return returnValue;
        }
         
        #region 延时订单
        Model.DelayedAwardsModel DelayedAwardsModel = new Model.DelayedAwardsModel();
        DelayedAwardsModel.OrderId = model.Id; 
        DelayedAwardsModel.CreateTime = DateTime.Now;
        DelayedAwardsModel.DelayedTime = DateTime.Now.AddMinutes(Common.TypeHelper.StringToInt(WebFramework.GeneralMethodBase.GetKeyConfig(51).Val));
        #endregion

        model.States = 1;
        model.IsGrant = 0;
        model.Account = UserSession.UserName;
        model.UpdateTime = DateTime.Now;

        mdlog.Status = model.States;
        mdlog.Description = "";
        mdlog.HideContent = "";
        mdlog.OrderCode = model.OrderCode;
        mdlog.Mobile = model.Mob;
        mdlog.OperationType = "审核通过-转入延时发奖";

        int i = OrderDal.EditAndDelayedOrder(model, mdlog, DelayedAwardsModel);
        if (i > 0)
        { 

            returnValue.Success = true;
            return returnValue;
        }

        returnValue.Success = false;
        returnValue.ErrMessage = "保存失败";

        return returnValue;

    }

    public Model.ReturnValue Fail()
    {

        int id = Common.TypeHelper.ObjectToInt(Request["OrderId"], 0);

        model = OrderDal.GetModel(id);

        if (model.Id <= 0)
        {
            returnValue.ErrMessage = "订单不存在";
            returnValue.Success = false;
            return returnValue;
        }

        if (model.States != 0)
        {
            returnValue.Success = false;
            returnValue.ErrMessage = "订单已审核";
            return returnValue;
        }
         
        Model.MsgConfigModel msgModel = MsgConfigDal.GetModel(1);

        if (msgModel.Id <= 0) {
            returnValue.Success = false;
            returnValue.ErrMessage = "未设置手动作废短信";
            return returnValue;
        }
         
        model.States = -1;
        model.Account = UserSession.UserName;
        model.UpdateTime = DateTime.Now;

        mdlog.OperationType = "作废订单";
        mdlog.Status = model.States;
        mdlog.Description = msgModel.MsgTemp;
        mdlog.HideContent = mdlog.Description;
        mdlog.OrderCode = model.OrderCode;
        mdlog.Mobile = model.Mob;

        int i = 0;

        if (IsBack)
        {
            if (model.IsBack != 0)
            {
                returnValue.Success = false;
                returnValue.ErrMessage = "订单异常-订单奖项已回库";
                return returnValue;
            }

            model.IsBack = 1;//奖项回库
            i = OrderDal.UpdateFail(model, mdlog);
        }
        else
        {
            i = OrderDal.EditOrder(model, mdlog);
        }

        if (i > 0)
        {
            #region 发送短信
            if (mdlog.HideContent.Length > 10)
            {
                Common.MessageApi.SendMessage(msgModel.MsgTemp, model.Mob, msgModel.MsgType, msgModel.SupplierId, Common.TypeHelper.ObjectToInt(WebFramework.GeneralMethodBase.GetKeyConfig(20).Val, 0), WebFramework.GeneralMethodBase.GetKeyConfig(21).Val);
            }
            #endregion

            #region 大数据录入-请在订单完成时调用该方法
            WebFramework.OrderService.OrderMethod.OrderInstance.AddOrderApi(model);
            #endregion

            returnValue.Success = true;
            return returnValue;
        }

        returnValue.Success = false;
        returnValue.ErrMessage = "修改失败";

        return returnValue;

    }
     
    public Model.ReturnValue Unable()
    {
        int id = Common.TypeHelper.ObjectToInt(Request["OrderId"], 0);

        model = OrderDal.GetModel(id);

        if (model.Id <= 0)
        {
            returnValue.ErrMessage = "订单不存在";
            returnValue.Success = false;
            return returnValue;
        }

        if (model.Note.Length > 0)
        {
            returnValue.Success = false;
            returnValue.ErrMessage = "订单已设置无法审核";
            return returnValue;
        }

        model.Note = "无法审核";
        model.Account = UserSession.UserName;
        model.UpdateTime = DateTime.Now;

        mdlog.OperationType = "无法审核订单";
        mdlog.OrderCode = model.OrderCode;
        mdlog.Mobile = model.Mob; 
         
        int i = OrderDal.EditOrder(model, mdlog);
        if (i > 0)
        { 
            returnValue.Success = true;
            return returnValue;
        }

        returnValue.Success = false;
        returnValue.ErrMessage = "修改失败";

        return returnValue;

    }

    public Model.ReturnValue Recovery()
    {
        int id = Common.TypeHelper.ObjectToInt(Request["OrderId"], 0);

        model = OrderDal.GetModel(id);

        if (model.Id <= 0)
        {
            returnValue.ErrMessage = "订单不存在";
            returnValue.Success = false;
            return returnValue;
        }

        if (UserSession.GroupId != 2)
        { 
            returnValue.Success = false;
            returnValue.ErrMessage = "请联系管理员执行此操作";
            return returnValue;
        }

        if (model.States != -1)
        {
            returnValue.Success = false;
            returnValue.ErrMessage = "订单未作废无法恢复";
            return returnValue;
        }

        model.States = 0;
        model.UpdateTime = DateTime.Now;

        mdlog.Status = model.States;
        mdlog.OperationType = "恢复订单状态";
        mdlog.OrderCode = model.OrderCode;
        mdlog.Mobile = model.Mob;

        int i = 0;

        if (IsBack)
        {
            #region Check中奖数是否已满
            List<Model.AwardsStatisticsModel> AwardsList = new Db.AwardsStatisticsDal().GetModelList().Where(w => (w.AwardsId == model.AwardId && w.AwardsType == 1) || (w.AwardsId == model.RedAwardId && w.AwardsType == 2)).ToList();
            if (AwardsList.Count == 0)
            { 
                returnValue.Success = false;
                returnValue.ErrMessage = "系统错误，请联系管理员";
                return returnValue; 
            }
            foreach (var AwardsModel in AwardsList)
            {
                string sqlwhere = "";
                if (AwardsModel.AwardsType == 1)
                    sqlwhere = string.Format(" and AwardId={0} and IsBack=0", model.AwardId);
                if (AwardsModel.AwardsType == 2)
                    sqlwhere = string.Format(" and RedAwardId={0} and IsBack=0", model.RedAwardId);

                if (OrderDal.CheckCount(sqlwhere) >= GetPrizeCount(AwardsModel.AwardsType, AwardsModel.AwardsName))
                {
                    returnValue.Success = false;
                    returnValue.ErrMessage = "奖项总数已达上限，无法恢复订单";
                    return returnValue;
                }
            }
            #endregion

            i = OrderDal.Recovery(model, mdlog);
        }
        else
        {
            i = OrderDal.EditOrder(model, mdlog);
        }

        if (i > 0)
        {
            returnValue.Success = true;
            return returnValue;
        }

        returnValue.Success = false;
        returnValue.ErrMessage = "修改失败";

        return returnValue;

    }

    public Model.ReturnValue BatchDelete() {

        string Delid = Request["BatchId"];

        if (string.IsNullOrEmpty(Delid))
        {
            returnValue.Success = false;
            returnValue.ErrMessage = "请勾选订单！";
            return returnValue; 
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
        int delcont = OrderDal.Del(orderlist);

        if (delcont > 0)
        {
            #region 记录操作日志
            mdlog.Description = "Delid:" + Delid;
            mdlog.OperationType = "批量删除";

            logDal.Add(mdlog);
            #endregion

            returnValue.Success = true;
            returnValue.ErrMessage = "删除成功！";
            return returnValue; 
        }
        returnValue.Success = false;
        returnValue.ErrMessage = "删除失败";
        return returnValue; 
    
    }

    public Model.ReturnValue BatchFail()
    {
        if (IsBack) {
            returnValue.Success = false;
            returnValue.ErrMessage = "作废自动回库暂不支持批量作废！";
            return returnValue;
        }

        string Delid = Request["BatchId"];

        if (string.IsNullOrEmpty(Delid))
        {
            returnValue.Success = false;
            returnValue.ErrMessage = "请勾选订单！";
            return returnValue;
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
        int delcont = OrderDal.Update(orderlist);
        if (delcont > 0)
        {
            #region 记录操作日志
            mdlog.Description = "Delid:" + Delid;
            mdlog.OperationType = "批量作废";

            logDal.Add(mdlog);
            #endregion

            returnValue.Success = true;
            returnValue.ErrMessage = "作废成功！";
            return returnValue;
        }
        returnValue.Success = false;
        returnValue.ErrMessage = "作废失败";
        return returnValue;

    }

    public Model.ReturnValue BatchRecorded()
    {

        string Delid = Request["BatchId"];

        if (string.IsNullOrEmpty(Delid))
        {
            returnValue.Success = false;
            returnValue.ErrMessage = "请勾选订单！";
            return returnValue;
        }

        BDImgApi.APIKey.appUserName = WebFramework.GeneralMethodBase.GetKeyConfig(25).Val; ;// 
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
                int OId = Common.TypeHelper.ObjectToInt(Did, 0);
                if (OId > 0)
                {
                    Model.OrderInfoModel ordel = OrderDal.GetModel(OId);
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
        mdlog.OperationType = "批量OCR预审";

        logDal.Add(mdlog);
        #endregion
          
        returnValue.Success = true;
        returnValue.ErrMessage = "成功";
        return returnValue;

    }

    public Model.ReturnValue BatchRecordedAll()
    {

        string appUserName = WebFramework.GeneralMethodBase.GetKeyConfig(25).Val;
        string appKey = WebFramework.GeneralMethodBase.GetKeyConfig(26).Val;
        string appSecret = WebFramework.GeneralMethodBase.GetKeyConfig(27).Val;

        Task task1 = Task.Factory.StartNew(() =>
        {

            BDImgApi.APIKey.appUserName = appUserName;// 
            BDImgApi.APIKey.appKey = appKey;// 
            BDImgApi.APIKey.appSecret = appSecret;// 

            string ResultStr = "";
            try
            {
                List<Model.OrderInfoModel> Notorderlist = OrderDal.GetModelNotOCRList(100);

                foreach (Model.OrderInfoModel NotModel in Notorderlist)
                {
                    if (NotModel.Id > 0)
                    {
                        BDImgApi.GetApiResult Result = new BDImgApi.GetApiResult();
                        Model.FileInfoModel FileModel = new Db.FileInfoDal().GetModel(NotModel.FilesId);

                        if (FileModel.Id > 0)
                            ResultStr += NotModel.OrderCode + ":" + Result.GetMatchingImg(FileModel.Hashdata, "1", FileModel.FileName, NotModel.OrderCode, false).errMsg + "\n";
                    }
                }
            }
            catch (Exception ex)
            {

            }

            Common.LogCommon.WriteFileLog(ResultStr, "BDImgApi");
        });

        #region 记录操作日志
        mdlog.Description = "";
        mdlog.OperationType = "批量OCR预审未录入小票";

        logDal.Add(mdlog);
        #endregion

        returnValue.Success = true;
        returnValue.ErrMessage = "成功";
        return returnValue;
    }

    public Model.ReturnValue DelayedReissue()
    {
        int id = Common.TypeHelper.ObjectToInt(Request["DelayedId"], 0);

        Model.DelayedAwardsModel DelayedModel=new Db.DelayedAwardsDal().GetModel(id);

        if (DelayedModel.Id <= 0)
        {
            returnValue.ErrMessage = "延时订单不存在";
            returnValue.Success = false;
            return returnValue;
        }

        if (DelayedModel.StatusId != -1)
        {
            returnValue.Success = false;
            returnValue.ErrMessage = "延时订单状态非发送失败";
            return returnValue;
        }

        model = OrderDal.GetModel(DelayedModel.OrderId);

        if (model.Id <= 0)
        {
            returnValue.ErrMessage = "订单不存在";
            returnValue.Success = false;
            return returnValue;
        }

        if (model.States != 0) {
            returnValue.Success = false;
            returnValue.ErrMessage = "订单已审核";
            return returnValue;
        }

        //发奖逻辑写完  删除此代码
        returnValue.Success = false;
        returnValue.ErrMessage = "订单已审核";
        return returnValue;

        string hideMsg = "";//隐藏串码 短信
        Model.MsgConfigModel msgModel = null;

        if (model.Jx == "一等奖")
        {
            msgModel = MsgConfigDal.GetModel(3);
             
        }
        else
        {
            returnValue.Success = false;
            returnValue.ErrMessage = "奖项无需审核发奖";
            return returnValue; 
        }

        DelayedModel.StatusId = 1;
        DelayedModel.UpdateTime = DateTime.Now;

        model.States = 1; 
        model.Account = UserSession.UserName;
        model.UpdateTime = DateTime.Now;

        model.IsGrant = 1;
        model.GrantTime = model.UpdateTime;

        mdlog.Status = model.States;
        mdlog.Description = hideMsg;
        mdlog.HideContent = msgModel.MsgTemp;
        mdlog.OrderCode = model.OrderCode;
        mdlog.Mobile = model.Mob;
        mdlog.OperationType = "延时订单发送失败，补发订单";

        int i = OrderDal.EditDelayedOrder(model, mdlog, DelayedModel);
        if (i > 0)
        {

            #region 发送短信
            if (msgModel != null && msgModel.MsgTemp.Length > 10 && model.PrizeCode.Length > 5)
            {
                Common.MessageApi.SendMessage(msgModel.MsgTemp, model.Mob, msgModel.MsgType, msgModel.SupplierId, Common.TypeHelper.ObjectToInt(WebFramework.GeneralMethodBase.GetKeyConfig(20).Val, 0), WebFramework.GeneralMethodBase.GetKeyConfig(21).Val);
            }
            #endregion

            #region 大数据录入-请在订单完成时调用该方法
            WebFramework.OrderService.OrderMethod.OrderInstance.AddOrderApi(model);
            #endregion

            returnValue.Success = true;
            return returnValue;
        } 

        returnValue.Success = false;
        returnValue.ErrMessage = "保存失败";

        return returnValue;

    }

    public Model.ReturnValue ReissueHb()
    {
        int id = Common.TypeHelper.ObjectToInt(Request["OrderId"], 0);

        model = OrderDal.GetModel(id);

        if (model.Id <= 0)
        {
            returnValue.ErrMessage = "订单不存在";
            returnValue.Success = false;
            return returnValue;
        }

        if (model.States != 1)
        {
            returnValue.Success = false;
            returnValue.ErrMessage = "订单未审核";
            return returnValue;
        } 

        string OpenId = "";
        string HbOrderCode = "";
        int money = 0;

        string SearchMsg = "";
         
        SearchMsg = Common.SearchRedPack.SearchOrder(model.HbOrderCode);
        if (SearchMsg != "已退款" && SearchMsg == "发放失败")
        {
            returnValue.Success = false;
            returnValue.ErrMessage = SearchMsg;
            return returnValue;
        }

        OpenId = model.OpenId;
        HbOrderCode = model.HbOrderCode.Substring(0, 11) + "9" + model.HbOrderCode.Substring(12, 6);
        money = model.RedPackMoney; 

        #region 发送红包
        Common.RedPackHelper rp = new Common.RedPackHelper();

        if (WebFramework.GeneralMethodBase.GetKeyConfig(3).States == 1)
        {
            money = 100;
        }

        if (WebFramework.GeneralMethodBase.GetKeyConfig(6).Val.ToLower() == "false") //红包开关
        {
            returnValue.Success = false;
            returnValue.ErrMessage = "微信红包发放未开启，请联系管理员";
            return returnValue;
        }

        int acid = Common.TypeHelper.ObjectToInt(WebFramework.GeneralMethodBase.GetKeyConfig(29).Val, 0);
        int hid = acid;
        string ckey = WebFramework.GeneralMethodBase.GetKeyConfig(30).Val;
        string hkey = WebFramework.GeneralMethodBase.GetKeyConfig(31).Val;

        Common.RedPackHelper.result result2 = rp.send(acid, hid, OpenId, HbOrderCode, money, ckey, hkey);

        if (result2.SendStatus == false)
        {
            returnValue.Success = false;
            returnValue.ErrMessage = result2.MSG;
            return returnValue;
        }
        #endregion

        mdlog.LStatus = 0;
        mdlog.Status = 1;
        mdlog.Description = "原单号：" + model.HbOrderCode;
        mdlog.HideContent = "修改后单号：" + HbOrderCode + ",发送金额：" + money;
        mdlog.OrderCode = model.OrderCode;
        mdlog.Mobile = model.Mob;
        mdlog.OperationType = "红包退款-补发红包";
         
        model.HbOrderCode = HbOrderCode; 

        int i = OrderDal.ReissueHb(model, mdlog);
        if (i > 0)
        {
            returnValue.Success = true;
            return returnValue;
        }

        returnValue.Success = false;
        returnValue.ErrMessage = "保存失败";

        return returnValue;

    }

    public int GetPrizeCount(int id,string PrizeName) {

        Model.ZpConfigModel ZpConfigModel = new Db.ZpConfigDal().GetModel(id);

        int Count = 0;

        switch (PrizeName) { 

            case "一等奖":
                Count = ZpConfigModel.Zjl8;
                break;
            case "二等奖":
                Count = ZpConfigModel.Zjl10;
                break;
            case "三等奖":
                Count = ZpConfigModel.Zjl12;
                break;
            case "四等奖":
                Count = ZpConfigModel.Zjl14;
                break;
            case "五等奖":
                Count = ZpConfigModel.Zjl16;
                break;
            case "六等奖":
                Count = ZpConfigModel.Zjl18;
                break;  
        }
        return Count;
    }

}