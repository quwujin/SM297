using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
 

namespace Db
{
    public class UserInfoDal
    {
        public string conn = SqlHelper.ConnectionString;



        public Model.UserInfoModel GetModel(string username) {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select  top 1 a.*,b.groupName from UserInfo a left join UserInfo_Group b on a.groupId = b.groupId     ");
            strSql.Append(" where UserName='"+username +"'");


            Model.UserInfoModel model = new Model.UserInfoModel();
            DataSet ds = SqlHelper.ExecuteDataset(conn, CommandType.Text, strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {

                if (ds.Tables[0].Rows[0]["UserId"] != null && ds.Tables[0].Rows[0]["UserId"].ToString() != "")
                {
                    model.UserId = int.Parse(ds.Tables[0].Rows[0]["UserId"].ToString());
                }

                if (ds.Tables[0].Rows[0]["GroupName"] != null && ds.Tables[0].Rows[0]["GroupName"].ToString() != "")
                {
                    model.GroupName = ds.Tables[0].Rows[0]["GroupName"].ToString();
                }


                if (ds.Tables[0].Rows[0]["UserName"] != null && ds.Tables[0].Rows[0]["UserName"].ToString() != "")
                {
                    model.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PassWord"] != null && ds.Tables[0].Rows[0]["PassWord"].ToString() != "")
                {
                    model.PassWord = ds.Tables[0].Rows[0]["PassWord"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RealName"] != null && ds.Tables[0].Rows[0]["RealName"].ToString() != "")
                {
                    model.RealName = ds.Tables[0].Rows[0]["RealName"].ToString();
                }

                if (ds.Tables[0].Rows[0]["Mob"] != null && ds.Tables[0].Rows[0]["Mob"].ToString() != "")
                {
                    model.Mob = ds.Tables[0].Rows[0]["Mob"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Email"] != null && ds.Tables[0].Rows[0]["Email"].ToString() != "")
                {
                    model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                }
                if (ds.Tables[0].Rows[0]["StatusId"] != null && ds.Tables[0].Rows[0]["StatusId"].ToString() != "")
                {
                    model.StatusId = int.Parse(ds.Tables[0].Rows[0]["StatusId"].ToString());
                }

                if (ds.Tables[0].Rows[0]["GroupId"] != null && ds.Tables[0].Rows[0]["GroupId"].ToString() != "")
                {
                    model.GroupId = int.Parse(ds.Tables[0].Rows[0]["GroupId"].ToString());
                }

                if (ds.Tables[0].Rows[0]["LevelId"] != null && ds.Tables[0].Rows[0]["LevelId"].ToString() != "")
                {
                    model.LevelId = int.Parse(ds.Tables[0].Rows[0]["LevelId"].ToString());
                }

                if (ds.Tables[0].Rows[0]["LoginCount"] != null && ds.Tables[0].Rows[0]["LoginCount"].ToString() != "")
                {
                    model.LoginCount = int.Parse(ds.Tables[0].Rows[0]["LoginCount"].ToString());
                }

                if (ds.Tables[0].Rows[0]["LastLoginTime"] != null && ds.Tables[0].Rows[0]["LastLoginTime"].ToString() != "")
                {
                    model.LastLoginTime = DateTime.Parse(ds.Tables[0].Rows[0]["LastLoginTime"].ToString());
                }

                if (ds.Tables[0].Rows[0]["PostId"] != null && ds.Tables[0].Rows[0]["PostId"].ToString() != "")
                {
                    model.PostId = int.Parse(ds.Tables[0].Rows[0]["PostId"].ToString());
                }

                if (ds.Tables[0].Rows[0]["RoleId"] != null && ds.Tables[0].Rows[0]["RoleId"].ToString() != "")
                {
                    model.RoleId = int.Parse(ds.Tables[0].Rows[0]["RoleId"].ToString());
                }

                return model;
            }
            else
            {
                return null;
            }
        }



        public Model.UserInfoModel GetModel(int userid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select  top 1 a.*,b.GroupName from UserInfo a left join UserInfo_Group b on a.groupId = b.groupId     ");
            strSql.Append(" where userid= " + userid);


            Model.UserInfoModel model = new Model.UserInfoModel();
            DataSet ds = SqlHelper.ExecuteDataset(conn, CommandType.Text, strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["UserId"] != null && ds.Tables[0].Rows[0]["UserId"].ToString() != "")
                {
                    model.UserId = int.Parse(ds.Tables[0].Rows[0]["UserId"].ToString());
                }

                if (ds.Tables[0].Rows[0]["GroupName"] != null && ds.Tables[0].Rows[0]["GroupName"].ToString() != "")
                {
                    model.GroupName = ds.Tables[0].Rows[0]["GroupName"].ToString();
                }


                if (ds.Tables[0].Rows[0]["UserName"] != null && ds.Tables[0].Rows[0]["UserName"].ToString() != "")
                {
                    model.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PassWord"] != null && ds.Tables[0].Rows[0]["PassWord"].ToString() != "")
                {
                    model.PassWord = ds.Tables[0].Rows[0]["PassWord"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RealName"] != null && ds.Tables[0].Rows[0]["RealName"].ToString() != "")
                {
                    model.RealName = ds.Tables[0].Rows[0]["RealName"].ToString();
                }

                if (ds.Tables[0].Rows[0]["Mob"] != null && ds.Tables[0].Rows[0]["Mob"].ToString() != "")
                {
                    model.Mob = ds.Tables[0].Rows[0]["Mob"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Email"] != null && ds.Tables[0].Rows[0]["Email"].ToString() != "")
                {
                    model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                }
                if (ds.Tables[0].Rows[0]["StatusId"] != null && ds.Tables[0].Rows[0]["StatusId"].ToString() != "")
                {
                    model.StatusId = int.Parse(ds.Tables[0].Rows[0]["StatusId"].ToString());
                }

                if (ds.Tables[0].Rows[0]["GroupId"] != null && ds.Tables[0].Rows[0]["GroupId"].ToString() != "")
                {
                    model.GroupId = int.Parse(ds.Tables[0].Rows[0]["GroupId"].ToString());
                }

                if (ds.Tables[0].Rows[0]["LevelId"] != null && ds.Tables[0].Rows[0]["LevelId"].ToString() != "")
                {
                    model.LevelId = int.Parse(ds.Tables[0].Rows[0]["LevelId"].ToString());
                }

                if (ds.Tables[0].Rows[0]["LoginCount"] != null && ds.Tables[0].Rows[0]["LoginCount"].ToString() != "")
                {
                    model.LoginCount = int.Parse(ds.Tables[0].Rows[0]["LoginCount"].ToString());
                }

                if (ds.Tables[0].Rows[0]["LastLoginTime"] != null && ds.Tables[0].Rows[0]["LastLoginTime"].ToString() != "")
                {
                    model.LastLoginTime = DateTime.Parse(ds.Tables[0].Rows[0]["LastLoginTime"].ToString());
                }

                if (ds.Tables[0].Rows[0]["PostId"] != null && ds.Tables[0].Rows[0]["PostId"].ToString() != "")
                {
                    model.PostId = int.Parse(ds.Tables[0].Rows[0]["PostId"].ToString());
                }

                if (ds.Tables[0].Rows[0]["RoleId"] != null && ds.Tables[0].Rows[0]["RoleId"].ToString() != "")
                {
                    model.RoleId = int.Parse(ds.Tables[0].Rows[0]["RoleId"].ToString());
                }

              
                return model;
            }
            else
            {
                return null;
            }
        }




        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Model.UserInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update UserInfo set ");
            strSql.Append("PassWord=@PassWord,");
            strSql.Append("Mob=@Mob,");
            strSql.Append("Email=@Email,");
            strSql.Append("GroupId=@GroupId,");
            strSql.Append("LevelId=@LevelId,");
            strSql.Append("PostId=@PostId,");
            strSql.Append("RoleId=@RoleId,");
            strSql.Append("StatusId=@StatusId ");
            strSql.Append(" where UserId=@UserId");
            SqlParameter[] parameters = {
					new SqlParameter("@RealName", SqlDbType.NVarChar,50),
					new SqlParameter("@Mob", SqlDbType.NVarChar,50),
					new SqlParameter("@Email", SqlDbType.NVarChar,80),
					new SqlParameter("@StatusId", SqlDbType.Int,4),
					new SqlParameter("@UserId", SqlDbType.Int,4),
                   new SqlParameter("@GroupId", SqlDbType.Int,4),
                    new SqlParameter("@PostId",SqlDbType.Int,4),
                    new SqlParameter("@RoleId",SqlDbType.Int,4),
                    new SqlParameter("@LevelId",SqlDbType.Int,4),
                    new SqlParameter("@PassWord",SqlDbType.NVarChar,50)
                             };
            parameters[0].Value = model.RealName;
            parameters[1].Value = model.Mob;
            parameters[2].Value = model.Email;
            parameters[3].Value = model.StatusId;
            parameters[4].Value = model.UserId;
            parameters[5].Value = model.GroupId;
            parameters[6].Value = model.PostId;
            parameters[7].Value = model.RoleId;
            parameters[8].Value = model.LevelId;
            parameters[9].Value = model.PassWord;

            try
            {
                return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception)
            {
                return 0;
            }
        }



        /// <summary>
        /// 增加一条会员数据
        /// </summary>
        public int Add(Model.UserInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into  UserInfo(");
            strSql.Append("UserName,PassWord,RealName,Mob,Email,StatusId,GroupId,PostId,RoleId,LevelId)");
            strSql.Append(" values (");
            strSql.Append("@UserName,@PassWord,@RealName,@Mob,@Email,@StatusId,@GroupId,@PostId,@RoleId,@LevelId)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@PassWord", SqlDbType.NVarChar,50),
					new SqlParameter("@RealName", SqlDbType.NVarChar,50),
					new SqlParameter("@Mob", SqlDbType.NVarChar,50),
					new SqlParameter("@Email", SqlDbType.NVarChar,80),
					new SqlParameter("@StatusId", SqlDbType.Int,4),
                    new SqlParameter("@GroupId", SqlDbType.Int,4),
                    new SqlParameter("@PostId",SqlDbType.Int,4),
                    new SqlParameter("@RoleId",SqlDbType.Int,4),
                    new SqlParameter("@LevelId",SqlDbType.Int,4)
                    
                                        };
            parameters[0].Value = model.UserName;
            parameters[1].Value = model.PassWord;
            parameters[2].Value = model.RealName;
            parameters[3].Value = model.Mob;
            parameters[4].Value = model.Email;
            parameters[5].Value = model.StatusId;
            parameters[6].Value = model.GroupId;
            parameters[7].Value = model.PostId;
            parameters[8].Value = model.RoleId;
            parameters[9].Value = model.LevelId;


            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }



        /// <summary>
        /// 用户名是否存在 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool Exists(string userName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from UserInfo");
            strSql.Append(" where userName=@userName");
            SqlParameter[] parameters = {
					new SqlParameter("@userName", SqlDbType.NVarChar,20)
			};
            parameters[0].Value = userName;

            object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);
            if (Convert.ToInt32(obj.ToString()) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public int Del(int uid)
        {
            string sql = "delete from UserInfo where Userid=" + uid;
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Login(Model.UserInfoModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select count(userId) from UserInfo where [username]='" + model.UserName + "' and [password]='" + model.PassWord + "' and statusId=1 ");
            try
            {
                int obj = Convert.ToInt32(SqlHelper.ExecuteScalar(conn, CommandType.Text, sql.ToString()));
                return obj;
            }
            catch
            {
                return 0;
            }

        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdatePwd(Model.UserInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update UserInfo set ");
            strSql.Append("password=@password");
            strSql.Append(" where username=@UserName");
            SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@PassWord", SqlDbType.NVarChar,50),
            };
            parameters[0].Value = model.UserName;
            parameters[1].Value = model.PassWord;

            int obj = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
            return obj;
        }


        /// <summary>
        /// 登录次数
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public int AddLoginCount(string username)
        {
            string sql = "update userinfo set  LastLoginTime='" + DateTime.Now + "',loginCount=LoginCount+1 where userName='" + username+"'";
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql);
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
            pages.ReturnFileds = "userid";
            pages.TableName = " UserInfo ";
            pages.JoinTable = " left join UserInfo_Group b on a.groupid=b.groupid " + joinString;
            pages.CountFields = " a.userid ";
            pages.OrderString = " ";
            pages.SelectFileds = "  a.*,b.groupName ";
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


        public DataTable GetList(string sqlstr) {
            StringBuilder sql = new StringBuilder();
            sql.Append("select a.UserId,a.UserName,a.StatusId,a.Email,a.Mob,a.LastLoginTime,b.GroupName,p.PostName,l.LevelName,r.RoleName from UserInfo a");
            sql.Append(" left join UserInfo_Group b on a.GroupId = b.GroupId ");
            sql.Append(" left join UserInfo_Post p on p.PostId = a.PostId ");
            sql.Append(" left join UserInfo_Level  l on l.id   = a.LevelId ");
            sql.Append(" left join UserInfo_Role r on r.RoleId = a.RoleId ");
            sql.Append(" where 1=1   " + sqlstr);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }

        public DataTable GetList(string sqlstr, int pageindex, int pagesize, string joinString) {
            Model.PageInfo pages = new Model.PageInfo();
            pages.PageIndex = pageindex;
            pages.PageSize = pagesize;
            pages.SqlWhere = sqlstr;
            pages.ReturnFileds = "t.*";
            pages.TableName = " UserInfo ";
            pages.JoinTable = " left join UserInfo_Group b on a.groupid=b.groupid " + joinString;
            pages.CountFields = " a.userId ";
            pages.OrderString = " order by t.userId desc";
            pages.SelectFileds = " a.*,b.groupName ";
            pages.doCount = 0;
            PageHelper p = new PageHelper();
            DataTable dt = p.GetList(pages);
            return dt;
        }



        public DataTable GetMenuListByUser(int Gid)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from v_UserMenuBid where groupid="+Gid +" order by  orderid ,menuid asc");
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }


        public DataTable GetMenuListForSid(int gid, int bid)
        {
            StringBuilder sql = new StringBuilder();
              sql.Append("select a.PerId,a.GroupId,b.MenuId,b.MenuUrl,b.MenuName,b.Bid from Permission  a ");
              sql.Append("    left join MenuInfo b on a.menuId= b.MenuId ");
              sql.Append("where groupid="+gid+" and menubid="+bid+" and statusid =1 order by orderid desc ");
              return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
    }
}
