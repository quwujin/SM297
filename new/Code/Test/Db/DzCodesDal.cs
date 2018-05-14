using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Db
{
    public class DzCodesDal
    {
        public string conn = SqlHelper.ConnectionString;

        /// <summary>
        /// 添加组织
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(Model.DzCodesModel model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into  Dz_Codes(");
            strSql.Append("Codes,Price,CTime,DTime,Mob,PCodes,States,Notes)");
            strSql.Append(" values (");
            strSql.Append("@Codes,@Price,@CTime,@DTime,@Mob,@PCodes,@States,@Notes)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Codes", model.Codes),
					new SqlParameter("@Price", model.Price),
					new SqlParameter("@CTime", model.CTime),
					new SqlParameter("@DTime", model.DTime),
					new SqlParameter("@Mob", model.Mob),
					new SqlParameter("@PCodes", model.PCodes),
					new SqlParameter("@States", model.States),
                    new SqlParameter("@Notes",model.Notes)
				           
                 };

            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }
        /// <summary>
        /// 设置使用
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Set(Model.DzCodesModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Dz_Codes set DTime=@DTime,Mob=@Mob,PCodes=@PCodes,States=1");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@DTime", model.DTime),
					new SqlParameter("@mob", model.Mob),
					new SqlParameter("@PCodes", model.PCodes),
					new SqlParameter("@id", model.Id)				           
                 };
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }

        public int Del(int cityid)
        {
            string sql = "delete from Dz_Codes where Id=" + cityid;
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql);
        }
        public DataTable GetList(int n, string sqlwhere)
        {
            string sql = "select top " + n + " *  from Dz_Codes where 1=1 " + sqlwhere;
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql);
        }

        public int CheckCount(string sqlwhere)
        {
            string sql = "select count(*)  from Dz_Codes where 1=1 " + sqlwhere;
            return Convert.ToInt32(SqlHelper.ExecuteScalar(conn, CommandType.Text, sql));
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
            pages.ReturnFileds = "id";
            pages.TableName = " Dz_Codes ";
            pages.JoinTable = " ";
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

        public DataTable GetList(string sqlstr, int pageindex, int pagesize)
        {
            Model.PageInfo pages = new Model.PageInfo();
            pages.PageIndex = pageindex;
            pages.PageSize = pagesize;
            pages.SqlWhere = sqlstr;
            pages.ReturnFileds = "t.*";
            pages.TableName = " Dz_Codes ";
            pages.JoinTable = " ";
            pages.CountFields = " a.id ";
            pages.OrderString = " order by t.id desc";
            pages.SelectFileds = " a.* ";
            pages.doCount = 0;
            PageHelper p = new PageHelper();
            DataTable dt = p.GetList(pages);
            return dt;
        }
         
        public DataTable GetPageList(string sqlstr, int pageindex, int pagesize)
        {
            Model.PageInfo pages = new Model.PageInfo();
            pages.PageIndex = pageindex;
            pages.PageSize = pagesize;
            pages.SqlWhere = sqlstr;
            pages.ReturnFileds = "t.*";
            pages.TableName = " Dz_Codes ";
            pages.JoinTable = " ";
            pages.CountFields = " a.Id ";
            pages.OrderString = " order by t.Id desc";
            pages.SelectFileds = " a.*";
            pages.doCount = 0;
            PageHelper p = new PageHelper();
            DataTable dt = p.GetList(pages);
            return dt;
        }

    }
}
