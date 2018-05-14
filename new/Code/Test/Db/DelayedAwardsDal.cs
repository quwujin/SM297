using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data; 

namespace Db
{
    public class DelayedAwardsDal
    {
        public string conn = SqlHelper.ConnectionString;

        
        
        #region Dal Core Functional

        #region Add
        public int Add(Model.DelayedAwardsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into  [DelayedAwards]");
            strSql.Append("(OrderId,StatusId,CreateTime,DelayedTime,UpdateTime,Remark)");
            strSql.Append(" values (@OrderId,@StatusId,@CreateTime,@DelayedTime,@UpdateTime,@Remark)");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderId", DbTool.FixSqlParameter(model.OrderId))
,					new SqlParameter("@StatusId", DbTool.FixSqlParameter(model.StatusId))
,					new SqlParameter("@CreateTime", DbTool.FixSqlParameter(model.CreateTime))
,					new SqlParameter("@DelayedTime", DbTool.FixSqlParameter(model.DelayedTime))
,					new SqlParameter("@UpdateTime", DbTool.FixSqlParameter(model.UpdateTime))
,					new SqlParameter("@Remark", DbTool.FixSqlParameter(model.Remark))
                 };


            return DbTool.ConvertObject<int>(SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters),0);
         
        }
         
         
         #endregion
         
        #region Update
        public int Update(Model.DelayedAwardsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update DelayedAwards set ");
            strSql.Append("OrderId=@OrderId,StatusId=@StatusId,CreateTime=@CreateTime,DelayedTime=@DelayedTime,UpdateTime=@UpdateTime,Remark=@Remark ");
            strSql.Append(" where Id=@Id ");
       
            SqlParameter[] parameters = {
					new SqlParameter("@OrderId", DbTool.FixSqlParameter(model.OrderId))
,					new SqlParameter("@StatusId", DbTool.FixSqlParameter(model.StatusId))
,					new SqlParameter("@CreateTime", DbTool.FixSqlParameter(model.CreateTime))
,					new SqlParameter("@DelayedTime", DbTool.FixSqlParameter(model.DelayedTime))
,					new SqlParameter("@UpdateTime", DbTool.FixSqlParameter(model.UpdateTime))
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
            sql.Append("delete from DelayedAwards where Id = " + id);
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region BindDataReader
        protected void BindDataReader(Model.DelayedAwardsModel model,SqlDataReader dr)
        {

                model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
                model.OrderId = DbTool.ConvertObject<System.Int32>(dr["OrderId"]);
                model.StatusId = DbTool.ConvertObject<System.Int32>(dr["StatusId"]);
                model.CreateTime = DbTool.ConvertObject<System.DateTime>(dr["CreateTime"]);
                model.DelayedTime = DbTool.ConvertObject<System.DateTime>(dr["DelayedTime"]);
                model.UpdateTime = DbTool.ConvertObject<System.DateTime>(dr["UpdateTime"]);
                model.Remark = DbTool.ConvertObject<System.String>(dr["Remark"]);


        }
         #endregion
         
        #region AutoBindDataReader
        protected Model.DelayedAwardsModel AutoBindDataReader(SqlDataReader dr, params string[] fields)
        {

           var model = new Model.DelayedAwardsModel();
                if (DbTool.HasFields("Id", fields)) model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
                if (DbTool.HasFields("OrderId", fields)) model.OrderId = DbTool.ConvertObject<System.Int32>(dr["OrderId"]);
                if (DbTool.HasFields("StatusId", fields)) model.StatusId = DbTool.ConvertObject<System.Int32>(dr["StatusId"]);
                if (DbTool.HasFields("CreateTime", fields)) model.CreateTime = DbTool.ConvertObject<System.DateTime>(dr["CreateTime"]);
                if (DbTool.HasFields("DelayedTime", fields)) model.DelayedTime = DbTool.ConvertObject<System.DateTime>(dr["DelayedTime"]);
                if (DbTool.HasFields("UpdateTime", fields)) model.UpdateTime = DbTool.ConvertObject<System.DateTime>(dr["UpdateTime"]);
                if (DbTool.HasFields("Remark", fields)) model.Remark = DbTool.ConvertObject<System.String>(dr["Remark"]);

           return model;

        }
         #endregion
         
        #endregion 

        #region GetList
        public DataTable GetList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from DelayedAwards where 1=1 ");
            sql.Append(sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region GetModel
        public Model.DelayedAwardsModel GetModel(int Id)
        {

            string sql = "select top 1 * from DelayedAwards where Id =" + Id;
            Model.DelayedAwardsModel model = new Model.DelayedAwardsModel();
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
        public List<Model.DelayedAwardsModel> GetModelList()
        {

            List<Model.DelayedAwardsModel> result = new List<Model.DelayedAwardsModel>();
            string sql = "select * from DelayedAwards where 1=1";
            Model.DelayedAwardsModel model = new Model.DelayedAwardsModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            //var fields = DbTool.GetReaderFieldNames(dr);
            while (dr.Read())
            {
                 //model = AutoBindDataReader(dr, fields);
                 model = new Model.DelayedAwardsModel(); 
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
            sql.Append("select count(*) from DelayedAwards where 1=1 ");
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
            pages.TableName = "DelayedAwards";
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
           pages.TableName = "DelayedAwards";
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
           StringBuilder sql = new StringBuilder("select a.* from DelayedAwards a  where 1=1 " + sqlstr);
           return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
        #endregion


        #region Custom Function

        #region GetModelListBySqlwhere
        public List<Model.DelayedAwardsModel> GetModelList(int Top, string sqlwhere)
        {

            List<Model.DelayedAwardsModel> result = new List<Model.DelayedAwardsModel>();
            string sql = "select " + (Top > 0 ? "Top " + Top : "") + " * from DelayedAwards where 1=1 " + sqlwhere;
            Model.DelayedAwardsModel model = new Model.DelayedAwardsModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            //var fields = DbTool.GetReaderFieldNames(dr);
            while (dr.Read())
            {
                //model = AutoBindDataReader(dr, fields);
                model = new Model.DelayedAwardsModel();
                BindDataReader(model, dr);
                result.Add(model);
            }
            dr.Close();
            return result;
        }
        #endregion

        #region Edit

        public int Edit(Model.DelayedAwardsModel model, Model.OrderInfoModel OrderModel)
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
                        strSql.Append("update OrderInfo set ");
                        strSql.Append("IsGrant=@IsGrant,GrantTime=@GrantTime,States=@States");
                        strSql.Append(" where Id=@Id ");

                        SqlParameter[] parameters = {
                                        new SqlParameter("@IsGrant", DbTool.FixSqlParameter(OrderModel.IsGrant))
                        ,					new SqlParameter("@GrantTime", DbTool.FixSqlParameter(OrderModel.GrantTime)) 
                        ,					new SqlParameter("@States", DbTool.FixSqlParameter(OrderModel.States)) 
                        ,					new SqlParameter("@Id", OrderModel.Id)
                                        };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);

                        StringBuilder strSql2 = new StringBuilder();
                        strSql2.Append("update DelayedAwards set ");
                        strSql2.Append("StatusId=@StatusId,UpdateTime=@UpdateTime");
                        strSql2.Append(" where Id=@Id ");
                        SqlParameter[] parameters2 = {	 
                            new SqlParameter("@StatusId", model.StatusId),
                            new SqlParameter("@UpdateTime", model.UpdateTime),
                            new SqlParameter("@Id", model.Id) 
                        };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql2.ToString(), parameters2);
                        if (rtn > 1)
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

        public int Update(List<Model.DelayedAwardsModel> modellist)
        {
            if (modellist == null || modellist.Count == 0)
                return 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("begin tran ");
            strSql.Append("begin try ");
            foreach (Model.DelayedAwardsModel model in modellist)
            {
                strSql.Append(@" UPDATE [DelayedAwards] SET [StatusId]=2 WHERE Id=");
                strSql.Append(model.Id + ";");
            }
            strSql.Append("COMMIT TRAN ");
            strSql.Append("SELECT 1 ");
            strSql.Append("end try ");
            strSql.Append("BEGIN CATCH ");
            strSql.Append("ROLLBACK TRAN ");
            strSql.Append("SELECT 0 ");
            strSql.Append("end catch ");

            try
            {
                return Convert.ToInt32(SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), new SqlParameter[] { }));
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        #endregion


        #endregion
 
    }
}
