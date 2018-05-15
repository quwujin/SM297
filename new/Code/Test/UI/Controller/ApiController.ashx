<%@ WebHandler Language="C#" Class="ApiController" %>

using System;
using System.Web;

public class ApiController : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest (HttpContext context) {

        HttpContext.Current.Response.ContentType = "text/plain";

        var action = context.Request["GetResult"];

        Model.ReturnValue result = new Model.ReturnValue();

        if (string.IsNullOrEmpty(action) == false)
        {
            action = action.ToLower();

            if (action == "uploadfile")
            {
                ProcessUploadFile process = new ProcessUploadFile();
                result = process.ProcessUpload(context);
            }
            if (action == "uploadadds")
            {
                ProcessUploadFile process = new ProcessUploadFile();
                result = process.ProcessUpdateInfo(context);
            }
            if (action == "getcode")
            {
                ProcessCodeDraw process = new ProcessCodeDraw();
                result = process.ProcessCode(context);
            }
            //获取奖项
            if (action == "getjx")
            {
                ProcessCodeDraw process = new ProcessCodeDraw();
                result = process.GetJx(context);
            }  
            //更新地址信息
            if (action == "updateaddress")
            {
                ProcessCodeDraw process = new ProcessCodeDraw();
                result = process.AddAddressInfo(context);
            }
        }

        context.Response.Write(Common.JsonHelper.GetJsonString(result));

    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}