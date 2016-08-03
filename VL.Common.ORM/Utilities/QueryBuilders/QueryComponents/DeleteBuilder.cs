using System.Data.Common;
using VL.Common.DAS.Objects;

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

        public override void AppendQueryParameter(ref DbCommand command, DbSession session)
        {
            ComponentWhere.Wheres.AddParameter(session, command);
        }

        public override string ToQueryString(DbSession session)
        {
            return string.Format("delete from {0}{1}", TableName
                , ComponentWhere.Wheres.Count > 0 ? ComponentWhere.ToQueryString(session) : "");
        }
    }
}
