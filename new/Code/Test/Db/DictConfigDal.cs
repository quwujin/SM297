using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Db
{
    public class DictConfigDal
    {
        private string conn = SqlHelper.ConnectionString;


        public int Add(Model.DictConfigModel model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" insert into DictConfig(Title,Val,Bid,OrderId) values(");
            strSql.Append("@Title,@Val,@Bid,@OrderId)");
            SqlParameter[] parameters = {	 
                    new SqlParameter("@Title", model.Title),
                    new SqlParameter("@Val", model.Val),
                    new SqlParameter("@Bid", model.Bid), 
                    new SqlParameter("@OrderId", model.OrderId),   
                 };
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }


        public int Update(Model.DictConfigModel model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update DictConfig set Title=@Title,Val=@Val,Bid=@Bid,OrderId=@OrderId where Id=@Id");
            SqlParameter[] parameters = {	 
                    new SqlParameter("@Title", model.Title),
                    new SqlParameter("@Val", model.Val),
                    new SqlParameter("@Bid", model.Bid),
                    new SqlParameter("@OrderId", model.OrderId),
                    new SqlParameter("@Id", model.Id)
                 };
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }


        public DataTable GetList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder("select * from DictConfig where 1=1 " + sqlwhere + " ");
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }

        public int Del(int cityid)
        {
            string sql = "delete from DictConfig where Id=" + cityid;
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql);
        }


        public Model.DictConfigModel GetModel(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select a.* from DictConfig a where a.Id=" + id);
            Model.DictConfigModel model = new Model.DictConfigModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            if (dr.Read())
            {
                model.Id = Convert.ToInt32(dr["Id"].ToString());
                model.Title = dr["Title"].ToString();
                model.Val = dr["Val"].ToString();
                model.Bid = Convert.ToInt32(dr["Bid"].ToString());
                model.OrderId = Convert.ToInt32(dr["OrderId"].ToString());
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
            pages.TableName = " DictConfig ";
            pages.JoinTable = " ";
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
            pages.TableName = " DictConfig ";
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
            pages.TableName = " DictConfig ";
            pages.JoinTable = " ";
            pages.CountFields = " a.Id ";
            pages.OrderString = " order by t.Id desc";
            pages.SelectFileds = " a.* ";
            pages.doCount = 0;
            PageHelper p = new PageHelper();
            DataTable dt = p.GetList(pages);
            return dt;
        }
        public int IsHave(int bid)
        {
            string sql = "select count(Id) from DictConfig where bid=" + bid;
            int j = Convert.ToInt32(SqlHelper.ExecuteScalar(conn, CommandType.Text, sql));
            return j;
        }
    }
    
        
}

