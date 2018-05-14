using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Db
{
    public class PolicyDal
    {
        public string conn = SqlHelper.ConnectionString;

        public int Add(Model.PolicyModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Policy(SId,Title,Price,Notes) values(");
            strSql.Append("@SId,@Title,@Price,@Notes)");
            SqlParameter[] parameters = {	 
                        new SqlParameter("@SId", model.SId),
                        new SqlParameter("@Title", model.Title),
                        new SqlParameter("@Price", model.Price),
                        new SqlParameter("@Notes", model.Notes)
                 };
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }


        public int Update(Model.PolicyModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update  Policy set SId=@SId,Title=@Title,Price=@Price,Notes=@Notes where Id=@Id ");
            SqlParameter[] parameters = {	 
                        new SqlParameter("@SId", model.SId),
                        new SqlParameter("@Title", model.Title),
                        new SqlParameter("@Price", model.Price),
                        new SqlParameter("@Notes", model.Notes),
                        new SqlParameter("@Id",model.Id)
                 };
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
        }

        public DataTable GetSupplierList(Model.PolicyModel model)
        {
            StringBuilder sql = new StringBuilder("select * from Policy where SId=" + model.SId);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }

        public Model.PolicyModel GetInfo(Model.PolicyModel m)
        {
            StringBuilder sql = new StringBuilder("select * from Policy where SId=" + m.SId + " and Title='" + m.Title + "'");
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            Model.PolicyModel model = new Model.PolicyModel();
            model.Id = 0;
            if (dr.Read())
            {
                model.Id = Convert.ToInt32(dr["Id"].ToString());
                model.SId = Convert.ToInt32(dr["SId"].ToString());
                model.Title = dr["Title"].ToString();
                model.Notes = dr["Notes"].ToString();
                if (dr["Price"] != DBNull.Value) { model.Price = Decimal.Parse(dr["Price"].ToString()); } else { model.Price = 0; }
            }
            return model;
        }

        public DataTable GetList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder("select * from Policy where 1=1 " + sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }

        public int Del(int id)
        {
            string sql = "delete from Policy where Id=" + id;
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql);
        }

        public Model.PolicyModel GetModel(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from Policy  where Id=" + id);
            Model.PolicyModel model = new Model.PolicyModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            if (dr.Read())
            {
                model.Id = Convert.ToInt32(dr["Id"].ToString());
                model.SId = Convert.ToInt32(dr["SId"].ToString());
                model.Title = dr["Title"].ToString();
                model.Notes = dr["Notes"].ToString();
                if (dr["Price"] != DBNull.Value) { model.Price = Decimal.Parse(dr["Price"].ToString()); } else { model.Price = 0; }
            }
            return model;
        }
    }
}