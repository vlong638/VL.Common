using System;
using System.Collections.Generic;
using VL.Common.DAS.Objects;
using VL.Common.ORM.Objects;
using VL.Common.ORM.Utilities.QueryBuilders;

namespace VL.Common.ORM.DbOperateLib.Utilities.QueryOperators
{
    public abstract class IDbQueryOperator
    {
        /// <summary>
        /// 不同的数据库 对应操作的优化 不同
        /// </summary>
        /// <param name="databaseType"></param>
        /// <returns></returns>
        public static IDbQueryOperator GetQueryOperator(DbSession session)
        {
            switch (session.DatabaseType)
            {
                case EDatabaseType.MSSQL:
                    return new MSSQLQueryOperator();
                case EDatabaseType.Oracle:
                case EDatabaseType.MySQL:
                //TODO 未支持多数据库
                default:
                    throw new NotImplementedException("未为该类型" + session.DatabaseType + "实现对应的QueryOperator");
            }
        }

        #region 基于QueryBuilder的查询方法
        /// <summary>
        /// 返回 是否有数据受操作影响
        /// </summary>
        public abstract bool Insert<T>(DbSession session, IDbQueryBuilder queryBuilder) where T : IPDMTBase, new();
        /// <summary>
        /// 返回 是否有数据受操作影响
        /// </summary>
        public abstract bool InsertAll<T>(DbSession session, IDbQueryBuilder queryBuilder) where T : IPDMTBase, new();
        /// <summary>
        /// 返回 是否有数据受操作影响
        /// </summary>
        public abstract bool Delete<T>(DbSession session, IDbQueryBuilder queryBuilder) where T : IPDMTBase, new();
        /// <summary>
        /// 返回 是否有数据受操作影响
        /// </summary>
        public abstract bool Update<T>(DbSession session, IDbQueryBuilder queryBuilder) where T : IPDMTBase, new();
        /// <summary>
        /// 返回 是否有数据受操作影响
        /// </summary>
        public abstract bool UpdateAll<T>(DbSession session, IDbQueryBuilder queryBuilder) where T : IPDMTBase, new();
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public abstract T Select<T>(DbSession session, IDbQueryBuilder queryBuilder) where T : IPDMTBase, new();
        /// <summary>
        /// 未查询到数据时返回 New List()
        /// 单个SelectBuilder查询一组数据
        /// </summary>
        public abstract List<T> SelectAll<T>(DbSession session, IDbQueryBuilder queryBuilder) where T : IPDMTBase, new();
        /// <summary>
        /// 未查询到数据时返回 New List()
        /// 单个SelectBuilder查询一组数据
        /// </summary>
        public abstract List<T> SelectAll<T>(DbSession session) where T : IPDMTBase, new();
        /// <summary>
        /// 未查询到数据时返回 New List()
        /// 多个SelectBuilder组合查询
        /// </summary>
        public abstract List<T> SelectUnion<T>(DbSession session, IDbQueryBuilder queryBuilder) where T : IPDMTBase, new();
        ///// <summary>
        ///// 未查询到数据时返回 New List()
        ///// </summary>
        //public abstract List<T> SelectAll<T>(DbSession session, IDbQueryBuilder queryBuilder) where T : IPDMTBase, new();
        #endregion
    }
}
