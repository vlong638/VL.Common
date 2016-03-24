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

namespace VL.ORM.DbOperateLib.Utilities
{
    public static class DbTranslateHelper
    {
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
        public static DbParameter GetDbParameter(this PDMDbProperty property, DbSession session, object value)
        {
            switch (session.DatabaseType)
            {
                case EDatabaseType.MSSQL:
                    return new SqlParameter(property.Title, value);
                case EDatabaseType.Oracle:
                    return new OracleParameter(property.Title, value);
                case EDatabaseType.MySQL:
                    return new MySqlParameter(property.Title, value);
                default:
                    throw new NotImplementedException("未支持该类型数据库的参数生成" + session.DatabaseType.ToString());
            }
        }
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
