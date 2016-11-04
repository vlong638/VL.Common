using System;
using System.Data.Common;
using System.Linq;
using VL.Common.DAS.Objects;
using VL.Common.ORM.Utilities.Interfaces;

namespace VL.Common.ORM.Utilities.QueryBuilders
{
    /// <summary>
    /// Update 语句构建器
    /// </summary>
    public class UpdateBuilder: IQueryBuilder
    {
        private ComponentOfWhere componentWhere;
        private ComponentOfSet componentSet;
        /// <summary>
        /// Where 语法段
        /// </summary>
        public ComponentOfWhere ComponentWhere
        {
            get
            {
                if (componentWhere==null)
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
        /// Set 语法段
        /// </summary>
        public ComponentOfSet ComponentSet
        {
            get
            {
                if (componentSet == null)
                {
                    componentSet = new ComponentOfSet(this);
                }
                return componentSet;
            }
            set
            {
                componentSet = value;
            }
        }

        /// <summary>
        /// 构建query
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public override string ToQueryString(DbSession session)
        {
            return string.Format("update {0}{1}{2}", TableName, ComponentSet.ToQueryString(session)
                , ComponentWhere.ToQueryString(session));
        }
        /// <summary>
        ///  添加query语句所对应的参数
        /// </summary>
        /// <param name="command"></param>
        /// <param name="session"></param>
        public override void AddParameter(DbCommand command, DbSession session)
        {
            ComponentWhere.AddParameter(command, session);
            ComponentSet.AddParameter(command, session);
        }
    }
}
