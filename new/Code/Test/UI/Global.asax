<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        //在应用程序启动时运行的代码

        //启动延时轮询事件机制
        //WebFramework.BMAEvent.Start();
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //在应用程序关闭时运行的代码

    }
    void Application_BeginRequest(object sender, EventArgs e) 
    {
         //每次访问时运行

        string Url = Request.Url.AbsolutePath;

        string strRegex = "^/D_([a-zA-Z0-9]{10})$"; 
        string toUrl = "/Default.aspx?codes=$1";
        
        if (Regex.IsMatch(Url, strRegex))
        {
            System.Text.RegularExpressions.Regex oReg;
            oReg = new System.Text.RegularExpressions.Regex(strRegex);
            string ReWriteUrl = oReg.Replace(Request.Url.AbsolutePath, toUrl);
            HttpContext.Current.RewritePath(ReWriteUrl);
            return; 
        }
        else
        {
            //HttpContext.Current.Response.Write(Url);
            //HttpContext.Current.Response.End();
            //return;
        }

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        //在出现未处理的错误时运行的代码

        Exception ex = Server.GetLastError();

        if (ex.Message.Contains("does not exist.") || ex.Message.Contains("文件不存在"))
        {
            //if (FrameWorkV4.AdminFunction.FileAction.ProcessFileError(ex))
            //{
            //    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.RawUrl);

            //}
            HttpContext.Current.Response.StatusCode = 404;
            Server.ClearError();
            return;
        }

        if (ex.InnerException != null && ex.InnerException.ToString().IndexOf("MyEmail.cs:line 155") != -1)
        {
            HttpContext.Current.Response.Redirect(HttpContext.Current.Request.RawUrl);
            Server.ClearError();
            return;
        }


        if (HttpContext.Current.Items["ErrorTransaction"] != null)
        {
             
        }



        LogTool.Model.App_Log_SQL _log = new LogTool.Model.App_Log_SQL(); 
        LogTool.GlobalErrorHandler _globalErrorHandler = new LogTool.GlobalErrorHandler();
        _log.Notes = ex.Message + "--" + ex.InnerException + "--" + ex.StackTrace + "--" + _globalErrorHandler.ExceptionToString(ex);
        _log.Success = false;
        _log.Source = "Globel";
        _log.SQLType = "Code";
        _log.Url = HttpContext.Current.Request.Url.ToString();
        int logid = _log.SaveLog();
        Server.ClearError();
        
        if (WebFramework.GeneralMethodBase.GetKeyConfig(3).States == 0)
        {
            HttpContext.Current.Response.Redirect("~/Error.aspx?type=K190IL782HKGUNB26URNMKNRQWSR43" + DateTime.Now.ToString("yyyyMMddhhmmssfff"));
            return;
        }
        //HttpContext.Current.Response.Redirect("~/ErrorPage.aspx?ErrorID=" + logid.ToString());


    }

    void Session_Start(object sender, EventArgs e) 
    {
        //在新会话启动时运行的代码

    }

    void Session_End(object sender, EventArgs e) 
    {
        //在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式 
        //设置为 StateServer 或 SQLServer，则不会引发该事件。

    }
       
</script>
