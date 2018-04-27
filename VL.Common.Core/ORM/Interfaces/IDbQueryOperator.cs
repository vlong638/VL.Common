using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Text;
using VL.Common.Core.DAS;

namespace VL.Common.Core.ORM//.Utilities.QueryOperators
{
    /// <summary>
    /// 数据库操作类
    /// 提供数据库操作类的规范
    /// </summary>
    public abstract class IDbQueryOperator
    {
        #region Log
        ///TODO 其实ORM的Operator对日志记录的需求需要对外界的日志记录工具产生一个依赖
        ///可以依赖于ServiceContext的某一个日志记录工具
        /// <summary>
        /// 日志输出文件夹
        /// </summary>
        public static string DirectoryPath
        {
            get
            {
                if (string.IsNullOrEmpty(_directoryPath))
                {
                    _directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
                    if (!Directory.Exists(_directoryPath))
                    {
                        Directory.CreateDirectory(_directoryPath);
                    }
                }
                return _directoryPath;
            }
        }
        private static string _directoryPath;
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
                    //if (!File.Exists(_filePath))
                    //{
                    //    File.Create(_filePath);
                    //}
                }
                return _filePath;
            }
        }
        private static string _filePath;
        /// <summary>
        /// 日志锁
        /// </summary>
        public static object LogLocker = new object();

        /// <summary>
        /// 输出日志
        /// </summary>
        public void WriteQueryLog(DbCommand command, DbSession session)
        {
            if (session.IsLogQuery)
            {
                lock (LogLocker)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("--------------");
                    sb.AppendLine("执行时间:" + DateTime.Now);
                    sb.AppendLine("执行语句:" + command.CommandText);
                    foreach (DbParameter Parameter in command.Parameters)
                    {
                        sb.AppendLine(string.Format("参数名:{0}={1}", Parameter.ParameterName, Parameter.Value));
                    }
                    File.AppendAllText(FilePath, sb.ToString());
                }
            }

        }
        #endregion

        protected DbSession Session { set; get; }

        #region 基于QueryBuilder的查询方法
        /// <summary>
        /// 返回 是否有数据受操作影响
        /// </summary>
        public abstract bool Insert<T>(DbQueryBuilder queryBuilder) where T : VLModel_DB, new();
        /// <summary>
        /// 返回 是否有数据受操作影响
        /// </summary>
        public abstract bool Insert<T>(InsertBuilder insertBuilder) where T : VLModel_DB, new();
        /// <summary>
        /// 返回 是否有数据受操作影响
        /// </summary>
        public abstract bool InsertAll<T>(DbQueryBuilder queryBuilder) where T : VLModel_DB, new();
        /// <summary>
        /// 返回 是否有数据受操作影响
        /// </summary>
        public abstract bool Delete<T>(DbQueryBuilder queryBuilder) where T : VLModel_DB, new();
        /// <summary>
        /// 返回 是否有数据受操作影响
        /// </summary>
        public abstract bool Delete<T>(DeleteBuilder deleteBuilder) where T : VLModel_DB, new();
        /// <summary>
        /// 返回 是否有数据受操作影响
        /// </summary>
        public abstract bool Update<T>(DbQueryBuilder queryBuilder) where T : VLModel_DB, new();
        /// <summary>
        /// 返回 是否有数据受操作影响
        /// </summary>
        public abstract bool Update<T>(UpdateBuilder updateBuilder) where T : VLModel_DB, new();
        /// <summary>
        /// 返回 是否有数据受操作影响
        /// </summary>
        public abstract bool UpdateAll<T>(DbQueryBuilder queryBuilder) where T : VLModel_DB, new();
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public abstract T Select<T>(DbQueryBuilder queryBuilder) where T : VLModel_DB, new();
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public abstract T Select<T>(SelectBuilder selectBuilder) where T : VLModel_DB, new();
        /// <summary>
        /// 未查询到数据时返回 New List()
        /// 单个SelectBuilder查询一组数据
        /// </summary>
        public abstract List<T> SelectAll<T>(DbQueryBuilder queryBuilder) where T : VLModel_DB, new();
        /// <summary>
        /// 未查询到数据时返回 New List()
        /// 单个SelectBuilder查询一组数据
        /// </summary>
        public abstract List<T> SelectAll<T>(SelectBuilder selectBuilder) where T : VLModel_DB, new();
        /// <summary>
        /// 未查询到数据时返回 New List()
        /// 单个SelectBuilder查询一组数据
        /// </summary>
        public abstract List<T> SelectAll<T>() where T : VLModel_DB, new();//IDbSession session
        /// <summary>
        /// 未查询到数据时返回 New List()
        /// 多个SelectBuilder组合查询
        /// </summary>
        public abstract List<T> SelectUnion<T>(DbQueryBuilder queryBuilder) where T : VLModel_DB, new();
        ///// <summary>
        ///// 未查询到数据时返回 New List()
        ///// </summary>
        //public abstract List<T> SelectAll<T>(DbQueryBuilder queryBuilder) where T : IPDMTBase, new();
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public abstract bool? SelectAsBool<T>(SelectBuilder selectBuilder) where T : VLModel_DB, new();
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public abstract short? SelectAsShort<T>(SelectBuilder selectBuilder) where T : VLModel_DB, new();
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public abstract int? SelectAsInt<T>(SelectBuilder selectBuilder) where T : VLModel_DB, new();
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public abstract long? SelectAsLong<T>(SelectBuilder selectBuilder) where T : VLModel_DB, new();
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public abstract Guid? SelectAsGuid<T>(SelectBuilder selectBuilder) where T : VLModel_DB, new();
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public abstract string SelectAsString<T>(SelectBuilder selectBuilder) where T : VLModel_DB, new();
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public abstract List<string> SelectAsStrings<T>(SelectBuilder selectBuilder) where T : VLModel_DB, new();
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public abstract DateTime? SelectAsDateTime<T>(SelectBuilder selectBuilder) where T : VLModel_DB, new();
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public abstract DbDataReader SelectAsDataReader<T>(SelectBuilder selectBuilder) where T : VLModel_DB, new();
        #endregion
    }
}
