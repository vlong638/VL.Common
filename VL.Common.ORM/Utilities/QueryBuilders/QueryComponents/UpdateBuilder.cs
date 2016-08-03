using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using VL.Common.DAS.Objects;

namespace VL.Common.ORM.Utilities.QueryBuilders
{
    public class UpdateBuilder: IQueryBuilder
    {
        private ComponentOfWhere componentWhere;
        private ComponentOfSet componentSet;

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

        public override string ToQueryString(DbSession session)
        {
            return string.Format("update {0}{1}{2}", TableName, ComponentSet.ToQueryString(session)
                , ComponentWhere.Wheres.Count > 0 ? ComponentWhere.ToQueryString(session) : "");
        }
        public override void AppendQueryParameter(ref DbCommand command, DbSession session)
        {
            ComponentWhere.Wheres.AddParameter(session, command);
            ComponentSet.Values.AddParameter(session, command);
        }
    }
}
