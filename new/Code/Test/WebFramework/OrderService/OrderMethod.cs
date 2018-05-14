using Esmart.Crm.Interface.Vip;
using Esmart.Framework.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiClient;

namespace WebFramework.OrderService
{
    /// <summary>
    /// 订单相关操作
    /// </summary>
    public class OrderMethod
    {

        #region 单例模式

        // 定义一个静态变量来保存类的实例
        private static OrderMethod uniqueInstance;

        // 定义一个标识确保线程同步
        private static readonly object locker = new object();

        /// <summary>
        /// 定义公有方法提供一个全局访问点,同时你也可以定义公有属性来提供全局访问点
        /// </summary>
        /// <returns></returns>
        public static OrderMethod OrderInstance
        {
            get
            {
                // 当第一个线程运行到这里时，此时会对locker对象 "加锁"，
                // 当第二个线程运行该方法时，首先检测到locker对象为"加锁"状态，该线程就会挂起等待第一个线程解锁
                // lock语句运行完之后（即线程运行完之后）会对该对象"解锁"
                // 双重锁定只需要一句判断就可以了
                if (uniqueInstance == null)
                {
                    lock (locker)
                    {
                        // 如果类的实例不存在则创建，否则直接返回
                        if (uniqueInstance == null)
                        {
                            uniqueInstance = new OrderMethod();
                        }
                    }
                }
                return uniqueInstance;
            }
        }
         
        #endregion

        #region 获取JACK资源库
        /// <summary>
        /// 获取JACK资源库
        /// </summary>
        /// <param name="objectId">项目ID</param>
        /// <param name="eventId">资源Id</param>
        /// <param name="mob">手机号</param>
        /// <param name="ordercode">订单号</param>
        /// <param name="ObjectNum">项目编号</param>
        /// <returns></returns>
        public  string GetAPIClient(int objectId, int eventId, string mob, string ordercode, string ObjectNum)
        {
            if (WebFramework.GeneralMethodBase.GetKeyConfig(3).Val.ToLower() == "true")//测试开关
            {
                //ykHost = "demo.eswapi.com";
                //ykkey = "DEMO_Pass_Edrt%Rt";

                return "CS0000001";
            }

            if (WebFramework.GeneralMethodBase.GetKeyConfig(5).Val.ToLower() == "false") //调取资源开关
            {
                return "";
            }

            string ykkey = "";
            string ykHost = "e.eswapi.com";

            YHAPIClient.APIClient client = new YHAPIClient.APIClient(ykkey, objectId, ObjectNum, true);
            client.ApiHost = ykHost;

            var test = client.CallServiceCard(ordercode, eventId, mob);

            if (test.Success)
                return test.Password;
            else
                WebFramework.GeneralMethodBase.SendErroEmail("优酷未调取到串码<br/>ERROR：" + test.Code + ":" + test.Msg + ",ordercode:" + ordercode + ",Mob:" + mob);

            return "";
        }
        #endregion

        #region 获取资源库
        /// <summary>
        /// 获取资源库
        /// </summary>
        /// <param name="objectId">项目ID</param>
        /// <param name="eventId">资源Id</param>
        /// <param name="mob">手机号</param>
        /// <param name="ordercode">订单号</param>
        /// <param name="qty">调取数量</param>
        /// <returns></returns>
        public  string GetYHPsiAPI(int objectId, int eventId, string mob, string ordercode, int qty = 1)
        {
            YHPsiAPI.YHPsiService yhpsi = new YHPsiAPI.YHPsiService();

            yhpsi.SetTestMode = false;

            YHPsiAPI.APIResult ApiResult = null;

            if (WebFramework.GeneralMethodBase.GetKeyConfig(3).Val.ToLower() == "true")//测试开关
            {
                return "CS0000001";
            }

            if (WebFramework.GeneralMethodBase.GetKeyConfig(5).Val.ToLower() == "false") //调取资源开关
            {
                return "";
            }

            ApiResult = yhpsi.GetCommonCode(objectId, ordercode, mob, eventId, qty);

            if (ApiResult.status == "true")
                return ApiResult.data.codes;
            else
                WebFramework.GeneralMethodBase.SendErroEmail("YHPsiService未调取到串码<br/>ERROR：" + ApiResult.data.error + ",ordercode:" + ordercode + ",Mob:" + mob);

            return "";
        }
        #endregion

        #region 资源库新接口 
        /// <summary>
        /// 资源库新接口
        /// </summary>
        /// <param name="objectId">项目ID</param>
        /// <param name="eventId">资源Id</param>
        /// <param name="mob">手机号</param>
        /// <param name="ordercode">订单号</param>
        /// <param name="qty">调取数量</param>
        /// <returns></returns>
        public  string GetEswAPI(int objectId, int eventId, string mob, string ordercode, int qty = 1)
        {

            if (WebFramework.GeneralMethodBase.GetKeyConfig(3).Val.ToLower() == "true")//测试开关
            {
                return "CS0000001";
            }

            if (WebFramework.GeneralMethodBase.GetKeyConfig(5).Val.ToLower() == "false") //调取资源开关
            {
                return "";
            }

            Common.EswAPIHelper.ResultMessage ApiResult = Common.EswAPIHelper.GetStockCode(mob, eventId, objectId, ordercode, qty);

            if (ApiResult.IsSuccess)
                return ApiResult.ResultData.codes;
            else
                WebFramework.GeneralMethodBase.SendErroEmail("EswAPIHelper未调取到串码<br/>ERROR：" + ApiResult.Message + ",ordercode:" + ordercode + ",Mob:" + mob);

            return "";
        }
        #endregion

        #region 检查手机号-openid-IP参与次数是否超限
        public  string CheckMobOpidIp(string mob, string opid, string Ip)
        {
            Db.OrderInfoDal oddal = new Db.OrderInfoDal();

            string ReturnMsg = "";

            #region 验证手机号参与次数是否超限
            if (string.IsNullOrEmpty(mob) == false)
            {
                int mobCount = 0;

                ReturnMsg = WebFramework.GeneralMethodBase.GetKeyConfig(40).Val;

                int mobAllcont = Common.TypeHelper.ObjectToInt(WebFramework.GeneralMethodBase.GetKeyConfig(37).Val, 0);
                if (mobAllcont >= 0)
                {
                    mobCount = oddal.CheckCount(string.Format(" and Mob='{0}'", mob));

                    if (mobCount >= mobAllcont)
                    {
                        return ReturnMsg.Replace("每日", "").Replace("$mob$", mobAllcont.ToString()); ;
                    }
                }

                int mobcont = Common.TypeHelper.ObjectToInt(WebFramework.GeneralMethodBase.GetKeyConfig(34).Val, 0);
                if (mobcont >= 0)
                {
                    mobCount = oddal.CheckCount(string.Format(" and Mob='{0}' and DateStamp='{1}' ", mob, DateTime.Now.ToString("yyyyMMdd")));

                    if (mobCount >= mobcont)
                    {
                        return ReturnMsg.Replace("$mob$", mobcont.ToString()); ;
                    }
                }

            }
            #endregion

            #region 验证Openid参与次数是否超限
            if (string.IsNullOrEmpty(opid) == false)
            {
                int OpenidCount = 0;

                ReturnMsg = WebFramework.GeneralMethodBase.GetKeyConfig(41).Val;

                int opidAllcont = Common.TypeHelper.ObjectToInt(WebFramework.GeneralMethodBase.GetKeyConfig(38).Val, 0);
                if (opidAllcont >= 0)
                {
                    OpenidCount = oddal.CheckCount(string.Format(" and OpenId='{0}'", opid));

                    if (OpenidCount >= opidAllcont)
                    {
                        return ReturnMsg.Replace("每日", "").Replace("$opid$", opidAllcont.ToString()); ;
                    }
                }

                int opidcont = Common.TypeHelper.ObjectToInt(WebFramework.GeneralMethodBase.GetKeyConfig(35).Val, 0);
                if (opidcont >= 0)
                {
                    OpenidCount = oddal.CheckCount(string.Format(" and OpenId='{0}' and DateStamp='{1}' ", opid, DateTime.Now.ToString("yyyyMMdd")));

                    if (OpenidCount >= opidcont)
                    {
                        return ReturnMsg.Replace("$opid$", opidcont.ToString());
                    }
                }



            }
            #endregion

            #region 验证Ip参与次数是否超限
            if (string.IsNullOrEmpty(Ip) == false)
            {
                int IpCount = 0;

                ReturnMsg = WebFramework.GeneralMethodBase.GetKeyConfig(42).Val;

                int ipAllcont = Common.TypeHelper.ObjectToInt(WebFramework.GeneralMethodBase.GetKeyConfig(39).Val, 0);
                if (ipAllcont >= 0)
                {
                    IpCount = oddal.CheckCount(string.Format(" and Ip='{0}'", Ip));

                    if (IpCount >= ipAllcont)
                    {
                        return ReturnMsg;
                    }
                }

                int ipcont = Common.TypeHelper.ObjectToInt(WebFramework.GeneralMethodBase.GetKeyConfig(36).Val, 0);

                if (ipcont >= 0)
                {
                    IpCount = oddal.CheckCount(string.Format(" and Ip='{0}' and DateStamp='{1}' ", Ip, DateTime.Now.ToString("yyyyMMdd")));
                    if (IpCount >= ipcont)
                    {
                        return ReturnMsg;
                    }
                }


            }
            #endregion

            return "";
        }
        #endregion

        #region 大数据记录
        /// <summary>
        /// 大数据记录
        /// </summary>
        /// <param name="model">订单</param>
        /// <returns></returns>
        public  string AddOrderApi(Model.OrderInfoModel model)
        {
            #region 抽红包保存信息
            string temp = "";

            string ActivityNo = WebFramework.GeneralMethodBase.GetKeyConfig(22).Val;

            Task<string> task1 = Task.Factory.StartNew<string>(() =>
            {


                // WriteLog(message);
                try
                {

                    WebApiInterface webapi = new WebApiInterface();
                    Esmart_Totlal_Root customer = new Esmart_Totlal_Root();

                    #region  必填项  项目编号-品牌-openid
                    customer.ActivityNo = ActivityNo;

                    customer.OpenId = model.OpenId;
                    #endregion

                    #region  订单信息
                    customer.OrderCode = model.OrderCode;

                    customer.OrderStatus = 0;
                    customer.GiftName = model.Jp;
                    customer.GiftMoney = model.RedPackMoney.ToString();

                    customer.PrizeCode = model.PrizeCode;//兑换码
                    #endregion

                    #region  客户信息
                    customer.Ip = model.Ip;
                    customer.IpAdds = model.IpAddress;
                    customer.Mob = model.Mob;
                    #endregion

                    temp = webapi.InertActivityReqDetail(customer);

                    if (temp == "")
                    {
                        return "请求服务异常404";

                        //WebDebugLog("请求服务异常404");
                    }
                    else
                    {
                        MessageResponse<bool> obj = JsonConvert.DeserializeObject<MessageResponse<bool>>(temp);
                        if (obj.Code == 0)
                        {
                            return "OK";
                        }
                        else
                        {
                            return obj.Message;
                            //WebDebugLog("请求服务异常");

                        }

                    }
                }
                catch (Exception ex)
                {
                    return "在调用服务前出现异常-" + ex.Message;
                }

            });

            temp = task1.Result;
            return temp;

            #endregion
        }

        #endregion

        #region OCR预录
        /// <summary>
        /// OCR预录
        /// </summary>
        /// <param name="OrderCode">订单号</param>
        /// <param name="Hashdata">签名</param>
        /// <param name="FileName">图片名称</param>
        /// <returns></returns>
        public  string OcrRecorded(string OrderCode, string Hashdata, string FileName)
        {
            #region OCR预录
            string temp = "";

            if (WebFramework.GeneralMethodBase.GetKeyConfig(23).States == 1)
            {
                int limitCount = Common.TypeHelper.ObjectToInt(WebFramework.GeneralMethodBase.GetKeyConfig(24).Val, 0);

                BDImgApi.APIKey.appUserName = WebFramework.GeneralMethodBase.GetKeyConfig(25).Val;
                BDImgApi.APIKey.appKey = WebFramework.GeneralMethodBase.GetKeyConfig(26).Val;
                BDImgApi.APIKey.appSecret = WebFramework.GeneralMethodBase.GetKeyConfig(27).Val;

                Task<string> task1 = Task.Factory.StartNew<string>(() =>
                {
                    // WriteLog(message);
                    try
                    {
                        int RecordedCount = new Db.OrderInfoDal().CheckCount(string.Format(" and IdCard='{0}'", DateTime.Now.ToString("yyyyMMdd")));

                        if (RecordedCount <= limitCount)
                        {
                            BDImgApi.GetApiResult Result = new BDImgApi.GetApiResult();

                            return Result.GetMatchingImg(Hashdata, "1", FileName, OrderCode, false).errMsg;

                        }

                        return "调用已超限";
                    }
                    catch (Exception ex)
                    {
                        return "出现异常-" + ex.Message;
                    }

                });

                temp = task1.Result;
            }

            return temp;

            #endregion
        }

        #endregion

        #region 项目体检
        /// <summary>
        /// 项目体检
        /// </summary>
        /// <returns></returns>
        public  string ObjectPhysical()
        {

            string PhysicalMsg = "";

            #region 检查激活码是否创建多单
            int CodeRepeatOrder = new Db.OrderInfoDal().GetCodeRepeatList("Code", 1, "").Rows.Count;
            if (CodeRepeatOrder > 0)
            {
                PhysicalMsg += "激活码创建多单-";
            }
            #endregion

            #region 检查手机号总数是否超过限制
            int MobAllCont = Common.TypeHelper.ObjectToInt(WebFramework.GeneralMethodBase.GetKeyConfig(37).Val, 0);
            if (MobAllCont >= 0)
            {
                int MobBeyondOrder = new Db.OrderInfoDal().GetCodeRepeatList("Mob", MobAllCont, "").Rows.Count;
                if (MobBeyondOrder > 0)
                {
                    PhysicalMsg += "手机号总数超过限制总数-";
                }
            }
            #endregion

            #region 检查手机号每日总数是否超过限制
            int MobDayCont = Common.TypeHelper.ObjectToInt(WebFramework.GeneralMethodBase.GetKeyConfig(34).Val, 0);
            if (MobDayCont >= 0)
            {
                int MobBeyondOrder = new Db.OrderInfoDal().GetCodeRepeatList("Mob", MobDayCont, "and [DateStamp]='" + DateTime.Now.ToString("yyyyMMdd") + "'").Rows.Count;
                if (MobBeyondOrder > 0)
                {
                    PhysicalMsg += "手机号每日总数超过限制总数-";
                }
            }
            #endregion

            #region 检查OpenId总数是否超过限制
            int OpenIdAllCont = Common.TypeHelper.ObjectToInt(WebFramework.GeneralMethodBase.GetKeyConfig(38).Val, 0);
            if (OpenIdAllCont >= 0)
            {
                int OpenIdBeyondOrder = new Db.OrderInfoDal().GetCodeRepeatList("OpenId", OpenIdAllCont, "").Rows.Count;
                if (OpenIdBeyondOrder > 0)
                {
                    PhysicalMsg += "OpenId总数超过限制总数-";
                }
            }
            #endregion

            #region 检查OpenId每日总数是否超过限制
            int OpenIdDayCont = Common.TypeHelper.ObjectToInt(WebFramework.GeneralMethodBase.GetKeyConfig(35).Val, 0);
            if (OpenIdDayCont >= 0)
            {
                int OpenIdBeyondOrder = new Db.OrderInfoDal().GetCodeRepeatList("OpenId", OpenIdDayCont, "and [DateStamp]='" + DateTime.Now.ToString("yyyyMMdd") + "'").Rows.Count;
                if (OpenIdBeyondOrder > 0)
                {
                    PhysicalMsg += "OpenId每日总数超过限制总数-";
                }
            }
            #endregion

            #region 检查IP总数是否超过限制
            int IPAllCont = Common.TypeHelper.ObjectToInt(WebFramework.GeneralMethodBase.GetKeyConfig(39).Val, 0);
            if (IPAllCont >= 0)
            {
                int IPBeyondOrder = new Db.OrderInfoDal().GetCodeRepeatList("IP", IPAllCont, "").Rows.Count;
                if (IPBeyondOrder > 0)
                {
                    PhysicalMsg += "IP总数超过限制总数-";
                }
            }
            #endregion

            #region 检查IP每日总数是否超过限制
            int IPDayCont = Common.TypeHelper.ObjectToInt(WebFramework.GeneralMethodBase.GetKeyConfig(36).Val, 0);
            if (IPDayCont >= 0)
            {
                int IPBeyondOrder = new Db.OrderInfoDal().GetCodeRepeatList("IP", IPDayCont, "and [DateStamp]='" + DateTime.Now.ToString("yyyyMMdd") + "'").Rows.Count;
                if (IPBeyondOrder > 0)
                {
                    PhysicalMsg += "IP每日总数超过限制总数-";
                }
            }
            #endregion

            PhysicalMsg = PhysicalMsg.TrimEnd('-');

            if (string.IsNullOrEmpty(PhysicalMsg) == false)
            {
                WebFramework.GeneralMethodBase.SendErroEmail(PhysicalMsg);
            }

            return string.IsNullOrEmpty(PhysicalMsg) ? "项目正常" : PhysicalMsg;

        }

        #endregion


    }
}
