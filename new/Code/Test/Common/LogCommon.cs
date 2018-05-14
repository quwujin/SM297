using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Configuration;

namespace Common
{
    public class LogCommon
    {
        /// <summary>
        /// 
        /// </summary>
        public static string urlPath
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return string.Empty;
                }
                string siteUrl = HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
                string siteFolder = HttpContext.Current.Request.ServerVariables["PATH_INFO"];
                int prevSeperator = siteFolder.LastIndexOf("/");
                siteFolder = siteFolder.Substring(0, prevSeperator);
                siteUrl += siteFolder;

                if (siteUrl.LastIndexOf("/") == siteUrl.Length - 1)
                {
                    siteUrl = siteUrl.Substring(0, siteUrl.Length - 1);
                }
                return siteUrl;
            }
        }

        //public static void WriteLog(Exception ex) 
        //{
        //    WriteLog(ex, null);
        //}

        //public static void WriteLog(Exception ex,string logtype) 
        //{ 
        //    LogTool.Model.App_Log_SQL _log = new LogTool.Model.App_Log_SQL();
        //    LogTool.GlobalErrorHandler _globalErrorHandler = new LogTool.GlobalErrorHandler();
        //    _log.Notes = ex.Message + "--" + ex.InnerException + "--" + ex.StackTrace + "--" + _globalErrorHandler.ExceptionToString(ex);
        //    _log.Success = false;
        //    _log.Source = "FrontEnd";
        //    _log.SQLType = logtype??"SysLog";
        //    _log.SaveLog();
        //    WebFramework.GeneralMethodBase.WebDebugLog(_log.Notes);
        //}



        public static void WebDebugLog(string message)
        {

            if (WebDebug == false) 
            {
                return;
            }
            WriteLog(message);
        }


        public static void WriteLog(string message)
        {
             
            if (string.IsNullOrEmpty(message)) 
            {
                return;
            }

            string defaultname="Default";

            if( HttpContext.Current!=null)
            {
                defaultname=HttpContext.Current.Request.Url.Host;
            }


            string path = System.AppDomain.CurrentDomain.BaseDirectory+"Log/log_" + defaultname + "_" + System.DateTime.Now.ToString("yyyyMMdd") + ".log";

            string _message = "";

            if (message.ToUpper() == "FINISH") 
            {
                _message = System.DateTime.Now.ToString() + "     Finish\r\n\r\n\r\n\r\n";
            }
            else
            {
                _message = System.DateTime.Now.ToString() + "     " + message + "\r\n";
            }

            #region Save File

            if (!File.Exists(path))
            {
                try
                {
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine(_message);
                    }
                }
                catch { }
            }
            else
            {
                try
                {
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        if ((File.GetAttributes(path) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                            File.SetAttributes(path, FileAttributes.Normal);
                        sw.WriteLine(_message);
                    }
                }
                catch { }
            }
            #endregion
        }



        private static bool? _webDebug = null;
        public static bool WebDebug 
        {
            get 
            {
                if (_webDebug == null)
                {
                    bool result = false;
                    if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["LogTool_WebFramework.GeneralMethodBase.WebDebugLog"]) == false)
                    {
                        Boolean.TryParse(ConfigurationManager.AppSettings["LogTool_WebFramework.GeneralMethodBase.WebDebugLog"], out result);
                        _webDebug = result;
                        return result;
                    }
                    else
                    {
                        _webDebug = result;
                        return result;
                    }
                }
                else 
                {
                    return _webDebug.Value;
                }
            }
        }


        private static bool? _sendEmailLog = null;
        public static bool SendEmailLog
        {
            get
            {
                if (_sendEmailLog == null)
                {
                    bool result = false;
                    if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["LogTool_SendEmailLog"]) == false)
                    {
                        Boolean.TryParse(ConfigurationManager.AppSettings["LogTool_SendEmailLog"], out result);
                        _sendEmailLog = result;
                        return result;
                    }
                    else
                    {
                        _sendEmailLog = result;
                        return result;
                    }
                }
                else
                {
                    return _sendEmailLog.Value;
                }
            }
        }


        public static void WriteFileLog(string message, string filekey)
        {
            if (string.IsNullOrEmpty(message)) 
            {
                return;
            }

            string path = System.AppDomain.CurrentDomain.BaseDirectory + "Log/log_" + filekey + "_" + System.DateTime.Now.ToString("yyyyMMdd") + ".log";

            string _message = "";

            if (message.ToUpper() == "FINISH")
            {
                _message = System.DateTime.Now.ToString() + "     Finish\r\n\r\n\r\n\r\n";
            }
            else
            {
                _message = System.DateTime.Now.ToString() + "     " + message + "\r\n";
            }

            #region Save File

            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(_message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    if ((File.GetAttributes(path) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                        File.SetAttributes(path, FileAttributes.Normal);
                    sw.WriteLine(_message);
                }
            }
            #endregion
        }


    } 
}
