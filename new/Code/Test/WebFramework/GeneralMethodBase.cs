using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Configuration;
using Esmart.Crm.Interface.Vip; 
using Esmart.Framework.Model;
using Newtonsoft.Json;
using WebApiClient;
using System.Collections;
using System.IO;

namespace WebFramework
{
    public static class GeneralMethodBase
    { 
         
        #region 验证客户端
        public static bool IsMoblie()
        {
            string agent = (HttpContext.Current.Request.UserAgent + "").ToLower().Trim();

            if (agent == "" ||
                agent.IndexOf("mobile") != -1 ||
                agent.IndexOf("mobi") != -1 ||
                agent.IndexOf("nokia") != -1 ||
                agent.IndexOf("samsung") != -1 ||
                agent.IndexOf("sonyericsson") != -1 ||
                agent.IndexOf("mot") != -1 ||
                agent.IndexOf("blackberry") != -1 ||
                agent.IndexOf("lg") != -1 ||
                agent.IndexOf("htc") != -1 ||
                agent.IndexOf("j2me") != -1 ||
                agent.IndexOf("ucweb") != -1 ||
                agent.IndexOf("opera mini") != -1 ||
                agent.IndexOf("mobi") != -1 ||
                agent.IndexOf("android") != -1 ||
                agent.IndexOf("iphone") != -1)
            {
                //终端可能是手机
                return true;

            }
            return false;
        }
        #endregion

        #region 跳转活动结束页面
        public static void End()
        {
            HttpContext.Current.Response.Redirect("~/Error.aspx?type=QSFSVHY25E8ANUJEAY7792EJSHIUAB887E" + DateTime.Now.ToString("yyyyMMddhhmmssfff"));
            HttpContext.Current.Response.End();
            return;
        }
        #endregion

        #region 跳转错误页面
        public static void Ero()
        {
            HttpContext.Current.Response.Redirect("~/Error.aspx?type=K190IL782HKGUNB26URNMKNRQWSR43" + DateTime.Now.ToString("yyyyMMddhhmmssfff"));
            HttpContext.Current.Response.End();
            return;
        }
        #endregion

        #region 获取奖项
        /// <summary>
        /// 获取奖项（最多7个）
        /// </summary>
        /// <param name="PrizeCont">奖项总个数（最多7个）</param>
        /// <param name="AwardsType">抽奖类型 1：常规奖 2：红包奖</param>
        /// <returns>奖项</returns> 
        public static Model.AwardsStatisticsModel GetPrize(int PrizeCont, int AwardsType)
        {
            if (PrizeCont > 7) { return null; }

            Db.AwardsStatisticsDal AwardsDal = new Db.AwardsStatisticsDal();
            Db.ZpConfigDal dal = new Db.ZpConfigDal();
            Model.ZpConfigModel m = dal.GetModel(AwardsType);

            #region 获取所有奖项设置值

            #region 奖项每天总数
            int[] PrizeDayCont = new int[] { m.Zjl7, m.Zjl9, m.Zjl11, m.Zjl13, m.Zjl15, m.Zjl17 };
            #endregion

            #region 奖项总数
            int[] PrizeAllCont = new int[] { m.Zjl8, m.Zjl10, m.Zjl12, m.Zjl14, m.Zjl16, m.Zjl18 };
            #endregion

            #region 奖项预设中奖率
            
            int[] PresetProbability = new int[] { m.Zjl1, m.Zjl2, m.Zjl3, m.Zjl4, m.Zjl5, m.Zjl6 };
            #endregion

            #region 奖项实际中奖率
            int[] ActualProbability = new int[] { 0, 0, 0, 0, 0, 0 };
            #endregion
             
            #endregion

            #region 获取奖项统计参与详细 
            List<Model.AwardsStatisticsModel> AwardsList = AwardsDal.GetModelList(string.Format(" and AwardsType={0} order by id", AwardsType));
            #endregion

            #region 获取奖项实际参与量与预设奖项总量比较后的中奖率
            for (int i = 0; i < PrizeCont - 1; i++)
            {
                ActualProbability[i] = PresetProbability[i];

                Model.AwardsStatisticsModel model = AwardsList.Where(w => w.AwardsId == i + 1).FirstOrDefault();//获取奖项参与详情
                if (model == null) { throw new ArgumentNullException("获取奖项失败"); }

                #region 如果当前奖项日期不等于当天，则每日总数为0
                if (model.DateStamp != DateTime.Now.ToShortDateString())
                { 
                    model.TodayTotal = 0; 
                }
                #endregion

                if (model.TodayTotal >= PrizeDayCont[i] || model.AllTotal - model.BackTotal >= PrizeAllCont[i])
                {
                    ActualProbability[i] = 0;
                }
            }
            #endregion

            #region 抽奖
            int seven = 100 - ActualProbability[0] - ActualProbability[1] - ActualProbability[2] - ActualProbability[3] - ActualProbability[4] - ActualProbability[5];
            Common.Prize prize = new Common.Prize(ActualProbability[0], ActualProbability[1], ActualProbability[2], ActualProbability[3], ActualProbability[4], ActualProbability[5], seven);
            #endregion

            #region 赋值中奖详情-奖项-角度-奖品名称-奖项Id
            string[] PrizeModel = prize.GetPrize();

            Model.AwardsStatisticsModel AwardsPrizeModel = AwardsList.Where(s => s.AwardsName == PrizeModel[0]).FirstOrDefault();
            if (AwardsPrizeModel == null) { throw new ArgumentNullException("获取奖项失败"); }

            AwardsPrizeModel.Angle = PrizeModel[1];//角度 
            #endregion

            return AwardsPrizeModel;
        }
        #endregion

        #region 发送用户领取异常提醒邮件
        public static void SendErroEmail(string text, string emailto = "")
        {
            Common.Email.EmailTool.sendEmail(System.Configuration.ConfigurationManager.AppSettings["LogErrorEmailTo"] + ";" + emailto, GetHost(), text, "");
        }
        #endregion

        #region 生成订单号（项目名+日期）
        public static string CreateOrderCode(int guid)
        {
            return GetHost().ToUpper() + DateTime.Now.ToString("yyyyMMddHHmmss") + guid;
        }
        #endregion

        #region 生成唯一订单key（项目名+日期+oid+7位字母数字随机）
        public static string CreateOrderKeyCode(int goid)
        {
            return Common.getMD5.MD5((GetHost().ToUpper() + DateTime.Now.ToString("yyyyMMddHHmmss") + goid + GetZmNum(7))).ToUpper();
        }
        #endregion
          
        #region 创建唯一ID
        public static int GenerationOrderId(int num)
        {
            return Db.GenerationOrderIdDal.GetOrderNumber(num);
        }
        #endregion

        #region 创建微信红包订单号

        public static string CreateHbCode(int goid)
        {
            int hid = Common.TypeHelper.ObjectToInt(GetKeyConfig(29).Val, 0);

            if (hid == 0) { throw new ArgumentNullException("未填写微信红包编号"); }

            string codes = DateTime.Now.ToString("yyyyMMdd") + hid + goid;
            return codes;
        }
        #endregion

        #region 创建随机数（0-9）
        private static string GetCodeNum(int num)
        {
            string a = "0123456789";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < num; i++)
            {
                sb.Append(a[new Random(Guid.NewGuid().GetHashCode()).Next(0, a.Length - 1)]);
            }

            return sb.ToString();
        }
        #endregion

        #region 创建随机数（0-9A-Z）
        private static string GetZmNum(int num)
        {
            string a = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < num; i++)
            {
                sb.Append(a[new Random(Guid.NewGuid().GetHashCode()).Next(0, a.Length - 1)]);
            }

            return sb.ToString();
        }
        #endregion

        #region showbox
        public static string showbox(string text, int ty)
        {
            return "<script>showbox(" + ty + ",'" + text + "')</script>";
        }
        #endregion
         
        #region 检查上下线时间
        public static string CheckStartOrEnd()
        {
            if (GetKeyConfig(3).States == 1)
            {
                return showbox(GetKeyConfig(43).Val, 1);
            }
            var _starTime = DateTime.Parse(GetKeyConfig(8).Val);

            if (Common.DateTimeHelper.IsGreaterDateTime(_starTime.ToString()))
            {
                return showbox("活动将于<br>" + _starTime.ToString() + "<br>开始，敬请期待！", 4);
            }
            if (Common.DateTimeHelper.IsLessDateTime(DateTime.Parse(GetKeyConfig(9).Val).ToString()))
            {
                End();
            }

            return "";
        }
        #endregion

        #region 获取配置

        public static Model.ConfigModel GetKeyConfig(int key)
        {
            var objectdel = CacheBase.FindCacheObjectInfo;

            Model.ConfigModel model = objectdel.Where(w => w.KId == key).FirstOrDefault();

            if (model == null) { throw new ArgumentNullException("key不存在"); }

            return model;
        }


        #endregion
         
        #region 输入日志
        public static void WebDebugLog(string LockValue, string FailureReason, HttpContext context = null)
        {
            //ILogger log = new Logger();

            StackTrace trace = new StackTrace();
            StackFrame frame = trace.GetFrame(1);
            MethodBase method = frame.GetMethod();
            string className = method.ReflectedType.Name;
            MethodBase methodName = trace.GetFrame(1).GetMethod();

            if (string.IsNullOrEmpty(LockValue) == false)
            {
                string InputStream = "";

                if (context != null)
                {
                    using (StreamReader sr = new StreamReader(context.Request.InputStream))
                    {
                        InputStream = sr.ReadLine();
                    }
                }

                SaveBehaviorLog(new Model.BehaviorLogModel()
                {
                    CreateTime = DateTime.Now,
                    BehaviorType = string.IsNullOrEmpty(InputStream) ? 2 : 1,
                    FailureReason = FailureReason,
                    Ip = Common.ClientIP.GetIp(),
                    LockValue = LockValue,
                    Remark = InputStream
                });
            }
            Common.LogCommon.WebDebugLog(className + "--" + methodName.Name + "   " + LockValue + "：" + FailureReason);
        }
        #endregion

        #region 获取根域名
        public static string GetHost()
        {
            string Urlhost = HttpContext.Current.Request.Url.Host;
            if (Urlhost.Split('.').Length < 3)
            {
                return "localhost";
            }
            return Urlhost.Split('.')[0];
        }

        #endregion

        #region 系统维护提示
        public static string IsCheckWH()
        {
            if (GetKeyConfig(4).States == 1)
            {
                string wh_Satr=GetKeyConfig(44).Val;
                string wh_End=GetKeyConfig(45).Val;

                if (wh_Satr == "0" && wh_End == "0") {
                    return showbox("系统维护中", 2);
                }

                DateTime dt_n = DateTime.Now;
                DateTime startDate = DateTime.Parse(dt_n.ToString("yyyy-MM-dd ") + wh_Satr);//上午
                DateTime endDate = DateTime.Parse(dt_n.ToString("yyyy-MM-dd ") + wh_End);//下午
                if (dt_n < startDate || dt_n > endDate)
                {
                    return showbox(GetKeyConfig(46).Val, 2);
                }
            }
            return "";
        }
        #endregion
         
        #region 发送短信
        public static string GetMsg(int MsgId, string mob = "", string ordercode = "")
        {
            Db.MsgConfigDal msgdal = new Db.MsgConfigDal();
            Model.MsgConfigModel msgdel = msgdal.GetModel(MsgId);

            if (string.IsNullOrEmpty(msgdel.MsgTemp))
                msgdel.MsgTemp = "";

            if (string.IsNullOrEmpty(mob) == false && string.IsNullOrEmpty(ordercode) == false && msgdel.MsgTemp.Length > 10)
            {
                Common.MessageApi.SendMessage(msgdel.MsgTemp, mob,msgdel.MsgType, msgdel.SupplierId
                    , Common.TypeHelper.ObjectToInt(GetKeyConfig(20).Val, 0), GetKeyConfig(21).Val);

                AddLog(0, mob, ordercode, msgdel.MsgTemp);

            }
            return msgdel.MsgTemp;
        }
        #endregion

        #region 添加日志
        public static void AddLog(int oid, string mob, string ordercode, string note)
        {

            Model.OrderLogModel mdlog = new Model.OrderLogModel();
            mdlog.OId = oid;
            mdlog.OrderCode = ordercode;
            mdlog.Mob = mob;
            mdlog.UpTime = DateTime.Now;
            mdlog.LStatus = 0;
            mdlog.Status = 1;
            mdlog.Notes = note;

            new Db.OrderLogDal().Add(mdlog);

        }
        #endregion

        #region 加载默认Js
        public static string LoadJs()
        {

            var date = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            var ShareHost = HttpContext.Current.Request.Url.Host.Contains("ysea") ? "wxmarspifac2f1" : HttpContext.Current.Request.Url.Host.Contains("iseedling") ? "wxshareapi2" : "wxshareapicom";

            string js = "";

            js += "<title>" + GetKeyConfig(10).Val + "</title>";
            js += "<script src=\"http://apps.bdimg.com/libs/jquery/1.9.1/jquery.min.js\"></script>";
            js += "<script src=\"http://res.wx.qq.com/open/js/jweixin-1.0.0.js\" type=\"text/javascript\"></script>";
            js += "<script src=\"http://" + ShareHost + ".esmartwave.com/wxshare.aspx?t=" + date + "\"></script>";
            js += "<script src=\"../js/weixin.js?t=" + date + "\"></script>";
            js += "<script>";
            var BdTongji = GetKeyConfig(17).Val;
            if (BdTongji.Length > 0)
            {
                js += "var _hmt = _hmt || [];";
                js += "(function () {";
                js += "var hm = document.createElement(\"script\");";
                js += "hm.src =\"" + BdTongji + "\";";
                js += "var s = document.getElementsByTagName(\"script\")[0];";
                js += "s.parentNode.insertBefore(hm, s);";
                js += "})();";
            }
            js += "GameContent.title=\"" + GetKeyConfig(12).Val + "\";";
            js += "GameContent.friend=\"" + GetKeyConfig(13).Val + "\";";
            js += "GameContent.isshare=" + GetKeyConfig(14).Val + ";";
            js += "</script>";
            js += "<link href=\"../css/global.css?t=" + date + "\" rel=\"stylesheet\" />";
            js += "<link href=\"../css/index.css?t=" + date + "\" rel=\"stylesheet\" />";
            return js;
        }
        #endregion
         
        #region 获取后台用户登录信息
        public static Model.UserInfoModel GetUserSession()
        {
            Model.UserInfoModel OrderUser = new Model.UserInfoModel();
            if (HttpContext.Current.Session["UserSysSession"] == null)
            {
                return null;
            }
            OrderUser = (Model.UserInfoModel)HttpContext.Current.Session["UserSysSession"];
            if (System.Web.HttpUtility.UrlDecode(Common.Des.Decode(OrderUser.RealName)).Length <= 0)
            {
                return null;
            }
            return OrderUser;
        }
        #endregion

        #region 记录用户行为日志
        public static void SaveBehaviorLog(Model.BehaviorLogModel model)
        {

            try
            {
                new Db.BehaviorLogDal().Add(model);
            }
            catch
            {
                string ErrMessage = getProperties(model);
                Common.LogCommon.WriteFileLog(ErrMessage, "BehaviorLog");
            }

        }

        #endregion 

        #region 遍历获取类的属性及属性的值

        public static string getProperties<T>(T t)
        {
            string tStr = string.Empty;
            if (t == null)
            {
                return tStr;
            }
            System.Reflection.PropertyInfo[] properties = t.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

            if (properties.Length <= 0)
            {
                return tStr;
            }
            foreach (System.Reflection.PropertyInfo item in properties)
            {
                string name = item.Name;
                object value = item.GetValue(t, null);
                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                {
                    tStr += string.Format("{0}:{1},", name, value);
                }
                else
                {
                    getProperties(value);
                }
            }
            return tStr;
        }

        #endregion

        #region 获取上传图片Url

        public static string GetUploadImgURL
        {
            get {

                string UploadImgURL = "http://oss.esurl.cn/api/Images/Upload?{0}";

                /// <param name="sign">签名(签名算法: 项目编号+开发人员用户名+开发人员唯一标识 )</param>
                /// <param name="pnum">项目编号</param>
                /// <param name="uid">开发人员用户名</param>
                /// <param name="areas">区域参数(逗号分隔的字符串: 国家,省份,城市,区县,地址)</param>
                /// <param name="watermarkDesc">水印文本(当是否打水印参数为1时使用该文本,若该文本为空,默认打当前时间的水印)</param>
                /// <param name="hpfb">图片流</param>
                /// <param name="isWatermark">是否打水印(0:不打水印,1:打水印)</param>
                /// <param name="watermarkPosition">水印位置:(1:左上,2:右上,3:左下,4:右下)</param>
                /// <param name="isRetainOriginPic">是否保留原图(1:默认值(表示保留原图),0:不保留)</param>
                /// <param name="zipRate">压缩比例(-1:不压缩,0:调用默认压缩比,其它值:按照传入的参数进行压缩)</param>
                /// <param name="degrees">旋转度数(0:不旋转,其他值:按照传入的度数进行旋转)</param>
                /// <param name="longitude">gps经度</param>
                /// <param name="latitude">gps纬度</param>

                Common.Data Dictionary = new Common.Data();

                Dictionary.SetValue("pnum", GetKeyConfig(22).Val);//GetKeyConfig(22).Val
                Dictionary.SetValue("uid", GetKeyConfig(32).Val);//GetKeyConfig(32).Val
                Dictionary.SetValue("key", GetKeyConfig(33).Val);//GetKeyConfig(33).Val
                Dictionary.SetValue("sign", Dictionary.MakeSign());
                //Dictionary.SetValue("areas", "");
                //Dictionary.SetValue("watermarkDesc", "");
                //Dictionary.SetValue("hpfb", "");
                //Dictionary.SetValue("isWatermark", "");
                //Dictionary.SetValue("watermarkPosition", "");
                //Dictionary.SetValue("isRetainOriginPic", "");
                //Dictionary.SetValue("zipRate", "");
                //Dictionary.SetValue("degrees", "");
                //Dictionary.SetValue("longitude", "");
                //Dictionary.SetValue("latitude", ""); 

                return string.Format(UploadImgURL, Dictionary.ToUrl());
            }
        }



        #endregion

    }
}
