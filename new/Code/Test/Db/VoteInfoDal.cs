using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Db;

namespace Db
{
    public class VoteInfoDal
    {
        public string conn = SqlHelper.ConnectionString;

        
        
        #region Dal Core Functional

        #region Add
        public int Add(Model.VoteInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into  [VoteInfo]");
            strSql.Append("(OrderCode,VoteDate,VoteDt,Ip,VoteName,VoteOpid,VoteId,States,OpenId,NickName,NickImg)");
            strSql.Append(" values (@OrderCode,@VoteDate,@VoteDt,@Ip,@VoteName,@VoteOpid,@VoteId,@States,@OpenId,@NickName,@NickImg)");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderCode", DbTool.FixSqlParameter(model.OrderCode))
,					new SqlParameter("@VoteDate", DbTool.FixSqlParameter(model.VoteDate))
,					new SqlParameter("@VoteDt", DbTool.FixSqlParameter(model.VoteDt))
,					new SqlParameter("@Ip", DbTool.FixSqlParameter(model.Ip))
,					new SqlParameter("@VoteName", DbTool.FixSqlParameter(model.VoteName))
,					new SqlParameter("@VoteOpid", DbTool.FixSqlParameter(model.VoteOpid))
,					new SqlParameter("@VoteId", DbTool.FixSqlParameter(model.VoteId))
,					new SqlParameter("@States", DbTool.FixSqlParameter(model.States))
,					new SqlParameter("@OpenId", DbTool.FixSqlParameter(model.OpenId))
,					new SqlParameter("@NickName", DbTool.FixSqlParameter(model.NickName))
,					new SqlParameter("@NickImg", DbTool.FixSqlParameter(model.NickImg))
                 };


            return DbTool.ConvertObject<int>(SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters),0);
         
        }
         
         
         #endregion

        #region AddandOrder
        public int AddandOrder(Model.VoteInfoModel model, Model.OrderInfoModel oddel, Model.OrderLogModel mdlog)
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
                        strSql.Append("insert into  [VoteInfo]");
                        strSql.Append("(OrderCode,VoteDate,VoteDt,Ip,VoteName,VoteOpid,VoteId,States,OpenId,NickName,NickImg)");
                        strSql.Append(" values (@OrderCode,@VoteDate,@VoteDt,@Ip,@VoteName,@VoteOpid,@VoteId,@States,@OpenId,@NickName,@NickImg)");
                        strSql.Append(";select SCOPE_IDENTITY()");
                        SqlParameter[] parameters = {
					        new SqlParameter("@OrderCode", DbTool.FixSqlParameter(model.OrderCode))
        ,					new SqlParameter("@VoteDate", DbTool.FixSqlParameter(model.VoteDate))
        ,					new SqlParameter("@VoteDt", DbTool.FixSqlParameter(model.VoteDt))
        ,					new SqlParameter("@Ip", DbTool.FixSqlParameter(model.Ip))
        ,					new SqlParameter("@VoteName", DbTool.FixSqlParameter(model.VoteName))
        ,					new SqlParameter("@VoteOpid", DbTool.FixSqlParameter(model.VoteOpid))
        ,					new SqlParameter("@VoteId", DbTool.FixSqlParameter(model.VoteId))
        ,					new SqlParameter("@States", DbTool.FixSqlParameter(model.States))
        ,					new SqlParameter("@OpenId", DbTool.FixSqlParameter(model.OpenId))
        ,					new SqlParameter("@NickName", DbTool.FixSqlParameter(model.NickName))
        ,					new SqlParameter("@NickImg", DbTool.FixSqlParameter(model.NickImg))
                         };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);

                        
                            StringBuilder strSql3 = new StringBuilder();
                            strSql3.Append("update orderInfo set Number=Number+1");
                            strSql3.Append(" where id=@id");
                            SqlParameter[] parameters3 = { 
					            new SqlParameter("@id", oddel.Id)				           
                                };
                            rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql3.ToString(), parameters3);
                        
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
                        
                        if (rtn == 3)
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
        #region Update
        public int Update(Model.VoteInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update VoteInfo set ");
            strSql.Append("OrderCode=@OrderCode,VoteDate=@VoteDate,VoteDt=@VoteDt,Ip=@Ip,VoteName=@VoteName,VoteOpid=@VoteOpid,VoteId=@VoteId,States=@States,OpenId=@OpenId,NickName=@NickName,NickImg=@NickImg ");
            strSql.Append(" where Id=@Id ");
       
            SqlParameter[] parameters = {
					new SqlParameter("@OrderCode", DbTool.FixSqlParameter(model.OrderCode))
,					new SqlParameter("@VoteDate", DbTool.FixSqlParameter(model.VoteDate))
,					new SqlParameter("@VoteDt", DbTool.FixSqlParameter(model.VoteDt))
,					new SqlParameter("@Ip", DbTool.FixSqlParameter(model.Ip))
,					new SqlParameter("@VoteName", DbTool.FixSqlParameter(model.VoteName))
,					new SqlParameter("@VoteOpid", DbTool.FixSqlParameter(model.VoteOpid))
,					new SqlParameter("@VoteId", DbTool.FixSqlParameter(model.VoteId))
,					new SqlParameter("@States", DbTool.FixSqlParameter(model.States))
,					new SqlParameter("@OpenId", DbTool.FixSqlParameter(model.OpenId))
,					new SqlParameter("@NickName", DbTool.FixSqlParameter(model.NickName))
,					new SqlParameter("@NickImg", DbTool.FixSqlParameter(model.NickImg))
,					new SqlParameter("@Id", model.Id)
                 };


            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }
         #endregion
         
        #region Delete
        public int Del(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from VoteInfo where Id = " + id);
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region BindDataReader
        protected void BindDataReader(Model.VoteInfoModel model,SqlDataReader dr)
        {

                model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
                model.OrderCode = DbTool.ConvertObject<System.String>(dr["OrderCode"]);
                model.VoteDate = DbTool.ConvertObject<System.DateTime>(dr["VoteDate"]);
                model.VoteDt = DbTool.ConvertObject<System.String>(dr["VoteDt"]);
                model.Ip = DbTool.ConvertObject<System.String>(dr["Ip"]);
                model.VoteName = DbTool.ConvertObject<System.String>(dr["VoteName"]);
                model.VoteOpid = DbTool.ConvertObject<System.String>(dr["VoteOpid"]);
                model.VoteId = DbTool.ConvertObject<System.Int32>(dr["VoteId"]);
                model.States = DbTool.ConvertObject<System.Int32>(dr["States"]);
                model.OpenId = DbTool.ConvertObject<System.String>(dr["OpenId"]);
                model.NickName = DbTool.ConvertObject<System.String>(dr["NickName"]);
                model.NickImg = DbTool.ConvertObject<System.String>(dr["NickImg"]);


        }
         #endregion
         
        #region AutoBindDataReader
        protected Model.VoteInfoModel AutoBindDataReader(SqlDataReader dr, params string[] fields)
        {

           var model = new Model.VoteInfoModel();
                if (DbTool.HasFields("Id", fields)) model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
                if (DbTool.HasFields("OrderCode", fields)) model.OrderCode = DbTool.ConvertObject<System.String>(dr["OrderCode"]);
                if (DbTool.HasFields("VoteDate", fields)) model.VoteDate = DbTool.ConvertObject<System.DateTime>(dr["VoteDate"]);
                if (DbTool.HasFields("VoteDt", fields)) model.VoteDt = DbTool.ConvertObject<System.String>(dr["VoteDt"]);
                if (DbTool.HasFields("Ip", fields)) model.Ip = DbTool.ConvertObject<System.String>(dr["Ip"]);
                if (DbTool.HasFields("VoteName", fields)) model.VoteName = DbTool.ConvertObject<System.String>(dr["VoteName"]);
                if (DbTool.HasFields("VoteOpid", fields)) model.VoteOpid = DbTool.ConvertObject<System.String>(dr["VoteOpid"]);
                if (DbTool.HasFields("VoteId", fields)) model.VoteId = DbTool.ConvertObject<System.Int32>(dr["VoteId"]);
                if (DbTool.HasFields("States", fields)) model.States = DbTool.ConvertObject<System.Int32>(dr["States"]);
                if (DbTool.HasFields("OpenId", fields)) model.OpenId = DbTool.ConvertObject<System.String>(dr["OpenId"]);
                if (DbTool.HasFields("NickName", fields)) model.NickName = DbTool.ConvertObject<System.String>(dr["NickName"]);
                if (DbTool.HasFields("NickImg", fields)) model.NickImg = DbTool.ConvertObject<System.String>(dr["NickImg"]);

           return model;

        }
         #endregion
         
        #endregion 

        #region GetList
        public DataTable GetList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from VoteInfo where 1=1 ");
            sql.Append(sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region distinct
        public DataTable GetDistinct(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select distinct(NickImg) from VoteInfo where 1=1 ");
            sql.Append(sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
         #endregion
        #region GetModel
        public Model.VoteInfoModel GetModel(int Id)
        {

            string sql = "select top 1 * from VoteInfo where Id =" + Id;
            Model.VoteInfoModel model = new Model.VoteInfoModel();
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
        public List<Model.VoteInfoModel> GetModelList()
        {

            List<Model.VoteInfoModel> result = new List<Model.VoteInfoModel>();
            string sql = "select * from VoteInfo where 1=1";
            Model.VoteInfoModel model = new Model.VoteInfoModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            //var fields = DbTool.GetReaderFieldNames(dr);
            while (dr.Read())
            {
                 //model = AutoBindDataReader(dr, fields);
                 model = new Model.VoteInfoModel(); 
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
            sql.Append("select count(*) from VoteInfo where 1=1 ");
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
            pages.TableName = "VoteInfo";
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
           pages.TableName = "VoteInfo";
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
           StringBuilder sql = new StringBuilder("select a.* from VoteInfo a  where 1=1 " + sqlstr);
           return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
        #endregion
         

        
 
    }
}
