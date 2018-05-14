using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data; 

namespace Db
{
    public class Login_LogDal
    {
        public string conn = SqlHelper.ConnectionString;

        
        
        #region Dal Core Functional

        #region Add
        public int Add(Model.Login_LogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into  [Login_Log]");
            strSql.Append("(LoginTime,LoginIp,UserName,Notes)");
            strSql.Append(" values (@LoginTime,@LoginIp,@UserName,@Notes)");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
					new SqlParameter("@LoginTime", DbTool.FixSqlParameter(model.LoginTime))
,					new SqlParameter("@LoginIp", DbTool.FixSqlParameter(model.LoginIp))
,					new SqlParameter("@UserName", DbTool.FixSqlParameter(model.UserName))
,                   new SqlParameter("@Notes", DbTool.FixSqlParameter(model.Notes))
                 };


            return DbTool.ConvertObject<int>(SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters),0);
         
        }
         
         
         #endregion
         
        #region Update
        public int Update(Model.Login_LogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Login_Log set ");
            strSql.Append("LoginTime=@LoginTime,LoginIp=@LoginIp,UserName=@UserName ");
            strSql.Append(" where LogId=@LogId ");
       
            SqlParameter[] parameters = {
					new SqlParameter("@LoginTime", DbTool.FixSqlParameter(model.LoginTime))
,					new SqlParameter("@LoginIp", DbTool.FixSqlParameter(model.LoginIp))
,					new SqlParameter("@UserName", DbTool.FixSqlParameter(model.UserName))
,					new SqlParameter("@LogId", model.LogId)
                 };


            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }
         #endregion
         
        #region Delete
        public int Del(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from Login_Log where LogId = " + id);
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region BindDataReader
        protected void BindDataReader(Model.Login_LogModel model,SqlDataReader dr)
        {

                model.LogId = DbTool.ConvertObject<System.Int32>(dr["LogId"]);
                model.LoginTime = DbTool.ConvertObject<System.DateTime>(dr["LoginTime"]);
                model.LoginIp = DbTool.ConvertObject<System.String>(dr["LoginIp"]);
                model.UserName = DbTool.ConvertObject<System.String>(dr["UserName"]);


        }
         #endregion
         
        #region AutoBindDataReader
        protected Model.Login_LogModel AutoBindDataReader(SqlDataReader dr, params string[] fields)
        {

           var model = new Model.Login_LogModel();
                if (DbTool.HasFields("LogId", fields)) model.LogId = DbTool.ConvertObject<System.Int32>(dr["LogId"]);
                if (DbTool.HasFields("LoginTime", fields)) model.LoginTime = DbTool.ConvertObject<System.DateTime>(dr["LoginTime"]);
                if (DbTool.HasFields("LoginIp", fields)) model.LoginIp = DbTool.ConvertObject<System.String>(dr["LoginIp"]);
                if (DbTool.HasFields("UserName", fields)) model.UserName = DbTool.ConvertObject<System.String>(dr["UserName"]);

           return model;

        }
         #endregion
         
        #endregion 

        #region GetList
        public DataTable GetList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from Login_Log where 1=1 ");
            sql.Append(sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region GetModel
        public Model.Login_LogModel GetModel(int Id)
        {

            string sql = "select top 1 * from Login_Log where LogId =" + Id;
            Model.Login_LogModel model = new Model.Login_LogModel();
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
        public List<Model.Login_LogModel> GetModelList()
        {

            List<Model.Login_LogModel> result = new List<Model.Login_LogModel>();
            string sql = "select * from Login_Log where 1=1";
            Model.Login_LogModel model = new Model.Login_LogModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            //var fields = DbTool.GetReaderFieldNames(dr);
            while (dr.Read())
            {
                 //model = AutoBindDataReader(dr, fields);
                 model = new Model.Login_LogModel(); 
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
            sql.Append("select count(1) from Login_Log where 1=1 ");
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
            pages.TableName = "Login_Log";
            pages.JoinTable = "   ";
            pages.CountFields = " a.LogId ";
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
           pages.TableName = "Login_Log";
           pages.JoinTable = " ";
           pages.CountFields = " a.LogId ";
           pages.OrderString = " order by a.LogId desc";
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
           StringBuilder sql = new StringBuilder("select a.* from Login_Log a  where 1=1 " + sqlstr);
           return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
        #endregion
         

        
 
    }
}
