using System.Data.Common;
using VL.Common.Object.DAS;

namespace VL.Common.Object.ORM//.Utilities.Interfaces
{
    /// <summary>
    /// query构建器,负责query的组织和构建
    /// </summary>
    public abstract class IQueryBuilder : IQueriable
    {
        /// <summary>
        /// 基于表的query
        /// </summary>
        public string TableName { get; set; }
        // TODO可以是虚拟的表,基于Select的Query

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string ToQueryString(DbSession session, string tableName)
        {
            TableName = tableName;
            return ToQueryString(session);
        }

        /// <summary>
        /// 构建query语句
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public abstract string ToQueryString(DbSession session);
        /// <summary>
        /// 添加query语句所对应的参数
        /// </summary>
        /// <param name="command"></param>
        /// <param name="session"></param>
        public abstract void AddParameter(DbCommand command, DbSession session);
    }
}
