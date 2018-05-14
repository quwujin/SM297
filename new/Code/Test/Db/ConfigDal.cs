using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data; 

namespace Db
{
    public class ConfigDal
    {
        public string conn = SqlHelper.ConnectionString;

        #region Dal Core Functional

        #region Add
        public int Add(Model.ConfigModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into  [Config]");
            strSql.Append("(TId,KId,Title,Val,Types,States,Sort,Remark)");
            strSql.Append(" values (@TId,@KId,@Title,@Types,@States,@Remark)");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
					new SqlParameter("@TId", DbTool.FixSqlParameter(model.TId)),
					new SqlParameter("@KId", DbTool.FixSqlParameter(model.KId))
,					new SqlParameter("@Title", DbTool.FixSqlParameter(model.Title))
,					new SqlParameter("@Val", DbTool.FixSqlParameter(model.Val))
,					new SqlParameter("@Types", DbTool.FixSqlParameter(model.Types))
,					new SqlParameter("@States", DbTool.FixSqlParameter(model.States))
,					new SqlParameter("@Sort", DbTool.FixSqlParameter(model.Sort))
,					new SqlParameter("@Remark", DbTool.FixSqlParameter(model.Remark))
                 };


            return DbTool.ConvertObject<int>(SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters),0);
         
        }
         
         
         #endregion

        public int CheckCount(string sqlwhere)
        {
            string sql = "select count(*)  from Config where 1=1 " + sqlwhere;
            return Convert.ToInt32(SqlHelper.ExecuteScalar(conn, CommandType.Text, sql));
        } 
        #region Update
        public int UpdateStates(Model.ConfigModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Config set ");
            strSql.Append("States=@States ");
            strSql.Append(" where Id=@Id ");

            SqlParameter[] parameters = {
					new SqlParameter("@States", DbTool.FixSqlParameter(model.States))
,					new SqlParameter("@Id", model.Id)
                 };


            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }
        public int AddConfig(Model.ConfigModel model, Model.ConfigLogModel mdlog)
        {
            int rtn = 0;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("insert into  [Config]");
                        strSql.Append("(TId,KId,Title,Val,Types,States,Sort,Remark)");
                        strSql.Append(" values (@TId,@KId,@Title,@Val,@Types,@States,@Sort,@Remark)");
                        strSql.Append(";select SCOPE_IDENTITY()");
                        SqlParameter[] parameters = {
					new SqlParameter("@TId", DbTool.FixSqlParameter(model.TId)),
					new SqlParameter("@KId", DbTool.FixSqlParameter(model.KId))
,					new SqlParameter("@Title", DbTool.FixSqlParameter(model.Title))
,					new SqlParameter("@Val", DbTool.FixSqlParameter(model.Val))
,					new SqlParameter("@Types", DbTool.FixSqlParameter(model.Types))
,					new SqlParameter("@States", DbTool.FixSqlParameter(model.States))
,					new SqlParameter("@Sort", DbTool.FixSqlParameter(model.Sort))
,					new SqlParameter("@Remark", DbTool.FixSqlParameter(model.Remark))
                 };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);

                        StringBuilder strSql2 = new StringBuilder();
                        strSql2.Append("insert into  [ConfigLog]");
                        strSql2.Append("(ConfigId,UserId,Title,Note,CTime)");
                        strSql2.Append(" values (@ConfigId,@UserId,@Title,@Note,@CTime)");
                        strSql2.Append(";select SCOPE_IDENTITY()");
                        SqlParameter[] parameters2 = {
					                    new SqlParameter("@ConfigId", DbTool.FixSqlParameter(mdlog.ConfigId))
                    ,					new SqlParameter("@UserId", DbTool.FixSqlParameter(mdlog.UserId))
                    ,					new SqlParameter("@Title", DbTool.FixSqlParameter(mdlog.Title))
                    ,					new SqlParameter("@Note", DbTool.FixSqlParameter(mdlog.Note))
                    ,					new SqlParameter("@CTime", DbTool.FixSqlParameter(mdlog.CTime))
                 };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql2.ToString(), parameters2);
                        trans.Commit();
                        return rtn;
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        rtn = 0;
                    }
                }
            }
            return 0;

        }
        public int UpdateC(Model.ConfigModel model, Model.ConfigLogModel mdlog)
        {
            int rtn = 0;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("update Config set ");
                        strSql.Append("Val=@Val,States=@States,Sort=@Sort,Types=@Types,TId=@TId,Title=@Title ");
                        strSql.Append(" where Id=@Id ");

                        SqlParameter[] parameters = {
					new SqlParameter("@Val", DbTool.FixSqlParameter(model.Val))
,					new SqlParameter("@States", DbTool.FixSqlParameter(model.States))
,					new SqlParameter("@Sort", DbTool.FixSqlParameter(model.Sort))
,					new SqlParameter("@Types", DbTool.FixSqlParameter(model.Types))
,					new SqlParameter("@Title", DbTool.FixSqlParameter(model.Title))
,					new SqlParameter("@TId", DbTool.FixSqlParameter(model.TId))
,					new SqlParameter("@Id", model.Id)
                 };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);

                        StringBuilder strSql2 = new StringBuilder();
                        strSql2.Append("insert into  [ConfigLog]");
                        strSql2.Append("(ConfigId,UserId,Title,Note,CTime)");
                        strSql2.Append(" values (@ConfigId,@UserId,@Title,@Note,@CTime)");
                        strSql2.Append(";select SCOPE_IDENTITY()");
                        SqlParameter[] parameters2 = {
					                    new SqlParameter("@ConfigId", DbTool.FixSqlParameter(mdlog.ConfigId))
                    ,					new SqlParameter("@UserId", DbTool.FixSqlParameter(mdlog.UserId))
                    ,					new SqlParameter("@Title", DbTool.FixSqlParameter(mdlog.Title))
                    ,					new SqlParameter("@Note", DbTool.FixSqlParameter(mdlog.Note))
                    ,					new SqlParameter("@CTime", DbTool.FixSqlParameter(mdlog.CTime))
                 };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql2.ToString(), parameters2);
                        trans.Commit();
                        return rtn;
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        rtn = 0;
                    }
                }
            }
            return 0;

        }
        public int UpdateConfig(Model.ConfigModel model, Model.ConfigLogModel mdlog)
        {
            int rtn = 0;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("update Config set ");
                        strSql.Append("Title=@Title,Val=@Val,Types=@Types,States=@States,Sort=@Sort,Remark=@Remark ");
                        strSql.Append(" where Id=@Id ");

                        SqlParameter[] parameters = {
					new SqlParameter("@Title", DbTool.FixSqlParameter(model.Title))
,					new SqlParameter("@Val", DbTool.FixSqlParameter(model.Val))
,					new SqlParameter("@Types", DbTool.FixSqlParameter(model.Types))
,					new SqlParameter("@States", DbTool.FixSqlParameter(model.States))
,					new SqlParameter("@Sort", DbTool.FixSqlParameter(model.Sort))
,					new SqlParameter("@Remark", DbTool.FixSqlParameter(model.Remark))
,					new SqlParameter("@Id", model.Id)
                 };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);

                        StringBuilder strSql2 = new StringBuilder();
                        strSql2.Append("insert into  [ConfigLog]");
                        strSql2.Append("(ConfigId,UserId,Title,Note,CTime)");
                        strSql2.Append(" values (@ConfigId,@UserId,@Title,@Note,@CTime)");
                        strSql2.Append(";select SCOPE_IDENTITY()");
                        SqlParameter[] parameters2 = {
					                    new SqlParameter("@ConfigId", DbTool.FixSqlParameter(mdlog.ConfigId))
                    ,					new SqlParameter("@UserId", DbTool.FixSqlParameter(mdlog.UserId))
                    ,					new SqlParameter("@Title", DbTool.FixSqlParameter(mdlog.Title))
                    ,					new SqlParameter("@Note", DbTool.FixSqlParameter(mdlog.Note))
                    ,					new SqlParameter("@CTime", DbTool.FixSqlParameter(mdlog.CTime))
                 };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql2.ToString(), parameters2);
                        trans.Commit();
                        return rtn;
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        rtn = 0;
                    }
                }
            }
            return 0;

        }
        public int Update(Model.ConfigModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Config set ");
            strSql.Append("KId=@KId,Title=@Title,Val=@Val,Types=@Types,States=@States,Sort=@Sort,Remark=@Remark ");
            strSql.Append(" where Id=@Id ");
       
            SqlParameter[] parameters = {
					new SqlParameter("@KId", DbTool.FixSqlParameter(model.KId))
,					new SqlParameter("@Title", DbTool.FixSqlParameter(model.Title))
,					new SqlParameter("@Val", DbTool.FixSqlParameter(model.Val))
,					new SqlParameter("@Types", DbTool.FixSqlParameter(model.Types))
,					new SqlParameter("@States", DbTool.FixSqlParameter(model.States))
,					new SqlParameter("@Sort", DbTool.FixSqlParameter(model.Sort))
,					new SqlParameter("@Remark", DbTool.FixSqlParameter(model.Remark))
,					new SqlParameter("@Id", model.Id)
                 };


            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }
         #endregion
         
        #region Delete
        public int DelConfig(Model.ConfigModel model, Model.ConfigLogModel mdlog)
        {
            int rtn = 0;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {

                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("delete from Config ");
                        strSql.Append(" where Id=@Id ");

                        SqlParameter[] parameters = {
                                                        new SqlParameter("@Id", model.Id)
                    };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);

                        StringBuilder strSql2 = new StringBuilder();
                        strSql2.Append("insert into  [ConfigLog]");
                        strSql2.Append("(ConfigId,UserId,Title,Note,CTime)");
                        strSql2.Append(" values (@ConfigId,@UserId,@Title,@Note,@CTime)");
                        strSql2.Append(";select SCOPE_IDENTITY()");
                        SqlParameter[] parameters2 = {
					                    new SqlParameter("@ConfigId", DbTool.FixSqlParameter(mdlog.ConfigId))
                    ,					new SqlParameter("@UserId", DbTool.FixSqlParameter(mdlog.UserId))
                    ,					new SqlParameter("@Title", DbTool.FixSqlParameter(mdlog.Title))
                    ,					new SqlParameter("@Note", DbTool.FixSqlParameter(mdlog.Note))
                    ,					new SqlParameter("@CTime", DbTool.FixSqlParameter(mdlog.CTime))
                 };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql2.ToString(), parameters2);
                        trans.Commit();
                        return rtn;
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        rtn = 0;
                    }
                }
            }
            return 0;

        }
        public int Del(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from Config where Id = " + id);
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region BindDataReader                                                                           
        protected void BindDataReader(Model.ConfigModel model,SqlDataReader dr)
        {

            model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
            model.TId = DbTool.ConvertObject<System.Int32>(dr["TId"]);
            model.KId = DbTool.ConvertObject<System.Int32>(dr["KId"]);
                model.Title = DbTool.ConvertObject<System.String>(dr["Title"]);
                model.Val = DbTool.ConvertObject<System.String>(dr["Val"]);
                model.Types = DbTool.ConvertObject<System.Int32>(dr["Types"]);
                model.States = DbTool.ConvertObject<System.Int32>(dr["States"]);
                model.Sort = DbTool.ConvertObject<System.Int32>(dr["Sort"]);
                model.Remark = DbTool.ConvertObject<System.String>(dr["Remark"]);


        }
         #endregion
         
        #region AutoBindDataReader
        protected Model.ConfigModel AutoBindDataReader(SqlDataReader dr, params string[] fields)
        {

           var model = new Model.ConfigModel();
           if (DbTool.HasFields("Id", fields)) model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
           if (DbTool.HasFields("TId", fields)) model.TId = DbTool.ConvertObject<System.Int32>(dr["TId"]);
                if (DbTool.HasFields("KId", fields)) model.KId = DbTool.ConvertObject<System.Int32>(dr["KId"]);
                if (DbTool.HasFields("Title", fields)) model.Title = DbTool.ConvertObject<System.String>(dr["Title"]);
                if (DbTool.HasFields("Val", fields)) model.Val = DbTool.ConvertObject<System.String>(dr["Val"]);
                if (DbTool.HasFields("Types", fields)) model.Types = DbTool.ConvertObject<System.Int32>(dr["Types"]);
                if (DbTool.HasFields("States", fields)) model.States = DbTool.ConvertObject<System.Int32>(dr["States"]);
                if (DbTool.HasFields("Sort", fields)) model.Sort = DbTool.ConvertObject<System.Int32>(dr["Sort"]);
                if (DbTool.HasFields("Remark", fields)) model.Remark = DbTool.ConvertObject<System.String>(dr["Remark"]);

           return model;

        }
         #endregion
         
        #endregion 

        #region GetList
        public DataTable GetList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from Config where 1=1 ");
            sql.Append(sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
         #endregion

        #region GetModel
        public Model.ConfigModel GetModel(int Id)
        {

            string sql = "select top 1 * from Config where Id =" + Id;
            Model.ConfigModel model = new Model.ConfigModel();
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

        public Model.ConfigModel GetModelByKid()
        {

            string sql = "select top 1 * from Config order by [KId] desc";
            Model.ConfigModel model = new Model.ConfigModel();
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

        #region GetModel
        public Model.ConfigModel GetModelKId(int KId)
        {
            string sql = "select top 1 * from Config where KId =" + KId;
            Model.ConfigModel model = new Model.ConfigModel();
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
        public List<Model.ConfigModel> GetModelList()
        {

            List<Model.ConfigModel> result = new List<Model.ConfigModel>();
            string sql = "select * from Config where 1=1";
            Model.ConfigModel model = new Model.ConfigModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            //var fields = DbTool.GetReaderFieldNames(dr);
            while (dr.Read())
            {
                 //model = AutoBindDataReader(dr, fields);
                 model = new Model.ConfigModel(); 
                 BindDataReader(model, dr);
                 result.Add(model);
            }
            dr.Close();
            return result;
        }
         #endregion
         

        
 
    }
}
