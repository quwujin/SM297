using System;
using System.Text.RegularExpressions;
using System.Web;
namespace Common
{
    /// <summary>
    /// 验证帮助类
    /// </summary>
    public static class ValidateHelper
    {

        #region 常规验证
        //邮件正则表达式
        private static Regex _emailregex = new Regex(@"^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.[a-zA-Z0-9]{2,6}$", RegexOptions.IgnoreCase);
        //手机号正则表达式
        private static Regex _mobileregex = new Regex("^(13|14|15|16|18|17|19)[0-9]{9}$");
        //固话号正则表达式
        private static Regex _phoneregex = new Regex(@"^(\d{3,4}-?)?\d{7,8}$");
        //IP正则表达式
        private static Regex _ipregex = new Regex(@"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$");
        //日期正则表达式
        private static Regex _dateregex = new Regex(@"^(\d{4})-(\d{1,2})-(\d{1,2})$");
        //数值(包括整数和小数)正则表达式
        private static Regex _numericregex = new Regex(@"^[-]?[0-9]+(\.[0-9]+)?$");

        //邮政编码正则表达式
        private static Regex _zipcoderegex = new Regex(@"^\d{6}$");

        //图片名正则表达式
        private static Regex _imgnameregex = new Regex(@"^[0-9a-zA-Z/:._-]+$");

        //大写字母加数字正则表达式
        private static Regex _coderegex = new Regex("^[A-Za-z0-9]+$");

        //Openid正则表达式
        private static Regex _openidregex = new Regex("^([A-Za-z0-9___]|[-]){28}$");

        //名字正则表达式
        private static Regex _nameregex = new Regex(@"^([a-zA-Z\s\u4E00-\u9FA5]+)$");

        //地址正则表达式 （?=.* 必须包含）
        private static Regex _addrsregex = new Regex(@"^(?=.*[\u4E00-\u9FA5])[\w\u4E00-\u9FA5!！，,。.；;-]{0,180}$");

        /// <summary>
        /// 是否为姓名
        /// </summary>
        public static bool IsName(string s)
        {
            if (string.IsNullOrEmpty(s)) { return false; }

            return _nameregex.IsMatch(s);
        }

        /// <summary>
        /// 是否为Openid
        /// </summary>
        public static bool IsOpenid(string s)
        {
            if (string.IsNullOrEmpty(s)) { return false; }

            return _openidregex.IsMatch(s);
        }

        /// <summary>
        /// 是否为地址
        /// </summary>
        public static bool IsAddrs(string s)
        {
            if (string.IsNullOrEmpty(s)) { return false; }
             
            return _addrsregex.IsMatch(s);
        }


        /// <summary>
        /// 是否为激活码
        /// </summary>
        public static bool IsCode(string s)
        {
            if (string.IsNullOrEmpty(s)) { return false; }
            return _coderegex.IsMatch(s);
        }

        /// <summary>
        /// 是否为激活码
        /// </summary>
        public static bool IsCode(string s, int Length)
        {
            if (string.IsNullOrEmpty(s)) { return false; }
            if (s.Length != Length) { return false; }
            return _coderegex.IsMatch(s);
        }

        /// <summary>
        /// 是否为邮箱名
        /// </summary>
        public static bool IsEmail(string s)
        {
            if (string.IsNullOrEmpty(s)) { return false; }

            return _emailregex.IsMatch(s);
        }

        /// <summary>
        /// 是否为手机号
        /// </summary>
        public static bool IsMobile(string s)
        {
            if (string.IsNullOrEmpty(s)) { return false; }

            return _mobileregex.IsMatch(s);
        }

        /// <summary>
        /// 是否为固话号
        /// </summary>
        public static bool IsPhone(string s)
        {
            if (string.IsNullOrEmpty(s)) { return false; }

            return _phoneregex.IsMatch(s);
        }

        /// <summary>
        /// 是否为IP
        /// </summary>
        public static bool IsIP(string s)
        {
            if (string.IsNullOrEmpty(s)) { return false; }

            return _ipregex.IsMatch(s);
        }

        /// <summary>
        /// 是否是身份证号
        /// </summary>
        public static bool IsIdCard(string id)
        { 
            if (id.Length == 18)
                return CheckIDCard18(id);
            else if (id.Length == 15)
                return CheckIDCard15(id);
            else
                return false;
        }

        /// <summary>
        /// 是否为18位身份证号
        /// </summary>
        private static bool CheckIDCard18(string Id)
        {
            long n = 0;
            if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
                return false;//数字验证

            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
                return false;//省份验证

            string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
                return false;//生日验证

            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = Id.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());

            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
                return false;//校验码验证

            return true;//符合GB11643-1999标准
        }

        /// <summary>
        /// 是否为15位身份证号
        /// </summary>
        private static bool CheckIDCard15(string Id)
        {
            long n = 0;
            if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))
                return false;//数字验证

            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
                return false;//省份验证

            string birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
                return false;//生日验证

            return true;//符合15位身份证标准
        }

        /// <summary>
        /// 是否为日期
        /// </summary>
        public static bool IsDate(string s)
        {
            if (string.IsNullOrEmpty(s)) { return false; }
            s = s.Replace("/","-");

            return _dateregex.IsMatch(s);
        }

        /// <summary>
        /// 是否是数值(包括整数和小数)
        /// </summary>
        public static bool IsNumeric(string numericStr)
        {
            if (string.IsNullOrEmpty(numericStr)) { return false; }

            return _numericregex.IsMatch(numericStr);
        }

        /// <summary>
        /// 是否为邮政编码
        /// </summary>
        public static bool IsZipCode(string s)
        {
            if (string.IsNullOrEmpty(s)) { return false; }

            return _zipcoderegex.IsMatch(s);
        }

        /// <summary>
        /// 是否是图片文件名
        /// </summary>
        /// <returns> </returns>
        public static bool IsImgFileName(string fileName,int length)
        {
            if (fileName.Length > length || fileName.IndexOf(".") == -1 || fileName.Split('.').Length > 5)
                return false;

            if (_imgnameregex.IsMatch(fileName) == false)
                return false;

            string tempFileName = fileName.Trim().ToLower();
            string extension = tempFileName.Substring(tempFileName.LastIndexOf("."));
            return extension == ".png" || extension == ".bmp" || extension == ".jpg" || extension == ".jpeg" || extension == ".gif";
        }

       

        /// <summary>
        /// 是否是数值(包括整数和小数)
        /// </summary>
        public static bool IsNumericArray(string[] numericStrList)
        {
            if (numericStrList != null && numericStrList.Length > 0)
            {
                foreach (string numberStr in numericStrList)
                {
                    if (!IsNumeric(numberStr))
                        return false;
                }
                return true;
            }
            return false;
        }

        #endregion

        #region 判断请求
        /// <summary>
        /// 是否是Ajax请求
        /// </summary>
        /// <returns></returns>
        public static bool IsAjax()
        {
            return HttpContext.Current.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }
        /// <summary>
        /// 是否是移动设备请求
        /// </summary>
        /// <returns></returns>
        public static bool IsMobile()
        {
            if (HttpContext.Current.Request.Browser.IsMobileDevice)
                return true;

            bool isTablet = false;
            if (bool.TryParse(HttpContext.Current.Request.Browser["IsTablet"], out isTablet) && isTablet)
                return true;

            return false;
        }
        #endregion

        
    }
}
