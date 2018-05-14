using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data; 

namespace Db.Security
{
    public class IpAccessControlLogDal
    {
        public string conn = SqlHelper.ConnectionString;

        
        
        #region Dal Core Functional

        #region Add
        public int Add(Model.Security.IpAccessControlLogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into  [IpAccessControlLog]");
            strSql.Append("(IpAddress,LogType,LockedDate,CreateOn,LockValue,LockReason,SourceURL)");
            strSql.Append(" values (@IpAddress,@LogType,@LockedDate,@CreateOn,@LockValue,@LockReason,@SourceURL)");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
					new SqlParameter("@IpAddress", DbTool.FixSqlParameter(model.IpAddress))
,					new SqlParameter("@LogType", DbTool.FixSqlParameter(model.LogType))
,					new SqlParameter("@LockedDate", DbTool.FixSqlParameter(model.LockedDate))
,					new SqlParameter("@CreateOn", DbTool.FixSqlParameter(model.CreateOn))
,					new SqlParameter("@LockValue", DbTool.FixSqlParameter(model.LockValue))
,					new SqlParameter("@LockReason", DbTool.FixSqlParameter(model.LockReason))
,					new SqlParameter("@SourceURL", DbTool.FixSqlParameter(model.SourceURL))
                 };


            return DbTool.ConvertObject<int>(SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters),0);
         
        }
         
         
         #endregion
         
        #region Update
        public int Update(Model.Security.IpAccessControlLogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update IpAccessControlLog set ");
            strSql.Append("IpAddress=@IpAddress,LogType=@LogType,LockedDate=@LockedDate,CreateOn=@CreateOn,LockValue=@LockValue,LockReason=@LockReason,SourceURL=@SourceURL ");
            strSql.Append(" where Id=@Id ");
       
            SqlParameter[] parameters = {
					new SqlParameter("@IpAddress", DbTool.FixSqlParameter(model.IpAddress))
,					new SqlParameter("@LogType", DbTool.FixSqlParameter(model.LogType))
,					new SqlParameter("@LockedDate", DbTool.FixSqlParameter(model.LockedDate))
,					new SqlParameter("@CreateOn", DbTool.FixSqlParameter(model.CreateOn))
,					new SqlParameter("@LockValue", DbTool.FixSqlParameter(model.LockValue))
,					new SqlParameter("@LockReason", DbTool.FixSqlParameter(model.LockReason))
,					new SqlParameter("@SourceURL", DbTool.FixSqlParameter(model.SourceURL))
,					new SqlParameter("@Id", model.Id)
                 };


            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }
         #endregion
         
        #region Delete
        public int Del(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from IpAccessControlLog where Id = " + id);
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region BindDataReader
        protected void BindDataReader(Model.Security.IpAccessControlLogModel model,SqlDataReader dr)
        {

                model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
                model.IpAddress = DbTool.ConvertObject<System.String>(dr["IpAddress"]);
                model.LogType = DbTool.ConvertObject<System.String>(dr["LogType"]);
                model.LockedDate = DbTool.ConvertObject<System.DateTime>(dr["LockedDate"]);
                model.CreateOn = DbTool.ConvertObject<System.DateTime>(dr["CreateOn"]);
                model.LockValue = DbTool.ConvertObject<System.String>(dr["LockValue"]);
                model.LockReason = DbTool.ConvertObject<System.String>(dr["LockReason"]);
                model.SourceURL = DbTool.ConvertObject<System.String>(dr["SourceURL"]);


        }
         #endregion
         
        #region AutoBindDataReader
        protected Model.Security.IpAccessControlLogModel AutoBindDataReader(SqlDataReader dr, params string[] fields)
        {

           var model = new Model.Security.IpAccessControlLogModel();
                if (DbTool.HasFields("Id", fields)) model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
                if (DbTool.HasFields("IpAddress", fields)) model.IpAddress = DbTool.ConvertObject<System.String>(dr["IpAddress"]);
                if (DbTool.HasFields("LogType", fields)) model.LogType = DbTool.ConvertObject<System.String>(dr["LogType"]);
                if (DbTool.HasFields("LockedDate", fields)) model.LockedDate = DbTool.ConvertObject<System.DateTime>(dr["LockedDate"]);
                if (DbTool.HasFields("CreateOn", fields)) model.CreateOn = DbTool.ConvertObject<System.DateTime>(dr["CreateOn"]);
                if (DbTool.HasFields("LockValue", fields)) model.LockValue = DbTool.ConvertObject<System.String>(dr["LockValue"]);
                if (DbTool.HasFields("LockReason", fields)) model.LockReason = DbTool.ConvertObject<System.String>(dr["LockReason"]);
                if (DbTool.HasFields("SourceURL", fields)) model.SourceURL = DbTool.ConvertObject<System.String>(dr["SourceURL"]);

           return model;

        }
         #endregion
         
        #endregion 

        #region GetList
        public DataTable GetList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from IpAccessControlLog where 1=1 ");
            sql.Append(sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region GetModel
        public Model.Security.IpAccessControlLogModel GetModel(int Id)
        {

            string sql = "select top 1 * from IpAccessControlLog where Id =" + Id;
            Model.Security.IpAccessControlLogModel model = new Model.Security.IpAccessControlLogModel();
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
        public List<Model.Security.IpAccessControlLogModel> GetModelList()
        {

            List<Model.Security.IpAccessControlLogModel> result = new List<Model.Security.IpAccessControlLogModel>();
            string sql = "select * from IpAccessControlLog where 1=1";
            Model.Security.IpAccessControlLogModel model = new Model.Security.IpAccessControlLogModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            //var fields = DbTool.GetReaderFieldNames(dr);
            while (dr.Read())
            {
                 //model = AutoBindDataReader(dr, fields);
                 model = new Model.Security.IpAccessControlLogModel(); 
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
            sql.Append("select count(1) from IpAccessControlLog where 1=1 ");
            sql.Append(sqlwhere);
            return DbTool.ConvertObject<int>(SqlHelper.ExecuteScalar(conn, CommandType.Text, sql.ToString()), 0);
        }
         #endregion
           
        
 
    }
}
