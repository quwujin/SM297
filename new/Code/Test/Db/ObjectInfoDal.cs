using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data; 

namespace Db
{
    public class ObjectInfoDal
    {
        public string conn = SqlHelper.ConnectionString;

        
        
        #region Dal Core Functional

        #region Add
        public int Add(Model.ObjectInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into  [ObjectInfo]");
            strSql.Append("(ObjectName,IsTest,OnlineTime,OnOffTime,MobCount,OpenIdCount,IpCount,NoStartText,CStext,EndText,WHtext,Note)");
            strSql.Append(" values (@ObjectName,@IsTest,@OnlineTime,@OnOffTime,@MobCount,@OpenIdCount,@IpCount,@NoStartText,@CStext,@EndText,@WHtext,@Note)");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
					new SqlParameter("@ObjectName", DbTool.FixSqlParameter(model.ObjectName))
,					new SqlParameter("@IsTest", DbTool.FixSqlParameter(model.IsTest))
,					new SqlParameter("@OnlineTime", DbTool.FixSqlParameter(model.OnlineTime))
,					new SqlParameter("@OnOffTime", DbTool.FixSqlParameter(model.OnOffTime))
,					new SqlParameter("@MobCount", DbTool.FixSqlParameter(model.MobCount))
,					new SqlParameter("@OpenIdCount", DbTool.FixSqlParameter(model.OpenIdCount))
,					new SqlParameter("@IpCount", DbTool.FixSqlParameter(model.IpCount))
,					new SqlParameter("@NoStartText", DbTool.FixSqlParameter(model.NoStartText))
,					new SqlParameter("@CStext", DbTool.FixSqlParameter(model.CStext))
,					new SqlParameter("@EndText", DbTool.FixSqlParameter(model.EndText))
,					new SqlParameter("@WHtext", DbTool.FixSqlParameter(model.WHtext))
,					new SqlParameter("@Note", DbTool.FixSqlParameter(model.Note))
                 };


            return DbTool.ConvertObject<int>(SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters),0);
         
        }
         
         
         #endregion
         
        #region Update
        public int Update(Model.ObjectInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ObjectInfo set ");
            strSql.Append("ObjectName=@ObjectName,IsTest=@IsTest,OnlineTime=@OnlineTime,OnOffTime=@OnOffTime,MobCount=@MobCount,OpenIdCount=@OpenIdCount,IpCount=@IpCount,NoStartText=@NoStartText,CStext=@CStext,EndText=@EndText,WHtext=@WHtext,Note=@Note ");
            strSql.Append(" where Id=@Id ");
       
            SqlParameter[] parameters = {
					new SqlParameter("@ObjectName", DbTool.FixSqlParameter(model.ObjectName))
,					new SqlParameter("@IsTest", DbTool.FixSqlParameter(model.IsTest))
,					new SqlParameter("@OnlineTime", DbTool.FixSqlParameter(model.OnlineTime))
,					new SqlParameter("@OnOffTime", DbTool.FixSqlParameter(model.OnOffTime))
,					new SqlParameter("@MobCount", DbTool.FixSqlParameter(model.MobCount))
,					new SqlParameter("@OpenIdCount", DbTool.FixSqlParameter(model.OpenIdCount))
,					new SqlParameter("@IpCount", DbTool.FixSqlParameter(model.IpCount))
,					new SqlParameter("@NoStartText", DbTool.FixSqlParameter(model.NoStartText))
,					new SqlParameter("@CStext", DbTool.FixSqlParameter(model.CStext))
,					new SqlParameter("@EndText", DbTool.FixSqlParameter(model.EndText))
,					new SqlParameter("@WHtext", DbTool.FixSqlParameter(model.WHtext))
,					new SqlParameter("@Note", DbTool.FixSqlParameter(model.Note))
,					new SqlParameter("@Id", model.Id)
                 };


            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }
         #endregion
         
        #region Delete
        public int Del(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from ObjectInfo where Id = " + id);
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region BindDataReader
        protected void BindDataReader(Model.ObjectInfoModel model,SqlDataReader dr)
        {

                model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
                model.ObjectName = DbTool.ConvertObject<System.String>(dr["ObjectName"]);
                model.IsTest = DbTool.ConvertObject<System.Int32>(dr["IsTest"]);
                model.OnlineTime = DbTool.ConvertObject<System.DateTime>(dr["OnlineTime"]);
                model.OnOffTime = DbTool.ConvertObject<System.DateTime>(dr["OnOffTime"]);
                model.MobCount = DbTool.ConvertObject<System.Int32>(dr["MobCount"]);
                model.OpenIdCount = DbTool.ConvertObject<System.Int32>(dr["OpenIdCount"]);
                model.IpCount = DbTool.ConvertObject<System.Int32>(dr["IpCount"]);
                model.NoStartText = DbTool.ConvertObject<System.String>(dr["NoStartText"]);
                model.CStext = DbTool.ConvertObject<System.String>(dr["CStext"]);
                model.EndText = DbTool.ConvertObject<System.String>(dr["EndText"]);
                model.WHtext = DbTool.ConvertObject<System.String>(dr["WHtext"]);
                model.Note = DbTool.ConvertObject<System.String>(dr["Note"]);


        }
         #endregion
         
        #region AutoBindDataReader
        protected Model.ObjectInfoModel AutoBindDataReader(SqlDataReader dr, params string[] fields)
        {

           var model = new Model.ObjectInfoModel();
                if (DbTool.HasFields("Id", fields)) model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
                if (DbTool.HasFields("ObjectName", fields)) model.ObjectName = DbTool.ConvertObject<System.String>(dr["ObjectName"]);
                if (DbTool.HasFields("IsTest", fields)) model.IsTest = DbTool.ConvertObject<System.Int32>(dr["IsTest"]);
                if (DbTool.HasFields("OnlineTime", fields)) model.OnlineTime = DbTool.ConvertObject<System.DateTime>(dr["OnlineTime"]);
                if (DbTool.HasFields("OnOffTime", fields)) model.OnOffTime = DbTool.ConvertObject<System.DateTime>(dr["OnOffTime"]);
                if (DbTool.HasFields("MobCount", fields)) model.MobCount = DbTool.ConvertObject<System.Int32>(dr["MobCount"]);
                if (DbTool.HasFields("OpenIdCount", fields)) model.OpenIdCount = DbTool.ConvertObject<System.Int32>(dr["OpenIdCount"]);
                if (DbTool.HasFields("IpCount", fields)) model.IpCount = DbTool.ConvertObject<System.Int32>(dr["IpCount"]);
                if (DbTool.HasFields("NoStartText", fields)) model.NoStartText = DbTool.ConvertObject<System.String>(dr["NoStartText"]);
                if (DbTool.HasFields("CStext", fields)) model.CStext = DbTool.ConvertObject<System.String>(dr["CStext"]);
                if (DbTool.HasFields("EndText", fields)) model.EndText = DbTool.ConvertObject<System.String>(dr["EndText"]);
                if (DbTool.HasFields("WHtext", fields)) model.WHtext = DbTool.ConvertObject<System.String>(dr["WHtext"]);
                if (DbTool.HasFields("Note", fields)) model.Note = DbTool.ConvertObject<System.String>(dr["Note"]);

           return model;

        }
         #endregion
         
        #endregion 

        #region GetList
        public DataTable GetList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from ObjectInfo where 1=1 ");
            sql.Append(sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region GetModel
        public Model.ObjectInfoModel GetModel(int Id)
        {

            string sql = "select top 1 * from ObjectInfo where Id =" + Id;
            Model.ObjectInfoModel model = new Model.ObjectInfoModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            if (dr.Read()) {
                 //var fields = DbTool.GetReaderFieldNames(dr);
                 //model = AutoBindDataReader(dr, fields);
                 BindDataReader(model, dr);
            }
            dr.Close();
            return model;
        }
         #endregion
         
        #region GetModelList
        public List<Model.ObjectInfoModel> GetModelList()
        {

            List<Model.ObjectInfoModel> result = new List<Model.ObjectInfoModel>();
            string sql = "select * from ObjectInfo where 1=1";
            Model.ObjectInfoModel model = new Model.ObjectInfoModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            //var fields = DbTool.GetReaderFieldNames(dr);
            while (dr.Read())
            {
                 //model = AutoBindDataReader(dr, fields);
                 model = new Model.ObjectInfoModel(); 
                 BindDataReader(model, dr);
                 result.Add(model);
            }
            dr.Close();
            return result;
        }
         #endregion
         
        #region CheckCount
        public int CheckCount(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select count(*) from ObjectInfo where 1=1 ");
            sql.Append(sqlwhere);
            return DbTool.ConvertObject<int>(SqlHelper.ExecuteScalar(conn, CommandType.Text, sql.ToString()), 0);
        }
         #endregion
         
        #region 分页计算总数
         public int GetCount(string sqlstr, string joinString)
        {
            Model.PageInfo pages = new Model.PageInfo();
            pages.SqlWhere = sqlstr;
            pages.ReturnFileds = "Id";
            pages.SqlWhere = sqlstr;
            pages.TableName = "ObjectInfo";
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
        #endregion
         
        #region 分页计算GetList
        public DataTable GetList(string sqlstr, int pageindex, int pagesize)
        {
           Model.PageInfo pages = new Model.PageInfo();
           pages.PageIndex = pageindex;
           pages.PageSize = pagesize;
           pages.SqlWhere = sqlstr;
           pages.ReturnFileds = "t.*";
           pages.TableName = "ObjectInfo";
           pages.JoinTable = " ";
           pages.CountFields = " a.Id ";
           pages.OrderString = " order by t.Id desc";
           pages.SelectFileds = " a.* ";
           pages.doCount = 0;
           PageHelper p = new PageHelper();
           DataTable dt = p.GetList(pages);
           return dt;
         }
         #endregion
         
        #region GetExcelList
        public DataTable GetExcelList(string sqlstr)
        {
           StringBuilder sql = new StringBuilder("select a.* from ObjectInfo a  where 1=1 " + sqlstr);
           return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
        #endregion
         

        
 
    }
}
