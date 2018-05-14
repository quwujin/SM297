using Db;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
 

namespace Db
{
    public class AddReplayDal
    {
        public string conn = SqlHelper.ConnectionString;

        public int Add(Model.AddReplayModel model) {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into  Wx_AddReplay(");
            strSql.Append("Contents,Types,Pic,Mid,Sid)");
            strSql.Append(" values (");
            strSql.Append("@Contents,@Types,@Pic,@Mid,@Sid)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Contents", model.Contents),
					new SqlParameter("@Types", model.Types),
					new SqlParameter("@Pic", model.Pic),
					new SqlParameter("@Mid", model.Mid),
					new SqlParameter("@Sid", model.Sid)
					            
                 };

            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
        }



        public int Update(Model.AddReplayModel model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Wx_AddReplay set ");
            strSql.Append("Contents=@Contents,Types=@Types,Pic=@Pic,Mid=@Mid,Sid=@Sid where Id=@Id");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Contents", model.Contents),
					new SqlParameter("@Types", model.Types),
					new SqlParameter("@Pic", model.Pic),
					new SqlParameter("@Mid", model.Mid),
					new SqlParameter("@Sid", model.Sid),
					new SqlParameter("@Id", model.Id)
					            
                 };

            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
        }


        public Model.AddReplayModel GetModel(int Id)
        {

            string sql = "select * from Wx_AddReplay where id =" + Id;
            Model.AddReplayModel model = new Model.AddReplayModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            if (dr.Read())
            {
                model.Id = Id;
                model.Contents = dr["Contents"].ToString();
                model.Types = dr["Types"].ToString();
                model.Pic = dr["Pic"].ToString();
                model.Mid = dr["Mid"].ToString();
                model.Sid = dr["Sid"].ToString();
                
            }
            return model;
        }
    }
}
