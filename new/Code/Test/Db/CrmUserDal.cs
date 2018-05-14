using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Db
{
    public class CrmUserDal
    {
        public string conn = SqlHelper.ConnectionString;

        public int Add(Model.CrmUserModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Crm_User(UName,UPwd,Codes,PId,CId,States,Types,Notes,CTime,LTime) values(");
            strSql.Append("@UName,@UPwd,@Codes,@PId,@CId,@States,@Types,@Notes,@CTime,@LTime);select @@IDENTITY");
            SqlParameter[] parameters = {	 
                        new SqlParameter("@UName", model.UName),
                        new SqlParameter("@UPwd", model.UPwd),
                        new SqlParameter("@Codes", model.Codes),
                        new SqlParameter("@PId", model.PId),
                        new SqlParameter("@CId", model.CId),
                        new SqlParameter("@States", model.States),
                        new SqlParameter("@Types", model.Types),
                        new SqlParameter("@Notes", model.Notes),
                        new SqlParameter("@CTime", model.CTime),
                        new SqlParameter("@LTime", model.LTime)
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


        public int Update(Model.CrmUserModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update Crm_User set UName=@UName,UPwd=@UPwd,Codes=@Codes,PId=@PId,CId=@CId,States=@States,Types=@Types,Notes=@Notes where Id=@Id ");
            SqlParameter[] parameters = {	 
                        new SqlParameter("@UName", model.UName),
                        new SqlParameter("@UPwd", model.UPwd),
                        new SqlParameter("@Codes", model.Codes),
                        new SqlParameter("@PId", model.PId),
                        new SqlParameter("@CId", model.CId),
                        new SqlParameter("@States", model.States),
                        new SqlParameter("@Types", model.Types),
                        new SqlParameter("@Notes", model.Notes),
                        new SqlParameter("@Id",model.Id)
                 };
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
        }

        public int UpdateLTime(Model.CrmUserModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update Crm_User set LTime=@LTime where Id=@Id ");
            SqlParameter[] parameters = {	 
                        new SqlParameter("@LTime", model.LTime),
                        new SqlParameter("@Id",model.Id)
                 };
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
        }

        public int UpdateCity(Model.CrmUserModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update Crm_User set PId=@PId, CId=@CId where UName=@UName ");
            SqlParameter[] parameters = {	 
                        new SqlParameter("@PId", model.PId),
                        new SqlParameter("@CId",model.CId),
                        new SqlParameter("@UName",model.UName)
                 };
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
        }

        public int UpdatePwd(Model.CrmUserModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update Crm_User set UPwd=@UPwd where Id=@Id ");
            SqlParameter[] parameters = {	 
                        new SqlParameter("@UPwd", model.UPwd),
                        new SqlParameter("@Id",model.Id)
                 };
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
        }

        public Model.CrmUserModel GetUName(string UName, string sqlstr)
        {
            StringBuilder sql = new StringBuilder("select * from Crm_User where UName='" + UName + "' " + sqlstr);
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            Model.CrmUserModel model = new Model.CrmUserModel();
            model.Id = 0;
            if (dr.Read())
            {
                model.Id = Convert.ToInt32(dr["Id"].ToString());
                model.UName=dr["UName"].ToString();
                model.UPwd=dr["UPwd"].ToString();
                model.Codes=dr["Codes"].ToString();
                model.PId= Convert.ToInt32(dr["PId"].ToString());
                model.CId= Convert.ToInt32(dr["CId"].ToString());
                model.States= Convert.ToInt32(dr["States"].ToString());
                model.Types= Convert.ToInt32(dr["Types"].ToString());
                model.Notes=dr["Notes"].ToString();
                model.CTime = DateTime.Parse(dr["CTime"].ToString());
                model.LTime = DateTime.Parse(dr["LTime"].ToString());
            }
            return model;
        }

        public int CheckCount(string sqlwhere)
        {
            string sql = "select count(*)  from Crm_User where 1=1 " + sqlwhere;
            return Convert.ToInt32(SqlHelper.ExecuteScalar(conn, CommandType.Text, sql));
        }
        public DataTable GetList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder("select * from Crm_User where 1=1 " + sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }

        public int Del(int id)
        {
            string sql = "delete from Crm_User where Id=" + id;
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql);
        }

        public Model.CrmUserModel GetModel(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from Crm_User  where Id=" + id);
            Model.CrmUserModel model = new Model.CrmUserModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            if (dr.Read())
            {
                model.Id = Convert.ToInt32(dr["Id"].ToString());
                model.UName = dr["UName"].ToString();
                model.UPwd = dr["UPwd"].ToString();
                model.Codes = dr["Codes"].ToString();
                model.PId = Convert.ToInt32(dr["PId"].ToString());
                model.CId = Convert.ToInt32(dr["CId"].ToString());
                model.States = Convert.ToInt32(dr["States"].ToString());
                model.Types = Convert.ToInt32(dr["Types"].ToString());
                model.Notes = dr["Notes"].ToString();
                model.CTime = DateTime.Parse(dr["CTime"].ToString());
                model.LTime = DateTime.Parse(dr["LTime"].ToString());
            }
            return model;
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
            pages.TableName = " Crm_User ";
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
            pages.TableName = " Crm_User ";
            pages.JoinTable = "  left join S_Province sp on sp.ProvinceId=a.PId left join S_City sc on sc.CityId=a.CId ";
            pages.CountFields = " a.Id ";
            pages.OrderString = " order by t.Id desc";
            pages.SelectFileds = " a.*,sp.ProvinceName,sc.CityName ";
            pages.doCount = 0;
            PageHelper p = new PageHelper();
            DataTable dt = p.GetList(pages);
            return dt;
        }
        public DataTable GetExcelList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder("select a.*,sp.ProvinceName,sc.CityName from Crm_User a  left join S_Province sp on sp.ProvinceId=a.PId left join S_City sc on sc.CityId=a.CId where 1=1 " + sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }

    }
}
