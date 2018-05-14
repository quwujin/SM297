using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data; 

namespace Db
{
    public class MsgConfigDal
    {
        public string conn = SqlHelper.ConnectionString;

        
        
        #region Dal Core Functional

        #region Add
        public int Add(Model.MsgConfigModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into  [MsgConfig]");
            strSql.Append("(SupplierId,MsgType,MsgTitle,MsgTemp)");
            strSql.Append(" values (@SupplierId,@MsgType,@MsgTitle,@MsgTemp)");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
					new SqlParameter("@SupplierId", DbTool.FixSqlParameter(model.SupplierId))
,					new SqlParameter("@MsgType", DbTool.FixSqlParameter(model.MsgType))
,					new SqlParameter("@MsgTitle", DbTool.FixSqlParameter(model.MsgTitle))
,					new SqlParameter("@MsgTemp", DbTool.FixSqlParameter(model.MsgTemp))
                 };


            return DbTool.ConvertObject<int>(SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters),0);
         
        }
         
         
         #endregion
         
        #region Update
        public int Update(Model.MsgConfigModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MsgConfig set ");
            strSql.Append("SupplierId=@SupplierId,MsgType=@MsgType,MsgTitle=@MsgTitle,MsgTemp=@MsgTemp ");
            strSql.Append(" where Id=@Id ");
       
            SqlParameter[] parameters = {
					new SqlParameter("@SupplierId", DbTool.FixSqlParameter(model.SupplierId))
,					new SqlParameter("@MsgType", DbTool.FixSqlParameter(model.MsgType))
,					new SqlParameter("@MsgTitle", DbTool.FixSqlParameter(model.MsgTitle))
,					new SqlParameter("@MsgTemp", DbTool.FixSqlParameter(model.MsgTemp))
,					new SqlParameter("@Id", model.Id)
                 };


            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }
         #endregion
         
        #region Delete
        public int Del(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from MsgConfig where Id = " + id);
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region BindDataReader
        protected void BindDataReader(Model.MsgConfigModel model,SqlDataReader dr)
        {

                model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
                model.SupplierId = DbTool.ConvertObject<System.Int32>(dr["SupplierId"]);
                model.MsgType = DbTool.ConvertObject<System.String>(dr["MsgType"]);
                model.MsgTitle = DbTool.ConvertObject<System.String>(dr["MsgTitle"]);
                model.MsgTemp = DbTool.ConvertObject<System.String>(dr["MsgTemp"]);


        }
         #endregion
         
        #region AutoBindDataReader
        protected Model.MsgConfigModel AutoBindDataReader(SqlDataReader dr, params string[] fields)
        {

           var model = new Model.MsgConfigModel();
                if (DbTool.HasFields("Id", fields)) model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
                if (DbTool.HasFields("SupplierId", fields)) model.SupplierId = DbTool.ConvertObject<System.Int32>(dr["SupplierId"]);
                if (DbTool.HasFields("MsgType", fields)) model.MsgType = DbTool.ConvertObject<System.String>(dr["MsgType"]);
                if (DbTool.HasFields("MsgTitle", fields)) model.MsgTitle = DbTool.ConvertObject<System.String>(dr["MsgTitle"]);
                if (DbTool.HasFields("MsgTemp", fields)) model.MsgTemp = DbTool.ConvertObject<System.String>(dr["MsgTemp"]);

           return model;

        }
         #endregion
         
        #endregion 

        #region GetList
        public DataTable GetList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from MsgConfig where 1=1 ");
            sql.Append(sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region GetModel
        public Model.MsgConfigModel GetModel(int Id)
        {

            string sql = "select top 1 * from MsgConfig where Id =" + Id;
            Model.MsgConfigModel model = new Model.MsgConfigModel();
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
        public List<Model.MsgConfigModel> GetModelList()
        {

            List<Model.MsgConfigModel> result = new List<Model.MsgConfigModel>();
            string sql = "select * from MsgConfig where 1=1";
            Model.MsgConfigModel model = new Model.MsgConfigModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            //var fields = DbTool.GetReaderFieldNames(dr);
            while (dr.Read())
            {
                 //model = AutoBindDataReader(dr, fields);
                 model = new Model.MsgConfigModel(); 
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
            sql.Append("select count(*) from MsgConfig where 1=1 ");
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
            pages.TableName = "MsgConfig";
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
           pages.TableName = "MsgConfig";
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
           StringBuilder sql = new StringBuilder("select a.* from MsgConfig a  where 1=1 " + sqlstr);
           return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
        #endregion
         

        
 
    }
}
