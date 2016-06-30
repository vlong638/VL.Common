using System;
using System.Collections.Generic;
using System.IO;
using VL.Common.DAS.Objects;
using VL.Common.ORM.Objects;
using VL.Common.ORM.Utilities.QueryBuilders;

namespace VL.Common.ORM.Utilities.QueryOperators
{
    /// <summary>
    /// 数据库操作类
    /// 提供数据库操作类的规范
    /// </summary>
    public abstract class IDbQueryOperator
    {
        #region Log
        private static string _directoryPath;
        private static string _filePath;

        /// <summary>
        /// 是否输出查询字符串
        /// </summary>
        public bool IsLogQuery { set; get; } = false;
        /// <summary>
        /// 日志输出文件夹
        /// </summary>
        public static string DirectoryPath
        {
            get
            {
                if (string.IsNullOrEmpty(_directoryPath))
                {
                    _directoryPath = Path.Combine(Environment.CurrentDirectory, "Logs");
                    if (!Directory.Exists(_directoryPath))
                    {
                        Directory.CreateDirectory(_directoryPath);
                    }
                }
                return _directoryPath;
            }
        }
        /// <summary>
        /// 日志输出文件
        /// </summary>
        public static string FilePath
        {
            get
            {
                if (string.IsNullOrEmpty(_filePath))
                {
                    _filePath = Path.Combine(DirectoryPath, nameof(ORM) + ".txt");
                    if (!File.Exists(_filePath))
                    {
                        File.Create(_filePath);
                    }
                }
                return _filePath;
            }
        }
        /// <summary>
        /// 日志锁
        /// </summary>
        public static object LogLocker = new object();

        /// <summary>
        /// 输出日志
        /// </summary>
        /// <param name="log"></param>
        public void WriteLog(string log)
        {
            if (IsLogQuery)
            {
                lock (LogLocker)
                {
                    File.AppendAllText(FilePath, log + System.Environment.NewLine);
                }
            }
        } 
        #endregion

        //0630迁移至Protocol
        ///// <summary>
        ///// 不同的数据库 对应操作的优化 不同
        ///// </summary>
        //public static IDbQueryOperator GetQueryOperator(DbSession session)
        //{
        //    switch (session.DatabaseType)
        //    {
        //        case EDatabaseType.MSSQL:
        //            return new MSSQLQueryOperator();
        //        case EDatabaseType.Oracle:
        //        case EDatabaseType.MySQL:
        //        //TODO 未支持多数据库
        //        default:
        //            throw new NotImplementedException("未为该类型" + session.DatabaseType + "实现对应的QueryOperator");
        //    }
        //}

        #region 基于QueryBuilder的查询方法
        /// <summary>
        /// 返回 是否有数据受操作影响
        /// </summary>
        public abstract bool Insert<T>(DbSession session, IDbQueryBuilder queryBuilder) where T : IPDMTBase, new();
        /// <summary>
        /// 返回 是否有数据受操作影响
        /// </summary>
        public abstract bool Insert<T>(DbSession session, InsertBuilder insertBuilder) where T : IPDMTBase, new();
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
        public abstract bool Delete<T>(DbSession session, DeleteBuilder deleteBuilder) where T : IPDMTBase, new();
        /// <summary>
        /// 返回 是否有数据受操作影响
        /// </summary>
        public abstract bool Update<T>(DbSession session, IDbQueryBuilder queryBuilder) where T : IPDMTBase, new();
        /// <summary>
        /// 返回 是否有数据受操作影响
        /// </summary>
        public abstract bool Update<T>(DbSession session, UpdateBuilder updateBuilder) where T : IPDMTBase, new();
        /// <summary>
        /// 返回 是否有数据受操作影响
        /// </summary>
        public abstract bool UpdateAll<T>(DbSession session, IDbQueryBuilder queryBuilder) where T : IPDMTBase, new();
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public abstract T Select<T>(DbSession session, IDbQueryBuilder queryBuilder) where T : IPDMTBase, new();
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public abstract T Select<T>(DbSession session, SelectBuilder selectBuilder) where T : IPDMTBase, new();
        /// <summary>
        /// 未查询到数据时返回 New List()
        /// 单个SelectBuilder查询一组数据
        /// </summary>
        public abstract List<T> SelectAll<T>(DbSession session, IDbQueryBuilder queryBuilder) where T : IPDMTBase, new();
        /// <summary>
        /// 未查询到数据时返回 New List()
        /// 单个SelectBuilder查询一组数据
        /// </summary>
        public abstract List<T> SelectAll<T>(DbSession session, SelectBuilder selectBuilder) where T : IPDMTBase, new();
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
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public abstract int? SelectAsInt(DbSession session, SelectBuilder selectBuilder);
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public abstract long? SelectAsLong(DbSession session, SelectBuilder selectBuilder);
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public abstract string SelectAsString(DbSession session, SelectBuilder selectBuilder);
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public abstract DateTime? SelectAsDateTime(DbSession session, SelectBuilder selectBuilder);
        #endregion
    }
}
