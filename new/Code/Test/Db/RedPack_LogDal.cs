using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data; 

namespace Db
{
    public class RedPack_LogDal
    {
        public string conn = SqlHelper.ConnectionString;


        public int Add(Model.RedPack_LogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into  [RedPack_Log]");
            strSql.Append("(Acid,Hid,Openid,Orderid,Money,Ctime,Note)");
            strSql.Append(" values (@Acid,@Hid,@Openid,@Orderid,@Money,@Ctime,@Note)");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
					new SqlParameter("@Acid", model.Acid)
,					new SqlParameter("@Hid", model.Hid)
,					new SqlParameter("@Openid", model.Openid)
,					new SqlParameter("@Orderid", model.Orderid)
,					new SqlParameter("@Money", model.Money)
,					new SqlParameter("@Ctime", model.Ctime)
,					new SqlParameter("@Note", model.Note)
                 };


            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }
        public int CheckCount(string sqlwhere)
        {
            string sql = "select count(*)  from RedPack_Log where 1=1 " + sqlwhere;
            return Convert.ToInt32(SqlHelper.ExecuteScalar(conn, CommandType.Text, sql));
        }


        public DataTable GetList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder("select * from RedPack_Log where 1=1 " + sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
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
            pages.TableName = " RedPack_Log ";
            pages.JoinTable = "  ";
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
       
        public DataTable GetList(string sqlstr, int pageindex, int pagesize)
        {
            Model.PageInfo pages = new Model.PageInfo();
            pages.PageIndex = pageindex;
            pages.PageSize = pagesize;
            pages.SqlWhere = sqlstr;
            pages.ReturnFileds = "t.*";
            pages.TableName = " RedPack_Log ";
            pages.JoinTable = "";
            pages.CountFields = " a.Id ";
            pages.OrderString = " order by t.Id DESC ";
            pages.SelectFileds = " a.*";
            pages.doCount = 0;
            PageHelper p = new PageHelper();
            DataTable dt = p.GetList(pages);
            return dt;
        }


        public DataTable GetExcelList(string sqlstr)
        {
            string sql = "select a.Id as '编号',a.MOB as '手机号',dm.MobileArea as '城市递归',a.Name as '姓名',a.Texts as '地址',a.jx as '奖项',a.CreateTime as '订单时间',a.States as '状态',a.Note as '备注' from RedPack_Log a  where 1=1 " + sqlstr;
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
     
    }
}
