using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data; 

namespace Db
{
    public class Operation_LogDal
    {
        public string conn = SqlHelper.ConnectionString;

        
        
        #region Dal Core Functional

        #region Add
        public int Add(Model.Operation_LogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into  [Operation_Log]");
            strSql.Append("(Mobile,OrderCode,LStatus,Status,OperationType,Description,CreateTime,UpdateTime,UserName,Remark,HideContent)");
            strSql.Append(" values (@Mobile,@OrderCode,@LStatus,@Status,@OperationType,@Description,@CreateTime,@UpdateTime,@UserName,@Remark,@HideContent)");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
					new SqlParameter("@Mobile", DbTool.FixSqlParameter(model.Mobile))
,					new SqlParameter("@OrderCode", DbTool.FixSqlParameter(model.OrderCode))
,					new SqlParameter("@LStatus", DbTool.FixSqlParameter(model.LStatus))
,					new SqlParameter("@Status", DbTool.FixSqlParameter(model.Status))
,					new SqlParameter("@OperationType", DbTool.FixSqlParameter(model.OperationType))
,					new SqlParameter("@Description", DbTool.FixSqlParameter(model.Description))
,					new SqlParameter("@CreateTime", DbTool.FixSqlParameter(model.CreateTime))
,					new SqlParameter("@UpdateTime", DbTool.FixSqlParameter(model.UpdateTime))
,					new SqlParameter("@UserName", DbTool.FixSqlParameter(model.UserName))
,					new SqlParameter("@Remark", DbTool.FixSqlParameter(model.Remark))
,					new SqlParameter("@HideContent", DbTool.FixSqlParameter(model.HideContent))
                 };


            return DbTool.ConvertObject<int>(SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters),0);
         
        }
         
         
         #endregion
         
        #region Update
        public int Update(Model.Operation_LogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Operation_Log set ");
            strSql.Append("Mobile=@Mobile,OrderCode=@OrderCode,LStatus=@LStatus,Status=@Status,OperationType=@OperationType,Description=@Description,CreateTime=@CreateTime,UpdateTime=@UpdateTime,UserName=@UserName,Remark=@Remark,HideContent=@HideContent ");
            strSql.Append(" where Id=@Id ");
       
            SqlParameter[] parameters = {
					new SqlParameter("@Mobile", DbTool.FixSqlParameter(model.Mobile))
,					new SqlParameter("@OrderCode", DbTool.FixSqlParameter(model.OrderCode))
,					new SqlParameter("@LStatus", DbTool.FixSqlParameter(model.LStatus))
,					new SqlParameter("@Status", DbTool.FixSqlParameter(model.Status))
,					new SqlParameter("@OperationType", DbTool.FixSqlParameter(model.OperationType))
,					new SqlParameter("@Description", DbTool.FixSqlParameter(model.Description))
,					new SqlParameter("@CreateTime", DbTool.FixSqlParameter(model.CreateTime))
,					new SqlParameter("@UpdateTime", DbTool.FixSqlParameter(model.UpdateTime))
,					new SqlParameter("@UserName", DbTool.FixSqlParameter(model.UserName))
,					new SqlParameter("@Remark", DbTool.FixSqlParameter(model.Remark))
,					new SqlParameter("@HideContent", DbTool.FixSqlParameter(model.HideContent))
,					new SqlParameter("@Id", model.Id)
                 };


            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }
         #endregion
         
        #region Delete
        public int Del(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from Operation_Log where Id = " + id);
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region BindDataReader
        protected void BindDataReader(Model.Operation_LogModel model,SqlDataReader dr)
        {

                model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
                model.Mobile = DbTool.ConvertObject<System.String>(dr["Mobile"]);
                model.OrderCode = DbTool.ConvertObject<System.String>(dr["OrderCode"]);
                model.LStatus = DbTool.ConvertObject<System.Int32>(dr["LStatus"]);
                model.Status = DbTool.ConvertObject<System.Int32>(dr["Status"]);
                model.OperationType = DbTool.ConvertObject<System.String>(dr["OperationType"]);
                model.Description = DbTool.ConvertObject<System.String>(dr["Description"]);
                model.CreateTime = DbTool.ConvertObject<System.DateTime>(dr["CreateTime"]);
                model.UpdateTime = DbTool.ConvertObject<System.DateTime>(dr["UpdateTime"]);
                model.UserName = DbTool.ConvertObject<System.String>(dr["UserName"]);
                model.Remark = DbTool.ConvertObject<System.String>(dr["Remark"]);
                model.HideContent = DbTool.ConvertObject<System.String>(dr["HideContent"]);


        }
         #endregion
         
        #region AutoBindDataReader
        protected Model.Operation_LogModel AutoBindDataReader(SqlDataReader dr, params string[] fields)
        {

           var model = new Model.Operation_LogModel();
                if (DbTool.HasFields("Id", fields)) model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
                if (DbTool.HasFields("Mobile", fields)) model.Mobile = DbTool.ConvertObject<System.String>(dr["Mobile"]);
                if (DbTool.HasFields("OrderCode", fields)) model.OrderCode = DbTool.ConvertObject<System.String>(dr["OrderCode"]);
                if (DbTool.HasFields("LStatus", fields)) model.LStatus = DbTool.ConvertObject<System.Int32>(dr["LStatus"]);
                if (DbTool.HasFields("Status", fields)) model.Status = DbTool.ConvertObject<System.Int32>(dr["Status"]);
                if (DbTool.HasFields("OperationType", fields)) model.OperationType = DbTool.ConvertObject<System.String>(dr["OperationType"]);
                if (DbTool.HasFields("Description", fields)) model.Description = DbTool.ConvertObject<System.String>(dr["Description"]);
                if (DbTool.HasFields("CreateTime", fields)) model.CreateTime = DbTool.ConvertObject<System.DateTime>(dr["CreateTime"]);
                if (DbTool.HasFields("UpdateTime", fields)) model.UpdateTime = DbTool.ConvertObject<System.DateTime>(dr["UpdateTime"]);
                if (DbTool.HasFields("UserName", fields)) model.UserName = DbTool.ConvertObject<System.String>(dr["UserName"]);
                if (DbTool.HasFields("Remark", fields)) model.Remark = DbTool.ConvertObject<System.String>(dr["Remark"]);
                if (DbTool.HasFields("HideContent", fields)) model.HideContent = DbTool.ConvertObject<System.String>(dr["HideContent"]);

           return model;

        }
         #endregion
         
        #endregion 

        #region GetList
        public DataTable GetList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from Operation_Log where 1=1 ");
            sql.Append(sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region GetModel
        public Model.Operation_LogModel GetModel(int Id)
        {

            string sql = "select top 1 * from Operation_Log where Id =" + Id;
            Model.Operation_LogModel model = new Model.Operation_LogModel();
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
        public List<Model.Operation_LogModel> GetModelList()
        {

            List<Model.Operation_LogModel> result = new List<Model.Operation_LogModel>();
            string sql = "select * from Operation_Log where 1=1";
            Model.Operation_LogModel model = new Model.Operation_LogModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            //var fields = DbTool.GetReaderFieldNames(dr);
            while (dr.Read())
            {
                 //model = AutoBindDataReader(dr, fields);
                 model = new Model.Operation_LogModel(); 
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
            sql.Append("select count(1) from Operation_Log where 1=1 ");
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
            pages.TableName = "Operation_Log";
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
           pages.TableName = "Operation_Log";
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
           StringBuilder sql = new StringBuilder("select a.* from Operation_Log a  where 1=1 " + sqlstr);
           return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
        #endregion
         

        
 
    }
}
