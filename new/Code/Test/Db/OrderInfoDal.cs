using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Db
{
    public class OrderInfoDal
    {
        public string conn = SqlHelper.ConnectionString;

        
        
        #region Dal Core Functional

        #region Add
        public int Add(Model.OrderInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into  [OrderInfo]");
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
        public int Update(Model.OrderInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update OrderInfo set ");
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
            sql.Append("delete from OrderInfo where Id = " + id);
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region BindDataReader
        protected void BindDataReader(Model.OrderInfoModel model,SqlDataReader dr)
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
        protected Model.OrderInfoModel AutoBindDataReader(SqlDataReader dr, params string[] fields)
        {

           var model = new Model.OrderInfoModel();
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
            sql.Append("select * from OrderInfo where 1=1 ");
            sql.Append(sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region GetModel
        public Model.OrderInfoModel GetModel(int Id)
        {

            string sql = "select top 1 * from OrderInfo where Id =" + Id;
            Model.OrderInfoModel model = new Model.OrderInfoModel();
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
        public List<Model.OrderInfoModel> GetModelList()
        {

            List<Model.OrderInfoModel> result = new List<Model.OrderInfoModel>();
            string sql = "select * from OrderInfo where 1=1";
            Model.OrderInfoModel model = new Model.OrderInfoModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            //var fields = DbTool.GetReaderFieldNames(dr);
            while (dr.Read())
            {
                 //model = AutoBindDataReader(dr, fields);
                 model = new Model.OrderInfoModel(); 
                 BindDataReader(model, dr);
                 result.Add(model);
            }
            dr.Close();
            return result;
        }
         #endregion

        #region 自定义方法

        #region Add
        public int AddOrderInfo_UpdatePassCodeInfo_AddOrderLog(Model.OrderInfoModel model, Model.PassCodeInfoModel pcdel, Model.OrderLogModel mdlog, int AwardId, int RedAwardId, ref int OrderId)
        {
            int rtn = 0;
            int Num = 1;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {

                        #region 添加订单
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("insert into  [OrderInfo]");
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
                        OrderId = DbTool.ConvertObject<System.Int32>(SqlHelper.ExecuteScalar(trans, CommandType.Text, strSql.ToString(), parameters));
                        if (OrderId > 0) { rtn += 1; }
                        #endregion

                        #region 统计奖项中奖详情订单
                        if (AwardId > 0 || RedAwardId > 0)
                        {
                            Num++;

                            StringBuilder strSql4 = new StringBuilder();
                            strSql4.Append("update AwardsStatistics set ");
                            strSql4.Append("DateStamp='" + DateTime.Now.ToShortDateString() + "',YesterdayTotal=TodayTotal,TodayTotal=0,UpdateTime='" + DateTime.Now + "'");
                            strSql4.Append(" where DateStamp<>'" + DateTime.Now.ToShortDateString() + "';");
                            if (AwardId > 0)
                            {
                                strSql4.Append("update AwardsStatistics set ");
                                strSql4.Append("TodayTotal=TodayTotal+1,AllTotal=AllTotal+1");
                                strSql4.Append(" where Id=" + AwardId + " and AwardsType=1;");
                            }
                            if (RedAwardId > 0)
                            {
                                strSql4.Append("update AwardsStatistics set ");
                                strSql4.Append("TodayTotal=TodayTotal+1,AllTotal=AllTotal+1");
                                strSql4.Append(" where Id=" + RedAwardId + " and AwardsType=2;");
                            }
                            SqlParameter[] parameters4 = { };

                            if (SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql4.ToString(), parameters4) > 0)
                            {
                                rtn += 1;
                            }
                        }
                        #endregion

                        #region 修改激活码订单
                        if (pcdel != null)
                        {
                            Num++;

                            StringBuilder strSql3 = new StringBuilder();
                            strSql3.Append("update PassCodeInfo set StatusId=@StatusId,OpenId=@OpenId,Mob=@Mob,ActiveTime=@ActiveTime");
                            strSql3.Append(" where id=@id and StatusId=0");
                            SqlParameter[] parameters3 = {
					            new SqlParameter("@StatusId", pcdel.StatusId),
                                new SqlParameter("@ActiveTime", pcdel.ActiveTime),
                                new SqlParameter("@OpenId", pcdel.OpenId),
					            new SqlParameter("@mob", pcdel.Mob),
					            new SqlParameter("@id", pcdel.Id)				           
                                };
                            rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql3.ToString(), parameters3);
                        }
                        #endregion

                        #region 添加订单日志
                        if (mdlog != null)
                        {
                            Num++;

                            StringBuilder strSql2 = new StringBuilder();
                            strSql2.Append(" insert into OrderLog(Oid,OrderCode,Mob,UpTime,LStatus,Status,Notes)");
                            strSql2.Append(" values(@Oid,@OrderCode,@Mob,@UpTime,@LStatus,@Status,@Notes)");
                            SqlParameter[] parameters2 = {	 
                                new SqlParameter("@Oid", mdlog.OId),
                                new SqlParameter("@OrderCode", mdlog.OrderCode),
                                new SqlParameter("@Mob", mdlog.Mob),
                                new SqlParameter("@UpTime", mdlog.UpTime),
                                new SqlParameter("@LStatus", mdlog.LStatus),
                                new SqlParameter("@Status", mdlog.Status),
                               new SqlParameter("@Notes", mdlog.Notes) 
                             };
                            rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql2.ToString(), parameters2);
                        }
                        #endregion

                        if (rtn == Num)
                        {
                            trans.Commit();
                            return rtn;
                        }
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        rtn = 0;
                    }
                }
            }
            return 0;

        }

        #endregion

        #region Update

        public int UpdateInfo(Model.OrderInfoModel model, Model.OrderLogModel mdlog)
        {
            int rtn = 0;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("update OrderInfo set ");
                        strSql.Append("Name=@Name,Types=@Types,Adds=@Adds,States=@States,PrizeCode=@PrizeCode,Mob=@Mob");
                        strSql.Append(" where Id=@Id ");

                        SqlParameter[] parameters = {
                                        new SqlParameter("@Name", DbTool.FixSqlParameter(model.Name))
                        ,					new SqlParameter("@Types", DbTool.FixSqlParameter(model.Types))
                        ,					new SqlParameter("@Adds", DbTool.FixSqlParameter(model.Adds))
                        ,					new SqlParameter("@States", DbTool.FixSqlParameter(model.States))
                        ,					new SqlParameter("@PrizeCode", DbTool.FixSqlParameter(model.PrizeCode))
                        ,					new SqlParameter("@Mob", DbTool.FixSqlParameter(model.Mob))
                        ,					new SqlParameter("@Id", model.Id)
                                        };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);

                        StringBuilder strSql2 = new StringBuilder();
                        strSql2.Append(" insert into OrderLog(Oid,OrderCode,Mob,UpTime,LStatus,Status,Notes)");
                        strSql2.Append(" values(@Oid,@OrderCode,@Mob,@UpTime,@LStatus,@Status,@Notes)");
                        SqlParameter[] parameters2 = {	 
                        new SqlParameter("@Oid", mdlog.OId),
                        new SqlParameter("@OrderCode", mdlog.OrderCode),
                        new SqlParameter("@Mob", mdlog.Mob),
                        new SqlParameter("@UpTime", mdlog.UpTime),
                        new SqlParameter("@LStatus", mdlog.LStatus),
                        new SqlParameter("@Status", mdlog.Status),
                        new SqlParameter("@Notes", mdlog.Notes) 
                        };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql2.ToString(), parameters2);
                        if (rtn > 1)
                        {
                            trans.Commit();
                            return rtn;
                        }
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        rtn = 0;
                    }
                }
            }
            return 0;

        }

        public int UpdateFiles(Model.OrderInfoModel model, Model.OrderLogModel mdlog)
        {
            int rtn = 0;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("update OrderInfo set ");
                        strSql.Append("FilesId=@FilesId,States=@States");
                        strSql.Append(" where Id=@Id ");
                        SqlParameter[] parameters = {
                                        new SqlParameter("@FilesId", DbTool.FixSqlParameter(model.FilesId)) 
                        ,					new SqlParameter("@States", DbTool.FixSqlParameter(model.States)) 
                        ,					new SqlParameter("@Id", model.Id)
                                        };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);

                        StringBuilder strSql2 = new StringBuilder();
                        strSql2.Append(" insert into OrderLog(Oid,OrderCode,Mob,UpTime,LStatus,Status,Notes)");
                        strSql2.Append(" values(@Oid,@OrderCode,@Mob,@UpTime,@LStatus,@Status,@Notes)");
                        SqlParameter[] parameters2 = {	 
                        new SqlParameter("@Oid", mdlog.OId),
                        new SqlParameter("@OrderCode", mdlog.OrderCode),
                        new SqlParameter("@Mob", mdlog.Mob),
                        new SqlParameter("@UpTime", mdlog.UpTime),
                        new SqlParameter("@LStatus", mdlog.LStatus),
                        new SqlParameter("@Status", mdlog.Status),
                        new SqlParameter("@Notes", mdlog.Notes) 
                        };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql2.ToString(), parameters2);
                        if (rtn > 1)
                        {
                            trans.Commit();
                            return rtn;
                        }
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        rtn = 0;
                    }
                }
            }
            return 0;

        }

        public int UpdateTypes(Model.OrderInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update OrderInfo set ");
            strSql.Append("Types=@Types");
            strSql.Append(" where Id=@Id ");

            SqlParameter[] parameters = {
				new SqlParameter("@Types", DbTool.FixSqlParameter(model.Types))
,					new SqlParameter("@Id", model.Id)
                };


            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }

        public int Update(List<Model.OrderInfoModel> modellist)
        {
            if (modellist == null || modellist.Count == 0)
                return 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("begin tran ");
            strSql.Append("begin try ");
            foreach (Model.OrderInfoModel model in modellist)
            {
                strSql.Append(@" UPDATE [OrderInfo] SET [States]=-1 WHERE Id=");
                strSql.Append(model.Id + ";");
            }
            strSql.Append("COMMIT TRAN ");
            strSql.Append("SELECT 1 ");
            strSql.Append("end try ");
            strSql.Append("BEGIN CATCH ");
            strSql.Append("ROLLBACK TRAN ");
            strSql.Append("SELECT 0 ");
            strSql.Append("end catch ");

            try
            {
                return Convert.ToInt32(SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), new SqlParameter[] { }));
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int EditOrder(Model.OrderInfoModel model, Model.Operation_LogModel mdlog)
        {
            int rtn = 0;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();

                        strSql.Append(" update OrderInfo set States=@States,PrizeCode=@PrizeCode,UpdateTime=@UpdateTime,Account=@Account,Note=@Note where Id=@Id");
                        SqlParameter[] parameters = {	 
                            new SqlParameter("@States", model.States), 
                            new SqlParameter("@PrizeCode", model.PrizeCode), 
                            new SqlParameter("@UpdateTime", model.UpdateTime),
                            new SqlParameter("@Account", model.Account),
                            new SqlParameter("@Note", model.Note),
                            new SqlParameter("@Id", model.Id) 
                   
                            };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);

                        StringBuilder strSql2 = new StringBuilder();
                        strSql2.Append("insert into  [Operation_Log]");
                        strSql2.Append("(Mobile,OrderCode,LStatus,Status,OperationType,Description,CreateTime,UpdateTime,UserName,Remark,HideContent)");
                        strSql2.Append(" values (@Mobile,@OrderCode,@LStatus,@Status,@OperationType,@Description,@CreateTime,@UpdateTime,@UserName,@Remark,@HideContent)");
                        strSql2.Append(";select SCOPE_IDENTITY()");
                        SqlParameter[] parameters2 = {
				        new SqlParameter("@Mobile", DbTool.FixSqlParameter(mdlog.Mobile))
                        ,					new SqlParameter("@OrderCode", DbTool.FixSqlParameter(mdlog.OrderCode))
                        ,					new SqlParameter("@LStatus", DbTool.FixSqlParameter(mdlog.LStatus))
                        ,					new SqlParameter("@Status", DbTool.FixSqlParameter(mdlog.Status))
                        ,					new SqlParameter("@OperationType", DbTool.FixSqlParameter(mdlog.OperationType))
                        ,					new SqlParameter("@Description", DbTool.FixSqlParameter(mdlog.Description))
                        ,					new SqlParameter("@CreateTime", DbTool.FixSqlParameter(mdlog.CreateTime))
                        ,					new SqlParameter("@UpdateTime", DbTool.FixSqlParameter(mdlog.UpdateTime))
                        ,					new SqlParameter("@UserName", DbTool.FixSqlParameter(mdlog.UserName))
                        ,					new SqlParameter("@Remark", DbTool.FixSqlParameter(mdlog.Remark))
                        ,					new SqlParameter("@HideContent", DbTool.FixSqlParameter(mdlog.HideContent))
                        };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql2.ToString(), parameters2);
                        if (rtn > 1)
                        {
                            trans.Commit();
                            return rtn;
                        }
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        rtn = 0;
                    }
                }
            }
            return 0;

        }

        public int EditDelayedOrder(Model.OrderInfoModel model, Model.Operation_LogModel mdlog, Model.DelayedAwardsModel DelayedModel)
        {
            int rtn = 0;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();

                        strSql.Append(" update OrderInfo set States=@States,PrizeCode=@PrizeCode,UpdateTime=@UpdateTime,Account=@Account,IsGrant=@IsGrant,GrantTime=@GrantTime where Id=@Id");
                        SqlParameter[] parameters = {	 
                            new SqlParameter("@States", model.States), 
                            new SqlParameter("@PrizeCode", model.PrizeCode), 
                            new SqlParameter("@UpdateTime", model.UpdateTime),
                            new SqlParameter("@Account", model.Account),
                            new SqlParameter("@GrantTime", model.GrantTime),
                            new SqlParameter("@IsGrant", model.IsGrant),
                            new SqlParameter("@Id", model.Id) 
                   
                            };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);

                        StringBuilder strSql2 = new StringBuilder();
                        strSql2.Append("insert into  [Operation_Log]");
                        strSql2.Append("(Mobile,OrderCode,LStatus,Status,OperationType,Description,CreateTime,UpdateTime,UserName,Remark,HideContent)");
                        strSql2.Append(" values (@Mobile,@OrderCode,@LStatus,@Status,@OperationType,@Description,@CreateTime,@UpdateTime,@UserName,@Remark,@HideContent)");
                        strSql2.Append(";select SCOPE_IDENTITY()");
                        SqlParameter[] parameters2 = {
				        new SqlParameter("@Mobile", DbTool.FixSqlParameter(mdlog.Mobile))
                        ,					new SqlParameter("@OrderCode", DbTool.FixSqlParameter(mdlog.OrderCode))
                        ,					new SqlParameter("@LStatus", DbTool.FixSqlParameter(mdlog.LStatus))
                        ,					new SqlParameter("@Status", DbTool.FixSqlParameter(mdlog.Status))
                        ,					new SqlParameter("@OperationType", DbTool.FixSqlParameter(mdlog.OperationType))
                        ,					new SqlParameter("@Description", DbTool.FixSqlParameter(mdlog.Description))
                        ,					new SqlParameter("@CreateTime", DbTool.FixSqlParameter(mdlog.CreateTime))
                        ,					new SqlParameter("@UpdateTime", DbTool.FixSqlParameter(mdlog.UpdateTime))
                        ,					new SqlParameter("@UserName", DbTool.FixSqlParameter(mdlog.UserName))
                        ,					new SqlParameter("@Remark", DbTool.FixSqlParameter(mdlog.Remark))
                        ,					new SqlParameter("@HideContent", DbTool.FixSqlParameter(mdlog.HideContent))
                        };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql2.ToString(), parameters2);

                        StringBuilder strSql3 = new StringBuilder();
                        strSql3.Append("update DelayedAwards set ");
                        strSql3.Append("StatusId=@StatusId,UpdateTime=@UpdateTime");
                        strSql3.Append(" where Id=@Id ");
                        SqlParameter[] parameters3 = {	 
                            new SqlParameter("@StatusId", DelayedModel.StatusId),
                            new SqlParameter("@UpdateTime", DelayedModel.UpdateTime),
                            new SqlParameter("@Id", DelayedModel.Id) 
                        };

                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql3.ToString(), parameters3);
                        if (rtn > 2)
                        {
                            trans.Commit();
                            return rtn;
                        }
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        rtn = 0;
                    }
                }
            }
            return 0;

        }

        public int EditAndDelayedOrder(Model.OrderInfoModel model, Model.Operation_LogModel mdlog, Model.DelayedAwardsModel DelayedAwardsModel)
        {
            int rtn = 0;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();

                        strSql.Append(" update OrderInfo set States=@States,PrizeCode=@PrizeCode,UpdateTime=@UpdateTime,Account=@Account,Note=@Note,IsGrant=@IsGrant where Id=@Id");
                        SqlParameter[] parameters = {	 
                            new SqlParameter("@States", model.States), 
                            new SqlParameter("@PrizeCode", model.PrizeCode), 
                            new SqlParameter("@UpdateTime", model.UpdateTime),
                            new SqlParameter("@Account", model.Account),
                            new SqlParameter("@Note", model.Note),
                            new SqlParameter("@IsGrant", model.IsGrant), 
                            new SqlParameter("@Id", model.Id) 
                   
                            };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);

                        StringBuilder strSql2 = new StringBuilder();
                        strSql2.Append("insert into  [Operation_Log]");
                        strSql2.Append("(Mobile,OrderCode,LStatus,Status,OperationType,Description,CreateTime,UpdateTime,UserName,Remark,HideContent)");
                        strSql2.Append(" values (@Mobile,@OrderCode,@LStatus,@Status,@OperationType,@Description,@CreateTime,@UpdateTime,@UserName,@Remark,@HideContent)");
                        strSql2.Append(";select SCOPE_IDENTITY()");
                        SqlParameter[] parameters2 = {
				        new SqlParameter("@Mobile", DbTool.FixSqlParameter(mdlog.Mobile))
                        ,					new SqlParameter("@OrderCode", DbTool.FixSqlParameter(mdlog.OrderCode))
                        ,					new SqlParameter("@LStatus", DbTool.FixSqlParameter(mdlog.LStatus))
                        ,					new SqlParameter("@Status", DbTool.FixSqlParameter(mdlog.Status))
                        ,					new SqlParameter("@OperationType", DbTool.FixSqlParameter(mdlog.OperationType))
                        ,					new SqlParameter("@Description", DbTool.FixSqlParameter(mdlog.Description))
                        ,					new SqlParameter("@CreateTime", DbTool.FixSqlParameter(mdlog.CreateTime))
                        ,					new SqlParameter("@UpdateTime", DbTool.FixSqlParameter(mdlog.UpdateTime))
                        ,					new SqlParameter("@UserName", DbTool.FixSqlParameter(mdlog.UserName))
                        ,					new SqlParameter("@Remark", DbTool.FixSqlParameter(mdlog.Remark))
                        ,					new SqlParameter("@HideContent", DbTool.FixSqlParameter(mdlog.HideContent))
                        };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql2.ToString(), parameters2);

                        StringBuilder strSql3 = new StringBuilder();
                        strSql3.Append("insert into  [DelayedAwards]");
                        strSql3.Append("(OrderId,StatusId,CreateTime,DelayedTime,Remark)");
                        strSql3.Append(" values (@OrderId,@StatusId,@CreateTime,@DelayedTime,@Remark)");
                        strSql3.Append(";select SCOPE_IDENTITY()");
                        SqlParameter[] parameters3 = {
					new SqlParameter("@OrderId", DbTool.FixSqlParameter(DelayedAwardsModel.OrderId)) 
,					new SqlParameter("@StatusId", DbTool.FixSqlParameter(DelayedAwardsModel.StatusId))
,					new SqlParameter("@CreateTime", DbTool.FixSqlParameter(DelayedAwardsModel.CreateTime))
,					new SqlParameter("@DelayedTime", DbTool.FixSqlParameter(DelayedAwardsModel.DelayedTime))
,					new SqlParameter("@Remark", DbTool.FixSqlParameter(DelayedAwardsModel.Remark))
                 };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql3.ToString(), parameters3);

                        if (rtn > 2)
                        {
                            trans.Commit();
                            return rtn;
                        }
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        rtn = 0;
                    }
                }
            }
            return 0;

        }

        public int UpdateFail(Model.OrderInfoModel model, Model.Operation_LogModel mdlog)
        {
            int rtn = 0;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();

                        strSql.Append(" update OrderInfo set States=@States,UpdateTime=@UpdateTime,Account=@Account,Note=@Note,IsBack=@IsBack where Id=@Id");
                        SqlParameter[] parameters = {	 
                            new SqlParameter("@States", model.States),  
                            new SqlParameter("@UpdateTime", model.UpdateTime),
                            new SqlParameter("@Account", model.Account),
                            new SqlParameter("@Note", model.Note),
                            new SqlParameter("@IsBack", model.IsBack),
                            new SqlParameter("@Id", model.Id) 
                   
                            };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);

                        StringBuilder strSql2 = new StringBuilder();
                        strSql2.Append("insert into  [Operation_Log]");
                        strSql2.Append("(Mobile,OrderCode,LStatus,Status,OperationType,Description,CreateTime,UpdateTime,UserName,Remark,HideContent)");
                        strSql2.Append(" values (@Mobile,@OrderCode,@LStatus,@Status,@OperationType,@Description,@CreateTime,@UpdateTime,@UserName,@Remark,@HideContent)");
                        strSql2.Append(";select SCOPE_IDENTITY()");
                        SqlParameter[] parameters2 = {
				        new SqlParameter("@Mobile", DbTool.FixSqlParameter(mdlog.Mobile))
                        ,					new SqlParameter("@OrderCode", DbTool.FixSqlParameter(mdlog.OrderCode))
                        ,					new SqlParameter("@LStatus", DbTool.FixSqlParameter(mdlog.LStatus))
                        ,					new SqlParameter("@Status", DbTool.FixSqlParameter(mdlog.Status))
                        ,					new SqlParameter("@OperationType", DbTool.FixSqlParameter(mdlog.OperationType))
                        ,					new SqlParameter("@Description", DbTool.FixSqlParameter(mdlog.Description))
                        ,					new SqlParameter("@CreateTime", DbTool.FixSqlParameter(mdlog.CreateTime))
                        ,					new SqlParameter("@UpdateTime", DbTool.FixSqlParameter(mdlog.UpdateTime))
                        ,					new SqlParameter("@UserName", DbTool.FixSqlParameter(mdlog.UserName))
                        ,					new SqlParameter("@Remark", DbTool.FixSqlParameter(mdlog.Remark))
                        ,					new SqlParameter("@HideContent", DbTool.FixSqlParameter(mdlog.HideContent))
                        };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql2.ToString(), parameters2);

                        #region 作废回库
                        StringBuilder BackSql = new StringBuilder();
                        BackSql.AppendFormat(" update AwardsStatistics set BackTotal=BackTotal+1 where (AwardsType=1 and AwardsId={0}) or (AwardsType=2 and AwardsId={1})", model.AwardId, model.RedAwardId);
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, BackSql.ToString());
                        #endregion

                        if (rtn > 2)
                        {
                            trans.Commit();
                            return rtn;
                        }
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        rtn = 0;
                    }
                }
            }
            return 0;

        }

        public int Recovery(Model.OrderInfoModel model, Model.Operation_LogModel mdlog)
        {
            int rtn = 0;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();

                        strSql.Append(" update OrderInfo set States=@States,UpdateTime=@UpdateTime,Account=@Account,Note=@Note where Id=@Id");
                        SqlParameter[] parameters = {	 
                            new SqlParameter("@States", model.States),  
                            new SqlParameter("@UpdateTime", model.UpdateTime),
                            new SqlParameter("@Account", model.Account),
                            new SqlParameter("@Note", model.Note),
                            new SqlParameter("@Id", model.Id) 
                   
                            };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);

                        StringBuilder strSql2 = new StringBuilder();
                        strSql2.Append("insert into  [Operation_Log]");
                        strSql2.Append("(Mobile,OrderCode,LStatus,Status,OperationType,Description,CreateTime,UpdateTime,UserName,Remark,HideContent)");
                        strSql2.Append(" values (@Mobile,@OrderCode,@LStatus,@Status,@OperationType,@Description,@CreateTime,@UpdateTime,@UserName,@Remark,@HideContent)");
                        strSql2.Append(";select SCOPE_IDENTITY()");
                        SqlParameter[] parameters2 = {
				        new SqlParameter("@Mobile", DbTool.FixSqlParameter(mdlog.Mobile))
                        ,					new SqlParameter("@OrderCode", DbTool.FixSqlParameter(mdlog.OrderCode))
                        ,					new SqlParameter("@LStatus", DbTool.FixSqlParameter(mdlog.LStatus))
                        ,					new SqlParameter("@Status", DbTool.FixSqlParameter(mdlog.Status))
                        ,					new SqlParameter("@OperationType", DbTool.FixSqlParameter(mdlog.OperationType))
                        ,					new SqlParameter("@Description", DbTool.FixSqlParameter(mdlog.Description))
                        ,					new SqlParameter("@CreateTime", DbTool.FixSqlParameter(mdlog.CreateTime))
                        ,					new SqlParameter("@UpdateTime", DbTool.FixSqlParameter(mdlog.UpdateTime))
                        ,					new SqlParameter("@UserName", DbTool.FixSqlParameter(mdlog.UserName))
                        ,					new SqlParameter("@Remark", DbTool.FixSqlParameter(mdlog.Remark))
                        ,					new SqlParameter("@HideContent", DbTool.FixSqlParameter(mdlog.HideContent))
                        };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql2.ToString(), parameters2);

                        #region 恢复订单 减掉回库数量
                        StringBuilder BackSql = new StringBuilder();
                        BackSql.AppendFormat(" update AwardsStatistics set BackTotal=BackTotal-1 where (AwardsType=1 and AwardsId={0}) or (AwardsType=2 and AwardsId={1})", model.AwardId, model.RedAwardId);
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, BackSql.ToString());
                        #endregion

                        if (rtn > 2)
                        {
                            trans.Commit();
                            return rtn;
                        }
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        rtn = 0;
                    }
                }
            }
            return 0;

        }

        public int UpdateAdds(Model.OrderInfoModel model, Model.Operation_LogModel mdlog)
        {
            int rtn = 0;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();

                        strSql.Append(" update OrderInfo set Title=@Title,Texts=@Texts,Tdate=@Tdate,Age=@Age where Id=@Id");
                        SqlParameter[] parameters = {	 
                               new SqlParameter("@Title", model.Title), 
                               new SqlParameter("@Texts", model.Texts), 
                               new SqlParameter("@Tdate", model.Tdate), 
                               new SqlParameter("@Age", model.Age), 
                               new SqlParameter("@Id", model.Id) 
                   
                             };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);

                        StringBuilder strSql2 = new StringBuilder();
                        strSql2.Append("insert into  [Operation_Log]");
                        strSql2.Append("(Mobile,OrderCode,LStatus,Status,OperationType,Description,CreateTime,UpdateTime,UserName,Remark,HideContent)");
                        strSql2.Append(" values (@Mobile,@OrderCode,@LStatus,@Status,@OperationType,@Description,@CreateTime,@UpdateTime,@UserName,@Remark,@HideContent)");
                        strSql2.Append(";select SCOPE_IDENTITY()");
                        SqlParameter[] parameters2 = {
					new SqlParameter("@Mobile", DbTool.FixSqlParameter(mdlog.Mobile))
                    ,					new SqlParameter("@OrderCode", DbTool.FixSqlParameter(mdlog.OrderCode))
                    ,					new SqlParameter("@LStatus", DbTool.FixSqlParameter(mdlog.LStatus))
                    ,					new SqlParameter("@Status", DbTool.FixSqlParameter(mdlog.Status))
                    ,					new SqlParameter("@OperationType", DbTool.FixSqlParameter(mdlog.OperationType))
                    ,					new SqlParameter("@Description", DbTool.FixSqlParameter(mdlog.Description))
                    ,					new SqlParameter("@CreateTime", DbTool.FixSqlParameter(mdlog.CreateTime))
                    ,					new SqlParameter("@UpdateTime", DbTool.FixSqlParameter(mdlog.UpdateTime))
                    ,					new SqlParameter("@UserName", DbTool.FixSqlParameter(mdlog.UserName))
                    ,					new SqlParameter("@Remark", DbTool.FixSqlParameter(mdlog.Remark))
                    ,					new SqlParameter("@HideContent", DbTool.FixSqlParameter(mdlog.HideContent))
                 };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql2.ToString(), parameters2);
                        if (rtn > 1)
                        {
                            trans.Commit();
                            return rtn;
                        }
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        rtn = 0;
                    }
                }
            }
            return 0;

        }

        public int BackAll(string DateStamp, Model.Operation_LogModel mdlog)
        {
            int rtn = 0;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();

                        strSql.Append(" update a set BackTotal=BackTotal+1 from AwardsStatistics a,OrderInfo o ");
                        strSql.Append(" where  FilesId=0 and o.DateStamp<=@DateStamp and o.IsBack=0 and ( ");
                        strSql.Append(" (AwardsType=1 and a.AwardsId=o.AwardId) or (AwardsType=2 and a.AwardsId=o.RedAwardId))");
                        SqlParameter[] parameters = {	 
                               new SqlParameter("@DateStamp", DateStamp),  
                   
                             };
                        if (SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters) > 0)
                            rtn += 1;

                        StringBuilder strSql2 = new StringBuilder();
                        strSql2.Append("update OrderInfo set IsBack=1  where  FilesId=0 and DateStamp<=@DateStamp and IsBack=0 ");
                        SqlParameter[] parameters2 = {
					            new SqlParameter("@DateStamp", DateStamp),  
                             };
                        if (SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql2.ToString(), parameters2) > 0)
                            rtn += 1;

                        if (rtn > 1)
                        {
                            trans.Commit();
                            return rtn;
                        }
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        rtn = 0;
                    }
                }
            }
            return 0;

        }

        public int ReissueHb(Model.OrderInfoModel model, Model.Operation_LogModel mdlog)
        {
            int rtn = 0;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();

                        strSql.Append(" update OrderInfo set HbOrderCode=@HbOrderCode where Id=@Id");
                        SqlParameter[] parameters = {	 
                               new SqlParameter("@HbOrderCode", model.HbOrderCode),  
                               new SqlParameter("@Id", model.Id) 
                   
                             };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);

                        StringBuilder strSql2 = new StringBuilder();
                        strSql2.Append("insert into  [Operation_Log]");
                        strSql2.Append("(Mobile,OrderCode,LStatus,Status,OperationType,Description,CreateTime,UpdateTime,UserName,Remark,HideContent)");
                        strSql2.Append(" values (@Mobile,@OrderCode,@LStatus,@Status,@OperationType,@Description,@CreateTime,@UpdateTime,@UserName,@Remark,@HideContent)");
                        strSql2.Append(";select SCOPE_IDENTITY()");
                        SqlParameter[] parameters2 = {
					new SqlParameter("@Mobile", DbTool.FixSqlParameter(mdlog.Mobile))
                    ,					new SqlParameter("@OrderCode", DbTool.FixSqlParameter(mdlog.OrderCode))
                    ,					new SqlParameter("@LStatus", DbTool.FixSqlParameter(mdlog.LStatus))
                    ,					new SqlParameter("@Status", DbTool.FixSqlParameter(mdlog.Status))
                    ,					new SqlParameter("@OperationType", DbTool.FixSqlParameter(mdlog.OperationType))
                    ,					new SqlParameter("@Description", DbTool.FixSqlParameter(mdlog.Description))
                    ,					new SqlParameter("@CreateTime", DbTool.FixSqlParameter(mdlog.CreateTime))
                    ,					new SqlParameter("@UpdateTime", DbTool.FixSqlParameter(mdlog.UpdateTime))
                    ,					new SqlParameter("@UserName", DbTool.FixSqlParameter(mdlog.UserName))
                    ,					new SqlParameter("@Remark", DbTool.FixSqlParameter(mdlog.Remark))
                    ,					new SqlParameter("@HideContent", DbTool.FixSqlParameter(mdlog.HideContent))
                 };
                        rtn += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql2.ToString(), parameters2);
                        if (rtn > 1)
                        {
                            trans.Commit();
                            return rtn;
                        }
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        rtn = 0;
                    }
                }
            }
            return 0;

        }
        #endregion

        #region Delete
        public int Del()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from OrderInfo ");
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString());
        }

        public int Del(List<Model.OrderInfoModel> modellist)
        {
            if (modellist == null || modellist.Count == 0)
                return 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("begin tran ");
            strSql.Append("begin try ");
            foreach (Model.OrderInfoModel model in modellist)
            {
                strSql.Append(@" DELETE FROM [OrderInfo] WHERE Id=");
                strSql.Append(model.Id + ";");
            }
            strSql.Append("COMMIT TRAN ");
            strSql.Append("SELECT 1 ");
            strSql.Append("end try ");
            strSql.Append("BEGIN CATCH ");
            strSql.Append("ROLLBACK TRAN ");
            strSql.Append("SELECT 0 ");
            strSql.Append("end catch ");

            try
            {
                return Convert.ToInt32(SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), new SqlParameter[] { }));
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        #endregion

        #region GetList
        public DataTable GetTopList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select top 100 * from OrderInfo where 1=1 ");
            sql.Append(sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }

        public DataTable GetCodeRepeatList(string Type, int Sum, string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select " + Type + " from OrderInfo where 1=1 " + sqlwhere + " group by " + Type + " having count(" + Type + ")>" + Sum);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }

        public DataTable GetGroupByTypeList(int TopNum, string Type)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select top " + TopNum + " " + Type + ",COUNT(Ip) Total from OrderInfo group by " + Type + " order by COUNT(" + Type + ") desc");
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
        #endregion

        #region CheckCount
        public int CheckFilesRepeatCount(string Hashdata)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select count(1) from OrderInfo where [FilesId] in(select Id from [FileInfo] where Hashdata='" + Hashdata + "') ");
            return DbTool.ConvertObject<int>(SqlHelper.ExecuteScalar(conn, CommandType.Text, sql.ToString()), 0);
        }
        #endregion

        #region GetModel

        public Model.OrderInfoModel GetModel(string sqlwhere)
        {

            string sql = "select top 1 * from OrderInfo where 1=1 " + sqlwhere;
            Model.OrderInfoModel model = new Model.OrderInfoModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            if (dr.Read())
            {
                BindDataReader(model, dr);
            }
            dr.Close();
            return model;
        }

        public Model.OrderInfoModel GetModelByCode(string Code)
        {

            string sql = "select top 1 * from OrderInfo where Code='" + Code + "'";
            Model.OrderInfoModel model = new Model.OrderInfoModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            if (dr.Read())
            {
                BindDataReader(model, dr);
            }
            dr.Close();
            return model;
        }

        public Model.OrderInfoModel GetModelByOpenId(string OpenId)
        {

            string sql = "select top 1 * from OrderInfo where OpenId='" + OpenId + "'";
            Model.OrderInfoModel model = new Model.OrderInfoModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            if (dr.Read())
            {
                BindDataReader(model, dr);
            }
            dr.Close();
            return model;
        }
        #endregion

        #region GetModelList
        public List<Model.OrderInfoModel> GetModelList(string sqlwhere)
        {

            List<Model.OrderInfoModel> result = new List<Model.OrderInfoModel>();
            string sql = "select * from OrderInfo where 1=1 " + sqlwhere;
            Model.OrderInfoModel model = new Model.OrderInfoModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            //var fields = DbTool.GetReaderFieldNames(dr);
            while (dr.Read())
            {
                //model = AutoBindDataReader(dr, fields);
                model = new Model.OrderInfoModel();
                BindDataReader(model, dr);
                result.Add(model);
            }
            dr.Close();
            return result;
        }

        public List<Model.OrderInfoModel> GetModelNotOCRList(int TopNum = 500)
        {

            List<Model.OrderInfoModel> result = new List<Model.OrderInfoModel>();
            string sql = "select top " + TopNum + " a.* from OrderInfo a left join dbo.ApiResultImgData r on a.OrderCode=r.Note where r.Note is null and a.FilesId>0";
            Model.OrderInfoModel model = new Model.OrderInfoModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            //var fields = DbTool.GetReaderFieldNames(dr);
            while (dr.Read())
            {
                //model = AutoBindDataReader(dr, fields);
                model = new Model.OrderInfoModel();
                BindDataReader(model, dr);
                result.Add(model);
            }
            dr.Close();
            return result;
        }

        public List<Model.OrderInfoModel> GetModelByTopList(int num, string sqlwhere)
        {

            List<Model.OrderInfoModel> result = new List<Model.OrderInfoModel>();
            string sql = "select top " + num + " * from OrderInfo where 1=1 " + sqlwhere;
            Model.OrderInfoModel model = new Model.OrderInfoModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            //var fields = DbTool.GetReaderFieldNames(dr);
            while (dr.Read())
            {
                //model = AutoBindDataReader(dr, fields);
                model = new Model.OrderInfoModel();
                BindDataReader(model, dr);
                result.Add(model);
            }
            dr.Close();
            return result;
        }
        #endregion

        #region GetExcelList
        /// <summary>
        /// 导出数据
        /// </summary>
        /// <param name="sqlstr">查询条件</param>
        /// <param name="ckName">显示名称</param>
        /// <param name="ckField">列</param>
        /// <param name="joinTab">左连接表</param>
        /// <param name="dataTable">导出表名</param>
        /// <param name="NoColum">取消导出列</param>
        /// <returns>DataTable</returns> 
        public DataTable GetExcelList(string sqlstr, string ckName, string ckField, string joinTab, string dataTable, string NoColum, bool isOCR = false)
        {
            string checksql = "";

            if (isOCR)
            {

                ckName += ";客户名称;超市名称;小票编号;小票流水号;购物总金额;购物时间;商品名称;商品个数;商品金额;商品合计";
                ckField += ";p.Customer;p.StoreName;p.StoreNum;p.SerialNumber;p.MaxMonery;p.ShoppingTime;I.Commodity;I.Num;I.Monery;I.Total";

                joinTab += "left join fileinfo f on a.[FilesId]=f.Id left join ApiImgProduct as p on p.note=a.OrderCode left join ApiImgProductInfo as I on I.note=a.OrderCode ";
            }

            var _name = ckName.Split(';');
            var _field = ckField.Split(';');
            var _NoColum = NoColum.Split(',');

            for (int i = 0; i < _name.Length; i++)
            {
                if (_NoColum.Contains(i.ToString()) == false)
                {
                    checksql += _field[i] + " as " + _name[i];
                    checksql += ",";
                }
            }
            string sql = "select " + checksql.TrimEnd(',') + " from " + dataTable + " a " + joinTab + " where 1=1 " + sqlstr;

            DataTable dt = SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());

            if (isOCR)
            {
                var ordercode = "";

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    if (dt.Rows[i]["订单号"].ToString() != ordercode)
                    {
                        ordercode = dt.Rows[i]["订单号"].ToString();
                    }
                    else
                    {
                        for (int t = 0; t < _name.Length - 3; t++)
                        {
                            if (_NoColum.Contains(t.ToString()) == false)
                            {
                                dt.Rows[i][_name[t]] = DBNull.Value;
                            }
                        }
                    }
                }
            }

            return dt;

        }

        /// <summary>
        /// Vue导出数据
        /// </summary>
        /// <param name="sqlstr">查询条件</param> 
        /// <param name="joinTab">左连接表</param>
        /// <param name="dataTable">导出表名</param>
        /// <param name="NoColum">取消导出列</param>
        /// <returns>DataTable</returns> 
        public DataTable GetVueExcelList(string sqlstr, string ExportData, string joinTab, string dataTable, bool isOCR = false)
        {
            if (isOCR)
            {

                ExportData += ",p.Customer as 客户名称,p.StoreName as 超市名称,p.SerialNumber as 小票流水号";
                ExportData += ",p.MaxMonery as 购物总金额,p.ShoppingTime as 购物时间,I.Commodity as 商品名称,I.Num as 商品个数";
                ExportData += ",I.Monery as 商品金额,I.Total as 商品合计";

                joinTab += "left join ApiImgProduct as p on p.note=a.OrderCode left join ApiImgProductInfo as I on I.note=a.OrderCode ";
            }

            string sql = "select " + ExportData + " from " + dataTable + " a " + joinTab + " where 1=1 " + sqlstr;

            DataTable dt = SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());

            if (isOCR)
            {
                var ordercode = "";

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    if (dt.Rows[i]["编号"].ToString() != ordercode)
                    {
                        ordercode = dt.Rows[i]["编号"].ToString();
                    }
                    else
                    {
                        for (int t = 0; t < dt.Columns.Count - 9; t++)
                        {
                            dt.Rows[i][t] = DBNull.Value;
                        }
                    }
                }
            }

            return dt;

        }


        #endregion

        #region CheckCount
        public int CheckCount(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select count(1) from OrderInfo where 1=1 ");
            sql.Append(sqlwhere);
            return DbTool.ConvertObject<int>(SqlHelper.ExecuteScalar(conn, CommandType.Text, sql.ToString()), 0);
        }
        #endregion

        #region 分页计算总数
        public int GetCount(string sqlstr, string joinString, bool IsFile = true)
        {
            Model.PageInfo pages = new Model.PageInfo();
            pages.SqlWhere = IsFile ? sqlstr : sqlstr.Replace(";f.Hashdata", "");
            pages.ReturnFileds = "Id";
            pages.SqlWhere = sqlstr;
            pages.TableName = "OrderInfo";
            pages.JoinTable = IsFile ? " left join fileinfo f on a.[FilesId]=f.Id left join ApiImgRotate r on a.[ordercode]=r.note left join ApiImgProduct ap on a.[ordercode]=ap.note" : "";
            pages.CountFields = " a.Id ";
            pages.OrderString = " ";
            pages.SelectFileds = "a.*";
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
        public DataTable GetList(string sqlstr, int pageindex, int pagesize, bool IsFile = true)
        {
            Model.PageInfo pages = new Model.PageInfo();
            pages.PageIndex = pageindex;
            pages.PageSize = pagesize;
            pages.SqlWhere = sqlstr;
            pages.ReturnFileds = "t.*";
            pages.TableName = " OrderInfo ";
            pages.JoinTable = IsFile ? " left join fileinfo f on a.[FilesId]=f.Id left join ApiImgRotate r on a.[ordercode]=r.note left join ApiImgProduct ap on a.[ordercode]=ap.note" : "";
            pages.CountFields = " a.Id";
            pages.OrderString = " order by a.Id asc ";
            pages.SelectFileds = IsFile ? " a.*,r.NewImgurl,r.NewImgsign,f.Hashdata,f.FileName,f.SaveName,(select COUNT(*) from fileinfo ff left join [OrderInfo]oo on oo.[FilesId]=ff.Id where ff.Hashdata=f.Hashdata and oo.Id is not null ) as HashCount " : "a.*";
            pages.doCount = 0;
            PageHelper p = new PageHelper();
            DataTable dt = p.GetList(pages);
            return dt;
        }
        #endregion

        #endregion
         
 
    }
}
