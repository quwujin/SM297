<%@ WebHandler Language="C#" Class="LogConfig" %>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Db;
using Model;

public class LogConfig : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public UserInfoModel UserSession = null;
    ReturnValue returnValue = new ReturnValue();
    protected LogConfigDal logConfigDal = new LogConfigDal();

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        returnValue.Success = true;
        returnValue.ErrMessage = "获取成功";

        //验证用户登录信息
        UserSession = WebFramework.GeneralMethodBase.GetUserSession();

        if (UserSession == null || new Db.UserInfoDal().Exists(UserSession.UserName) == false)
        {
            returnValue.Success = false;
            returnValue.ErrMessage = "登录信息失效！";

            context.Response.Write(Common.JsonHelper.GetJsonString(returnValue));
            context.Response.End();
            return;
        }

        string ActionName = string.IsNullOrEmpty(context.Request["ActionName"]) ? "" : context.Request["ActionName"];//执行类型
        string Val = string.IsNullOrEmpty(context.Request["Val"]) ? "" : context.Request["Val"];
        string Title = string.IsNullOrEmpty(context.Request["Title"]) ? "" : context.Request["Title"];
        int tabId = Common.TypeHelper.ObjectToInt(context.Request["tabId"], 0);
        int Types = Common.TypeHelper.ObjectToInt(context.Request["Types"], 0);
        List<LogConfigModel> logList = logConfigDal.ReaderLogConfig();
        bool States = Common.TypeHelper.ObjectToBool(context.Request["States"], false);
        
        try
        {
            #region Edie 根据tabid和title找到每个配置项,然后修改内容
            if (ActionName == "EditConfig")
            {
                if (tabId <= 0)
                {
                    returnValue.Success = false;
                    returnValue.ErrMessage = "操作异常";

                    context.Response.Write(Common.JsonHelper.GetJsonString(returnValue));
                    context.Response.End();
                    return;
                }

                var toBeEditItem = logList.FirstOrDefault(x => x.TabId == tabId && x.Title == Title);

                if (toBeEditItem == null)
                {
                    returnValue.Success = false;
                    returnValue.ErrMessage = "该配置项不存在";

                    context.Response.Write(Common.JsonHelper.GetJsonString(returnValue));
                    context.Response.End();
                    return;
                }

                if (Types == 0)
                {
                    toBeEditItem.Val = States ? "true" : "false";
                }
                else
                {
                    toBeEditItem.Val = Val;
                }

                tabId = toBeEditItem.TabId;

                // 修改该条目
                logConfigDal.UpdateLogItem(toBeEditItem);
                returnValue.ErrMessage = "修改成功";
            }

            #endregion

            //获取配置列表 
            returnValue.ObjectValue = Common.JsonHelper.GetJsonString(logConfigDal.GetConfigModelsByTabId(logList, tabId));
        }
        catch (Exception ex) {
            returnValue.Success = false;
            returnValue.ErrMessage = ex.ToString();
        }
        context.Response.Write(Common.JsonHelper.GetJsonString(returnValue));
        context.Response.End();
        return;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}