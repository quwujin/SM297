using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Db
{
    public class VerificationCodeDal
    {
        public string conn = SqlHelper.ConnectionString;

        
        
        #region Dal Core Functional

        #region Add
        public int Add(Model.VerificationCodeModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into  [VerificationCode]");
            strSql.Append("(Code,Mobile,OpenId,CreateTime,TimeStamp,Ip,StatusId,ExpiryTime,Remark)");
            strSql.Append(" values (@Code,@Mobile,@OpenId,@CreateTime,@TimeStamp,@Ip,@StatusId,@ExpiryTime,@Remark)");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
					new SqlParameter("@Code", DbTool.FixSqlParameter(model.Code))
,					new SqlParameter("@Mobile", DbTool.FixSqlParameter(model.Mobile))
,					new SqlParameter("@OpenId", DbTool.FixSqlParameter(model.OpenId))
,					new SqlParameter("@CreateTime", DbTool.FixSqlParameter(model.CreateTime))
,					new SqlParameter("@TimeStamp", DbTool.FixSqlParameter(model.TimeStamp))
,					new SqlParameter("@Ip", DbTool.FixSqlParameter(model.Ip))
,					new SqlParameter("@StatusId", DbTool.FixSqlParameter(model.StatusId))
,					new SqlParameter("@ExpiryTime", DbTool.FixSqlParameter(model.ExpiryTime))
,					new SqlParameter("@Remark", DbTool.FixSqlParameter(model.Remark))
                 };


            return DbTool.ConvertObject<int>(SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters),0);
         
        }
         
         
         #endregion
         
        #region Update
        public int Update(Model.VerificationCodeModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update VerificationCode set ");
            strSql.Append("Code=@Code,Mobile=@Mobile,OpenId=@OpenId,CreateTime=@CreateTime,TimeStamp=@TimeStamp,Ip=@Ip,StatusId=@StatusId,ExpiryTime=@ExpiryTime,Remark=@Remark ");
            strSql.Append(" where Id=@Id ");
       
            SqlParameter[] parameters = {
					new SqlParameter("@Code", DbTool.FixSqlParameter(model.Code))
,					new SqlParameter("@Mobile", DbTool.FixSqlParameter(model.Mobile))
,					new SqlParameter("@OpenId", DbTool.FixSqlParameter(model.OpenId))
,					new SqlParameter("@CreateTime", DbTool.FixSqlParameter(model.CreateTime))
,					new SqlParameter("@TimeStamp", DbTool.FixSqlParameter(model.TimeStamp))
,					new SqlParameter("@Ip", DbTool.FixSqlParameter(model.Ip))
,					new SqlParameter("@StatusId", DbTool.FixSqlParameter(model.StatusId))
,					new SqlParameter("@ExpiryTime", DbTool.FixSqlParameter(model.ExpiryTime))
,					new SqlParameter("@Remark", DbTool.FixSqlParameter(model.Remark))
,					new SqlParameter("@Id", model.Id)
                 };


            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }
         #endregion
         
        #region Delete
        public int Del(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from VerificationCode where Id = " + id);
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region BindDataReader
        protected void BindDataReader(Model.VerificationCodeModel model,SqlDataReader dr)
        {

                model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
                model.Code = DbTool.ConvertObject<System.String>(dr["Code"]);
                model.Mobile = DbTool.ConvertObject<System.String>(dr["Mobile"]);
                model.OpenId = DbTool.ConvertObject<System.String>(dr["OpenId"]);
                model.CreateTime = DbTool.ConvertObject<System.DateTime>(dr["CreateTime"]);
                model.TimeStamp = DbTool.ConvertObject<System.String>(dr["TimeStamp"]);
                model.Ip = DbTool.ConvertObject<System.String>(dr["Ip"]);
                model.StatusId = DbTool.ConvertObject<System.Int32>(dr["StatusId"]);
                model.ExpiryTime = DbTool.ConvertObject<System.DateTime>(dr["ExpiryTime"]);
                model.Remark = DbTool.ConvertObject<System.String>(dr["Remark"]);


        }
         #endregion
         
        #region AutoBindDataReader
        protected Model.VerificationCodeModel AutoBindDataReader(SqlDataReader dr, params string[] fields)
        {

           var model = new Model.VerificationCodeModel();
                if (DbTool.HasFields("Id", fields)) model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
                if (DbTool.HasFields("Code", fields)) model.Code = DbTool.ConvertObject<System.String>(dr["Code"]);
                if (DbTool.HasFields("Mobile", fields)) model.Mobile = DbTool.ConvertObject<System.String>(dr["Mobile"]);
                if (DbTool.HasFields("OpenId", fields)) model.OpenId = DbTool.ConvertObject<System.String>(dr["OpenId"]);
                if (DbTool.HasFields("CreateTime", fields)) model.CreateTime = DbTool.ConvertObject<System.DateTime>(dr["CreateTime"]);
                if (DbTool.HasFields("TimeStamp", fields)) model.TimeStamp = DbTool.ConvertObject<System.String>(dr["TimeStamp"]);
                if (DbTool.HasFields("Ip", fields)) model.Ip = DbTool.ConvertObject<System.String>(dr["Ip"]);
                if (DbTool.HasFields("StatusId", fields)) model.StatusId = DbTool.ConvertObject<System.Int32>(dr["StatusId"]);
                if (DbTool.HasFields("ExpiryTime", fields)) model.ExpiryTime = DbTool.ConvertObject<System.DateTime>(dr["ExpiryTime"]);
                if (DbTool.HasFields("Remark", fields)) model.Remark = DbTool.ConvertObject<System.String>(dr["Remark"]);

           return model;

        }
         #endregion
         
        #endregion 

        #region GetList
        public DataTable GetList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from VerificationCode where 1=1 ");
            sql.Append(sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region GetModel
        public Model.VerificationCodeModel GetModel(int Id)
        {

            string sql = "select top 1 * from VerificationCode where Id =" + Id;
            Model.VerificationCodeModel model = new Model.VerificationCodeModel();
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
        public List<Model.VerificationCodeModel> GetModelList()
        {

            List<Model.VerificationCodeModel> result = new List<Model.VerificationCodeModel>();
            string sql = "select * from VerificationCode where 1=1";
            Model.VerificationCodeModel model = new Model.VerificationCodeModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            //var fields = DbTool.GetReaderFieldNames(dr);
            while (dr.Read())
            {
                 //model = AutoBindDataReader(dr, fields);
                 model = new Model.VerificationCodeModel(); 
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
            sql.Append("select count(*) from VerificationCode where 1=1 ");
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
            pages.TableName = "VerificationCode";
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
           pages.TableName = "VerificationCode";
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
           StringBuilder sql = new StringBuilder("select a.* from VerificationCode a  where 1=1 " + sqlstr);
           return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
        #endregion
         

        
 
    }
}
