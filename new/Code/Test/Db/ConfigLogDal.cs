using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Db
{
    public class ConfigLogDal
    {
        public string conn = SqlHelper.ConnectionString;

        
        
        #region Dal Core Functional

        #region Add
        public int Add(Model.ConfigLogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into  [ConfigLog]");
            strSql.Append("(ConfigId,UserId,Title,Note,CTime)");
            strSql.Append(" values (@ConfigId,@UserId,@Title,@Note,@CTime)");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
					new SqlParameter("@ConfigId", DbTool.FixSqlParameter(model.ConfigId))
,					new SqlParameter("@UserId", DbTool.FixSqlParameter(model.UserId))
,					new SqlParameter("@Title", DbTool.FixSqlParameter(model.Title))
,					new SqlParameter("@Note", DbTool.FixSqlParameter(model.Note))
,					new SqlParameter("@CTime", DbTool.FixSqlParameter(model.CTime))
                 };


            return DbTool.ConvertObject<int>(SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters),0);
         
        }
         
         
         #endregion
         
        #region Update
        public int Update(Model.ConfigLogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ConfigLog set ");
            strSql.Append("ConfigId=@ConfigId,UserId=@UserId,Title=@Title,Note=@Note,CTime=@CTime ");
            strSql.Append(" where Id=@Id ");
       
            SqlParameter[] parameters = {
					new SqlParameter("@ConfigId", DbTool.FixSqlParameter(model.ConfigId))
,					new SqlParameter("@UserId", DbTool.FixSqlParameter(model.UserId))
,					new SqlParameter("@Title", DbTool.FixSqlParameter(model.Title))
,					new SqlParameter("@Note", DbTool.FixSqlParameter(model.Note))
,					new SqlParameter("@CTime", DbTool.FixSqlParameter(model.CTime))
,					new SqlParameter("@Id", model.Id)
                 };


            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }
         #endregion
         
        #region Delete
        public int Del(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from ConfigLog where Id = " + id);
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region BindDataReader
        protected void BindDataReader(Model.ConfigLogModel model,SqlDataReader dr)
        {

                model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
                model.ConfigId = DbTool.ConvertObject<System.Int32>(dr["ConfigId"]);
                model.UserId = DbTool.ConvertObject<System.Int32>(dr["UserId"]);
                model.Title = DbTool.ConvertObject<System.String>(dr["Title"]);
                model.Note = DbTool.ConvertObject<System.String>(dr["Note"]);
                model.CTime = DbTool.ConvertObject<System.DateTime>(dr["CTime"]);


        }
         #endregion
         
        #region AutoBindDataReader
        protected Model.ConfigLogModel AutoBindDataReader(SqlDataReader dr, params string[] fields)
        {

           var model = new Model.ConfigLogModel();
                if (DbTool.HasFields("Id", fields)) model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
                if (DbTool.HasFields("ConfigId", fields)) model.ConfigId = DbTool.ConvertObject<System.Int32>(dr["ConfigId"]);
                if (DbTool.HasFields("UserId", fields)) model.UserId = DbTool.ConvertObject<System.Int32>(dr["UserId"]);
                if (DbTool.HasFields("Title", fields)) model.Title = DbTool.ConvertObject<System.String>(dr["Title"]);
                if (DbTool.HasFields("Note", fields)) model.Note = DbTool.ConvertObject<System.String>(dr["Note"]);
                if (DbTool.HasFields("CTime", fields)) model.CTime = DbTool.ConvertObject<System.DateTime>(dr["CTime"]);

           return model;

        }
         #endregion
         
        #endregion 

        #region GetList
        public DataTable GetList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from ConfigLog where 1=1 ");
            sql.Append(sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        /// <summary>
        /// 获取记录数
        /// </summary>
        public int GetRowCount()
        {
            string sql = "select count(*) from ConfigLog ";

            var obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, sql,null); 
            return DbTool.ConvertObject<int>(obj,0);
        }

        #region GetModel
        public Model.ConfigLogModel GetModel(int Id)
        {

            string sql = "select top 1 * from ConfigLog where Id =" + Id;
            Model.ConfigLogModel model = new Model.ConfigLogModel();
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
        public List<Model.ConfigLogModel> GetModelList()
        {

            List<Model.ConfigLogModel> result = new List<Model.ConfigLogModel>();
            string sql = "select * from ConfigLog where 1=1";
            Model.ConfigLogModel model = new Model.ConfigLogModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            //var fields = DbTool.GetReaderFieldNames(dr);
            while (dr.Read())
            {
                 //model = AutoBindDataReader(dr, fields);
                 model = new Model.ConfigLogModel(); 
                 BindDataReader(model, dr);
                 result.Add(model);
            }
            dr.Close();
            return result;
        }
         #endregion
         

        
 
    }
}
