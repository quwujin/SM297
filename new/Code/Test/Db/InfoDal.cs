using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Db
{
    public class InfoDal
    {
        public string conn = SqlHelper.ConnectionString;

        public int Add(Model.InfoModel model)
        {   
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into hdINfo(Title,Notes) values(");
            strSql.Append("@Title,@Notes);select @@IDENTITY");
            SqlParameter[] parameters = {	 
                        new SqlParameter("@Title", model.Title),
                        
                        new SqlParameter("@Notes", model.Notes)
                 };
            try
            {
                return Convert.ToInt16(SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters));
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Update(Model.InfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update hdINfo set Title=@Title,Notes=@Notes where Id=@Id ");
            SqlParameter[] parameters = {
                        new SqlParameter("@Title", model.Title),

                        new SqlParameter("@Notes", model.Notes),
                        new SqlParameter("@Id",model.Id)
                 };
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
        }
            
        public Model.InfoModel GetModel(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select top 1 * from hdINfo  where Id=" + id);
            Model.InfoModel model = new Model.InfoModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            if (dr.Read())
            {
                model.Id = Convert.ToInt32(dr["Id"].ToString());
                model.Title = dr["Title"].ToString();
                model.Notes = dr["Notes"].ToString();
                
            }
            dr.Close();
            return model;
        }

        #region GetList
        public DataTable GetList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from hdINfo where 1=1 ");
            sql.Append(sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
        #endregion

    }
}
