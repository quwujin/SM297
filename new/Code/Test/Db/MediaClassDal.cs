using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using Db;

namespace Db
{
    public class MediaClassDal
    {
        public string conn = SqlHelper.ConnectionString;


 
   

        public int Del(int id)
        {

            string sql = " delete from Wx_MediaClass where id=" + id;
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql);
        }



        public int Update(Model.WxKeysClassModel model)
        {


            int j = 0;
            int obj = 0;


            StringBuilder sql = new StringBuilder();

            sql.Append(" Update Wx_MediaClass set Contents=@Contents,Types=@Types,Mid=@Mid,Sid=@Sid where Id=@Id");




            SqlParameter[] para = new SqlParameter[]
			{
					     
                new SqlParameter("@Types",model.Types),
                new SqlParameter("@Contents",model.Contents),
                          
                new SqlParameter("@Mid",model.Mid),
                new SqlParameter("@Sid",model.Sid),
                new SqlParameter("@Id",model.Id)
			};

            try
            {
                SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString(), para);
                obj = model.Id;

            }
            catch (Exception)
            {

                obj = 0;
            }


            return obj;
        }



    
        public DataTable GetList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from Wx_MediaClass where 1=1 ");
            sql.Append(sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }



        public Model.WxKeysClassModel GetModel(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from Wx_MediaClass where id=" + id);
            Model.WxKeysClassModel model = new Model.WxKeysClassModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            if (dr.Read())
            {
                model.Id = Convert.ToInt32(dr["Id"].ToString());
        
                model.Sid = dr["Sid"].ToString();
          
                model.Mid = dr["Mid"].ToString();
                model.Contents = dr["contents"].ToString();

                model.Types = dr["Types"].ToString();



            }
            return model;

        }











    }
}
