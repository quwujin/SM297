using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebFramework.ESLog;


/// <summary>
/// ProcessGetPrize 的摘要说明
/// </summary>
public class ProcessUploadFile
{

      private Model.SessionModel orderSession = null; 

      public ProcessUploadFile()
      {

      }

      Db.OrderInfoDal oddal = new Db.OrderInfoDal();
      Db.OrderLogDal logdal = new Db.OrderLogDal();
      Db.FileInfoDal FileInfoDal = new Db.FileInfoDal();

      private static readonly object _SyncLock = new object();
      private static readonly object _PrizeLock = new object();
    //上传小票
      public Model.ReturnValue ProcessUpload(HttpContext context)
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
          int FileId = orderSession.FileId;
          string Ip = Common.ClientIP.GetIp();

          #region 验证OpenId
          if (Common.ValidateHelper.IsOpenid(OpenId) == false)
          {
              ESLogMethod.ESLogInstance.Debug("Openid格式异常", OpenId, Mobile);

              result.ErrMessage = "系统繁忙，请稍后再试";
              result.Success = false;
              return result; 
          }
          #endregion

          #region 验证手机号
          if (Common.ValidateHelper.IsMobile(Mobile) == false)
          {
              ESLogMethod.ESLogInstance.Debug("手机号错误", Mobile);

              result.ErrMessage = "请填写正确手机号";
              result.Success = false;
              return result;  
          }
          #endregion
           
          lock (_SyncLock)
          { 
              #region 验证小票
              Model.FileInfoModel filedel = new Db.FileInfoDal().GetModel(FileId);
              if (filedel.Id <= 0)
              {
                  ESLogMethod.ESLogInstance.Debug("小票不存在", FileId.ToString(), Mobile);

                  result.ErrMessage = "请上传小票";
                  result.Success = false;
                  return result;
              }

              #region 验证小票是否重复
              if (Common.TypeHelper.ObjectToBool(WebFramework.GeneralMethodBase.GetKeyConfig(11).Val, true))
              {
                  int RepetCount = new Db.OrderInfoDal().CheckFilesRepeatCount(filedel.Hashdata);

                  if (RepetCount > 0)
                  {
                      filedel.States = -1;
                  } 
              }
              #endregion 
              #endregion

              #region 检查是否有中奖未完善信息订单

              //Model.OrderInfoModel Nomobdel = oddal.GetModel(string.Format(" Mob='{0}' and Types=0 and Jx<>'参与奖' and States=0", mob));
              //if (Nomobdel.Id > 0)
              //{
              //    orderSession.Id = Nomobdel.Id;

              //    returnErro(context, "true", "提交成功", "");
              //    context.Response.End();
              //    return;
              //}
              #endregion

              #region 检查手机号-openid-IP 参与次数是否超限
              string maxcont = WebFramework.OrderService.OrderMethod.OrderInstance.CheckMobOpidIp(Mobile, OpenId, Ip);

              if (string.IsNullOrEmpty(maxcont) == false)
              {
                  ESLogMethod.ESLogInstance.Debug("参与总数超限", maxcont);

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
               
              model.Jx = PrizeModel.AwardsName;
              model.OrderCode = WebFramework.GeneralMethodBase.CreateOrderCode(goid);
              model.Jp = PrizeModel.PrizeName;
              model.States = filedel.States;
              model.DateStamp = DateTime.Now.ToString("yyyyMMdd");
              model.Ip = Ip;
              model.IpAddress = string.Join("-", Common.ClientIP.GetArrayAdds(model.Ip));
              model.CreateTime = DateTime.Now;
              model.OpenId = OpenId;
              model.Mob = Mobile;
              model.FilesId = FileId;

              #region 抽取红包奖项
              Model.AwardsStatisticsModel RedPackPrizeModel = new Model.AwardsStatisticsModel();
              if (model.Jp == "微信红包")
              {
                  //RedPackPrizeModel = WebFramework.GeneralMethodBase.GetPrize(7, 2);
                  //model.HbOrderCode = WebFramework.GeneralMethodBase.CreateHbCode(goid);//创建红包单号
                  //model.RedPackMoney = Common.TypeHelper.ObjectToInt(RedPackPrizeModel.PrizeName, 0);//获取红包金额
              }
              #endregion

              int OrderId = 0;

              //此方法不可替换更改，因与抽奖查询关联
              if (oddal.AddOrderInfo_UpdatePassCodeInfo_AddOrderLog(model, null, null, PrizeModel.Id, RedPackPrizeModel.Id, ref OrderId) > 0)
              {

                  #region 发送自动作废短信
                  if (model.States == -1)
                  {
                      WebFramework.GeneralMethodBase.GetMsg(2, model.Mob, model.OrderCode); 
                  }
                  #endregion

                  #region OCR预录
                  if (filedel != null)
                  {
                      WebFramework.OrderService.OrderMethod.OrderInstance.OcrRecorded(model.OrderCode, filedel.Hashdata, filedel.FileName);
                  }
                  #endregion

                  orderSession.Id = OrderId;
                  WebFramework.SessionManage.SessionMethod.SessionInstance.SetSession(orderSession);

                  result.ErrMessage = "提交成功";
                  result.Success = true;
                  return result;
              }
              #endregion
          }

          ESLogMethod.ESLogInstance.Error("添加订单失败", Mobile);

          result.ErrMessage = "系统繁忙，请稍后再试";
          result.Success = false;
          return result; 
      }

      public Model.ReturnValue ProcessUploadFiles(HttpContext context)
      {
          Model.ReturnValue result = new Model.ReturnValue();

          #region 验证Session
          result = CheckSession();
          if (result.Success == false)
          {
              ESLogMethod.ESLogInstance.Debug("Session不通过",""); 
              return result;
          }
          #endregion

          string OpenId = orderSession.OpenId;
          int FileId = orderSession.FileId;

          int orderId = Common.TypeHelper.ObjectToInt(orderSession.Id, 0);

          #region 验证openid
          if (Common.ValidateHelper.IsOpenid(OpenId) == false)
          {
              ESLogMethod.ESLogInstance.Debug("OpenId格式不正确", OpenId, orderId.ToString());

              result.ErrMessage = "系统繁忙，请稍后再试";
              result.Success = false;
              return result;
          }
          #endregion

          #region 验证小票
          Model.FileInfoModel filedel = new Db.FileInfoDal().GetModel(FileId);
          if (filedel.Id <= 0)
          {
              ESLogMethod.ESLogInstance.Debug("小票不存在", FileId.ToString(), orderId.ToString());

              result.ErrMessage = "请上传小票";
              result.Success = false;
              return result;
          }

          #region 验证小票是否重复
          if (Common.TypeHelper.ObjectToBool(WebFramework.GeneralMethodBase.GetKeyConfig(11).Val, true))
          {
              int RepetCount = new Db.OrderInfoDal().CheckFilesRepeatCount(filedel.Hashdata);

              if (RepetCount > 0)
              {
                  filedel.States = -1;
              }
          }
          #endregion
          #endregion

          lock (_PrizeLock)
          {
              #region 验证订单Id
              Model.OrderInfoModel orderdel = oddal.GetModel(orderId);
              if (orderId <= 0)
              {
                  ESLogMethod.ESLogInstance.Debug("订单ID不存在", orderId.ToString());

                  result.ErrMessage = "系统繁忙，请稍后再试";
                  result.Success = false;
                  return result;
              }
              if (orderdel.FilesId > 0)
              {
                  result.ErrMessage = "提交成功";
                  result.Success = false;
                  return result;
              }
              if (OpenId != orderdel.OpenId)
              {
                  ESLogMethod.ESLogInstance.Debug("OpenId与订单不匹配", OpenId, orderId.ToString());

                  result.ErrMessage = "系统繁忙，请稍后再试";
                  result.Success = false;
                  return result;
              }
              #endregion

              orderdel.FilesId = FileId;
              orderdel.States = filedel.States; 

              Model.OrderLogModel OrderLog = new Model.OrderLogModel();
              OrderLog.OId = orderdel.Id;
              OrderLog.Mob = orderdel.Mob;
              OrderLog.OrderCode = orderdel.OrderCode;
              OrderLog.LStatus = 9;//上传小票
              OrderLog.Notes = orderdel.FilesId + ";" + orderdel.States;
              OrderLog.Status = 1;
              OrderLog.UpTime = DateTime.Now;

              if (oddal.UpdateFiles(orderdel, OrderLog) > 0)
              {
                  #region 发送自动作废短信
                  if (orderdel.States == -1)
                  {
                      WebFramework.GeneralMethodBase.GetMsg(2, orderdel.Mob, orderdel.OrderCode);
                  }
                  #endregion

                  #region OCR预录
                  if (filedel != null)
                  {
                      WebFramework.OrderService.OrderMethod.OrderInstance.OcrRecorded(orderdel.OrderCode, filedel.Hashdata, filedel.FileName);
                  }
                  #endregion

                  result.ErrMessage = "提交成功";
                  result.Success = true;
                  return result;
              }

              ESLogMethod.ESLogInstance.Error("修改订单失败", orderId.ToString());

              result.ErrMessage = "系统繁忙，请稍后再试";
              result.Success = false;
              return result;
          }
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
          string Name = context.Request["name"];
          string Mobile = context.Request["mob"];
          string Adds = context.Request["adds"];

          int orderId = Common.TypeHelper.ObjectToInt(orderSession.Id, 0);

          #region 验证openid
          if (Common.ValidateHelper.IsOpenid(OpenId) == false)
          {
              ESLogMethod.ESLogInstance.Debug("OpenId格式不正确", OpenId, Mobile); 

              result.ErrMessage = "系统繁忙，请稍后再试";
              result.Success = false;
              return result; 
          }
          #endregion

          #region 验证手机号
          if (Common.ValidateHelper.IsMobile(Mobile) == false)
          {
              ESLogMethod.ESLogInstance.Debug("手机号错误", Mobile, OpenId); 

              result.ErrMessage = "请填写正确手机号";
              result.Success = false;
              return result;
          }
          #endregion

          #region 验证姓名
          if (Common.ValidateHelper.IsName(Name) == false || Name.Length > 12)
          {
              ESLogMethod.ESLogInstance.Debug("手机号错误", Name, Mobile); 

              result.ErrMessage = "请填写正确姓名";
              result.Success = false;
              return result;  
          }
          #endregion

          #region 验证地址

          if (Common.ValidateHelper.IsAddrs(Adds) == false || Adds.Length > 100)
          {
              ESLogMethod.ESLogInstance.Debug("地址错误", Adds, Mobile); 

              result.ErrMessage = "请填写正确地址";
              result.Success = false;
              return result;   
          }
          #endregion

          lock (_PrizeLock)
          {
              #region 验证订单Id
              Model.OrderInfoModel orderdel = oddal.GetModel(orderId);
              if (orderId <= 0)
              {
                  ESLogMethod.ESLogInstance.Debug("订单ID不存在", orderId.ToString(), Mobile); 

                  result.ErrMessage = "系统繁忙，请稍后再试";
                  result.Success = false;
                  return result;
              }
              if (orderdel.Types > 0)
              { 
                  result.ErrMessage = "提交成功";
                  result.Success = false;
                  return result;
              }
              if (OpenId != orderdel.OpenId)
              {
                  ESLogMethod.ESLogInstance.Debug("订单OpenId不匹配", OpenId, orderId.ToString()); 

                  result.ErrMessage = "系统繁忙，请稍后再试";
                  result.Success = false;
                  return result;
              }
              #endregion

              orderdel.Name = Name;
              orderdel.Adds = Adds;
              orderdel.Types = 1;
              orderdel.States = 0;
              orderdel.PrizeCode = "";
              orderdel.Mob = Mobile;

              #region 调取资源库串码
              //string codes = WebFramework.OrderService.OrderMethod.OrderInstance.GetYHPsiAPI(0, 0, orderdel.Mob, orderdel.OrderCode);

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

              if (oddal.UpdateInfo(orderdel, OrderLog) > 0)
              {
                  result.ErrMessage = "提交成功";
                  result.Success = true;
                  return result;
              }

              ESLogMethod.ESLogInstance.Error("修改订单失败", orderId.ToString(), Mobile); 

              result.ErrMessage = "系统繁忙，请稍后再试";
              result.Success = false;
              return result;
          }
      }
     
      public Model.ReturnValue CheckSession()
      {
          Model.ReturnValue result = new Model.ReturnValue();

          #region 获取session
          orderSession = WebFramework.SessionManage.SessionMethod.SessionInstance.GetSession();
          if (orderSession == null)
          {
              //WebFramework.GeneralMethodBase.WebDebugLog("session不通过");
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