using System;
using System.Data.Common;
using VL.Common.DAS.Objects;
using VL.Common.ORM.Utilities.Interfaces;

namespace VL.Common.ORM.Utilities.QueryBuilders
{
    /// <summary>
    /// Insert 语句构建器
    /// </summary>
    public class InsertBuilder : IQueryBuilder
    {
        private ComponentOfInsert componentInsert;
        /// <summary>
        /// Insert 语法段
        /// </summary>
        public ComponentOfInsert ComponentInsert
        {
            get
            {
                if (componentInsert == null)
                {
                    componentInsert = new ComponentOfInsert(this);
                }
                return componentInsert;
            }

            set
            {
                componentInsert = value;
            }
        }

        /// <summary>
        /// 添加query语句所对应的参数
        /// </summary>
        /// <param name="command"></param>
        /// <param name="session"></param>
        public override void AddParameter(DbCommand command, DbSession session)
        {
            ComponentInsert.AddParameter(command, session);
        }
        /// <summary>
        /// 构建query
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public override string ToQueryString(DbSession session)
        {
            return string.Format("insert into {0}{1}", TableName , ComponentInsert.ToQueryString(session));
        }
    }
}
