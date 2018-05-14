using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Common
{
    public class JScript
    {

        /// <summary>
        /// 填出自定义字符串的弹出框
        /// </summary>
        /// <param name="page"></param>
        /// <param name="msg"></param>
        public static void SimpleMessageBox(Page page, string msg)
        {
         
            page.ClientScript.RegisterStartupScript(page.GetType(), "mys", "alert('" + msg + "')", true);
        }

        public static void LoctionTop(Page page, string msg,string url)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "ok", "alert('"+msg+"');self.parent.location.href='"+url+"'", true);
     

        }

        /// <summary>
        /// 弹出自定义脚本
        /// </summary>
        /// <param name="page"></param>
        /// <param name="script"></param>
        public static void ExecuteScript(Page page, string script)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "mys", script, true);
        }

        /// <summary>
        /// 控件点击 消息确认提示框Control
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        public static void ShowConfirm(System.Web.UI.WebControls.WebControl Control, string msg)
        {
            //Control.Attributes.Add("onClick","if (!window.confirm('"+msg+"')){return false;}");
            Control.Attributes.Add("onclick", "return confirm('" + msg + "');");
        }

        /// <summary>
        /// 显示消息提示对话框，并进行页面跳转
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        /// <param name="url">跳转的目标URL</param>
        public static void ShowAndRedirect(System.Web.UI.Page page, string msg, string url)
        {
            StringBuilder Builder = new StringBuilder();
            Builder.Append("<script language='javascript' defer>");
            Builder.AppendFormat("alert('{0}');", msg);
            //Builder.AppendFormat("top.location.href='{0}'", url);
            Builder.AppendFormat("location.href='{0}'", url);
            Builder.Append("</script>");
            page.RegisterStartupScript("message", Builder.ToString());

        }

        #region JS alert
        public static void alert(string ScriptName, string str, System.Web.UI.Page s)
        {
            ClientScriptManager CSM = s.ClientScript;
            if (!CSM.IsClientScriptBlockRegistered(ScriptName))
            {
                CSM.RegisterStartupScript(s.GetType(), ScriptName, "<script language='javascript'>alert( '" + str + "' );</script>");
            }
        }
        #endregion

        #region JS Loction
        public static void Loction(string ScriptName, string url, System.Web.UI.Page s)
        {
            ClientScriptManager CSM = s.ClientScript;
            if (!CSM.IsClientScriptBlockRegistered(ScriptName))
            {
                CSM.RegisterStartupScript(s.GetType(), ScriptName, "<script>window.location.href='" + url + "';</script>");
            }
        }
        #endregion


        #region JS alert+loaction
        public static void alert(string ScriptName, string str, string url, System.Web.UI.Page s)
        {
            ClientScriptManager CSM = s.ClientScript;
            if (!CSM.IsClientScriptBlockRegistered(ScriptName))
            {
                CSM.RegisterStartupScript(s.GetType(), ScriptName, "<script language='javascript'>alert( '" + str + "' );window.location.href='" + url + "';</script>");
            }
        }



        public static void Ok(string ScriptName, string str,  System.Web.UI.Page s)
        {
            ClientScriptManager CSM = s.ClientScript;
            if (!CSM.IsClientScriptBlockRegistered(ScriptName))
            {
                CSM.RegisterStartupScript(s.GetType(), ScriptName, "<script language='javascript'>return window.confirm( '" + str + "' );</script>");
            }
        }
        #endregion

        #region JS alert+loaction
        public static void alert(string ScriptName, string str, System.Web.UI.Page s, string self)
        {
            ClientScriptManager CSM = s.ClientScript;
            if (!CSM.IsClientScriptBlockRegistered(ScriptName))
            {
                CSM.RegisterStartupScript(s.GetType(), ScriptName, str);
            }
        }
        #endregion


        #region JS alert+back
        public static void alertBack(string ScriptName, string str, System.Web.UI.Page s)
        {
            ClientScriptManager CSM = s.ClientScript;
            if (!CSM.IsClientScriptBlockRegistered(ScriptName))
            {
                CSM.RegisterStartupScript(s.GetType(), ScriptName, "<script language='javascript'>alert( '" + str + "' );window.history.back(-1);</script>");
            }
        }
        #endregion

        public static int Cint(string str)
        {
            if (str == "" || str == null)
            {
                return 0;
            }
            else
            {
                string pattern = @"^\-?[0-9]+$";
                if (System.Text.RegularExpressions.Regex.IsMatch(str, pattern))
                {
                    return int.Parse(str);
                }
                else
                {
                    return 0;
                }

            }

        }



    }
}
