<%@ WebHandler Language="C#" Class="PrivacyConfig" %>

using System;
using System.Web;

public class PrivacyConfig : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public Model.UserInfoModel UserSession = null;
    Model.ReturnValue returnValue = new Model.ReturnValue();

    Db.ConfigDal ConfigDal = new Db.ConfigDal();
    
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
        
        int ConfigId = Common.TypeHelper.ObjectToInt(context.Request["ConfigId"], 0);
        int Types = Common.TypeHelper.ObjectToInt(context.Request["Types"], 0);
        string Val = string.IsNullOrEmpty(context.Request["Val"]) ? "" : context.Request["Val"];
        string Title = string.IsNullOrEmpty(context.Request["Title"]) ? "" : context.Request["Title"];
        bool States = Common.TypeHelper.ObjectToBool(context.Request["States"], false);
        int TId = Common.TypeHelper.ObjectToInt(context.Request["TId"], 0);

        if (ActionName == "EditConfig")
        { 
            Model.ConfigModel mm = ConfigDal.GetModel(ConfigId);
            if (mm.Id <= 0) {
                returnValue.Success = false;
                returnValue.ErrMessage = "该配置项不存在";

                context.Response.Write(Common.JsonHelper.GetJsonString(returnValue));
                context.Response.End();
                return; 
            }


            string old = "O:" + mm.Id + "," + mm.TId + "," + mm.KId + "," + mm.Title + "," + mm.Val + "," + mm.Val + "," + mm.States + "," + mm.Sort + "," + mm.Remark;
            mm.Id = ConfigId;
            mm.Sort = mm.KId;
            mm.States = States ? 1 : 0;
            if (Types == 0)
                mm.Val = States ? "true" : "false";
            else
                mm.Val = Val;
            mm.Types = Types;
            mm.TId = TId;
            mm.Title = Title;

            Model.ConfigLogModel mdlog = new Model.ConfigLogModel();
            mdlog.ConfigId = mm.Id;
            mdlog.UserId = UserSession.UserId;
            mdlog.Title = "修改配置";
            mdlog.Note = old + "N:" + mm.Id + "," + mm.TId + "," + mm.KId + "," + mm.Title + "," + mm.Val + "," + mm.Val + "," + mm.States + "," + mm.Sort + "," + mm.Remark;

            mdlog.CTime = DateTime.Now;
            int ccc = ConfigDal.UpdateC(mm, mdlog);
            if (ccc > 0)
            {
                var str = CacheBase.ClearCacheObjectInfo;//清除缓存 
                returnValue.ErrMessage = "修改成功";

                //重置延时时间
                if (mm.KId == 49)
                    WebFramework.BMAEvent.restEvent();
            }
            else
            {
                returnValue.Success = false;
                returnValue.ErrMessage = "修改失败";

                context.Response.Write(Common.JsonHelper.GetJsonString(returnValue));
                context.Response.End();
                return; 
            }
            
        }

        if (ActionName == "DeleteConfig") {

            if (UserSession.GroupId != 2)
            {
                returnValue.Success = false;
                returnValue.ErrMessage = "操作异常";

                context.Response.Write(Common.JsonHelper.GetJsonString(returnValue));
                context.Response.End();
                return;
            }
            
            Model.ConfigModel mm = ConfigDal.GetModel(ConfigId);
            if (mm.Id <= 0)
            {
                returnValue.Success = false;
                returnValue.ErrMessage = "该配置项不存在";

                context.Response.Write(Common.JsonHelper.GetJsonString(returnValue));
                context.Response.End();
                return;
            }
            Model.ConfigLogModel mdlog = new Model.ConfigLogModel();
            mdlog.ConfigId = mm.Id;
            mdlog.UserId = UserSession.UserId;
            mdlog.Title = "删除配置";
            mdlog.Note = "O:" + mm.Id + "," + mm.KId + "," + mm.Title + "," + mm.Val + "," + mm.Val + "," + mm.States + "," + mm.Sort + "," + mm.Remark;
            
            int ccc = ConfigDal.DelConfig(mm, mdlog);
            if (ccc > 0)
            {
                var str = CacheBase.ClearCacheObjectInfo;//清除缓存 
                returnValue.ErrMessage = "删除成功";
            }
            else
            {
                returnValue.Success = false;
                returnValue.ErrMessage = "删除配置失败";

                context.Response.Write(Common.JsonHelper.GetJsonString(returnValue));
                context.Response.End();
                return;
            }
        }


        //获取配置列表 
        string sqlwhere = " and tid=29 order by sort asc";

        returnValue.ObjectValue = Common.JsonHelper.DataTableToJson(ConfigDal.GetList(sqlwhere));

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