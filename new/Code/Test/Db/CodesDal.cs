using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Db
{
    public  class CodesDal
    {
        private string conn = SqlHelper.ConnectionString;

        //UsedTime 数据默认空
        public int Add(Model.CodesModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" insert into Codes(SupplierId,PyId,StartDate,EndDate,MCode,MPassword,IsUsed,CreateTime,Mob,Price,Num,Options,Types,Limits,Notes) values(");
            strSql.Append("@SupplierId,@PyId,@StartDate,@EndDate,@MCode,@MPassword,@IsUsed,@CreateTime,@Mob,@Price,@Num,@Options,@Types,@Limits,@Notes)");
            SqlParameter[] parameters = {	 
                    new SqlParameter("@SupplierId",model.SupplierId),
                    new SqlParameter("@PyId",model.PyId),
                    new SqlParameter("@StartDate", model.StartDate),
                    new SqlParameter("@EndDate", model.EndDate),
                    new SqlParameter("@MCode", model.MCode),
                    new SqlParameter("@MPassword", model.MPassword),
                    new SqlParameter("@IsUsed", model.IsUsed),
                    new SqlParameter("@CreateTime", model.CreateTime),
                 //   new SqlParameter("@UsedTime", model.UsedTime),
                    new SqlParameter("@Mob", model.Mob),
                    new SqlParameter("@Price", model.Price),
                    new SqlParameter("@Num", model.Num),
                    new SqlParameter("@Options", model.Options),
                    new SqlParameter("@Types", model.Types),
                    new SqlParameter("@Limits", model.Limits),
                    new SqlParameter("@Notes", model.Notes)                   
                 };
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }


        //CreateTime,UsedTime,IsUsed,Mob 未修改
        public int Update(Model.CodesModel model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update Codes set SupplierId=@SupplierId,PyId=@PyId,StartDate=@StartDate,EndDate=@EndDate,MCode=@MCode," +
                "MPassword=@MPassword,Price=@Price,Num=@num,Options=@options,Types=@Types,Limits=@Limits,Notes=@Notes where id=@id");
            SqlParameter[] parameters = {	 
                    new SqlParameter("@SupplierId",model.SupplierId),
                    new SqlParameter("@PyId",model.PyId),
                    new SqlParameter("@StartDate", model.StartDate),
                    new SqlParameter("@EndDate", model.EndDate),
                    new SqlParameter("@MCode", model.MCode),
                    new SqlParameter("@MPassword", model.MPassword),
               //     new SqlParameter("@IsUsed", model.IsUsed),
               //     new SqlParameter("@CreateTime", model.CreateTime),
               //     new SqlParameter("@UsedTime", model.UsedTime),
                //    new SqlParameter("@Mob", model.Mob),
                    new SqlParameter("@Price", model.Price),
                    new SqlParameter("@Num", model.Num),
                    new SqlParameter("@Options", model.Options),
                    new SqlParameter("@Types", model.Types),
                    new SqlParameter("@Limits", model.Limits),
                    new SqlParameter("@Notes", model.Notes),
                    new SqlParameter("@id", model.Id)
                   
                 };
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }
        //修改两个电影码的记录，订单表的状态，订单日志
        public int SetsCode(Model.CodesModel codes1, Model.CodesModel codes2, Model.OrderInfoModel model, Model.OrderLogModel mdlog)
        {
            int rtn = 0;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql10 = new StringBuilder();
                        strSql10.Append("update Codes set IsUsed=@IsUsed,Mob=@Mob,UsedTime=@UsedTime ");
                        strSql10.Append(" where id=@id");
                        SqlParameter[] parameter10 = {
					            new SqlParameter("@IsUsed", codes1.IsUsed),
					            new SqlParameter("@mob", codes1.Mob),
					            new SqlParameter("@UsedTime", codes1.UsedTime),
					            new SqlParameter("@id", codes1.Id)				           
                             };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql10.ToString(), parameter10);

                        StringBuilder strSql2 = new StringBuilder();
                        strSql2.Append("update Codes set IsUsed=@IsUsed,Mob=@Mob,UsedTime=@UsedTime ");
                        strSql2.Append(" where id=@id");
                         SqlParameter[] parameter2 = {
					            new SqlParameter("@IsUsed", codes2.IsUsed),
					            new SqlParameter("@mob", codes2.Mob),
					            new SqlParameter("@UsedTime", codes2.UsedTime),
					            new SqlParameter("@id", codes2.Id)				           
                             };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql2.ToString(), parameter2);

                         StringBuilder strSql = new StringBuilder();
                        strSql.Append(" update OrderInfo set States=@States where Id=@Id");
                        SqlParameter[] parameters = {	 
                                new SqlParameter("@States", model.States),
                               new SqlParameter("@Id", model.Id) 
                   
                             };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);

                        StringBuilder strSql1 = new StringBuilder();
                        strSql1.Append(" insert into OrderLog(Oid,OrderCode,Mob,UpTime,LStatus,Status,Notes)");
                        strSql1.Append(" values(@Oid,@OrderCode,@Mob,@UpTime,@LStatus,@Status,@Notes)");
                        SqlParameter[] parameters2 = {	 
                                new SqlParameter("@Oid", mdlog.OId),
                                new SqlParameter("@OrderCode", mdlog.OrderCode),
                                new SqlParameter("@Mob", mdlog.Mob),
                                new SqlParameter("@UpTime", mdlog.UpTime),
                                new SqlParameter("@LStatus", mdlog.LStatus),
                                new SqlParameter("@Status", mdlog.Status),
                                new SqlParameter("@Notes", mdlog.Notes) 
                             };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql1.ToString(), parameters2);
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
        //修改两个电影码的记录
        public int SetCode(Model.CodesModel codes1, Model.CodesModel codes2) {
            int rtn = 0;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction()) 
                {
                    try
                    {

                        StringBuilder strSql10 = new StringBuilder();
                        strSql10.Append("update Codes set IsUsed=@IsUsed,Mob=@Mob,UsedTime=@UsedTime ");
                        strSql10.Append(" where id=@id");
                        SqlParameter[] parameter10 = {
					            new SqlParameter("@IsUsed", codes1.IsUsed),
					            new SqlParameter("@mob", codes1.Mob),
					            new SqlParameter("@UsedTime", codes1.UsedTime),
					            new SqlParameter("@id", codes1.Id)				           
                             };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql10.ToString(), parameter10);

                        StringBuilder strSql2 = new StringBuilder();
                        strSql2.Append("update Codes set IsUsed=@IsUsed,Mob=@Mob,UsedTime=@UsedTime ");
                        strSql2.Append(" where id=@id");
                        SqlParameter[] parameter2 = {
					            new SqlParameter("@IsUsed", codes2.IsUsed),
					            new SqlParameter("@mob", codes2.Mob),
					            new SqlParameter("@UsedTime", codes2.UsedTime),
					            new SqlParameter("@id", codes2.Id)				           
                             };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql2.ToString(), parameter2);
                        
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

        public int SetUsed(Model.OrderLogModel mdlog, Model.CodesModel codes1,Model.CodesModel codes2)
        {
            int rtn = 0;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {

                        StringBuilder strSql10 = new StringBuilder();
                        strSql10.Append("update Codes set IsUsed=@IsUsed,Mob=@Mob,UsedTime=@UsedTime ");
                        strSql10.Append(" where id=@id");
                        SqlParameter[] parameter10 = {
					            new SqlParameter("@IsUsed", codes1.IsUsed),
					            new SqlParameter("@mob", codes1.Mob),
					            new SqlParameter("@UsedTime", codes1.UsedTime),
					            new SqlParameter("@id", codes1.Id)				           
                             };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql10.ToString(), parameter10);

                        StringBuilder strSql2 = new StringBuilder();
                        strSql2.Append("update Codes set IsUsed=@IsUsed,Mob=@Mob,UsedTime=@UsedTime ");
                        strSql2.Append(" where id=@id");
                        SqlParameter[] parameter2 = {
					            new SqlParameter("@IsUsed", codes2.IsUsed),
					            new SqlParameter("@mob", codes2.Mob),
					            new SqlParameter("@UsedTime", codes2.UsedTime),
					            new SqlParameter("@id", codes2.Id)				           
                             };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql2.ToString(), parameter2);

                            StringBuilder strSql3 = new StringBuilder();
                            strSql3.Append(" insert into OrderLog(Oid,OrderCode,Mob,UpTime,LStatus,Status,Notes)");
                            strSql3.Append(" values(@Oid,@OrderCode,@Mob,@UpTime,@LStatus,@Status,@Notes)");
                            SqlParameter[] parameters3 = {	 
                                new SqlParameter("@Oid", mdlog.OId),
                                new SqlParameter("@OrderCode", mdlog.OrderCode),
                                new SqlParameter("@Mob", mdlog.Mob),
                                new SqlParameter("@UpTime", mdlog.UpTime),
                                new SqlParameter("@LStatus", mdlog.LStatus),
                                new SqlParameter("@Status", mdlog.Status),
                               new SqlParameter("@Notes", mdlog.Notes) 
                             };
                            rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql3.ToString(), parameters3);
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


        public DataTable GetList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder("select * from Codes where 1=1 " + sqlwhere + " ");
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }



        public int Del(int cityid)
        {
            string sql = "delete from Codes where Id=" + cityid;
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql);
        }


        public Model.CodesModel GetModel(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select a.*  from Codes  a   where a.Id=" + id);
            Model.CodesModel model = new Model.CodesModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            if (dr.Read())
            {
                model.Id = Convert.ToInt32(dr["Id"].ToString());
                model.SupplierId = Convert.ToInt32(dr["SupplierId"].ToString());
                model.PyId = Convert.ToInt32(dr["PyId"].ToString());
                model.StartDate = Convert.ToDateTime(dr["StartDate"].ToString());
                model.EndDate = Convert.ToDateTime(dr["EndDate"].ToString());
                model.MCode = dr["MCode"].ToString();
                model.MPassword = dr["MPassword"].ToString();
                model.IsUsed = Convert.ToInt32(dr["IsUsed"].ToString());
                model.CreateTime = Convert.ToDateTime(dr["CreateTime"].ToString());
                if (dr["UsedTime"] != DBNull.Value)
                {
                    model.UsedTime = Convert.ToDateTime(dr["UsedTime"].ToString());
                }
                model.Mob = dr["Mob"].ToString();
                model.Price = dr["Price"].ToString();
                model.Num = Convert.ToInt32(dr["Num"].ToString());
                model.Options = dr["Options"].ToString();
                model.Types = dr["Types"].ToString();
                model.Limits = dr["Limits"].ToString();
                model.Notes = dr["Notes"].ToString();
            }
		 dr.Close();
            return model;
        }
        public Model.CodesModel GetSendModel(string sqlStr)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select a.*  from Codes a where 1=1 ");
            sql.Append(sqlStr);
            Model.CodesModel model = new Model.CodesModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            if (dr.Read())
            {
                model.Id = Convert.ToInt32(dr["Id"].ToString());
                model.SupplierId = Convert.ToInt32(dr["SupplierId"].ToString());
                model.PyId = Convert.ToInt32(dr["PyId"].ToString());
                model.StartDate = Convert.ToDateTime(dr["StartDate"].ToString());
                model.EndDate = Convert.ToDateTime(dr["EndDate"].ToString());
                model.MCode = dr["MCode"].ToString();
                model.MPassword = dr["MPassword"].ToString();
                model.IsUsed = Convert.ToInt32(dr["IsUsed"].ToString());
                model.CreateTime = Convert.ToDateTime(dr["CreateTime"].ToString());
                if (dr["UsedTime"] != DBNull.Value)
                {
                    model.UsedTime = Convert.ToDateTime(dr["UsedTime"].ToString());
                }
                model.Mob = dr["Mob"].ToString();
                model.Price = dr["Price"].ToString();
                model.Num = Convert.ToInt32(dr["Num"].ToString());
                model.Options = dr["Options"].ToString();
                model.Types = dr["Types"].ToString();
                model.Limits = dr["Limits"].ToString();
                model.Notes = dr["Notes"].ToString();
            }
		 dr.Close();
            return model;
        }


        /// <summary>
        /// 分页计算总数
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <param name="joinString"></param>
        /// <returns></returns>
        public int GetCount(string sqlstr, string joinString)
        {
            Model.PageInfo pages = new Model.PageInfo();

            pages.SqlWhere = sqlstr;
            pages.ReturnFileds = "Id";
            pages.TableName = " Codes ";
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


        public DataTable GetList(string sqlstr, int pageindex, int pagesize)
        {
            Model.PageInfo pages = new Model.PageInfo();
            pages.PageIndex = pageindex;
            pages.PageSize = pagesize;
            pages.SqlWhere = sqlstr;
            pages.ReturnFileds = "t.*";
            pages.TableName = " Codes ";
            pages.JoinTable = "  ";
            pages.CountFields = " a.Id ";
            pages.OrderString = " order by t.Id desc";
            pages.SelectFileds = " a.*  ";
            pages.doCount = 0;
            PageHelper p = new PageHelper();
            DataTable dt = p.GetList(pages);
            return dt;
        }


        public DataTable GetPageList(string sqlstr, int pageindex, int pagesize)
        {
            Model.PageInfo pages = new Model.PageInfo();
            pages.PageIndex = pageindex;
            pages.PageSize = pagesize;
            pages.SqlWhere = sqlstr;
            pages.ReturnFileds = "t.*";
            pages.TableName = " Codes ";
            pages.JoinTable = " left join SupplierInfo s on s.SupplierId=a.SupplierId left join Policy p on p.Id=a.PyId ";
            pages.CountFields = " a.Id ";
            pages.OrderString = " order by t.Id desc";
            pages.SelectFileds = " a.*,s.SupplierName,p.Title ";
            pages.doCount = 0;
            PageHelper p = new PageHelper();
            DataTable dt = p.GetList(pages);
            return dt;
        }

        public DataTable GetExcelList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder("select a.*,s.SupplierName,p.Title from Codes a left join SupplierInfo s on s.SupplierId=a.SupplierId left join Policy p on p.Id=a.PyId  where 1=1 " + sqlwhere + " ");
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
        //CreateTime,UsedTime,IsUsed,Mob 未修改
        public int UpdateUse(Model.CodesModel model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update Codes set IsUsed=@IsUsed,UsedTime=@UsedTime,Mob=@Mob where id=@id");
            SqlParameter[] parameters = {	 
                   new SqlParameter("@IsUsed", model.IsUsed),
                    new SqlParameter("@UsedTime", model.UsedTime),
                    new SqlParameter("@Mob", model.Mob),
                    new SqlParameter("@id", model.Id)
                   
                 };
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }


    }
}

