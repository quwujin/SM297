using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Db
{
    public class OrderLogDal
    {

        private string conn = SqlHelper.ConnectionString;


        public int Add(Model.OrderLogModel model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" insert into OrderLog(Oid,OrderCode,Mob,UpTime,LStatus,Status,Notes)");
            strSql.Append(" values(@Oid,@OrderCode,@Mob,@UpTime,@LStatus,@Status,@Notes)");
            SqlParameter[] parameters = {	 
                new SqlParameter("@Oid", model.OId),
                new SqlParameter("@OrderCode", model.OrderCode),
                new SqlParameter("@Mob", model.Mob),
                new SqlParameter("@UpTime", model.UpTime),
                new SqlParameter("@LStatus", model.LStatus),
                new SqlParameter("@Status", model.Status),
                new SqlParameter("@Notes", model.Notes) 
                };
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
        }

        public int Update(Model.OrderLogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update OrderLog set Oid=@Oid,OrderCode=@OrderCode,Mob=@Mob,UpTime=@UpTime,LStatus=@LStatus,Status=@Status,Notes=@Notes where ID=@ID");
            SqlParameter[] parameters = {	 
                    new SqlParameter("@Oid", model.OId),
                    new SqlParameter("@OrderCode", model.OrderCode),
                    new SqlParameter("@Mob", model.Mob),
                    new SqlParameter("@UpTime", model.UpTime),
                    new SqlParameter("@LStatus", model.LStatus),
                    new SqlParameter("@Status", model.Status),
                   new SqlParameter("@Notes", model.Notes),
                   new SqlParameter("@Id", model.Id) 
                 };
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
        }

        public DataTable GetList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder("select * from OrderLog where 1=1 " + sqlwhere + " ");
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }




        public int Del(int cityid)
        {
            string sql = "delete from OrderLog where Id=" + cityid;
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql);
        }


        public Model.OrderLogModel GetModel(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select a.*  from OrderLog  a   where a.Id=" + id);
            Model.OrderLogModel model = new Model.OrderLogModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            if (dr.Read())
            {
                model.Id = Convert.ToInt32(dr["ID"].ToString());
                model.OId = Convert.ToInt32(dr["OId"].ToString());
                model.OrderCode = dr["OrderCode"].ToString();
                model.Mob = dr["Mob"].ToString();
                model.UpTime = Convert.ToDateTime(dr["UpTime"].ToString());
                model.LStatus = Convert.ToInt32(dr["LStatus"].ToString());
                model.Status = Convert.ToInt32(dr["Status"].ToString());
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
            pages.TableName = " OrderLog ";
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
            pages.TableName = " OrderLog ";
            pages.JoinTable = "  ";
            pages.CountFields = " a.Id ";
            pages.OrderString = " order by a.Id desc";
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
            pages.TableName = " OrderLog ";
            pages.JoinTable = " ";
            pages.CountFields = " a.Id ";
            pages.OrderString = " order by a.Id desc";
            pages.SelectFileds = " a.* ";
            pages.doCount = 0;
            PageHelper p = new PageHelper();
            DataTable dt = p.GetList(pages);
            return dt;
        }


    }
}