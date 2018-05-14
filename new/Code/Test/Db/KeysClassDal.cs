using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using Db;

namespace Db
{
    public class KeysClassDal
    {
        public string conn = SqlHelper.ConnectionString;



        public int Add(Model.WxKeysClassModel model)
        {


            int j = 0;
            int obj = 0;

            StringBuilder sql = new StringBuilder();
            sql.Append(" INSERT Wx_KeysClass(");
            sql.Append(" ClassName,");
            sql.Append("StatusId,");
            sql.Append("Keys,Contents,Mp3,Pic,Types,Mid,Sid");


            sql.Append(" )values(");
            sql.Append("@ClassName,");
            sql.Append("@StatusId,");
            sql.Append("@Keys,@Contents,@Mp3,@Pic,@Types,@Mid,@Sid)");


            SqlParameter[] para = new SqlParameter[]
				            {
					            new SqlParameter("@ClassName", model.ClassName),
					            new SqlParameter("@StatusId", model.StatusId),
					            new SqlParameter("@Keys", model.Keys),
                                new SqlParameter("@Types",model.Types),
                                new SqlParameter("@Contents",model.Contents),
                                new SqlParameter("@Mp3",model.Mp3),
                                new SqlParameter("@Pic",model.Pic),
                                new SqlParameter("@Mid",model.Mid),
                                new SqlParameter("@Sid",model.Sid)
				            };
            try
            {
                obj = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString(), para);
                //          string a = SqlHelper.ExecuteDataTable(trans, CommandType.Text, "select @@IDENTITY").Rows[0][0].ToString();

                // string sqls = "insert into wx_keys(classid,types,contents,mp3,pic,mid,sid) values('" + obj + "','" + m.Types + "','" + m.Contents + "','" + m.Mp3 + "','" + m.Pic + "','" + m.Mid + "','" + m.Sid + "') ";



            }
            catch (Exception)
            {

                obj = 0;
            }





            return obj;
        }


        /*
        public int Add(Model.WxKeysClassModel model)
        {


            int j = 0;
            int obj = 0;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionString))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append(" INSERT Wx_KeysClass(");
                    sql.Append(" ClassName,");
                    sql.Append("StatusId,");
                    sql.Append("Keys");
                    
 
                    sql.Append(" )values(");
                    sql.Append("@ClassName,");
                    sql.Append("@StatusId,");
                    sql.Append("@Keys)");
               

                    SqlParameter[] para = new SqlParameter[]
				            {
					            new SqlParameter("@ClassName", model.ClassName),
					            new SqlParameter("@StatusId", model.StatusId),
					            new SqlParameter("@Keys", model.Keys)
				            };
                    try
                    {
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql.ToString(), para);
                        string a = SqlHelper.ExecuteDataTable(trans, CommandType.Text, "select @@IDENTITY").Rows[0][0].ToString();
                        obj = Convert.ToInt32(a);

                        if (obj > 0)
                        {
                            foreach (Model.WxKeysModel m in model.WxKeysList)
                            {
                                string sqls = "insert into wx_keys(classid,types,contents,mp3,pic,mid,sid) values('" + obj + "','" + m.Types + "','"+m.Contents+"','"+m.Mp3+"','"+m.Pic+"','"+m.Mid+"','"+m.Sid+"') ";
                                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sqls);
                            }

                        }


                        trans.Commit();
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        obj = 0;
                    }


                }
            }

            return obj;
        }
        */

        public int Del(int id)
        {

            string sql = " delete from Wx_KeysClass where id=" + id;
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql);
        }



        public int Update(Model.WxKeysClassModel model)
        {


            int j = 0;
            int obj = 0;


            StringBuilder sql = new StringBuilder();

            sql.Append(" Update Wx_KeysClass set ClassName=@ClassName,Mp3=@Mp3,Contents=@Contents,Pic=@Pic,Types=@Types,Mid=@Mid,Sid=@Sid,Keys=@Keys where Id=@Id");




            SqlParameter[] para = new SqlParameter[]
				            {
					            new SqlParameter("@ClassName", model.ClassName),
					            new SqlParameter("@StatusId", model.StatusId),
					            new SqlParameter("@Keys", model.Keys),
                                new SqlParameter("@Types",model.Types),
                                new SqlParameter("@Contents",model.Contents),
                                new SqlParameter("@Mp3",model.Mp3),
                                new SqlParameter("@Pic",model.Pic),
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



        /*
        public int Update(Model.WxKeysClassModel model)
        {


            int j = 0;
            int obj = 0;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionString))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append(" Update Wx_KeysClass set ClassName=@ClassName,StatusId=@StatusId,Keys=@Keys where Id=@Id");
                   

                    SqlParameter[] para = new SqlParameter[]
                            {
                                new SqlParameter("@ClassName", model.ClassName),
                                new SqlParameter("@StatusId", model.StatusId),
                                new SqlParameter("@Keys", model.Keys),
                                new SqlParameter("@Id",model.Id)
                            };
                    try
                    {
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql.ToString(), para);
                     

                        if (model.Id > 0)
                        {
                            string sqlss = " delete from  wx_keys where classid= " + model.Id;
                            int jj = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sqlss.ToString(), para);
                       
                            foreach (Model.WxKeysModel m in model.WxKeysList)
                            {
                                string sqls = "insert into wx_keys(classid,types,contents,mp3,pic,mid,sid) values('" + model.Id + "','" + m.Types + "','" + m.Contents + "','" + m.Mp3 + "','" + m.Pic + "','" + m.Mid + "','" + m.Sid + "') ";
                                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sqls);
                            }

                        }

                        obj = model.Id;
                        trans.Commit();
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        obj = 0;
                    }


                }
            }

            return obj;
        }

        */
        public DataTable GetList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from wx_keysClass where 1=1 ");
            sql.Append(sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }



        public Model.WxKeysClassModel GetModel(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from wx_keysClass where id=" + id);
            Model.WxKeysClassModel model = new Model.WxKeysClassModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            if (dr.Read())
            {
                model.Id = Convert.ToInt32(dr["Id"].ToString());
                model.ClassName = dr["ClassName"].ToString();
                model.Keys = dr["Keys"].ToString();
                model.Sid = dr["Sid"].ToString();
                model.Pic = dr["Pic"].ToString();
                model.Mp3 = dr["Mp3"].ToString();
                model.Mid = dr["Mid"].ToString();
                model.Contents = dr["contents"].ToString();

                model.Types = dr["Types"].ToString();



            }
            return model;

        }











    }
}
