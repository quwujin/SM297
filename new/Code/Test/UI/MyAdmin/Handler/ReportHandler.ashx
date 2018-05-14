<%@ WebHandler Language="C#" Class="ReportHandler" %>

using System;
using System.Web;
using WebFramework.Report;

public class ReportHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";

        Model.ReturnValue rv = new Model.ReturnValue();
        
        try
        {
            rv = ReportMethod.ReportInstance.GetReport(context);
            
        }
        catch (Exception ex) { 
            
        }

       context.Response.Write(Common.JsonHelper.GetJsonString(rv));
        
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}