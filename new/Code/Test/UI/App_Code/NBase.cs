using Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using WebFramework.SessionManage;

    
    public class NBase : System.Web.UI.Page
    { 
        public Model.SessionModel orderSession = null;

        #region 初始化方法
        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e); if (WebFramework.GeneralMethodBase.IsMoblie() == false)
            {
                Response.End();
                return;
            }

            orderSession = SessionMethod.SessionInstance.GetSession();

            #region 检查session
            if (orderSession == null)
            {
                Response.Redirect("/default.aspx");
                Response.End();
                return;
            }
            #endregion

        }
        #endregion


    }
 