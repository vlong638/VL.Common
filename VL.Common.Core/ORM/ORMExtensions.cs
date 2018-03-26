using System;
using VL.Common.Core.DAS;
using VL.Common.Core.ORM;

namespace VL.Common.Core.ORM//.IService.IORM
{
    public static class ORMEx
    {
        /// <summary>
        /// 提供对应数据库的数据库语句构建类
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static DbQueryBuilder GetDbQueryBuilder(this DbSession session)
        {
            DbQueryBuilder result;
            switch (session.DatabaseType)
            {
                case EDatabaseType.MSSQL:
                case EDatabaseType.MySQL://TODO MySQL暂时启用MSSQL方案
                case EDatabaseType.SQLite:// TODO SQLite暂时启用MSSQL方案
                     result = new DbQueryBuilder();
                    break;
                case EDatabaseType.Oracle:
                //TODO 未支持多数据库
                default:
                    throw new NotImplementedException("未实现该类别的QueryBuilder" + session.DatabaseType);
            }
            return result;
        }
        /// <summary>
        /// 提供对应数据库的操作类
        /// </summary>
        public static IDbQueryOperator GetQueryOperator(this DbSession session)
        {
            IDbQueryOperator result;
            switch (session.DatabaseType)
            {
                case EDatabaseType.MSSQL:
                case EDatabaseType.MySQL://TODO MySQL暂时启用MSSQL方案
                    result = new MSSQLQueryOperator(session);
                    break;
                case EDatabaseType.SQLite:
                    result = new SQLiteQueryOperator(session);
                    break;
                case EDatabaseType.Oracle:
                //TODO 未支持多数据库
                default:
                    throw new NotImplementedException("未为该类型" + session.DatabaseType + "实现对应的QueryOperator");
            }
            return result;
        }
    }
}
