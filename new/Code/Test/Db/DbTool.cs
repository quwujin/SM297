using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;

namespace Db
{
    public class DbTool
    {
        public static object FixSqlParameter(object obj)
        {
            object _result;
            if (obj != null)
            {
                try
                {
                    if (obj is DateTime)
                    {
                        if (((DateTime)obj).Year == 1)
                        {
                            _result = DBNull.Value;
                        }
                        else
                        {

                            _result = String.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", obj);
                        }
                    }
                    else
                    {
                        _result = obj;
                    }
                    
                }
                catch
                {

                    return DBNull.Value;
                }
            }
            else 
            {
                return DBNull.Value;
            }

            return _result;
        }


        public static T ConvertObject<T>(object obj)
        {
            T _result = default(T);
            if (obj != null)
            {
                try
                {
                    _result = (T)Convert.ChangeType(obj, typeof(T));
                }
                catch { };
            }

            return _result;
        }


        public static T ConvertObject<T>(object val, T defaultVal)
        {
            if (Convert.IsDBNull(val) || val == null)
                return defaultVal;
            else
            {
                try
                {
                    return (T)Convert.ChangeType(val, typeof(T));
                }
                catch
                {
                    return defaultVal;
                }
            }
        }


        public static object GetDataRow(SqlDataReader dr,string fieldname) 
        {
            if (dr.GetSchemaTable().Columns.Contains(fieldname)) 
            {
                return dr[fieldname];
            }
            else
            {
                return null;
            }
        }


        #region SqlParameter Helper

        public static SqlParameter[] CopyParameters(params SqlParameter[] parames)
        {
            SqlParameter[] ps = new SqlParameter[parames.Length];
            for (int i = 0; i < parames.Length; i++)
            {
                ps[i] = (SqlParameter)((ICloneable)parames[i]).Clone();
            }
            return ps;
        }

        public static string[] GetReaderFieldNames(DbDataReader reader)
        {
            var fields = new string[reader.FieldCount];
            for (var i = 0; i < reader.FieldCount; i++)
            {
                fields[i] = reader.GetName(i);
            }
            return fields;
        }

        public static bool HasFields(string field, params string[] fields)
        {
            if (fields == null || fields.Length == 0)
                return true;
            return fields.Contains(field);
        }

        public static string GetParameterName(string name)
        {
            if (name.StartsWith("@"))
            {
                return name;
            }
            return "@" + name;
        }

        public static SqlParameter CreateParameter(string name, object value)
        {
            name = GetParameterName(name);
            if (value == null)
            {
                return new SqlParameter(name, DBNull.Value);
            }
            return new SqlParameter(name, value);
        }

        public static SqlParameter CreateOutParameter(string name, SqlDbType dbType)
        {
            name = GetParameterName(name);
            var p = new SqlParameter(name, dbType);
            p.Direction = ParameterDirection.Output;
            return p;
        }

        public static SqlParameter CreateReturnParameter(SqlDbType dbType)
        {
            return CreateReturnParameter("@ret", dbType);
        }

        public static SqlParameter CreateReturnParameter(string name, SqlDbType dbType)
        {
            name = GetParameterName(name);
            var p = new SqlParameter(name, dbType);
            p.Direction = ParameterDirection.ReturnValue;
            return p;
        }
        #endregion


    }
}
