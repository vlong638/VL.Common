using System;
using VL.Common.DAS;
using VL.Common.ORM;

namespace VL.Common.Protocol.IService//.IORM
{
    public static class IORMExtensions
    {
        /// <summary>
        /// 提供对应数据库的数据库语句构建类
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static IDbQueryBuilder GetDbQueryBuilder(this DbSession session)
        {
            IDbQueryBuilder result;
            switch (session.DatabaseType)
            {
                case EDatabaseType.MSSQL:
                    result = new MSSQLQueryBuilder();
                    break;
                case EDatabaseType.MySQL:
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
                    result = new MSSQLQueryOperator();
                    break;
                case EDatabaseType.Oracle:
                case EDatabaseType.MySQL:
                //TODO 未支持多数据库
                default:
                    throw new NotImplementedException("未为该类型" + session.DatabaseType + "实现对应的QueryOperator");
            }
            return result;
        }
    }
}
