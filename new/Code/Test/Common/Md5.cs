using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Common
{
    public class getMD5
    {
        public getMD5()
        {

        }

        /// <summary> 
        /// MD5 加密函数 
        /// </summary> 
        /// <param name="str"></param> 
        /// <param name="code"></param> 
        /// <returns></returns> 
        public static string MD5(string str, int code)
        {

            if (code == 16)
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower().Substring(8, 16);
            }

            if (code == 32)
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
            }

            return "00000000000000000000000000000000";
        }

        public static string MD5(string str)
        {
          return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
        }
        
    }
}

