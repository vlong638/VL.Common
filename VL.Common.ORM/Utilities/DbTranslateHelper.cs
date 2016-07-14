using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using VL.Common.DAS.Objects;
using VL.Common.ORM.Objects;
using VL.Common.ORM.Utilities.QueryBuilders;

namespace VL.Common.ORM.Utilities
{
    /// <summary>
    /// 数据库兼容
    /// </summary>
    public static class DbTranslateHelper
    {
        /// <summary>
        /// 获取对应数据库的参数对象
        /// </summary>
        /// <param name="session"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DbParameter GetDbParameter(DbSession session,string name, object value)
        {
            switch (session.DatabaseType)
            {
                case EDatabaseType.MSSQL:
                    return new SqlParameter(name, value);
                case EDatabaseType.Oracle:
                    return new OracleParameter(name, value);
                case EDatabaseType.MySQL:
                    return new MySqlParameter(name, value);
                default:
                    throw new NotImplementedException("未支持该类型数据库的参数生成" + session.DatabaseType.ToString());
            }
        }
        /// <summary>
        /// 获取对应数据库的参数对象
        /// </summary>
        public static DbParameter GetDbParameter(this PDMDbProperty property, DbSession session, object value,string nickName)//TODO =null
        {
            switch (session.DatabaseType)
            {
                case EDatabaseType.MSSQL:
                    return new SqlParameter(nickName??property.Title, value);
                case EDatabaseType.Oracle:
                    return new OracleParameter(nickName ?? property.Title, value);
                case EDatabaseType.MySQL:
                    return new MySqlParameter(nickName ?? property.Title, value);
                default:
                    throw new NotImplementedException("未支持该类型数据库的参数生成" + session.DatabaseType.ToString());
            }
        }
        /// <summary>
        /// 转译为对应数据库的查询语句
        /// </summary>
        /// <param name="operatorType"></param>
        /// <returns></returns>
        public static string ToQueryString(this OperatorType operatorType)
        {
            switch (operatorType)
            {
                case OperatorType.Equal:
                    return "=";
                case OperatorType.NotEqual:
                    return "!=";
                case OperatorType.In:
                    return "in";
                case OperatorType.NotIn:
                    return "not in";
                default:
                    throw new NotImplementedException("未支持该类型的操作符" + operatorType.ToString());
            }
        }
        /// <summary>
        /// 转译为对应数据库的字符串
        /// </summary>
        /// <param name="fieldAliases"></param>
        /// <returns></returns>
        public static string ToSQLString(this List<FieldAlias> fieldAliases)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var fieldAlias in fieldAliases)
            {
                if (string.IsNullOrEmpty(fieldAlias.Alias))
                {
                    sb.Append("," + fieldAlias.FieldName);
                }
                else
                {
                    sb.Append("," + fieldAlias.FieldName + " " + fieldAlias.Alias);
                }
            }
            return sb.ToString().TrimStart(',');
        }
        /// <summary>
        /// 转译为对应数据库的字符串
        /// </summary>
        /// <param name="fieldAlias"></param>
        /// <returns></returns>
        public static string ToSQLString(this FieldAlias fieldAlias)
        {
            if (string.IsNullOrEmpty(fieldAlias.Alias))
            {
                return fieldAlias.FieldName;
            }
            else
            {
                return fieldAlias.FieldName + " " + fieldAlias.Alias;
            }
        }
    }
}
