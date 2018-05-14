using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Db;


namespace Db
{
    class PageHelper
    {

        public DataTable GetList(Model.PageInfo pmodel)
        {
            SqlParameter[] parameters = {
                                            new SqlParameter("@tblName", SqlDbType.NVarChar, 100),
                                            new SqlParameter("@SelectFileds", SqlDbType.NVarChar, 2000),
                                            new SqlParameter("@PageSize", SqlDbType.Int,4),  
                                            new SqlParameter("@PageIndex", SqlDbType.Int, 4),      
                                            new SqlParameter("@doCount", SqlDbType.Int,2),
                                            new SqlParameter("@JoinTable", SqlDbType.NVarChar,4000),
                                            new SqlParameter("@CountFields", SqlDbType.VarChar, 1000),
                                            new SqlParameter("@ReturnFileds", SqlDbType.NVarChar, 4000),
                                            new SqlParameter("@strWhere", SqlDbType.NVarChar, 2000),
                                            new SqlParameter("@OrderString", SqlDbType.NVarChar,1000)
                                             
                                        };

            parameters[0].Value = pmodel.TableName;
            if (pmodel.SelectFileds != null)
            {
                parameters[1].Value = pmodel.SelectFileds;
            }
            else
            {
                parameters[1].Value = "";
            }
            parameters[2].Value = pmodel.PageSize;
            parameters[3].Value = pmodel.PageIndex;
            parameters[4].Value = pmodel.doCount;

            if (pmodel.JoinTable != null)
            {
                parameters[5].Value = pmodel.JoinTable;
            }
            else
            {
                parameters[5].Value = "";
            }


            if (pmodel.CountFields != null)
            {
                parameters[6].Value = pmodel.CountFields;
            }
            else
            {
                parameters[6].Value = " id ";
            }


            if (pmodel.ReturnFileds != null)
            {
                parameters[7].Value = pmodel.ReturnFileds;
            }
            else
            {
                parameters[7].Value = "*";
            }


            if (pmodel.SqlWhere != null)
            {
                parameters[8].Value = pmodel.SqlWhere;
            }
            else
            {
                parameters[8].Value = "";
            }

            parameters[9].Value = pmodel.OrderString;


            DataTable ds = SqlHelper.ExecuteDataTable(SqlHelper.ConnectionString, CommandType.StoredProcedure, "sp_Page", parameters);

            return ds;
        }



    }


}