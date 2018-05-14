using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data; 

namespace Db
{
    public class Cj_LogDal
    {
        public string conn = SqlHelper.ConnectionString;

        
        
        #region Dal Core Functional

        #region Add
        public int Add(Model.Cj_LogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into  [Cj_Log]");
            strSql.Append("(OrderCode,OpenId,Mob,Jx,Jp,States,CTime,Ip,Pros,City,Adds,Code,Note)");
            strSql.Append(" values (@OrderCode,@OpenId,@Mob,@Jx,@Jp,@States,@CTime,@Ip,@Pros,@City,@Adds,@Code,@Note)");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderCode", DbTool.FixSqlParameter(model.OrderCode))
,					new SqlParameter("@OpenId", DbTool.FixSqlParameter(model.OpenId))
,					new SqlParameter("@Mob", DbTool.FixSqlParameter(model.Mob))
,					new SqlParameter("@Jx", DbTool.FixSqlParameter(model.Jx))
,					new SqlParameter("@Jp", DbTool.FixSqlParameter(model.Jp))
,					new SqlParameter("@States", DbTool.FixSqlParameter(model.States))
,					new SqlParameter("@CTime", DbTool.FixSqlParameter(model.CTime))
,					new SqlParameter("@Ip", DbTool.FixSqlParameter(model.Ip))
,					new SqlParameter("@Pros", DbTool.FixSqlParameter(model.Pros))
,					new SqlParameter("@City", DbTool.FixSqlParameter(model.City))
,					new SqlParameter("@Adds", DbTool.FixSqlParameter(model.Adds))
,					new SqlParameter("@Code", DbTool.FixSqlParameter(model.Code))
,					new SqlParameter("@Note", DbTool.FixSqlParameter(model.Note))
                 };


            return DbTool.ConvertObject<int>(SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters),0);
         
        }
         
         
         #endregion
         
        #region Update
        public int Update(Model.Cj_LogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Cj_Log set ");
            strSql.Append("OrderCode=@OrderCode,OpenId=@OpenId,Mob=@Mob,Jx=@Jx,Jp=@Jp,States=@States,CTime=@CTime,Ip=@Ip,Pros=@Pros,City=@City,Adds=@Adds,Code=@Code,Note=@Note ");
            strSql.Append(" where Id=@Id ");
       
            SqlParameter[] parameters = {
					new SqlParameter("@OrderCode", DbTool.FixSqlParameter(model.OrderCode))
,					new SqlParameter("@OpenId", DbTool.FixSqlParameter(model.OpenId))
,					new SqlParameter("@Mob", DbTool.FixSqlParameter(model.Mob))
,					new SqlParameter("@Jx", DbTool.FixSqlParameter(model.Jx))
,					new SqlParameter("@Jp", DbTool.FixSqlParameter(model.Jp))
,					new SqlParameter("@States", DbTool.FixSqlParameter(model.States))
,					new SqlParameter("@CTime", DbTool.FixSqlParameter(model.CTime))
,					new SqlParameter("@Ip", DbTool.FixSqlParameter(model.Ip))
,					new SqlParameter("@Pros", DbTool.FixSqlParameter(model.Pros))
,					new SqlParameter("@City", DbTool.FixSqlParameter(model.City))
,					new SqlParameter("@Adds", DbTool.FixSqlParameter(model.Adds))
,					new SqlParameter("@Code", DbTool.FixSqlParameter(model.Code))
,					new SqlParameter("@Note", DbTool.FixSqlParameter(model.Note))
,					new SqlParameter("@Id", model.Id)
                 };


            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }
         #endregion
         
        #region Delete
        public int Del(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from Cj_Log where Id = " + id);
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region BindDataReader
        protected void BindDataReader(Model.Cj_LogModel model,SqlDataReader dr)
        {

                model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
                model.OrderCode = DbTool.ConvertObject<System.String>(dr["OrderCode"]);
                model.OpenId = DbTool.ConvertObject<System.String>(dr["OpenId"]);
                model.Mob = DbTool.ConvertObject<System.String>(dr["Mob"]);
                model.Jx = DbTool.ConvertObject<System.String>(dr["Jx"]);
                model.Jp = DbTool.ConvertObject<System.String>(dr["Jp"]);
                model.States = DbTool.ConvertObject<System.Int32>(dr["States"]);
                model.CTime = DbTool.ConvertObject<System.DateTime>(dr["CTime"]);
                model.Ip = DbTool.ConvertObject<System.String>(dr["Ip"]);
                model.Pros = DbTool.ConvertObject<System.String>(dr["Pros"]);
                model.City = DbTool.ConvertObject<System.String>(dr["City"]);
                model.Adds = DbTool.ConvertObject<System.String>(dr["Adds"]);
                model.Code = DbTool.ConvertObject<System.String>(dr["Code"]);
                model.Note = DbTool.ConvertObject<System.String>(dr["Note"]);


        }
         #endregion
         
        #region AutoBindDataReader
        protected Model.Cj_LogModel AutoBindDataReader(SqlDataReader dr, params string[] fields)
        {

           var model = new Model.Cj_LogModel();
                if (DbTool.HasFields("Id", fields)) model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
                if (DbTool.HasFields("OrderCode", fields)) model.OrderCode = DbTool.ConvertObject<System.String>(dr["OrderCode"]);
                if (DbTool.HasFields("OpenId", fields)) model.OpenId = DbTool.ConvertObject<System.String>(dr["OpenId"]);
                if (DbTool.HasFields("Mob", fields)) model.Mob = DbTool.ConvertObject<System.String>(dr["Mob"]);
                if (DbTool.HasFields("Jx", fields)) model.Jx = DbTool.ConvertObject<System.String>(dr["Jx"]);
                if (DbTool.HasFields("Jp", fields)) model.Jp = DbTool.ConvertObject<System.String>(dr["Jp"]);
                if (DbTool.HasFields("States", fields)) model.States = DbTool.ConvertObject<System.Int32>(dr["States"]);
                if (DbTool.HasFields("CTime", fields)) model.CTime = DbTool.ConvertObject<System.DateTime>(dr["CTime"]);
                if (DbTool.HasFields("Ip", fields)) model.Ip = DbTool.ConvertObject<System.String>(dr["Ip"]);
                if (DbTool.HasFields("Pros", fields)) model.Pros = DbTool.ConvertObject<System.String>(dr["Pros"]);
                if (DbTool.HasFields("City", fields)) model.City = DbTool.ConvertObject<System.String>(dr["City"]);
                if (DbTool.HasFields("Adds", fields)) model.Adds = DbTool.ConvertObject<System.String>(dr["Adds"]);
                if (DbTool.HasFields("Code", fields)) model.Code = DbTool.ConvertObject<System.String>(dr["Code"]);
                if (DbTool.HasFields("Note", fields)) model.Note = DbTool.ConvertObject<System.String>(dr["Note"]);

           return model;

        }
         #endregion
         
        #endregion 

        #region GetList
        public DataTable GetList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from Cj_Log where 1=1 ");
            sql.Append(sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region GetModel
        public Model.Cj_LogModel GetModel(int Id)
        {

            string sql = "select top 1 * from Cj_Log where Id =" + Id;
            Model.Cj_LogModel model = new Model.Cj_LogModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            if (dr.Read()) {
                 //var fields = DbTool.GetReaderFieldNames(dr);
                 //model = AutoBindDataReader(dr, fields);
                 BindDataReader(model, dr);
            }
            dr.Close();
            return model;
        }
        public Model.Cj_LogModel GetModel(string openid)
        {

            string sql = "select top 1 * from Cj_Log where OpenId ='" + openid + "'";
            Model.Cj_LogModel model = new Model.Cj_LogModel();
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
        public List<Model.Cj_LogModel> GetModelList()
        {

            List<Model.Cj_LogModel> result = new List<Model.Cj_LogModel>();
            string sql = "select * from Cj_Log where 1=1";
            Model.Cj_LogModel model = new Model.Cj_LogModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            //var fields = DbTool.GetReaderFieldNames(dr);
            while (dr.Read())
            {
                 //model = AutoBindDataReader(dr, fields);
                 model = new Model.Cj_LogModel(); 
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
            sql.Append("select count(*) from Cj_Log where 1=1 ");
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
            pages.TableName = "Cj_Log";
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
           pages.TableName = "Cj_Log";
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
        public DataTable GetExcelList(string sqlstr, string ckName, string ckField, string joinTab)
        {
            string checksql = "";
            for (int i = 0; i < ckName.Split(',').Length; i++)
            {
                checksql += ckField.Split(',')[i] + " as " + ckName.Split(',')[i];
                if (i != ckName.Split(',').Length - 1)
                {
                    checksql += ",";
                }
            }
            string sql = "select " + checksql + " from Cj_Log a " + joinTab + " where 1=1 " + sqlstr;
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
        #endregion

        #region 添加流量订单
        public int AddLiuLiang(Model.Cj_LogModel cjdelone, Model.Cj_LogModel cjdeltwo, Model.Cj_LogModel cjdelthree, int i)
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
                        strSql.Append("insert into  [Cj_Log]");
                        strSql.Append("(OrderCode,OpenId,Mob,Jx,Jp,States,CTime,Ip,Pros,City,Adds,Code,Note)");
                        strSql.Append(" values (@OrderCode,@OpenId,@Mob,@Jx,@Jp,@States,@CTime,@Ip,@Pros,@City,@Adds,@Code,@Note)");
                        strSql.Append(";select SCOPE_IDENTITY()");
                        SqlParameter[] parameters = {
					                        new SqlParameter("@OrderCode", DbTool.FixSqlParameter(cjdelone.OrderCode))
                        ,					new SqlParameter("@OpenId", DbTool.FixSqlParameter(cjdelone.OpenId))
                        ,					new SqlParameter("@Mob", DbTool.FixSqlParameter(cjdelone.Mob))
                        ,					new SqlParameter("@Jx", DbTool.FixSqlParameter(cjdelone.Jx))
                        ,					new SqlParameter("@Jp", DbTool.FixSqlParameter(cjdelone.Jp))
                        ,					new SqlParameter("@States", DbTool.FixSqlParameter(cjdelone.States))
                        ,					new SqlParameter("@CTime", DbTool.FixSqlParameter(cjdelone.CTime))
                        ,					new SqlParameter("@Ip", DbTool.FixSqlParameter(cjdelone.Ip))
                        ,					new SqlParameter("@Pros", DbTool.FixSqlParameter(cjdelone.Pros))
                        ,					new SqlParameter("@City", DbTool.FixSqlParameter(cjdelone.City))
                        ,					new SqlParameter("@Adds", DbTool.FixSqlParameter(cjdelone.Adds))
                        ,					new SqlParameter("@Code", DbTool.FixSqlParameter(cjdelone.Code))
                        ,					new SqlParameter("@Note", DbTool.FixSqlParameter(cjdelone.Note))
                                         };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);

                        if (cjdeltwo != null)
                        {
                            StringBuilder strSql2 = new StringBuilder();
                            strSql2.Append("insert into  [Cj_Log]");
                            strSql2.Append("(OrderCode,OpenId,Mob,Jx,Jp,States,CTime,Ip,Pros,City,Adds,Code,Note)");
                            strSql2.Append(" values (@OrderCode,@OpenId,@Mob,@Jx,@Jp,@States,@CTime,@Ip,@Pros,@City,@Adds,@Code,@Note)");
                            strSql2.Append(";select SCOPE_IDENTITY()");
                            SqlParameter[] parameters2 = {
					                            new SqlParameter("@OrderCode", DbTool.FixSqlParameter(cjdeltwo.OrderCode))
                            ,					new SqlParameter("@OpenId", DbTool.FixSqlParameter(cjdeltwo.OpenId))
                            ,					new SqlParameter("@Mob", DbTool.FixSqlParameter(cjdeltwo.Mob))
                            ,					new SqlParameter("@Jx", DbTool.FixSqlParameter(cjdeltwo.Jx))
                            ,					new SqlParameter("@Jp", DbTool.FixSqlParameter(cjdeltwo.Jp))
                            ,					new SqlParameter("@States", DbTool.FixSqlParameter(cjdeltwo.States))
                            ,					new SqlParameter("@CTime", DbTool.FixSqlParameter(cjdeltwo.CTime))
                            ,					new SqlParameter("@Ip", DbTool.FixSqlParameter(cjdeltwo.Ip))
                            ,					new SqlParameter("@Pros", DbTool.FixSqlParameter(cjdeltwo.Pros))
                            ,					new SqlParameter("@City", DbTool.FixSqlParameter(cjdeltwo.City))
                            ,					new SqlParameter("@Adds", DbTool.FixSqlParameter(cjdeltwo.Adds))
                            ,					new SqlParameter("@Code", DbTool.FixSqlParameter(cjdeltwo.Code))
                            ,					new SqlParameter("@Note", DbTool.FixSqlParameter(cjdeltwo.Note))
                                             };
                            rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql2.ToString(), parameters2);
                        }
                        if (cjdelthree != null)
                        {
                            StringBuilder strSql3 = new StringBuilder();
                            strSql3.Append("insert into  [Cj_Log]");
                            strSql3.Append("(OrderCode,OpenId,Mob,Jx,Jp,States,CTime,Ip,Pros,City,Adds,Code,Note)");
                            strSql3.Append(" values (@OrderCode,@OpenId,@Mob,@Jx,@Jp,@States,@CTime,@Ip,@Pros,@City,@Adds,@Code,@Note)");
                            strSql3.Append(";select SCOPE_IDENTITY()");
                            SqlParameter[] parameters3 = {
					                            new SqlParameter("@OrderCode", DbTool.FixSqlParameter(cjdelthree.OrderCode))
                            ,					new SqlParameter("@OpenId", DbTool.FixSqlParameter(cjdelthree.OpenId))
                            ,					new SqlParameter("@Mob", DbTool.FixSqlParameter(cjdelthree.Mob))
                            ,					new SqlParameter("@Jx", DbTool.FixSqlParameter(cjdelthree.Jx))
                            ,					new SqlParameter("@Jp", DbTool.FixSqlParameter(cjdelthree.Jp))
                            ,					new SqlParameter("@States", DbTool.FixSqlParameter(cjdelthree.States))
                            ,					new SqlParameter("@CTime", DbTool.FixSqlParameter(cjdelthree.CTime))
                            ,					new SqlParameter("@Ip", DbTool.FixSqlParameter(cjdelthree.Ip))
                            ,					new SqlParameter("@Pros", DbTool.FixSqlParameter(cjdelthree.Pros))
                            ,					new SqlParameter("@City", DbTool.FixSqlParameter(cjdelthree.City))
                            ,					new SqlParameter("@Adds", DbTool.FixSqlParameter(cjdelthree.Adds))
                            ,					new SqlParameter("@Code", DbTool.FixSqlParameter(cjdelthree.Code))
                            ,					new SqlParameter("@Note", DbTool.FixSqlParameter(cjdelthree.Note))
                                             };
                            rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql3.ToString(), parameters3);
                        }


                        if (rtn == i)
                        {
                            trans.Commit();
                            return rtn;
                        }
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
        #endregion

        #region 修改流量充值状态
        public int UpdateStatusId(Model.Cj_LogModel model, Model.OrderLogModel mdlog)
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

                        strSql.Append(" update Cj_Log set States=@States,City=@City where Id=@Id");
                        SqlParameter[] parameters = {	 
                               new SqlParameter("@States", model.States),
                               new SqlParameter("@City", model.City),  
                               new SqlParameter("@Id", model.Id) 
                             };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);

                        StringBuilder strSql2 = new StringBuilder();
                        strSql2.Append(" insert into OrderLog(Oid,OrderCode,Mob,UpTime,LStatus,Status,Notes)");
                        strSql2.Append(" values(@Oid,@OrderCode,@Mob,@UpTime,@LStatus,@Status,@Notes)");
                        SqlParameter[] parameters2 = {	 
                                new SqlParameter("@Oid", mdlog.OId),
                                new SqlParameter("@OrderCode", mdlog.OrderCode),
                                new SqlParameter("@Mob", mdlog.Mob),
                                new SqlParameter("@UpTime", mdlog.UpTime),
                                new SqlParameter("@LStatus", mdlog.LStatus),
                                new SqlParameter("@Status", mdlog.Status),
                               new SqlParameter("@Notes", mdlog.Notes) 
                             };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql2.ToString(), parameters2);

                        if (rtn == 2)
                        {
                            trans.Commit();
                            return rtn;
                        }
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
        #endregion

        #region 补发流量
        public int UpdateOrderCode(Model.Cj_LogModel model, Model.OrderLogModel mdlog)
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

                        strSql.Append(" update Cj_Log set States=@States,OrderCode=@OrderCode,Note=@Note where Id=@Id");
                        SqlParameter[] parameters = {	 
                               new SqlParameter("@States", model.States),
                               new SqlParameter("@OrderCode", model.OrderCode),  
                               new SqlParameter("@Note", model.Note),  
                               new SqlParameter("@Id", model.Id) 
                             };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);

                        StringBuilder strSql2 = new StringBuilder();
                        strSql2.Append(" insert into OrderLog(Oid,OrderCode,Mob,UpTime,LStatus,Status,Notes)");
                        strSql2.Append(" values(@Oid,@OrderCode,@Mob,@UpTime,@LStatus,@Status,@Notes)");
                        SqlParameter[] parameters2 = {	 
                                new SqlParameter("@Oid", mdlog.OId),
                                new SqlParameter("@OrderCode", mdlog.OrderCode),
                                new SqlParameter("@Mob", mdlog.Mob),
                                new SqlParameter("@UpTime", mdlog.UpTime),
                                new SqlParameter("@LStatus", mdlog.LStatus),
                                new SqlParameter("@Status", mdlog.Status),
                               new SqlParameter("@Notes", mdlog.Notes) 
                             };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql2.ToString(), parameters2);

                        if (rtn == 2)
                        {
                            trans.Commit();
                            return rtn;
                        }
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
        #endregion
        
 
    }
}
