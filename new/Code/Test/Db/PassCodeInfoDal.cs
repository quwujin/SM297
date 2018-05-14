using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data; 

namespace Db
{
    public class PassCodeInfoDal
    {
        public string conn = SqlHelper.ConnectionString;

        
        
        #region Dal Core Functional

        #region Add
        public int Add(Model.PassCodeInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into  [PassCodeInfo]");
            strSql.Append("(Codes,CodeIndex,CodeName,OpenId,StatusId,CreateTime,ActiveTime,Mob,ActiveIp,EventId,CustomerId,Notes)");
            strSql.Append(" values (@Codes,@CodeIndex,@CodeName,@OpenId,@StatusId,@CreateTime,@ActiveTime,@Mob,@ActiveIp,@EventId,@CustomerId,@Notes)");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
					new SqlParameter("@Codes", DbTool.FixSqlParameter(model.Codes))
,					new SqlParameter("@CodeIndex", DbTool.FixSqlParameter(model.CodeIndex))
,					new SqlParameter("@CodeName", DbTool.FixSqlParameter(model.CodeName))
,					new SqlParameter("@OpenId", DbTool.FixSqlParameter(model.OpenId))
,					new SqlParameter("@StatusId", DbTool.FixSqlParameter(model.StatusId))
,					new SqlParameter("@CreateTime", DbTool.FixSqlParameter(model.CreateTime))
,					new SqlParameter("@ActiveTime", DbTool.FixSqlParameter(model.ActiveTime))
,					new SqlParameter("@Mob", DbTool.FixSqlParameter(model.Mob))
,					new SqlParameter("@ActiveIp", DbTool.FixSqlParameter(model.ActiveIp))
,					new SqlParameter("@EventId", DbTool.FixSqlParameter(model.EventId))
,					new SqlParameter("@CustomerId", DbTool.FixSqlParameter(model.CustomerId))
,					new SqlParameter("@Notes", DbTool.FixSqlParameter(model.Notes))
                 };


            return DbTool.ConvertObject<int>(SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters),0);
         
        }
         
         
         #endregion
         
        #region Update
        public int Update(Model.PassCodeInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PassCodeInfo set ");
            strSql.Append("Codes=@Codes,CodeIndex=@CodeIndex,CodeName=@CodeName,OpenId=@OpenId,StatusId=@StatusId,CreateTime=@CreateTime,ActiveTime=@ActiveTime,Mob=@Mob,ActiveIp=@ActiveIp,EventId=@EventId,CustomerId=@CustomerId,Notes=@Notes ");
            strSql.Append(" where Id=@Id ");
       
            SqlParameter[] parameters = {
					new SqlParameter("@Codes", DbTool.FixSqlParameter(model.Codes))
,					new SqlParameter("@CodeIndex", DbTool.FixSqlParameter(model.CodeIndex))
,					new SqlParameter("@CodeName", DbTool.FixSqlParameter(model.CodeName))
,					new SqlParameter("@OpenId", DbTool.FixSqlParameter(model.OpenId))
,					new SqlParameter("@StatusId", DbTool.FixSqlParameter(model.StatusId))
,					new SqlParameter("@CreateTime", DbTool.FixSqlParameter(model.CreateTime))
,					new SqlParameter("@ActiveTime", DbTool.FixSqlParameter(model.ActiveTime))
,					new SqlParameter("@Mob", DbTool.FixSqlParameter(model.Mob))
,					new SqlParameter("@ActiveIp", DbTool.FixSqlParameter(model.ActiveIp))
,					new SqlParameter("@EventId", DbTool.FixSqlParameter(model.EventId))
,					new SqlParameter("@CustomerId", DbTool.FixSqlParameter(model.CustomerId))
,					new SqlParameter("@Notes", DbTool.FixSqlParameter(model.Notes))
,					new SqlParameter("@Id", model.Id)
                 };


            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }
         #endregion
         
        #region Delete
        public int Del(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from PassCodeInfo where Id = " + id);
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region BindDataReader
        protected void BindDataReader(Model.PassCodeInfoModel model,SqlDataReader dr)
        {

                model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
                model.Codes = DbTool.ConvertObject<System.String>(dr["Codes"]);
                model.CodeIndex = DbTool.ConvertObject<System.String>(dr["CodeIndex"]);
                model.CodeName = DbTool.ConvertObject<System.String>(dr["CodeName"]);
                model.OpenId = DbTool.ConvertObject<System.String>(dr["OpenId"]);
                model.StatusId = DbTool.ConvertObject<System.Int32>(dr["StatusId"]);
                model.CreateTime = DbTool.ConvertObject<System.DateTime>(dr["CreateTime"]);
                model.ActiveTime = DbTool.ConvertObject<System.DateTime>(dr["ActiveTime"]);
                model.Mob = DbTool.ConvertObject<System.String>(dr["Mob"]);
                model.ActiveIp = DbTool.ConvertObject<System.String>(dr["ActiveIp"]);
                model.EventId = DbTool.ConvertObject<System.Int32>(dr["EventId"]);
                model.CustomerId = DbTool.ConvertObject<System.Int32>(dr["CustomerId"]);
                model.Notes = DbTool.ConvertObject<System.String>(dr["Notes"]);


        }
         #endregion
         
        #region AutoBindDataReader
        protected Model.PassCodeInfoModel AutoBindDataReader(SqlDataReader dr, params string[] fields)
        {

           var model = new Model.PassCodeInfoModel();
           if (DbTool.HasFields("Id", fields)) model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
                if (DbTool.HasFields("Codes", fields)) model.Codes = DbTool.ConvertObject<System.String>(dr["Codes"]);
                if (DbTool.HasFields("CodeIndex", fields)) model.CodeIndex = DbTool.ConvertObject<System.String>(dr["CodeIndex"]);
                if (DbTool.HasFields("CodeName", fields)) model.CodeName = DbTool.ConvertObject<System.String>(dr["CodeName"]);
                if (DbTool.HasFields("OpenId", fields)) model.OpenId = DbTool.ConvertObject<System.String>(dr["OpenId"]);
                if (DbTool.HasFields("StatusId", fields)) model.StatusId = DbTool.ConvertObject<System.Int32>(dr["StatusId"]);
                if (DbTool.HasFields("CreateTime", fields)) model.CreateTime = DbTool.ConvertObject<System.DateTime>(dr["CreateTime"]);
                if (DbTool.HasFields("ActiveTime", fields)) model.ActiveTime = DbTool.ConvertObject<System.DateTime>(dr["ActiveTime"]);
                if (DbTool.HasFields("Mob", fields)) model.Mob = DbTool.ConvertObject<System.String>(dr["Mob"]);
                if (DbTool.HasFields("ActiveIp", fields)) model.ActiveIp = DbTool.ConvertObject<System.String>(dr["ActiveIp"]);
                if (DbTool.HasFields("EventId", fields)) model.EventId = DbTool.ConvertObject<System.Int32>(dr["EventId"]);
                if (DbTool.HasFields("CustomerId", fields)) model.CustomerId = DbTool.ConvertObject<System.Int32>(dr["CustomerId"]);
                if (DbTool.HasFields("Notes", fields)) model.Notes = DbTool.ConvertObject<System.String>(dr["Notes"]);

           return model;

        }
         #endregion
         
        #endregion 

        #region GetList
        public DataTable GetList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from PassCodeInfo where 1=1 ");
            sql.Append(sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region GetModel
        public Model.PassCodeInfoModel GetModel(int Id)
        {

            string sql = "select top 1 * from PassCodeInfo where Id =" + Id;
            Model.PassCodeInfoModel model = new Model.PassCodeInfoModel();
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
        public List<Model.PassCodeInfoModel> GetModelList()
        {

            List<Model.PassCodeInfoModel> result = new List<Model.PassCodeInfoModel>();
            string sql = "select * from PassCodeInfo where 1=1";
            Model.PassCodeInfoModel model = new Model.PassCodeInfoModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            //var fields = DbTool.GetReaderFieldNames(dr);
            while (dr.Read())
            {
                 //model = AutoBindDataReader(dr, fields);
                 model = new Model.PassCodeInfoModel(); 
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
            sql.Append("select count(*) from PassCodeInfo where 1=1 ");
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
            pages.TableName = "PassCodeInfo";
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
           pages.TableName = "PassCodeInfo";
           pages.JoinTable = " ";
           pages.CountFields = " a.Id ";
           pages.OrderString = " order by a.Id desc";
           pages.SelectFileds = " a.* ";
           pages.doCount = 0;
           PageHelper p = new PageHelper();
           DataTable dt = p.GetList(pages);
           return dt;
         }
         #endregion

        #region 自定义方法

        #region GetModelByCode
        public Model.PassCodeInfoModel GetModelByCode(string Code)
        {

            string sql = "select top 1 * from PassCodeInfo where Codes ='" + Code + "'";
            Model.PassCodeInfoModel model = new Model.PassCodeInfoModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            if (dr.Read())
            {
                //var fields = DbTool.GetReaderFieldNames(dr);
                //model = AutoBindDataReader(dr, fields);
                BindDataReader(model, dr);
            }
            dr.Close();
            return model;
        }
        #endregion

        #region GetExcelList
        public DataTable GetExcelList(string sqlstr, string ckName, string ckField, string joinTab)
        {
            string checksql = "";
            for (int i = 0; i < ckName.Split(',').Length; i++)
            {
                checksql += ckField.Split(',')[i] + " as " + ckName.Split(',')[i];
                if (i != ckName.Split(',').Length - 1)
                {
                    checksql += ",";
                }
            }
            string sql = "select " + checksql + " from PassCodeInfo a " + joinTab + " where 1=1 " + sqlstr;
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
        #endregion

        #endregion

         

        
 
    }
}
