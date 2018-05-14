using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Db
{
    public class UserInfoGroupDal
    {
        public string conn = SqlHelper.ConnectionString;

        /// <summary>
        /// 添加组织
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(Model.UserInfoGroupModel model) {

            int obj = 0;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionString))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("insert into UserInfo_Group(GroupName) values('" + model.GroupName + "')");
                        strSql.Append(";select @@IDENTITY");
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString());
                        string a = SqlHelper.ExecuteDataTable(trans, CommandType.Text, "select @@IDENTITY").Rows[0][0].ToString();
                        obj = Convert.ToInt32(a);
                        if (obj>0)
                        {
                            foreach (Model.PermissionModel m in model.PerList)
                            {
                                string sql = "insert into Permission(MenuId,GroupId,MenuBid) values(" + m.MenuId + "," + obj + "," + m.MenuBid + ") ";
                                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);
                            }
                            
                        }
                        trans.Commit();
                        return 1;
                    }
                    catch (Exception)
                    {

                        trans.Rollback();
                        return 0;
                    }
                }
            }
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Model.UserInfoGroupModel model)
        {

            int j = 0;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionString))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("update   UserInfo_Group set GroupName='" + model.GroupName + "' where groupid=" + model.GroupId);
                       
                         j=SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString());
                        
                       
                        if (j > 0)
                        {
                            foreach (Model.PermissionModel m in model.PerList)
                            {
                                string sql = "insert into Permission(MenuId,GroupId,MenuBid) values(" + m.MenuId + "," + model.GroupId + "," + m.MenuBid + ") ";
                                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);
                            }

                        }
                        trans.Commit();
                        return 1;
                    }
                    catch (Exception)
                    {

                        trans.Rollback();
                        return 0;
                    }
                }
            }
        }


        public Model.UserInfoGroupModel GetModel(int groupId)
        {
            string sql = "select groupid,groupName from UserInfo_Group where GroupId =" + groupId;
            Model.UserInfoGroupModel model = new Model.UserInfoGroupModel();
            DataTable dt = SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql);
            if (dt.Rows.Count>0)
            {
                if (dt.Rows[0]["GroupId"] != null && dt.Rows[0]["GroupId"].ToString() != "")
                {
                    model.GroupId = int.Parse(dt.Rows[0]["GroupId"].ToString());
                }
                if (dt.Rows[0]["GroupName"] != null && dt.Rows[0]["GroupName"].ToString() != "")
                {
                    model.GroupName = dt.Rows[0]["GroupName"].ToString() ;
                }
                return model;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 返回列表
        /// </summary>
        /// <param name="sqlwhere"></param>
        /// <returns></returns>
        public DataTable GetList(string sqlwhere)
        {
            string sql = " select groupId,groupName from UserInfo_Group where   1=1    " + sqlwhere;
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql);
        }

        /// <summary>
        /// 删除权限菜单
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public int DelPer(int pid)
        { 
            string sql ="delete from Permission where perid="+pid;
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql);
        }


        public int Del(int gid)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from Permission where groupid=" + gid + "; delete from UserInfo_Group where groupid=" + gid);
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString());
           
        }

        /// <summary>
        /// 不拥有的权限
        /// </summary>
        /// <returns></returns>
        public DataTable GetNoSelectMenu(int gid,int bid) {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select * from MenuInfo a where bid="+bid+" and menuid not in(	select menuid from dbo.Permission where groupid="+gid+" )");
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }

        /// <summary>
        /// 拥有的权限
        /// </summary>
        /// <param name="gid"></param>
        /// <param name="bid"></param>
        /// <returns></returns>
        public DataTable GetSelectMenu(int gid,string bid)
        {
            
            StringBuilder sql = new StringBuilder();
            sql.Append("select menuId,MenuName from menuinfo where statusid=1 and bid="+bid+" and menuid not in(select a.menuid from Permission a where a.menubid="+bid+" and a.groupid="+gid+")");
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
        

        public DataTable GetSelectMenuBid(int gid,string bid)
        { 
              StringBuilder sql = new StringBuilder();
             sql.Append(" select * from Permission a ");
             sql.Append("left join MenuInfo b on b.menuId=a.menuid ");
             sql.Append("where a.menubid="+bid+" and a.groupid="+gid+" and b.statusid=1 ");
             return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
        
    }
}
