using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Db
{
    public class WxMsgDal
    {
        public string conn = SqlHelper.ConnectionString;

        public int AddImg(Model.WxMsgModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Wx_Msg(ToUserName,FromUserName,CreateTime,MsgType,PicUrl,MediaId,MsgId,States) values(");
            strSql.Append("@ToUserName,@FromUserName,@CreateTime,@MsgType,@PicUrl,@MediaId,@MsgId,@States);select @@IDENTITY");
            SqlParameter[] parameters = {	 
                        new SqlParameter("@ToUserName",model.ToUserName),
                        new SqlParameter("@FromUserName", model.FromUserName),
                        new SqlParameter("@CreateTime", model.CreateTime),
                        new SqlParameter("@MsgType", model.MsgType),
                        new SqlParameter("@PicUrl", model.PicUrl),
                        new SqlParameter("@MediaId", model.MediaId),
                        new SqlParameter("@MsgId", model.MsgId),
                        new SqlParameter("@States", model.States)
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
        public int Add(Model.WxMsgModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Wx_Msg(ToUserName,FromUserName,CreateTime,MsgType,PicUrl,MediaId,MsgId,Contents,Format,ThumbMediaId," +
                "Location_X,Location_Y,Scale,Label,Title,States) values(");
            strSql.Append("@ToUserName,@FromUserName,@CreateTime,@MsgType,@PicUrl,@MediaId,@MsgId,@Contents,@Format,@ThumbMediaId,@" +
                "Location_X,@Location_Y,@Scale,@Label,@Title,@States);select @@IDENTITY");
            SqlParameter[] parameters = {	 
                        new SqlParameter("@ToUserName",model.ToUserName),
                        new SqlParameter("@FromUserName", model.FromUserName),
                        new SqlParameter("@CreateTime", model.CreateTime),
                        new SqlParameter("@MsgType", model.MsgType),
                        new SqlParameter("@PicUrl", model.PicUrl),
                        new SqlParameter("@MediaId", model.MediaId),
                        new SqlParameter("@MsgId", model.MsgId),
                        new SqlParameter("@Contents", model.Contents),
                        new SqlParameter("@Format", model.Format),
                        new SqlParameter("@ThumbMediaId", model.ThumbMediaId),
                        new SqlParameter("@Location_X", model.Location_X),
                        new SqlParameter("@Location_Y", model.Location_Y),
                        new SqlParameter("@Scale", model.Scale),
                        new SqlParameter("@Label", model.Label),
                        new SqlParameter("@Title", model.Title),
                        new SqlParameter("@States", model.States)
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


        public int Update(Model.WxMsgModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update Wx_Msg set States=@States,DTime=@DTime where Id=@Id");
            SqlParameter[] parameters = {	 
                        new SqlParameter("@States", model.States),
                        new SqlParameter("@DTime", model.DTime),
                        new SqlParameter("@Id", model.Id)
                 };
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
        }
        public Model.WxMsgModel GetOpenIdInfo(string FromUserName, string sqlstr)
        {
            StringBuilder sql = new StringBuilder("select Top 1 * from Wx_Msg where FromUserName='" + FromUserName + "' " + sqlstr);
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            Model.WxMsgModel model = new Model.WxMsgModel();
            model.Id = 0;
            if (dr.Read())
            {
                model.Id = Convert.ToInt32(dr["Id"].ToString());
                model.ToUserName=dr["ToUserName"].ToString();
                model.FromUserName=dr["FromUserName"].ToString();
                model.CreateTime=DateTime.Parse(dr["CreateTime"].ToString());
                if(dr["DTime"]!=DBNull.Value){
                model.DTime=DateTime.Parse(dr["DTime"].ToString());
                }
                model.MsgType=dr["MsgType"].ToString();
                model.PicUrl=dr["PicUrl"].ToString();
                model.MediaId=dr["MediaId"].ToString();
                model.MsgId=dr["MsgId"].ToString();
                model.Contents=dr["Contents"].ToString();
                model.Format=dr["Format"].ToString();
                model.ThumbMediaId=dr["ThumbMediaId"].ToString();
                model.Location_X=dr["Location_X"].ToString();
                model.Location_Y=dr["Location_Y"].ToString();
                model.Scale=dr["Scale"].ToString();
                model.Label=dr["Label"].ToString();
                model.Title=dr["Title"].ToString();
                model.States =Convert.ToInt32(dr["States"].ToString());
            }
            return model;
        }

        public int CheckCount(string sqlwhere)
        {
            string sql = "select count(*)  from Wx_Msg where 1=1 " + sqlwhere;
            return Convert.ToInt32(SqlHelper.ExecuteScalar(conn, CommandType.Text, sql));
        }
        public DataTable GetList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder("select * from Wx_Msg where 1=1 " + sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }

        public int Del(int id)
        {
            string sql = "delete from Wx_Msg where Id=" + id;
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql);
        }

        public Model.WxMsgModel GetModel(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from Wx_Msg  where Id=" + id);
            Model.WxMsgModel model = new Model.WxMsgModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            if (dr.Read())
            {
                model.Id = Convert.ToInt32(dr["Id"].ToString());
                model.ToUserName = dr["ToUserName"].ToString();
                model.FromUserName = dr["FromUserName"].ToString();
                model.CreateTime = DateTime.Parse(dr["CreateTime"].ToString());
                if (dr["DTime"] != DBNull.Value)
                {
                    model.DTime = DateTime.Parse(dr["DTime"].ToString());
                }
                model.MsgType = dr["MsgType"].ToString();
                model.PicUrl = dr["PicUrl"].ToString();
                model.MediaId = dr["MediaId"].ToString();
                model.MsgId = dr["MsgId"].ToString();
                model.Contents = dr["Contents"].ToString();
                model.Format = dr["Format"].ToString();
                model.ThumbMediaId = dr["ThumbMediaId"].ToString();
                model.Location_X = dr["Location_X"].ToString();
                model.Location_Y = dr["Location_Y"].ToString();
                model.Scale = dr["Scale"].ToString();
                model.Label = dr["Label"].ToString();
                model.Title = dr["Title"].ToString();
                model.States = Convert.ToInt32(dr["States"].ToString());
            }
            return model;
        }

        public int UpdateStatus(Model.WxMsgModel model, Model.OrderLogModel mdlog)
        {
            int rtn = 0;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append(" update Wx_Msg set States=@States where Id=@Id");
                        SqlParameter[] parameters = {	 
                                new SqlParameter("@States", model.States),
                               new SqlParameter("@Id", model.Id) 
                   
                             };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);

                        StringBuilder strSql2 = new StringBuilder();
                        strSql2.Append(" insert into OrderLog(Oid,OrderCode,Mob,UpTime,LStatus,Status,Notes)");
                        strSql2.Append(" values(@Oid,@OrderCode,@Mob,@UpTime,@LStatus,@Status,@Notes)");
                        SqlParameter[] parameters2 = {	 
                                new SqlParameter("@Oid", mdlog.OId),
                                new SqlParameter("@OrderCode", mdlog.OrderCode),
                                new SqlParameter("@Mob", mdlog.Mob),
                                new SqlParameter("@UpTime", mdlog.UpTime),
                                new SqlParameter("@LStatus", mdlog.LStatus),
                                new SqlParameter("@Status", mdlog.Status),
                               new SqlParameter("@Notes", mdlog.Notes) 
                             };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql2.ToString(), parameters2);
                        trans.Commit();
                        return rtn;
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        rtn = 0;
                    }
                }
            }
            return 0;

        }

        /// <summary>
        /// 分页计算总数
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <param name="joinString"></param>
        /// <returns></returns>
        public int GetCount(string sqlstr, string joinString)
        {
            Model.PageInfo pages = new Model.PageInfo();

            pages.SqlWhere = sqlstr;
            pages.ReturnFileds = "Id";
            pages.TableName = " Wx_Msg ";
            pages.JoinTable = "   ";
            pages.CountFields = " a.Id ";
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


        public DataTable GetPageList(string sqlstr, int pageindex, int pagesize)
        {
            Model.PageInfo pages = new Model.PageInfo();
            pages.PageIndex = pageindex;
            pages.PageSize = pagesize;
            pages.SqlWhere = sqlstr;
            pages.ReturnFileds = "t.*";
            pages.TableName = " Wx_Msg ";
            pages.JoinTable = " ";
            pages.CountFields = " a.Id ";
            pages.OrderString = " order by t.Id desc";
            pages.SelectFileds = " a.*";
            pages.doCount = 0;
            PageHelper p = new PageHelper();
            DataTable dt = p.GetList(pages);
            return dt;
        }

        public DataTable GetExcelList(string sqlstr)
        {
            StringBuilder sql = new StringBuilder("select a.* from Wx_Msg a where 1=1 " + sqlstr);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }

    }
}
