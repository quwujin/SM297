using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common
{
    public static class ReplaceErorrSql
    {

        //防SQL注入正则表达式2
        private static Regex _sqlkeywordregex2 = new Regex(@"(select|insert|delete|from|count\(|drop|table|update|truncate|asc\(|mid\(|char\(|xp_cmdshell|exec|master|local|group|administrators|user|or|and|\*|\')", RegexOptions.IgnoreCase);

        public static string FixSql(this string sqlvalue)
        {
            if (string.IsNullOrEmpty(sqlvalue))
            {
                return "";
            }
            else  
            {
                return sqlvalue.Replace("'", "''").Replace("<script>", "").Replace("alert", "").Replace("</","");
            }
        }

        /// <summary>    
        /// 删除SQL注入特殊字符    
        /// 解然 20070622加入对输入参数sql为Null的判断    
        /// </summary>    
        public static string StripSQLInjection(string sql)
        {
            if (!string.IsNullOrEmpty(sql))
            {
                //过滤 ' --    
                string pattern1 = @"(\%27)|(\')|(\-\-)";

                //防止执行 ' or    
                string pattern2 = @"((\%27)|(\'))\s*((\%6F)|o|(\%4F))((\%72)|r|(\%52))";

                //防止执行sql server 内部存储过程或扩展存储过程    
                string pattern3 = @"\s+exec(\s|\+)+(s|x)p\w+";

                sql = Regex.Replace(sql, pattern1, string.Empty, RegexOptions.IgnoreCase);
                sql = Regex.Replace(sql, pattern2, string.Empty, RegexOptions.IgnoreCase);
                sql = Regex.Replace(sql, pattern3, string.Empty, RegexOptions.IgnoreCase);
            }
            return sql;
        }

        /// <summary>
        /// 替换特殊字符
        /// </summary>
        /// <param name="hexData">字符串</param>
        /// <returns></returns>
        public static string RemoveSpecialCharacter(string hexData)
        {
            return Regex.Replace(hexData, "[ \\[ \\] \\^ \\-_*×――(^)$%~!@#$…&%￥—+=<>《》!！??？*/:：•`·、.;\"‘’“”-]", "").ToUpper();
        }

    }
}
