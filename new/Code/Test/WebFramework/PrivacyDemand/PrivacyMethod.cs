using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFramework.ESLog;

namespace WebFramework.PrivacyDemand
{
    /// <summary>
    /// 隐私方法
    /// </summary>
    public class PrivacyMethod
    {
        Db.DelayedAwardsDal DelayedAwardsDal = new Db.DelayedAwardsDal();

        #region 单例模式

        // 定义一个静态变量来保存类的实例
        private static PrivacyMethod uniqueInstance;

        // 定义一个标识确保线程同步
        private static readonly object locker = new object();

        /// <summary>
        /// 定义公有方法提供一个全局访问点,同时你也可以定义公有属性来提供全局访问点
        /// </summary>
        /// <returns></returns>
        public static PrivacyMethod PrivacyInstance
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
                            uniqueInstance = new PrivacyMethod();
                        }
                    }
                }
                return uniqueInstance;
            }
        }

        #endregion
          

        #region 添加延时订单
        /// <summary>
        /// 添加延时订单
        /// </summary>
        /// <param name="OrderId">订单ID</param>
        /// <returns></returns>
        public Boolean DelayedMethod(int OrderId) {

            int DelayedTime = Common.TypeHelper.StringToInt(WebFramework.GeneralMethodBase.GetKeyConfig(49).Val);

            if (DelayedTime > 0)
            {
                try
                {

                    Model.DelayedAwardsModel DelayedAwardsModel = new Model.DelayedAwardsModel();
                    DelayedAwardsModel.OrderId = OrderId;
                    DelayedAwardsModel.CreateTime = DateTime.Now;
                    DelayedAwardsModel.DelayedTime = DateTime.Now.AddMinutes(DelayedTime);

                    if (DelayedAwardsDal.Add(DelayedAwardsModel) <= 0)
                    {
                        WebFramework.GeneralMethodBase.SendErroEmail(OrderId + ":添加延时订单失败");
                    }
                    ESLogMethod.ESLogInstance.Error("添加延时订单失败", OrderId.ToString());
                }
                catch (Exception ex) {

                    ESLogMethod.ESLogInstance.Error("添加延时订单失败", OrderId.ToString(), ex);
                }
                 
                return true;
            }

            return false;
        }

        #endregion

        #region 添加虚拟订单
        /// <summary>
        /// 添加虚拟订单
        /// </summary>
        /// <param name="model">订单</param>
        public void AddFictitiousOrder(Model.OrderInfoModel model)
        {
            //虚拟比例
            var Fictitious = Common.TypeHelper.StringToInt(WebFramework.GeneralMethodBase.GetKeyConfig(51).Val);

            if (Fictitious > 0)
            {

                var task = Task.Factory.StartNew(() =>
                {

                    Common.Prize prize = new Common.Prize(Fictitious, 0, 0, 0, 0, 0, 100 - Fictitious);

                    //奖项为一等奖时添加虚拟订单
                    if (prize.GetPrize()[0] == "参与奖")
                    {
                        return;
                    }

                    Model.FictitiousOrderModel FictitiousModel = new Model.FictitiousOrderModel();
                    FictitiousModel.OrderCode = model.OrderCode;
                    FictitiousModel.Jx = model.Jx;
                    FictitiousModel.Jp = model.Jp;
                    FictitiousModel.DateStamp = model.DateStamp;
                    FictitiousModel.Ip = model.Ip;
                    FictitiousModel.IpAddress = model.IpAddress;
                    FictitiousModel.CreateTime = model.CreateTime;
                    FictitiousModel.OpenId = model.OpenId;
                    FictitiousModel.Mob = model.Mob;
                    FictitiousModel.Code = model.Code;
                    FictitiousModel.States = model.States;
                    FictitiousModel.HbOrderCode = model.HbOrderCode;
                    FictitiousModel.RedPackMoney = model.RedPackMoney;

                    new Db.FictitiousOrderDal().Add(FictitiousModel);
                });


            }
        }
        #endregion

        #region 奖品发放节流
        /// <summary>
        /// 奖品发放节流
        /// </summary> 
        public bool Throttling()
        {
            //节流比例
            var throttling = Common.TypeHelper.StringToInt(WebFramework.GeneralMethodBase.GetKeyConfig(50).Val);

            if (throttling > 0)
            {
                Common.Prize prize = new Common.Prize(throttling, 0, 0, 0, 0, 0, 100 - throttling);

                if (prize.GetPrize()[0] == "一等奖")
                {
                    return true;
                }
            }

            return false;
        }
        #endregion

    }
}
