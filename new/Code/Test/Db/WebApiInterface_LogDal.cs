using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data; 

namespace Db
{
    public class WebApiInterface_LogDal
    {
        public string conn = SqlHelper.ConnectionString;

        
        
        #region Dal Core Functional

        #region Add
        public int Add(Model.WebApiInterface_LogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into  [WebApiInterface_Log]");
            strSql.Append("(OrderId,ResponseData,CreateTime,StatusId,Remark)");
            strSql.Append(" values (@OrderId,@ResponseData,@CreateTime,@StatusId,@Remark)");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderId", DbTool.FixSqlParameter(model.OrderId))
,					new SqlParameter("@ResponseData", DbTool.FixSqlParameter(model.ResponseData))
,					new SqlParameter("@CreateTime", DbTool.FixSqlParameter(model.CreateTime))
,					new SqlParameter("@StatusId", DbTool.FixSqlParameter(model.StatusId))
,					new SqlParameter("@Remark", DbTool.FixSqlParameter(model.Remark))
                 };


            return DbTool.ConvertObject<int>(SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters),0);
         
        }
         
         
         #endregion
         
        #region Update
        public int Update(Model.WebApiInterface_LogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WebApiInterface_Log set ");
            strSql.Append("OrderId=@OrderId,ResponseData=@ResponseData,CreateTime=@CreateTime,StatusId=@StatusId,Remark=@Remark ");
            strSql.Append(" where Id=@Id ");
       
            SqlParameter[] parameters = {
					new SqlParameter("@OrderId", DbTool.FixSqlParameter(model.OrderId))
,					new SqlParameter("@ResponseData", DbTool.FixSqlParameter(model.ResponseData))
,					new SqlParameter("@CreateTime", DbTool.FixSqlParameter(model.CreateTime))
,					new SqlParameter("@StatusId", DbTool.FixSqlParameter(model.StatusId))
,					new SqlParameter("@Remark", DbTool.FixSqlParameter(model.Remark))
,					new SqlParameter("@Id", model.Id)
                 };


            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }
         #endregion
         
        #region Delete
        public int Del(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from WebApiInterface_Log where Id = " + id);
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region BindDataReader
        protected void BindDataReader(Model.WebApiInterface_LogModel model,SqlDataReader dr)
        {

                model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
                model.OrderId = DbTool.ConvertObject<System.Int32>(dr["OrderId"]);
                model.ResponseData = DbTool.ConvertObject<System.String>(dr["ResponseData"]);
                model.CreateTime = DbTool.ConvertObject<System.DateTime>(dr["CreateTime"]);
                model.StatusId = DbTool.ConvertObject<System.Int32>(dr["StatusId"]);
                model.Remark = DbTool.ConvertObject<System.String>(dr["Remark"]);


        }
         #endregion
         
        #region AutoBindDataReader
        protected Model.WebApiInterface_LogModel AutoBindDataReader(SqlDataReader dr, params string[] fields)
        {

           var model = new Model.WebApiInterface_LogModel();
                if (DbTool.HasFields("Id", fields)) model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
                if (DbTool.HasFields("OrderId", fields)) model.OrderId = DbTool.ConvertObject<System.Int32>(dr["OrderId"]);
                if (DbTool.HasFields("ResponseData", fields)) model.ResponseData = DbTool.ConvertObject<System.String>(dr["ResponseData"]);
                if (DbTool.HasFields("CreateTime", fields)) model.CreateTime = DbTool.ConvertObject<System.DateTime>(dr["CreateTime"]);
                if (DbTool.HasFields("StatusId", fields)) model.StatusId = DbTool.ConvertObject<System.Int32>(dr["StatusId"]);
                if (DbTool.HasFields("Remark", fields)) model.Remark = DbTool.ConvertObject<System.String>(dr["Remark"]);

           return model;

        }
         #endregion
         
        #endregion 

        #region GetList
        public DataTable GetList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from WebApiInterface_Log where 1=1 ");
            sql.Append(sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region GetModel
        public Model.WebApiInterface_LogModel GetModel(int Id)
        {
            string sql = "select top 1 * from WebApiInterface_Log where Id =" + Id;
            Model.WebApiInterface_LogModel model = new Model.WebApiInterface_LogModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            if (dr.Read()) {
                 //var fields = DbTool.GetReaderFieldNames(dr);
                 //model = AutoBindDataReader(dr, fields);
                 BindDataReader(model, dr);
            }
            dr.Close();
            return model;
        }
        public Model.WebApiInterface_LogModel GetModelByIdDesc()
        {
            string sql = "select top 1 * from WebApiInterface_Log order by id desc ";
            Model.WebApiInterface_LogModel model = new Model.WebApiInterface_LogModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            if (dr.Read())
            {
                //var fields = DbTool.GetReaderFieldNames(dr);
                //model = AutoBindDataReader(dr, fields);
                BindDataReader(model, dr);
            }
            dr.Close();
            return model;
        }
         #endregion
         
        #region GetModelList
        public List<Model.WebApiInterface_LogModel> GetModelList(string sqlwhere)
        {

            List<Model.WebApiInterface_LogModel> result = new List<Model.WebApiInterface_LogModel>();
            string sql = "select * from WebApiInterface_Log where 1=1 " + sqlwhere;
            Model.WebApiInterface_LogModel model = new Model.WebApiInterface_LogModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            //var fields = DbTool.GetReaderFieldNames(dr);
            while (dr.Read())
            {
                 //model = AutoBindDataReader(dr, fields);
                 model = new Model.WebApiInterface_LogModel(); 
                 BindDataReader(model, dr);
                 result.Add(model);
            }
            dr.Close();
            return result;
        }

        public List<Model.WebApiInterface_LogModel> GetModelByTopList(int num, string sqlwhere)
        {

            List<Model.WebApiInterface_LogModel> result = new List<Model.WebApiInterface_LogModel>();
            string sql = "select top " + num + " * from WebApiInterface_Log where 1=1 " + sqlwhere;
            Model.WebApiInterface_LogModel model = new Model.WebApiInterface_LogModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            //var fields = DbTool.GetReaderFieldNames(dr);
            while (dr.Read())
            {
                //model = AutoBindDataReader(dr, fields);
                model = new Model.WebApiInterface_LogModel();
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
            sql.Append("select count(*) from WebApiInterface_Log where 1=1 ");
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
            pages.TableName = "WebApiInterface_Log";
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
           pages.TableName = "WebApiInterface_Log";
           pages.JoinTable = " ";
           pages.CountFields = " a.Id ";
           pages.OrderString = " order by t.Id desc";
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
           StringBuilder sql = new StringBuilder("select a.* from WebApiInterface_Log a  where 1=1 " + sqlstr);
           return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
        #endregion
         

        
 
    }
}
