using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data; 

namespace Db
{
    public class SourceInfoDal
    {
        public string conn = SqlHelper.ConnectionString;

        
        
        #region Dal Core Functional

        #region Add
        public int Add(Model.SourceInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into  [SourceInfo]");
            strSql.Append("(Ip,Pros,City,SourceUrl,Dtime,Type)");
            strSql.Append(" values (@Ip,@Pros,@City,@SourceUrl,@Dtime,@Type)");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
					new SqlParameter("@Ip", DbTool.FixSqlParameter(model.Ip))
,					new SqlParameter("@Pros", DbTool.FixSqlParameter(model.Pros))
,					new SqlParameter("@City", DbTool.FixSqlParameter(model.City))
,					new SqlParameter("@SourceUrl", DbTool.FixSqlParameter(model.SourceUrl))
,					new SqlParameter("@Dtime", DbTool.FixSqlParameter(model.Dtime))
,					new SqlParameter("@Type", DbTool.FixSqlParameter(model.Type))
                 };


            return DbTool.ConvertObject<int>(SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters),0);
         
        }
         
         
         #endregion
         
        #region Update
        public int Update(Model.SourceInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SourceInfo set ");
            strSql.Append("Ip=@Ip,Pros=@Pros,City=@City,SourceUrl=@SourceUrl,Dtime=@Dtime,Type=@Type ");
            strSql.Append(" where Id=@Id ");
       
            SqlParameter[] parameters = {
					new SqlParameter("@Ip", DbTool.FixSqlParameter(model.Ip))
,					new SqlParameter("@Pros", DbTool.FixSqlParameter(model.Pros))
,					new SqlParameter("@City", DbTool.FixSqlParameter(model.City))
,					new SqlParameter("@SourceUrl", DbTool.FixSqlParameter(model.SourceUrl))
,					new SqlParameter("@Dtime", DbTool.FixSqlParameter(model.Dtime))
,					new SqlParameter("@Type", DbTool.FixSqlParameter(model.Type))
,					new SqlParameter("@Id", model.Id)
                 };


            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }
         #endregion
         
        #region Delete
        public int Del(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from SourceInfo where Id = " + id);
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region BindDataReader
        protected void BindDataReader(Model.SourceInfoModel model,SqlDataReader dr)
        {

                model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
                model.Ip = DbTool.ConvertObject<System.String>(dr["Ip"]);
                model.Pros = DbTool.ConvertObject<System.String>(dr["Pros"]);
                model.City = DbTool.ConvertObject<System.String>(dr["City"]);
                model.SourceUrl = DbTool.ConvertObject<System.String>(dr["SourceUrl"]);
                model.Dtime = DbTool.ConvertObject<System.DateTime>(dr["Dtime"]);
                model.Type = DbTool.ConvertObject<System.String>(dr["Type"]);


        }
         #endregion
         
        #region AutoBindDataReader
        protected Model.SourceInfoModel AutoBindDataReader(SqlDataReader dr, params string[] fields)
        {

           var model = new Model.SourceInfoModel();
                if (DbTool.HasFields("Id", fields)) model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
                if (DbTool.HasFields("Ip", fields)) model.Ip = DbTool.ConvertObject<System.String>(dr["Ip"]);
                if (DbTool.HasFields("Pros", fields)) model.Pros = DbTool.ConvertObject<System.String>(dr["Pros"]);
                if (DbTool.HasFields("City", fields)) model.City = DbTool.ConvertObject<System.String>(dr["City"]);
                if (DbTool.HasFields("SourceUrl", fields)) model.SourceUrl = DbTool.ConvertObject<System.String>(dr["SourceUrl"]);
                if (DbTool.HasFields("Dtime", fields)) model.Dtime = DbTool.ConvertObject<System.DateTime>(dr["Dtime"]);
                if (DbTool.HasFields("Type", fields)) model.Type = DbTool.ConvertObject<System.String>(dr["Type"]);

           return model;

        }
         #endregion
         
        #endregion 

        #region GetList
        public DataTable GetList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from SourceInfo where 1=1 ");
            sql.Append(sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region GetModel
        public Model.SourceInfoModel GetModel(int Id)
        {

            string sql = "select top 1 * from SourceInfo where Id =" + Id;
            Model.SourceInfoModel model = new Model.SourceInfoModel();
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
        public List<Model.SourceInfoModel> GetModelList()
        {

            List<Model.SourceInfoModel> result = new List<Model.SourceInfoModel>();
            string sql = "select * from SourceInfo where 1=1";
            Model.SourceInfoModel model = new Model.SourceInfoModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            //var fields = DbTool.GetReaderFieldNames(dr);
            while (dr.Read())
            {
                 //model = AutoBindDataReader(dr, fields);
                 model = new Model.SourceInfoModel(); 
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
            sql.Append("select count(*) from SourceInfo where 1=1 ");
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
            pages.TableName = "SourceInfo";
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
           pages.TableName = "SourceInfo";
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
           StringBuilder sql = new StringBuilder("select a.* from SourceInfo a  where 1=1 " + sqlstr);
           return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
        #endregion
         

        
 
    }
}
