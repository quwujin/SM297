using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Db
{
    public class ConDal
    {
       
            public static string conn = SqlHelper.ConnectionString;

            public static DataTable GetList(string sql)
            {
                return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql);
            }

            public static int Execute(string sql)
            {
                return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql);
            }


            #region 分页计算总数
            public static int GetCount(string TableName, string sqlstr, string joinString)
            {
                Model.PageInfo pages = new Model.PageInfo();
                pages.SqlWhere = sqlstr;
                pages.ReturnFileds = "Id";
                pages.SqlWhere = sqlstr;
                pages.TableName = TableName;
                pages.JoinTable = joinString;
                pages.CountFields = " a.Id ";
                pages.OrderString = " ";
                pages.SelectFileds = "a.*";
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
            #endregion

            #region 分页计算GetList
            public static DataTable GetList(string TableName, string sqlstr, int pageindex, int pagesize, string joinString)
            {
                Model.PageInfo pages = new Model.PageInfo();
                pages.PageIndex = pageindex;
                pages.PageSize = pagesize;
                pages.SqlWhere = sqlstr;
                pages.ReturnFileds = "t.*";
                pages.TableName = TableName;
                pages.JoinTable = joinString;
                pages.CountFields = " a.Id";
                pages.OrderString = " order by a.Id asc ";
                pages.SelectFileds = "a.*";
                pages.doCount = 0;
                PageHelper p = new PageHelper();
                DataTable dt = p.GetList(pages);
                return dt;
            }
            #endregion
 
    }
}
