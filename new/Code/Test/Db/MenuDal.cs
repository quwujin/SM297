using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
 

namespace Db
{
    public class MenuDal
    {
        public string conn = SqlHelper.ConnectionString;

        public int Add(Model.MenuInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into  MenuInfo(");
            strSql.Append("MenuName,MenuUrl,OrderId,Bid,StatusId)");
            strSql.Append(" values (");
            strSql.Append("@MenuName,@MenuUrl,@OrderId,@Bid,@StatusId)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@MenuName", model.MenuName),
					new SqlParameter("@MenuUrl", model.MenuUrl),
					new SqlParameter("@OrderId", model.OrderId),
					new SqlParameter("@Bid", model.Bid),
					new SqlParameter("@StatusId", model.StatusId)              
                 };

            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(),parameters);
         
        }



        public int Update(Model.MenuInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update   MenuInfo set ");
            strSql.Append("MenuName=@MenuName,MenuUrl=@MenuUrl,OrderId=@OrderId,Bid=@Bid,StatusId=@StatusId ");
            strSql.Append(" where MenuId = @MenuId ");
       
            SqlParameter[] parameters = {
					new SqlParameter("@MenuName", model.MenuName),
					new SqlParameter("@MenuUrl", model.MenuUrl),
					new SqlParameter("@OrderId", model.OrderId),
					new SqlParameter("@Bid", model.Bid),
					new SqlParameter("@StatusId", model.StatusId),
                    new SqlParameter("@MenuId", model.MenuId),
              
                 };

            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }



        public DataTable GetList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from MenuInfo where 1=1 ");
            sql.Append(sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }

        public int Del(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from MenuInfo where MenuId = " + id);
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString());
        }


        public Model.MenuInfoModel GetModel(int Id)
        {

            string sql = "select * from MenuInfo where MenuId =" + Id;
            Model.MenuInfoModel model = new Model.MenuInfoModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            if (dr.Read()) {
                model.MenuId = Convert.ToInt32(dr["MenuId"].ToString());
                model.MenuName = dr["MenuName"].ToString();
                model.MenuUrl = dr["MenuUrl"].ToString();
                model.Bid = Convert.ToInt32(dr["Bid"].ToString());
                model.OrderId = Convert.ToInt32(dr["OrderId"].ToString());
                model.StatusId = Convert.ToInt32(dr["StatusId"].ToString());
            }
            return model;
        }

        public int CheckGroupIdByAbsolutePath(string AbsoluteUrl,int GroupId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT COUNT(1) FROM [MenuInfo] m left join Permission p on p.MenuId=m.MenuId ");
            sql.Append("where [MenuUrl] like '%" + AbsoluteUrl + "%' and  GroupId=" + GroupId);
            return DbTool.ConvertObject<int>(SqlHelper.ExecuteScalar(conn, CommandType.Text, sql.ToString()), 0);
        }
         
        public int IsHave(int bid) {
            string sql = "select count(MenuId) from MenuInfo where bid=" + bid;
            int j =Convert.ToInt32(SqlHelper.ExecuteScalar(conn,CommandType.Text,sql));
            return j;
        }

    }
}
