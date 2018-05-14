<%@ WebHandler Language="C#" Class="Servicefile" %>

using System;
using System.Web;

public class Servicefile : IHttpHandler,System.Web.SessionState.IRequiresSessionState {

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        Model.SessionModel orderSession = WebFramework.SessionManage.SessionMethod.SessionInstance.GetSession();

        if (orderSession == null)
        {
            LogTool.LogCommon.WebDebugLog("未获取到Session");
            context.Response.Write("-1");
            context.Response.End();
            return;
        }
         
        string FileName = context.Request["FileName"];
        string FileHash = context.Request["FileHash"];
        string OriginFileName = context.Request["OriginFileName"];

        if (Common.ValidateHelper.IsCode(FileHash) == false
            || Common.ValidateHelper.IsImgFileName(OriginFileName, 180) == false
            || Common.ValidateHelper.IsImgFileName(FileName, 180) == false
            )
        {
            LogTool.LogCommon.WebDebugLog("值异常：" + FileHash + "," + OriginFileName + "," + FileName);
            context.Response.Write("-1");
            context.Response.End();
            return;
        }
         
        Model.FileInfoModel fldel = new Model.FileInfoModel();
        fldel.Hashdata = FileHash;
        fldel.FileName = FileName;//压缩图
        fldel.SaveName = OriginFileName;//原图
        fldel.Size = "";
        fldel.Type = "";
        fldel.States = 0;
        fldel.Note = "";
        fldel.CreateTime = DateTime.Now;

        Db.FileInfoDal fldal = new Db.FileInfoDal();
        int FileId = fldal.Add(fldel);
        if (FileId <= 0)
        {
            LogTool.LogCommon.WebDebugLog("添加失败：" + FileHash + "," + OriginFileName + "," + FileName);
            context.Response.Write("-1");
            context.Response.End();
            return;
        }

        orderSession.FileId = FileId;
        WebFramework.SessionManage.SessionMethod.SessionInstance.SetSession(orderSession);
            
        context.Response.Write("0");
        context.Response.End();
        return;
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}