using System;
using System.Collections.Generic;
using System.Threading;

namespace WebFramework
{

    /// <summary>
    /// 事件管理类
    /// </summary>
    public class BMAEvent
    {
        private static Db.DelayedAwardsDal delayedAwardsDal = new Db.DelayedAwardsDal();
        private static Timer _timer;//定时器

        static BMAEvent()
        {
             
        }

        public static void restEvent(){

            if (_timer != null)
            {
                _timer.Dispose();
            }

            //延时时间  以分钟为单位
            int DelayedTime = Common.TypeHelper.StringToInt(WebFramework.GeneralMethodBase.GetKeyConfig(51).Val);
             
            if (DelayedTime > 0)
            { 
                //Common.LogCommon.WriteFileLog(DelayedTime.ToString(), "Processor");

                //1、第一个参数是要执行的方法
                //2、第二个参数是回调方法要使用的对象信息；
                //3、第三个参数是延时启动的时间间隔，“0”表示立即启动；
                //4、第四个参数是Timer的Interval。

                _timer = new Timer(new TimerCallback(Processor), FindParameter, 0, DelayedTime * 60000);
            }
        }

        /// <summary>
        /// 此方法为空，只是起到激活restEvent事件处理机制的作用
        /// </summary>
        public static void Start() { restEvent(); }
         
        /// <summary>
        /// 事件处理程序
        /// </summary>
        /// <param name="state">参数对象</param>
        private static void Processor(object state)
        {
            try
            {
                PrizeParameter Parameter = (PrizeParameter)state;
                 
                List<Model.DelayedAwardsModel> delayedAwardsModelList = delayedAwardsDal.GetModelList(500, string.Format(" and StatusId=0 and DelayedTime<=getdate()"));

                if (delayedAwardsModelList.Count == 0)
                    return;

                #region 修改订单状态为发送中
                if (delayedAwardsDal.Update(delayedAwardsModelList) <= 0)
                    return;
                #endregion

                //循环执行每个事件
                foreach (Model.DelayedAwardsModel model in delayedAwardsModelList)
                {
                    //如果订单已执行
                    if (model.StatusId != 0)
                        continue;

                    ExecuteMethod(model, Parameter);

                }
            }
            catch (Exception ex) {
                Common.LogCommon.WriteFileLog(ex.ToString(), "DelayedError");
            } 
        }

        /// <summary>
        /// 执行指定方法
        /// </summary>
        private static void ExecuteMethod(Model.DelayedAwardsModel DelayedModel, PrizeParameter Parameter)
        {
            try
            {
                if (DelayedModel.Id > 0 && DelayedModel.StatusId == 0)
                {
                    Model.OrderInfoModel OrderModel = new Db.OrderInfoDal().GetModel(DelayedModel.OrderId);
                    if (OrderModel.Id > 0)
                    {
                        //发放奖品
                        SendPrize(DelayedModel, OrderModel, Parameter); 
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Email.EmailTool.sendEmail(System.Configuration.ConfigurationManager.AppSettings["LogErrorEmailTo"], "延时发奖异常", ex.ToString(), "");
            }

        }

        /// <summary>
        /// 发放奖品
        /// </summary>
        /// <param name="OrderModel">订单</param>
        /// <returns></returns>
        private static void SendPrize(Model.DelayedAwardsModel DelayedModel, Model.OrderInfoModel OrderModel, PrizeParameter Parameter)
        {
            //具体发奖逻辑根据项目来


            Common.RedPackHelper rp = new Common.RedPackHelper();

            int moeny = OrderModel.RedPackMoney;

            if (Parameter.IsTest == 1)
            {
                moeny = 100;
            }

            if (Parameter.RedSwitch == 0) //红包开关
            {
                return ;
            }

            Common.RedPackHelper.result result = rp.send(Parameter.acid, Parameter.acid, OrderModel.OpenId, OrderModel.HbOrderCode, moeny, Parameter.ckey, Parameter.hkey);

            if (result.SendStatus)
            {
                DelayedModel.StatusId = 1;
                OrderModel.States = 1;
                OrderModel.IsGrant = 1;
            } 
            else
            {
                DelayedModel.StatusId = -1; 
                OrderModel.IsGrant = -1;
            }

            //执行时间
            DelayedModel.UpdateTime = DateTime.Now; 
            OrderModel.GrantTime = DelayedModel.UpdateTime;

            delayedAwardsDal.Edit(DelayedModel, OrderModel);

        }

        /// <summary>
        /// 获取发奖配置
        /// </summary>
        private static PrizeParameter FindParameter
        {

            get {

                PrizeParameter Parameter = new PrizeParameter(); 
                Parameter.acid = Common.TypeHelper.ObjectToInt(WebFramework.GeneralMethodBase.GetKeyConfig(29).Val, 0);
                Parameter.ckey = WebFramework.GeneralMethodBase.GetKeyConfig(30).Val;
                Parameter.hkey = WebFramework.GeneralMethodBase.GetKeyConfig(31).Val;
                Parameter.IsTest = WebFramework.GeneralMethodBase.GetKeyConfig(3).States;
                Parameter.ProjectId = Common.TypeHelper.ObjectToInt(WebFramework.GeneralMethodBase.GetKeyConfig(49).Val, 0);
                Parameter.RedSwitch = WebFramework.GeneralMethodBase.GetKeyConfig(6).States;
                Parameter.SercetKey = WebFramework.GeneralMethodBase.GetKeyConfig(50).Val; 

                return Parameter;
            
            }
        
        }

        /// <summary>
        /// 发奖配置
        /// </summary>
        private class PrizeParameter
        {
            public int ProjectId { get; set; }
            public string SercetKey { get; set; }
            public int IsTest { get; set; }
            public int RedSwitch { get; set; }
            public int acid { get; set; }
            public string ckey { get; set; }
            public string hkey { get; set; } 
        }


    }
}
