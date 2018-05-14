using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data; 

namespace Db.Security
{
    public class IpAccessControlSettingDal
    {
        public string conn = SqlHelper.ConnectionString;

        
        
        #region Dal Core Functional

        #region Add
        public int Add(Model.Security.IpAccessControlSettingModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into  [IpAccessControlSetting]");
            strSql.Append("(LogType,IPAccessEnable,IPAccessMaxCount,IPAccessControlTime,IPAccessControlLockTime)");
            strSql.Append(" values (@LogType,@IPAccessEnable,@IPAccessMaxCount,@IPAccessControlTime,@IPAccessControlLockTime)");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
					new SqlParameter("@LogType", DbTool.FixSqlParameter(model.LogType))
,					new SqlParameter("@IPAccessEnable", DbTool.FixSqlParameter(model.IPAccessEnable))
,					new SqlParameter("@IPAccessMaxCount", DbTool.FixSqlParameter(model.IPAccessMaxCount))
,					new SqlParameter("@IPAccessControlTime", DbTool.FixSqlParameter(model.IPAccessControlTime))
,					new SqlParameter("@IPAccessControlLockTime", DbTool.FixSqlParameter(model.IPAccessControlLockTime))
                 };


            return DbTool.ConvertObject<int>(SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters),0);
         
        }
         
         
         #endregion
         
        #region Update
        public int Update(Model.Security.IpAccessControlSettingModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update IpAccessControlSetting set ");
            strSql.Append("LogType=@LogType,IPAccessEnable=@IPAccessEnable,IPAccessMaxCount=@IPAccessMaxCount,IPAccessControlTime=@IPAccessControlTime,IPAccessControlLockTime=@IPAccessControlLockTime ");
            strSql.Append(" where Id=@Id ");
       
            SqlParameter[] parameters = {
					new SqlParameter("@LogType", DbTool.FixSqlParameter(model.LogType))
,					new SqlParameter("@IPAccessEnable", DbTool.FixSqlParameter(model.IPAccessEnable))
,					new SqlParameter("@IPAccessMaxCount", DbTool.FixSqlParameter(model.IPAccessMaxCount))
,					new SqlParameter("@IPAccessControlTime", DbTool.FixSqlParameter(model.IPAccessControlTime))
,					new SqlParameter("@IPAccessControlLockTime", DbTool.FixSqlParameter(model.IPAccessControlLockTime))
,					new SqlParameter("@Id", model.Id)
                 };


            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }
         #endregion
         
        #region Delete
        public int Del(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from IpAccessControlSetting where Id = " + id);
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region BindDataReader
        protected void BindDataReader(Model.Security.IpAccessControlSettingModel model, SqlDataReader dr)
        {

                model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
                model.LogType = DbTool.ConvertObject<System.String>(dr["LogType"]);
                model.IPAccessEnable = DbTool.ConvertObject<System.Boolean>(dr["IPAccessEnable"]);
                model.IPAccessMaxCount = DbTool.ConvertObject<System.Int32>(dr["IPAccessMaxCount"]);
                model.IPAccessControlTime = DbTool.ConvertObject<System.Int32>(dr["IPAccessControlTime"]);
                model.IPAccessControlLockTime = DbTool.ConvertObject<System.Int32>(dr["IPAccessControlLockTime"]);


        }
         #endregion
         
        #endregion 

        #region GetList
        public DataTable GetList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from IpAccessControlSetting where 1=1 ");
            sql.Append(sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region GetModel
        public Model.Security.IpAccessControlSettingModel GetModelByLogtype(string logtype)
        {

            string sql = "select top 1 * from IpAccessControlSetting where logtype ='" + logtype + "'";
            Model.Security.IpAccessControlSettingModel model = new Model.Security.IpAccessControlSettingModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            if (dr.Read()) {
                 BindDataReader(model, dr);
            }
            dr.Close();
            return model;
        }
         #endregion
         
        #region GetModelList
        public List<Model.Security.IpAccessControlSettingModel> GetModelList()
        {

            List<Model.Security.IpAccessControlSettingModel> result = new List<Model.Security.IpAccessControlSettingModel>();
            string sql = "select * from IpAccessControlSetting where 1=1";
            Model.Security.IpAccessControlSettingModel model ;
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            while (dr.Read())
            {
                model = new Model.Security.IpAccessControlSettingModel(); 
                 BindDataReader(model, dr);
                 result.Add(model);
            }
            dr.Close();
            return result;
        }
         #endregion
         

        
 
    }
}
