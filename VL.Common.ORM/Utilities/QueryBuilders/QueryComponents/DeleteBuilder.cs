using System.Data.Common;
using VL.Common.DAS.Objects;

namespace VL.Common.ORM.Utilities.QueryBuilders
{
    /// <summary>
    /// Delete相关的过滤条件
    /// </summary>
    public class DeleteBuilder : IQueryBuilder
    {
        private ComponentWhere componentWhere;

        public ComponentWhere ComponentWhere
        {
            get
            {
                if (componentWhere == null)
                {
                    componentWhere = new ComponentWhere(this);
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
            command.AddParameter(session, ComponentWhere.Wheres);
        }

        public override string ToQueryString(DbSession session, string tableName)
        {
            return string.Format("delete from {0}{1}", tableName
                , ComponentWhere.Wheres.Count > 0 ? " where " + ComponentWhere.ToQueryComponentOfWheres(session) : "");
        }
    }
}
