using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebFramework.SessionManage
{

    public class SessionMethod
    {
        private readonly string SessionName = "YH_Session_Web";

        #region 单例模式

        // 定义一个静态变量来保存类的实例
        private static SessionMethod uniqueInstance;

        // 定义一个标识确保线程同步
        private static readonly object locker = new object();

        /// <summary>
        /// 定义公有方法提供一个全局访问点,同时你也可以定义公有属性来提供全局访问点
        /// </summary>
        /// <returns></returns>
        public static SessionMethod SessionInstance
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
                            uniqueInstance = new SessionMethod();
                        }
                    }
                }
                return uniqueInstance;
            }
        }

        #endregion
          
        #region 存取订单Session
        public  void SetSession(Model.SessionModel orderSession)
        {
            Model.SessionModel OrderUser = new Model.SessionModel();
            OrderUser.Id = orderSession.Id;
            OrderUser.Code = orderSession.Code;
            OrderUser.OpenId = orderSession.OpenId;
            OrderUser.Mob = orderSession.Mob;
            OrderUser.OrderKey = orderSession.OrderKey;
            OrderUser.States = orderSession.States;
            OrderUser.Nickname = orderSession.Nickname;
            OrderUser.HeadImgurl = orderSession.HeadImgurl;
            OrderUser.FileId = orderSession.FileId;
            OrderUser.FileName = orderSession.FileName;
            OrderUser.dt = Common.Des.Encode(System.Web.HttpUtility.HtmlEncode(OrderUser.Id + OrderUser.Code + OrderUser.OpenId + OrderUser.Mob + OrderUser.OrderKey + OrderUser.States + OrderUser.Nickname + OrderUser.HeadImgurl + OrderUser.FileId + OrderUser.FileName));
            HttpContext.Current.Session[SessionName] = OrderUser;
        }

        public  void SetSession(string mob, string opid, string code)
        {
            Model.SessionModel OrderUser = new Model.SessionModel();
            OrderUser.Id = 0;
            OrderUser.Code = code;
            OrderUser.OpenId = opid;
            OrderUser.Mob = mob;
            OrderUser.OrderKey = "";
            OrderUser.States = 0;
            OrderUser.Nickname = "";
            OrderUser.HeadImgurl = "";
            OrderUser.FileId = 0;
            OrderUser.FileName = "";
            OrderUser.dt = Common.Des.Encode(System.Web.HttpUtility.HtmlEncode(OrderUser.Id + OrderUser.Code + OrderUser.OpenId + OrderUser.Mob + OrderUser.OrderKey + OrderUser.States + OrderUser.Nickname + OrderUser.HeadImgurl + OrderUser.FileId + OrderUser.FileName));
            HttpContext.Current.Session[SessionName] = OrderUser;
        }

        public  void SetSession(string Nickname, string opid, string HeadImgurl, string code)
        {
            Model.SessionModel OrderUser = new Model.SessionModel();
            OrderUser.Id = 0;
            OrderUser.Code = code;
            OrderUser.OpenId = opid;
            OrderUser.Mob = "";
            OrderUser.OrderKey = "";
            OrderUser.States = 0;
            OrderUser.Nickname = Nickname;
            OrderUser.HeadImgurl = HeadImgurl;
            OrderUser.FileId = 0;
            OrderUser.FileName = "";
            OrderUser.dt = Common.Des.Encode(System.Web.HttpUtility.HtmlEncode(OrderUser.Id + OrderUser.Code + OrderUser.OpenId + OrderUser.Mob + OrderUser.OrderKey + OrderUser.States + OrderUser.Nickname + OrderUser.HeadImgurl + OrderUser.FileId + OrderUser.FileName));
            HttpContext.Current.Session[SessionName] = OrderUser;
        }

        public  void SetSession(string mob, string opid, string code, int id)
        {
            Model.SessionModel OrderUser = new Model.SessionModel();
            OrderUser.Id = id;
            OrderUser.Code = code;
            OrderUser.OpenId = opid;
            OrderUser.Mob = mob;
            OrderUser.OrderKey = "";
            OrderUser.States = 0;
            OrderUser.Nickname = "";
            OrderUser.HeadImgurl = "";
            OrderUser.FileId = 0;
            OrderUser.FileName = "";
            OrderUser.dt = Common.Des.Encode(System.Web.HttpUtility.HtmlEncode(OrderUser.Id + OrderUser.Code + OrderUser.OpenId + OrderUser.Mob + OrderUser.OrderKey + OrderUser.States + OrderUser.Nickname + OrderUser.HeadImgurl + OrderUser.FileId + OrderUser.FileName));
            HttpContext.Current.Session[SessionName] = OrderUser;
        }

        public  Model.SessionModel GetSession()
        {
            Model.SessionModel OrderUser = new Model.SessionModel();
            if (HttpContext.Current.Session[SessionName] == null)
            {
                return null;
            }
            OrderUser = (Model.SessionModel)HttpContext.Current.Session[SessionName];
            if (System.Web.HttpUtility.UrlDecode(Common.Des.Decode(OrderUser.dt)).Length <= 0)
            {
                return null;
            }
            //LogTool.LogCommon.WebFramework.GeneralMethodBase.WebDebugLog(System.Web.HttpUtility.UrlDecode(Common.Des.Decode(OrderUser.dt)));
            return OrderUser;
        }
        #endregion



    }
}
