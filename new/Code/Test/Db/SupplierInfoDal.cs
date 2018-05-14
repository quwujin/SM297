using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Db
{
    public class SupplierInfoDal
    {
        public string conn = SqlHelper.ConnectionString;

        public int Add(Model.SupplierInfoModel model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" insert into SupplierInfo(SupplierName,Types) values(");
            strSql.Append("@SupplierName,@Types)");
            SqlParameter[] parameters = {	 
                    new SqlParameter("@SupplierName", model.SupplierName),
                    new SqlParameter("@Types", model.Types)  
                 };
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }


        public int Update(Model.SupplierInfoModel model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update SupplierInfo set SupplierName=@SupplierName,Types=@Types  where SupplierId=@SupplierId");

            SqlParameter[] parameters = {	 
                    new SqlParameter("@SupplierName", model.SupplierName),
                    new SqlParameter("@Types", model.Types),
					new SqlParameter("@SupplierId", model.SupplierId)
                     
                 };
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }

        public DataTable GetList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder("select * from SupplierInfo where 1=1 " + sqlwhere + "  ");
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }


        public int Del(int sid)
        {
                string sqls = "delete from SupplierInfo  where SupplierId=" + sid;
                return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sqls);
        }


        public Model.SupplierInfoModel GetModel(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select* from SupplierInfo where  SupplierID=" + id);
            Model.SupplierInfoModel model = new Model.SupplierInfoModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            if (dr.Read())
            {
                model.SupplierId = Convert.ToInt32(dr["SupplierId"].ToString());
                model.SupplierName = dr["SupplierName"].ToString();
                model.Types = dr["Types"].ToString();

            }
            return model;
        }

    }
}
