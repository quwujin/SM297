using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data; 

namespace Db
{
    public class DistanceDal
    {
        public string conn = SqlHelper.ConnectionString;

        
        
        #region Dal Core Functional

        #region Add
        public int Add(Model.DistanceModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into  [Distance]");
            strSql.Append("(Lng,Lat,Province,City,District,Address,StorName,Channel,CreateTime,StatusId,Describe,Note)");
            strSql.Append(" values (@Lng,@Lat,@Province,@City,@District,@Address,@StorName,@Channel,@CreateTime,@StatusId,@Describe,@Note)");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
					new SqlParameter("@Lng", DbTool.FixSqlParameter(model.Lng))
,					new SqlParameter("@Lat", DbTool.FixSqlParameter(model.Lat))
,					new SqlParameter("@Province", DbTool.FixSqlParameter(model.Province))
,					new SqlParameter("@City", DbTool.FixSqlParameter(model.City))
,					new SqlParameter("@District", DbTool.FixSqlParameter(model.District))
,					new SqlParameter("@Address", DbTool.FixSqlParameter(model.Address))
,					new SqlParameter("@StorName", DbTool.FixSqlParameter(model.StorName))
,					new SqlParameter("@Channel", DbTool.FixSqlParameter(model.Channel))
,					new SqlParameter("@CreateTime", DbTool.FixSqlParameter(model.CreateTime))
,					new SqlParameter("@StatusId", DbTool.FixSqlParameter(model.StatusId))
,					new SqlParameter("@Describe", DbTool.FixSqlParameter(model.Describe))
,					new SqlParameter("@Note", DbTool.FixSqlParameter(model.Note))
                 };


            return DbTool.ConvertObject<int>(SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters),0);
         
        }
         
         
         #endregion
         
        #region Update
        public int Update(Model.DistanceModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Distance set ");
            strSql.Append("Lng=@Lng,Lat=@Lat,Province=@Province,City=@City,District=@District,Address=@Address,StorName=@StorName,Channel=@Channel,CreateTime=@CreateTime,StatusId=@StatusId,Describe=@Describe,Note=@Note ");
            strSql.Append(" where Id=@Id ");
       
            SqlParameter[] parameters = {
					new SqlParameter("@Lng", DbTool.FixSqlParameter(model.Lng))
,					new SqlParameter("@Lat", DbTool.FixSqlParameter(model.Lat))
,					new SqlParameter("@Province", DbTool.FixSqlParameter(model.Province))
,					new SqlParameter("@City", DbTool.FixSqlParameter(model.City))
,					new SqlParameter("@District", DbTool.FixSqlParameter(model.District))
,					new SqlParameter("@Address", DbTool.FixSqlParameter(model.Address))
,					new SqlParameter("@StorName", DbTool.FixSqlParameter(model.StorName))
,					new SqlParameter("@Channel", DbTool.FixSqlParameter(model.Channel))
,					new SqlParameter("@CreateTime", DbTool.FixSqlParameter(model.CreateTime))
,					new SqlParameter("@StatusId", DbTool.FixSqlParameter(model.StatusId))
,					new SqlParameter("@Describe", DbTool.FixSqlParameter(model.Describe))
,					new SqlParameter("@Note", DbTool.FixSqlParameter(model.Note))
,					new SqlParameter("@Id", model.Id)
                 };


            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }
        public int UpdateLngLat(Model.DistanceModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Distance set ");
            strSql.Append("Lng=@Lng,Lat=@Lat");
            strSql.Append(" where Id=@Id ");

            SqlParameter[] parameters = {
					new SqlParameter("@Lng", DbTool.FixSqlParameter(model.Lng))
,					new SqlParameter("@Lat", DbTool.FixSqlParameter(model.Lat)) 
,					new SqlParameter("@Id", model.Id)
                 };


            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }
         #endregion
         
        #region Delete
        public int Del(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from Distance where Id = " + id);
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region BindDataReader
        protected void BindDataReader(Model.DistanceModel model,SqlDataReader dr)
        {

                model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
                model.Lng = DbTool.ConvertObject<System.Decimal>(dr["Lng"]);
                model.Lat = DbTool.ConvertObject<System.Decimal>(dr["Lat"]);
                model.Province = DbTool.ConvertObject<System.String>(dr["Province"]);
                model.City = DbTool.ConvertObject<System.String>(dr["City"]);
                model.District = DbTool.ConvertObject<System.String>(dr["District"]);
                model.Address = DbTool.ConvertObject<System.String>(dr["Address"]);
                model.StorName = DbTool.ConvertObject<System.String>(dr["StorName"]);
                model.Channel = DbTool.ConvertObject<System.String>(dr["Channel"]);
                model.CreateTime = DbTool.ConvertObject<System.DateTime>(dr["CreateTime"]);
                model.StatusId = DbTool.ConvertObject<System.Int32>(dr["StatusId"]);
                model.Describe = DbTool.ConvertObject<System.String>(dr["Describe"]);
                model.Note = DbTool.ConvertObject<System.String>(dr["Note"]);


        }
         #endregion
         
        #region AutoBindDataReader
        protected Model.DistanceModel AutoBindDataReader(SqlDataReader dr, params string[] fields)
        {

           var model = new Model.DistanceModel();
                if (DbTool.HasFields("Id", fields)) model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
                if (DbTool.HasFields("Lng", fields)) model.Lng = DbTool.ConvertObject<System.Decimal>(dr["Lng"]);
                if (DbTool.HasFields("Lat", fields)) model.Lat = DbTool.ConvertObject<System.Decimal>(dr["Lat"]);
                if (DbTool.HasFields("Province", fields)) model.Province = DbTool.ConvertObject<System.String>(dr["Province"]);
                if (DbTool.HasFields("City", fields)) model.City = DbTool.ConvertObject<System.String>(dr["City"]);
                if (DbTool.HasFields("District", fields)) model.District = DbTool.ConvertObject<System.String>(dr["District"]);
                if (DbTool.HasFields("Address", fields)) model.Address = DbTool.ConvertObject<System.String>(dr["Address"]);
                if (DbTool.HasFields("StorName", fields)) model.StorName = DbTool.ConvertObject<System.String>(dr["StorName"]);
                if (DbTool.HasFields("Channel", fields)) model.Channel = DbTool.ConvertObject<System.String>(dr["Channel"]);
                if (DbTool.HasFields("CreateTime", fields)) model.CreateTime = DbTool.ConvertObject<System.DateTime>(dr["CreateTime"]);
                if (DbTool.HasFields("StatusId", fields)) model.StatusId = DbTool.ConvertObject<System.Int32>(dr["StatusId"]);
                if (DbTool.HasFields("Describe", fields)) model.Describe = DbTool.ConvertObject<System.String>(dr["Describe"]);
                if (DbTool.HasFields("Note", fields)) model.Note = DbTool.ConvertObject<System.String>(dr["Note"]);

           return model;

        }
         #endregion
         
        #endregion 

        #region GetList
        public DataTable GetList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from Distance where 1=1 ");
            sql.Append(sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
         #endregion

        #region 根据经纬度查找最近的门店
        public DataTable GetStorList(string lng, string lat, int km, int num)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT top (@num) * from [dbo].Distance a where (6371.004*ACOS(SIN(@GPSLat/180*PI())*SIN(a.Lat/180*PI())+COS(@GPSLat/180*PI())*COS(a.Lat/180*PI())*COS((@GPSLng-a.Lng)/180*PI())))<@km");
             
            SqlParameter[] parameters = {
                                            new SqlParameter("@GPSLng", DbTool.ConvertObject<System.Decimal>(lng,0)),
                                            new SqlParameter("@GPSLat",DbTool.ConvertObject<System.Decimal>(lat,0)),
                                            new SqlParameter("@km", DbTool.FixSqlParameter(km)),
                                            new SqlParameter("@num", DbTool.FixSqlParameter(num)),
                                        };

            DataTable ds = SqlHelper.ExecuteDataTable(conn, CommandType.Text, strSql.ToString(), parameters);

            return ds;
        }
        #endregion
        
        #region GetModel
        public Model.DistanceModel GetModel(int Id)
        {

            string sql = "select top 1 * from Distance where Id =" + Id;
            Model.DistanceModel model = new Model.DistanceModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            if (dr.Read()) {
                 //var fields = DbTool.GetReaderFieldNames(dr);
                 //model = AutoBindDataReader(dr, fields);
                 BindDataReader(model, dr);
            }
            dr.Close();
            return model;
        }
         #endregion
         
        #region GetModelList
        public List<Model.DistanceModel> GetModelList(string sqlwhere)
        {

            List<Model.DistanceModel> result = new List<Model.DistanceModel>();
            string sql = "select * from Distance where 1=1 " + sqlwhere;
            Model.DistanceModel model = new Model.DistanceModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            //var fields = DbTool.GetReaderFieldNames(dr);
            while (dr.Read())
            {
                 //model = AutoBindDataReader(dr, fields);
                 model = new Model.DistanceModel(); 
                 BindDataReader(model, dr);
                 result.Add(model);
            }
            dr.Close();
            return result;
        }
         #endregion
         
        #region CheckCount
        public int CheckCount(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select count(*) from Distance where 1=1 ");
            sql.Append(sqlwhere);
            return DbTool.ConvertObject<int>(SqlHelper.ExecuteScalar(conn, CommandType.Text, sql.ToString()), 0);
        }
         #endregion
         
        #region 分页计算总数
         public int GetCount(string sqlstr, string joinString)
        {
            Model.PageInfo pages = new Model.PageInfo();
            pages.SqlWhere = sqlstr;
            pages.ReturnFileds = "Id";
            pages.SqlWhere = sqlstr;
            pages.TableName = "Distance";
            pages.JoinTable = "   ";
            pages.CountFields = " a.Id ";
            pages.OrderString = " ";
            pages.SelectFileds = "  a.* ";
            pages.doCount = 1;
            PageHelper p = new PageHelper();
            DataTable dt = p.GetList(pages);
            if (dt.Rows.Count > 0)
            {
             return Convert.ToInt32(dt.Rows[0][0].ToString());
            }
            else
            {
             return 0;
            }
        }
        #endregion
         
        #region 分页计算GetList
        public DataTable GetList(string sqlstr, int pageindex, int pagesize)
        {
           Model.PageInfo pages = new Model.PageInfo();
           pages.PageIndex = pageindex;
           pages.PageSize = pagesize;
           pages.SqlWhere = sqlstr;
           pages.ReturnFileds = "t.*";
           pages.TableName = "Distance";
           pages.JoinTable = " ";
           pages.CountFields = " a.Id ";
           pages.OrderString = " order by a.Id desc";
           pages.SelectFileds = " a.* ";
           pages.doCount = 0;
           PageHelper p = new PageHelper();
           DataTable dt = p.GetList(pages);
           return dt;
         }
         #endregion
         
        #region GetExcelList
        public DataTable GetExcelList(string sqlstr)
        {
           StringBuilder sql = new StringBuilder("select a.* from Distance a  where 1=1 " + sqlstr);
           return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
        #endregion
         

        
 
    }
}
