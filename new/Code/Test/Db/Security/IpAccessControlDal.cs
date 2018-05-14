using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Db.Security
{
    public class IpAccessControlDal
    {
        public string conn = SqlHelper.ConnectionString;



        #region Dal Core Functional

        #region Add
        public int Add(Model.Security.IpAccessControlModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into  [IpAccessControl]");
            strSql.Append("(LogType,IpAddress,AccessCount,Islocked,FistDateTime,UpdateDate)");
            strSql.Append(" values (@LogType,@IpAddress,@AccessCount,@Islocked,@FistDateTime,@UpdateDate)");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
					new SqlParameter("@LogType", DbTool.FixSqlParameter(model.LogType))
,					new SqlParameter("@IpAddress", DbTool.FixSqlParameter(model.IpAddress))
,					new SqlParameter("@AccessCount", DbTool.FixSqlParameter(model.AccessCount))
,					new SqlParameter("@Islocked", DbTool.FixSqlParameter(model.Islocked))
,					new SqlParameter("@FistDateTime", DbTool.FixSqlParameter(model.FistDateTime))
,					new SqlParameter("@UpdateDate", DbTool.FixSqlParameter(model.UpdateDate))
                 };


            return DbTool.ConvertObject<int>(SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters), 0);

        }


        #endregion

        #region Update
        public int Update(Model.Security.IpAccessControlModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update IpAccessControl set ");
            strSql.Append("LogType=@LogType,IpAddress=@IpAddress,AccessCount=@AccessCount,Islocked=@Islocked,FistDateTime=@FistDateTime,UpdateDate=@UpdateDate ");
            strSql.Append(" where Id=@Id ");

            SqlParameter[] parameters = {
					new SqlParameter("@LogType", DbTool.FixSqlParameter(model.LogType))
,					new SqlParameter("@IpAddress", DbTool.FixSqlParameter(model.IpAddress))
,					new SqlParameter("@AccessCount", DbTool.FixSqlParameter(model.AccessCount))
,					new SqlParameter("@Islocked", DbTool.FixSqlParameter(model.Islocked))
,					new SqlParameter("@FistDateTime", DbTool.FixSqlParameter(model.FistDateTime))
,					new SqlParameter("@UpdateDate", DbTool.FixSqlParameter(model.UpdateDate))
,					new SqlParameter("@Id", model.Id)
                 };


            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }
        #endregion

        #region Delete
        public int Del(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from IpAccessControl where Id = " + id);
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString());
        }
        #endregion

        #region BindDataReader
        protected void BindDataReader(Model.Security.IpAccessControlModel model, SqlDataReader dr)
        {

            model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
            model.LogType = DbTool.ConvertObject<System.String>(dr["LogType"]);
            model.IpAddress = DbTool.ConvertObject<System.String>(dr["IpAddress"]);
            model.AccessCount = DbTool.ConvertObject<System.Int32>(dr["AccessCount"]);
            model.Islocked = DbTool.ConvertObject<System.Boolean>(dr["Islocked"]);
            model.FistDateTime = DbTool.ConvertObject<System.DateTime>(dr["FistDateTime"]);
            model.UpdateDate = DbTool.ConvertObject<System.DateTime>(dr["UpdateDate"]);


        }
        #endregion

        #endregion 

        #region GetList
        public DataTable GetList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from IpAccessControl where 1=1 ");
            sql.Append(sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region GetModel
        public Model.Security.IpAccessControlModel GetModelByAddressId(string addressId,string logtype)
        {

            string sql = "select top 1 * from IpAccessControl where IpAddress ='" + addressId + "' and logtype='" + logtype + "'";
            Model.Security.IpAccessControlModel model = new Model.Security.IpAccessControlModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            if (dr.Read()) {
                 BindDataReader(model, dr);
            }
            dr.Close();
            return model;
        }
         #endregion
         
        #region GetModelList
        public List<Model.Security.IpAccessControlModel> GetModelList()
        {

            List<Model.Security.IpAccessControlModel> result = new List<Model.Security.IpAccessControlModel>();
            string sql = "select * from IpAccessControl where 1=1";
            Model.Security.IpAccessControlModel model = new Model.Security.IpAccessControlModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            while (dr.Read())
            {
                 model = new Model.Security.IpAccessControlModel(); 
                 BindDataReader(model, dr);
                 result.Add(model);
            }
            dr.Close();
            return result;
        }
         #endregion


        #region Update Function

        public int ClearLockedIp(Model.Security.IpAccessControlSettingModel logsetting)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(string.Format("delete from IpAccessControl where Islocked=1 and datediff(minute,FistDateTime,'{0}')>{1} and LogType='{2}' and IsBlackIP=0;", System.DateTime.Now, logsetting.IPAccessControlTime + logsetting.IPAccessControlLockTime, logsetting.LogType));
            strSql.Append(string.Format("delete from IpAccessControl where Islocked=0 and datediff(minute,FistDateTime,'{0}')>{1} and LogType='{2}';", System.DateTime.Now, logsetting.IPAccessControlTime, logsetting.LogType));
            strSql.Append(string.Format("delete from IpAccessControl where Islocked=0 and datediff(minute,FistDateTime,'{0}')>{1} and LogType='{2}' and AccessCount<6", System.DateTime.Now, 60, logsetting.LogType));
   
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), null);

        }
         

        #endregion

        #region Update IPCount
        /// <summary>
        /// 检查当前访问IP是否可用
        /// </summary>
        /// <param name="logAccess">是否记录当前访问</param>
        /// <param name="logtype">控制类型</param>
        /// <returns></returns>
        public static bool CheckIpIsOK(bool logAccess, string logtype, string IP)
        { 
            var logsetting = new IpAccessControlSettingDal().GetModelByLogtype(logtype);
            return CheckIpIsOK(logAccess, logsetting, IP, "", "", "");
            
        }

        /// <summary>
        /// 检查当前访问IP是否可用
        /// </summary>
        /// <param name="logAccess">是否记录当前访问</param>
        /// <param name="logtype">控制类型</param>
        /// <returns></returns>
        public static bool CheckIpIsOK(bool logAccess, Model.Security.IpAccessControlSettingModel logsetting, string IP, string LockValue, string LockReason, string SourceURL = "")
        {
            bool _result = true;
            //var logsetting = new IpAccessControlSettingDal().GetModelByLogtype(logtype);
            //考虑性能这里的参数固定写死
            // Model.Security.IpAccessControlSettingModel logsetting = new Model.Security.IpAccessControlSettingModel();
            if (logsetting == null)
            {
                return _result;
            }
            if (logsetting.IPAccessEnable)
            {
                IpAccessControlDal dal = new IpAccessControlDal();

                #region Clear LockedIp

                dal.ClearLockedIp(logsetting);

                #endregion

                #region Process

                string ip = IP;

                var model = dal.GetModelByAddressId(ip, logsetting.LogType);
                if (model.Id > 0)
                {
                    if (model.Islocked)
                    {
                        _result = false;
                    }
                    else
                    {
                        _result = true;

                        #region logAccess

                        if (logAccess)
                        {
                            model.AccessCount += 1;
                            if (model.AccessCount >= logsetting.IPAccessMaxCount)
                            {
                                _result = false;

                                #region LockIP
                                model.Islocked = true;
                                model.UpdateDate = DateTime.Now;
                                dal.Update(model);
                                #endregion

                            }
                            else
                            {
                                model.UpdateDate = DateTime.Now;
                                dal.Update(model);
                            }

                            #region add lock log
                            new IpAccessControlLogDal().Add(new Model.Security.IpAccessControlLogModel()
                            {
                                IpAddress = ip,
                                LockedDate = System.DateTime.Now,
                                LogType = logsetting.LogType,
                                CreateOn = DateTime.Now,
                                LockReason = LockReason,
                                LockValue = LockValue,
                                SourceURL = SourceURL,

                            });
                            #endregion

                        }

                        #endregion

                    }

                }
                else
                {
                    _result = true;

                    if (logAccess)
                    {
                        #region Log IP
                        model.FistDateTime = DateTime.Now;
                        model.AccessCount += 1;
                        model.IpAddress = ip;
                        model.LogType = logsetting.LogType;
                        model.Islocked = false;
                        dal.Add(model);
                        #endregion
                    }

                }


                #endregion

            }
            return _result;

        }
        #endregion



       
 
    }
}
