<%@ WebHandler Language="C#" Class="getBaseCity" %>

using System;
using System.Web;
using System.Data;
using System.Collections.Generic;

public class getBaseCity : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";

        Db.ProvinceCityDal distancedal = new Db.ProvinceCityDal();

        string GetType = context.Request["GetType"]; 

        string str = "";

        if (string.IsNullOrEmpty(GetType) == false && GetType == "Prov") {

            int ProvId = Common.TypeHelper.ObjectToInt(context.Request["ProvId"], 0);

            if (ProvId == 0) {
                context.Response.Write("-1");
                context.Response.End();
                return;
            }

            List<Model.ProvinceModel> CityList = distancedal.GetCityModelList(string.Format(" and ProvinceID='{0}'", ProvId));
            foreach (Model.ProvinceModel model in CityList)
            {
                str += "<li><input type=\"button\" class=\"sel_btn\" data=\"" + model.CityId + "\" value=\"" + model.CityName + "\" /></li>";
            }

            context.Response.Write(str);
            context.Response.End();
            return;
        }

        if (string.IsNullOrEmpty(GetType) == false && GetType == "City")
        {

            int CityId = Common.TypeHelper.ObjectToInt(context.Request["CityId"], 0);

            if (CityId == 0)
            {
                context.Response.Write("-1");
                context.Response.End();
                return;
            }

            List<Model.ProvinceModel> DistList = distancedal.GetDistrictModelList(string.Format(" and CityID='{0}'", CityId));
            foreach (Model.ProvinceModel model in DistList)
            {
                str += "<li><input type=\"button\" class=\"sel_btn\" data=\"" + model.DistrictID + "\" value=\"" + model.DistrictName + "\" /></li>";
            }

            context.Response.Write(str);
            context.Response.End();
            return;
        }

        if (string.IsNullOrEmpty(GetType) == false && GetType == "Dist")
        {
            string CityName = context.Request["CityName"];

            if (string.IsNullOrEmpty(CityName) || Common.ValidateHelper.IsName(CityName) == false) {
                context.Response.Write("-1");
                context.Response.End();
                return;
            }

            Model.ProvinceModel mCityModel = distancedal.GetCityModel(CityName);

            if (mCityModel.CityId <= 0) {
                context.Response.Write("-1");
                context.Response.End();
                return;
            }

            DataTable lotlist = distancedal.GetDistrictList(string.Format(" and CityID ='{0}'", mCityModel.CityId));

            if (lotlist.Rows.Count > 0)
            {
                foreach (DataRow model in lotlist.Rows)
                {
                    str += "<li><input type=\"button\" class=\"sel_btn\" data=\"" + model["DistrictID"] + "\"  value=\"" + model["DistrictName"] + "\" /></li> ";
                }
                context.Response.Write(str);
                context.Response.End();
                return;
            }  
             
        }

        context.Response.Write("-1");
        context.Response.End();
        return;
         
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