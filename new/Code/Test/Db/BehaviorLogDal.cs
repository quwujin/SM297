using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data; 

namespace Db
{
    public class BehaviorLogDal
    {
        public string conn = SqlHelper.ConnectionString;

        
        
        #region Dal Core Functional

        #region Add
        public int Add(Model.BehaviorLogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into  [BehaviorLog]");
            strSql.Append("(Ip,BehaviorType,FailureReason,LockValue,CreateTime,Remark)");
            strSql.Append(" values (@Ip,@BehaviorType,@FailureReason,@LockValue,@CreateTime,@Remark)");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
					new SqlParameter("@Ip", DbTool.FixSqlParameter(model.Ip))
,					new SqlParameter("@BehaviorType", DbTool.FixSqlParameter(model.BehaviorType))
,					new SqlParameter("@FailureReason", DbTool.FixSqlParameter(model.FailureReason))
,					new SqlParameter("@LockValue", DbTool.FixSqlParameter(model.LockValue))
,					new SqlParameter("@CreateTime", DbTool.FixSqlParameter(model.CreateTime))
,					new SqlParameter("@Remark", DbTool.FixSqlParameter(model.Remark))
                 };


            return DbTool.ConvertObject<int>(SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters),0);
         
        }
         
         
         #endregion
         
        #region Update
        public int Update(Model.BehaviorLogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BehaviorLog set ");
            strSql.Append("Ip=@Ip,BehaviorType=@BehaviorType,FailureReason=@FailureReason,LockValue=@LockValue,CreateTime=@CreateTime,Remark=@Remark ");
            strSql.Append(" where Id=@Id ");
       
            SqlParameter[] parameters = {
					new SqlParameter("@Ip", DbTool.FixSqlParameter(model.Ip))
,					new SqlParameter("@BehaviorType", DbTool.FixSqlParameter(model.BehaviorType))
,					new SqlParameter("@FailureReason", DbTool.FixSqlParameter(model.FailureReason))
,					new SqlParameter("@LockValue", DbTool.FixSqlParameter(model.LockValue))
,					new SqlParameter("@CreateTime", DbTool.FixSqlParameter(model.CreateTime))
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
            sql.Append("delete from BehaviorLog where Id = " + id);
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region BindDataReader
        protected void BindDataReader(Model.BehaviorLogModel model,SqlDataReader dr)
        {

                model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
                model.Ip = DbTool.ConvertObject<System.String>(dr["Ip"]);
                model.BehaviorType = DbTool.ConvertObject<System.Int32>(dr["BehaviorType"]);
                model.FailureReason = DbTool.ConvertObject<System.String>(dr["FailureReason"]);
                model.LockValue = DbTool.ConvertObject<System.String>(dr["LockValue"]);
                model.CreateTime = DbTool.ConvertObject<System.DateTime>(dr["CreateTime"]);
                model.Remark = DbTool.ConvertObject<System.String>(dr["Remark"]);


        }
         #endregion
         
        #region AutoBindDataReader
        protected Model.BehaviorLogModel AutoBindDataReader(SqlDataReader dr, params string[] fields)
        {

           var model = new Model.BehaviorLogModel();
                if (DbTool.HasFields("Id", fields)) model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
                if (DbTool.HasFields("Ip", fields)) model.Ip = DbTool.ConvertObject<System.String>(dr["Ip"]);
                if (DbTool.HasFields("BehaviorType", fields)) model.BehaviorType = DbTool.ConvertObject<System.Int32>(dr["BehaviorType"]);
                if (DbTool.HasFields("FailureReason", fields)) model.FailureReason = DbTool.ConvertObject<System.String>(dr["FailureReason"]);
                if (DbTool.HasFields("LockValue", fields)) model.LockValue = DbTool.ConvertObject<System.String>(dr["LockValue"]);
                if (DbTool.HasFields("CreateTime", fields)) model.CreateTime = DbTool.ConvertObject<System.DateTime>(dr["CreateTime"]);
                if (DbTool.HasFields("Remark", fields)) model.Remark = DbTool.ConvertObject<System.String>(dr["Remark"]);

           return model;

        }
         #endregion
         
        #endregion 

        #region GetList
        public DataTable GetList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from BehaviorLog where 1=1 ");
            sql.Append(sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region GetModel
        public Model.BehaviorLogModel GetModel(int Id)
        {

            string sql = "select top 1 * from BehaviorLog where Id =" + Id;
            Model.BehaviorLogModel model = new Model.BehaviorLogModel();
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
        public List<Model.BehaviorLogModel> GetModelList()
        {

            List<Model.BehaviorLogModel> result = new List<Model.BehaviorLogModel>();
            string sql = "select * from BehaviorLog where 1=1";
            Model.BehaviorLogModel model = new Model.BehaviorLogModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            //var fields = DbTool.GetReaderFieldNames(dr);
            while (dr.Read())
            {
                 //model = AutoBindDataReader(dr, fields);
                 model = new Model.BehaviorLogModel(); 
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
            sql.Append("select count(*) from BehaviorLog where 1=1 ");
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
            pages.TableName = "BehaviorLog";
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
           pages.TableName = "BehaviorLog";
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
           StringBuilder sql = new StringBuilder("select a.* from BehaviorLog a  where 1=1 " + sqlstr);
           return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
        #endregion
         

        
 
    }
}
