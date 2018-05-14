<%@ WebHandler Language="C#" Class="getCity" %>

using System;
using System.Web;
using System.Data;

public class getCity : IHttpHandler,System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";

        Db.DistanceDal distancedal = new Db.DistanceDal();
        
        string lat = context.Request["lat"];
        string lng = context.Request["lng"];

        string str = "";
        
        if (string.IsNullOrEmpty(lat) == false && string.IsNullOrEmpty(lng) == false)
        {

            if (Common.TypeHelper.StringToDecimal(lat, 0) <= 0 || Common.TypeHelper.StringToDecimal(lng, 0) <= 0)
            {

                ReturnMsg(context, "-1", str);
                context.Response.End();
                return;
            }
            
            //查找5公里内的门店
            DataTable distanceDt = distancedal.GetStorList(lng, lat, 5, 4);

            if (distanceDt.Rows.Count > 0)
            {
                foreach (DataRow model in distanceDt.Rows)
                {
                    str += "{\"Channel\":\"" + model["Channel"] + "\",\"Lng\":\"" + model["Lng"] + "\",\"Lat\":\"" + model["Lat"] + "\",\"Address\":\"" + model["Address"] + "\"},";
                }

                ReturnMsg(context, "0", str.TrimEnd(','));
                context.Response.End();
                return;
            }
             

            //查找10公里内的门店
            distanceDt = distancedal.GetStorList(lng, lat, 10, 4);

            if (distanceDt.Rows.Count > 0)
            {
                foreach (DataRow model in distanceDt.Rows)
                {
                    str += "{\"Channel\":\"" + model["Channel"] + "\",\"Lng\":\"" + model["Lng"] + "\",\"Lat\":\"" + model["Lat"] + "\",\"Address\":\"" + model["Address"] + "\"},";
                }

                ReturnMsg(context, "0", str.TrimEnd(','));
                context.Response.End();
                return;
            }
            

            //查找20公里内的门店
            distanceDt = distancedal.GetStorList(lng, lat, 20, 4); 

            if (distanceDt.Rows.Count > 0)
            {
                foreach (DataRow model in distanceDt.Rows)
                {
                    str += "{\"Channel\":\"" + model["Channel"] + "\",\"Lng\":\"" + model["Lng"] + "\",\"Lat\":\"" + model["Lat"] + "\",\"Address\":\"" + model["Address"] + "\"},";
                }

                ReturnMsg(context, "0", str.TrimEnd(','));
                context.Response.End();
                return;
            } 
            
        }
         
    }

    public void ReturnMsg(HttpContext context,string status, string msg)
    { 
        context.Response.Write("{\"status\":\"" + status + "\",\"msg\":[" + msg + "]}");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}