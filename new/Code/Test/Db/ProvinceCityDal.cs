using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Db
{
    public class ProvinceCityDal
    {
        public string conn = SqlHelper.ConnectionString;

        public int AddProvince(Model.ProvinceModel model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" insert into s_Province(ProvinceName) values(");
            strSql.Append("@ProvinceName)");
            SqlParameter[] parameters = {	 
	 
                    new SqlParameter("@ProvinceName", model.ProvinceName)       
                 };
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }


        public int UpdateProvince(Model.ProvinceModel model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update s_Province set ProvinceName=@ProvinceName  where ProvinceId=@ProvinceId");
 
            SqlParameter[] parameters = {	 
                    new SqlParameter("@ProvinceName", model.ProvinceName),
					new SqlParameter("@ProvinceId", model.ProvinceID)
                     
                 };
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }



        public int AddCity(Model.ProvinceModel model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" insert into S_City(ProvinceID,CityName,ZipCode,SupplierId) values(");
            strSql.Append("@ProvinceID,@CityName,@ZipCode,@SupplierId)");
            SqlParameter[] parameters = {	 
					new SqlParameter("@ProvinceID", model.ProvinceID),
                    new SqlParameter("@CityName", model.CityName),
                    new SqlParameter("@ZipCode", model.ZipCode), 
                    new SqlParameter("@SupplierId", model.SupplierId) 
                     
                 };
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }
        public int AddDistrict(Model.ProvinceModel model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" insert into S_District(DistrictName,CityId) values(");
            strSql.Append("@DistrictName,@CityId)");
            SqlParameter[] parameters = {	 
					new SqlParameter("@DistrictName", model.DistrictName),
                    new SqlParameter("@CityId", model.CityId)
                 };
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }


        public int UpdateCity(Model.ProvinceModel model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update S_City set ProvinceID=@ProvinceID,CityName=@CityName,ZipCode=@ZipCode,SupplierId=@SupplierId where CityId=@CityId ");

            SqlParameter[] parameters = {	 
					new SqlParameter("@ProvinceID", model.ProvinceID),
                    new SqlParameter("@CityName", model.CityName),
                  
                        new SqlParameter("@ZipCode", model.ZipCode),
                        new SqlParameter("@SupplierId", model.SupplierId),
                     new SqlParameter("@CityId", model.CityId)
                     
                 };
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }

        public int UpdateDistrict(Model.ProvinceModel model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update S_District set DistrictName=@DistrictName,CityId=@CityId where CityId=@CityId ");

            SqlParameter[] parameters = {	 
					new SqlParameter("@DistrictName", model.DistrictName),
                     new SqlParameter("@CityId", model.CityId)
                     
                 };
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }


        public DataTable GetProvinceList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder("select * from S_Province where 1=1 " + sqlwhere + "  ");
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }


        public DataTable GetCityExcelList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder("select a.*,sp.ProvinceName,si.SupplierName from s_City a left join S_Province sp on sp.ProvinceID=a.ProvinceID left join SupplierInfo si on si.SupplierId=a.SupplierId " +
                " where 1=1 "+sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }

        public DataTable GetCityList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder("select * from s_City where 1=1 " + sqlwhere + " ");
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }


        public DataTable GetDistrictList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder("select * from s_District where 1=1 " + sqlwhere + " ");
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }

        public int DelProvince(int pid)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select count(cityId)   from s_City where   ProvinceID=" + pid);
            if(Convert.ToInt32( SqlHelper.ExecuteScalar(conn,CommandType.Text,sql.ToString()).ToString()) >0)
            {
                return 0;
            }
            else
            {
                string sqls = "delete from S_Province  where ProvinceID=" + pid;
                return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sqls);
            }
        }


        public int DelCity(int cityid)
        {


            StringBuilder sql = new StringBuilder();
            sql.Append("select count(DistrictID) from s_District where  CityId=" + cityid);
            if (Convert.ToInt32(SqlHelper.ExecuteScalar(conn, CommandType.Text, sql.ToString()).ToString()) > 0)
            {
                return 0;
            }
            else
            {
                string sqls = "delete from s_city  where cityid=" + cityid;
                return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sqls);
            }
        }

        public int DelDistrict(int id)
        {
            string sqls = "delete from s_District where DistrictId=" + id;
                return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sqls);
        }

        public List<Model.ProvinceModel> GetProvinceModelList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from s_Province where 1=1 " + sqlwhere);
            List<Model.ProvinceModel> result = new List<Model.ProvinceModel>();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            while (dr.Read())
            {
                Model.ProvinceModel model = new Model.ProvinceModel();
                model.ProvinceID = DbTool.ConvertObject<System.Int32>(dr["ProvinceID"].ToString());
                model.ProvinceName =DbTool.ConvertObject<System.String>(dr["ProvinceName"].ToString());

                result.Add(model);
            }
            dr.Close();
            return result;
        }

        public List<Model.ProvinceModel> GetCityModelList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from s_city where 1=1 " + sqlwhere);
            List<Model.ProvinceModel> result = new List<Model.ProvinceModel>();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            while (dr.Read())
            {
                Model.ProvinceModel model = new Model.ProvinceModel();
                model.CityId = DbTool.ConvertObject<System.Int32>(dr["CityID"].ToString());
                model.CityName = DbTool.ConvertObject<System.String>(dr["CityName"].ToString());

                result.Add(model);
            }
            dr.Close();
            return result;
        }

        public List<Model.ProvinceModel> GetDistrictModelList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from s_District where 1=1 " + sqlwhere);
            List<Model.ProvinceModel> result = new List<Model.ProvinceModel>();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            while (dr.Read())
            {
                Model.ProvinceModel model = new Model.ProvinceModel();
                model.DistrictID = DbTool.ConvertObject<System.Int32>(dr["DistrictID"].ToString());
                model.DistrictName = DbTool.ConvertObject<System.String>(dr["DistrictName"].ToString());

                result.Add(model);
            }
            dr.Close();
            return result;
        }

        public Model.ProvinceModel GetDistrictModel(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select* from s_District where  DistrictID=" + id);
            Model.ProvinceModel model = new Model.ProvinceModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            if (dr.Read())
            {
                model.DistrictID = Convert.ToInt32(dr["DistrictID"].ToString());
                model.DistrictName = dr["DistrictName"].ToString();
                model.CityId = Convert.ToInt32(dr["CityId"].ToString());

            }
		    dr.Close();
            return model;
        }

        public Model.ProvinceModel GetCityModel(string cityName)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select top 1 * from s_city where  CityName='" + cityName + "'");
            Model.ProvinceModel model = new Model.ProvinceModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            if (dr.Read())
            {
                model.CityId = Convert.ToInt32(dr["CityId"].ToString());
                model.CityName = dr["CityName"].ToString();
                model.ZipCode = Convert.ToInt32(dr["ZipCode"].ToString());
                model.SupplierId = Convert.ToInt32(dr["SupplierId"].ToString());

                model.ProvinceID = Convert.ToInt32(dr["ProvinceID"].ToString());

            } dr.Close();
            return model;
        }

        public Model.ProvinceModel GetCityModel(int cityId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from s_city where  cityid=" + cityId);
            Model.ProvinceModel model = new Model.ProvinceModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            if (dr.Read())
            {
                model.CityId = Convert.ToInt32(dr["CityId"].ToString());
                model.CityName = dr["CityName"].ToString();
                model.ZipCode = Convert.ToInt32(dr["ZipCode"].ToString());
                model.SupplierId = Convert.ToInt32(dr["SupplierId"].ToString());

                model.ProvinceID = Convert.ToInt32(dr["ProvinceID"].ToString());

            } dr.Close();
            return model;
        }


        public Model.ProvinceModel GetProvinceModel(int pid)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select  *  from s_Province  where  ProvinceID=" + pid);
            Model.ProvinceModel model = new Model.ProvinceModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            if (dr.Read())
            {
                model.ProvinceID = Convert.ToInt32(dr["ProvinceID"].ToString());
                model.ProvinceName = dr["ProvinceName"].ToString();
            }
            return model;
        }

        public Model.ProvinceModel GetProvinceModel1(string ProvinceName)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select  *  from s_Province  where  ProvinceName='" + ProvinceName + "'");
            Model.ProvinceModel model = new Model.ProvinceModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            if (dr.Read())
            {
                model.ProvinceID = Convert.ToInt32(dr["ProvinceID"].ToString());
                model.ProvinceName = dr["ProvinceName"].ToString();
            }
            return model;
        }
        public Model.ProvinceModel GetCityModel1(int pid,string CityName)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select  *  from s_City  where ProvinceID="+pid+" and  CityName='" + CityName + "'");
            Model.ProvinceModel model = new Model.ProvinceModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            if (dr.Read())
            {
                model.CityId = Convert.ToInt32(dr["CityId"].ToString());
                model.CityName = dr["CityName"].ToString();
                model.ProvinceID = Convert.ToInt32(dr["ProvinceID"].ToString());
            }
            return model;
        }

        public Model.ProvinceModel GetDistrictModel1(int cid, string DistrictName)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select  *  from S_District  where CityId=" + cid + " and  DistrictName='" + DistrictName + "'");
            Model.ProvinceModel model = new Model.ProvinceModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            if (dr.Read())
            {
                model.DistrictID = Convert.ToInt32(dr["DistrictID"].ToString());
                model.DistrictName = dr["DistrictName"].ToString();
                model.CityId = Convert.ToInt32(dr["CityId"].ToString());
            }
            return model;
        }

        public int UpdateC(int cid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update s_city set SupplierId=@SupplierId  where CityId=@CityId");

            SqlParameter[] parameters = {	 
                    new SqlParameter("@SupplierId", 1),
					new SqlParameter("@CityId", cid)
                     
                 };
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
        }



    }
}
