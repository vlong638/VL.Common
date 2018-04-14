using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Text;
using VL.Common.Core.DAS;

namespace VL.Common.Core.ORM
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
        public static DbParameter GetDbParameter(DbSession session, string name, object value)
        {
            switch (session.DatabaseType)
            {
                case EDatabaseType.MSSQL:
                    return new SqlParameter(name, value);
                case EDatabaseType.Oracle:
                    return new OracleParameter(name, value);
                case EDatabaseType.MySQL:
                    return new MySqlParameter(name, value);
                case EDatabaseType.SQLite:
                    return new SQLiteParameter(name, value);
                default:
                    throw new NotImplementedException("未支持该类型数据库的参数生成" + session.DatabaseType.ToString());
            }
        }
        /// <summary>
        /// 获取对应数据库的参数对象
        /// </summary>
        public static DbParameter GetDbParameter(this PDMDbProperty property, DbSession session, object value, string nickName)//TODO =null
        {
            switch (session.DatabaseType)
            {
                case EDatabaseType.MSSQL:
                    return new SqlParameter(nickName ?? property.Title, value.GetType().IsEnum ? (int)value : value);
                case EDatabaseType.Oracle:
                    return new OracleParameter(!string.IsNullOrEmpty(nickName) ? nickName : property.Title, value.GetType().IsEnum ? (int)value : value);
                case EDatabaseType.MySQL:
                    return new MySqlParameter(!string.IsNullOrEmpty(nickName) ? nickName : property.Title, value.GetType().IsEnum ? (int)value : value);
                 case EDatabaseType.SQLite:
                    return new SQLiteParameter(!string.IsNullOrEmpty(nickName) ? nickName : (property.IsKeyWord ? "[" + property.Title + "]" : property.Title), value.GetType().IsEnum ? (int)value : value);
                default:
                    throw new NotImplementedException("未支持该类型数据库的参数生成" + session.DatabaseType.ToString());
            }
        }
        /// <summary>
        /// 转译为对应数据库的查询语句
        /// </summary>
        public static string ToQueryString(this LocateType operatorType)
        {
            switch (operatorType)
            {
                case LocateType.Equal:
                    return "=";
                case LocateType.NotEqual:
                    return "!=";
                case LocateType.In:
                    return "in";
                case LocateType.NotIn:
                    return "not in";
                case LocateType.GreatThan:
                    return ">";
                case LocateType.LessThan:
                    return "<";
                default:
                    throw new NotImplementedException("未支持该类型的操作符" + operatorType.ToString());
            }
        }
        /// <summary>
        /// 转译为对应数据库的查询语句
        /// </summary>
        public static string ToQueryString(this UpdateType updateType)
        {
            switch (updateType)
            {
                case UpdateType.SetValue:
                    return "=";
                case UpdateType.IncreaseByValue:
                    return "+=";
                default:
                    throw new NotImplementedException("未支持该类型的操作符" + updateType.ToString());
            }
        }
        /// <summary>
        /// 转译为对应数据库的字符串
        /// </summary>
        /// <param name="fieldAliases"></param>
        /// <returns></returns>
        public static string ToSQLString(this List<ComponentValueOfSelect> fieldAliases)
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
        public static string ToSQLString(this ComponentValueOfSelect fieldAlias)
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
