using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebFramework.ESLog;


/// <summary>
/// ProcessCodeDraw 的摘要说明
/// </summary>
public class ProcessCodeDraw {

    private Model.SessionModel orderSession = null; 

    public ProcessCodeDraw()
    {

    }

    Db.OrderInfoDal OrderDal = new Db.OrderInfoDal(); 
    Db.PassCodeInfoDal PassCodeDal = new Db.PassCodeInfoDal();

    private static readonly object _SyncLock = new object();
    private static readonly object _PrizeLock = new object();

    public Model.ReturnValue ProcessCode(HttpContext context)
    {
        Model.ReturnValue result = new Model.ReturnValue();
        
        #region 验证Session
        result = CheckSession();
        if (result.Success == false)
        {
            ESLogMethod.ESLogInstance.Debug("Session不通过", "");

            return result;
        }
        #endregion

        string OpenId = orderSession.OpenId;
        string Mobile = context.Request["mob"];
        string Code = context.Request["code"];

        #region 验证OpenId
        if (Common.ValidateHelper.IsOpenid(OpenId) == false)
        {
            ESLogMethod.ESLogInstance.Debug("Openid格式异常", OpenId, Code);

            result.ErrMessage = "系统繁忙，请稍后再试";
            result.Success = false;
            return result;
        }
        #endregion

        #region 验证手机号
        if (Common.ValidateHelper.IsMobile(Mobile) == false)
        {
            ESLogMethod.ESLogInstance.Debug("手机号错误", Mobile, Code);

            result.ErrMessage = "请填写正确手机号";
            result.Success = false;
            return result;
        }
        #endregion

        string Ip = Common.ClientIP.GetIp();

        #region 检查IP
        if (Db.Security.IpAccessControlDal.CheckIpIsOK(false, CacheBase.IPSettingModel, Ip, "", "") == false)
        {
            ESLogMethod.ESLogInstance.Debug("IP模型超过限制", Ip, Code);

            result.ErrMessage = "系统繁忙，请稍后再试";
            result.Success = false;
            return result;
        }
        #endregion

        #region 验证激活码
        if (Common.ValidateHelper.IsCode(Code, 13) == false)
        {

            #region 登记IP
            Db.Security.IpAccessControlDal.CheckIpIsOK(true, CacheBase.IPSettingModel, Ip, Code, "激活码格式错误");
            #endregion

            ESLogMethod.ESLogInstance.Debug("激活码格式错误", Code, Mobile);

            result.ErrMessage = "请填写正确的激活码";
            result.Success = false;
            return result;  
        }
        #endregion
         
        lock (_SyncLock)
        {
            Model.PassCodeInfoModel PassCodeModel = PassCodeDal.GetModelByCode(Code);

            #region 验证激活码状态
            if (PassCodeModel.Id <= 0)
            {
                #region 登记IP
                Db.Security.IpAccessControlDal.CheckIpIsOK(true, CacheBase.IPSettingModel, Ip, Code, "库无此激活码");
                #endregion

                ESLogMethod.ESLogInstance.Debug("激活码不存在", Code, Mobile);

                result.ErrMessage = "请填写正确的激活码";
                result.Success = false;
                return result;
            }

            if (PassCodeModel.StatusId == 1)
            {
                Model.OrderInfoModel orderdel = OrderDal.GetModelByCode(Code);

                if (orderdel.Id <= 0)
                {
                    #region 登记IP
                    Db.Security.IpAccessControlDal.CheckIpIsOK(true, CacheBase.IPSettingModel, Ip, Code, "订单无此串码");
                    #endregion

                    ESLogMethod.ESLogInstance.Debug("激活码已激活无订单", Code, Mobile);

                    result.ErrMessage = "请填写正确的激活码";
                    result.Success = false;
                    return result;
                }

                if (orderdel.OpenId != OpenId)
                {
                    ESLogMethod.ESLogInstance.Debug("激活码已绑定其他微信号", OpenId, Code);
                     
                    result.ErrMessage = "激活码已绑定其他微信号";
                    result.Success = false;
                    return result;
                } 

                result.ErrMessage = "提交成功";
                result.Success = true;
                return result;
            }
            #endregion

            #region 检查手机号-openid-IP 参与次数是否超限
            string maxcont = WebFramework.OrderService.OrderMethod.OrderInstance.CheckMobOpidIp(Mobile, OpenId, Ip);

            if (string.IsNullOrEmpty(maxcont) == false)
            {
                ESLogMethod.ESLogInstance.Debug("参与次数超限", Code, maxcont);

                result.ErrMessage = maxcont;
                result.Success = false;
                return result;
            }
            #endregion

            #region 抽奖-保存订单
            int goid = WebFramework.GeneralMethodBase.GenerationOrderId(0);

            #region 抽奖
            Model.AwardsStatisticsModel PrizeModel = WebFramework.GeneralMethodBase.GetPrize(7, 1);
            #endregion

            Model.OrderInfoModel model = new Model.OrderInfoModel();
            model.OrderCode = WebFramework.GeneralMethodBase.CreateOrderCode(goid);
            model.Jx = PrizeModel.AwardsName;
            model.Jp = PrizeModel.PrizeName;
            model.DateStamp = DateTime.Now.ToString("yyyyMMdd");
            model.Ip = Ip;
            model.IpAddress = string.Join("-", Common.ClientIP.GetArrayAdds(model.Ip));
            model.CreateTime = DateTime.Now;
            model.OpenId = OpenId;
            model.Mob = Mobile;
            model.Code = Code;
            model.States = 0;

            #region 抽取红包奖项
            Model.AwardsStatisticsModel RedPackPrizeModel = new Model.AwardsStatisticsModel();

            if (model.Jp == "微信红包")
            {
                //RedPackPrizeModel = WebFramework.GeneralMethodBase.GetPrize(7, 2);
                //model.HbOrderCode = WebFramework.GeneralMethodBase.CreateHbCode(goid);//创建红包单号
                //model.RedPackMoney = Common.TypeHelper.ObjectToInt(RedPackPrizeModel.PrizeName, 0);//获取红包金额
            }
            #endregion

            #region 节流
            if (WebFramework.PrivacyDemand.PrivacyMethod.PrivacyInstance.Throttling())
            {
                model.States = 1;
            }
            #endregion

            PassCodeModel.Mob = Mobile;
            PassCodeModel.StatusId = 1;
            PassCodeModel.ActiveTime = DateTime.Now;
            PassCodeModel.OpenId = model.OpenId;

            Model.OrderLogModel mdlog = new Model.OrderLogModel();
            mdlog.OId = 0;
            mdlog.OrderCode = model.OrderCode;
            mdlog.Mob = Mobile;
            mdlog.UpTime = DateTime.Now;
            mdlog.LStatus = 0;
            mdlog.Status = 1;
            mdlog.Notes = "";

            int OrderId = 0;//插入订单ID

            //此方法不可替换更改，因与抽奖查询关联
            if (OrderDal.AddOrderInfo_UpdatePassCodeInfo_AddOrderLog(model, PassCodeModel, mdlog, PrizeModel.Id, RedPackPrizeModel.Id, ref OrderId) > 0)
            {
                #region 虚拟订单
                WebFramework.PrivacyDemand.PrivacyMethod.PrivacyInstance.AddFictitiousOrder(model);
                #endregion
                 
                result.ErrMessage = "提交成功";
                result.Success = true;
                return result; ;
            }
            #endregion

        }

        ESLogMethod.ESLogInstance.Error("添加订单失败", Code, Mobile);

        result.ErrMessage = "系统繁忙，请稍后再试";
        result.Success = false;
        return result; 

    }
     
    public Model.ReturnValue ProcessUpdateInfo(HttpContext context)
    {

        Model.ReturnValue result = new Model.ReturnValue();

        #region 验证Session
        result = CheckSession();
        if (result.Success == false)
        {
            ESLogMethod.ESLogInstance.Debug("Session不通过", ""); 
            return result;
        }
        #endregion

        string OpenId = orderSession.OpenId;
        string Code = orderSession.Code; 
        string Name = context.Request["name"];
        string Mobile = context.Request["mob"];
        string Adds = context.Request["adds"];

        #region 验证openid
        if (Common.ValidateHelper.IsOpenid(OpenId) == false)
        {
            ESLogMethod.ESLogInstance.Debug("Openid格式异常", OpenId, Code);

            result.ErrMessage = "系统繁忙，请稍后再试";
            result.Success = false;
            return result;
        }
        #endregion

        #region 验证手机号
        if (Common.ValidateHelper.IsMobile(Mobile) == false)
        {
            ESLogMethod.ESLogInstance.Debug("手机号错误", Mobile, Code);

            result.ErrMessage = "请填写正确手机号";
            result.Success = false;
            return result;
        }
        #endregion

        #region 验证姓名
        if (Common.ValidateHelper.IsName(Name) == false || Name.Length > 12)
        {
            ESLogMethod.ESLogInstance.Debug("姓名错误", Name, Code);

            result.ErrMessage = "请填写正确姓名";
            result.Success = false;
            return result; 
        }
        #endregion

        #region 验证地址

        if (Common.ValidateHelper.IsAddrs(Adds) == false || Adds.Length > 100)
        {
            ESLogMethod.ESLogInstance.Debug("地址错误", Adds, Code);

            result.ErrMessage = "请填写正确地址";
            result.Success = false;
            return result;
        }
        #endregion

        lock (_PrizeLock)
        {
            Model.OrderInfoModel orderdel = OrderDal.GetModelByCode(Code);

            #region 验证Code订单状态
            if (orderdel.Id <= 0)
            {
                ESLogMethod.ESLogInstance.Debug("Code不存在", Code, Mobile);

                result.ErrMessage = "系统繁忙，请稍后再试";
                result.Success = false;
                return result;
            }

            if (orderdel.Types != 0)
            {
                ESLogMethod.ESLogInstance.Debug("订单Types异常", orderdel.Types.ToString(), Code);

                result.ErrMessage = "系统繁忙，请稍后再试";
                result.Success = false;
                return result;
            }

            if (OpenId != orderdel.OpenId)
            {
                ESLogMethod.ESLogInstance.Debug("订单OpenId不匹配", OpenId, Code);

                result.ErrMessage = "系统繁忙，请稍后再试";
                result.Success = false;
                return result;
            }

            if (orderdel.States > 0)
            {
                result.ErrMessage = "提交成功";
                result.Success = true;
                return result;
            }
            #endregion

            orderdel.Name = Name;
            orderdel.Adds = Adds;
            orderdel.Types = 1;
            orderdel.States = 0;
            orderdel.PrizeCode = "";
            orderdel.Mob = Mobile;

            #region 延时发放奖品

            if (WebFramework.PrivacyDemand.PrivacyMethod.PrivacyInstance.DelayedMethod(orderdel.Id)) {

                result.ErrMessage = "提交成功";
                result.Success = true;
                return result;
            }
            #endregion

            #region 调取资源库串码
            string codes = WebFramework.OrderService.OrderMethod.OrderInstance.GetEswAPI(0, 0, orderdel.Mob, orderdel.OrderCode);

            //if (string.IsNullOrEmpty(codes) == false)
            //{
            //    orderdel.PrizeCode = codes;
            //    orderdel.States = 1;

            //}
            #endregion

            #region 调取优酷串码
            //string codes = WebFramework.OrderService.OrderMethod.OrderInstance.GetAPIClient(0, 0, orderdel.Mob, orderdel.OrderCode, WebFramework.GeneralMethodBase.GetKeyConfig(22).Val);

            //if (string.IsNullOrEmpty(codes) == false)
            //{
            //    orderdel.PrizeCode = codes;
            //    orderdel.States = 1;
            //}
            #endregion

            Model.OrderLogModel OrderLog = new Model.OrderLogModel();
            OrderLog.OId = orderdel.Id;
            OrderLog.Mob = Mobile;
            OrderLog.OrderCode = orderdel.PrizeCode;
            OrderLog.LStatus = 8;//上传信息 
            OrderLog.Notes = orderdel.Name + ";" + orderdel.Adds;
            OrderLog.Status = 1;
            OrderLog.UpTime = DateTime.Now;
             
            if (OrderDal.UpdateInfo(orderdel, OrderLog) > 0)
            {

                #region 大数据录入-请在订单完成时调用该方法
                if (orderdel.States == 1)
                {
                    WebFramework.OrderService.OrderMethod.OrderInstance.AddOrderApi(orderdel);
                }
                #endregion

                result.ErrMessage = "提交成功";
                result.Success = true;
                return result;
            }

            ESLogMethod.ESLogInstance.Error("修改订单失败", Code);

            result.ErrMessage = "系统繁忙，请稍后再试";
            result.Success = false;
            return result;
        }

    }

    public Model.ReturnValue ProcessUpdateTypes(HttpContext context)
    {
        Model.ReturnValue result = new Model.ReturnValue();

        #region 验证Session
        result = CheckSession();
        if (result.Success == false)
        {
            WebFramework.GeneralMethodBase.WebDebugLog("session", "session不通过", context);
            return result;
        }
        #endregion

        string OpenId = orderSession.OpenId;
        string Code = orderSession.Code;

        #region 验证openid
        if (Common.ValidateHelper.IsOpenid(OpenId) == false)
        {
            WebFramework.GeneralMethodBase.WebDebugLog(Code, "OpenId格式不正确:" + OpenId);

            result.ErrMessage = "系统繁忙，请稍后再试";
            result.Success = false;
            return result;
        }
        #endregion

        #region 验证Code

        Model.OrderInfoModel orderdel = OrderDal.GetModelByCode(Code);
        if (orderdel.Id <= 0)
        {
            WebFramework.GeneralMethodBase.WebDebugLog(Code, "Code不存在：" + Code);

            result.ErrMessage = "系统繁忙，请稍后再试";
            result.Success = false;
            return result;  
        }
        if (orderdel.Types != 0)
        {
            WebFramework.GeneralMethodBase.WebDebugLog(Code, "订单Types异常：" + orderdel.Types);

            result.ErrMessage = "系统繁忙，请稍后再试";
            result.Success = false;
            return result;
        }

        if (OpenId != orderdel.OpenId)
        {
            WebFramework.GeneralMethodBase.WebDebugLog(Code, "订单OpenId不匹配-订单OpenId:" + orderdel.OpenId + "-OpenId:" + OpenId);

            result.ErrMessage = "系统繁忙，请稍后再试";
            result.Success = false;
            return result;
        }
        #endregion

        orderdel.Types = 1;

        if (OrderDal.UpdateTypes(orderdel) > 0)
        {
            result.ErrMessage = "提交成功";
            result.Success = true;
            return result;
        }

        ESLogMethod.ESLogInstance.Error("修改订单失败", Code);

        result.ErrMessage = "系统繁忙，请稍后再试";
        result.Success = false;
        return result; 

    }

    public Model.ReturnValue CheckSession()
    {
        Model.ReturnValue result = new Model.ReturnValue();

        #region 获取session
        orderSession = WebFramework.SessionManage.SessionMethod.SessionInstance.GetSession();
        if (orderSession == null)
        {
             
            result.ErrMessage = "系统繁忙，请稍后再试！";
            result.Success = false;

            return result;
        }
        else
        {
            result.ErrMessage = "";
            result.Success = true;

            return result;
        }

        #endregion

    }

}