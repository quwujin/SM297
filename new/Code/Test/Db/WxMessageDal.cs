using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Db
{
    public class WxMessageDal
    {
        public string conn = SqlHelper.ConnectionString;

        public int Add(Model.WxMessageModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into  Wx_messageinfo(");
            strSql.Append("[MsgType],Contents,PicUrl,PShow,Title,Description,Url,HUrl,GroupCode,CreateTime,Author,OrderId)");
            strSql.Append(" values (");
            strSql.Append("@MsgType,@Contents,@PicUrl,@PShow,@Title,@Description,@Url,@HUrl,@GroupCode,@CreateTime,@Author,@OrderId)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@MsgType", model.MsgType),
					new SqlParameter("@Contents", model.Contents),
					new SqlParameter("@PicUrl", model.PicUrl),
					new SqlParameter("@PShow", model.PShow),
					new SqlParameter("@Title", model.Title),
					new SqlParameter("@Description", model.Description),
                    new SqlParameter("@Url", model.Url)   ,
                    new SqlParameter("@HUrl", model.HUrl)   ,
                    new SqlParameter("@GroupCode", model.GroupCode)   ,   
                    new SqlParameter("@CreateTime", model.CreateTime)  ,
                    new SqlParameter("@Author",model.Author),
                   new SqlParameter("@OrderId",model.OrderId)
                 };

            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Adds(Model.WxMessageModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into  Wx_messageinfo(");
            strSql.Append("[MsgType],Contents,PicUrl,PShow,Title,Description,Url,HUrl,GroupCode,CreateTime,Author,OrderId)");
            strSql.Append(" values (");
            strSql.Append("@MsgType,@Contents,@PicUrl,@PShow,@Title,@Description,@Url,@HUrl,@GroupCode,@CreateTime,@Author,@OrderId)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@MsgType", model.MsgType),
					new SqlParameter("@Contents", model.Contents),
					new SqlParameter("@PicUrl", model.PicUrl),
					new SqlParameter("@PShow", model.PShow),
					new SqlParameter("@Title", model.Title),
					new SqlParameter("@Description", model.Description)   ,
                    new SqlParameter("@Url", model.Url)   ,
                    new SqlParameter("@HUrl", model.HUrl)   ,
                    new SqlParameter("@GroupCode", model.GroupCode)   ,   
                    new SqlParameter("@CreateTime", model.CreateTime)  ,
                    new SqlParameter("@Author",model.Author),
                   new SqlParameter("@OrderId",model.OrderId)
  

                    };

            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionString))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                using (System.Data.SqlClient.SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
                        //return i;
                        string a = SqlHelper.ExecuteDataTable(trans, CommandType.Text, "select @@IDENTITY").Rows[0][0].ToString();
                        trans.Commit();
                        return Convert.ToInt32(a);
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        return 0;
                    }
                }
            }


        }


        public int UpdateStatus(string code)
        {
            string sql = "update Wx_messageinfo set statusid=1 where groupcode='" + code + "'";
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql);
        }



        public int Update(Model.WxMessageModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update   Wx_messageinfo set ");
            strSql.Append("MsgType=@MsgType,Author=@Author,Contents=@Contents,PicUrl=@PicUrl,PShow=@PShow,Title=@Title,Description=@Description,Url=@Url,HUrl=@HUrl,GroupCode=@GroupCode,CreateTime=@CreateTime ");
            strSql.Append(" where Id = @Id ");

            SqlParameter[] parameters = {
					new SqlParameter("@MsgType", model.MsgType),
					new SqlParameter("@Contents", model.Contents),
					new SqlParameter("@PicUrl", model.PicUrl),
					new SqlParameter("@PShow", model.PShow),
					new SqlParameter("@Title", model.Title),
					new SqlParameter("@Description", model.Description)   ,
                    new SqlParameter("@Url", model.Url)   ,
                    new SqlParameter("@HUrl", model.HUrl)   ,
                    new SqlParameter("@GroupCode", model.GroupCode)   ,   
                    new SqlParameter("@CreateTime", model.CreateTime)   ,
                        new SqlParameter("@Author",model.Author),
                    new SqlParameter("@Id",model.Id),
                 
              
                 };

            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }



        public DataTable GetList(string sqlwhere, string sql2, string order)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select *  from Wx_messageinfo where 1=1");
            sql.Append(sqlwhere);

            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }

        public int Del(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from Wx_messageinfo where Id = " + id);

            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString());
        }


        public Model.WxMessageModel GetModel(int Id)
        {

            string sql = "select * from Wx_messageinfo where Id =" + Id;
            Model.WxMessageModel model = new Model.WxMessageModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            if (dr.Read())
            {


                model.MsgType = dr["MsgType"].ToString();
                model.Contents = dr["Contents"].ToString();
                model.Title = dr["Title"].ToString();
                model.PicUrl = dr["PicUrl"].ToString();
                model.PShow = Convert.ToInt32(dr["PShow"].ToString());
                model.Description = dr["Description"].ToString();
                model.Url = dr["Url"].ToString();
                model.HUrl = dr["HUrl"].ToString();
                model.GroupCode = dr["GroupCode"].ToString();
                model.Author = dr["Author"].ToString();
                model.CreateTime = Convert.ToDateTime(dr["CreateTime"].ToString());
                model.OrderId = Convert.ToInt32(dr["OrderId"].ToString());
                model.Id = Id;

            }

            return model;
        }


        public Model.WxMessageModel GetModel(string code)
        {

            string sql = "select * from Wx_messageinfo where GroupCode ='" + code + "'";
            Model.WxMessageModel model = new Model.WxMessageModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            if (dr.Read())
            {


                model.MsgType = dr["MsgType"].ToString();
                model.Contents = dr["Contents"].ToString();
                model.Title = dr["Title"].ToString();
                model.PicUrl = dr["PicUrl"].ToString();
                model.PShow = Convert.ToInt32(dr["PShow"].ToString());
                model.Description = dr["Description"].ToString();
                model.Url = dr["Url"].ToString();
                model.HUrl = dr["HUrl"].ToString();
                model.GroupCode = dr["GroupCode"].ToString();
                model.Author = dr["Author"].ToString();
                model.CreateTime = Convert.ToDateTime(dr["CreateTime"].ToString());
                model.Id = Convert.ToInt32(dr["Id"].ToString());
                model.OrderId = Convert.ToInt32(dr["OrderId"].ToString());

            }

            return model;
        }






        /// <summary>
        /// 分页计算总数
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <param name="joinString"></param>
        /// <returns></returns>
        public int GetCount(string sqlstr)
        {
            Model.PageInfo pages = new Model.PageInfo();

            pages.SqlWhere = sqlstr;
            pages.ReturnFileds = "id";
            pages.TableName = " Wx_MessageInfo ";
            pages.JoinTable = "";
            pages.CountFields = " a.id ";
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


        public string IsMultiPic()
        {
            string sql = "select top 1 GroupCode from Wx_messageInfo where statusid=0 and msgType='多图文' ";
            DataTable dt = SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            else
            {
                return "0";
            }
        }


        public DataTable GetList(string sqlstr, int pageindex, int pagesize)
        {
            Model.PageInfo pages = new Model.PageInfo();
            pages.PageIndex = pageindex;
            pages.PageSize = pagesize;
            pages.SqlWhere = sqlstr;
            pages.ReturnFileds = "t.*";
            pages.TableName = " Wx_MessageInfo ";
            pages.JoinTable = "";
            pages.CountFields = " a.id ";
            pages.OrderString = " order by t.id desc";
            pages.SelectFileds = " a.* ";
            pages.doCount = 0;
            PageHelper p = new PageHelper();
            DataTable dt = p.GetList(pages);
            return dt;
        }


        public DataTable GetListMit(string sqlstr, int pageindex, int pagesize)
        {
            Model.PageInfo pages = new Model.PageInfo();
            pages.PageIndex = pageindex;
            pages.PageSize = pagesize;
            pages.SqlWhere = sqlstr;
            pages.ReturnFileds = "t.*";
            pages.TableName = " v_message ";
            pages.JoinTable = "";
            pages.CountFields = " a.GroupCode ";
            pages.OrderString = " order by t.GroupCode desc";
            pages.SelectFileds = " a.* ";
            pages.doCount = 0;
            PageHelper p = new PageHelper();
            DataTable dt = p.GetList(pages);
            return dt;
        }

        /// <summary>
        /// 分页计算总数
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <param name="joinString"></param>
        /// <returns></returns>
        public int GetCountMit(string sqlstr)
        {
            Model.PageInfo pages = new Model.PageInfo();

            pages.SqlWhere = sqlstr;
            pages.ReturnFileds = "GroupCode";
            pages.TableName = " v_message ";
            pages.JoinTable = "";
            pages.CountFields = " a.GroupCode ";
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



    }
}