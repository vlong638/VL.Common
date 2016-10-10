using System;
using System.Data.Common;
using VL.Common.DAS.Objects;
using VL.Common.ORM.Utilities.Interfaces;

namespace VL.Common.ORM.Utilities.QueryBuilders
{
    /// <summary>
    /// Delete相关的过滤条件
    /// </summary>
    public class DeleteBuilder : IQueryBuilder
    {
        private ComponentOfWhere componentWhere;

        public ComponentOfWhere ComponentWhere
        {
            get
            {
                if (componentWhere == null)
                {
                    componentWhere = new ComponentOfWhere(this);
                }
                return componentWhere;
            }
            set
            {
                componentWhere = value;
            }
        }

        /// <summary>
        /// 添加query语句所对应的参数
        /// </summary>
        /// <param name="command"></param>
        /// <param name="session"></param>
        public override void AddParameter(DbCommand command, DbSession session)
        {
            ComponentWhere.AddParameter(command, session);
        }
        /// <summary>
        /// 构建query
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public override string ToQueryString(DbSession session)
        {
            return string.Format("delete from {0}{1}", TableName
                , ComponentWhere.ToQueryString(session));
        }
    }
}
