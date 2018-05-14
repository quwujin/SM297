using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;
using System.Web.UI.WebControls;

namespace Db
{
    public class CommonFunctionDal
    {
        public string FromValueGetText(CommonModel comodel)
        {
            string OutTextStr = "";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select " + comodel.ReturnText + " from " + comodel.TableName + " where 1=1  " + comodel.WhereStr + "");

            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.Text, strSql.ToString());
            if (obj != null)
            {
                OutTextStr = obj.ToString();
            }
            return OutTextStr;
        }


        public DataTable GetSelectTable(CommonModel model)
        {
            StringBuilder strSql = new StringBuilder();
            string ReturnStr = "";
            if (!String.IsNullOrEmpty(model.ReturnValue))
            {
                ReturnStr += model.ReturnValue + ",";
            }
            if (!String.IsNullOrEmpty(model.ReturnText))
            {
                ReturnStr += model.ReturnText + ",";
            }
            if (!String.IsNullOrEmpty(ReturnStr))
            {
                ReturnStr = ReturnStr.TrimEnd(',');
            }
            strSql.Append("select " + ReturnStr + " from " + model.TableName + " where 1=1  " + model.WhereStr + "");
            if (!String.IsNullOrEmpty(model.OrderStr))
            {
                strSql.Append(" order by " + model.OrderStr);
            }

            DataTable dt = SqlHelper.ExecuteDataTable(SqlHelper.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (dt != null)
            {
                return dt;
            }
            else
            {
                return null;
            }
        }

        public DataTable GetSelectTables(CommonModel model)
        {
            StringBuilder strSql = new StringBuilder();
            string ReturnStr = "";
         
            if (!String.IsNullOrEmpty(model.ReturnText))
            {
                ReturnStr += model.ReturnText + ",";
            }
            if (!String.IsNullOrEmpty(ReturnStr))
            {
                ReturnStr = ReturnStr.TrimEnd(',');
            }
            strSql.Append("select " + ReturnStr + " from " + model.TableName + " where 1=1  " + model.WhereStr + "");
            if (!String.IsNullOrEmpty(model.OrderStr))
            {
                strSql.Append(" order by " + model.OrderStr);
            }

            DataTable dt = SqlHelper.ExecuteDataTable(SqlHelper.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (dt != null)
            {
                return dt;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 绑定下拉菜单
        /// </summary>
        /// <param name="model"></param>
        /// <param name="DDLName"></param>
        /// <param name="AddType">1全部,2请选择3 空 0 无</param>
        public void DataBindDropDownList(string ReturnText, string ReturnValue, string TableName, string WhereStr, string OrderStr, DropDownList DDLName, int AddType)
        {
            CommonModel model = new CommonModel();
            model.ReturnText = ReturnText;
            model.ReturnValue = ReturnValue;
            model.TableName = TableName;
            model.WhereStr = WhereStr;
            model.OrderStr = OrderStr;

            DataTable DT = GetSelectTable(model);

            DDLName.DataTextField = ReturnText;
            DDLName.DataValueField = ReturnValue;
            DDLName.DataSource = DT;
            DDLName.DataBind();
          
            if (AddType == 1)
            {
                DDLName.Items.Insert(0, new ListItem("全部", "0"));
            }
            else if (AddType == 2)
            {
                DDLName.Items.Insert(0, new ListItem("=请选择= ", "0"));
            }
            else if (AddType == 3)
            {
                DDLName.Items.Insert(0, new ListItem(" ", "0"));
            }
            DT.Dispose();
        }



        /// <summary>
        /// 绑定下拉菜单
        /// </summary>
        /// <param name="model"></param>
        /// <param name="DDLName"></param>
        /// <param name="AddType">1全部,2请选择3 空 0 无</param>
        public void DataBindDropDownList(string ReturnText, string TableName, string WhereStr, string OrderStr, DropDownList DDLName, int AddType)
        {
            CommonModel model = new CommonModel();
            model.ReturnText = ReturnText;
            model.ReturnValue = ReturnText;
            model.TableName = TableName;
            model.WhereStr = WhereStr;
            model.OrderStr = OrderStr;

            DataTable DT = GetSelectTable(model);

            DDLName.DataTextField = ReturnText;
            DDLName.DataValueField = ReturnText;
            DDLName.DataSource = DT;
            DDLName.DataBind();

            if (AddType == 1)
            {
                DDLName.Items.Insert(0, new ListItem("全部", "0"));
            }
            else if (AddType == 2)
            {
                DDLName.Items.Insert(0, new ListItem("=请选择= ", "0"));
            }
            else if (AddType == 3)
            {
                DDLName.Items.Insert(0, new ListItem(" ", "0"));
            }
            DT.Dispose();
        }

        public void DataBindCheckBoxList(string ReturnText, string ReturnValue, string TableName, string WhereStr, string OrderStr, CheckBoxList CBLName)
        {
            CommonModel model = new CommonModel();
            model.ReturnText = ReturnText;
            model.ReturnValue = ReturnValue;
            model.TableName = TableName;
            model.WhereStr = WhereStr;
            model.OrderStr = OrderStr;

            DataTable DT = GetSelectTable(model);

            CBLName.DataTextField = ReturnText;
            CBLName.DataValueField = ReturnValue;
            CBLName.DataSource = DT;
            CBLName.DataBind();

            DT.Dispose();
        }

        /// <summary>
        /// 根据菜单地址，返回对应的菜单ID
        /// </summary>
        /// <param name="url"></param>
        /// <param name="bid"></param>
        /// <returns></returns>
        public string GeMenuSid(string url, string bid) {
         //   StringBuilder sql = new StringBuilder("select MenuId from MenuInfo where MenuUrl like '%" + url + "%' and bid=" + bid);

           // DataTable dt = SqlHelper.ExecuteDataTable(SqlHelper.ConnectionString, CommandType.Text, sql.ToString());
           // if (dt.Rows.Count > 0)
           // {
           //     return dt.Rows[0][0].ToString();
          //  }
          //  else
          ////  {
                //return "0";
           // } 

            return "0";
        }


        public string GetName(string TextShow, string TableName, string SqlWhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select " + TextShow + " from " + TableName + " where 1=1  " + SqlWhere);

            DataTable dt = SqlHelper.ExecuteDataTable(SqlHelper.ConnectionString, CommandType.Text, sql.ToString());
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            else
            {
                return "未知";
            }
        }


        public string GetNameTopOne(string TextShow, string TableName, string SqlWhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select top 1 " + TextShow + " from " + TableName + " where 1=1  " + SqlWhere);

            DataTable dt = SqlHelper.ExecuteDataTable(SqlHelper.ConnectionString, CommandType.Text, sql.ToString());
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }


        public DataTable GetList(string sql)
        {
            return SqlHelper.ExecuteDataTable(SqlHelper.ConnectionString, CommandType.Text, sql);
        }

    }
}
