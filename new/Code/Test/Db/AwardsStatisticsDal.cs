using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data; 

namespace Db
{
    public class AwardsStatisticsDal
    {
        public string conn = SqlHelper.ConnectionString;

        
        
        #region Dal Core Functional

        #region Add
        public int Add(Model.AwardsStatisticsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into  [AwardsStatistics]");
            strSql.Append("(AwardsId,AwardsName,DateStamp,YesterdayTotal,TodayTotal,AllTotal,BackTotal,AwardsType,PrizeName,Angle,PresetValue,CreateTime,UpdateTime,StatusId,Remark)");
            strSql.Append(" values (@AwardsId,@AwardsName,@DateStamp,@YesterdayTotal,@TodayTotal,@AllTotal,@BackTotal,@AwardsType,@PrizeName,@Angle,@PresetValue,@CreateTime,@UpdateTime,@StatusId,@Remark)");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
					new SqlParameter("@AwardsId", DbTool.FixSqlParameter(model.AwardsId))
,					new SqlParameter("@AwardsName", DbTool.FixSqlParameter(model.AwardsName))
,					new SqlParameter("@DateStamp", DbTool.FixSqlParameter(model.DateStamp))
,					new SqlParameter("@YesterdayTotal", DbTool.FixSqlParameter(model.YesterdayTotal))
,					new SqlParameter("@TodayTotal", DbTool.FixSqlParameter(model.TodayTotal))
,					new SqlParameter("@AllTotal", DbTool.FixSqlParameter(model.AllTotal))
,					new SqlParameter("@BackTotal", DbTool.FixSqlParameter(model.BackTotal))
,					new SqlParameter("@AwardsType", DbTool.FixSqlParameter(model.AwardsType))
,					new SqlParameter("@PrizeName", DbTool.FixSqlParameter(model.PrizeName))
,					new SqlParameter("@Angle", DbTool.FixSqlParameter(model.Angle))
,					new SqlParameter("@PresetValue", DbTool.FixSqlParameter(model.PresetValue))
,					new SqlParameter("@CreateTime", DbTool.FixSqlParameter(model.CreateTime))
,					new SqlParameter("@UpdateTime", DbTool.FixSqlParameter(model.UpdateTime))
,					new SqlParameter("@StatusId", DbTool.FixSqlParameter(model.StatusId))
,					new SqlParameter("@Remark", DbTool.FixSqlParameter(model.Remark))
                 };


            return DbTool.ConvertObject<int>(SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters),0);
         
        }
         
         
         #endregion
         
        #region Update
        public int Update(Model.AwardsStatisticsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AwardsStatistics set ");
            strSql.Append("AwardsId=@AwardsId,AwardsName=@AwardsName,DateStamp=@DateStamp,YesterdayTotal=@YesterdayTotal,TodayTotal=@TodayTotal,AllTotal=@AllTotal,BackTotal=@BackTotal,AwardsType=@AwardsType,PrizeName=@PrizeName,Angle=@Angle,PresetValue=@PresetValue,CreateTime=@CreateTime,UpdateTime=@UpdateTime,StatusId=@StatusId,Remark=@Remark ");
            strSql.Append(" where Id=@Id ");
       
            SqlParameter[] parameters = {
					new SqlParameter("@AwardsId", DbTool.FixSqlParameter(model.AwardsId))
,					new SqlParameter("@AwardsName", DbTool.FixSqlParameter(model.AwardsName))
,					new SqlParameter("@DateStamp", DbTool.FixSqlParameter(model.DateStamp))
,					new SqlParameter("@YesterdayTotal", DbTool.FixSqlParameter(model.YesterdayTotal))
,					new SqlParameter("@TodayTotal", DbTool.FixSqlParameter(model.TodayTotal))
,					new SqlParameter("@AllTotal", DbTool.FixSqlParameter(model.AllTotal))
,					new SqlParameter("@BackTotal", DbTool.FixSqlParameter(model.BackTotal))
,					new SqlParameter("@AwardsType", DbTool.FixSqlParameter(model.AwardsType))
,					new SqlParameter("@PrizeName", DbTool.FixSqlParameter(model.PrizeName))
,					new SqlParameter("@Angle", DbTool.FixSqlParameter(model.Angle))
,					new SqlParameter("@PresetValue", DbTool.FixSqlParameter(model.PresetValue))
,					new SqlParameter("@CreateTime", DbTool.FixSqlParameter(model.CreateTime))
,					new SqlParameter("@UpdateTime", DbTool.FixSqlParameter(model.UpdateTime))
,					new SqlParameter("@StatusId", DbTool.FixSqlParameter(model.StatusId))
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
            sql.Append("delete from AwardsStatistics where Id = " + id);
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region BindDataReader
        protected void BindDataReader(Model.AwardsStatisticsModel model,SqlDataReader dr)
        {

                model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
                model.AwardsId = DbTool.ConvertObject<System.Int32>(dr["AwardsId"]);
                model.AwardsName = DbTool.ConvertObject<System.String>(dr["AwardsName"]);
                model.DateStamp = DbTool.ConvertObject<System.String>(dr["DateStamp"]);
                model.YesterdayTotal = DbTool.ConvertObject<System.Int32>(dr["YesterdayTotal"]);
                model.TodayTotal = DbTool.ConvertObject<System.Int32>(dr["TodayTotal"]);
                model.AllTotal = DbTool.ConvertObject<System.Int32>(dr["AllTotal"]);
                model.BackTotal = DbTool.ConvertObject<System.Int32>(dr["BackTotal"]);
                model.AwardsType = DbTool.ConvertObject<System.Int32>(dr["AwardsType"]);
                model.PrizeName = DbTool.ConvertObject<System.String>(dr["PrizeName"]);
                model.Angle = DbTool.ConvertObject<System.String>(dr["Angle"]);
                model.PresetValue = DbTool.ConvertObject<System.String>(dr["PresetValue"]);
                model.CreateTime = DbTool.ConvertObject<System.DateTime>(dr["CreateTime"]);
                model.UpdateTime = DbTool.ConvertObject<System.DateTime>(dr["UpdateTime"]);
                model.StatusId = DbTool.ConvertObject<System.Int32>(dr["StatusId"]);
                model.Remark = DbTool.ConvertObject<System.String>(dr["Remark"]);


        }
         #endregion
         
        #region AutoBindDataReader
        protected Model.AwardsStatisticsModel AutoBindDataReader(SqlDataReader dr, params string[] fields)
        {

           var model = new Model.AwardsStatisticsModel();
                if (DbTool.HasFields("Id", fields)) model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
                if (DbTool.HasFields("AwardsId", fields)) model.AwardsId = DbTool.ConvertObject<System.Int32>(dr["AwardsId"]);
                if (DbTool.HasFields("AwardsName", fields)) model.AwardsName = DbTool.ConvertObject<System.String>(dr["AwardsName"]);
                if (DbTool.HasFields("DateStamp", fields)) model.DateStamp = DbTool.ConvertObject<System.String>(dr["DateStamp"]);
                if (DbTool.HasFields("YesterdayTotal", fields)) model.YesterdayTotal = DbTool.ConvertObject<System.Int32>(dr["YesterdayTotal"]);
                if (DbTool.HasFields("TodayTotal", fields)) model.TodayTotal = DbTool.ConvertObject<System.Int32>(dr["TodayTotal"]);
                if (DbTool.HasFields("AllTotal", fields)) model.AllTotal = DbTool.ConvertObject<System.Int32>(dr["AllTotal"]);
                if (DbTool.HasFields("BackTotal", fields)) model.BackTotal = DbTool.ConvertObject<System.Int32>(dr["BackTotal"]);
                if (DbTool.HasFields("AwardsType", fields)) model.AwardsType = DbTool.ConvertObject<System.Int32>(dr["AwardsType"]);
                if (DbTool.HasFields("PrizeName", fields)) model.PrizeName = DbTool.ConvertObject<System.String>(dr["PrizeName"]);
                if (DbTool.HasFields("Angle", fields)) model.Angle = DbTool.ConvertObject<System.String>(dr["Angle"]);
                if (DbTool.HasFields("PresetValue", fields)) model.PresetValue = DbTool.ConvertObject<System.String>(dr["PresetValue"]);
                if (DbTool.HasFields("CreateTime", fields)) model.CreateTime = DbTool.ConvertObject<System.DateTime>(dr["CreateTime"]);
                if (DbTool.HasFields("UpdateTime", fields)) model.UpdateTime = DbTool.ConvertObject<System.DateTime>(dr["UpdateTime"]);
                if (DbTool.HasFields("StatusId", fields)) model.StatusId = DbTool.ConvertObject<System.Int32>(dr["StatusId"]);
                if (DbTool.HasFields("Remark", fields)) model.Remark = DbTool.ConvertObject<System.String>(dr["Remark"]);

           return model;

        }
         #endregion
         
        #endregion 

        #region GetList
        public DataTable GetList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from AwardsStatistics where 1=1 ");
            sql.Append(sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region GetModel
        public Model.AwardsStatisticsModel GetModel(int Id)
        {

            string sql = "select top 1 * from AwardsStatistics where Id =" + Id;
            Model.AwardsStatisticsModel model = new Model.AwardsStatisticsModel();
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
        public List<Model.AwardsStatisticsModel> GetModelList()
        {

            List<Model.AwardsStatisticsModel> result = new List<Model.AwardsStatisticsModel>();
            string sql = "select * from AwardsStatistics where 1=1";
            Model.AwardsStatisticsModel model = new Model.AwardsStatisticsModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            //var fields = DbTool.GetReaderFieldNames(dr);
            while (dr.Read())
            {
                 //model = AutoBindDataReader(dr, fields);
                 model = new Model.AwardsStatisticsModel(); 
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
            sql.Append("select count(*) from AwardsStatistics where 1=1 ");
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
            pages.TableName = "AwardsStatistics";
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
           pages.TableName = "AwardsStatistics";
           pages.JoinTable = " ";
           pages.CountFields = " a.Id ";
           pages.OrderString = " order by a.Id ";
           pages.SelectFileds = " a.* ";
           pages.doCount = 0;
           PageHelper p = new PageHelper();
           DataTable dt = p.GetList(pages);
           return dt;
         }
         #endregion 
         
        #region CustomFunction

        public DataTable GetByColumnList(string Column, string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select " + Column + " from AwardsStatistics where 1=1 ");
            sql.Append(sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }

        #region GetModel
        public Model.AwardsStatisticsModel GetModelByAwardId(int AwardId)
        {

            string sql = "select top 1 * from AwardsStatistics where AwardId =" + AwardId;
            Model.AwardsStatisticsModel model = new Model.AwardsStatisticsModel();
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

        #region GetModelList
        public List<Model.AwardsStatisticsModel> GetModelList(string sqlwhere)
        {

            List<Model.AwardsStatisticsModel> result = new List<Model.AwardsStatisticsModel>();
            string sql = "select * from AwardsStatistics where 1=1 " + sqlwhere;
            Model.AwardsStatisticsModel model = new Model.AwardsStatisticsModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            //var fields = DbTool.GetReaderFieldNames(dr);
            while (dr.Read())
            {
                //model = AutoBindDataReader(dr, fields);
                model = new Model.AwardsStatisticsModel();
                BindDataReader(model, dr);
                result.Add(model);
            }
            dr.Close();
            return result;
        }

        public List<Model.AwardsStatisticsModel> GetModelTopList(int TopNum, string sqlwhere)
        {

            List<Model.AwardsStatisticsModel> result = new List<Model.AwardsStatisticsModel>();
            string sql = "select top " + TopNum + " * from AwardsStatistics where 1=1 " + sqlwhere + " order by id ";
            Model.AwardsStatisticsModel model = new Model.AwardsStatisticsModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            //var fields = DbTool.GetReaderFieldNames(dr);
            while (dr.Read())
            {
                //model = AutoBindDataReader(dr, fields);
                model = new Model.AwardsStatisticsModel();
                BindDataReader(model, dr);
                result.Add(model);
            }
            dr.Close();
            return result;
        }

        #endregion

        #region Update
        public int UpdatePrizeNameById(Model.AwardsStatisticsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AwardsStatistics set ");
            strSql.Append("PrizeName=@PrizeName,PresetValue=@PresetValue");
            strSql.Append(" where Id=@Id ");

            SqlParameter[] parameters = {
					new SqlParameter("@PrizeName", DbTool.FixSqlParameter(model.PrizeName))
,					new SqlParameter("@PresetValue", DbTool.FixSqlParameter(model.PresetValue)) 
,					new SqlParameter("@Id", model.Id)
                 };


            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }
        #endregion



        #endregion
        
 
    }
}
