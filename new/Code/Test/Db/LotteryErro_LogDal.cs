using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data; 

namespace Db
{
    public class LotteryErro_LogDal
    {
        public string conn = SqlHelper.ConnectionString;

        
        
        #region Dal Core Functional

        #region Add
        public int Add(Model.LotteryErro_LogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into  [LotteryErro_Log]");
            strSql.Append("(Ip,Mob,Code,OpenId,CreateTime,Types,LotErro,Pros,City,Note)");
            strSql.Append(" values (@Ip,@Mob,@Code,@OpenId,@CreateTime,@Types,@LotErro,@Pros,@City,@Note)");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
					new SqlParameter("@Ip", DbTool.FixSqlParameter(model.Ip))
,					new SqlParameter("@Mob", DbTool.FixSqlParameter(model.Mob))
,					new SqlParameter("@Code", DbTool.FixSqlParameter(model.Code))
,					new SqlParameter("@OpenId", DbTool.FixSqlParameter(model.OpenId))
,					new SqlParameter("@CreateTime", DbTool.FixSqlParameter(model.CreateTime))
,					new SqlParameter("@Types", DbTool.FixSqlParameter(model.Types))
,					new SqlParameter("@LotErro", DbTool.FixSqlParameter(model.LotErro))
,					new SqlParameter("@Pros", DbTool.FixSqlParameter(model.Pros))
,					new SqlParameter("@City", DbTool.FixSqlParameter(model.City))
,					new SqlParameter("@Note", DbTool.FixSqlParameter(model.Note))
                 };


            return DbTool.ConvertObject<int>(SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters),0);
         
        }
         
         
         #endregion
         
        #region Update
        public int Update(Model.LotteryErro_LogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update LotteryErro_Log set ");
            strSql.Append("Ip=@Ip,Mob=@Mob,Code=@Code,OpenId=@OpenId,CreateTime=@CreateTime,Types=@Types,LotErro=@LotErro,Pros=@Pros,City=@City,Note=@Note ");
            strSql.Append(" where Id=@Id ");
       
            SqlParameter[] parameters = {
					new SqlParameter("@Ip", DbTool.FixSqlParameter(model.Ip))
,					new SqlParameter("@Mob", DbTool.FixSqlParameter(model.Mob))
,					new SqlParameter("@Code", DbTool.FixSqlParameter(model.Code))
,					new SqlParameter("@OpenId", DbTool.FixSqlParameter(model.OpenId))
,					new SqlParameter("@CreateTime", DbTool.FixSqlParameter(model.CreateTime))
,					new SqlParameter("@Types", DbTool.FixSqlParameter(model.Types))
,					new SqlParameter("@LotErro", DbTool.FixSqlParameter(model.LotErro))
,					new SqlParameter("@Pros", DbTool.FixSqlParameter(model.Pros))
,					new SqlParameter("@City", DbTool.FixSqlParameter(model.City))
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
            sql.Append("delete from LotteryErro_Log where Id = " + id);
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region BindDataReader
        protected void BindDataReader(Model.LotteryErro_LogModel model,SqlDataReader dr)
        {

                model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
                model.Ip = DbTool.ConvertObject<System.String>(dr["Ip"]);
                model.Mob = DbTool.ConvertObject<System.String>(dr["Mob"]);
                model.Code = DbTool.ConvertObject<System.String>(dr["Code"]);
                model.OpenId = DbTool.ConvertObject<System.String>(dr["OpenId"]);
                model.CreateTime = DbTool.ConvertObject<System.String>(dr["CreateTime"]);
                model.Types = DbTool.ConvertObject<System.Int32>(dr["Types"]);
                model.LotErro = DbTool.ConvertObject<System.String>(dr["LotErro"]);
                model.Pros = DbTool.ConvertObject<System.String>(dr["Pros"]);
                model.City = DbTool.ConvertObject<System.String>(dr["City"]);
                model.Note = DbTool.ConvertObject<System.String>(dr["Note"]);


        }
         #endregion
         
        #region AutoBindDataReader
        protected Model.LotteryErro_LogModel AutoBindDataReader(SqlDataReader dr, params string[] fields)
        {

           var model = new Model.LotteryErro_LogModel();
                if (DbTool.HasFields("Id", fields)) model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
                if (DbTool.HasFields("Ip", fields)) model.Ip = DbTool.ConvertObject<System.String>(dr["Ip"]);
                if (DbTool.HasFields("Mob", fields)) model.Mob = DbTool.ConvertObject<System.String>(dr["Mob"]);
                if (DbTool.HasFields("Code", fields)) model.Code = DbTool.ConvertObject<System.String>(dr["Code"]);
                if (DbTool.HasFields("OpenId", fields)) model.OpenId = DbTool.ConvertObject<System.String>(dr["OpenId"]);
                if (DbTool.HasFields("CreateTime", fields)) model.CreateTime = DbTool.ConvertObject<System.String>(dr["CreateTime"]);
                if (DbTool.HasFields("Types", fields)) model.Types = DbTool.ConvertObject<System.Int32>(dr["Types"]);
                if (DbTool.HasFields("LotErro", fields)) model.LotErro = DbTool.ConvertObject<System.String>(dr["LotErro"]);
                if (DbTool.HasFields("Pros", fields)) model.Pros = DbTool.ConvertObject<System.String>(dr["Pros"]);
                if (DbTool.HasFields("City", fields)) model.City = DbTool.ConvertObject<System.String>(dr["City"]);
                if (DbTool.HasFields("Note", fields)) model.Note = DbTool.ConvertObject<System.String>(dr["Note"]);

           return model;

        }
         #endregion
         
        #endregion 

        #region GetList
        public DataTable GetList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from LotteryErro_Log where 1=1 ");
            sql.Append(sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
        public DataTable GetDistinctByCityList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select Distinct(Pros) from LotteryErro_Log where 1=1 ");
            sql.Append(sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region GetModel
        public Model.LotteryErro_LogModel GetModel(int Id)
        {

            string sql = "select top 1 * from LotteryErro_Log where Id =" + Id;
            Model.LotteryErro_LogModel model = new Model.LotteryErro_LogModel();
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
        public List<Model.LotteryErro_LogModel> GetModelList()
        {

            List<Model.LotteryErro_LogModel> result = new List<Model.LotteryErro_LogModel>();
            string sql = "select * from LotteryErro_Log where 1=1";
            Model.LotteryErro_LogModel model = new Model.LotteryErro_LogModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            //var fields = DbTool.GetReaderFieldNames(dr);
            while (dr.Read())
            {
                 //model = AutoBindDataReader(dr, fields);
                 model = new Model.LotteryErro_LogModel(); 
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
            sql.Append("select count(*) from LotteryErro_Log where 1=1 ");
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
            pages.TableName = "LotteryErro_Log";
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
           pages.TableName = "LotteryErro_Log";
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
            string sql = "select " + checksql + " from LotteryErro_Log a " + joinTab + " where 1=1 " + sqlstr;
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
        #endregion
        
 
    }
}
