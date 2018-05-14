using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace Common
{
    /// <summary>
    /// Web帮助类
    /// </summary>
    public class DateTimeHelper
    {

        //星期数组
        private static string[] _weekdays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
         

        #region 时间操作

        /// <summary>
        /// 获得当前时间的""yyyy-MM-dd HH:mm:ss:fffffff""格式字符串
        /// </summary>
        public static string GetDateTimeMS()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fffffff");
        }

        /// <summary>
        /// 获得当前时间的""yyyy年MM月dd日 HH:mm:ss""格式字符串
        /// </summary>
        public static string GetDateTimeU()
        {
            return string.Format("{0:U}", DateTime.Now);
        }

        /// <summary>
        /// 获得当前时间的""yyyy-MM-dd HH:mm:ss""格式字符串
        /// </summary>
        public static string GetDateTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 获得当前日期
        /// </summary>
        public static string GetDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 获得中文当前日期
        /// </summary>
        public static string GetChineseDate()
        {
            return DateTime.Now.ToString("yyyy月MM日dd");
        }

        /// <summary>
        /// 获得当前时间(不含日期部分)
        /// </summary>
        public static string GetTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }

        /// <summary>
        /// 获得当前小时
        /// </summary>
        public static string GetHour()
        {
            return DateTime.Now.Hour.ToString("00");
        }

        /// <summary>
        /// 获得当前天
        /// </summary>
        public static string GetDay()
        {
            return DateTime.Now.Day.ToString("00");
        }

        /// <summary>
        /// 获得当前月
        /// </summary>
        public static string GetMonth()
        {
            return DateTime.Now.Month.ToString("00");
        }

        /// <summary>
        /// 获得当前年
        /// </summary>
        public static string GetYear()
        {
            return DateTime.Now.Year.ToString();
        }

        /// <summary>
        /// 获得当前星期(数字)
        /// </summary>
        public static string GetDayOfWeek()
        {
            return ((int)DateTime.Now.DayOfWeek).ToString();
        }

        /// <summary>
        /// 获得当前星期(汉字)
        /// </summary>
        public static string GetWeek()
        {
            return _weekdays[(int)DateTime.Now.DayOfWeek];
        }

        /// <summary>
        /// 获得日期组合数字
        /// </summary> 
        public static string GetDateTimeffff()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssfffffff");
        }

        /// <summary>
        /// 比较日期是否比当前日期小
        /// </summary> 
        public static bool IsLessDateTime(string time)
        {
            if (string.IsNullOrWhiteSpace(time)) return false;
            DateTime dt = DateTime.Parse(time);
            if (dt < DateTime.Now) return true;
            return false;
        }

        /// <summary>
        /// 比较日期是否大于当前日期
        /// </summary> 
        public static bool IsGreaterDateTime(string time)
        { 
            if (string.IsNullOrWhiteSpace(time)) return false; 
            DateTime dt = DateTime.Parse(time);
            if (dt > DateTime.Now) return true;
            return false;
        }


        /// <summary>
        /// 获取本周一日期
        /// </summary> 
        public static string GetMonday()
        {
            DateTime startWeek = DateTime.Now.AddDays(1 - Convert.ToInt32(DateTime.Now.DayOfWeek.ToString("d")));  //本周周一
            return startWeek.ToString("yyyyMMdd");
        }

        /// <summary>
        /// 获取本周日日期
        /// </summary> 
        public static string GetSunday()
        {
            DateTime startWeek = DateTime.Now.AddDays(1 - Convert.ToInt32(DateTime.Now.DayOfWeek.ToString("d")));  //本周周一
            DateTime endWeek = startWeek.AddDays(6);  //本周周日
            return endWeek.ToString("yyyyMMdd");
        }

        #endregion

        /// <summary>
        /// 声明期间类型枚举
        /// </summary>
        public enum Period { Day, Week, Month, Year };
        /// <summary>
        /// 获取指定期间的起止日期
        /// </summary>
        /// <param name="period">期间类型</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        public static void GetPeriod(Period period, out DateTime beginDate, out DateTime endDate)
        {
            int year = DateTime.Today.Year;
            int month = DateTime.Today.Month;
            switch (period)
            {
                case Period.Year: //年
                    beginDate = new DateTime(year, 1, 1);
                    endDate = new DateTime(year, 12, 31);
                    break;
                case Period.Month: //月
                    beginDate = new DateTime(year, month, 1);
                    endDate = beginDate.AddMonths(1).AddDays(-1);
                    break;
                case Period.Week: //周
                    int week = (int)DateTime.Today.DayOfWeek;
                    if (week == 0) week = 7; //周日
                    beginDate = DateTime.Today.AddDays(-(week - 1));
                    endDate = beginDate.AddDays(6);
                    break;
                default: //日
                    beginDate = DateTime.Today;
                    endDate = DateTime.Today;
                    break;
            }
        }

        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name=”timeStamp”></param>
        /// <returns></returns>
        public static DateTime GetTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name=”time”></param>
        /// <returns></returns>
        public static string ConvertDateTime(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return Convert.ToString((int)(time - startTime).TotalSeconds);
        }


        /// <summary>

        /// 根据第几周 获取开始时间和结束时间

        /// </summary>

        /// <param name="week">周数</param>

        /// <param name="month">月份</param>

        /// <returns></returns> 
        public static Tuple<DateTime, DateTime> GetWeeks(int? week, int? month)
        {

            DateTime dt = DateTime.Now;

            string[] arrDate0 = new string[6];

            string[] arrDate1 = new string[6];

            //年

            int year = dt.Year;

            //月 

            //当前月第一天

            DateTime weekStart = new DateTime(year, month.Value, 1);

            //该月的最后一天

            DateTime monEnd = weekStart.AddMonths(1).AddDays(-1);

            int i = 1;

            //当前月第一天是星期几

            int dayOfWeek = Convert.ToInt32(weekStart.DayOfWeek.ToString("d"));

            //该月第一周结束日期

            DateTime weekEnd = dayOfWeek == 0 ? weekStart : weekStart.AddDays(7 - dayOfWeek);

            string content = "";

            // content += "第" + i + "周起始日期： " + weekStart.ToShortDateString() + "   结束日期： " + weekEnd.ToShortDateString() + "\n";

            arrDate0[0] = weekStart.ToString("yyyy-MM-dd 00:00:00");

            arrDate1[0] = weekEnd.ToString("yyyy-MM-dd 23:59:59");



            //当日期小于或等于该月的最后一天



            while (weekEnd.AddDays(1) <= monEnd)
            {

                i++;

                //该周的开始时间

                weekStart = weekEnd.AddDays(1);

                //该周结束时间

                weekEnd = weekEnd.AddDays(7) > monEnd ? monEnd : weekEnd.AddDays(7);



                arrDate0[i - 1] = weekStart.ToString("yyyy-MM-dd 00:00:00");

                arrDate1[i - 1] = weekEnd.ToString("yyyy-MM-dd 23:59:59");

                // content += "第" + i + "周起始日期： " + weekStart.ToShortDateString() + "   结束日期： " + weekEnd.ToShortDateString() + "\n";

            }



            content += year + "年" + month + "月共有" + i + "周\n";

            int AddDay = 6 - (DateTime.Parse(arrDate1[week.Value]).Day - DateTime.Parse(arrDate0[week.Value]).Day);

            return Tuple.Create(DateTime.Parse(arrDate0[week.Value]), DateTime.Parse(arrDate1[week.Value]).AddDays(AddDay));

        }
         
        /// <summary>

        /// 根据季度 获取开始时间和结束时间

        /// </summary>

        /// <param name="quarter">季度</param>

        /// <returns></returns> 
        private Tuple<DateTime, DateTime> GetQurater(string quarter)
        {

            DateTime dt = new DateTime();

            switch (quarter)
            {

                case "一季度":

                    dt = DateTime.Parse(DateTime.Now.Year + ",1, 01");

                    break;

                case "二季度":

                    dt = DateTime.Parse(DateTime.Now.Year + ",4, 01");

                    break;

                case "三季度":

                    dt = DateTime.Parse(DateTime.Now.Year + ",7, 01");

                    break;

                case "四季度":

                    dt = DateTime.Parse(DateTime.Now.Year + ",10, 01");

                    break;



            }



            //本季度初 

            string start = dt.AddMonths(0 - (dt.Month - 1) % 3).AddDays(1 - dt.Day).ToString("yyyy-MM-01 00:00:00");  //本季度初    

            //获取本季度的最后一天  

            string end = dt.AddMonths(0 - (dt.Month - 1) % 3).AddDays(1 - dt.Day).AddMonths(3).AddDays(-1).ToString("yyyy-MM-dd 23:59:59"); ;  //
             
            return Tuple.Create(DateTime.Parse(start), DateTime.Parse(end));

        }
      
    }
}
