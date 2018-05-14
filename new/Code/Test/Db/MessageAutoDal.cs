using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Db
{
    public class MessageAutoDal
    {
        public string conn = SqlHelper.ConnectionString;

        public int Add(Model.AddReplayModel model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into  Wx_MessageAuto(");
            strSql.Append("Contents,Types,Pic)");
            strSql.Append(" values (");
            strSql.Append("@Contents,@Types,@Pic,)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Contents", model.Contents),
					new SqlParameter("@Types", model.Types),
					new SqlParameter("@Pic", model.Pic)
					            
                 };

            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
        }



        public int Update(Model.AddReplayModel model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Wx_MessageAuto set ");
            strSql.Append("Contents='"+model.Contents+"',Types='"+model.Types+"',Pic='"+model.Pic+"' where Id="+model.Id);
         

            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString());
        }


        public Model.AddReplayModel GetModel(int Id)
        {

            string sql = "select * from Wx_MessageAuto where id =" + Id;
            Model.AddReplayModel model = new Model.AddReplayModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            if (dr.Read())
            {
                model.Id = Id;
                model.Contents = dr["Contents"].ToString();
                model.Types = dr["Types"].ToString();
                model.Pic = dr["Pic"].ToString();

            }
            return model;
        }
    }
}
