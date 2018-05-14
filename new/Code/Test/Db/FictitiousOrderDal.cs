using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Db
{
    public class FictitiousOrderDal
    {
        public string conn = SqlHelper.ConnectionString;

        
        
        #region Dal Core Functional

        #region Add
        public int Add(Model.FictitiousOrderModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into  [FictitiousOrder]");
            strSql.Append("(OrderCode,OpenId,NickName,HeadImgurl,Texts,FilesId,States,Number,Name,Mob,CreateTime,Title,Age,Tdate,Jp,Jx,PrizeCode,IDCard,DateStamp,Code,HbOrderCode,Ip,Types,Adds,RedPackMoney,MobHome,IpAddress,Province,City,Area,Sources,UpdateTime,Account,Note,AwardId,RedAwardId,IsBack,IsGrant,GrantTime,IsThrottle)");
            strSql.Append(" values (@OrderCode,@OpenId,@NickName,@HeadImgurl,@Texts,@FilesId,@States,@Number,@Name,@Mob,@CreateTime,@Title,@Age,@Tdate,@Jp,@Jx,@PrizeCode,@IDCard,@DateStamp,@Code,@HbOrderCode,@Ip,@Types,@Adds,@RedPackMoney,@MobHome,@IpAddress,@Province,@City,@Area,@Sources,@UpdateTime,@Account,@Note,@AwardId,@RedAwardId,@IsBack,@IsGrant,@GrantTime,@IsThrottle)");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderCode", DbTool.FixSqlParameter(model.OrderCode))
,					new SqlParameter("@OpenId", DbTool.FixSqlParameter(model.OpenId))
,					new SqlParameter("@NickName", DbTool.FixSqlParameter(model.NickName))
,					new SqlParameter("@HeadImgurl", DbTool.FixSqlParameter(model.HeadImgurl))
,					new SqlParameter("@Texts", DbTool.FixSqlParameter(model.Texts))
,					new SqlParameter("@FilesId", DbTool.FixSqlParameter(model.FilesId))
,					new SqlParameter("@States", DbTool.FixSqlParameter(model.States))
,					new SqlParameter("@Number", DbTool.FixSqlParameter(model.Number))
,					new SqlParameter("@Name", DbTool.FixSqlParameter(model.Name))
,					new SqlParameter("@Mob", DbTool.FixSqlParameter(model.Mob))
,					new SqlParameter("@CreateTime", DbTool.FixSqlParameter(model.CreateTime))
,					new SqlParameter("@Title", DbTool.FixSqlParameter(model.Title))
,					new SqlParameter("@Age", DbTool.FixSqlParameter(model.Age))
,					new SqlParameter("@Tdate", DbTool.FixSqlParameter(model.Tdate))
,					new SqlParameter("@Jp", DbTool.FixSqlParameter(model.Jp))
,					new SqlParameter("@Jx", DbTool.FixSqlParameter(model.Jx))
,					new SqlParameter("@PrizeCode", DbTool.FixSqlParameter(model.PrizeCode))
,					new SqlParameter("@IDCard", DbTool.FixSqlParameter(model.IDCard))
,					new SqlParameter("@DateStamp", DbTool.FixSqlParameter(model.DateStamp))
,					new SqlParameter("@Code", DbTool.FixSqlParameter(model.Code))
,					new SqlParameter("@HbOrderCode", DbTool.FixSqlParameter(model.HbOrderCode))
,					new SqlParameter("@Ip", DbTool.FixSqlParameter(model.Ip))
,					new SqlParameter("@Types", DbTool.FixSqlParameter(model.Types))
,					new SqlParameter("@Adds", DbTool.FixSqlParameter(model.Adds))
,					new SqlParameter("@RedPackMoney", DbTool.FixSqlParameter(model.RedPackMoney))
,					new SqlParameter("@MobHome", DbTool.FixSqlParameter(model.MobHome))
,					new SqlParameter("@IpAddress", DbTool.FixSqlParameter(model.IpAddress))
,					new SqlParameter("@Province", DbTool.FixSqlParameter(model.Province))
,					new SqlParameter("@City", DbTool.FixSqlParameter(model.City))
,					new SqlParameter("@Area", DbTool.FixSqlParameter(model.Area))
,					new SqlParameter("@Sources", DbTool.FixSqlParameter(model.Sources))
,					new SqlParameter("@UpdateTime", DbTool.FixSqlParameter(model.UpdateTime))
,					new SqlParameter("@Account", DbTool.FixSqlParameter(model.Account))
,					new SqlParameter("@Note", DbTool.FixSqlParameter(model.Note))
,					new SqlParameter("@AwardId", DbTool.FixSqlParameter(model.AwardId))
,					new SqlParameter("@RedAwardId", DbTool.FixSqlParameter(model.RedAwardId))
,					new SqlParameter("@IsBack", DbTool.FixSqlParameter(model.IsBack))
,					new SqlParameter("@IsGrant", DbTool.FixSqlParameter(model.IsGrant))
,					new SqlParameter("@GrantTime", DbTool.FixSqlParameter(model.GrantTime))
,					new SqlParameter("@IsThrottle", DbTool.FixSqlParameter(model.IsThrottle))
                 };


            return DbTool.ConvertObject<int>(SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters),0);
         
        }
         
         
         #endregion
         
        #region Update
        public int Update(Model.FictitiousOrderModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FictitiousOrder set ");
            strSql.Append("OrderCode=@OrderCode,OpenId=@OpenId,NickName=@NickName,HeadImgurl=@HeadImgurl,Texts=@Texts,FilesId=@FilesId,States=@States,Number=@Number,Name=@Name,Mob=@Mob,CreateTime=@CreateTime,Title=@Title,Age=@Age,Tdate=@Tdate,Jp=@Jp,Jx=@Jx,PrizeCode=@PrizeCode,IDCard=@IDCard,DateStamp=@DateStamp,Code=@Code,HbOrderCode=@HbOrderCode,Ip=@Ip,Types=@Types,Adds=@Adds,RedPackMoney=@RedPackMoney,MobHome=@MobHome,IpAddress=@IpAddress,Province=@Province,City=@City,Area=@Area,Sources=@Sources,UpdateTime=@UpdateTime,Account=@Account,Note=@Note,AwardId=@AwardId,RedAwardId=@RedAwardId,IsBack=@IsBack,IsGrant=@IsGrant,GrantTime=@GrantTime,IsThrottle=@IsThrottle ");
            strSql.Append(" where Id=@Id ");
       
            SqlParameter[] parameters = {
					new SqlParameter("@OrderCode", DbTool.FixSqlParameter(model.OrderCode))
,					new SqlParameter("@OpenId", DbTool.FixSqlParameter(model.OpenId))
,					new SqlParameter("@NickName", DbTool.FixSqlParameter(model.NickName))
,					new SqlParameter("@HeadImgurl", DbTool.FixSqlParameter(model.HeadImgurl))
,					new SqlParameter("@Texts", DbTool.FixSqlParameter(model.Texts))
,					new SqlParameter("@FilesId", DbTool.FixSqlParameter(model.FilesId))
,					new SqlParameter("@States", DbTool.FixSqlParameter(model.States))
,					new SqlParameter("@Number", DbTool.FixSqlParameter(model.Number))
,					new SqlParameter("@Name", DbTool.FixSqlParameter(model.Name))
,					new SqlParameter("@Mob", DbTool.FixSqlParameter(model.Mob))
,					new SqlParameter("@CreateTime", DbTool.FixSqlParameter(model.CreateTime))
,					new SqlParameter("@Title", DbTool.FixSqlParameter(model.Title))
,					new SqlParameter("@Age", DbTool.FixSqlParameter(model.Age))
,					new SqlParameter("@Tdate", DbTool.FixSqlParameter(model.Tdate))
,					new SqlParameter("@Jp", DbTool.FixSqlParameter(model.Jp))
,					new SqlParameter("@Jx", DbTool.FixSqlParameter(model.Jx))
,					new SqlParameter("@PrizeCode", DbTool.FixSqlParameter(model.PrizeCode))
,					new SqlParameter("@IDCard", DbTool.FixSqlParameter(model.IDCard))
,					new SqlParameter("@DateStamp", DbTool.FixSqlParameter(model.DateStamp))
,					new SqlParameter("@Code", DbTool.FixSqlParameter(model.Code))
,					new SqlParameter("@HbOrderCode", DbTool.FixSqlParameter(model.HbOrderCode))
,					new SqlParameter("@Ip", DbTool.FixSqlParameter(model.Ip))
,					new SqlParameter("@Types", DbTool.FixSqlParameter(model.Types))
,					new SqlParameter("@Adds", DbTool.FixSqlParameter(model.Adds))
,					new SqlParameter("@RedPackMoney", DbTool.FixSqlParameter(model.RedPackMoney))
,					new SqlParameter("@MobHome", DbTool.FixSqlParameter(model.MobHome))
,					new SqlParameter("@IpAddress", DbTool.FixSqlParameter(model.IpAddress))
,					new SqlParameter("@Province", DbTool.FixSqlParameter(model.Province))
,					new SqlParameter("@City", DbTool.FixSqlParameter(model.City))
,					new SqlParameter("@Area", DbTool.FixSqlParameter(model.Area))
,					new SqlParameter("@Sources", DbTool.FixSqlParameter(model.Sources))
,					new SqlParameter("@UpdateTime", DbTool.FixSqlParameter(model.UpdateTime))
,					new SqlParameter("@Account", DbTool.FixSqlParameter(model.Account))
,					new SqlParameter("@Note", DbTool.FixSqlParameter(model.Note))
,					new SqlParameter("@AwardId", DbTool.FixSqlParameter(model.AwardId))
,					new SqlParameter("@RedAwardId", DbTool.FixSqlParameter(model.RedAwardId))
,					new SqlParameter("@IsBack", DbTool.FixSqlParameter(model.IsBack))
,					new SqlParameter("@IsGrant", DbTool.FixSqlParameter(model.IsGrant))
,					new SqlParameter("@GrantTime", DbTool.FixSqlParameter(model.GrantTime))
,					new SqlParameter("@IsThrottle", DbTool.FixSqlParameter(model.IsThrottle))
,					new SqlParameter("@Id", model.Id)
                 };


            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }
         #endregion
         
        #region Delete
        public int Del(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from FictitiousOrder where Id = " + id);
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region BindDataReader
        protected void BindDataReader(Model.FictitiousOrderModel model,SqlDataReader dr)
        {

                model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
                model.OrderCode = DbTool.ConvertObject<System.String>(dr["OrderCode"]);
                model.OpenId = DbTool.ConvertObject<System.String>(dr["OpenId"]);
                model.NickName = DbTool.ConvertObject<System.String>(dr["NickName"]);
                model.HeadImgurl = DbTool.ConvertObject<System.String>(dr["HeadImgurl"]);
                model.Texts = DbTool.ConvertObject<System.String>(dr["Texts"]);
                model.FilesId = DbTool.ConvertObject<System.Int32>(dr["FilesId"]);
                model.States = DbTool.ConvertObject<System.Int32>(dr["States"]);
                model.Number = DbTool.ConvertObject<System.Int32>(dr["Number"]);
                model.Name = DbTool.ConvertObject<System.String>(dr["Name"]);
                model.Mob = DbTool.ConvertObject<System.String>(dr["Mob"]);
                model.CreateTime = DbTool.ConvertObject<System.DateTime>(dr["CreateTime"]);
                model.Title = DbTool.ConvertObject<System.String>(dr["Title"]);
                model.Age = DbTool.ConvertObject<System.String>(dr["Age"]);
                model.Tdate = DbTool.ConvertObject<System.String>(dr["Tdate"]);
                model.Jp = DbTool.ConvertObject<System.String>(dr["Jp"]);
                model.Jx = DbTool.ConvertObject<System.String>(dr["Jx"]);
                model.PrizeCode = DbTool.ConvertObject<System.String>(dr["PrizeCode"]);
                model.IDCard = DbTool.ConvertObject<System.String>(dr["IDCard"]);
                model.DateStamp = DbTool.ConvertObject<System.String>(dr["DateStamp"]);
                model.Code = DbTool.ConvertObject<System.String>(dr["Code"]);
                model.HbOrderCode = DbTool.ConvertObject<System.String>(dr["HbOrderCode"]);
                model.Ip = DbTool.ConvertObject<System.String>(dr["Ip"]);
                model.Types = DbTool.ConvertObject<System.Int32>(dr["Types"]);
                model.Adds = DbTool.ConvertObject<System.String>(dr["Adds"]);
                model.RedPackMoney = DbTool.ConvertObject<System.Int32>(dr["RedPackMoney"]);
                model.MobHome = DbTool.ConvertObject<System.String>(dr["MobHome"]);
                model.IpAddress = DbTool.ConvertObject<System.String>(dr["IpAddress"]);
                model.Province = DbTool.ConvertObject<System.String>(dr["Province"]);
                model.City = DbTool.ConvertObject<System.String>(dr["City"]);
                model.Area = DbTool.ConvertObject<System.String>(dr["Area"]);
                model.Sources = DbTool.ConvertObject<System.String>(dr["Sources"]);
                model.UpdateTime = DbTool.ConvertObject<System.DateTime>(dr["UpdateTime"]);
                model.Account = DbTool.ConvertObject<System.String>(dr["Account"]);
                model.Note = DbTool.ConvertObject<System.String>(dr["Note"]);
                model.AwardId = DbTool.ConvertObject<System.Int32>(dr["AwardId"]);
                model.RedAwardId = DbTool.ConvertObject<System.Int32>(dr["RedAwardId"]);
                model.IsBack = DbTool.ConvertObject<System.Int32>(dr["IsBack"]);
                model.IsGrant = DbTool.ConvertObject<System.Int32>(dr["IsGrant"]);
                model.GrantTime = DbTool.ConvertObject<System.DateTime>(dr["GrantTime"]);
                model.IsThrottle = DbTool.ConvertObject<System.Int32>(dr["IsThrottle"]);


        }
         #endregion
         
        #region AutoBindDataReader
        protected Model.FictitiousOrderModel AutoBindDataReader(SqlDataReader dr, params string[] fields)
        {

           var model = new Model.FictitiousOrderModel();
                if (DbTool.HasFields("Id", fields)) model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
                if (DbTool.HasFields("OrderCode", fields)) model.OrderCode = DbTool.ConvertObject<System.String>(dr["OrderCode"]);
                if (DbTool.HasFields("OpenId", fields)) model.OpenId = DbTool.ConvertObject<System.String>(dr["OpenId"]);
                if (DbTool.HasFields("NickName", fields)) model.NickName = DbTool.ConvertObject<System.String>(dr["NickName"]);
                if (DbTool.HasFields("HeadImgurl", fields)) model.HeadImgurl = DbTool.ConvertObject<System.String>(dr["HeadImgurl"]);
                if (DbTool.HasFields("Texts", fields)) model.Texts = DbTool.ConvertObject<System.String>(dr["Texts"]);
                if (DbTool.HasFields("FilesId", fields)) model.FilesId = DbTool.ConvertObject<System.Int32>(dr["FilesId"]);
                if (DbTool.HasFields("States", fields)) model.States = DbTool.ConvertObject<System.Int32>(dr["States"]);
                if (DbTool.HasFields("Number", fields)) model.Number = DbTool.ConvertObject<System.Int32>(dr["Number"]);
                if (DbTool.HasFields("Name", fields)) model.Name = DbTool.ConvertObject<System.String>(dr["Name"]);
                if (DbTool.HasFields("Mob", fields)) model.Mob = DbTool.ConvertObject<System.String>(dr["Mob"]);
                if (DbTool.HasFields("CreateTime", fields)) model.CreateTime = DbTool.ConvertObject<System.DateTime>(dr["CreateTime"]);
                if (DbTool.HasFields("Title", fields)) model.Title = DbTool.ConvertObject<System.String>(dr["Title"]);
                if (DbTool.HasFields("Age", fields)) model.Age = DbTool.ConvertObject<System.String>(dr["Age"]);
                if (DbTool.HasFields("Tdate", fields)) model.Tdate = DbTool.ConvertObject<System.String>(dr["Tdate"]);
                if (DbTool.HasFields("Jp", fields)) model.Jp = DbTool.ConvertObject<System.String>(dr["Jp"]);
                if (DbTool.HasFields("Jx", fields)) model.Jx = DbTool.ConvertObject<System.String>(dr["Jx"]);
                if (DbTool.HasFields("PrizeCode", fields)) model.PrizeCode = DbTool.ConvertObject<System.String>(dr["PrizeCode"]);
                if (DbTool.HasFields("IDCard", fields)) model.IDCard = DbTool.ConvertObject<System.String>(dr["IDCard"]);
                if (DbTool.HasFields("DateStamp", fields)) model.DateStamp = DbTool.ConvertObject<System.String>(dr["DateStamp"]);
                if (DbTool.HasFields("Code", fields)) model.Code = DbTool.ConvertObject<System.String>(dr["Code"]);
                if (DbTool.HasFields("HbOrderCode", fields)) model.HbOrderCode = DbTool.ConvertObject<System.String>(dr["HbOrderCode"]);
                if (DbTool.HasFields("Ip", fields)) model.Ip = DbTool.ConvertObject<System.String>(dr["Ip"]);
                if (DbTool.HasFields("Types", fields)) model.Types = DbTool.ConvertObject<System.Int32>(dr["Types"]);
                if (DbTool.HasFields("Adds", fields)) model.Adds = DbTool.ConvertObject<System.String>(dr["Adds"]);
                if (DbTool.HasFields("RedPackMoney", fields)) model.RedPackMoney = DbTool.ConvertObject<System.Int32>(dr["RedPackMoney"]);
                if (DbTool.HasFields("MobHome", fields)) model.MobHome = DbTool.ConvertObject<System.String>(dr["MobHome"]);
                if (DbTool.HasFields("IpAddress", fields)) model.IpAddress = DbTool.ConvertObject<System.String>(dr["IpAddress"]);
                if (DbTool.HasFields("Province", fields)) model.Province = DbTool.ConvertObject<System.String>(dr["Province"]);
                if (DbTool.HasFields("City", fields)) model.City = DbTool.ConvertObject<System.String>(dr["City"]);
                if (DbTool.HasFields("Area", fields)) model.Area = DbTool.ConvertObject<System.String>(dr["Area"]);
                if (DbTool.HasFields("Sources", fields)) model.Sources = DbTool.ConvertObject<System.String>(dr["Sources"]);
                if (DbTool.HasFields("UpdateTime", fields)) model.UpdateTime = DbTool.ConvertObject<System.DateTime>(dr["UpdateTime"]);
                if (DbTool.HasFields("Account", fields)) model.Account = DbTool.ConvertObject<System.String>(dr["Account"]);
                if (DbTool.HasFields("Note", fields)) model.Note = DbTool.ConvertObject<System.String>(dr["Note"]);
                if (DbTool.HasFields("AwardId", fields)) model.AwardId = DbTool.ConvertObject<System.Int32>(dr["AwardId"]);
                if (DbTool.HasFields("RedAwardId", fields)) model.RedAwardId = DbTool.ConvertObject<System.Int32>(dr["RedAwardId"]);
                if (DbTool.HasFields("IsBack", fields)) model.IsBack = DbTool.ConvertObject<System.Int32>(dr["IsBack"]);
                if (DbTool.HasFields("IsGrant", fields)) model.IsGrant = DbTool.ConvertObject<System.Int32>(dr["IsGrant"]);
                if (DbTool.HasFields("GrantTime", fields)) model.GrantTime = DbTool.ConvertObject<System.DateTime>(dr["GrantTime"]);
                if (DbTool.HasFields("IsThrottle", fields)) model.IsThrottle = DbTool.ConvertObject<System.Int32>(dr["IsThrottle"]);

           return model;

        }
         #endregion
         
        #endregion 

        #region GetList
        public DataTable GetList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from FictitiousOrder where 1=1 ");
            sql.Append(sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region GetModel
        public Model.FictitiousOrderModel GetModel(int Id)
        {

            string sql = "select top 1 * from FictitiousOrder where Id =" + Id;
            Model.FictitiousOrderModel model = new Model.FictitiousOrderModel();
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
        public List<Model.FictitiousOrderModel> GetModelList()
        {

            List<Model.FictitiousOrderModel> result = new List<Model.FictitiousOrderModel>();
            string sql = "select * from FictitiousOrder where 1=1";
            Model.FictitiousOrderModel model = new Model.FictitiousOrderModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            //var fields = DbTool.GetReaderFieldNames(dr);
            while (dr.Read())
            {
                 //model = AutoBindDataReader(dr, fields);
                 model = new Model.FictitiousOrderModel(); 
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
            sql.Append("select count(*) from FictitiousOrder where 1=1 ");
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
            pages.TableName = "FictitiousOrder";
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
           pages.TableName = "FictitiousOrder";
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
        public DataTable GetExcelList(string sqlstr)
        {
           StringBuilder sql = new StringBuilder("select a.* from FictitiousOrder a  where 1=1 " + sqlstr);
           return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
        #endregion
         

        
 
    }
}
