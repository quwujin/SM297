using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GetDistance_GetDistance : System.Web.UI.Page
{
    Db.DistanceDal Disdal = new Db.DistanceDal();
    protected void Page_Load(object sender, EventArgs e)
    {

        //获取所有地址的经纬度

        List<Model.DistanceModel> mDistanceList = Disdal.GetModelList(" and (len(Lng) is null or len(Lng)=0)");

        foreach (Model.DistanceModel model in mDistanceList) {
            GetGeocoding(model);
        }

    }

    public void GetGeocoding(Model.DistanceModel model)
    {
        string Address = model.City + model.District + model.Address;

        Common.Location lnglat = Common.GetGeocoding.GetLngLat(Address, model.Province);

        if (lnglat != null) {
            model.Lat = lnglat.lat;
            model.Lng = lnglat.lng;

            Disdal.UpdateLngLat(model);
        }

        

    }

}