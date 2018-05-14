using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Db
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data.SqlClient;
    using System.Data;


    namespace Dal
    {
        public class WxKeysDal
        {
            public string conn = SqlHelper.ConnectionString;

            public int Add(Model.WxMenuInfoModel model)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into  Wx_keys(");
                strSql.Append("Name,Type,Keys,Bid,OrderId,Url)");
                strSql.Append(" values (");
                strSql.Append("@Name,@Type,@Keys,@Bid,@OrderId,@Url)");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
					new SqlParameter("@Name", model.Name),
					new SqlParameter("@Type", model.Type),
					new SqlParameter("@Keys", model.Keys),
					new SqlParameter("@Bid", model.Bid),
					new SqlParameter("@OrderId", model.OrderId),
                    new SqlParameter("@Url",model.Url)
                 };

                return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

            }



            public int Update(Model.WxMenuInfoModel model)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update   Wx_keys set ");
                strSql.Append("Name=@Name,Type=@Type,Keys=@Keys,Mp3=@Mp3,Pic=@Pic,Contents=@Contents, OrderId=@OrderId,Url=@Url,ClassName=@ClassName ");
                strSql.Append(" where Id = @Id ");

                SqlParameter[] parameters = {
					 new SqlParameter("@Name", model.Name),
					new SqlParameter("@Type", model.Type),
					new SqlParameter("@Keys", model.Keys),
				    new SqlParameter("@ClassName",model.ClassName),
					new SqlParameter("@OrderId", model.OrderId),
                    new SqlParameter("@Url",model.Url),
                    new SqlParameter("@Contents",model.Contents),
                    new SqlParameter("@Pic",model.Pic),
                    new SqlParameter("@Mp3",model.Mp3),
                    new SqlParameter("Id",model.Id)

              
                 };

                return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

            }



            public int Updates(Model.WxMenuInfoModel model)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update   Wx_keys set ");
                strSql.Append("Name=@Name ");
                strSql.Append(" where Id = @Id ");

                SqlParameter[] parameters = {
					 new SqlParameter("@Name", model.Name),
					 
                    new SqlParameter("Id",model.Id)

              
                 };

                return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

            }



            public DataTable GetList(string sqlwhere)
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select * from Wx_keys where 1=1 ");
                sql.Append(sqlwhere);
                return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
            }

            public int Del(int id, int bid)
            {
                StringBuilder sql = new StringBuilder();


                sql.Append("delete from Wx_keys where Id = " + id);

                return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString());
            }


            public Model.WxMenuInfoModel GetModel(int Id)
            {

                string sql = "select * from Wx_keys where Id =" + Id;
                Model.WxMenuInfoModel model = new Model.WxMenuInfoModel();
                SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
                if (dr.Read())
                {
                    model.Id = Convert.ToInt32(dr["Id"].ToString());
                    model.Name = dr["Name"].ToString();
                    model.Url = dr["Url"].ToString();
                    model.Bid = Convert.ToInt32(dr["Bid"].ToString());
                    model.OrderId = Convert.ToInt32(dr["OrderId"].ToString());
                    model.ClassName = dr["ClassName"].ToString();
                    model.Type = dr["Type"].ToString();
                    model.Contents = dr["Contents"].ToString();
                    model.Keys = dr["Keys"].ToString();
                    model.Pic = dr["pic"].ToString();
                    model.Mp3 = dr["mp3"].ToString();

                }
                return model;
            }


            public int IsHave(string sqlwhere)
            {
                string sql = "select count(Id) from Wx_keys where 1=1 " + sqlwhere;
                int j = Convert.ToInt32(SqlHelper.ExecuteScalar(conn, CommandType.Text, sql));
                return j;
            }

        }
    }
}
