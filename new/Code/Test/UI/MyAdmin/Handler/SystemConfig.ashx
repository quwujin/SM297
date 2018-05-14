<%@ WebHandler Language="C#" Class="SystemConfig" %>

using System;
using System.Web;

public class SystemConfig : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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

        string ActiveName = string.IsNullOrEmpty(context.Request["ActiveName"]) ? "" : context.Request["ActiveName"];//配置组
        string ActionName = string.IsNullOrEmpty(context.Request["ActionName"]) ? "" : context.Request["ActionName"];//执行类型
        
        int ConfigId = Common.TypeHelper.ObjectToInt(context.Request["ConfigId"], 0);
        int Types = Common.TypeHelper.ObjectToInt(context.Request["Types"], 0);
        string Val = string.IsNullOrEmpty(context.Request["Val"]) ? "" : context.Request["Val"];
        string Title = string.IsNullOrEmpty(context.Request["Title"]) ? "" : context.Request["Title"];
        bool States = Common.TypeHelper.ObjectToBool(context.Request["States"], false);
        int TId = Common.TypeHelper.ObjectToInt(context.Request["TId"], 0);

        if (ActionName == "SaveConfig")
        {
            if (UserSession.GroupId != 2) {
                returnValue.Success = false;
                returnValue.ErrMessage = "操作异常";

                context.Response.Write(Common.JsonHelper.GetJsonString(returnValue));
                context.Response.End();
                return;
            }

            if (TId <= 0) {
                returnValue.Success = false;
                returnValue.ErrMessage = "操作异常";

                context.Response.Write(Common.JsonHelper.GetJsonString(returnValue));
                context.Response.End();
                return;
            }
            
            Model.ConfigModel mm = new Model.ConfigModel();
            mm.TId = TId;
            mm.KId = (ConfigDal.GetModelByKid().KId + 1);
            mm.Title = Title;
            if (Types == 0)
                mm.Val = States ? "true" : "false";
            else
                mm.Val =  Val;
            mm.Types = Types;
            mm.Sort = mm.KId;
            mm.Remark = "";
            mm.States = States ? 1 : 0;

            Model.ConfigLogModel mdlog = new Model.ConfigLogModel();
            mdlog.ConfigId = mm.KId;
            mdlog.UserId = UserSession.UserId;
            mdlog.Title = "新增配置";
            mdlog.Note = mm.Id + "," + mm.TId + "," + mm.KId + "," + mm.Title + "," + mm.Val + "," + mm.Val + "," + mm.States + "," + mm.Sort + "," + mm.Remark;
            mdlog.CTime = DateTime.Now;
            int ccc = ConfigDal.AddConfig(mm, mdlog);
            if (ccc > 0)
            {
                var str = CacheBase.ClearCacheObjectInfo;//清除缓存 
                returnValue.ErrMessage = "添加成功";
            }
            else
            {
                returnValue.Success = false;
                returnValue.ErrMessage = "添加失败";

                context.Response.Write(Common.JsonHelper.GetJsonString(returnValue));
                context.Response.End();
                return;
            }
            
        }

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
        string sqlwhere = "";
        switch (ActiveName)
        {
            case "first":
                sqlwhere = " and tid=28 order by sort asc";
                break;
            case "second":
                sqlwhere = " and tid=27 order by sort asc";
                break;
            case "third":
                sqlwhere = " and tid=26 order by sort asc";
                break;
            case "fourth":
                sqlwhere = " and tid=25 order by sort asc";
                break;
        }

        if (UserSession.GroupId != 2 && (ActiveName == "first" || ActiveName == "fourth" || TId == 28 || TId == 25)) {
            returnValue.Success = false;
            returnValue.ErrMessage = "操作异常";

            context.Response.Write(Common.JsonHelper.GetJsonString(returnValue));
            context.Response.End();
            return;
        }

        if ((ActionName == "SaveConfig" || ActionName == "EditConfig") && TId > 0)
            sqlwhere = " and tid=" + TId + " order by sort asc";

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