using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Db
{
    public class GenerationOrderIdDal
    { 
         
        #region MyCode

        public static int GetOrderNumber(int activityId) 
        {
            int result = 0; 
            string sql = string.Format("INSERT INTO [GenerationOrderId] (SalesActivityId,[CreateOn]) VALUES ({0},GETDATE());select SCOPE_IDENTITY()", activityId);
            var obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.Text, sql.ToString(), null);
            result = Convert.ToInt32(obj); 

            return result;
        }

        #endregion
        
 
    }
}
