using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebFramework.Report
{

    /// <summary>
    /// 后台统计报表输出
    /// </summary>
    public class ReportMethod
    {
        #region 单例模式

        // 定义一个静态变量来保存类的实例
        private static ReportMethod uniqueInstance;

        // 定义一个标识确保线程同步
        private static readonly object locker = new object();

        /// <summary>
        /// 定义公有方法提供一个全局访问点,同时你也可以定义公有属性来提供全局访问点
        /// </summary>
        /// <returns></returns>
        public static ReportMethod ReportInstance
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
                            uniqueInstance = new ReportMethod();
                        }
                    }
                }
                return uniqueInstance;
            }
        }

        #endregion
         

        /// <summary>
        /// 获取报表数据主入口
        /// </summary>
        /// <param name="context">请求context</param>
        /// <returns></returns>
        public Model.ReturnValue GetReport(HttpContext context)
        {
            Model.ReturnValue rv = new Model.ReturnValue();

            string GetResult = context.Request["GetResult"];

            switch (GetResult) {

                case "WheaterStatistics":
                    rv = GetWheaterStatistics();
                    break;
                case "PartakeTrend":
                    rv = GetPartakeTrend();
                    break;
                case "WholeCountry":
                    rv = GetWholeCountry();
                    break;
                case "AwardStatistics":
                    rv = GetAwardStatistics();
                    break;
                     
            }
             
            return rv;
        
        }

        /// <summary>
        /// 参与统计
        /// </summary>
        /// <returns></returns>
        public Model.ReturnValue GetWheaterStatistics() {

            Model.ReturnValue rv = new Model.ReturnValue();

            Dictionary<string, List<SeriesData>> Person = new Dictionary<string, List<SeriesData>>();

            Db.OrderInfoDal OrderInfoDal = new Db.OrderInfoDal();
            Db.AwardsStatisticsDal AwardsStatisticsDal = new Db.AwardsStatisticsDal();

            //获取奖项统计订单
            List<Model.AwardsStatisticsModel> AwardsStatisticsList = AwardsStatisticsDal.GetModelList().Where(w => w.AwardsType == 1).ToList();

            #region 昨日

            List<SeriesData> YesterdayData = new List<SeriesData>();
            YesterdayData.Add(new SeriesData() {
                name = "参与人次",
                value = AwardsStatisticsList.Sum(s => s.YesterdayTotal)
            });

            YesterdayData.Add(new SeriesData()
            {
                name = "中奖人次",
                value = AwardsStatisticsList.Where(w => w.AwardsId != 7).ToList().Sum(s => s.YesterdayTotal)
            });

            YesterdayData.Add(new SeriesData()
            {
                name = "发奖人次",
                value = OrderInfoDal.CheckCount(string.Format(" and Jx<>'参与奖' and States=1 and DateStamp='{0}'",DateTime.Now.AddDays(-1).ToString("yyyyMMdd")))
            });

            Person.Add("Data1", YesterdayData);
             
            #endregion

            #region 今日

            List<SeriesData> TodayData = new List<SeriesData>();
            TodayData.Add(new SeriesData()
            {
                name = "参与人次",
                value = AwardsStatisticsList.Sum(s => s.TodayTotal)
            });

            TodayData.Add(new SeriesData()
            {
                name = "中奖人次",
                value = AwardsStatisticsList.Where(w => w.AwardsId != 7).ToList().Sum(s => s.TodayTotal)
            });

            TodayData.Add(new SeriesData()
            {
                name = "发奖人次",
                value = OrderInfoDal.CheckCount(string.Format(" and Jx<>'参与奖' and States=1 and DateStamp='{0}'", DateTime.Now.ToString("yyyyMMdd")))
            });

            Person.Add("Data2", TodayData);

            #endregion

            #region 累计

            List<SeriesData> AllData = new List<SeriesData>();
            AllData.Add(new SeriesData()
            {
                name = "参与人次",
                value = AwardsStatisticsList.Sum(s => s.AllTotal)
            });

            AllData.Add(new SeriesData()
            {
                name = "中奖人次",
                value = AwardsStatisticsList.Where(w => w.AwardsId != 7).ToList().Sum(s => s.AllTotal)
            });

            AllData.Add(new SeriesData()
            {
                name = "发奖人次",
                value = OrderInfoDal.CheckCount(string.Format(" and Jx<>'参与奖' and States=1 "))
            });

            Person.Add("Data3", AllData);

            #endregion

            rv.ObjectValue = Person;
            rv.Success = true;
            rv.ErrMessage = "获取成功";

            return rv;
        }

        /// <summary>
        /// 参与趋势与中奖趋势
        /// </summary>
        /// <returns></returns>
        public Model.ReturnValue GetPartakeTrend()
        {

            Model.ReturnValue rv = new Model.ReturnValue();

            Dictionary<string, string> Person = new Dictionary<string, string>();

            Db.OrderInfoDal OrderInfoDal = new Db.OrderInfoDal();

            #region 参与人次

            string sql = " select a.Hours name,(select COUNT(1) from OrderInfo where DATEPART(hh, CreateTime) = a.hours) as value from hoursinfo a";
            DataTable dt = Db.ConDal.GetList(sql);

            Person.Add("Data1", Common.JsonHelper.DataTableToJson(dt));

            #endregion

            #region 中奖人次

            string sql2 = " select a.Hours name,(select COUNT(1) from OrderInfo where DATEPART(hh, CreateTime) = a.hours and Jx<>'参与奖') as value from hoursinfo a";
            DataTable dt2 = Db.ConDal.GetList(sql2);

            Person.Add("Data2", Common.JsonHelper.DataTableToJson(dt2));

            #endregion 

            rv.ObjectValue = Person;
            rv.Success = true;
            rv.ErrMessage = "获取成功";

            return rv;
        }

        /// <summary>
        /// 全国参与分布图
        /// </summary>
        /// <returns></returns>
        public Model.ReturnValue GetWholeCountry()
        {

            Model.ReturnValue rv = new Model.ReturnValue();

            Dictionary<string, string> Person = new Dictionary<string, string>();

            Db.OrderInfoDal OrderInfoDal = new Db.OrderInfoDal();

            #region IP地归

            string sql = " select right([IpAddress],(charindex('-',[IpAddress])-1)) as name,count([IpAddress]) as value FROM [OrderInfo] group by [IpAddress]";
            DataTable dt = Db.ConDal.GetList(sql);

            Person.Add("Data1", Common.JsonHelper.DataTableToJson(dt));

            #endregion
              
            rv.ObjectValue = Person;
            rv.Success = true;
            rv.ErrMessage = "获取成功";

            return rv;
        }

        /// <summary>
        /// 奖项统计
        /// </summary>
        /// <returns></returns>
        public Model.ReturnValue GetAwardStatistics()
        {

            Model.ReturnValue rv = new Model.ReturnValue();

            Dictionary<string, object> Person = new Dictionary<string, object>();

            Db.OrderInfoDal OrderInfoDal = new Db.OrderInfoDal();
            Db.AwardsStatisticsDal AwardsStatisticsDal = new Db.AwardsStatisticsDal();

            //获取中奖奖项统计订单
            List<Model.AwardsStatisticsModel> AwardsStatisticsList = AwardsStatisticsDal.GetModelList().Where(w => w.AwardsType == 1 && w.AwardsId != 7).ToList();

            #region 中奖人数

            List<SeriesData> DrawData = new List<SeriesData>();

            foreach (var model in AwardsStatisticsList) {
                DrawData.Add(new SeriesData()
                {
                    name = model.AwardsName,
                    value = model.AllTotal
                });
            } 

            Person.Add("Data1", DrawData);

            #endregion

            #region 发奖人数

            string sql = " select a.AwardsName name,(select COUNT(1) from [OrderInfo] where Jx=a.AwardsName) as value from AwardsStatistics a where  AwardsType = 1 and AwardsId <> 7";
            //string sql = "select jx as name,count(jx) as value FROM [OrderInfo] group by jx";
            DataTable dt = Db.ConDal.GetList(sql);

            Person.Add("Data2", Common.JsonHelper.DataTableToJson(dt));

            #endregion
             
            rv.ObjectValue = Person;
            rv.Success = true;
            rv.ErrMessage = "获取成功";

            return rv;
        }
         
        public class SeriesData {
            public string name { get; set; }
            public int value { get; set; }
        }

    }
}
