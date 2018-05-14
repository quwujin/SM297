<%@ WebHandler Language="C#" Class="CommonGetList" %>

using System;
using System.Web;
using System.Data;

public class CommonGetList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public Model.UserInfoModel UserSession = null;
    Model.ReturnValue returnValue = new Model.ReturnValue();

    public void ProcessRequest (HttpContext context) {
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
        int PageIndex = Common.TypeHelper.ObjectToInt(context.Request["PageIndex"], 1);
        int PageSize = Common.TypeHelper.ObjectToInt(context.Request["PageSize"], 10);

        string StarTimeText = context.Request["StarTimeText"];
        string EndTimeText = context.Request["EndTimeText"];

        if (Common.ValidateHelper.IsCode(ActionName) == false || ActionName.Length > 30) {
            
            returnValue.Success = false;
            returnValue.ErrMessage = "执行类型错误";

            context.Response.Write(Common.JsonHelper.GetJsonString(returnValue));
            context.Response.End();
            return;
        }

        string sqlwhere = ""; 
        string tableName = "";
        
        if (string.IsNullOrEmpty(StarTimeText) == false && string.IsNullOrEmpty(EndTimeText) == false)
        {
            if (Common.ValidateHelper.IsDate(StarTimeText) == false || Common.ValidateHelper.IsDate(EndTimeText) == false)
            {
                returnValue.Success = false;
                returnValue.ErrMessage = "日期格式错误";

                context.Response.Write(Common.JsonHelper.GetJsonString(returnValue));
                context.Response.End();
                return;
            }

            sqlwhere = string.Format(" and CreateTime>='{0}' and CreateTime<='{1}'", StarTimeText, EndTimeText);
        }
        
        switch (ActionName) {

            case "Delayed":
                sqlwhere += "";
                tableName = "DelayedAwards";
                break;
        }

        DataTable TableDt = Db.ConDal.GetList(tableName, sqlwhere, PageIndex, PageSize, "");

        returnValue.ObjectValue = Common.JsonHelper.DataTableToJson(TableDt);
        returnValue.EffectRows = TableDt.Rows.Count;

        context.Response.Write(Common.JsonHelper.GetJsonString(returnValue));
        context.Response.End();
        return;
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}