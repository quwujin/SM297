using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data; 

namespace Db
{
    public class FileInfoDal
    {
        public string conn = SqlHelper.ConnectionString;

        
        
        #region Dal Core Functional

        #region Add
        public int Add(Model.FileInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into  [FileInfo]");
            strSql.Append("(Hashdata,FileName,Type,Size,SaveName,States,CreateTime,Note)");
            strSql.Append(" values (@Hashdata,@FileName,@Type,@Size,@SaveName,@States,@CreateTime,@Note)");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
					new SqlParameter("@Hashdata", DbTool.FixSqlParameter(model.Hashdata))
,					new SqlParameter("@FileName", DbTool.FixSqlParameter(model.FileName))
,					new SqlParameter("@Type", DbTool.FixSqlParameter(model.Type))
,					new SqlParameter("@Size", DbTool.FixSqlParameter(model.Size))
,					new SqlParameter("@SaveName", DbTool.FixSqlParameter(model.SaveName))
,					new SqlParameter("@States", DbTool.FixSqlParameter(model.States))
,					new SqlParameter("@CreateTime", DbTool.FixSqlParameter(model.CreateTime))
,					new SqlParameter("@Note", DbTool.FixSqlParameter(model.Note))
                 };


            return DbTool.ConvertObject<int>(SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters),0);
         
        }
         
         
         #endregion
         
        #region Update
        public int Update(Model.FileInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FileInfo set ");
            strSql.Append("Hashdata=@Hashdata,FileName=@FileName,Type=@Type,Size=@Size,SaveName=@SaveName,States=@States,CreateTime=@CreateTime,Note=@Note ");
            strSql.Append(" where Id=@Id ");
       
            SqlParameter[] parameters = {
					new SqlParameter("@Hashdata", DbTool.FixSqlParameter(model.Hashdata))
,					new SqlParameter("@FileName", DbTool.FixSqlParameter(model.FileName))
,					new SqlParameter("@Type", DbTool.FixSqlParameter(model.Type))
,					new SqlParameter("@Size", DbTool.FixSqlParameter(model.Size))
,					new SqlParameter("@SaveName", DbTool.FixSqlParameter(model.SaveName))
,					new SqlParameter("@States", DbTool.FixSqlParameter(model.States))
,					new SqlParameter("@CreateTime", DbTool.FixSqlParameter(model.CreateTime))
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
            sql.Append("delete from FileInfo where Id = " + id);
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region BindDataReader
        protected void BindDataReader(Model.FileInfoModel model,SqlDataReader dr)
        {

                model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
                model.Hashdata = DbTool.ConvertObject<System.String>(dr["Hashdata"]);
                model.FileName = DbTool.ConvertObject<System.String>(dr["FileName"]);
                model.Type = DbTool.ConvertObject<System.String>(dr["Type"]);
                model.Size = DbTool.ConvertObject<System.String>(dr["Size"]);
                model.SaveName = DbTool.ConvertObject<System.String>(dr["SaveName"]);
                model.States = DbTool.ConvertObject<System.Int32>(dr["States"]);
                model.CreateTime = DbTool.ConvertObject<System.DateTime>(dr["CreateTime"]);
                model.Note = DbTool.ConvertObject<System.String>(dr["Note"]);


        }
         #endregion
         
        #region AutoBindDataReader
        protected Model.FileInfoModel AutoBindDataReader(SqlDataReader dr, params string[] fields)
        {

           var model = new Model.FileInfoModel();
                if (DbTool.HasFields("Id", fields)) model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
                if (DbTool.HasFields("Hashdata", fields)) model.Hashdata = DbTool.ConvertObject<System.String>(dr["Hashdata"]);
                if (DbTool.HasFields("FileName", fields)) model.FileName = DbTool.ConvertObject<System.String>(dr["FileName"]);
                if (DbTool.HasFields("Type", fields)) model.Type = DbTool.ConvertObject<System.String>(dr["Type"]);
                if (DbTool.HasFields("Size", fields)) model.Size = DbTool.ConvertObject<System.String>(dr["Size"]);
                if (DbTool.HasFields("SaveName", fields)) model.SaveName = DbTool.ConvertObject<System.String>(dr["SaveName"]);
                if (DbTool.HasFields("States", fields)) model.States = DbTool.ConvertObject<System.Int32>(dr["States"]);
                if (DbTool.HasFields("CreateTime", fields)) model.CreateTime = DbTool.ConvertObject<System.DateTime>(dr["CreateTime"]);
                if (DbTool.HasFields("Note", fields)) model.Note = DbTool.ConvertObject<System.String>(dr["Note"]);

           return model;

        }
         #endregion
         
        #endregion 

        #region GetList
        public DataTable GetList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from FileInfo where 1=1 ");
            sql.Append(sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region GetModel(id)
        public Model.FileInfoModel GetModel(int Id)
        {

            string sql = "select top 1 * from FileInfo where Id =" + Id;
            Model.FileInfoModel model = new Model.FileInfoModel();
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

        #region GetModel(file)
        public Model.FileInfoModel GetModel(string file)
        {

            string sql = "select top 1 * from FileInfo where SaveName ='" + file+"'";
            Model.FileInfoModel model = new Model.FileInfoModel();
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
        public List<Model.FileInfoModel> GetModelList()
        {

            List<Model.FileInfoModel> result = new List<Model.FileInfoModel>();
            string sql = "select * from FileInfo where 1=1";
            Model.FileInfoModel model = new Model.FileInfoModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            //var fields = DbTool.GetReaderFieldNames(dr);
            while (dr.Read())
            {
                 //model = AutoBindDataReader(dr, fields);
                 model = new Model.FileInfoModel(); 
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
            sql.Append("select count(*) from FileInfo where 1=1 ");
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
            pages.TableName = "FileInfo";
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
           pages.TableName = "FileInfo";
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
           StringBuilder sql = new StringBuilder("select a.* from FileInfo a  where 1=1 " + sqlstr);
           return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
        #endregion
         

        
 
    }
}
