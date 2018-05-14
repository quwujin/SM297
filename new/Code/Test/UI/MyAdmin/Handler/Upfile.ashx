<%@ WebHandler Language="C#" Class="Upfile" %>

using System;
using System.Web;

public class Upfile : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
 
      
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}