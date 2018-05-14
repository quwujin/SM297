using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Db
{
    public class MovieDal
    {
        public string conn = SqlHelper.ConnectionString;

        public int Add(Model.MovieModel model)
        {

            StringBuilder strSql = new StringBuilder();

            strSql.Append(" insert into MovieInfo(MovieName,PId,CId,DId,PyId,PName,CityName,IsHave,Notes,Address,Tel,AreaName,AgentName,MovieLine,SupplierId,QZ"+
",Price1,Price2,Price3,Price4,Price5,Ext1,Ext2,Ext3) values(");
            strSql.Append("@MovieName,@PId,@CId,@DId,@PyId,@PName,@CityName,@IsHave,@Notes,@Address,@Tel,@AreaName,@AgentName,@MovieLine,@SupplierId,@QZ" +
",@Price1,@Price2,@Price3,@Price4,@Price5,@Ext1,@Ext2,@Ext3)");
            SqlParameter[] parameters = {	 
                        new SqlParameter("@MovieName", model.MovieName),
                        new SqlParameter("@PId", model.PId),
                        new SqlParameter("@CId", model.CId),
                        new SqlParameter("@DId", model.DId),
                        new SqlParameter("@PyId", model.PyId),
                        new SqlParameter("@PName", model.PName),
                        new SqlParameter("@CityName", model.CityName),
                        new SqlParameter("@IsHave", model.IsHave),
                        new SqlParameter("@Notes", model.Notes),
                        new SqlParameter("@Address", model.Address),
                        new SqlParameter("@Tel", model.Tel),
                        new SqlParameter("@AreaName", model.AreaName),
                        new SqlParameter("@AgentName", model.AgentName),
                        new SqlParameter("@MovieLine", model.MovieLine),
                        new SqlParameter("@SupplierId", model.SupplierId),
                        new SqlParameter("@QZ", model.QZ),
                        new SqlParameter("@Price1", model.Price1),
                        new SqlParameter("@Price2", model.Price2),
                        new SqlParameter("@Price3", model.Price3),
                        new SqlParameter("@Price4", model.Price4),
                        new SqlParameter("@Price5", model.Price5),
                        new SqlParameter("@Ext1", model.Ext1),
                        new SqlParameter("@Ext2", model.Ext2),
                        new SqlParameter("@Ext3", model.Ext3)
                 };
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }


        public int Update(Model.MovieModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update  MovieInfo set MovieName=@MovieName,PId=@PId,CId=@CId,DId=@DId,SupplierId=@SupplierId,PyId=@PyId,PName=@PName,CityName=@CityName,IsHave=@IsHave,Notes=@Notes,Address=@Address,AgentName=@AgentName,AreaName=@AreaName,Tel=@Tel,MovieLine=@MovieLine where MovieId=@MovieId ");

            SqlParameter[] parameters = {	 
                    new SqlParameter("@MovieName", model.MovieName),
                    new SqlParameter("@PId", model.PId),
                    new SqlParameter("@CId", model.CId),
                    new SqlParameter("@DId", model.DId),
                    new SqlParameter("@SupplierId", model.SupplierId),
                    new SqlParameter("@PyId", model.PyId),
                    new SqlParameter("@PName", model.PName),
                    new SqlParameter("@CityName", model.CityName),
                    new SqlParameter("@IsHave", model.IsHave),
                    new SqlParameter("@Notes", model.Notes),
                    new SqlParameter("@MoveId",model.MovieId),
                      new SqlParameter("@Address", model.Address),
                      new SqlParameter("@AgentName", model.AgentName),
                       new SqlParameter("@AreaName", model.AreaName),
                        new SqlParameter("@Tel", model.Tel),
                        new SqlParameter("@MovieLine",model.MovieLine),
                        new SqlParameter("@MovieId",model.MovieId)
                     
                 };
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
        }

        public DataTable GetSupplierList(Model.MovieModel movie)
        {
            StringBuilder sql = new StringBuilder("select * from MovieInfo where PId=" + movie.PId + " and CId=" + movie.CId + " and SupplierId=" + movie.SupplierId);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }

        public Model.MovieModel GetSupplier(Model.MovieModel movie)
        {
            StringBuilder sql = new StringBuilder("select top 1 * from MovieInfo where PId=" + movie.PId + " and CId=" + movie.CId + " and SupplierId=" + movie.SupplierId);
            Model.MovieModel model = new Model.MovieModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            if (dr.Read())
            {
                model.MovieId = Convert.ToInt32(dr["MovieId"].ToString());
                model.MovieName = dr["MovieName"].ToString();
                model.Notes = dr["Notes"].ToString();
                model.PId = Convert.ToInt32(dr["PId"].ToString());
                model.CId = Convert.ToInt32(dr["CId"].ToString());
                model.DId = Convert.ToInt32(dr["DId"].ToString());
                model.PyId = Convert.ToInt32(dr["PyId"].ToString());
                model.PName = dr["PName"].ToString();
                model.CityName = dr["CityName"].ToString();
                model.IsHave = dr["IsHave"].ToString();
                model.Tel = dr["Tel"].ToString();
                model.MovieLine = dr["MovieLine"].ToString();
                model.Address = dr["Address"].ToString();
                model.AgentName = dr["AgentName"].ToString();
                model.AreaName = dr["AreaName"].ToString();
                model.Tel = dr["Tel"].ToString();
                model.SupplierId = Convert.ToInt32(dr["SupplierId"].ToString());
                model.QZ = Convert.ToInt32(dr["QZ"].ToString());
                model.Ext1 = Convert.ToInt32(dr["Ext1"].ToString());
                model.Ext2 = dr["Ext2"].ToString();
                model.Ext3 = dr["Ext3"].ToString();
                model.SupplierId = Convert.ToInt32(dr["SupplierId"].ToString());
                if (dr["Price1"] != DBNull.Value) { model.Price1 = Decimal.Parse(dr["Price1"].ToString()); } else { model.Price1 = 0; }
                if (dr["Price2"] != DBNull.Value) { model.Price2 = Decimal.Parse(dr["Price2"].ToString()); } else { model.Price2 = 0; }
                if (dr["Price3"] != DBNull.Value) { model.Price3 = Decimal.Parse(dr["Price3"].ToString()); } else { model.Price3 = 0; }
                if (dr["Price4"] != DBNull.Value) { model.Price4 = Decimal.Parse(dr["Price4"].ToString()); } else { model.Price4 = 0; }
                if (dr["Price5"] != DBNull.Value) { model.Price5 = Decimal.Parse(dr["Price5"].ToString()); } else { model.Price5 = 0; }
            }
            return model;
        }

        public DataTable GetList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder("select * from MovieInfo where 1=1 " + sqlwhere + " order by MovieName asc");
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }

        public DataTable GetCityList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder("  select a.movieId, isnull(s.DistrictName,' ')+'['+a.movieName+']' as q from MovieInfo a left join S_District s on s.DistrictID=a.DId  where 1=1 " + sqlwhere + " order by s.DistrictName asc ");
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }

        public int Del(int cityid)
        {
            string sql = "delete from MovieInfo where MovieId=" + cityid;
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql);
        }

        public Model.MovieModel GetModel(int MovieId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from MovieInfo  where MovieId=" + MovieId);
            Model.MovieModel model = new Model.MovieModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            if (dr.Read())
            {
                model.MovieId = Convert.ToInt32(dr["MovieId"].ToString());
                model.MovieName = dr["MovieName"].ToString();
                model.Notes = dr["Notes"].ToString();
                model.PId = Convert.ToInt32(dr["PId"].ToString());
                model.CId = Convert.ToInt32(dr["CId"].ToString());
                model.DId = Convert.ToInt32(dr["DId"].ToString());
                model.PyId = Convert.ToInt32(dr["PyId"].ToString());
                model.PName = dr["PName"].ToString();
                model.CityName = dr["CityName"].ToString();
                model.IsHave = dr["IsHave"].ToString();
                model.Tel = dr["Tel"].ToString();
                model.MovieLine = dr["MovieLine"].ToString();
                model.Address = dr["Address"].ToString();
                model.AgentName = dr["AgentName"].ToString();
                model.AreaName = dr["AreaName"].ToString();
                model.Tel = dr["Tel"].ToString();
                model.SupplierId = Convert.ToInt32(dr["SupplierId"].ToString());
                model.QZ = Convert.ToInt32(dr["QZ"].ToString());
                model.Ext1 = Convert.ToInt32(dr["Ext1"].ToString());
                model.Ext2 = dr["Ext2"].ToString();
                model.Ext3 = dr["Ext3"].ToString();
                model.SupplierId = Convert.ToInt32(dr["SupplierId"].ToString());
                if (dr["Price1"] != DBNull.Value) { model.Price1 = Decimal.Parse(dr["Price1"].ToString()); } else { model.Price1 = 0; }
                if (dr["Price2"] != DBNull.Value) { model.Price2 = Decimal.Parse(dr["Price2"].ToString()); } else { model.Price2 = 0; }
                if (dr["Price3"] != DBNull.Value) { model.Price3 = Decimal.Parse(dr["Price3"].ToString()); } else { model.Price3 = 0; }
                if (dr["Price4"] != DBNull.Value) { model.Price4 = Decimal.Parse(dr["Price4"].ToString()); } else { model.Price4 = 0; }
                if (dr["Price5"] != DBNull.Value) { model.Price5 = Decimal.Parse(dr["Price5"].ToString()); } else { model.Price5 = 0; }
            }
            return model;
        }

        public Model.MovieModel GetCityModel(int movieId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from MovieInfo  where movieId=" + movieId);
            Model.MovieModel model = new Model.MovieModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            if (dr.Read())
            {
                model.MovieId = Convert.ToInt32(dr["MovieId"].ToString());
                model.MovieName = dr["MovieName"].ToString();
                model.Notes = dr["Notes"].ToString();
                model.PId = Convert.ToInt32(dr["PId"].ToString());
                model.CId = Convert.ToInt32(dr["CId"].ToString());
                model.DId = Convert.ToInt32(dr["DId"].ToString());
                model.PyId = Convert.ToInt32(dr["PyId"].ToString());
                model.PName = dr["PName"].ToString();
                model.CityName = dr["CityName"].ToString();
                model.IsHave = dr["IsHave"].ToString();
                model.Tel = dr["Tel"].ToString();
                model.MovieLine = dr["MovieLine"].ToString();
                model.Address = dr["Address"].ToString();
                model.AgentName = dr["AgentName"].ToString();
                model.AreaName = dr["AreaName"].ToString();
                model.Tel = dr["Tel"].ToString();
                model.SupplierId = Convert.ToInt32(dr["SupplierId"].ToString());
                model.QZ = Convert.ToInt32(dr["QZ"].ToString());
                model.Ext1 = Convert.ToInt32(dr["Ext1"].ToString());
                model.Ext2 = dr["Ext2"].ToString();
                model.Ext3 = dr["Ext3"].ToString();
                model.SupplierId = Convert.ToInt32(dr["SupplierId"].ToString());
                if (dr["Price1"] != DBNull.Value) { model.Price1 = Decimal.Parse(dr["Price1"].ToString()); } else { model.Price1 = 0; }
                if (dr["Price2"] != DBNull.Value) { model.Price2 = Decimal.Parse(dr["Price2"].ToString()); } else { model.Price2 = 0; }
                if (dr["Price3"] != DBNull.Value) { model.Price3 = Decimal.Parse(dr["Price3"].ToString()); } else { model.Price3 = 0; }
                if (dr["Price4"] != DBNull.Value) { model.Price4 = Decimal.Parse(dr["Price4"].ToString()); } else { model.Price4 = 0; }
                if (dr["Price5"] != DBNull.Value) { model.Price5 = Decimal.Parse(dr["Price5"].ToString()); } else { model.Price5 = 0; }
            }
            return model;
        }


        public Model.MovieModel GetCityModel(string cityName)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select a.cityName,a.SupplierId,b.Types  from s_City  a left join SupplierInfo b on a.SupplierId=b.SupplierId  where a.cityName='" + cityName + "'");
            Model.MovieModel model = new Model.MovieModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            if (dr.Read())
            {

                model.CityName = dr["CityName"].ToString();

                model.SupplierId = dr["SupplierId"] != DBNull.Value ? Convert.ToInt32(dr["SupplierId"].ToString()) : 0;
                model.Types = dr["Types"].ToString();
            }
            return model;
        }


        /// <summary>
        /// 分页计算总数
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <param name="joinString"></param>
        /// <returns></returns>
        public int GetCount(string sqlstr, string joinstr)
        {
            Model.PageInfo pages = new Model.PageInfo();

            pages.SqlWhere = sqlstr;
            pages.ReturnFileds = "movieId";
            pages.TableName = " MovieInfo ";
            pages.JoinTable = "   ";
            pages.CountFields = " a.movieId ";
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

        public DataTable GetList(string sqlstr, int pageindex, int pagesize)
        {
            Model.PageInfo pages = new Model.PageInfo();
            pages.PageIndex = pageindex;
            pages.PageSize = pagesize;
            pages.SqlWhere = sqlstr;
            pages.ReturnFileds = "t.*";
            pages.TableName = " MovieInfo ";
            pages.JoinTable = "  left join SupplierInfo si on si.SupplierId =a.SupplierId  left join S_Province sp on sp.provinceid=a.PId " +
                "left join S_City sc on sc.CityId=a.CId left join S_District sd on sd.DistrictId=a.DId";
            pages.CountFields = " a.movieId ";
            pages.OrderString = " order by t.movieId desc";
            pages.SelectFileds = " a.* ,si.SupplierName,sp.ProvinceName as P_name,sc.CityName as C_Name,sd.DistrictName as D_name ";
            pages.doCount = 0;
            PageHelper p = new PageHelper();
            DataTable dt = p.GetList(pages);
            return dt;
        }

        public string GetCityModel4(int str, int max)
        {
            string temp = "";
            int er = str + max;

            StringBuilder sql = new StringBuilder();
            sql.Append("select a.*  from Codes_test a");
            Model.MovieModel model = new Model.MovieModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            while (dr.Read())
            {


                string Mcode = dr["Mcode"].ToString();
                string Mpwd = dr["Mpwd"].ToString();
                temp += "INSERT [dbo].[Codes] ([SupplierId], [StartDate], [EndDate], [MCode], [MPassword], [IsUsed], [CreateTime], [UsedTime], [Mob], [Price], [Num], [Options], [Types], [Limits], [Notes]) VALUES" +
" (1, CAST(0x0000A33200000000 AS DateTime), CAST(0x0000A41200000000 AS DateTime), N'" + Mcode + "', N'" + Mpwd + "', 0, CAST(0x0000A3320185E584 AS DateTime), NULL, N'', N'40', 2, N'全国通兑', N'2D', N'限价', N'限价小于等于47');<br/>";

            }
            return temp.ToString();
        }
        public string GetCityModel3(int str, int max)
        {
            int temp = 0;
            int er = str + max;
            StringBuilder sql = new StringBuilder();
            sql.Append("select a.*  from MovieTest  a where a.MovieID>" + str + " and a.MovieID<=" + er);
            Model.MovieModel model = new Model.MovieModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            while (dr.Read())
            {

                int cid = Convert.ToInt32(dr["CityName"].ToString());

                ProvinceCityDal pcdal = new ProvinceCityDal();
                pcdal.UpdateC(cid);

                temp += 1;
            }
            return temp.ToString();
        }

        public string GetCityModel2(int str, int max)
        {
            int temp = 0;
            int er = str + max;
            StringBuilder sql = new StringBuilder();
            sql.Append("select a.*  from MovieInfo  a where a.MovieID>" + str + " and a.MovieID<=" + er);
            Model.MovieModel model = new Model.MovieModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            while (dr.Read())
            {

                int cid = Convert.ToInt32(dr["CityName"].ToString());

                ProvinceCityDal pcdal = new ProvinceCityDal();
                pcdal.UpdateC(cid);

                temp += 1;
            }
            return temp.ToString();
        }


        public string GetMovieName(int cid)
        {
            string temp = "";
            StringBuilder sql = new StringBuilder();
            sql.Append("select a.*  from MovieInfo  a where a.CityName='" + cid + "'");
            Model.MovieModel model = new Model.MovieModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            if (dr.Read())
            {
                temp = dr["MovieName"].ToString();
            }
            return temp.ToString();
        }

        public int AddTest(Model.MovieModel model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" insert into MovieTest(PName,CityName,AreaName) values(");
            strSql.Append("@PName,@CityName,@AreaName)");
            SqlParameter[] parameters = {	 
                    new SqlParameter("@PName", model.PName),
                    new SqlParameter("@CityName", model.CityName),
                       new SqlParameter("@AreaName", model.AreaName)
                 };
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }


    }
}
