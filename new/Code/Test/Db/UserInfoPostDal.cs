using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Db
{
    public class UserInfPostDal
    {
        public string conn = SqlHelper.ConnectionString;

        /// <summary>
        /// 添加组织
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(Model.UserInfoPostModel model) {
 
                     int obj = 0;
           
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("insert into UserInfo_Post(PostName,OrderId) values('" + model.PostName + "'," + model.OrderId + ")");
                       obj= SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString());
                       return obj;
          
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Model.UserInfoPostModel model)
        {
            int obj = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update    UserInfo_Post set PostName='"+model.PostName+"',OrderId="+model.OrderId+"  where PostId="+model.PostId+"");
            obj = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString());
            return obj;
        }


        public Model.UserInfoPostModel GetModel(int postId)
        {
            string sql = "select postId,postName,orderId from UserInfo_Post where postId =" + postId;
            Model.UserInfoPostModel model = new Model.UserInfoPostModel();
            DataTable dt = SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql);
            if (dt.Rows.Count>0)
            {
                if (dt.Rows[0]["PostId"] != null && dt.Rows[0]["PostId"].ToString() != "")
                {
                    model.PostId = int.Parse(dt.Rows[0]["PostId"].ToString());
                }
                if (dt.Rows[0]["OrderId"] != null && dt.Rows[0]["OrderId"].ToString() != "")
                {
                    model.OrderId =  int.Parse(dt.Rows[0]["OrderId"].ToString());
                }
                if (dt.Rows[0]["PostName"] != null && dt.Rows[0]["PostName"].ToString() != "")
                {
                    model.PostName = dt.Rows[0]["PostName"].ToString() ;
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
            string sql = " select PostId,PostName,OrderId from UserInfo_Post where   1=1    " + sqlwhere;
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql);
        }

 

        public int Del(int gid)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append( " delete from UserInfo_Post where PostId="+gid);
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString());
           
        }

       
  
    }
}
