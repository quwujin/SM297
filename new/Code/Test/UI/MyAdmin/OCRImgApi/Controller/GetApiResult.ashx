<%@ WebHandler Language="C#" Class="GetApiResult" %>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Threading.Tasks;

public class GetApiResult : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";

        BDImgApi.APIKey.appUserName = WebFramework.GeneralMethodBase.GetKeyConfig(25).Val;
        BDImgApi.APIKey.appKey = WebFramework.GeneralMethodBase.GetKeyConfig(26).Val;
        BDImgApi.APIKey.appSecret = WebFramework.GeneralMethodBase.GetKeyConfig(27).Val;
         
        //OSS上传请求账号
        BDImgApi.APIKey.pnum = WebFramework.GeneralMethodBase.GetKeyConfig(22).Val;
        BDImgApi.APIKey.uid = WebFramework.GeneralMethodBase.GetKeyConfig(32).Val;
        BDImgApi.APIKey.key = WebFramework.GeneralMethodBase.GetKeyConfig(33).Val;
         
        BDImgApi.ParameterCheck ParameterCheck = new BDImgApi.ParameterCheck(); 

        //ReturnApiResult 可传入 bool IsRead = true; 强制录入
            
        context.Response.Write(ParameterCheck.ReturnApiResult(context));
        context.Response.End();
        return; 

    } 
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}
 